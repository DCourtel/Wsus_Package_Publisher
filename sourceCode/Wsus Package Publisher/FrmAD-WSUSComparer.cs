using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmAD_WSUSComparer : Form
    {
        private struct ASyncClientParameters
        {
            internal int RowIndex { get; set; }
            internal string Login { get; set; }
            internal string Password { get; set; }
            internal System.Threading.CountdownEvent CountEvent { get; set; }
        }

        private struct ServiceParameters
        {
            internal string ServiceName { get; set; }
            internal string Command { get; set; }
            internal string Login { get; set; }
            internal string Password { get; set; }internal int RowIndex { get; set; }
            internal System.Threading.CountdownEvent CountEvent { get; set; }
        }

        private WrongCredentialsWatcher _wrongCrendentialsWatcher;
        private object _QuerySusClientIDLocker = new object();
        private object _QueryWuAuSrvStatusLocker = new object();
        private object _ResetSusClientIDLocker = new object();
        private object _ChangeServiceStateLocker = new object();
        List<ADComputer> ADcomputers;
        List<ADComputer> computers = new List<ADComputer>();
        public delegate void UpdateRowDelegate(object[] args);
        FrmOUSelector ouSelector = new FrmOUSelector();
        private static bool _closing = false;
        private static bool _aborting = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmAD_WSUSComparer).Assembly);

        internal FrmAD_WSUSComparer()
        {
            Logger.EnteringMethod("FrmAD_WSUSComparer");
            InitializeComponent();

            _closing = false;
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("GetWsusClientID"), "GetWsusClientID"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("ResetWsusClientID"), "ResetWsusClientID"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("StartService"), "StartService"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("StopService"), "StopService"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("RestartService"), "RestartService"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("QueryServiceStatus"), "QueryServiceStatus"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("ShowWindowsUpdateLog"), "ShowWindowsUpdateLog"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendDetectNow"), "SendDetectNow"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendReportNow"), "SendReportNow"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendRebootNow"), "SendRebootNow"));

            foreach (DataGridViewColumn column in dtGrdVResult.Columns)
                ctxMnuHeader.Items.Add(GetItem(column.HeaderText, column.Name, column.Visible));
        }

        private string WindowsUpdateLogReader { get; set; }

        private ToolStripMenuItem GetItem(string text, string itemName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = itemName;
            return item;
        }

        private ToolStripMenuItem GetItem(string text, string itemName, bool isChecked)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = itemName;
            item.Checked = isChecked;
            return item;
        }

        private void FrmAD_WSUSComparer_Load(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            txtBxDomainName.Text = ADHelper.GetDomainName();
            Logger.Write(txtBxDomainName);
            lblCredentials.Text = (Credentials.GetInstance()).CredentialNotice;
            Logger.Write(lblCredentials);
        }

        private ToolStripMenuItem GetToolStripItem(string headerText, bool visible, string columnName)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(headerText);
            item.Checked = visible;
            item.Tag = columnName;
            return item;
        }

        private void btnSearchComputers_Click(object sender, EventArgs e)
        {
            Logger.Write("Will search for computers");
            ChangeUIAccess(false);
            lblProgress.Text = resMan.GetString("SearchingComputerInAD");
            lblProgress.Refresh();

            _aborting = false;
            dtGrdVResult.Rows.Clear();
            dtGrdVResult.Refresh();
            this.Cursor = Cursors.WaitCursor;

            if (ouSelector.SearchInAllAD)
                ADcomputers = ADHelper.GetComputers("LDAP://" + txtBxDomainName.Text);
            else
                ADcomputers = ADHelper.GetComputers(ouSelector.SelectedOUList);
            Logger.Write("Found " + ADcomputers.Count + " computers in domaine " + txtBxDomainName.Text);
            lblProgress.Text = ADcomputers.Count + resMan.GetString("ComputersFound");

            WsusWrapper wsus = WsusWrapper.GetInstance();
            computers.Clear();
            prgBrSearch.Value = 0;
            prgBrSearch.Maximum = ADcomputers.Count;
            lblProgress.Text = resMan.GetString("SearchingComputersInWSUS");
            lblProgress.Refresh();

            foreach (ADComputer computer in ADcomputers)
            {
                if (!_closing && !_aborting)
                {
                    ADComputer tempComputer = new ADComputer(computer.Name, computer.OUName, computer.LastLogon, computer.OSName, computer.OSServicePack, computer.OSVersion);
                    try
                    {
                        Logger.Write("Searching " + computer.Name + " in WSUS...");
                        Microsoft.UpdateServices.Administration.IComputerTarget target = wsus.GetComputerTargetByName(computer.Name);
                        if (target != null)
                        {
                            tempComputer.IsInWsus = true;
                            Logger.Write("...Found it.");
                        }
                        else
                        {
                            Logger.Write("...not found.");
                            tempComputer.IsInWsus = false;
                        }
                    }
                    catch (Exception)
                    {
                    }
                    if (!computers.Contains(tempComputer))
                        computers.Add(tempComputer);
                    prgBrSearch.Value++;
                    prgBrSearch.Refresh();
                }
                else
                    break;
            }
            DisplayComputers();
            ChangeUIAccess(true);
            this.Cursor = Cursors.Default;
        }

        private void DisplayComputers()
        {
            Logger.EnteringMethod();
            int displayedComputers = 0;
            dtGrdVResult.Rows.Clear();
            dtGrdVResult.SuspendLayout();
            foreach (ADComputer computer in computers)
            {
                if (!chkBxShowOnlyMissingsComputers.Checked || !computer.IsInWsus)
                {
                    Logger.Write("Display computer : " + computer.Name);
                    int index = dtGrdVResult.Rows.Add();
                    DataGridViewRow row = dtGrdVResult.Rows[index];

                    row.Cells["ComputerName"].Value = computer;
                    row.Cells["OU"].Value = computer.OUName;
                    row.Cells["LastLogon"].Value = computer.LastLogon;
                    if (computer.SusClientID == string.Empty)
                        row.Cells["SusClientID"].Value = resMan.GetString(computer.NetworkState.ToString());
                    else
                        row.Cells["SusClientID"].Value = computer.SusClientID;
                    row.Cells["OSName"].Value = computer.OSName;
                    row.Cells["OSServicePack"].Value = computer.OSServicePack;
                    row.Cells["OSVersion"].Value = computer.OSVersion;
                    row.Cells["ServiceStatus"].Value = computer.WuAuServiceStatus;
                    if (computer.HasDuplicateWsusClientID)
                    {
                        row.Cells["SusClientID"].Style.BackColor = Color.OrangeRed;
                        row.Cells["SusClientID"].Style.SelectionBackColor = Color.OrangeRed;
                    }
                    else
                    {
                        row.Cells["SusClientID"].Style.BackColor = Color.White;
                        row.Cells["SusClientID"].Style.SelectionBackColor = Color.Teal;
                    }
                    displayedComputers++;
                }
            }
            dtGrdVResult.ResumeLayout();
            lblProgress.Text = computers.Count + resMan.GetString("ComputersFound") + ", " + displayedComputers.ToString() + " " + resMan.GetString("Displayed");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (saveExport.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                btnExport.Enabled = false;
                Logger.Write("Saving to :" + saveExport.FileName);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(saveExport.OpenFile(), Encoding.UTF8);

                writer.WriteLine("Computer Name;OU Name;Wsus Client ID;Last Logon;OS Name;OS Service Pack;OS Version;Service Status;Service Start Mode;Is In Wsus;Duplicate Wsus Client ID");
                foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
                {
                    ADComputer tempComputer = (ADComputer)row.Cells["ComputerName"].Value;
                    writer.WriteLine(
                        tempComputer.Name + ";" +
                        tempComputer.OUName + ";" +
                        tempComputer.SusClientID + ";" +
                        tempComputer.LastLogon + ";" +
                        tempComputer.OSName + ";" +
                        tempComputer.OSServicePack + ";" +
                        tempComputer.OSVersion + ";" +
                        tempComputer.WuAuServiceStatus + ";" +
                        tempComputer.IsInWsus.ToString() + ";" +
                        tempComputer.HasDuplicateWsusClientID.ToString());
                }
                writer.Flush();
                writer.Close();
                btnExport.Enabled = true;
            }
        }

        private void btnGetSusClientID_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            _aborting = false;
            prgBrSearch.Value = 0;
            prgBrSearch.Maximum = dtGrdVResult.SelectedRows.Count;
            prgBrSearch.Refresh();
            ChangeUIAccess(false);
            _wrongCrendentialsWatcher = new WrongCredentialsWatcher();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(StartQuerySusClientID));
            t.Start();
        }

        private void ChangeUIAccess(bool IsUnlock)
        {
            btnGetSusClientID.Enabled = IsUnlock;
            btnResetWsusClientID.Enabled = IsUnlock;
            btnSelectOU.Enabled = IsUnlock;
            btnSearchComputers.Enabled = IsUnlock;
            chkBxShowOnlyMissingsComputers.Enabled = IsUnlock;
            if (!IsUnlock)
                btnExport.Enabled = false;
            else
                btnExport.Enabled = (dtGrdVResult.Rows.Count != 0);
            BtnClose.Enabled = IsUnlock;
            btnAbort.Enabled = !IsUnlock;
        }

        private void StartQuerySusClientID()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            Credentials cred = Credentials.GetInstance();
            if (dtGrdVResult.SelectedRows.Count != 0)
                if (cred.InitializeCredential() == false)
                    return;

            Action startAction = () => { lblCredentials.Text = cred.CredentialNotice; };
            this.Invoke(startAction);
            Logger.Write(lblCredentials);

            System.Threading.CountdownEvent countEvent = new System.Threading.CountdownEvent(1);

            foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
            {
                if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
                {
                    countEvent.AddCount(1);
                    ASyncClientParameters parameters = new ASyncClientParameters();
                    parameters.RowIndex = row.Index;
                    parameters.Login = cred.Login;
                    parameters.Password = cred.Password;
                    parameters.CountEvent = countEvent;
                    System.Threading.ThreadPool.QueueUserWorkItem(QuerySusClientID, parameters);
                }
            }
            countEvent.Signal();
            countEvent.Wait(10000 * dtGrdVResult.SelectedRows.Count);

            if (!_closing && !_aborting)
                SearchDuplicateWsusClientID();
            Action endAction = () =>
            {
                ChangeUIAccess(true);
            };
            if (!_closing)
                this.Invoke(endAction);
        }

        private void QuerySusClientID(object obj)
        {
            ASyncClientParameters parameters = (ASyncClientParameters)obj;
            try
            {
                object[] args = new object[3];

                ADComputer computer = (ADComputer)dtGrdVResult.Rows[parameters.RowIndex].Cells["ComputerName"].Value;
                args[0] = string.Empty;
                args[1] = parameters.RowIndex;
                args[2] = "SusClientID";
                if (!_closing && !_aborting && !chkBxDontPing.Checked && !computer.Ping((int)nupPingTimeout.Value))
                {
                    args[0] = resMan.GetString("Unreachable");
                }
                else
                {
                    if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
                    {
                        lock (_QuerySusClientIDLocker)
                        {
                            try
                            {
                                if (!_wrongCrendentialsWatcher.IsAbortRequested && !_aborting && !_closing)
                                {
                                    computer.QuerySusClientID(parameters.Login, parameters.Password);
                                    args[0] = computer.SusClientID;
                                }
                                else
                                    args[0] = resMan.GetString("Aborted");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                _wrongCrendentialsWatcher.IsWrongCredentials = true;

                                if (!_wrongCrendentialsWatcher.ContinueWithFailedCredentials)
                                {
                                    if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                    {
                                        Logger.Write("interrupt on failed credentials.");
                                        _wrongCrendentialsWatcher.IsAbortRequested = true;
                                        args[0] = resMan.GetString("Aborted");
                                    }
                                    else
                                    {
                                        Logger.Write("Continue with bad credentials.");
                                        _wrongCrendentialsWatcher.ContinueWithFailedCredentials = true;
                                        args[0] = resMan.GetString("FailedToConnect");
                                    }
                                }
                                else
                                    args[0] = resMan.GetString("FailedToConnect");
                            }
                            catch (Exception ex)
                            {
                                Logger.Write(ex.Message);
                                args[0] = resMan.GetString("FailedToSendCommand");
                            }
                        }
                    }
                    else
                        args[0] = resMan.GetString("Aborted");
                }
                UpdateRow(args);
            }
            catch (Exception) { }
            finally { parameters.CountEvent.Signal(); }
        }

        private void UpdateRow(object[] obj)
        {
            Action action = () =>
            {
                string valueToDisplay = (string)obj[0];
                int index = (int)obj[1];
                string rowName = (string)obj[2];

                lock (dtGrdVResult)
                {
                    dtGrdVResult.Rows[index].Cells[rowName].Value = valueToDisplay;
                    if (prgBrSearch.InvokeRequired)
                        prgBrSearch.BeginInvoke((Action)delegate() { prgBrSearch.Value++; });
                    else
                        prgBrSearch.Value++;
                }
            };
            if (!_closing && !_aborting)
                this.Invoke(action);
        }

        private void SearchDuplicateWsusClientID()
        {
            Logger.EnteringMethod();
            for (int i = 0; i < computers.Count - 1; i++)
            {
                string wsusClientID = computers[i].SusClientID;
                if (wsusClientID != string.Empty)
                    for (int j = i + 1; j < computers.Count; j++)
                    {
                        if (wsusClientID == computers[j].SusClientID)
                        {
                            computers[i].DeclareHasDuplicate();
                            computers[j].DeclareHasDuplicate();
                        }
                    }
            }
            UpdateSusClientIDColor();
        }

        private void UpdateSusClientIDColor()
        {
            Logger.EnteringMethod();
            foreach (DataGridViewRow row in dtGrdVResult.Rows)
            {
                ADComputer computer = (ADComputer)row.Cells["ComputerName"].Value;

                if (computer.HasDuplicateWsusClientID)
                {
                    row.Cells["SusClientID"].Style.BackColor = Color.OrangeRed;
                    row.Cells["SusClientID"].Style.SelectionBackColor = Color.OrangeRed;
                }
                else
                {
                    row.Cells["SusClientID"].Style.BackColor = Color.White;
                    row.Cells["SusClientID"].Style.SelectionBackColor = Color.Teal;
                }
            }
        }

        private void GetWuAuServiceStatus()
        {
            Logger.EnteringMethod();
            prgBrSearch.Value = 0;
            prgBrSearch.Maximum = dtGrdVResult.SelectedRows.Count;
            prgBrSearch.Refresh();
            ChangeUIAccess(false);

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(StartQueryWuAuServiceStatus));
            t.Start();
        }

        private void StartQueryWuAuServiceStatus()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            Credentials cred = Credentials.GetInstance();
            if (dtGrdVResult.SelectedRows.Count != 0)
                if (cred.InitializeCredential() == false)
                    return;

            Action startAction = () => { lblCredentials.Text = cred.CredentialNotice; };
            if (!_closing && !_aborting)
                this.Invoke(startAction);

            System.Threading.CountdownEvent countEvent = new System.Threading.CountdownEvent(1);

            foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
            {
                if (!_closing && !_aborting)
                {
                    countEvent.AddCount();
                    ASyncClientParameters parameters = new ASyncClientParameters();
                    parameters.RowIndex = row.Index;
                    parameters.Login = cred.Login;
                    parameters.Password = cred.Password;
                    parameters.CountEvent = countEvent;

                    System.Threading.ThreadPool.QueueUserWorkItem(QueryWuAuServiceStatus, parameters);
                }
            }
            countEvent.Signal();
            countEvent.Wait(10000 * dtGrdVResult.SelectedRows.Count);

            Action endAction = () => { ChangeUIAccess(true); };
            if (!_closing)
                this.Invoke(endAction);
        }

        private void QueryWuAuServiceStatus(object obj)
        {
            char[] separator = new char[1] { ';' };
            ASyncClientParameters parameters = (ASyncClientParameters)obj;

            try
            {
                int index = parameters.RowIndex;
                string login = parameters.Login;
                string password = parameters.Password;
                object[] args = new object[3];

                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
                ADComputer computer = (ADComputer)dtGrdVResult.Rows[index].Cells["ComputerName"].Value;
                args[0] = resMan.GetString("FailedToConnect");
                args[1] = index;
                args[2] = "ServiceStatus";

                if (!_closing && !_aborting && !chkBxDontPing.Checked && !computer.Ping((int)nupPingTimeout.Value))
                {
                    args[0] = resMan.GetString("Unreachable");
                }
                else
                {
                    if (!_closing && !_aborting)
                    {
                        lock (_QueryWuAuSrvStatusLocker)
                        {
                            string result = string.Empty;
                            try
                            {
                                if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
                                {
                                    result = computer.QueryWuAuSrvStatus(login, password);
                                    if (result.IndexOf(';') != -1)
                                    {
                                        string[] tab = result.Split(separator);
                                        if (tab != null && tab.Length == 2)
                                        {
                                            result = resMan.GetString(tab[0]) + " " + tab[1];
                                        }
                                    }
                                    args[0] = result;
                                }
                                else
                                    args[0] = resMan.GetString("Aborted");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                _wrongCrendentialsWatcher.IsWrongCredentials = true;

                                if (!_wrongCrendentialsWatcher.ContinueWithFailedCredentials)
                                {
                                    if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                    {
                                        Logger.Write("interrupt on failed credentials.");
                                        _wrongCrendentialsWatcher.IsAbortRequested = true;
                                        args[0] = resMan.GetString("Aborted");
                                    }
                                    else
                                    {
                                        Logger.Write("Continue with bad credentials.");
                                        _wrongCrendentialsWatcher.ContinueWithFailedCredentials = true;
                                        args[0] = resMan.GetString("FailedToConnect");
                                    }
                                }
                                else
                                    args[0] = resMan.GetString("FailedToConnect");
                            }
                            catch (Exception ex)
                            {
                                Logger.Write(ex.Message);
                                args[0] = resMan.GetString("FailedToSendCommand");
                            }
                        }
                    }
                    else
                        args[0] = resMan.GetString("Aborted");
                }
                UpdateRow(args);
            }
            catch (Exception) { }
            finally { parameters.CountEvent.Signal(); }
        }

        private void btnResetWsusClientID_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            ChangeUIAccess(false);
            _aborting = false;
            prgBrSearch.Value = 0;
            prgBrSearch.Maximum = dtGrdVResult.SelectedRows.Count;
            prgBrSearch.Refresh();

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(StartResetSusClientID));
            t.Start();
        }

        private void StartResetSusClientID()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            Credentials cred = Credentials.GetInstance();

            if (dtGrdVResult.SelectedRows.Count != 0)
                if (cred.InitializeCredential() == false)
                    return;
            Logger.Write(lblCredentials);

            Action startAction = () => { lblCredentials.Text = cred.CredentialNotice; };
            if (!_closing)
                this.Invoke(startAction);

            System.Threading.CountdownEvent countEvent = new System.Threading.CountdownEvent(1);

            foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
            {
                countEvent.AddCount();
                ASyncClientParameters parameters = new ASyncClientParameters();
                parameters.RowIndex = row.Index;
                parameters.Login = cred.Login;
                parameters.Password = cred.Password;
                parameters.CountEvent = countEvent;

                if (!_closing & !_aborting)
                    System.Threading.ThreadPool.QueueUserWorkItem(ResetSusClientID, parameters);
            }
            countEvent.Signal();
            countEvent.Wait(10000 * dtGrdVResult.SelectedRows.Count);

            if (!_closing && !_aborting)
                SearchDuplicateWsusClientID();

            Action endAction = () => { ChangeUIAccess(true); };
            if (!_closing)
                this.Invoke(endAction);
        }

        private void ResetSusClientID(object obj)
        {
            ASyncClientParameters parameters = (ASyncClientParameters)obj;
            try
            {
                object[] args = new object[3];

                ADComputer computer = (ADComputer)dtGrdVResult.Rows[parameters.RowIndex].Cells["ComputerName"].Value;
                args[0] = resMan.GetString("FailedToSendCommand");
                args[1] = parameters.RowIndex;
                args[2] = "SusClientID";
                if (!_closing && !_aborting && !chkBxDontPing.Checked && !computer.Ping((int)nupPingTimeout.Value))
                    args[0] = resMan.GetString("Unreachable");
                else
                {
                    if (!_closing && !_aborting)
                        lock (_ResetSusClientIDLocker)
                        {
                            try
                            {
                                if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
                                {
                                    if (computer.ResetWsusClientID(parameters.Login, parameters.Password))
                                        args[0] = resMan.GetString("Reset");
                                    else
                                        args[0] = resMan.GetString("Failed");
                                }
                                else
                                    args[0] = resMan.GetString("Aborted");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                _wrongCrendentialsWatcher.IsWrongCredentials = true;

                                if (!_wrongCrendentialsWatcher.ContinueWithFailedCredentials)
                                {
                                    if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                    {
                                        Logger.Write("interrupt on failed credentials.");
                                        _wrongCrendentialsWatcher.IsAbortRequested = true;
                                        args[0] = resMan.GetString("Aborted");
                                    }
                                    else
                                    {
                                        Logger.Write("Continue with bad credentials.");
                                        _wrongCrendentialsWatcher.ContinueWithFailedCredentials = true;
                                        args[0] = resMan.GetString("FailedToConnect");
                                    }
                                }
                                else
                                    args[0] = resMan.GetString("FailedToConnect");
                            }
                            catch (Exception ex)
                            {
                                Logger.Write(ex.Message);
                                args[0] = resMan.GetString("FailedToSendCommand");
                            }
                        }
                    else
                        args[0] = resMan.GetString("Aborted");

                }
                UpdateRow(args);
            }
            catch (Exception) { }
            finally { parameters.CountEvent.Signal(); }
        }

        private void ManageService(string serviceName, string command, string login, string password)
        {
            Logger.EnteringMethod();
            prgBrSearch.Value = 0;
            prgBrSearch.Maximum = dtGrdVResult.SelectedRows.Count;
            prgBrSearch.Refresh();
            ChangeUIAccess(false);
            ServiceParameters serviceParams = new ServiceParameters();
            serviceParams.ServiceName = serviceName;
            serviceParams.Command = command;
            serviceParams.Login = login;
            serviceParams.Password = password;

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(StartManageService));
            t.Start(serviceParams);
        }

        private void StartManageService(object serviceparams)
        {
            ServiceParameters parameters = (ServiceParameters)serviceparams;

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);

            Credentials cred = Credentials.GetInstance();

            if (dtGrdVResult.SelectedRows.Count != 0)
                if (cred.InitializeCredential() == false)
                    return;
            Logger.Write(lblCredentials);

            Action startAction = () => { lblCredentials.Text = cred.CredentialNotice; };
            if (!_closing)
                this.Invoke(startAction);

            System.Threading.CountdownEvent countEvent = new System.Threading.CountdownEvent(1);

            foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
            {
                countEvent.AddCount();
                parameters.RowIndex = row.Index;
                parameters.CountEvent = countEvent;

                if (!_closing & !_aborting)
                    System.Threading.ThreadPool.QueueUserWorkItem(ChangeServiceState, parameters);
            }
            countEvent.Signal();
            countEvent.Wait(10000 * dtGrdVResult.SelectedRows.Count);

            if (!_closing && !_aborting)
                SearchDuplicateWsusClientID();

            Action endAction = () => { ChangeUIAccess(true); };
            if (!_closing)
                this.Invoke(endAction);
        }

        private void ChangeServiceState(object parameters)
        {
            ServiceParameters serviceParams = (ServiceParameters)parameters;

            object[] args = new object[3];
            args[0] = resMan.GetString("FailedToSendCommand");
            args[1] = serviceParams.RowIndex;
            args[2] = "ServiceStatus";

            if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
            {
                lock (_ChangeServiceStateLocker)
                {
                    try
                    {
                        if (!_closing && !_aborting && !_wrongCrendentialsWatcher.IsAbortRequested)
                        {
                            ADComputer computer = new ADComputer(dtGrdVResult.Rows[serviceParams.RowIndex].Cells["ComputerName"].Value.ToString());
                            bool result = false;
                            if (serviceParams.Command == "RestartService")
                            {
                                computer.ManageService(serviceParams.ServiceName, "StopService", serviceParams.Login, serviceParams.Password);
                                result = computer.ManageService(serviceParams.ServiceName, "StartService", serviceParams.Login, serviceParams.Password);
                                args[0] = result ? resMan.GetString("Running") : resMan.GetString("Failed");
                            }
                            else
                            {
                                result = computer.ManageService(serviceParams.ServiceName, serviceParams.Command, serviceParams.Login, serviceParams.Password);
                                args[0] = result ? ((serviceParams.Command == "StartService") ? resMan.GetString("Running") : resMan.GetString("Stopped")) : resMan.GetString("Failed");
                            }
                        }
                        else
                            args[0] = resMan.GetString("Aborted");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        _wrongCrendentialsWatcher.IsWrongCredentials = true;

                        if (!_wrongCrendentialsWatcher.ContinueWithFailedCredentials)
                        {
                            if (MessageBox.Show(resMan.GetString("CredentialFailed"), resMan.GetString("FailedToConnect"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            {
                                Logger.Write("interrupt on failed credentials.");
                                _wrongCrendentialsWatcher.IsAbortRequested = true;
                                args[0] = resMan.GetString("Aborted");
                            }
                            else
                            {
                                Logger.Write("Continue with bad credentials.");
                                _wrongCrendentialsWatcher.ContinueWithFailedCredentials = true;
                                args[0] = resMan.GetString("FailedToConnect");
                            }
                        }
                        else
                            args[0] = resMan.GetString("FailedToConnect");
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex.Message);
                        args[0] = resMan.GetString("FailedToSendCommand");
                    }
                }
            }
            else
                args[0] = resMan.GetString("Aborted");

            UpdateRow(args);
        }

        private void dtGrdVResult_SelectionChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (btnGetSusClientID.InvokeRequired)
                btnGetSusClientID.BeginInvoke((Action)delegate() { btnGetSusClientID.Enabled = (dtGrdVResult.SelectedRows.Count != 0); });
            else
                btnGetSusClientID.Enabled = (dtGrdVResult.SelectedRows.Count != 0);
            if (btnResetWsusClientID.InvokeRequired)
                btnResetWsusClientID.BeginInvoke((Action)delegate() { btnResetWsusClientID.Enabled = (dtGrdVResult.SelectedRows.Count != 0); });
            else
                btnResetWsusClientID.Enabled = (dtGrdVResult.SelectedRows.Count != 0);
            if (ctxMnuComputer.InvokeRequired)
                ctxMnuComputer.BeginInvoke((Action)delegate() { ctxMnuComputer.Items["ShowWindowsUpdateLog"].Enabled = (dtGrdVResult.SelectedRows.Count == 1); });
            else
                ctxMnuComputer.Items["ShowWindowsUpdateLog"].Enabled = (dtGrdVResult.SelectedRows.Count == 1);
        }

        private void chkBxDontPing_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            nupPingTimeout.Enabled = !chkBxDontPing.Checked;
            Logger.Write(chkBxDontPing);
        }

        private void dtGrdVResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
                ctxMnuComputer.Show(dtGrdVResult, dtGrdVResult.PointToClient(Cursor.Position));
        }

        private void dtGrdVResult_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1)
                ctxMnuHeader.Show(dtGrdVResult, dtGrdVResult.PointToClient(Cursor.Position));
        }

        private void ctxMnuHeader_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (e.ClickedItem as ToolStripMenuItem).Checked = !(e.ClickedItem as ToolStripMenuItem).Checked;

            foreach (ToolStripMenuItem menuItem in ctxMnuHeader.Items)
                dtGrdVResult.Columns[menuItem.Name].Visible = menuItem.Checked;
        }

        private void ctxMnuComputer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.EnteringMethod();
            Credentials cred = Credentials.GetInstance();
            lblCredentials.Text = cred.CredentialNotice;
            Logger.Write(lblCredentials);

            List<ADComputer> targetComputers = new List<ADComputer>();
            FrmRemoteExecution remoteExecution = new FrmRemoteExecution();
            Logger.Write(dtGrdVResult.SelectedRows.Count + " computers selected.");

            foreach (DataGridViewRow row in dtGrdVResult.SelectedRows)
            {
                Logger.Write("Selecting " + row.Cells[0].Value.ToString());
                targetComputers.Add((ADComputer)row.Cells[0].Value);
            }

            ctxMnuComputer.Hide();
            switch (e.ClickedItem.Name)
            {
                case "GetWsusClientID":
                    btnGetSusClientID_Click(null, null);
                    break;
                case "ResetWsusClientID":
                    btnResetWsusClientID_Click(null, null);
                    break;
                case "StartService":
                    Logger.Write("Start Service");
                    if (dtGrdVResult.SelectedRows.Count != 0)
                        if (cred.InitializeCredential() == false)
                            return;
                    _aborting = false;
                    ManageService("wuauserv", "StartService", cred.Login, cred.Password);
                    break;
                case "StopService":
                    Logger.Write("Stop Service");
                    if (dtGrdVResult.SelectedRows.Count != 0)
                        if (cred.InitializeCredential() == false)
                            return;
                    _aborting = false;
                    ManageService("wuauserv", "StopService", cred.Login, cred.Password);
                    break;
                case "RestartService":
                    Logger.Write("Restart Service");
                    if (dtGrdVResult.SelectedRows.Count != 0)
                        if (cred.InitializeCredential() == false)
                            return;
                    _aborting = false;
                    ManageService("wuauserv", "RestartService", cred.Login, cred.Password);
                    break;
                case "QueryServiceStatus":
                    Logger.Write("Querying Service Status");
                    this.Cursor = Cursors.WaitCursor;
                    _wrongCrendentialsWatcher = new WrongCredentialsWatcher();
                    _aborting = false;
                    GetWuAuServiceStatus();
                    this.Cursor = Cursors.Default;
                    break;
                case "ShowWindowsUpdateLog":
                    Logger.Write("Show WindowsUPdateLog");
                    if (cred.InitializeCredential() == false)
                        return;
                    this.Cursor = Cursors.WaitCursor;
                    ADComputer computer = new ADComputer(dtGrdVResult.SelectedRows[0].Cells["ComputerName"].Value.ToString());
                    if (!chkBxDontPing.Checked && !computer.Ping((int)nupPingTimeout.Value))
                        MessageBox.Show(resMan.GetString("ComputerUnreachable"));
                    else
                        computer.OpenWindowsUpdateLog(cred.Login, cred.Password);
                    this.Cursor = Cursors.Default;
                    break;
                case "SendDetectNow":
                    Logger.Write("Send DetectNow");
                    remoteExecution.Show(this);
                    remoteExecution.SendDetectNow(targetComputers, cred.Login, cred.Password);
                    break;
                case "SendReportNow":
                    Logger.Write("Send ReportNow");
                    remoteExecution.Show(this);
                    remoteExecution.SendReportNow(targetComputers, cred.Login, cred.Password);
                    break;
                case "SendRebootNow":
                    Logger.Write("Send RebootNow");
                    FrmRebootCommand rebootCommand = new FrmRebootCommand(targetComputers, cred.Login, cred.Password);
                    rebootCommand.Show();
                    break;
                default:
                    Logger.Write("**** Unable to detect the command");
                    break;
            }
        }

        private void chkBxShowOnlyMissingsComputers_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DisplayComputers();
        }

        private void btnSelectOU_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            ouSelector.ShowDialog();
            btnSearchComputers.Enabled = true;
        }

        private void FrmAD_WSUSComparer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closing = true;
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            _aborting = true;
        }
    }
}
