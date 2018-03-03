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
    public partial class UpdateListViewer : UserControl
    {
        private WsusWrapper _wsus;
        private List<MetaGroup> _metaGroups;
        private Product _product;
        private bool _populateDGV = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateListViewer).Assembly);

        public UpdateListViewer()
        {
            Logger.EnteringMethod("UpdateListViewer");
            InitializeComponent();
            _wsus = WsusWrapper.GetInstance();
            _wsus.UpdateExpired += new WsusWrapper.UpdateExpiredEventHandler(_wsus_UpdateExpired);
            _wsus.UpdateDeclined += new WsusWrapper.UpdateDeclinedEventHandler(_wsus_UpdateDeclined);
            _wsus.UpdateApprovalChange += new WsusWrapper.UpdateApprovalChangeEventHandler(_wsus_UpdateApprovalChange);
        }

        private ToolStripMenuItem GetItem(string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = text;
            item.Name = text;
            if (text == resMan.GetString(Properties.Settings.Default.UpdateDefaultAction))
            {
                Font newFont = new System.Drawing.Font(item.Font.FontFamily, item.Font.Size, FontStyle.Bold);
                item.Font = newFont;
                item.BackColor = Color.AliceBlue;
                item.Image = Properties.Resources.Star;
            }
            return item;
        }

        private bool HasSomeApprove(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            bool result = false;
            foreach (IUpdate update in updates)
            {
                Logger.Write("update : " + update.Title);
                if (update.IsApproved)
                {
                    result = true;
                    break;
                }
            }
            Logger.Write("Returning " + result.ToString());
            return result;
        }

        private bool HasSomeExpired(UpdateCollection updates)
        {
            Logger.EnteringMethod();
            bool result = false;
            foreach (IUpdate update in updates)
            {
                Logger.Write("update : " + update.Title);
                if (update.PublicationState == PublicationState.Expired)
                {
                    result = true;
                    break;
                }
            }
            Logger.Write("Returning " + result.ToString());
            return result;
        }

        private void _wsus_UpdateApprovalChange(IUpdate approvedUpdate)
        {
            Logger.EnteringMethod(approvedUpdate.Title);
            ViewedProduct.RefreshUpdate(approvedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == approvedUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = approvedUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        internal void UpdateDisplay()
        {
            Logger.EnteringMethod();
            if (ViewedProduct != null)
            {
                _populateDGV = true;
                ClearDisplay();
                dgvUpdateList.SuspendLayout();

                foreach (IUpdate update in ViewedProduct.Updates)
                {
                    Logger.Write("Adding " + update.Title);
                    dgvUpdateList.Rows.Add(update.Title, GetUpdateStatus(update), update.ArrivalDate.ToLocalTime(), update.CreationDate.ToLocalTime(), update);
                }
                if (ContentChanged != null)
                    ContentChanged();
                dgvUpdateList.ResumeLayout();
                _populateDGV = false;
                dgvUpdateList_SelectionChanged(null, null);
            }
        }

        private void UpdateInternalData(UpdateCollection updates)
        {
            Product currentProduct = ViewedProduct;

            foreach (IUpdate newUpdate in updates)
            {
                currentProduct.RefreshUpdate(newUpdate);
            }
        }

        private string GetUpdateStatus(IUpdate update)
        {
            Logger.EnteringMethod(update.Title);

            if (update.IsApproved)
                if (update.IsSuperseded)
                    return resMan.GetString("Approved") + " (" + resMan.GetString("Superseded") + ")";
                else
                    return resMan.GetString("Approved");
            if (update.IsDeclined && update.PublicationState == PublicationState.Expired)
                return resMan.GetString("Declined") + " (" + resMan.GetString("Expired") + ")";
            if (update.IsDeclined)
                return resMan.GetString("Declined");
            if (update.IsSuperseded)
                return resMan.GetString("Superseded");
            if (update.PublicationState == PublicationState.Expired)
                return resMan.GetString("Expired");

            return resMan.GetString("NotApproved");
        }

        internal void LockFunctionnalities(bool isLock)
        {
            foreach (ToolStripItem item in mnuStripUpdateListViewer.Items)
            {
                if (!isLock && item.Name == resMan.GetString("QuickApproval"))
                    item.Enabled = (_metaGroups.Count != 0);
                else
                    if (item.Name == resMan.GetString("Delete"))
                        item.Enabled = true;
                    else
                        item.Enabled = !isLock;
            }
        }

        internal void ClearDisplay()
        {
            dgvUpdateList.Rows.Clear();
            if (ContentChanged != null)
                ContentChanged();
        }

        internal void RunningLongOperation(bool running)
        {
            if (running)
                this.Cursor = Cursors.WaitCursor;
            else
                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Get a collection of updates selected in the control.
        /// </summary>
        /// <returns>Collection of IUpdate. May be empty if no update are selected.</returns>
        internal UpdateCollection GetSelectedUpdates()
        {
            Logger.EnteringMethod();
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                IUpdate update = (IUpdate)row.Cells["UpdateId"].Value;
                Logger.Write("Adding " + update.Title);
                updates.Add(update);
            }

            return updates;
        }

        /// <summary>
        /// Get a collection of updates displayed by the control.
        /// </summary>
        /// <returns>Collection of IUpdate. May be empty if no update are displayed.</returns>
        internal UpdateCollection GetDisplayedUpdates()
        {
            Logger.EnteringMethod();
            UpdateCollection updates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.Rows)
            {
                IUpdate update = (IUpdate)row.Cells["UpdateId"].Value;
                Logger.Write("Adding " + update.Title);
                updates.Add(update);
            }

            return updates;
        }

        internal Product ViewedProduct
        {
            get { return _product; }
            set
            {
                Logger.EnteringMethod(value.ProductName);
                _product = value;
                ClearDisplay();
                UpdateDisplay();
            }
        }

        internal int DataGridViewHeight
        {
            get
            {
                int height = 0;

                height += dgvUpdateList.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvUpdateList.Rows)
                {
                    height += row.Height;
                }

                return height;
            }
        }

        internal void SetMetaGroups(List<MetaGroup> metaGroups)
        {
            _metaGroups = metaGroups;

            mnuStripUpdateListViewer.Items.Clear();
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Approve")));
            ToolStripMenuItem quickApprovalItem = GetItem(resMan.GetString("QuickApproval"));
            foreach (MetaGroup metaGroup in metaGroups)
            {
                ToolStripMenuItem metaGroupItem = GetItem(metaGroup.Name);
                metaGroupItem.Click += new EventHandler(metaGroupItem_Click);
                quickApprovalItem.DropDownItems.Add(metaGroupItem);
            }
            quickApprovalItem.Enabled = (metaGroups.Count != 0);
            mnuStripUpdateListViewer.Items.Add(quickApprovalItem);
            mnuStripUpdateListViewer.Items.Add(new ToolStripSeparator());
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Revise")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Decline")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Expire")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Delete")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("Resign")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("CreateSupersedingUpdate")));
            mnuStripUpdateListViewer.Items.Add(new ToolStripSeparator());
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("ExportThisUpdate")));
            mnuStripUpdateListViewer.Items.Add(new ToolStripSeparator());
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("ShowInWsusConsole")));
            mnuStripUpdateListViewer.Items.Add(GetItem(resMan.GetString("HideInWsusConsole")));

            LockFunctionnalities(_wsus.IsReplica);
        }

        internal void UpdateDeleted(IUpdate deletedUpdate)
        {
            Logger.EnteringMethod(deletedUpdate.Title);
            ViewedProduct.RemoveUpdate(deletedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value) == deletedUpdate)
                {
                    dgvUpdateList.Rows.Remove(row);
                    if (ContentChanged != null)
                        ContentChanged();
                    break;
                }
            }
        }

        #region (Responses to Events - Réponses aux événements)

        private void _wsus_UpdateExpired(IUpdate expiredUpdate)
        {
            Logger.EnteringMethod(expiredUpdate.Title);
            ViewedProduct.RefreshUpdate(expiredUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == expiredUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = expiredUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        private void _wsus_UpdateDeclined(IUpdate declinedUpdate)
        {
            Logger.EnteringMethod(declinedUpdate.Title);
            ViewedProduct.RefreshUpdate(declinedUpdate);
            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                if (((IUpdate)row.Cells["UpdateId"].Value).Id.UpdateId == declinedUpdate.Id.UpdateId)
                {
                    row.Cells["UpdateId"].Value = declinedUpdate;
                    dgvUpdateList_SelectionChanged(null, null);
                    break;
                }
            }
        }

        private void dgvUpdateList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
            {
                AdjustSelection(e.RowIndex);
                bool visibility = (_wsus.CurrentServer.VisibleInWsusConsole != FrmSettings.MakeVisibleInWsusPolicy.Never && _wsus.IsLocal);
                mnuStripUpdateListViewer.Items[resMan.GetString("ShowInWsusConsole")].Enabled = visibility;
                mnuStripUpdateListViewer.Items[resMan.GetString("HideInWsusConsole")].Enabled = visibility;
                mnuStripUpdateListViewer.Items[resMan.GetString("Revise")].Enabled = _wsus.IsConsoleVersionAllowPublication() && !_wsus.IsReplica;
                mnuStripUpdateListViewer.Items[resMan.GetString("CreateSupersedingUpdate")].Enabled = _wsus.IsConsoleVersionAllowPublication() && !_wsus.IsReplica;
                mnuStripUpdateListViewer.Show(dgvUpdateList, dgvUpdateList.PointToClient(Cursor.Position));
            }
        }

        private void dgvUpdateList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Logger.EnteringMethod();

            if (e.Button == System.Windows.Forms.MouseButtons.Left && e.RowIndex != -1)
            {
                string command = resMan.GetString(Properties.Settings.Default.UpdateDefaultAction);
                ToolStripItemClickedEventArgs clickedItem = new ToolStripItemClickedEventArgs(new ToolStripMenuItem());
                clickedItem.ClickedItem.Text = command;
                if (command != resMan.GetString("Revise") || _wsus.IsConsoleVersionAllowPublication())
                    mnuStripUpdateListViewer_ItemClicked(null, clickedItem);
            }
        }

        private void AdjustSelection(int ClickedIndex)
        {
            DataGridViewRow clickedRow = dgvUpdateList.Rows[ClickedIndex];
            if (!dgvUpdateList.SelectedRows.Contains(clickedRow))
            {
                dgvUpdateList.ClearSelection();
                dgvUpdateList.Rows[ClickedIndex].Selected = true;
            }
        }

        private void dgvUpdateList_SelectionChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            UpdateCollection selectedUpdates = new UpdateCollection();

            foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
            {
                selectedUpdates.Add((IUpdate)row.Cells["UpdateId"].Value);
            }

            mnuStripUpdateListViewer.Items[resMan.GetString("Revise")].Enabled = ((dgvUpdateList.SelectedRows.Count == 1) && (!_wsus.IsReplica));
            mnuStripUpdateListViewer.Items[resMan.GetString("Delete")].Enabled = !HasSomeApprove(selectedUpdates);
            mnuStripUpdateListViewer.Items[resMan.GetString("Expire")].Enabled = !HasSomeExpired(selectedUpdates);
            mnuStripUpdateListViewer.Items[resMan.GetString("ExportThisUpdate")].Enabled = ((dgvUpdateList.SelectedRows.Count == 1) && (!_wsus.IsReplica));
            mnuStripUpdateListViewer.Items[resMan.GetString("CreateSupersedingUpdate")].Enabled = ((dgvUpdateList.SelectedRows.Count == 1) && (!_wsus.IsReplica));

            dgvUpdateList.Refresh();
            if (UpdateSelectionChanged != null && !_populateDGV)
                UpdateSelectionChanged(dgvUpdateList.SelectedRows);
        }

        private void mnuStripUpdateListViewer_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string command = e.ClickedItem.Text;
            Logger.EnteringMethod(command);
            mnuStripUpdateListViewer.Hide();
            this.Cursor = Cursors.WaitCursor;

            if (dgvUpdateList.SelectedRows != null && dgvUpdateList.SelectedRows.Count != 0)
            {
                UpdateCollection updates = new UpdateCollection();

                foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
                {
                    updates.Add((IUpdate)row.Cells["UpdateId"].Value);
                }

                if (!_wsus.IsReplica)
                {
                    if (command == resMan.GetString("Approve"))
                    {
                        if (ApproveUpdate != null)
                            ApproveUpdate(updates);
                    }

                    if (command == resMan.GetString("Revise"))
                    {
                        CertificateHelper.CertificateStatus certStatus = _wsus.GetCertificateStatus;
                        if (certStatus == CertificateHelper.CertificateStatus.Valid || certStatus == CertificateHelper.CertificateStatus.NearExpiration)
                        {
                            if (ReviseUpdate != null)
                                ReviseUpdate(updates[0]);
                        }
                        else
                            MessageBox.Show(resMan.GetString("SolveCertificateProblemBeforePublishing"));
                    }

                    if (command == resMan.GetString("Decline"))
                    {
                        if (DeclineUpdate != null)
                            DeclineUpdate(updates);
                    }

                    if (command == resMan.GetString("Expire"))
                    {
                        if (ExpireUpdate != null)
                            ExpireUpdate(updates);
                    }

                    if (command == resMan.GetString("Resign"))
                    {
                        if (ResignUpdate != null)
                        {
                            ResignUpdate(updates);
                            UpdateInternalData(updates);
                        }
                    }
                    if (command == resMan.GetString("CreateSupersedingUpdate"))
                    {
                        if (CreateSupersedingUpdate != null && updates.Count != 0)
                            CreateSupersedingUpdate(updates[0]);
                    }
                    if (command == resMan.GetString("ExportThisUpdate"))
                    {
                        if (ExportUpdate != null)
                            ExportUpdate();
                    }
                }

                if (command == resMan.GetString("ShowInWsusConsole"))
                {
                    ChangeVisibiltyInWsusConsole(updates, 0);
                }
                if (command == resMan.GetString("HideInWsusConsole"))
                {
                    ChangeVisibiltyInWsusConsole(updates, 1);
                }

                if (command == resMan.GetString("Delete"))
                {
                    if (DeleteUpdate != null)
                        DeleteUpdate(updates);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void metaGroupItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            string metaGroupName = sender.ToString();
            MetaGroup selectedMetaGroup = null;
            mnuStripUpdateListViewer.Hide();
            this.Cursor = Cursors.WaitCursor;

            foreach (MetaGroup metaGroup in _metaGroups)
            {
                if (metaGroup.Name == metaGroupName)
                {
                    selectedMetaGroup = metaGroup;
                    break;
                }
            }
            if (dgvUpdateList.SelectedRows.Count != 0)
            {
                UpdateCollection updates = new UpdateCollection();

                foreach (DataGridViewRow row in dgvUpdateList.SelectedRows)
                {
                    updates.Add((IUpdate)row.Cells["UpdateId"].Value);
                }
                if (QuicklyApproveUpdate != null)
                    QuicklyApproveUpdate(updates, selectedMetaGroup);
            }
        }

        private void ChangeVisibiltyInWsusConsole(UpdateCollection updates, int status)
        {
            Logger.EnteringMethod();
            SqlHelper sqlHelper = SqlHelper.GetInstance();
            List<Guid> updateIDs = new List<Guid>();
            string sqlServerName = _wsus.GetSqlServerName();
            string sqlDataBaseName = _wsus.GetSqlDataBaseName();
            System.Version wsusVersion = _wsus.GetServerVersion();

            if (sqlServerName.Contains("MICROSOFT##SSEE") || sqlServerName.Contains("MICROSOFT##WID"))
            {
                if (wsusVersion.Major == 3)
                    sqlHelper.ServerName = @"\\.\pipe\MSSQL$MICROSOFT##SSEE\sql\query";
                if (wsusVersion.Major == 6)
                    sqlHelper.ServerName = @"\\.\pipe\Microsoft##WID\tsql\query";
                sqlHelper.DataBaseName = "SUSDB";
            }
            else
            {
                sqlHelper.ServerName = _wsus.GetSqlServerName();
                sqlHelper.DataBaseName = _wsus.GetSqlDataBaseName();
            }

            if (sqlHelper.Connect(string.Empty, string.Empty))
            {
                foreach (IUpdate update in updates)
                {
                    updateIDs.Add(update.Id.UpdateId);
                    if (status == 0)
                        sqlHelper.ShowUpdatesInConsole(updateIDs);
                    if (status == 1)
                        sqlHelper.HideUpdatesInConsole(updateIDs);
                }

                updateIDs.Clear();
                foreach (IUpdate update in updates)
                {
                    UpdateCategoryCollection categories = update.GetUpdateCategories();
                    foreach (IUpdateCategory category in categories)
                    {
                        if (!updateIDs.Contains(category.Id) && (status == 0 || NumberOfVisibleUpdate(category.GetUpdates()) == 0))
                        {
                            updateIDs.Add(category.Id);
                            if (category.ProhibitsSubcategories && !category.ProhibitsUpdates)
                            {
                                IUpdateCategory parentCategory = category.GetParentUpdateCategory();
                                if (!updateIDs.Contains(parentCategory.Id) && (status == 0 || NumberOfVisibleCategory(parentCategory.GetSubcategories()) == 1))
                                    updateIDs.Add(parentCategory.Id);
                            }
                        }
                    }
                }

                if (status == 0)
                    sqlHelper.ShowUpdatesInConsole(updateIDs);
                if (status == 1)
                    sqlHelper.HideUpdatesInConsole(updateIDs);
                sqlHelper.Disconnect();
            }
        }

        private int NumberOfVisibleCategory(UpdateCategoryCollection updateCategoryCollection)
        {
            Logger.EnteringMethod();
            int number = 0;
            foreach (IUpdateCategory category in updateCategoryCollection)
            {
                if (category.UpdateSource == UpdateSource.MicrosoftUpdate)
                    number++;
            }
            Logger.Write("Returning " + number.ToString());
            return number;
        }

        private int NumberOfVisibleUpdate(UpdateCollection updateCollection)
        {
            Logger.EnteringMethod();
            int number = 0;
            foreach (IUpdate update in updateCollection)
            {
                if (update.UpdateSource == UpdateSource.MicrosoftUpdate)
                    number++;
            }
            Logger.Write("Returning " + number.ToString());
            return number;
        }

        #endregion

        public delegate void UpdateSelectionChangedEventHandler(DataGridViewSelectedRowCollection rowCollection);
        public event UpdateSelectionChangedEventHandler UpdateSelectionChanged;

        public delegate void ContentChangedEventHandler();
        public event ContentChangedEventHandler ContentChanged;

        public delegate void ApproveUpdateEventHandler(UpdateCollection udpatesToApprove);
        public event ApproveUpdateEventHandler ApproveUpdate;

        internal delegate void QuicklyApproveUpdateEventHandler(UpdateCollection udpatesToApprove, MetaGroup metaGroup);
        internal event QuicklyApproveUpdateEventHandler QuicklyApproveUpdate;

        public delegate void DeclineUpdateEventHandler(UpdateCollection udpatesToDecline);
        public event DeclineUpdateEventHandler DeclineUpdate;

        public delegate void ExpireUpdateEventHandler(UpdateCollection updatesToExpire);
        public event ExpireUpdateEventHandler ExpireUpdate;

        public delegate void DeleteUpdateEventHandler(UpdateCollection udpatesToDelete);
        public event DeleteUpdateEventHandler DeleteUpdate;

        public delegate void ReviseUpdateEventHandler(IUpdate updateToRevise);
        public event ReviseUpdateEventHandler ReviseUpdate;

        public delegate void ResignUpdateEventHandler(UpdateCollection updateToResign);
        public event ResignUpdateEventHandler ResignUpdate;

        public delegate void CreateSupersedingUpdateEventHandler(IUpdate updateToSupersed);
        public event CreateSupersedingUpdateEventHandler CreateSupersedingUpdate;

        public delegate void ExportUpdateEventHandler();
        public event ExportUpdateEventHandler ExportUpdate;

    }
}
