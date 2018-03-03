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
    public partial class FrmRemoteExecution : Form
    {
        private struct CommandParameters
        {
            internal List<ADComputer> TargetComputers { get; set; }
            internal string CommandLine { get; set; }
            internal string Login { get; set; }
            internal string Password { get; set; }
        }

        private static bool _cancel = false;
        private string localComputerName = string.Empty;
        private int success = 0;
        private int failed = 0;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmRemoteExecution).Assembly);

        public FrmRemoteExecution()
        {
            Logger.EnteringMethod("FrmRemoteExecution");
            InitializeComponent();
            localComputerName = Environment.MachineName;
        }

        internal void SendDetectNow(List<ADComputer> targetComputers, string login, string password)
        {
            Logger.EnteringMethod("On " + targetComputers.Count + " computers. With Login : " + login);
            CommandParameters parameters = new CommandParameters();

            parameters.TargetComputers = targetComputers;
            parameters.CommandLine = @"WuAuClt /DetectNow";
            parameters.Login = login;
            parameters.Password = password;
            success = 0;
            failed = 0;

            prgBarSendCommand.Value = 1;
            prgBarSendCommand.Maximum = targetComputers.Count + 1;
            prgBarSendCommand.Refresh();

            System.Threading.Thread commandThread = new System.Threading.Thread(SendCommand);
            commandThread.Start(parameters);
        }

        internal void SendReportNow(List<ADComputer> targetComputers, string login, string password)
        {
            Logger.EnteringMethod("On : " + targetComputers.Count + " computers. With Login " + login);
            CommandParameters parameters = new CommandParameters();

            parameters.TargetComputers = targetComputers;
            parameters.CommandLine = @"WuAuClt /ReportNow";
            parameters.Login = login;
            parameters.Password = password;
            success = 0;
            failed = 0;

            prgBarSendCommand.Value = 1;
            prgBarSendCommand.Maximum = targetComputers.Count + 1;
            prgBarSendCommand.Refresh();

            System.Threading.Thread commandThread = new System.Threading.Thread(SendCommand);
            commandThread.Start(parameters);
        }

        private void SendCommand(Object sendParameters)
        {
            Logger.EnteringMethod();
            _cancel = false;

            Action startAction = () =>
            {
                btnClose.Enabled = false;
                btnCancel.Enabled = true;
            };
            this.Invoke(startAction);

            CommandParameters parameters = (CommandParameters)sendParameters;
            bool credentialFailed = false;

            foreach (ADComputer computer in parameters.TargetComputers)
            {
                if (_cancel)
                    break;
                Logger.Write(computer.Name);
                DataGridViewRow row = null;
                Action addRow = () =>
                {
                    dtgvRemoteExecution.Rows.Add(computer.Name);
                    int index = dtgvRemoteExecution.Rows.GetLastRow(DataGridViewElementStates.None);
                    row = dtgvRemoteExecution.Rows[index];
                    dtgvRemoteExecution.CurrentCell = dtgvRemoteExecution[0, index];
                };
                this.Invoke(addRow);

                try
                {
                    Action pingAction = () =>
                    {
                        row.Cells["Result"].Value = resMan.GetString("Pinging");
                        dtgvRemoteExecution.Refresh();
                    };
                    this.Invoke(pingAction);

                    if (computer.Ping(100))
                    {
                        Logger.Write(computer.Name + " Ping ok");
                        Action sendCommandAction = () =>
                        {
                            row.Cells["Result"].Value = resMan.GetString("SendingCommand");
                            dtgvRemoteExecution.Refresh();
                        };
                        this.Invoke(sendCommandAction);

                        if (_cancel)
                            break;
                        ConnectionOptions connOptions = new ConnectionOptions();
                        connOptions.Impersonation = ImpersonationLevel.Impersonate;
                        connOptions.EnablePrivileges = true;
                        if (parameters.Login != null && parameters.Password != null && computer.Name.ToLower() != localComputerName.ToLower())
                        {
                            Logger.Write("Setting credentials");
                            connOptions.Username = parameters.Login;
                            connOptions.Password = parameters.Password;
                        }
                        ManagementScope mgmtScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", computer.Name), connOptions);
                        mgmtScope.Connect();
                        ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                        ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                        ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                        ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                        inParams["CommandLine"] = parameters.CommandLine;
                        processClass.InvokeMethod("Create", inParams, null);
                        processClass.Dispose();
                        mgmtPath = null;
                        objectGetOptions = null;
                        mgmtScope = null;
                        connOptions = null;
                        Action commandSentAction = () =>
                        {
                            row.Cells["Result"].Value = resMan.GetString("CommandSent");
                            row.Cells["Result"].Style.BackColor = Color.LightGreen;
                            dtgvRemoteExecution.Refresh();
                            prgBarSendCommand.PerformStep();
                        };
                        this.Invoke(commandSentAction);
                        success++;
                    }
                    else
                    {
                        Logger.Write(computer.Name + " don't Ping");
                        Action noPingAction = () =>
                        {
                            row.Cells["Result"].Value = resMan.GetString("Noping");
                            row.Cells["Result"].Style.BackColor = Color.Silver;
                            dtgvRemoteExecution.Refresh();
                            prgBarSendCommand.PerformStep();
                        };
                        this.Invoke(noPingAction);
                        failed++;
                    }
                    Action refresAction = () =>
                    {
                        dtgvRemoteExecution.Refresh();
                    };
                    this.Invoke(refresAction);
                }
                catch (UnauthorizedAccessException)
                {
                    Logger.Write(computer.Name + " access denied.");
                    Action exceptionAction = () =>
                    {
                        row.Cells["Result"].Value = resMan.GetString("Exception");
                        row.Cells["Result"].Style.BackColor = Color.Silver;
                        dtgvRemoteExecution.Refresh();
                        prgBarSendCommand.PerformStep();
                    };
                    this.Invoke(exceptionAction);
                    failed++;

                    if (!credentialFailed)
                    {
                        credentialFailed = true;
                        bool stopSendingCommand = false;
                        Action wrongCredentialsAction = () =>
                        {
                            if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            {
                                Logger.Write("interrupt on failed credential.");
                                stopSendingCommand = true;
                            }
                        };
                        this.Invoke(wrongCredentialsAction);
                        if (stopSendingCommand)
                            break;
                        Logger.Write("continue with failed credential");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("**** " + computer.Name + " throw an execption : " + ex.Message);
                    Action exceptionAction = () =>
                    {
                        row.Cells["Result"].Value = resMan.GetString("Exception");
                        row.Cells["Result"].Style.BackColor = Color.Silver;
                        dtgvRemoteExecution.Refresh();
                        prgBarSendCommand.PerformStep();
                    };
                    this.Invoke(exceptionAction);
                    failed++;
                }
            }
            Action stopAction = () =>
            {
                btnClose.Enabled = true;
                btnCancel.Enabled = false;                
                lblSummary.Text = resMan.GetString("Summary") + " : " + resMan.GetString("Succeeded") + " = " + success + "   " + resMan.GetString("Failed") + " = " + failed + " " + resMan.GetString("On") + " " + (success + failed);
                dtgvRemoteExecution.Sort(dtgvRemoteExecution.Columns["Result"], ListSortDirection.Descending);
                this.chkBxCloseWhenFinished.Enabled = false;

                this.prgBarSendCommand.Value = this.prgBarSendCommand.Maximum;
                this.prgBarSendCommand.Refresh();
            };
            this.Invoke(stopAction);
            if (this.chkBxCloseWhenFinished.Checked)
                this.btnClose.PerformClick();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancel = true;
            this.btnCancel.Enabled = false;
        }
    }
}
