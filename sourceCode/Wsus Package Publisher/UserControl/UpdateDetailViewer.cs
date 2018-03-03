using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;
using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;

namespace Wsus_Package_Publisher
{
    public partial class UpdateDetailViewer : UserControl
    {
        private struct ReportResult
        {
            internal DataGridViewRow Row { get; set; }
            internal int InstalledCount { get; set; }
            internal int InstalledPendingRebootCount { get; set; }
            internal int NotInstalledCount { get; set; }
            internal int DownloadedCount { get; set; }
            internal int NotApplicableCount { get; set; }
            internal int FailedCount { get; set; }
            internal int UnknownCount { get; set; }
            internal int GetTotal()
            {
                return InstalledCount + InstalledPendingRebootCount + NotInstalledCount + DownloadedCount + NotApplicableCount + FailedCount + UnknownCount;
            }
            internal UpdateApprovalAction Approval { get; set; }
            internal bool IsOptional { get; set; }
        }

        private struct SearchOnlineComputerData
        {
            internal DataGridViewRow Row { get; set; }
            internal System.Threading.CountdownEvent CountDown { get; set; }
        }

        private UpdateCollection _update;
        private WsusWrapper _wsus;
        private ComputerGroup _computerGroups;
        private List<UpdateInstallationState> _filter = new List<UpdateInstallationState>();
        private System.Threading.Thread displayReportThread;
        private System.Threading.Thread highLightOnLineComputersThread;
        private bool cancelDisplayReport = false;
        private bool cancelHighLightOnLineComputers = false;
        private bool cancelSearchOnlineComputers = false;
        private bool updateReportDisplayed = false;
        FrmExportUpdateProgress frmExportProgress;
        public delegate void DisplayOnLineStatusDelegate(object[] args);
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateDetailViewer).Assembly);

        public UpdateDetailViewer()
        {
            Logger.EnteringMethod("UpdateDetailViewer");
            InitializeComponent();
            lblCredentialNotice.Text = (Credentials.GetInstance()).CredentialNotice;
            _wsus = WsusWrapper.GetInstance();

            chkCmbBxFilter.AddItem(resMan.GetString("Downloaded"));
            chkCmbBxFilter.AddItem(resMan.GetString("Failed"));
            chkCmbBxFilter.AddItem(resMan.GetString("Installed"));
            chkCmbBxFilter.AddItem(resMan.GetString("InstalledPendingReboot"));
            chkCmbBxFilter.AddItem(resMan.GetString("NotApplicable"));
            chkCmbBxFilter.AddItem(resMan.GetString("NotInstalled"));
            chkCmbBxFilter.AddItem(resMan.GetString("Unknown"));

            List<object> allItem = new List<object>();
            allItem.Add(resMan.GetString("Downloaded"));
            allItem.Add(resMan.GetString("Failed"));
            allItem.Add(resMan.GetString("Installed"));
            allItem.Add(resMan.GetString("InstalledPendingReboot"));
            allItem.Add(resMan.GetString("NotApplicable"));
            allItem.Add(resMan.GetString("NotInstalled"));
            allItem.Add(resMan.GetString("Unknown"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("All"), allItem);

            List<object> neededButNotInstalled = new List<object>();
            neededButNotInstalled.Add(resMan.GetString("Downloaded"));
            neededButNotInstalled.Add(resMan.GetString("Failed"));
            neededButNotInstalled.Add(resMan.GetString("NotInstalled"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("NeededButNotInstalled"), neededButNotInstalled);

            List<object> installedOrNotApplicable = new List<object>();
            installedOrNotApplicable.Add(resMan.GetString("Installed"));
            installedOrNotApplicable.Add(resMan.GetString("InstalledPendingReboot"));
            installedOrNotApplicable.Add(resMan.GetString("NotApplicable"));

            chkCmbBxFilter.AddShortcut(resMan.GetString("InstalledOrNotApplicable"), installedOrNotApplicable);

            chkCmbBxFilter.SelectShortcut(resMan.GetString("All"), true);

            dgvReport.Columns["Groups"].HeaderText = resMan.GetString("Groups");
            dgvReport.Columns["Installed"].HeaderText = resMan.GetString("Installed");
            dgvReport.Columns["InstalledPendingReboot"].HeaderText = resMan.GetString("InstalledPendingReboot");
            dgvReport.Columns["NotInstalled"].HeaderText = resMan.GetString("NotInstalled");
            dgvReport.Columns["Downloaded"].HeaderText = resMan.GetString("Downloaded");
            dgvReport.Columns["NotApplicable"].HeaderText = resMan.GetString("NotApplicable");
            dgvReport.Columns["Failed"].HeaderText = resMan.GetString("Failed");
            dgvReport.Columns["Unknown"].HeaderText = resMan.GetString("Unknown");
            dgvReport.Columns["Approval"].HeaderText = resMan.GetString("Approval");

            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("SendDetectNow"), "DetectNow"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("SendReportNow"), "ReportNow"));
            ctxMnuCommand.Items.Add(new ToolStripSeparator());
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("ShowPendingUpdates"), "ShowPendingUpdates"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("InstallPendingUpdates"), "InstallPendingUpdates"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("InstallThisUpdate"), "InstallThisUpdate"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("ShowUpdateEventHistory"), "ShowUpdateEventHistory"));
            ctxMnuCommand.Items.Add(new ToolStripSeparator());
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("ShowCurrentLogonUser"), "ShowCurrentLogonUser"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("ShowWindowsUpdateLog"), "ShowWindowsUpdateLog"));
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("CleanSoftwareDistributionFolder"), "CleanSoftwareDistributionFolder"));
            ctxMnuCommand.Items.Add(new ToolStripSeparator());
            ctxMnuCommand.Items.Add(GetItem(resMan.GetString("SendRebootNow"), "RebootNow"));

            foreach (DataGridViewColumn column in dgvComputerStatus.Columns)
                ctxMnuHeader.Items.Add(GetItem(column.HeaderText, column.Name, column.Visible));

            LockFunctionnalities(_wsus.IsReplica);

            displayReportThread = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReport));
            displayReportThread.IsBackground = false;
            this.chkBxShowOnlineComputersOnly.Checked = Properties.Settings.Default.ShowOnlineComputersOnly;
        }

        public new void Dispose()
        {
            cancelDisplayReport = true;
            cancelHighLightOnLineComputers = true;
            cancelSearchOnlineComputers = true;

            if (displayReportThread != null && displayReportThread.ThreadState != System.Threading.ThreadState.Unstarted)
            {
                displayReportThread.Abort();
                displayReportThread.Join(500);
            }
            displayReportThread = null;

            if (highLightOnLineComputersThread != null &&
                (highLightOnLineComputersThread.ThreadState != System.Threading.ThreadState.Unstarted || highLightOnLineComputersThread.ThreadState != System.Threading.ThreadState.Stopped))
            {
                highLightOnLineComputersThread.Abort();
                highLightOnLineComputersThread.Join(500);
            }
            highLightOnLineComputersThread = null;

            base.Dispose(true);
        }

        #region (Methods - Méthodes)

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

        internal void SetComputerGroups(ComputerGroup computerGroups, TreeNode allComputersNode)
        {
            Logger.EnteringMethod();
            _computerGroups = computerGroups;
            cmbBxComputerGroup.Items.Clear();
            FillComputerGroup(computerGroups);
        }

        private void FillComputerGroup(ComputerGroup computerGrouptoAdd)
        {
            Logger.EnteringMethod(computerGrouptoAdd.Name);
            cmbBxComputerGroup.Items.Add(computerGrouptoAdd);
            int index = dgvReport.Rows.Add();
            dgvReport.Rows[index].Cells["Groups"].Value = computerGrouptoAdd;
            foreach (ComputerGroup group in computerGrouptoAdd.InnerComputerGroup)
            {
                FillComputerGroup(group);
            }
        }

        internal void DisplayUpdates(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            ClearDisplay();

            // Informations Tab

            if (updates != null && updates.Count != 0)
            {
                this.Enabled = true;
                Logger.Write("updates count : " + updates.Count.ToString());
                IUpdate update = updates[0];

                if ((update.CompanyTitles.Count != 0) && (!string.IsNullOrEmpty(update.CompanyTitles[0])))
                    txtBxCompany.Text = update.CompanyTitles[0].ToString();
                if ((update.ProductTitles.Count != 0) && (!string.IsNullOrEmpty(update.ProductTitles[0])))
                    txtBxProductTitle.Text = update.ProductTitles[0].ToString();

                chkBxIsApproved.CheckState = GetApproveState(updates);
                chkBxIsDeclined.CheckState = GetDeclineState(updates);
                chkBxIsExpired.CheckState = GetExpireState(updates);
                chkBxIsSupersedes.CheckState = GetSupersededState(updates);

                if (updates.Count > 1)
                {
                    btnRevise.Enabled = false;
                    btnDelete.Enabled = !HasSomeApprove(updates);
                    txtBxTitle.Text = "*";
                    lnkLbAdditionnalInformationURL.Text = string.Empty;
                    txtBxDescription.Text = "*";
                }
                else
                {
                    if (!string.IsNullOrEmpty(update.Title))
                        txtBxTitle.Text = update.Title;
                    if (update.CreationDate != null)
                        txtBxCreationDate.Text = update.CreationDate.ToString();
                    if (update.ArrivalDate != null)
                        txtBxArrivalDate.Text = update.ArrivalDate.ToString();
                    btnDecline.Enabled = !update.IsDeclined && !_wsus.IsReplica;
                    btnExpire.Enabled = (update.PublicationState != PublicationState.Expired && !_wsus.IsReplica);
                    btnRevise.Enabled = !_wsus.IsReplica && _wsus.IsConsoleVersionAllowPublication();
                    btnDelete.Enabled = !update.IsApproved;

                    if ((update.AdditionalInformationUrls.Count != 0) && (!string.IsNullOrEmpty(update.AdditionalInformationUrls[0].ToString())))
                    {
                        lnkLbAdditionnalInformationURL.Text = update.AdditionalInformationUrls[0].ToString();
                        lnkLbAdditionnalInformationURL.Enabled = true;
                    }
                    else
                    {
                        lnkLbAdditionnalInformationURL.Text = string.Empty;
                        lnkLbAdditionnalInformationURL.Enabled = false;
                    }
                    if (!string.IsNullOrEmpty(update.Description))
                        txtBxDescription.Text = FormatDescription(update.Description);
                    try
                    {
                        System.Collections.ObjectModel.ReadOnlyCollection<Microsoft.UpdateServices.Administration.IInstallableItem> items = update.GetInstallableItems();
                        if (items.Count != 0 && items[0].Files.Count != 0 && items[0].Files[0].FileUri != null)
                            lnkLblFolder.Text = items[0].Files[0].FileUri.ToString();
                    }
                    catch (Exception) { }

                    if (!System.IO.File.Exists(GetPathFromUrl(lnkLblFolder.Text)))
                    {
                        lnkLblFolder.BackColor = Color.Red;
                        lnkLblFolder.Enabled = false;
                    }
                    else
                    {
                        lnkLblFolder.BackColor = Color.White;
                        lnkLblFolder.Enabled = true;
                    }
                    lnkLblId.Text = update.Id.UpdateId.ToString();
                    if (!IsUpdateServicesPackagesFolderExists(lnkLblId.Text) && !_wsus.IsReplica)
                    {
                        lnkLblId.BackColor = Color.Orange;
                        lnkLblId.Enabled = false;
                    }
                    else
                    {
                        lnkLblId.BackColor = Color.White;
                        lnkLblId.Enabled = true;
                    }
                }
                // Status Tab

                if (ViewedUpdates.Count == 1)
                {
                    if (cmbBxComputerGroup.SelectedItem != null)
                    {
                        Guid targetGroupId = (cmbBxComputerGroup.SelectedItem as ComputerGroup).ComputerGroupId;
                        UpdateInstallationInfoCollection updateInfo = _wsus.GetUpdateInstallationInfoPerComputerTarget(targetGroupId, ViewedUpdates[0]);

                        this.Cursor = Cursors.WaitCursor;
                        foreach (IUpdateInstallationInfo info in updateInfo)
                        {
                            if (_filter.Contains(info.UpdateInstallationState))
                            {
                                IComputerTarget computer = _wsus.GetComputerTarget(info.ComputerTargetId);
                                dgvComputerStatus.Rows.Add(new ADComputer(computer.FullDomainName),
                                    resMan.GetString(info.UpdateInstallationState.ToString()),
                                    resMan.GetString(info.UpdateApprovalAction.ToString()),
                                    computer.LastSyncTime.ToLocalTime().ToString(),
                                    computer.LastReportedStatusTime.ToLocalTime().ToString());
                            }
                        }
                        this.Cursor = Cursors.Default;
                        if (dgvComputerStatus.SortedColumn != null)
                            dgvComputerStatus.Sort(dgvComputerStatus.SortedColumn, (dgvComputerStatus.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
                    }
                    cancelHighLightOnLineComputers = true;
                    cancelSearchOnlineComputers = true;

                    if (highLightOnLineComputersThread != null && highLightOnLineComputersThread.ThreadState == System.Threading.ThreadState.Running)
                    {
                        highLightOnLineComputersThread.Abort();
                        highLightOnLineComputersThread.Join(500);
                        highLightOnLineComputersThread = null;
                    }

                    highLightOnLineComputersThread = new System.Threading.Thread(new System.Threading.ThreadStart(HighLightOnLineComputers));
                    highLightOnLineComputersThread.Priority = System.Threading.ThreadPriority.BelowNormal;
                    cancelHighLightOnLineComputers = false;
                    cancelSearchOnlineComputers = false;
                    highLightOnLineComputersThread.Start();
                }
                // Report Tab

                ComputeReport();
            }
            else
            {
                this.Enabled = false;
            }
        }

        private string FormatDescription(string textToFormat)
        {
            return textToFormat.Replace("\n", "\r\n");
        }

        private void HighLightOnLineComputers()
        {
            Logger.EnteringMethod();
            string computerName = string.Empty;
            bool readyToSearch = false;
            DataGridViewRow[] rows = null;

            Action action = () =>
                {
                    readyToSearch = (tabUpdateDetailViewer.SelectedTab == tabUpdateDetailViewer.TabPages["TabStatus"] && !cancelSearchOnlineComputers && dgvComputerStatus.Rows.Count > 0);
                    rows = new DataGridViewRow[dgvComputerStatus.Rows.Count];
                    dgvComputerStatus.Rows.CopyTo(rows, 0);
                };
            if (!this.Disposing && !this.IsDisposed)
                this.Invoke(action);

            if (readyToSearch)
            {
                System.Threading.CountdownEvent countDown = new System.Threading.CountdownEvent(1);

                foreach (DataGridViewRow row in rows)
                {
                    if (cancelHighLightOnLineComputers)
                        break;
                    countDown.AddCount();
                    SearchOnlineComputerData data = new SearchOnlineComputerData();
                    data.Row = row;
                    data.CountDown = countDown;
                    System.Threading.ThreadPool.QueueUserWorkItem(SearchOnlineComputers, (object)data);
                }
                countDown.Signal();
                countDown.Wait(rows.Length * 10000);
                Action endingAction = () =>
                {
                    if (dgvComputerStatus.SortedColumn != null && dgvComputerStatus.SortedColumn.Name == "OnLine")
                        dgvComputerStatus.Sort(dgvComputerStatus.SortedColumn, (dgvComputerStatus.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
                };
                this.Invoke(endingAction);
            }
        }

        private void SearchOnlineComputers(object data)
        {
            SearchOnlineComputerData computerData = (SearchOnlineComputerData)data;
            object[] args = new object[2];
            DataGridViewRow currentRow = computerData.Row;
            System.Threading.CountdownEvent countDown = computerData.CountDown;

            try
            {
                if (!cancelSearchOnlineComputers)
                {
                    ADComputer computer = (ADComputer)currentRow.Cells["Computer"].Value;
                    args[0] = currentRow;
                    args[1] = "No";
                    if (computer.Ping(100))
                    {
                        args[1] = "Yes";
                    }
                }
            }
            catch (Exception) { }
            try
            {
                if (!cancelSearchOnlineComputers)
                {
                    DisplayOnLineStatusDelegate DisplayOnLineStatusHandler = new DisplayOnLineStatusDelegate(DisplayOnLineStatus);
                    DisplayOnLineStatusHandler(args);
                }
            }
            catch (Exception) { }
            finally { countDown.Signal(); }
        }

        private void DisplayOnLineStatus(object[] obj)
        {
            lock (dgvComputerStatus)
            {
                if (!cancelSearchOnlineComputers)
                {
                    DataGridViewRow currentRow = (DataGridViewRow)obj[0];
                    string online = (string)obj[1];
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
                    currentRow.Cells["OnLine"].Value = resMan.GetString(online);
                    Color cellColor = (online == "Yes") ? Color.PaleGreen : Color.Orange;
                    currentRow.Cells["OnLine"].Style.BackColor = cellColor;
                    currentRow.Cells["OnLine"].Style.SelectionBackColor = cellColor;
                    currentRow.Cells["OnLine"].Style.ForeColor = Color.Black;
                    currentRow.Cells["OnLine"].Style.SelectionForeColor = Color.Black;
                    currentRow.Visible = (!chkBxShowOnlineComputersOnly.Checked || online == "Yes");
                }
            }
        }

        private void ComputeReport()
        {
            if (!updateReportDisplayed && tabUpdateDetailViewer.SelectedTab == tabUpdateDetailViewer.TabPages["TabReport"])
            {
                ClearReport();
                cancelDisplayReport = true;

                if (displayReportThread.IsAlive)
                {
                    displayReportThread.Join(2000);
                }
                cancelDisplayReport = false;
                displayReportThread = null;
                displayReportThread = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReport));
                displayReportThread.Priority = System.Threading.ThreadPriority.Lowest;
                displayReportThread.Start();
                updateReportDisplayed = true;
            }
        }

        private bool HasSomeApprove(UpdateCollection updates)
        {
            Logger.EnteringMethod(updates.Count.ToString());
            foreach (IUpdate update in updates)
                if (update.IsApproved)
                {
                    Logger.Write("returning true for : " + update.Title);
                    return true;
                }
            Logger.Write("returning false");
            return false;
        }

        internal void ResetControl()
        {
            Logger.EnteringMethod();
            _update = null;
            this.cancelDisplayReport = true;
            updateReportDisplayed = false;
            ClearDisplay();
            dgvReport.Rows.Clear();
        }

        internal void ClearDisplay()
        {
            Logger.EnteringMethod();
            txtBxCompany.Text = "";
            txtBxProductTitle.Text = "";
            txtBxTitle.Text = "";
            txtBxCreationDate.Text = "";
            txtBxArrivalDate.Text = "";
            lnkLbAdditionnalInformationURL.Text = string.Empty;
            txtBxDescription.Text = "";
            lnkLblFolder.Text = string.Empty;
            lnkLblFolder.BackColor = SystemColors.Control;
            lnkLblId.Text = string.Empty;
            lnkLblId.BackColor = SystemColors.Control;

            dgvComputerStatus.Rows.Clear();
            dgvComputerStatus.Refresh();
        }

        internal void UpdateSelectionChanged(DataGridViewSelectedRowCollection rows)
        {
            Logger.EnteringMethod(rows.Count.ToString());
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in rows)
            {
                IUpdate update = (IUpdate)row.Cells["UpdateId"].Value;
                Logger.Write("Adding : " + update.Title);
                updates.Add(update);
            }

            ViewedUpdates = updates;
        }

        internal void LockFunctionnalities(bool isLock)
        {
            btnApprove.Enabled = !isLock;
            btnDecline.Enabled = !isLock;
            btnExpire.Enabled = !isLock;
            if (!isLock)
                btnRevise.Enabled = _wsus.IsConsoleVersionAllowPublication();
            else
                btnRevise.Enabled = false;
        }

        internal void RunningLongOperation(bool running)
        {
            if (running)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }

        private void DisplayReport()
        {
            Logger.EnteringMethod();
            char[] oneSpace = new char[] { ' ' };
            ReportResult resultToDisplay = new ReportResult();
            UpdateInstallationInfoCollection updateInfo;

            if (ViewedUpdates != null && ViewedUpdates.Count == 1)
            {
                IUpdate update = ViewedUpdates[0];
                int installedCount;
                int installedPendingRebootCount;
                int notInstalledCount;
                int downloadedCount;
                int notApplicableCount;
                int failedCount;
                int unknownCount;
                UpdateInstallationState state;

                foreach (DataGridViewRow row in dgvReport.Rows)
                {
                    installedCount = 0;
                    installedPendingRebootCount = 0;
                    notInstalledCount = 0;
                    downloadedCount = 0;
                    notApplicableCount = 0;
                    failedCount = 0;
                    unknownCount = 0;
                    resultToDisplay.Approval = UpdateApprovalAction.NotApproved;
                    resultToDisplay.IsOptional = false;

                    Guid computerGroupID = (row.Cells["Groups"].Value as ComputerGroup).ComputerGroupId;
                    if (!cancelDisplayReport)
                    {
                        updateInfo = _wsus.GetUpdateInstallationInfoPerComputerTarget(computerGroupID, update);
                        UpdateApprovalCollection approvals = _wsus.GetUpdateApprovalStatus(computerGroupID, update);
                        if (approvals.Count != 0)
                        {
                            resultToDisplay.Approval = approvals[0].Action;
                            resultToDisplay.IsOptional = approvals[0].IsOptional;
                        }
                    }
                    else
                        break;

                    foreach (IUpdateInstallationInfo info in updateInfo)
                    {
                        if (cancelDisplayReport)
                            break;
                        state = info.UpdateInstallationState;
                        switch (state)
                        {
                            case UpdateInstallationState.Downloaded:
                                downloadedCount++;
                                break;
                            case UpdateInstallationState.Failed:
                                failedCount++;
                                break;
                            case UpdateInstallationState.Installed:
                                installedCount++;
                                break;
                            case UpdateInstallationState.InstalledPendingReboot:
                                installedPendingRebootCount++;
                                break;
                            case UpdateInstallationState.NotApplicable:
                                notApplicableCount++;
                                break;
                            case UpdateInstallationState.NotInstalled:
                                notInstalledCount++;
                                break;
                            case UpdateInstallationState.Unknown:
                                unknownCount++;
                                break;
                            default:
                                break;
                        }
                    }
                    resultToDisplay.Row = row;
                    resultToDisplay.InstalledCount = installedCount;
                    resultToDisplay.InstalledPendingRebootCount = installedPendingRebootCount;
                    resultToDisplay.NotInstalledCount = notInstalledCount;
                    resultToDisplay.DownloadedCount = downloadedCount;
                    resultToDisplay.NotApplicableCount = notApplicableCount;
                    resultToDisplay.FailedCount = failedCount;
                    resultToDisplay.UnknownCount = unknownCount;
                    if (!cancelDisplayReport)
                        FillRow(resultToDisplay);

                }
                Action action = () =>
                {
                    if (dgvReport.SortedColumn != null)
                        dgvReport.Sort(dgvReport.SortedColumn, (dgvReport.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);

                    this.chkBxShowRowsWithApproval.Enabled = true;
                    this.chkBxShowRowsWithValues.Enabled = true;
                };
                if (!cancelDisplayReport)
                    this.Invoke(action);
            }
            else
                ClearReport();
        }

        private void ClearReport()
        {
            ReportResult resultToDisplay = new ReportResult();

            this.chkBxShowRowsWithApproval.Enabled = false;
            this.chkBxShowRowsWithValues.Enabled = false;

            dgvReport.SuspendLayout();
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                resultToDisplay.Row = row;
                resultToDisplay.InstalledCount = 0;
                resultToDisplay.InstalledPendingRebootCount = 0;
                resultToDisplay.NotInstalledCount = 0;
                resultToDisplay.DownloadedCount = 0;
                resultToDisplay.NotApplicableCount = 0;
                resultToDisplay.FailedCount = 0;
                resultToDisplay.UnknownCount = 0;
                resultToDisplay.Approval = UpdateApprovalAction.All;
                FillRow(resultToDisplay);
            }
            dgvReport.ResumeLayout();
            dgvReport.Refresh();
        }

        private void FillRow(ReportResult resultToDisplay)
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new Action<ReportResult>(FillRow), new object[] { resultToDisplay });
                return;
            }

            if (!this.cancelDisplayReport)
            {
                if (resultToDisplay.InstalledCount != 0)
                    resultToDisplay.Row.Cells["Installed"].Style.BackColor = Properties.Settings.Default.InstalledColor;
                else
                    resultToDisplay.Row.Cells["Installed"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["Installed"].Value = resultToDisplay.InstalledCount;

                if (resultToDisplay.InstalledPendingRebootCount != 0)
                    resultToDisplay.Row.Cells["InstalledPendingReboot"].Style.BackColor = Properties.Settings.Default.InstalledPendingRebootColor;
                else
                    resultToDisplay.Row.Cells["InstalledPendingReboot"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["InstalledPendingReboot"].Value = resultToDisplay.InstalledPendingRebootCount;

                if (resultToDisplay.DownloadedCount != 0)
                    resultToDisplay.Row.Cells["Downloaded"].Style.BackColor = Properties.Settings.Default.DownloadedColor;
                else
                    resultToDisplay.Row.Cells["Downloaded"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["Downloaded"].Value = resultToDisplay.DownloadedCount;

                if (resultToDisplay.NotApplicableCount != 0)
                    resultToDisplay.Row.Cells["NotApplicable"].Style.BackColor = Properties.Settings.Default.NotApplicableColor;
                else
                    resultToDisplay.Row.Cells["NotApplicable"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["NotApplicable"].Value = resultToDisplay.NotApplicableCount;

                if (resultToDisplay.NotInstalledCount != 0)
                    resultToDisplay.Row.Cells["NotInstalled"].Style.BackColor = Properties.Settings.Default.NotInstalledColor;
                else
                    resultToDisplay.Row.Cells["NotInstalled"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["NotInstalled"].Value = resultToDisplay.NotInstalledCount;

                if (resultToDisplay.UnknownCount != 0)
                    resultToDisplay.Row.Cells["Unknown"].Style.BackColor = Properties.Settings.Default.UnknownColor;
                else
                    resultToDisplay.Row.Cells["Unknown"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["Unknown"].Value = resultToDisplay.UnknownCount;

                if (resultToDisplay.FailedCount != 0)
                    resultToDisplay.Row.Cells["Failed"].Style.BackColor = Properties.Settings.Default.FailedColor;
                else
                    resultToDisplay.Row.Cells["Failed"].Style.BackColor = Color.White;
                resultToDisplay.Row.Cells["Failed"].Value = resultToDisplay.FailedCount;

                if (resultToDisplay.Approval != UpdateApprovalAction.All)
                {
                    resultToDisplay.Row.Cells["Approval"].Value = resMan.GetString(resultToDisplay.Approval.ToString());
                    if (resultToDisplay.Approval == UpdateApprovalAction.Install && resultToDisplay.IsOptional)
                        resultToDisplay.Row.Cells["Approval"].Value += " (" + resMan.GetString("Optional") + ")";
                }
                else
                    resultToDisplay.Row.Cells["Approval"].Value = string.Empty;

                resultToDisplay.Row.Visible = (!chkBxShowRowsWithValues.Checked || resultToDisplay.GetTotal() != 0) && (!chkBxShowRowsWithApproval.Checked || resultToDisplay.Approval != UpdateApprovalAction.NotApproved);
            }
        }

        private System.Windows.Forms.CheckState GetApproveState(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsApproved)
                {
                    Logger.Write("Update : " + update.Title + " is Approved");
                    nbrTrue++;
                }
                else
                {
                    Logger.Write("Update : " + update.Title + " is not Approved");
                    nbrFalse++;
                }
            }
            Logger.Write("nbrTrue : " + nbrTrue.ToString() + " / nbrFalse : " + nbrFalse.ToString());
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetDeclineState(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsDeclined)
                {
                    Logger.Write(update.Title + " is declined");
                    nbrTrue++;
                }
                else
                {
                    Logger.Write(update.Title + " is not declined");
                    nbrFalse++;
                }
            }
            Logger.Write("nbrTrue : " + nbrTrue.ToString() + " / nbrFalse : " + nbrFalse.ToString());
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetExpireState(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.PublicationState == PublicationState.Expired)
                {
                    Logger.Write(update.Title + " is Expired");
                    nbrTrue++;
                }
                else
                {
                    Logger.Write(update.Title + " is not Expired");
                    nbrFalse++;
                }
            }
            Logger.Write("nbrTrue : " + nbrTrue.ToString() + " / nbrFalse : " + nbrFalse.ToString());
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private System.Windows.Forms.CheckState GetSupersededState(UpdateCollection updates)
        {
            int nbrTrue = 0;
            int nbrFalse = 0;

            foreach (IUpdate update in updates)
            {
                if (update.IsSuperseded)
                {
                    Logger.Write(update.Title + " is superseded");
                    nbrTrue++;
                }
                else
                {
                    Logger.Write(update.Title + " is not superseded");
                    nbrFalse++;
                }
            }
            Logger.Write("nbrTrue : " + nbrTrue.ToString() + " / nbrFalse : " + nbrFalse.ToString());
            if (nbrTrue == 0)
                return System.Windows.Forms.CheckState.Unchecked;
            if (nbrFalse == 0)
                return System.Windows.Forms.CheckState.Checked;

            return System.Windows.Forms.CheckState.Indeterminate;
        }

        private string GetPathFromUrl(string url)
        {
            Logger.EnteringMethod(url);
            if (url.ToLower().StartsWith("http://"))
            {
                url = url.Substring(7);
                url = url.Replace('/', '\\');
                url = url.Replace("Content", "WsusContent");
                int index = url.IndexOf(':');
                if (index != -1)
                    url = url.Substring(0, index) + url.Substring(url.IndexOf('\\'));
                Logger.Write("Returning " + @"\\" + url);
                return @"\\" + url;
            }
            Logger.Write("**** Returning Empty");
            return string.Empty;
        }

        private bool IsUpdateServicesPackagesFolderExists(string subfolder)
        {
            Logger.EnteringMethod(subfolder);
            string updateFolder = @"\\" + _wsus.CurrentServer.Name + @"\UpdateServicesPackages\" + subfolder;
            bool result = System.IO.Directory.Exists(updateFolder);
            Logger.Write("Returing " + result.ToString());
            return result;
        }

        private void FilterOnlineComputers()
        {
            Logger.EnteringMethod();
            foreach (DataGridViewRow row in dgvComputerStatus.Rows)
                row.Visible = (!chkBxShowOnlineComputersOnly.Checked || (row.Cells["OnLine"].Value != null && row.Cells["OnLine"].Value.ToString() == resMan.GetString("Yes")));
        }

        internal void ExportAnUpdate()
        {
            Logger.EnteringMethod();

            if (ViewedUpdates != null && ViewedUpdates.Count == 1)
            {
                try
                {
                    IUpdate updateToExport = ViewedUpdates[0];
                    Logger.Write("will export : " + updateToExport.Title);

                    string tempFolder = Tools.Utilities.GetTempFolder();
                    if (System.IO.Directory.Exists(tempFolder + updateToExport.Id.UpdateId.ToString()))
                        System.IO.Directory.Delete(tempFolder + updateToExport.Id.UpdateId.ToString(), true);
                    System.Threading.Thread.Sleep(500);
                    System.IO.Directory.CreateDirectory(tempFolder + updateToExport.Id.UpdateId.ToString());

                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "CAB files|*.cab";
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.CheckPathExists = true;
                    saveFileDialog.DefaultExt = ".cab";
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.ValidateNames = true;
                    saveFileDialog.FileName = updateToExport.Title.Substring(0, (updateToExport.Title.Length < 41 ? updateToExport.Title.Length : 40)) + ".cab";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        frmExportProgress = new FrmExportUpdateProgress();
                        System.IO.FileInfo destinationFile = new System.IO.FileInfo(saveFileDialog.FileName);
                        if (destinationFile.Extension.ToLower() != ".cab")
                        {
                            destinationFile = new System.IO.FileInfo(saveFileDialog.FileName + ".cab");
                            MessageBox.Show(saveFileDialog.FileName + " " + resMan.GetString("WillBeRenameTo") + " " + destinationFile.FullName);
                        }
                        frmExportProgress.Description = resMan.GetString("SavingMetaData");
                        frmExportProgress.Show();
                        frmExportProgress.Refresh();
                        SoftwareDistributionPackage sdp = _wsus.GetMetaData(updateToExport);
                        if (sdp != null)
                        {
                            sdp.Save(tempFolder + updateToExport.Id.UpdateId.ToString() + @"\" + updateToExport.Id.UpdateId.ToString() + ".xml");
                            frmExportProgress.Description = resMan.GetString("CopyingFiles");
                            String sourceCabFolder  = @"\\" + _wsus.CurrentServer.Name + @"\UpdateServicesPackages\" + updateToExport.Id.UpdateId.ToString();
                            foreach (string file in System.IO.Directory.GetFiles(sourceCabFolder, "*.cab", System.IO.SearchOption.TopDirectoryOnly))
                            {
                                System.IO.File.Copy(file, tempFolder + updateToExport.Id.UpdateId.ToString() + @"\" + (new System.IO.FileInfo(file)).Name);
                            }
                            frmExportProgress.Description = resMan.GetString("CompressingFiles");
                            Microsoft.Deployment.Compression.Cab.CabInfo compressor = new Microsoft.Deployment.Compression.Cab.CabInfo(saveFileDialog.FileName);

                            EventHandler<Microsoft.Deployment.Compression.ArchiveProgressEventArgs> ExportProgressionEventHandler = new EventHandler<ArchiveProgressEventArgs>(ExportProgressionChanged);

                            compressor.Pack(tempFolder + updateToExport.Id.UpdateId.ToString(), true, Microsoft.Deployment.Compression.CompressionLevel.None, ExportProgressionEventHandler);
                            frmExportProgress.Description = resMan.GetString("UpdateSuccessfullyExported");
                            MessageBox.Show(resMan.GetString("UpdateSuccessfullyExported"));
                            frmExportProgress.Close();
                            frmExportProgress = null;
                            Tools.Utilities.DeleteFolder(tempFolder);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write("**** " + ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show(resMan.GetString("SelectOneAndOnlyOneUpdate"));
        }

        private void ExportProgressionChanged(object sender, Microsoft.Deployment.Compression.ArchiveProgressEventArgs args)
        {
            if (args.TotalFileBytes != 0)
            {
                frmExportProgress.SetProgressBar((int)(args.FileBytesProcessed * 100 / args.TotalFileBytes));
            }
        }

        #endregion

        #region (Properties - Propriétés)

        internal UpdateCollection ViewedUpdates
        {
            get { return _update; }
            set
            {
                Logger.EnteringMethod();
                _update = value;
                updateReportDisplayed = false;
                DisplayUpdates(ViewedUpdates);
            }
        }

        #endregion

        #region (response to events - Réponses aux événements)

        private void cmbBxComputerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DisplayUpdates(ViewedUpdates);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnApprove.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (ApproveUpdate != null)
                ApproveUpdate(ViewedUpdates);
            btnApprove.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnDecline.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (DeclineUpdate != null)
                DeclineUpdate(ViewedUpdates);
            btnDecline.Enabled = !_wsus.IsReplica;
            this.Cursor = Cursors.Default;
        }

        private void btnExpire_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnExpire.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (ExpireUpdate != null)
                ExpireUpdate(ViewedUpdates);
            this.Cursor = Cursors.Default;
        }

        private void btnRevise_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnRevise.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            CertificateHelper.CertificateStatus certStatus = _wsus.GetCertificateStatus;
            if (certStatus == CertificateHelper.CertificateStatus.Valid || certStatus == CertificateHelper.CertificateStatus.NearExpiration)
            {
                if (ReviseUpdate != null)
                    ReviseUpdate(ViewedUpdates[0]);
            }
            else
                MessageBox.Show(resMan.GetString("SolveCertificateProblemBeforePublishing"));

            btnRevise.Enabled = !_wsus.IsReplica && _wsus.IsConsoleVersionAllowPublication();
            this.Cursor = Cursors.Default;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnDelete.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (DeleteUpdate != null)
                DeleteUpdate(ViewedUpdates);
            btnDelete.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void chkCmbBxFilter_SelectionChanged()
        {
            Logger.EnteringMethod();
            _filter.Clear();
            List<object> selectedObj = chkCmbBxFilter.SelectedItems;

            foreach (object obj in selectedObj)
            {
                string item = obj.ToString();
                if (item == resMan.GetString("Downloaded"))
                    _filter.Add(UpdateInstallationState.Downloaded);

                if (item == resMan.GetString("Failed"))
                    _filter.Add(UpdateInstallationState.Failed);

                if (item == resMan.GetString("Installed"))
                    _filter.Add(UpdateInstallationState.Installed);

                if (item == resMan.GetString("InstalledPendingReboot"))
                    _filter.Add(UpdateInstallationState.InstalledPendingReboot);

                if (item == resMan.GetString("NotApplicable"))
                    _filter.Add(UpdateInstallationState.NotApplicable);

                if (item == resMan.GetString("NotInstalled"))
                    _filter.Add(UpdateInstallationState.NotInstalled);

                if (item == resMan.GetString("Unknown"))
                    _filter.Add(UpdateInstallationState.Unknown);
            }
            DisplayUpdates(ViewedUpdates);
        }

        private void ctxMnuStripCommand_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.EnteringMethod(e.ClickedItem.Name);
            List<ADComputer> targetComputers = new List<ADComputer>();
            FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

            foreach (DataGridViewRow row in dgvComputerStatus.SelectedRows)
            {
                if (row.Visible)
                    targetComputers.Add((ADComputer)row.Cells[0].Value);
            }
            ctxMnuCommand.Hide();
            Credentials cred = Credentials.GetInstance();
            if (targetComputers.Count != 0 && e.ClickedItem.Name != "ShowUpdateEventHistory")
                if (cred.InitializeCredential() == false)
                    return;
            lblCredentialNotice.Text = cred.CredentialNotice;

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
                case "InstallThisUpdate":
                    FrmInstallPendingUpdatesNow installThisUpdate = new FrmInstallPendingUpdatesNow();
                    installThisUpdate.Username = cred.Login;
                    installThisUpdate.Password = cred.Password;
                    installThisUpdate.Computers = targetComputers;
                    installThisUpdate.SearchString = "IsInstalled=0 And IsHidden=0 And Type='Software' And UpdateID='" + ViewedUpdates[0].Id.UpdateId.ToString() + "'";
                    installThisUpdate.PersonalizeSearchString = true;
                    installThisUpdate.ShowDialog();
                    break;
                case "ShowCurrentLogonUser":
                    this.Cursor = Cursors.WaitCursor;
                    ADComputer remoteComputer = (ADComputer)dgvComputerStatus.SelectedRows[0].Cells["Computer"].Value;
                    string logonUser = remoteComputer.GetCurrentLogonUser(cred.Login, cred.Password);
                    if (!string.IsNullOrEmpty(logonUser))
                        MessageBox.Show(resMan.GetString("CurrentLogonUserIs") + " : " + logonUser);
                    else
                        MessageBox.Show(resMan.GetString("UnableToGetLogonUser"));
                    this.Cursor = Cursors.Default;
                    break;
                case "ShowUpdateEventHistory":
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        IComputerTarget computerWithEvent = _wsus.GetComputerTargetByName(dgvComputerStatus.SelectedRows[0].Cells["Computer"].Value.ToString());
                        if (computerWithEvent != null)
                        {
                            UpdateEventCollection eventCollection = _wsus.GetUpdateEventHistory(computerWithEvent);
                            FrmEventDisplayer frmEventDisplayer = new FrmEventDisplayer(eventCollection, computerWithEvent);
                            frmEventDisplayer.ShowDialog(this);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                    }
                    this.Cursor = Cursors.Default;
                    break;
                case "ShowWindowsUpdateLog":
                    this.Cursor = Cursors.WaitCursor;
                    ADComputer computer = (ADComputer)dgvComputerStatus.SelectedRows[0].Cells["Computer"].Value;
                    if (!computer.Ping(100))
                        MessageBox.Show(resMan.GetString("ComputerUnreachable"));
                    else
                        computer.OpenWindowsUpdateLog(cred.Login, cred.Password);
                    this.Cursor = Cursors.Default;
                    break;
                case "CleanSoftwareDistributionFolder":
                    if (cred.InitializeCredential() == false)
                        return;
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
            Logger.EnteringMethod();
            (e.ClickedItem as ToolStripMenuItem).Checked = !(e.ClickedItem as ToolStripMenuItem).Checked;

            foreach (ToolStripMenuItem menuItem in ctxMnuHeader.Items)
                dgvComputerStatus.Columns[menuItem.Name].Visible = menuItem.Checked;
        }

        private void tabUpdateDetailViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod(tabUpdateDetailViewer.SelectedTab.Name);
            if (tabUpdateDetailViewer.SelectedTab == tabUpdateDetailViewer.TabPages["TabReport"])
                ComputeReport();
        }

        private void FilterRowSelectionChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            int total = 0;
            foreach (DataGridViewRow row in dgvReport.Rows)
            {
                total = (int)row.Cells["installed"].Value +
                    (int)row.Cells["installedPendingReboot"].Value +
                    (int)row.Cells["notInstalled"].Value +
                    (int)row.Cells["downloaded"].Value +
                    (int)row.Cells["notApplicable"].Value +
                    (int)row.Cells["failed"].Value +
                    (int)row.Cells["unknown"].Value;
                row.Visible = (!chkBxShowRowsWithValues.Checked || total != 0) && (!chkBxShowRowsWithApproval.Checked || row.Cells["Approval"].Value.ToString() != resMan.GetString(UpdateApprovalAction.NotApproved.ToString()));
            }
        }

        private void dtGrdVReport_DoubleClick(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            tabUpdateDetailViewer.SelectedTab = tabUpdateDetailViewer.TabPages["tabStatus"];
            tabUpdateDetailViewer.Refresh();
            cmbBxComputerGroup.SelectedItem = (ComputerGroup)dgvReport.SelectedRows[0].Cells["Groups"].Value;

            cancelSearchOnlineComputers = true;

            if (highLightOnLineComputersThread != null && highLightOnLineComputersThread.ThreadState == System.Threading.ThreadState.Running)
            {
                highLightOnLineComputersThread.Abort();
                highLightOnLineComputersThread.Join(500);
                highLightOnLineComputersThread = null;
            }

            cancelSearchOnlineComputers = false;
            highLightOnLineComputersThread = new System.Threading.Thread(new System.Threading.ThreadStart(HighLightOnLineComputers));
            highLightOnLineComputersThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            highLightOnLineComputersThread.Start();
        }

        private void dgvComputerStatus_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
            {
                bool IsInDomain = !string.IsNullOrEmpty(ADHelper.GetDomainName());
                AdjustSelection(e.RowIndex);
                ctxMnuCommand.Items["ShowPendingUpdates"].Enabled = (dgvComputerStatus.SelectedRows.Count == 1 && IsInDomain && System.IO.File.Exists("ShowPendingUpdates.exe"));
                ctxMnuCommand.Items["ShowWindowsUpdateLog"].Enabled = (dgvComputerStatus.SelectedRows.Count == 1 && IsInDomain);
                ctxMnuCommand.Items["InstallPendingUpdates"].Enabled = IsInDomain && System.IO.File.Exists("InstallPendingUpdates.exe") && System.IO.File.Exists("Interop.WUApiLib.dll");
                ctxMnuCommand.Items["InstallThisUpdate"].Enabled = IsInDomain && System.IO.File.Exists("InstallPendingUpdates.exe") && System.IO.File.Exists("Interop.WUApiLib.dll") && ViewedUpdates.Count == 1;
                ctxMnuCommand.Items["CleanSoftwareDistributionFolder"].Enabled = IsInDomain;
                ctxMnuCommand.Items["ShowCurrentLogonUser"].Enabled = IsInDomain && (dgvComputerStatus.SelectedRows.Count == 1);
                ctxMnuCommand.Items["ShowUpdateEventHistory"].Enabled = (dgvComputerStatus.SelectedRows.Count == 1);
                ctxMnuCommand.Show(dgvComputerStatus, dgvComputerStatus.PointToClient(Cursor.Position));
            }
        }

        private void AdjustSelection(int ClickedIndex)
        {
            DataGridViewRow clickedRow = dgvComputerStatus.Rows[ClickedIndex];
            if (!dgvComputerStatus.SelectedRows.Contains(clickedRow))
            {
                dgvComputerStatus.ClearSelection();
                dgvComputerStatus.Rows[ClickedIndex].Selected = true;
            }
        }

        private void dgvComputerStatus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1)
                ctxMnuHeader.Show(dgvComputerStatus, dgvComputerStatus.PointToClient(Cursor.Position));
        }

        private void chkBxShowOnlineComputersOnly_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod(chkBxShowOnlineComputersOnly.Checked.ToString());
            Properties.Settings.Default.ShowOnlineComputersOnly = this.chkBxShowOnlineComputersOnly.Checked;
            Properties.Settings.Default.Save();
            FilterOnlineComputers();
        }

        private void txtBxDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtBxDescription.SelectAll();
        }

        private void lnkLbAdditionnalInformationURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo("IExplore.exe", lnkLbAdditionnalInformationURL.Text);
                System.Diagnostics.Process.Start(procInfo);
            }
            catch (Exception) { };
        }

        private void lnkLblFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.EnteringMethod();
            //   http:// wsus01/Content/4D/421BBF7E360DACA8B302BCFF84EC6486A2206C4D.cab
            if (!string.IsNullOrEmpty(lnkLblFolder.Text))
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    string updateFile = GetPathFromUrl(lnkLblFolder.Text);
                    Logger.Write(updateFile);
                    if (System.IO.File.Exists(updateFile))
                        System.Diagnostics.Process.Start("explorer.exe", @"/Select, " + updateFile);
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    Logger.Write(lnkLblFolder.Text);
                    Clipboard.Clear();
                    Clipboard.SetText(lnkLblFolder.Text);
                }
            }
        }

        private void lnkLblId_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.EnteringMethod(lnkLblId.Text);
            if (!string.IsNullOrEmpty(lnkLblId.Text))
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (IsUpdateServicesPackagesFolderExists(lnkLblId.Text))
                        System.Diagnostics.Process.Start("explorer.exe", @"\\" + _wsus.CurrentServer.Name + @"\UpdateServicesPackages\" + lnkLblId.Text);
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    Logger.Write(lnkLblId.Text);
                    Clipboard.Clear();
                    Clipboard.SetText(lnkLblId.Text);
                }
            }
        }

        private void dgvComputerStatus_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Name == "LastContact" || e.Column.Name == "LastReport")
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

        #endregion

        #region (Event Delegates - événements)

        public delegate void ApproveUpdateEventHandler(UpdateCollection udpatesToApprove);
        public event ApproveUpdateEventHandler ApproveUpdate;

        public delegate void DeclineUpdateEventHandler(UpdateCollection udpatesToDecline);
        public event DeclineUpdateEventHandler DeclineUpdate;

        public delegate void ExpireUpdateEventHandler(UpdateCollection updatesToExpire);
        public event ExpireUpdateEventHandler ExpireUpdate;

        public delegate void DeleteUpdateEventHandler(UpdateCollection udpatesToDelete);
        public event DeleteUpdateEventHandler DeleteUpdate;

        public delegate void ReviseUpdateEventHandler(IUpdate updateToRevise);
        public event ReviseUpdateEventHandler ReviseUpdate;

        #endregion
    }
}
