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
    internal partial class UpdateControl : UserControl
    {
        WsusWrapper _wsus = WsusWrapper.GetInstance();
        Product _product;
        private UpdateDetailViewer updateDetailViewer1;
        ComputerGroup _computerGroups;
        List<MetaGroup> _metaGroups;
        TreeNode _allComputersNode;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(UpdateListViewer).Assembly);

        internal UpdateControl()
        {
            Logger.EnteringMethod("UpdateControl");
            InitializeComponent();
            updateDetailViewer1 = new UpdateDetailViewer();
            this.splitContainer1.Panel2.Controls.Add(this.updateDetailViewer1);
            updateDetailViewer1.Dock = DockStyle.Fill;

            updateListViewer1.ContentChanged += new UpdateListViewer.ContentChangedEventHandler(AdjustSplitterDistance);
            updateListViewer1.UpdateSelectionChanged += new UpdateListViewer.UpdateSelectionChangedEventHandler(updateListViewer1_UpdateSelectionChanged);
            updateListViewer1.ApproveUpdate += new UpdateListViewer.ApproveUpdateEventHandler(updateDetailViewer1_ApproveUpdate);
            updateListViewer1.QuicklyApproveUpdate += new UpdateListViewer.QuicklyApproveUpdateEventHandler(updateListViewer1_QuicklyApproveUpdate);
            updateListViewer1.DeclineUpdate += new UpdateListViewer.DeclineUpdateEventHandler(updateDetailViewer1_DeclineUpdate);
            updateListViewer1.DeleteUpdate += new UpdateListViewer.DeleteUpdateEventHandler(updateDetailViewer1_DeleteUpdate);
            updateListViewer1.ExpireUpdate += new UpdateListViewer.ExpireUpdateEventHandler(updateDetailViewer1_ExpireUpdate);
            updateListViewer1.ReviseUpdate += new UpdateListViewer.ReviseUpdateEventHandler(updateDetailViewer1_ReviseUpdate);
            updateListViewer1.ResignUpdate += new UpdateListViewer.ResignUpdateEventHandler(updateListViewer1_ResignUpdate);
            updateListViewer1.CreateSupersedingUpdate += new UpdateListViewer.CreateSupersedingUpdateEventHandler(updateListViewer1_CreateSupersedingUpdate);
            updateListViewer1.ExportUpdate += new UpdateListViewer.ExportUpdateEventHandler(updateListViewer1_ExportUpdate);

            updateDetailViewer1.ApproveUpdate += new UpdateDetailViewer.ApproveUpdateEventHandler(updateDetailViewer1_ApproveUpdate);
            updateDetailViewer1.DeclineUpdate += new UpdateDetailViewer.DeclineUpdateEventHandler(updateDetailViewer1_DeclineUpdate);
            updateDetailViewer1.DeleteUpdate += new UpdateDetailViewer.DeleteUpdateEventHandler(updateDetailViewer1_DeleteUpdate);
            updateDetailViewer1.ExpireUpdate += new UpdateDetailViewer.ExpireUpdateEventHandler(updateDetailViewer1_ExpireUpdate);
            updateDetailViewer1.ReviseUpdate += new UpdateDetailViewer.ReviseUpdateEventHandler(updateDetailViewer1_ReviseUpdate);
        }

        internal new void Dispose()
        {
            updateDetailViewer1.Dispose();
            updateListViewer1.Dispose();
        }

        #region (Properties - Propriétés)

        /// <summary>
        /// Get or Set the product for which updates are display.
        /// </summary>
        internal Product Product
        {
            get { return _product; }
            set
            {
                Logger.EnteringMethod(value.ProductName);
                _product = value;
                updateListViewer1.ViewedProduct = value;
            }
        }

        public Dictionary<Guid, Company> Companies { get; set; }

        #endregion

        #region (Methods - Méthodes)

        internal void RefreshDisplay()
        {
            Logger.EnteringMethod();
            updateListViewer1.UpdateDisplay();
        }

        internal void SetComputerGroups(ComputerGroup computerGroups, TreeNode allComputersNode)
        {
            Logger.EnteringMethod();
            _computerGroups = computerGroups;
            _allComputersNode = allComputersNode;
            updateDetailViewer1.SetComputerGroups(computerGroups, allComputersNode);
        }

        internal void SetMetaGroups(List<MetaGroup> metaGroups)
        {
            _metaGroups = metaGroups;
            updateListViewer1.SetMetaGroups(metaGroups);
        }

        internal void LockFunctionnalities(bool isLock)
        {
            updateDetailViewer1.LockFunctionnalities(isLock);
            updateListViewer1.LockFunctionnalities(isLock);
        }

        internal void ClearDisplay()
        {
            Logger.EnteringMethod();
            updateDetailViewer1.ResetControl();
            updateListViewer1.ClearDisplay();
        }

        internal void UpdateDeleted(IUpdate deletedUpdate)
        {
            Logger.EnteringMethod(deletedUpdate.Title);
            updateListViewer1.UpdateDeleted(deletedUpdate);
        }

        internal void ExportAnUpdate()
        {
            updateDetailViewer1.ExportAnUpdate();
        }

        private void AdjustSplitterDistance()
        {
            Logger.EnteringMethod();
            int height = updateListViewer1.DataGridViewHeight;

            if (height < (splitContainer1.Height / 2))
                splitContainer1.SplitterDistance = (int)(height + 10);
            else
                splitContainer1.SplitterDistance = (int)(splitContainer1.Height / 2);
        }

        #endregion

        #region (responses to events - réponses aux événements)

        private void updateListViewer1_UpdateSelectionChanged(DataGridViewSelectedRowCollection selectedRows)
        {
            updateDetailViewer1.UpdateSelectionChanged(selectedRows);
        }

        private void updateDetailViewer1_ExpireUpdate(UpdateCollection updatesToExpire)
        {
            this.Cursor = Cursors.WaitCursor;
            updateDetailViewer1.RunningLongOperation(true);
            updateListViewer1.RunningLongOperation(true);
            foreach (IUpdate update in updatesToExpire)
            {
                _wsus.ExpireUpdate(update);
                updateDetailViewer1.Refresh();
                updateListViewer1.Refresh();
                this.Refresh();
                System.Threading.Thread.Sleep(50);
            }
            updateDetailViewer1.RunningLongOperation(false);
            updateListViewer1.RunningLongOperation(false);
            this.Cursor = Cursors.Default;
        }

        private void updateDetailViewer1_DeleteUpdate(UpdateCollection updatesToDelete)
        {
            if (System.Windows.Forms.MessageBox.Show(resMan.GetString("ConfirmeUpdateDeletion"), "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                updateDetailViewer1.RunningLongOperation(true);
                updateListViewer1.RunningLongOperation(true);
                foreach (IUpdate update in updatesToDelete)
                {
                    _wsus.DeleteUpdate(update);
                    updateDetailViewer1.Refresh();
                    updateListViewer1.Refresh();
                    this.Refresh();
                    System.Threading.Thread.Sleep(50);
                }
                updateDetailViewer1.RunningLongOperation(false);
                updateListViewer1.RunningLongOperation(false);
                this.Cursor = Cursors.Default;
            }
        }

        private void updateDetailViewer1_DeclineUpdate(UpdateCollection updatesToDecline)
        {
            this.Cursor = Cursors.WaitCursor;
            updateDetailViewer1.RunningLongOperation(true);
            updateListViewer1.RunningLongOperation(true);
            foreach (IUpdate update in updatesToDecline)
            {
                _wsus.DeclineUpdate(update);
                updateDetailViewer1.Refresh();
                updateListViewer1.Refresh();
                this.Refresh();
                System.Threading.Thread.Sleep(50);
            }
            updateDetailViewer1.RunningLongOperation(false);
            updateListViewer1.RunningLongOperation(false);
            this.Cursor = Cursors.Default;
        }

        private void updateDetailViewer1_ApproveUpdate(UpdateCollection updatesToApprove)
        {
            this.Cursor = Cursors.WaitCursor;
            FrmApprovalSet approvalForm = new FrmApprovalSet(_metaGroups, _computerGroups, updatesToApprove);

            approvalForm.ShowDialog(this);
            if (approvalForm.DialogResult == DialogResult.OK)
            {
                updateDetailViewer1.RunningLongOperation(true);
                updateListViewer1.RunningLongOperation(true);
                foreach (IUpdate updateToApprove in updateDetailViewer1.ViewedUpdates)
                {
                    foreach (ApprovalObject approval in approvalForm.Approvals)
                    {
                        switch (approval.Approval)
                        {
                            case ApprovalObject.Approvals.ApproveForInstallation:
                                if (approval.HasDeadLine && !updateToApprove.InstallationBehavior.CanRequestUserInput)
                                    _wsus.ApproveUpdateForInstallation(approval.GroupId, updateToApprove, approval.DeadLine.ToUniversalTime());
                                else
                                    _wsus.ApproveUpdateForInstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.ApproveForOptionalInstallation:
                                _wsus.ApproveUpdateForOptionalInstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.ApproveForUninstallation:
                                if (approval.HasDeadLine && !updateToApprove.InstallationBehavior.CanRequestUserInput)
                                    _wsus.ApproveUpdateForUninstallation(approval.GroupId, updateToApprove, approval.DeadLine.ToUniversalTime());
                                else
                                    _wsus.ApproveUpdateForUninstallation(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.NotApproved:
                                _wsus.DisapproveUpdate(approval.GroupId, updateToApprove);
                                break;
                            case ApprovalObject.Approvals.Unchanged:
                                break;
                            default:
                                break;
                        }
                        updateDetailViewer1.Refresh();
                        updateListViewer1.Refresh();
                        this.Refresh();
                        System.Threading.Thread.Sleep(50);
                    }
                }
                updateDetailViewer1.RunningLongOperation(false);
                updateListViewer1.RunningLongOperation(false);
            }
            this.Cursor = Cursors.Default;
        }

        private void updateListViewer1_QuicklyApproveUpdate(UpdateCollection udpatesToApprove, MetaGroup metaGroup)
        {
            FrmApprovalSet approvalForm = new FrmApprovalSet(_metaGroups, _computerGroups, udpatesToApprove);
            approvalForm.QuicklyApprove(metaGroup);
            updateDetailViewer1.RunningLongOperation(true);
            updateListViewer1.RunningLongOperation(true);
            foreach (IUpdate updateToApprove in updateDetailViewer1.ViewedUpdates)
            {
                foreach (ApprovalObject approval in approvalForm.Approvals)
                {
                    _wsus.ApproveUpdateForInstallation(approval.GroupId, updateToApprove);

                    updateDetailViewer1.Refresh();
                    updateListViewer1.Refresh();
                    this.Refresh();
                    System.Threading.Thread.Sleep(50);
                }
            }
            updateDetailViewer1.RunningLongOperation(false);
            updateListViewer1.RunningLongOperation(false);
        }

        private void updateDetailViewer1_ReviseUpdate(IUpdate updateToRevise)
        {
            this.Cursor = Cursors.WaitCursor;
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(Companies,updateToRevise);
            updateWizard.ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void updateListViewer1_ResignUpdate(UpdateCollection updates)
        {
            foreach (IUpdate updateToResign in updates)
            {
                MessageBox.Show(_wsus.ResignPackage(updateToResign));
            }
        }

        private void updateListViewer1_CreateSupersedingUpdate(IUpdate updateToSupersed)
        {
            this.Cursor = Cursors.WaitCursor;
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(Companies);
            updateWizard.CreateSupersedingUpdate(updateToSupersed);
            updateWizard.ShowDialog();
            this.Cursor = Cursors.Default;
        }

        private void updateListViewer1_ExportUpdate()
        {
            ExportAnUpdate();
        }

        private void UpdateControl_SizeChanged(object sender, EventArgs e)
        {
            AdjustSplitterDistance();
        }

        #endregion

    }
}
