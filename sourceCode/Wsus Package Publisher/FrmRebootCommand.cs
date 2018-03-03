using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Wsus_Package_Publisher
{
    internal partial class FrmRebootCommand : Form
    {
        private struct CommandParameters
        {
            internal ADComputer TargetComputer { get; set; }
            internal string CommandLine { get; set; }
            internal string Login { get; set; }
            internal string Password { get; set; }
            internal DataGridViewRow Row { get; set; }
        }

        private WrongCredentialsWatcher _wrongCrendentialsWatcher;
        private List<ADComputer> _targetComputers = new List<ADComputer>();
        private string _login = string.Empty;
        private string _password = string.Empty;
        private bool _closing = false;
        private int _success = 0;
        private int _failed = 0;
        private string localComputerName = string.Empty;
        System.Threading.CountdownEvent countDown;
        private System.Threading.Thread sendThread;
        private object wmiLock = new object();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmRebootCommand).Assembly);

        internal FrmRebootCommand(List<ADComputer> targetComputers, string login, string password)
        {
            InitializeComponent();
            _wrongCrendentialsWatcher = new WrongCredentialsWatcher();
            _wrongCrendentialsWatcher.IsWrongCredentials = false;
            _wrongCrendentialsWatcher.IsAbortRequested = false;
            _wrongCrendentialsWatcher.ContinueWithFailedCredentials = false;

            _closing = false;

            _targetComputers = targetComputers;
            _login = login;
            _password = password;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.PersonalizedRebootMessage))
                txtBxMessage.Text = Properties.Settings.Default.PersonalizedRebootMessage;

            localComputerName = Environment.MachineName;
            string domaineName = ADHelper.GetDomainName();
            if (!string.IsNullOrEmpty(domaineName) && !localComputerName.EndsWith(domaineName))
                localComputerName += "." + domaineName;
        }

        #region (Private Methods - Méthodes Privées)

        private void LockUI(bool isLock)
        {
            rdBtnReboot.Enabled = !isLock;
            rdBtnShutdown.Enabled = !isLock;
            nupTimer.Enabled = !isLock;
            chkBxForceClosing.Enabled = !isLock;
            chkBxIncludeMessage.Enabled = !isLock;
            txtBxMessage.Enabled = !isLock;
            btnSend.Enabled = !isLock;
            btnAbort.Enabled = !btnSend.Enabled;
            btnClose.Enabled = !isLock;
        }

        private CommandParameters GetCommonParameters()
        {
            CommandParameters parameters = new CommandParameters();

            if (rdBtnReboot.Checked)
                parameters.CommandLine = "Shutdown -r";
            else
                parameters.CommandLine = "Shutdown -s";
            if (chkBxForceClosing.Checked)
                parameters.CommandLine += " -f";
            parameters.CommandLine += " -t " + nupTimer.Value.ToString();
            if (chkBxIncludeMessage.Checked && !string.IsNullOrEmpty(txtBxMessage.Text) && nupTimer.Value != 0)
                parameters.CommandLine += " -c \"" + txtBxMessage.Text.Replace("[TIMER]", nupTimer.Value.ToString()) + "\"";
            parameters.CommandLine += " -d p:2:17";

            return parameters;
        }

        private void SendCommandASynch()
        {
            countDown = new System.Threading.CountdownEvent(1);

            foreach (DataGridViewRow row in dgvComputers.Rows)
            {
                if (!_closing && !_wrongCrendentialsWatcher.IsAbortRequested)
                {
                    countDown.AddCount();
                    CommandParameters parameters = GetCommonParameters();
                    parameters.Row = row;
                    parameters.TargetComputer = (ADComputer)row.Cells["ADComputer"].Value;

                    parameters.Login = _login;
                    parameters.Password = _password;
                    System.Threading.ThreadPool.QueueUserWorkItem(SendCommandToComputer, (object)parameters);
                }
            }
            countDown.Signal();
            countDown.Wait(5000 * dgvComputers.Rows.Count);
            DisplayProgress();
            Action action = () =>
            {
                dgvComputers.Sort(dgvComputers.Columns["Result"], ListSortDirection.Descending);
                btnClose.Enabled = true;
                btnAbort.Enabled = false;
            };
            if (!_closing)
                this.Invoke(action);
        }

        private void SendCommandToComputer(object args)
        {
            CommandParameters parameters = (CommandParameters)args;
            DataGridViewRow row = parameters.Row;
            ADComputer computer = parameters.TargetComputer;

            if (!_wrongCrendentialsWatcher.IsAbortRequested && !_closing)
            {
                try
                {
                    DisplayMessage(row, resMan.GetString("Pinging"));

                    if (computer.Ping(100))
                    {
                        Logger.Write(computer.Name + " Ping ok");
                        DisplayMessage(row, resMan.GetString("SendingCommand"));

                        ConnectionOptions connOptions = new ConnectionOptions();
                        connOptions.Impersonation = ImpersonationLevel.Impersonate;
                        connOptions.EnablePrivileges = true;
                        if (parameters.Login != null && parameters.Password != null && computer.Name.ToLower() != localComputerName.ToLower())
                        {
                            connOptions.Username = parameters.Login;
                            connOptions.Password = parameters.Password;
                        }
                        ManagementScope mgmtScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", computer.Name), connOptions);

                        if (!_wrongCrendentialsWatcher.IsAbortRequested && !_closing)
                        {
                            lock (wmiLock)
                            {
                                try
                                {
                                    if (!_wrongCrendentialsWatcher.IsAbortRequested && !_closing)
                                        mgmtScope.Connect();
                                    else
                                    {
                                        DisplayMessage(row, resMan.GetString("Aborted"));
                                        ChangeBackColor(row, Color.Gainsboro);
                                        _failed++;
                                        DisplayProgress();
                                    }
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    _wrongCrendentialsWatcher.IsWrongCredentials = true;
                                    DisplayMessage(row, resMan.GetString("Exception"));
                                    ChangeBackColor(row, Color.Silver);
                                    _failed++;
                                    DisplayProgress();

                                    if (!_wrongCrendentialsWatcher.ContinueWithFailedCredentials)
                                    {
                                        if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                        {
                                            Logger.Write("interrupt on failed credentials.");
                                            _wrongCrendentialsWatcher.IsAbortRequested = true;
                                            DisplayMessage(row, resMan.GetString("Aborted"));
                                            ChangeBackColor(row, Color.Gainsboro);
                                        }
                                        else
                                        {
                                            Logger.Write("Continue with bad credentials.");
                                            _wrongCrendentialsWatcher.ContinueWithFailedCredentials = true;
                                        }
                                    }
                                }
                            }
                            if (mgmtScope.IsConnected && !_closing)
                            {
                                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                                ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                                ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                                inParams["CommandLine"] = parameters.CommandLine;
                                if (!_wrongCrendentialsWatcher.IsAbortRequested)
                                {
                                    processClass.InvokeMethod("Create", inParams, null);
                                    processClass.Dispose();
                                    mgmtPath = null;
                                    objectGetOptions = null;
                                    mgmtScope = null;
                                    connOptions = null;
                                    DisplayMessage(row, resMan.GetString("CommandSent"));
                                    ChangeBackColor(row, Color.LightGreen);
                                    _success++;
                                    DisplayProgress();
                                }
                            }
                        }
                        else
                        {
                            DisplayMessage(row, resMan.GetString("Aborted"));
                            ChangeBackColor(row, Color.Gainsboro);
                        }
                    }
                    else
                    {
                        Logger.Write(computer.Name + " don't Ping");
                        DisplayMessage(row, resMan.GetString("Noping"));
                        ChangeBackColor(row, Color.Silver);
                        _failed++;
                        DisplayProgress();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("**** " + computer.Name + " throw an execption : " + ex.Message);
                    DisplayMessage(row, resMan.GetString("Exception"));
                    ChangeBackColor(row, Color.Silver);
                    _failed++;
                    DisplayProgress();
                }
                finally { countDown.Signal(); }
            }
            else
                countDown.Signal();
        }

        private void DisplayMessage(DataGridViewRow row, string message)
        {
            Action action = () =>
            {
                lock (dgvComputers)
                {
                    row.Cells["Result"].Value = message;
                    dgvComputers.Refresh();
                }
            };
            if (!_closing)
                this.Invoke(action);
        }

        private void DisplayProgress()
        {
            Action action = () =>
            {
                lock (prgBrCommand)
                {
                    prgBrCommand.Value = _success + _failed;
                    lblResult.Text = resMan.GetString("Summary") + " : " + resMan.GetString("Succeeded") + " = " + _success + "   " + resMan.GetString("Failed") + " = " + _failed + " " + resMan.GetString("On") + " " + (_success + _failed);
                }
            };
            if (!_closing)
                this.Invoke(action);
        }

        private void ChangeBackColor(DataGridViewRow row, Color backColor)
        {
            Action action = () =>
            {
                lock (dgvComputers)
                {
                    row.Cells["Result"].Style.BackColor = backColor;
                    dgvComputers.Refresh();
                }
            };
            if (!_closing)
                this.Invoke(action);
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Responses to events - Réponses aux évènements)

        private void FrmRebootCommand_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            dgvComputers.Rows.Clear();
            foreach (ADComputer targetComputer in _targetComputers)
            {
                dgvComputers.Rows.Add(targetComputer.Name, string.Empty, targetComputer);
            }
        }

        private void chkBxIncludeMessage_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Write(chkBxIncludeMessage);
            txtBxMessage.Enabled = chkBxIncludeMessage.Checked;
        }

        private void FrmRebootCommand_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.EnteringMethod();
            _closing = true;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            prgBrCommand.Value = 0;
            prgBrCommand.Maximum = dgvComputers.Rows.Count;
            LockUI(true);
            _wrongCrendentialsWatcher.IsAbortRequested = false;
            _success = 0;
            _failed = 0;

            sendThread = new System.Threading.Thread(new System.Threading.ThreadStart(SendCommandASynch));
            sendThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            sendThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            sendThread.Start();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            _wrongCrendentialsWatcher.IsAbortRequested = true;
            btnAbort.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _wrongCrendentialsWatcher.IsAbortRequested = true;
            this.Close();
        }

        #endregion (Responses to events - Réponses aux évènements)

    }
}
