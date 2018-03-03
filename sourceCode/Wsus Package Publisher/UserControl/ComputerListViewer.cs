using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class ComputerListViewer : UserControl
    {
        private struct SearchOnlineComputerData
        {
            internal DataGridViewRow Row { get; set; }
            internal System.Threading.CountdownEvent countDown { get; set; }
        }

        private enum FilterComparison
        {
            StartWith,
            Contains,
            IsEqualTo,
            EndsWith
        }
        private ComputerTargetCollection _computerTargetCollection;
        private WsusWrapper _wsus;
        private bool populatingDataGridView;
        private bool cancelHighLightOnlineComputers;
        private bool cancelSearchOnlineComputers;
        private bool cancelRefreshDisplay;
        private System.Threading.Thread refreshDisplayThread;
        public delegate void DisplayOnLineStatusDelegate(object[] args);
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(ComputerListViewer).Assembly);

        public ComputerListViewer()
        {
            Logger.EnteringMethod("ComputerListViewer");
            InitializeComponent();

            populatingDataGridView = false;
            cancelHighLightOnlineComputers = false;
            cancelSearchOnlineComputers = false;

            lblCredentialNotice.Text = (Credentials.GetInstance()).CredentialNotice;
            _wsus = WsusWrapper.GetInstance();

            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendDetectNow"), "DetectNow"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendReportNow"), "ReportNow"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("ShowPendingUpdates"), "ShowPendingUpdates"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("InstallPendingUpdates"), "InstallPendingUpdates"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("ShowCurrentLogonUser"), "ShowCurrentLogonUser"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("ShowWindowsUpdateLog"), "ShowWindowsUpdateLog"));
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("CleanSoftwareDistributionFolder"), "CleanSoftwareDistributionFolder"));
            ctxMnuComputer.Items.Add(new ToolStripSeparator());
            ctxMnuComputer.Items.Add(GetItem(resMan.GetString("SendRebootNow"), "RebootNow"));

            foreach (string item in Enum.GetNames(typeof(FilterComparison)))
            {
                cmbBxFilterComparison.Items.Add(resMan.GetString(item));
            }
            cmbBxFilterComparison.SelectedIndex = 0;

            foreach (DataGridViewColumn column in dGVComputer.Columns)
            {
                column.Visible = false;
            }
            foreach (string column in GetDisplayedColumns(Properties.Settings.Default.ComputersListViewerDisplayedColumns))
            {
                dGVComputer.Columns[column].Visible = true;
            }

            foreach (DataGridViewColumn column in dGVComputer.Columns)
                ctxMnuHeader.Items.Add(GetItem(column.HeaderText, column.Name, column.Visible));

            Credentials.GetInstance().AuthentificationMethodChange += new Credentials.AuthentificationMethodChangeEventHandler(credentials_AuthentificationMethodChange);
            this.chkBxShowOnlineComputersOnly.Checked = Properties.Settings.Default.ShowOnlineComputersOnly;
        }

        public new void Dispose()
        {
            cancelRefreshDisplay = true;
            cancelHighLightOnlineComputers = true;
            cancelSearchOnlineComputers = true;
            if (refreshDisplayThread != null && refreshDisplayThread.ThreadState != System.Threading.ThreadState.Unstarted)
            {
                refreshDisplayThread.Abort();
                refreshDisplayThread.Join(500);
            }
            refreshDisplayThread = null;

            base.Dispose(true);
        }

        private void credentials_AuthentificationMethodChange()
        {
            Logger.EnteringMethod();
            lblCredentialNotice.Text = Credentials.GetInstance().CredentialNotice;
            Logger.Write(lblCredentialNotice);
        }

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

        private List<string> GetDisplayedColumns(string columnsList)
        {
            List<string> displayedColumns = new List<string>();

            if (!string.IsNullOrEmpty(columnsList))
            {
                try
                {
                    displayedColumns = columnsList.Split(new char[] { ';' }).ToList<string>();
                    if (displayedColumns.Count != 0)
                        return displayedColumns;
                }
                catch (Exception) { }
            }

            displayedColumns.Clear();
            displayedColumns.Add("ComputerName");
            displayedColumns.Add("IPAdress");
            displayedColumns.Add("LastReportedStatusTime");
            displayedColumns.Add("LastSyncTime");
            displayedColumns.Add("Make");
            displayedColumns.Add("Model");
            displayedColumns.Add("OSArchitecture");
            displayedColumns.Add("OSDescription");

            return displayedColumns;
        }

        internal ComputerTargetCollection ComputerCollection
        {
            get { return _computerTargetCollection; }
            set
            {
                Logger.EnteringMethod();
                cancelRefreshDisplay = true;
                cancelHighLightOnlineComputers = true;
                cancelSearchOnlineComputers = true;
                _computerTargetCollection = value;
                if (refreshDisplayThread != null &&
                    (refreshDisplayThread.ThreadState != System.Threading.ThreadState.Stopped || refreshDisplayThread.ThreadState != System.Threading.ThreadState.Unstarted))
                {
                    refreshDisplayThread.Abort();
                    refreshDisplayThread.Join(500);
                    refreshDisplayThread = null;
                }
                refreshDisplayThread = new System.Threading.Thread(new System.Threading.ThreadStart(RefreshDisplay));
                refreshDisplayThread.Priority = System.Threading.ThreadPriority.BelowNormal;
                cancelRefreshDisplay = false;
                refreshDisplayThread.Start();
            }
        }

        internal int DataGridViewHeight
        {
            get
            {
                int height = 0;

                height += dGVComputer.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dGVComputer.Rows)
                {
                    height += row.Height;
                }

                return height;
            }
        }

        private void RefreshDisplay()
        {
            Logger.EnteringMethod();
            populatingDataGridView = true;
            Action action = () =>
            {
                dGVComputer.SuspendLayout();
                dGVComputer.Rows.Clear();
                dGVComputer.Refresh();
                foreach (IComputerTarget computer in ComputerCollection)
                {
                    if (cancelRefreshDisplay)
                        break;
                    int index = dGVComputer.Rows.Add();
                    DataGridViewRow addedRow = dGVComputer.Rows[index];
                    addedRow.Cells["ComputerName"].Value = new ADComputer(computer.FullDomainName);
                    addedRow.Cells["IPAdress"].Value = computer.IPAddress;
                    addedRow.Cells["BiosName"].Value = computer.BiosInfo.Name;
                    addedRow.Cells["BiosVersion"].Value = computer.BiosInfo.Version;
                    addedRow.Cells["LastReportedStatusTime"].Value = computer.LastReportedStatusTime.ToLocalTime().ToString();
                    addedRow.Cells["LastSyncTime"].Value = computer.LastSyncTime.ToLocalTime().ToString();
                    addedRow.Cells["LastSyncResult"].Value = computer.LastSyncResult.ToString();
                    addedRow.Cells["Make"].Value = computer.Make;
                    addedRow.Cells["Model"].Value = computer.Model;
                    addedRow.Cells["OSArchitecture"].Value = computer.OSArchitecture;
                    addedRow.Cells["OSDescription"].Value = computer.OSDescription;
                }
                if (dGVComputer.SortedColumn != null)
                    dGVComputer.Sort(dGVComputer.SortedColumn, (dGVComputer.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
                dGVComputer.Refresh();
                populatingDataGridView = false;
                dGVComputer.ClearSelection();
                dGVComputer.ResumeLayout();
            };
            if (!this.IsDisposed && !this.Disposing)
                this.Invoke(action);
            cancelHighLightOnlineComputers = false;
            HighLightOnLineComputers();
        }

        internal void Display(Guid computerGroupId)
        {
            Logger.EnteringMethod(computerGroupId.ToString());
            txtBxFilterCriteria.Text = string.Empty;
            chkBxShowOnlineComputersOnly.Enabled = false;
            txtBxFilterCriteria.Enabled = false;
            cmbBxFilterComparison.Enabled = false;
            btnFilter.Enabled = false;
            btnClearFilter.Enabled = false;
            ComputerCollection = _wsus.GetComputerTargets(computerGroupId);
            chkBxShowOnlineComputersOnly.Enabled = true;
            btnFilter.Enabled = true;
            btnClearFilter.Enabled = true;
            txtBxFilterCriteria.Enabled = true;
            cmbBxFilterComparison.Enabled = true;
        }

        private void HighLightOnLineComputers()
        {
            Logger.EnteringMethod();
            string computerName = string.Empty;

            if (dGVComputer.Rows.Count > 0 && !cancelHighLightOnlineComputers)
            {
                DataGridViewRow[] rows = new DataGridViewRow[dGVComputer.Rows.Count];
                dGVComputer.Rows.CopyTo(rows, 0);

                cancelSearchOnlineComputers = false;
                System.Threading.CountdownEvent countDown = new System.Threading.CountdownEvent(1);
                foreach (DataGridViewRow row in rows)
                {
                    if (cancelHighLightOnlineComputers)
                        break;
                    countDown.AddCount();
                    SearchOnlineComputerData data = new SearchOnlineComputerData();
                    data.Row = row;
                    data.countDown = countDown;
                    System.Threading.ThreadPool.QueueUserWorkItem(SearchOnlineComputers, (object)data);
                }
                countDown.Signal();
                countDown.Wait(dGVComputer.Rows.Count * 10000);
                Action action = () =>
                {
                    if (dGVComputer.SortedColumn != null && dGVComputer.SortedColumn.Name == "OnLine")
                        dGVComputer.Sort(dGVComputer.SortedColumn, (dGVComputer.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
                };
                this.Invoke(action);
            }
        }

        private void SearchOnlineComputers(object data)
        {
            SearchOnlineComputerData computerData = (SearchOnlineComputerData)data;
            object[] args = new object[2];
            DataGridViewRow currentRow = computerData.Row;

            try
            {
                if (!cancelSearchOnlineComputers)
                {
                    ADComputer computer = (ADComputer)currentRow.Cells["ComputerName"].Value;
                    args[0] = currentRow;
                    args[1] = "No";
                    if (computer.Ping(100))
                    {
                        args[1] = "Yes";
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (!cancelSearchOnlineComputers)
                {
                    DisplayOnLineStatusDelegate DisplayOnLineStatusHandler = new DisplayOnLineStatusDelegate(DisplayOnLineStatus);
                    DisplayOnLineStatusHandler(args);
                }
            }
            catch (Exception)
            {
            }
            finally { computerData.countDown.Signal(); }
        }

        private void DisplayOnLineStatus(object[] obj)
        {
            Action action = () =>
            {
                lock (dGVComputer)
                {
                    if (!cancelSearchOnlineComputers)
                    {
                        DataGridViewRow currentRow = (DataGridViewRow)obj[0];
                        string online = (string)obj[1];
                        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
                        currentRow.Cells["OnLine"].Value = resMan.GetString(online);
                        Color cellColor = (online == "Yes") ? Color.PaleGreen : Color.Orange;
                        currentRow.Cells["IPAdress"].Style.BackColor = cellColor;
                        currentRow.Cells["IPAdress"].Style.SelectionBackColor = cellColor;
                        currentRow.Cells["IPAdress"].Style.ForeColor = Color.Black;
                        currentRow.Cells["IPAdress"].Style.SelectionForeColor = Color.Black;
                        currentRow.Visible = !IsComputerFiltered(currentRow.Cells["ComputerName"].Value.ToString()) && (!chkBxShowOnlineComputersOnly.Checked || online == "Yes");
                    }
                }
            };
            if (!this.IsDisposed && !this.Disposing && !cancelSearchOnlineComputers)
                this.Invoke(action);
        }

        private void FilterComputers()
        {
            Logger.EnteringMethod(chkBxShowOnlineComputersOnly.Checked.ToString());
            foreach (DataGridViewRow row in dGVComputer.Rows)
                row.Visible = !IsComputerFiltered(row.Cells["ComputerName"].Value.ToString()) && (!chkBxShowOnlineComputersOnly.Checked || (row.Cells["OnLine"].Value != null && row.Cells["OnLine"].Value.ToString() == resMan.GetString("Yes")));
        }

        private bool IsComputerFiltered(string computerName)
        {
            Logger.EnteringMethod(computerName);

            bool result = true;

            if (cmbBxFilterComparison.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxFilterCriteria.Text))
            {
                switch (cmbBxFilterComparison.SelectedIndex)
                {
                    case 0:
                        if (computerName.ToLower().StartsWith(txtBxFilterCriteria.Text.ToLower()))
                            result = false;
                        break;
                    case 1:
                        if (computerName.ToLower().Contains(txtBxFilterCriteria.Text.ToLower()))
                            result = false;
                        break;
                    case 2:
                        if (computerName.ToLower().Equals(txtBxFilterCriteria.Text.ToLower()))
                            result = false;
                        break;
                    case 3:
                        if (computerName.ToLower().EndsWith(txtBxFilterCriteria.Text.ToLower()))
                            result = false;
                        break;
                    default:
                        result = false;
                        break;
                }
            }
            else
                result = false;

            Logger.Write("Will return " + result);
            return result;
        }

        private void dGVComputer_SelectionChanged(object sender, EventArgs e)
        {
            if (SelectionChanged != null && !populatingDataGridView)
                SelectionChanged(dGVComputer.SelectedRows);
        }

        private void ctxMnuComputer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.EnteringMethod(e.ClickedItem.Name);
            List<ADComputer> targetComputers = new List<ADComputer>();
            FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

            foreach (DataGridViewRow row in dGVComputer.SelectedRows)
            {
                if (row.Visible)
                    targetComputers.Add((ADComputer)row.Cells["ComputerName"].Value);
            }
            ctxMnuComputer.Hide();
            Credentials cred = Credentials.GetInstance();
            if (targetComputers.Count != 0)
                if (cred.InitializeCredential() == false)
                    return;
            lblCredentialNotice.Text = cred.CredentialNotice;
            Logger.Write(lblCredentialNotice);

            switch (e.ClickedItem.Name)
            {
                case "DetectNow":
                    remoteExecution.Show(this);
                    remoteExecution.SendDetectNow(targetComputers, cred.Login, cred.Password);
                    break;
                case "ReportNow":
                    remoteExecution.Show(this);
                    remoteExecution.SendReportNow(targetComputers, cred.Login, cred.Password);
                    break;
                case "RebootNow":
                    FrmRebootCommand rebootCommand = new FrmRebootCommand(targetComputers, cred.Login, cred.Password);
                    rebootCommand.Show();
                    break;
                case "ShowPendingUpdates":
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    System.Security.SecureString securePassword = new System.Security.SecureString();

                    startInfo.FileName = Environment.CurrentDirectory + @"\ShowPendingUpdates.exe";
                    if (!System.IO.File.Exists(startInfo.FileName))
                    {
                        Logger.Write("Unable to find : " + startInfo.FileName);
                        MessageBox.Show(resMan.GetString("UnableToFindShowPendingUpdates"));
                    }
                    else
                    {
                        startInfo.Arguments = targetComputers[0].Name;
                        if (!string.IsNullOrEmpty(cred.Login) && !string.IsNullOrEmpty(cred.Password))
                        {
                            foreach (Char letter in cred.Password)
                                securePassword.AppendChar(letter);
                            startInfo.UserName = cred.Login;
                            startInfo.Password = securePassword;
                            startInfo.Domain = ADHelper.GetDomainName();
                        }
                        startInfo.UseShellExecute = false;
                        startInfo.WorkingDirectory = Environment.CurrentDirectory;
                        try
                        {
                            System.Diagnostics.Process.Start(startInfo);
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("**** " + ex.Message);
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case "InstallPendingUpdates":
                    FrmInstallPendingUpdatesNow installPendingUpdates = new FrmInstallPendingUpdatesNow();
                    installPendingUpdates.Username = cred.Login;
                    installPendingUpdates.Password = cred.Password;
                    installPendingUpdates.Computers = targetComputers;
                    installPendingUpdates.ShowDialog();
                    break;
                case "ShowCurrentLogonUser":
                    this.Cursor = Cursors.WaitCursor;
                    ADComputer remoteComputer = new ADComputer(dGVComputer.SelectedRows[0].Cells["ComputerName"].Value.ToString());
                    string logonUser = remoteComputer.GetCurrentLogonUser(cred.Login, cred.Password);
                    if (!string.IsNullOrEmpty(logonUser))
                        MessageBox.Show(resMan.GetString("CurrentLogonUserIs") + " : " + logonUser);
                    else
                        MessageBox.Show(resMan.GetString("UnableToGetLogonUser"));
                    this.Cursor = Cursors.Default;
                    break;
                case "ShowWindowsUpdateLog":
                    this.Cursor = Cursors.WaitCursor;
                    ADComputer computer = new ADComputer(dGVComputer.SelectedRows[0].Cells["ComputerName"].Value.ToString());
                    if (!computer.Ping(100))
                        MessageBox.Show(resMan.GetString("ComputerUnreachable"));
                    else
                        computer.OpenWindowsUpdateLog(cred.Login, cred.Password);
                    this.Cursor = Cursors.Default;
                    break;
                case "CleanSoftwareDistributionFolder":
                    this.Cursor = Cursors.WaitCursor;
                    FrmCleanSoftwareDistributionFolder frmCleanSoftwareDistributionFolder = new FrmCleanSoftwareDistributionFolder(targetComputers, cred.Login, cred.Password);
                    frmCleanSoftwareDistributionFolder.ShowDialog();
                    this.Cursor = Cursors.Default;
                    break;
                default:
                    break;
            }
        }

        private void ctxMnuHeader_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.EnteringMethod((e.ClickedItem as ToolStripMenuItem).Name);
            (e.ClickedItem as ToolStripMenuItem).Checked = !(e.ClickedItem as ToolStripMenuItem).Checked;

            string displayedColumns = string.Empty;

            foreach (ToolStripMenuItem menuItem in ctxMnuHeader.Items)
            {
                dGVComputer.Columns[menuItem.Name].Visible = menuItem.Checked;
                if (menuItem.Checked)
                    displayedColumns += menuItem.Name + ";";
            }
            if (!string.IsNullOrEmpty(displayedColumns))
                displayedColumns = displayedColumns.Substring(0, displayedColumns.Length - 1);
            else
            {
                displayedColumns = "ComputerName";
                dGVComputer.Columns["ComputerName"].Visible = true;
                (ctxMnuHeader.Items["ComputerName"] as ToolStripMenuItem).Checked = true;
            }
            Properties.Settings.Default.ComputersListViewerDisplayedColumns = displayedColumns;
            Properties.Settings.Default.Save();
        }

        private void dGVComputer_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
            {
                bool IsInDomain = !string.IsNullOrEmpty(ADHelper.GetDomainName());
                AdjustSelection(e.RowIndex);
                ctxMnuComputer.Items["ShowPendingUpdates"].Enabled = (dGVComputer.SelectedRows.Count == 1 && IsInDomain && System.IO.File.Exists("ShowPendingUpdates.exe"));
                ctxMnuComputer.Items["ShowWindowsUpdateLog"].Enabled = (dGVComputer.SelectedRows.Count == 1 && IsInDomain);
                ctxMnuComputer.Items["InstallPendingUpdates"].Enabled = IsInDomain && System.IO.File.Exists("InstallPendingUpdates.exe") && System.IO.File.Exists("Interop.WUApiLib.dll");
                ctxMnuComputer.Items["CleanSoftwareDistributionFolder"].Enabled = IsInDomain;
                ctxMnuComputer.Items["ShowCurrentLogonUser"].Enabled = IsInDomain && (dGVComputer.SelectedRows.Count == 1);
                ctxMnuComputer.Show(dGVComputer, dGVComputer.PointToClient(Cursor.Position));
            }
        }

        private void AdjustSelection(int ClickedIndex)
        {
            DataGridViewRow clickedRow = dGVComputer.Rows[ClickedIndex];
            if (!dGVComputer.SelectedRows.Contains(clickedRow))
            {
                dGVComputer.ClearSelection();
                dGVComputer.Rows[ClickedIndex].Selected = true;
            }
        }

        private void dGVComputer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1)
                ctxMnuHeader.Show(dGVComputer, dGVComputer.PointToClient(Cursor.Position));
        }

        private void chkBxShowOnlineComputersOnly_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Write(chkBxShowOnlineComputersOnly);
            Properties.Settings.Default.ShowOnlineComputersOnly = this.chkBxShowOnlineComputersOnly.Checked;
            Properties.Settings.Default.Save();
            FilterComputers();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FilterComputers();
        }

        private void txtBxFilterCriteria_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFilter.PerformClick();
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            txtBxFilterCriteria.Text = string.Empty;
            btnFilter.PerformClick();
        }

        private void dGVComputer_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "LastReportedStatusTime" || e.Column.Name == "LastSyncTime")
            {
                try
                {
                    DateTime date1 = Convert.ToDateTime(e.CellValue1.ToString());
                    DateTime date2 = Convert.ToDateTime(e.CellValue2.ToString());
                    e.SortResult = DateTime.Compare(date1, date2);

                    e.Handled = true;
                }
                catch (Exception) { }
            }
            else
            {
                e.Handled = false;
            }
        }

        #region (Event Delegates - événements)

        public delegate void SelectionChangedEventHandler(DataGridViewSelectedRowCollection rows);
        public event SelectionChangedEventHandler SelectionChanged;

        #endregion

    }
}
