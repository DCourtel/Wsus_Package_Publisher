using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WsusADComparator
{
    public partial class FrmWsusADComparator : Form
    {
        private Model _model;
        private FrmOUSelector _frmOUSelector = null;
        private Localization _localization = Localization.GetInstance();
        private SaveFileDialog saveDlg = new SaveFileDialog();
        private WPP.Security.Credential _credential = new WPP.Security.Credential();
        private enum ContextMenuTag
        {
            ShowByDefault = 1,
            CanBeHide = 2
        }

        public FrmWsusADComparator(string[] args)
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
            InitializeComponent();

            this.adgvComputer.AutoGenerateColumns = true;
            DataTable _computersTable = this.dataSet1.Tables.Add("Computers");
            this.bindingSource1.DataMember = _computersTable.TableName;
            this._model = new Model(_computersTable);

            this.SetOptionsFromArguments(args);
            string domainName = this._model.GetDomaineName();

            this.txtBxDomainName.Text = domainName;
            this._frmOUSelector = new FrmOUSelector(domainName);

            this.saveDlg.DefaultExt = ".csv";
            this.saveDlg.FileName = "Export.csv";
            this.saveDlg.Filter = this._localization.GetLocalizedString("ExportFilter");
            this.saveDlg.FilterIndex = 2;

            this._model.SearchComputerBegin += _model_SearchComputerBegin;
            this._model.SearchComputerProgress += _model_SearchComputerProgress;
            this._model.SearchComputerFinished += _model_SearchComputerFinished;
        }

        #region (methods)

        private void SetOptionsFromArguments(string[] args)
        {
            try
            {
                WPP.Tools.CommandLine commandLine = new WPP.Tools.CommandLine(args);
                if (commandLine.OptionExists("Server"))
                {
                    this.txtBxWsusServer.Text = commandLine.GetOptionValue<string>("Server", String.Empty);
                    this.txtBxWsusServer.BackColor = SystemColors.Control;
                }
                else
                {
                    this.txtBxWsusServer.Text = String.Empty;
                    this.txtBxWsusServer.BackColor = System.Drawing.Color.Bisque;
                }
                if (commandLine.OptionExists("Port"))
                {
                    this.nupServerPort.Value = commandLine.GetOptionValue<Decimal>("Port", 8530);
                    this.nupServerPort.BackColor = SystemColors.Control;
                }
                else
                {
                    this.nupServerPort.Value = (Decimal)8530;
                    this.nupServerPort.BackColor = System.Drawing.Color.Bisque;
                }

                if(commandLine.OptionExists("SSL"))
                {
                    this.chkBxUseSSL.Checked = commandLine.GetOptionValue<Boolean>("SSL", false);
                    this.chkBxUseSSL.BackColor = SystemColors.Control;
                }
                else
                {
                    this.chkBxUseSSL.Checked = false;
                    this.chkBxUseSSL.BackColor = System.Drawing.Color.Bisque;
                }

                if (commandLine.OptionExists("Login"))
                {
                    this.txtBxCredentials.Text = commandLine.GetOptionValue<string>("Login", String.Empty);
                    this.txtBxCredentials.BackColor = SystemColors.Control;
                }
                else
                {
                    this.txtBxCredentials.Text = String.Empty;
                    this.txtBxCredentials.BackColor = System.Drawing.Color.Bisque;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this._localization.GetLocalizedString("AnErrorOccursWhileParsingTheCommandLine") + "\r\n" + ex.Message);
            }
        }

        private void SetColumnsWidth()
        {
            foreach (System.Reflection.PropertyInfo propertyInfo in WPP.Management.WppComputer.GetDataGridViewProperties())
            {
                DataGridViewColumn column = this.adgvComputer.Columns[this._localization.GetLocalizedString(propertyInfo.Name)];
                WPP.Management.DataGridViewDataAttribute attribute = (WPP.Management.DataGridViewDataAttribute)propertyInfo.GetCustomAttributes(false)[0];
                column.Visible = attribute.Visible && attribute.DisplayedByDefault;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.FillWeight = attribute.Width;
                column.DataPropertyName = column.Name;
                column.Tag = attribute;
            }
        }

        private void InitializeContextMenuForColumnsHeader()
        {
            ToolStripMenuItem itemAllColumns = new ToolStripMenuItem(this._localization.GetLocalizedString("ShowAllColumns"));
            itemAllColumns.Tag = null;
            this.ctxMnuColumnHeader.Items.Add(itemAllColumns);
            ToolStripMenuItem itemDefaultColumns = new ToolStripMenuItem(this._localization.GetLocalizedString("ShowDefaultColumns"));
            itemDefaultColumns.Tag = null;
            this.ctxMnuColumnHeader.Items.Add(itemDefaultColumns);
            ToolStripSeparator itemSeparator = new ToolStripSeparator();
            itemSeparator.Tag = null;
            this.ctxMnuColumnHeader.Items.Add(itemSeparator);

            foreach (DataGridViewColumn column in this.adgvComputer.Columns)
            {
                WPP.Management.DataGridViewDataAttribute attribute = (WPP.Management.DataGridViewDataAttribute)column.Tag;
                if (attribute.Visible)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(column.HeaderText);
                    item.Checked = attribute.DisplayedByDefault;
                    item.Enabled = attribute.CanBeHide;
                    item.Tag = attribute.CanBeHide ? (ContextMenuTag.CanBeHide | (attribute.DisplayedByDefault ? ContextMenuTag.ShowByDefault : 0)) : (attribute.DisplayedByDefault ? ContextMenuTag.ShowByDefault : 0);
                    this.ctxMnuColumnHeader.Items.Add(item);
                }
            }
        }

        private void InitializeContextMenuForComputers()
        {
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("GetWsusClientID"), new EventHandler(GetWsusClientID_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("ResetWsusClientID"), new EventHandler(ResetWsusClientID_OnClick), true));
            ctxMnuComputers.Items.Add(new ToolStripSeparator());
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("StartService"), new EventHandler(StartService_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("StopService"), new EventHandler(StopService_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("RestartService"), new EventHandler(RestartService_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("QueryServiceStatus"), new EventHandler(QueryServiceStatus_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("ShowWindowsUpdateLog"), new EventHandler(ShowWindowsUpdateLog_OnClick), false));
            ctxMnuComputers.Items.Add(new ToolStripSeparator());
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("SendDetectNow"), new EventHandler(SendDetectNow_OnClick), true));
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("SendReportNow"), new EventHandler(SendReportNow_OnClick), true));
            ctxMnuComputers.Items.Add(new ToolStripSeparator());
            ctxMnuComputers.Items.Add(GetToolStripItem(_localization.GetLocalizedString("SendRebootNow"), new EventHandler(SendRebootNow_OnClick), true));
        }

        private ToolStripMenuItem GetToolStripItem(string text, EventHandler onClick, bool allowActionOnMultipleSelection)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text, null, onClick);
            item.Tag = allowActionOnMultipleSelection;
            return item;
        }

        private void ShowAllColumns()
        {
            try
            {
                foreach (ToolStripItem menuItem in ctxMnuColumnHeader.Items)
                {
                    if (menuItem.GetType() == typeof(ToolStripMenuItem) && menuItem.Tag != null)
                        (menuItem as ToolStripMenuItem).Checked = true;
                }
                this.ShowSelectedColumns();
            }
            catch (Exception) { }
        }

        private void ShowDefaultColumns()
        {
            try
            {
                foreach (ToolStripItem menuItem in this.ctxMnuColumnHeader.Items)
                {
                    if (menuItem.Tag != null)
                    {
                        (menuItem as ToolStripMenuItem).Checked = ((ContextMenuTag)menuItem.Tag & ContextMenuTag.ShowByDefault) == ContextMenuTag.ShowByDefault;
                    }
                }
                this.ShowSelectedColumns();
            }
            catch (Exception) { }
        }

        private void ShowSelectedColumns()
        {
            try
            {
                foreach (ToolStripItem menuItem in ctxMnuColumnHeader.Items)
                {
                    if (menuItem.GetType() == typeof(ToolStripMenuItem) && menuItem.Tag != null)
                        adgvComputer.Columns[(menuItem as ToolStripMenuItem).Text].Visible = (menuItem as ToolStripMenuItem).Checked;
                }
            }
            catch (Exception) { }
        }

        private void ShowComputerCount()
        {
            try
            {
                this.lblProgress.Text = String.Format(String.Format(this._localization.GetLocalizedString("ShowComputerCount"), this._model.ADComputerFound, this.adgvComputer.RowCount));
            }
            catch (Exception) { }
        }

        #endregion (methods)

        #region (Response to Events)

        // Buttons

        private void btnEditWsus_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEditWsusConnectionSettings wsusEditor = new FrmEditWsusConnectionSettings(this.txtBxWsusServer.Text, (int)this.nupServerPort.Value, this.chkBxUseSSL.Checked);
                if (wsusEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtBxWsusServer.Text = wsusEditor.ServerName;
                    this.txtBxWsusServer.BackColor = SystemColors.Control;
                    this.nupServerPort.Value = (decimal)wsusEditor.ServerPort;
                    this.nupServerPort.BackColor = SystemColors.Control;
                    this.chkBxUseSSL.Checked = wsusEditor.UseSSL;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this._localization.GetLocalizedString("AnErrorOccursWhileEditingWsusSettings") + "\r\n" + ex.Message);
            }
        }

        private void btnEditCredentials_Click(object sender, EventArgs e)
        {
            try
            {
                FrmEditCredentialsSettings credentialEditor = new FrmEditCredentialsSettings(this.txtBxCredentials.Text);
                if (credentialEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.txtBxCredentials.Text = credentialEditor.Credential.Username;
                    this.txtBxCredentials.BackColor = SystemColors.Control;
                    this._credential = credentialEditor.Credential;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this._localization.GetLocalizedString("AnErrorOccursWhileEditingCredentials") + "\r\n" + ex.Message);
            }
        }

        private void btnSelectOU_Click(object sender, EventArgs e)
        {
            this._frmOUSelector.ShowDialog();
            this.btnSearchComputers.Enabled = true;
        }

        private void btnSearchComputers_Click(object sender, EventArgs e)
        {
            this.lblProgress.Text = this._localization.GetLocalizedString("SearchingInAD");
            this.lblProgress.Refresh();
            this._model.ClearDataTable();
            this.adgvComputer.Refresh();
            this.btnSearchComputers.Enabled = false;
            this.btnSelectOU.Enabled = false;
            this.btnClose.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (this._frmOUSelector.SearchInAllAD)
                this._model.SearchComputers(this.txtBxDomainName.Text);
            else
                this._model.SearchComputers(this._frmOUSelector.SelectedOUList);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveDlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                btnExport.Enabled = false;
                try
                {
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(saveDlg.OpenFile(), Encoding.UTF8);

                    writer.WriteLine(this._model.GetDataForExport());
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this._localization.GetLocalizedString("ErrorOccursWhileExporting") + ex.Message);
                }
                btnExport.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // AdvancedDatagridView

        private void adgvComputer_FilterStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Filter = this.adgvComputer.FilterString;
            this.ShowComputerCount();
        }

        private void adgvComputer_SortStringChanged(object sender, EventArgs e)
        {
            this.bindingSource1.Sort = this.adgvComputer.SortString;
        }

        private void adgvComputer_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex == -1)
                    ctxMnuColumnHeader.Show(adgvComputer, adgvComputer.PointToClient(Cursor.Position));
            }
            catch (Exception) { }
        }

        private void adgvComputer_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
                {
                    bool ctrlKeyPress = Control.ModifierKeys == Keys.Control;
                    if (ctrlKeyPress)
                    {
                        this.adgvComputer.Rows[e.RowIndex].Selected = true;
                    }
                    else
                    {
                        if (!this.adgvComputer.Rows[e.RowIndex].Selected)
                        {
                            this.adgvComputer.ClearSelection();
                            this.adgvComputer.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    foreach (ToolStripItem item in this.ctxMnuComputers.Items)
                    {
                        if (item.GetType() == typeof(ToolStripMenuItem) && (bool)item.Tag == false)
                        {
                            item.Enabled = this.adgvComputer.SelectedRows.Count == 1;
                        }
                    }
                    ctxMnuComputers.Show(adgvComputer, adgvComputer.PointToClient(Cursor.Position));
                }
            }
            catch (Exception) { }
        }

        // Context Menu For Columns Header

        private void ctxMnuColumnHeader_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Text.Equals(this._localization.GetLocalizedString("ShowAllColumns")))
                {
                    this.ShowAllColumns();
                }
                else if (e.ClickedItem.Text.Equals(this._localization.GetLocalizedString("ShowDefaultColumns")))
                {
                    this.ShowDefaultColumns();
                }
                else
                {
                    (e.ClickedItem as ToolStripMenuItem).Checked = !(e.ClickedItem as ToolStripMenuItem).Checked;
                    this.ShowSelectedColumns();
                }
            }
            catch (Exception) { }
        }

        // Context Menu For Computers

        private void GetWsusClientID_OnClick(object sender, EventArgs e)
        {
            try
            {
                List<int> indexes = new List<int>();
                foreach (DataGridViewRow row in this.adgvComputer.SelectedRows)
                {
                    indexes.Add((row.Cells[this._localization.GetLocalizedString("ComputerObj")].Value as WPP.Management.WppComputer).DatatableIndex);
                }
                this._model.GetWsusClientID(indexes, this._credential);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this._localization.GetLocalizedString("ErrorOccursWhileRetrievingWsusClientID") + "\r\n" + ex.Message);
            }
        }

        private void ResetWsusClientID_OnClick(object sender, EventArgs e)
        {

        }

        private void StartService_OnClick(object sender, EventArgs e)
        {

        }

        private void StopService_OnClick(object sender, EventArgs e)
        {

        }

        private void RestartService_OnClick(object sender, EventArgs e)
        {

        }

        private void QueryServiceStatus_OnClick(object sender, EventArgs e)
        {

        }

        private void ShowWindowsUpdateLog_OnClick(object sender, EventArgs e)
        {

        }

        private void SendDetectNow_OnClick(object sender, EventArgs e)
        {

        }

        private void SendReportNow_OnClick(object sender, EventArgs e)
        {

        }

        private void SendRebootNow_OnClick(object sender, EventArgs e)
        {

        }

        // Search Computers

        private void _model_SearchComputerBegin(int total)
        {
            Action beginAction = () =>
            {
                this.progBar.Minimum = 0;
                this.progBar.Maximum = total;
                this.progBar.Value = 0;
            };
            try
            {
                this.Invoke(beginAction);
            }
            catch (Exception) { }
        }

        private void _model_SearchComputerProgress(int progress, string message)
        {
            Action progressAction = () =>
            {
                this.progBar.Value = System.Math.Min(progress, this.progBar.Maximum);
                this.lblProgress.Text = message;
            };
            try
            {
                this.Invoke(progressAction);
            }
            catch (Exception) { }
        }

        private void _model_SearchComputerFinished()
        {
            Action finishAction = () =>
            {
                this.progBar.Value = this.progBar.Maximum;
                this.lblProgress.Text = String.Empty;
                this.btnSearchComputers.Enabled = true;
                this.btnSelectOU.Enabled = true;
                this.btnClose.Enabled = true;
                this.btnGetWsusClientID.Enabled = true;
                this.btnExport.Enabled = true;
                this.Cursor = Cursors.Default;
                this.ShowComputerCount();
            };
            try
            {
                this.Invoke(finishAction);
            }
            catch (Exception) { }
        }

        // Shown

        private void FrmWsusADComparator_Shown(object sender, EventArgs e)
        {
            this.SetColumnsWidth();
            this.InitializeContextMenuForColumnsHeader();
            this.InitializeContextMenuForComputers();
        }

        #endregion (Response to Events)
    }
}