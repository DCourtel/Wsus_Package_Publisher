using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmApprovalSet : Form
    {
        private ComputerGroup _computersGroup;
        private List<MetaGroup> _metaGroups;
        private List<ApprovalObject> _approval = new List<ApprovalObject>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmApprovalSet).Assembly);
        private WsusWrapper _wsus;
        private IComputerTargetGroup _allComputerGroup;

        internal FrmApprovalSet(List<MetaGroup> metaGroups, ComputerGroup computersGroup, UpdateCollection updatesToApprove)
        {
            Logger.EnteringMethod("FrmApprovalSet");
            InitializeComponent();

            _wsus = WsusWrapper.GetInstance();
            _allComputerGroup = _wsus.GetAllComputerTargetGroup();
            _metaGroups = metaGroups;
            _computersGroup = computersGroup;
            dtDeadLine.Value = DateTime.Now.AddDays(_wsus.CurrentServer.DeadLineDaysSpan);
            nupHour.Value = _wsus.CurrentServer.DeadLineHour;
            nupMinute.Value = _wsus.CurrentServer.DeadLineMinute;
            FillDataGridView(updatesToApprove, metaGroups, computersGroup);
        }

        private void FillDataGridView(UpdateCollection updatesToApprove, List<MetaGroup> metaGroups, ComputerGroup computersGroup)
        {
            Logger.EnteringMethod();
            bool uninstallAllowed = IsUninstallationAllowed(updatesToApprove);
            object[] approvalsObj;

            if (uninstallAllowed)
            {
                lblUninstallationAllowed.Text = resMan.GetString("UninstallationAllowed");
                approvalsObj = new object[]
            { resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString()), 
              resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.ApproveForUninstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString())
            };
            }
            else
            {
                lblUninstallationAllowed.Text = resMan.GetString("UninstallationDisallowed");
                approvalsObj = new object[]
            { resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString()), 
              resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()),
              resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString())
            };
            }
            DateTime noDeadLineSet = DateTime.MaxValue;
            
            dgvTargetGroup.SuspendLayout();
            DataGridViewComboBoxColumn approvalColumn = (DataGridViewComboBoxColumn)dgvTargetGroup.Columns["Approval"];
            approvalColumn.Items.AddRange(approvalsObj);
            cmbBxApproval.Items.AddRange(approvalsObj);
            cmbBxApproval.SelectedIndex = 0;

            FillMetaGroup(metaGroups);
            FillComputerGroup(computersGroup);

            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                if ((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId == _allComputerGroup.Id)
                    (row.Cells["Approval"] as DataGridViewComboBoxCell).Items.Remove(resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString()));
                if (updatesToApprove.Count == 1)
                {
                    UpdateApprovalCollection approvals = _wsus.GetUpdateApprovalStatus((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId, updatesToApprove[0]);
                    if (approvals.Count != 0)
                    {
                        switch (approvals[0].Action)
                        {
                            case UpdateApprovalAction.All:
                                row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                                break;
                            case UpdateApprovalAction.Install:
                                if (approvals[0].IsOptional)
                                    row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString());
                                else
                                    row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString());
                                if (approvals[0].Deadline != noDeadLineSet)
                                    row.Cells["DeadLine"].Value = approvals[0].Deadline.ToLocalTime();
                                break;
                            case UpdateApprovalAction.NotApproved:
                                row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString());
                                break;
                            case UpdateApprovalAction.Uninstall:
                                row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForUninstallation.ToString());
                                if (approvals[0].Deadline != noDeadLineSet)
                                    row.Cells["DeadLine"].Value = approvals[0].Deadline.ToLocalTime();
                                break;
                            default:
                                row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                                break;
                        }
                    }
                    else
                        row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                }
                else
                    row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
            }
            dgvTargetGroup.ResumeLayout();
        }

        private void FillMetaGroup(List<MetaGroup> metaGroups)
        {
            Logger.EnteringMethod();
            dtGrdVwMetaGroup.Rows.Clear();
            foreach (MetaGroup group in metaGroups)
            {
                int index = dtGrdVwMetaGroup.Rows.Add();
                dtGrdVwMetaGroup.Rows[index].Cells["MetaGroup"].Value = group;
                dtGrdVwMetaGroup.Rows[index].Cells["Selected"].Value = false;
            }
            dtGrdVwMetaGroup.Sort(dtGrdVwMetaGroup.Columns["MetaGroup"], ListSortDirection.Ascending);
        }

        private void FillComputerGroup(ComputerGroup group)
        {
            Logger.EnteringMethod();
            dgvTargetGroup.Rows.Add(group);
            foreach (ComputerGroup innerGroup in group.InnerComputerGroup)
                FillComputerGroup(innerGroup);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            SetApprovals();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SetApprovals()
        {
            Logger.EnteringMethod();
            ApprovalObject.Approvals[] approvalsArray = new ApprovalObject.Approvals[]
            { ApprovalObject.Approvals.Unchanged, 
              ApprovalObject.Approvals.ApproveForInstallation,
              ApprovalObject.Approvals.ApproveForOptionalInstallation,
              ApprovalObject.Approvals.ApproveForUninstallation,
              ApprovalObject.Approvals.NotApproved
            };

            _approval.Clear();

            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                if (row.Cells["Approval"].Value != null && !string.IsNullOrEmpty(row.Cells["Approval"].Value.ToString()))
                {
                    ApprovalObject.Approvals approval = GetApproval(row.Cells["Approval"].Value.ToString());
                    if (approval != ApprovalObject.Approvals.Unchanged)
                    {
                        if (row.Cells["DeadLine"].Value != null)
                        {
                            DateTime deadLine;
                            if (DateTime.TryParse(row.Cells[2].Value.ToString(), out deadLine))
                                _approval.Add(new ApprovalObject((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId, approval, deadLine));
                            else
                                _approval.Add(new ApprovalObject((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId, approval));
                        }
                        else
                            _approval.Add(new ApprovalObject((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId, approval));
                    }

                }
            }
        }

        private ApprovalObject.Approvals GetApproval(string searchValue)
        {
            Logger.EnteringMethod();
            foreach (ApprovalObject.Approvals approval in Enum.GetValues(typeof(ApprovalObject.Approvals)))
            {
                if (resMan.GetString(approval.ToString()) == searchValue)
                    return approval;
            }
            return ApprovalObject.Approvals.Unchanged;
        }

        internal List<ApprovalObject> Approvals
        {
            get { return _approval; }
        }

        private bool IsUninstallationAllowed(UpdateCollection updatesToApprove)
        {
            Logger.EnteringMethod();
            bool result = true;

            foreach (IUpdate update in updatesToApprove)
            {
                if (update.UninstallationBehavior.IsSupported == false)
                {
                    Logger.Write(update.Title + " doesn't support uninstallation.");
                    result = false;
                    break;
                }
            }
            Logger.Write("Returning " + result);
            return result;
        }

        private void btnSetDeadLine_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
            {
                if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()) &&
                    row.Cells[1].Value.ToString() != resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString()) &&
                    row.Cells[1].Value.ToString() != resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()))
                    row.Cells[2].Value = new System.DateTime(dtDeadLine.Value.Year, dtDeadLine.Value.Month, dtDeadLine.Value.Day, (int)nupHour.Value, (int)nupMinute.Value, 0);
            }
        }

        private void btnSetApproval_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (cmbBxApproval.SelectedItem != null)
            {
                foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
                {
                    if (!((row.Cells["Group"].Value as ComputerGroup).ComputerGroupId == _allComputerGroup.Id && cmbBxApproval.SelectedItem.ToString() == resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString())))
                    {
                        row.Cells["Approval"].Value = cmbBxApproval.SelectedItem;
                        if (cmbBxApproval.SelectedItem.ToString() == resMan.GetString(ApprovalObject.Approvals.NotApproved.ToString()) ||
                            cmbBxApproval.SelectedItem.ToString() == resMan.GetString(ApprovalObject.Approvals.ApproveForOptionalInstallation.ToString()))
                            row.Cells["DeadLine"].Value = null;
                    }
                }
            }
        }

        private void dgvTargetGroup_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Logger.EnteringMethod();
            foreach (DataGridViewRow row in dgvTargetGroup.SelectedRows)
            {
                if (row.Cells[1].Value != null && !string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                    row.Cells[2].Value = null;
            }
        }

        private void dtGrdVwMetaGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Logger.EnteringMethod();
            if (e.RowIndex != -1)
            {
                DataGridViewRow clickedRow = dtGrdVwMetaGroup.Rows[e.RowIndex];
                MetaGroup clickedMetaGroup = (MetaGroup)clickedRow.Cells["MetaGroup"].Value;
                bool selected = (bool)clickedRow.Cells["Selected"].Value;

                clickedRow.Selected = !selected;
                clickedRow.Cells["Selected"].Value = !selected;
                AdjustComputerGroupState(clickedMetaGroup, !selected);
            }
        }

        private void AdjustComputerGroupState(MetaGroup clickedMetaGroup, bool state)
        {
            Logger.EnteringMethod();
            foreach (ComputerGroup computerGroupToSelect in clickedMetaGroup.InnerComputerGroups)
            {
                foreach (DataGridViewRow row in dgvTargetGroup.Rows)
                {
                    if ((ComputerGroup)row.Cells["Group"].Value == computerGroupToSelect)
                    {
                        row.Selected = state;
                        break;
                    }
                }
            }

            foreach (MetaGroup metaGroup in clickedMetaGroup.InnerMetaGroups)
                AdjustComputerGroupState(metaGroup, state);
        }

        internal void QuicklyApprove(MetaGroup selectedMetaGroup)
        {
            Logger.EnteringMethod();
            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                row.Selected = false;
                row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.Unchanged.ToString());
                row.Cells["DeadLine"].Value = null;
            }
            AdjustComputerGroupState(selectedMetaGroup, true);
            foreach (DataGridViewRow row in dgvTargetGroup.Rows)
            {
                if (row.Selected)
                    row.Cells["Approval"].Value = resMan.GetString(ApprovalObject.Approvals.ApproveForInstallation.ToString());
            }
            SetApprovals();
        }

        private void FrmApprovalSet_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            dgvTargetGroup.Rows[0].Selected = false;
        }

    }
}
