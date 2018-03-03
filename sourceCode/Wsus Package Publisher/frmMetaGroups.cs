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
    internal partial class frmMetaGroups : Form
    {
        private List<MetaGroup> _metaGroups;
        private ComputerGroup _computerGroups;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(frmMetaGroups).Assembly);

        internal frmMetaGroups(List<MetaGroup> metaGroups, ComputerGroup computerGroups)
        {
            Logger.EnteringMethod("frmMetaGroups");
            InitializeComponent();

            _metaGroups = metaGroups;
            FillMetaGroups();
            _computerGroups = computerGroups;
            FillComputerGroup(computerGroups);
            Localize();
            AdjustSplitterDistance();
        }

        internal frmMetaGroups(List<MetaGroup> metaGroups, ComputerGroup computerGroups, string metaGroupToEdit)
        {
            Logger.EnteringMethod("frmMetaGroups");
            InitializeComponent();

            _metaGroups = metaGroups;
            FillMetaGroups();
            _computerGroups = computerGroups;
            FillComputerGroup(computerGroups);
            Localize();
            foreach (MetaGroup item in cmbBxMetaGroups.Items)
                if (item.Name == metaGroupToEdit)
                {
                    cmbBxMetaGroups.SelectedItem = item;
                    break;
                }
            AdjustSplitterDistance();
        }

        #region {Methods - Méthodes}

        private void FillMetaGroups()
        {
            Logger.EnteringMethod();
            dtGrdVwMetaGroups.Rows.Clear();
            cmbBxMetaGroups.Items.Clear();
            foreach (MetaGroup metagroup in _metaGroups)
            {
                Logger.Write(metagroup.Name);
                int index = dtGrdVwMetaGroups.Rows.Add();
                dtGrdVwMetaGroups.Rows[index].Cells["MetaGroupName"].Value = metagroup;
                cmbBxMetaGroups.Items.Add(metagroup);
            }
            dtGrdVwMetaGroups.Sort(dtGrdVwMetaGroups.Columns["MetaGroupName"], ListSortDirection.Ascending);
            if (cmbBxMetaGroups.Items.Count != 0)
            {
                cmbBxMetaGroups.SelectedIndex = 0;
                ShowComputersGroup((MetaGroup)cmbBxMetaGroups.SelectedItem);
            }
            btnEdit.Enabled = (cmbBxMetaGroups.Items.Count != 0);
            btnDelete.Enabled = (cmbBxMetaGroups.Items.Count != 0);
        }

        private void FillComputerGroup(ComputerGroup group)
        {
            Logger.EnteringMethod(group.Name);
            int index = dtGrdVwComputerGroups.Rows.Add();
            dtGrdVwComputerGroups.Rows[index].Cells["ComputerGroupName"].Value = group;

            foreach (ComputerGroup innerGroup in group.InnerComputerGroup)
            {
                FillComputerGroup(innerGroup);
            }
        }

        private void SelectMetaGroup(MetaGroup selectedMetaGroup)
        {
            Logger.EnteringMethod(selectedMetaGroup.Name);
            foreach (DataGridViewRow row in dtGrdVwMetaGroups.Rows)
            {
                row.Selected = false;
                foreach (MetaGroup innerMetaGroup in selectedMetaGroup.InnerMetaGroups)
                    if ((row.Cells["MetaGroupName"].Value as MetaGroup).Name == innerMetaGroup.Name)
                    {
                        row.Selected = true;
                        break;
                    }
            }
        }

        private void Localize()
        {
            Logger.EnteringMethod();
            dtGrdVwMetaGroups.Columns["MetaGroupName"].HeaderText = resMan.GetString("MetaGroupName");
            dtGrdVwComputerGroups.Columns["ComputerGroupName"].HeaderText = resMan.GetString("ComputerGroupName");
        }

        private void ShowComputersGroup(MetaGroup selectedMetaGroup)
        {
            Logger.EnteringMethod(selectedMetaGroup.Name);
            foreach (DataGridViewRow row in dtGrdVwComputerGroups.Rows)
            {
                row.Selected = false;
                foreach (ComputerGroup group in selectedMetaGroup.InnerComputerGroups)
                {
                    if ((row.Cells["ComputerGroupName"].Value as ComputerGroup).Name == group.Name)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void AdjustSplitterDistance()
        {
            Logger.EnteringMethod();
            if (dtGrdVwMetaGroups.Rows.Count != 0)
            {
                int height = DataGridViewHeight;

                if (height < (splitContainer1.Height / 2))
                    splitContainer1.SplitterDistance = (int)(height + 10);
                else
                    splitContainer1.SplitterDistance = (int)(splitContainer1.Height / 2);
            }
        }

        #endregion {Methods - Méthodes}

        #region {Properties - Propriétés}

        internal List<MetaGroup> MetaGroupList { get { return _metaGroups; } }

        private int DataGridViewHeight
        {
            get
            {
                int height = 0;

                height += dtGrdVwMetaGroups.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dtGrdVwMetaGroups.Rows)
                {
                    height += row.Height;
                }

                return height;
            }
        }

        #endregion {Properties - Propriétés}

        #region {Responses to Events - Réponses aux évènements}

        private void dtGrdVwMetaGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Logger.EnteringMethod();
            if (dtGrdVwMetaGroups.SelectedRows.Count == 1)
            {
                cmbBxMetaGroups.SelectedItem = (MetaGroup)dtGrdVwMetaGroups.SelectedRows[0].Cells["MetaGroupName"].Value;
                btnEdit.PerformClick();
            }
        }

        private void cmbBxMetaGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (cmbBxMetaGroups.SelectedIndex != -1)
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
                SelectMetaGroup((MetaGroup)cmbBxMetaGroups.SelectedItem);
                ShowComputersGroup((MetaGroup)cmbBxMetaGroups.SelectedItem);
            }
            else
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            frmMetaGroupCreation metaGroupCreation = new frmMetaGroupCreation(_metaGroups, _computerGroups);

            if (metaGroupCreation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _metaGroups = metaGroupCreation.MetaGroupList;
                FillMetaGroups();
            }
            metaGroupCreation.Dispose();
            metaGroupCreation = null;
            AdjustSplitterDistance();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            MetaGroup metaGroupToEdit = (MetaGroup)cmbBxMetaGroups.SelectedItem;
            frmMetaGroupCreation metaGroupCreation = new frmMetaGroupCreation(_metaGroups, _computerGroups, metaGroupToEdit);

            if (metaGroupCreation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _metaGroups = metaGroupCreation.MetaGroupList;
                FillMetaGroups();
            }
            metaGroupCreation.Dispose();
            metaGroupCreation = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            MetaGroup metaGroupToDelete = (MetaGroup)cmbBxMetaGroups.SelectedItem;
            _metaGroups.Remove(metaGroupToDelete);
            foreach (MetaGroup metaGroup in _metaGroups)
            {
                if (metaGroup.InnerMetaGroups.Contains(metaGroupToDelete))
                    metaGroup.InnerMetaGroups.Remove(metaGroupToDelete);
            }
            FillMetaGroups();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void frmMetaGroups_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (cmbBxMetaGroups.SelectedIndex != -1)
            {
                SelectMetaGroup((MetaGroup)cmbBxMetaGroups.SelectedItem);
                ShowComputersGroup((MetaGroup)cmbBxMetaGroups.SelectedItem);
            }
        }

        #endregion {Responses to Events - Réponses aux évènements}

    }
}
