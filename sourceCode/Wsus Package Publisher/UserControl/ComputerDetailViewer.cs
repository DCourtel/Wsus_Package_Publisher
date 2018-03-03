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
    internal partial class ComputerDetailViewer : UserControl
    {
        internal struct ReportResult
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
            internal void ResetCounters()
            {
                InstalledCount = 0;
                InstalledPendingRebootCount = 0;
                NotInstalledCount = 0;
                DownloadedCount = 0;
                NotApplicableCount = 0;
                FailedCount = 0;
                UnknownCount = 0;
            }
        }

        DataGridViewSelectedRowCollection _selectedRows;
        WsusWrapper _wsus;
        ComputerControl _computerCtrl;
        public delegate void FillRowDelegate(object[] args);
        private System.Threading.Thread threadDisplayReport;
        private bool cancelDisplayReport;
        private bool _aborting = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(ComputerDetailViewer).Assembly);

        public ComputerDetailViewer()
        {
            Logger.EnteringMethod("ComputerDetailViewer");
            InitializeComponent();
            cancelDisplayReport = false;

            InstalledAfter = System.DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0));
            InstalledBefore = System.DateTime.Now;
            _wsus = WsusWrapper.GetInstance();
            _selectedRows = dtGvReport.SelectedRows;

            dtGvReport.Columns["Title"].HeaderText = resMan.GetString("Title");
            dtGvReport.Columns["ApprovalAction"].HeaderText = resMan.GetString("Approval");
            dtGvReport.Columns["Installed"].HeaderText = resMan.GetString("Installed");
            dtGvReport.Columns["InstalledPendingReboot"].HeaderText = resMan.GetString("InstalledPendingReboot");
            dtGvReport.Columns["NotInstalled"].HeaderText = resMan.GetString("NotInstalled");
            dtGvReport.Columns["Downloaded"].HeaderText = resMan.GetString("Downloaded");
            dtGvReport.Columns["NotApplicable"].HeaderText = resMan.GetString("NotApplicable");
            dtGvReport.Columns["Failed"].HeaderText = resMan.GetString("Failed");
            dtGvReport.Columns["Unknown"].HeaderText = resMan.GetString("Unknown");

            threadDisplayReport = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReportASynch));
            threadDisplayReport.IsBackground = false;
        }

        public new void Dispose()
        {
            cancelDisplayReport = true;
            _aborting = true;
            if (threadDisplayReport != null && threadDisplayReport.ThreadState != System.Threading.ThreadState.Unstarted)
            {
                threadDisplayReport.Abort();
                threadDisplayReport.Join(500);
            }
            threadDisplayReport = null;

            base.Dispose(true);
        }

        private void CheckDate()
        {
            if ((dtpInstalledAfter.Value > dtpInstalledBefore.Value) || (dtpInstalledBefore.Value < dtpInstalledAfter.Value))
                FlipDateTime();
            if (dtpInstalledBefore.Value.Date.Equals(dtpInstalledAfter.Value.Date))
            {
                dtpInstalledAfter.Enabled = false;
                dtpInstalledBefore.Enabled = false;
                dtpInstalledAfter.Value = dtpInstalledAfter.Value.Subtract(new TimeSpan(1, 0, 0, 0));
                dtpInstalledAfter.Enabled = true;
                dtpInstalledBefore.Enabled = true;
            }
        }

        private void FlipDateTime()
        {
            DateTime tempDateTime = new DateTime();
            dtpInstalledAfter.Enabled = false;
            dtpInstalledBefore.Enabled = false;
            tempDateTime = dtpInstalledAfter.Value;
            dtpInstalledAfter.Value = dtpInstalledBefore.Value;
            dtpInstalledBefore.Value = tempDateTime;
            dtpInstalledAfter.Enabled = true;
            dtpInstalledBefore.Enabled = true;
        }

        internal DateTime InstalledAfter
        {
            get { return dtpInstalledAfter.Value; }
            set { dtpInstalledAfter.Value = value; }
        }

        internal DateTime InstalledBefore
        {
            get { return dtpInstalledBefore.Value; }
            set { dtpInstalledBefore.Value = value; }
        }

        internal ComputerControl ComputerCtrl
        {
            private get { return _computerCtrl; }
            set { _computerCtrl = value; }
        }

        private DataGridViewSelectedRowCollection SelectedRows
        {
            get { return _selectedRows; }
            set { _selectedRows = value; }
        }

        private void dtpInstalledAfter_ValueChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CheckDate();
            Display(SelectedRows);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CheckDate();
            Display(SelectedRows);
        }

        internal void Display(DataGridViewSelectedRowCollection rowsToDisplay)
        {
            Logger.EnteringMethod();
            if (rowsToDisplay != null)
            {
                this.Cursor = Cursors.WaitCursor;
                SelectedRows = rowsToDisplay;
                if (tabCtrlComputerDetail.SelectedTab == tabCtrlComputerDetail.TabPages["tabLastInstalled"])
                {
                    btnRefresh.Enabled = false;
                    txtBxDetail.Text = "";

                    System.Threading.ThreadStart tStart = new System.Threading.ThreadStart(DisplayInstalledUpdates);
                    System.Threading.Thread t = new System.Threading.Thread(tStart);
                    t.Priority = System.Threading.ThreadPriority.BelowNormal;
                    t.Start();
                }
                else if (tabCtrlComputerDetail.SelectedTab == tabCtrlComputerDetail.TabPages["tabReport"])
                {
                    DisplayReport();
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void DisplayInstalledUpdates()
        {
            Logger.EnteringMethod();
            if (SelectedRows != null && SelectedRows.Count != 0)
            {
                List<Guid> installedUpdate = new List<Guid>();
                StringBuilder builder = new StringBuilder();

                DataGridViewRow[] selectedRows = new DataGridViewRow[SelectedRows.Count];
                SelectedRows.CopyTo(selectedRows, 0);

                for (int i = 0; i < selectedRows.Length; i++)
                {
                    try
                    {
                        if (!_aborting)
                        {
                            UpdateInstallationInfoCollection updateInfo = _wsus.GetUpdateInstallationInfo(selectedRows[i].Cells[0].Value.ToString(), InstalledAfter, InstalledBefore);

                            foreach (IUpdateInstallationInfo update in updateInfo)
                            {
                                if (!installedUpdate.Contains(update.UpdateId))
                                {
                                    installedUpdate.Add(update.UpdateId);
                                    builder.AppendLine(update.GetUpdate().Title);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                    }

                }
                Action action = () =>
                {
                    txtBxDetail.Text = builder.ToString();
                    btnRefresh.Enabled = true;
                };
                if (!this.IsDisposed && !this.Disposing && !_aborting)
                    this.Invoke(action);
            }
        }

        private void DisplayReport()
        {
            Logger.EnteringMethod();
            if (threadDisplayReport.IsAlive)
            {
                cancelDisplayReport = true;
                threadDisplayReport.Join(2000);
            }
            cancelDisplayReport = false;
            threadDisplayReport = null;
            threadDisplayReport = new System.Threading.Thread(new System.Threading.ThreadStart(DisplayReportASynch));
            threadDisplayReport.Priority = System.Threading.ThreadPriority.Lowest;

            dtGvReport.Rows.Clear();
            dtGvReport.Refresh();

            threadDisplayReport.Start();
        }

        private void DisplayReportASynch()
        {
            Dictionary<Guid, Company> companies = ComputerCtrl.Companies;
            UpdateApprovalCollection approvalsForThisGroup;
            UpdateApprovalCollection approvalsForAllComputersGroup;
            IComputerTargetGroup allComputerTargetGroup = _wsus.GetAllComputerTargetGroup();
            Dictionary<string, string> computers = new Dictionary<string, string>();
            ComputerTargetScope targetScope = new ComputerTargetScope();
            ReportResult resultToDisplay = new ReportResult();

            if (!cancelDisplayReport && SelectedRows.Count != 0)
            {
                targetScope.ComputerTargetGroups.Add(_wsus.GetComputerGroup(ComputerCtrl.ComputerGroupID));
                targetScope.IncludeDownstreamComputerTargets = true;

                foreach (DataGridViewRow row in SelectedRows)
                {
                    if (row.Index != -1 && row.Visible)
                    {
                        string tempComputer = _wsus.GetComputerTargetByName((row.Cells["ComputerName"].Value.ToString())).Id;
                        if (tempComputer != null)
                            computers.Add(tempComputer, row.Cells["ComputerName"].Value.ToString());
                    }
                }

                foreach (Company company in companies.Values)
                {
                    if (cancelDisplayReport)
                        break;
                    foreach (Product product in company.Products.Values)
                    {
                        if (cancelDisplayReport)
                            break;
                        foreach (IUpdate update in product.Updates)
                        {
                            if (cancelDisplayReport)
                                break;
                            resultToDisplay.ResetCounters();

                            approvalsForThisGroup = _wsus.GetUpdateApprovalStatus(ComputerCtrl.ComputerGroupID, update);
                            approvalsForAllComputersGroup = _wsus.GetUpdateApprovalStatus(allComputerTargetGroup.Id, update);
                            if ((approvalsForThisGroup.Count != 0 || approvalsForAllComputersGroup.Count !=0) && (chkBxShowSupersededUpdates.Checked || !update.IsSuperseded))
                            {
                                UpdateInstallationInfoCollection installationInfo = update.GetUpdateInstallationInfoPerComputerTarget(targetScope);
                                foreach (IUpdateInstallationInfo info in installationInfo)
                                {
                                    if (computers.ContainsKey(info.ComputerTargetId))
                                    {
                                        switch (info.UpdateInstallationState)
                                        {
                                            case UpdateInstallationState.Downloaded:
                                                resultToDisplay.DownloadedCount++;
                                                break;
                                            case UpdateInstallationState.Failed:
                                                resultToDisplay.FailedCount++;
                                                break;
                                            case UpdateInstallationState.Installed:
                                                resultToDisplay.InstalledCount++;
                                                break;
                                            case UpdateInstallationState.InstalledPendingReboot:
                                                resultToDisplay.InstalledPendingRebootCount++;
                                                break;
                                            case UpdateInstallationState.NotApplicable:
                                                resultToDisplay.NotApplicableCount++;
                                                break;
                                            case UpdateInstallationState.NotInstalled:
                                                resultToDisplay.NotInstalledCount++;
                                                break;
                                            case UpdateInstallationState.Unknown:
                                                resultToDisplay.UnknownCount++;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }

                                object[] args = new object[4];

                                args[0] = update.CompanyTitles[0];
                                args[1] = update.Title;
                                args[2] = ((approvalsForThisGroup.Count != 0) ? resMan.GetString(approvalsForThisGroup[0].Action.ToString()) : resMan.GetString(approvalsForAllComputersGroup[0].Action.ToString())) + ((update.IsSuperseded) ? "(" + resMan.GetString("Superseded") + ")" : string.Empty);
                                args[3] = resultToDisplay;

                                FillRow(args);
                            }
                        }
                    }
                }
                Action action = () =>
                {
                    if (dtGvReport.SortedColumn != null)
                        dtGvReport.Sort(dtGvReport.SortedColumn, (dtGvReport.SortOrder == SortOrder.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending);
                };
                this.Invoke(action);
            }
        }

        private void FillRow(object[] args)
        {
            if (!cancelDisplayReport)
            {
                if (InvokeRequired)
                {
                    this.BeginInvoke(new Action<object[]>(FillRow), new object[] { args });
                    return;
                }

                string company = (string)args[0];
                string title = (string)args[1];
                string approbation = (string)args[2];
                ReportResult resultToDisplay = (ReportResult)args[3];

                int index = dtGvReport.Rows.Add();
                resultToDisplay.Row = dtGvReport.Rows[index];

                resultToDisplay.Row.Cells["Title"].Value = "(" + company + ") " + title;
                resultToDisplay.Row.Cells["ApprovalAction"].Value = approbation;

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
            }
        }

        private void tabCtrlComputerDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            tabCtrlComputerDetail.SelectedTab.Refresh();
            Display(SelectedRows);
        }

        private void chkBxShowSupersededUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DisplayReport();
        }
    }
}
