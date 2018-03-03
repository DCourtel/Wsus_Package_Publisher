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
    internal partial class FrmPrerequisiteSubscribers : Form
    {
        internal enum ActionsOnSubsribers
        {
            UnsubscribeAndDelete,
            Unsubscribe,
            DoNothing
        }
        private UpdateCollection subscribers = new UpdateCollection();
        private ActionsOnSubsribers option = ActionsOnSubsribers.DoNothing;
        private IUpdate updateToDelete = null;

        public FrmPrerequisiteSubscribers()
        {
            Logger.EnteringMethod("FrmPrerequisiteSubscribers");
            InitializeComponent();
        }

        internal FrmPrerequisiteSubscribers(IUpdate updateToDelete)
        {
            Logger.EnteringMethod(updateToDelete.Title);
            InitializeComponent();
            this.updateToDelete = updateToDelete;
            lblUpdateTitle.Text = updateToDelete.Title;
        }

        internal UpdateCollection Subscribers
        {
            get { return subscribers; }
        }

        internal ActionsOnSubsribers ActionOnSubsribers
        {
            get { return option; }
            private set
            {
                Logger.Write("Setting action to : " + value.ToString());
                option = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            subscribers.Clear();

            foreach (DataGridViewRow row in dgvUpdates.Rows)
            {
                IUpdate update = (IUpdate)row.Cells["Update"].Value;
                subscribers.Add(update);
                Logger.Write(update.Title);
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void OptionChange(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (rdBtnDoNothing.Checked)
                ActionOnSubsribers = ActionsOnSubsribers.DoNothing;
            if (rdBtnUnsubscribe.Checked)
                ActionOnSubsribers = ActionsOnSubsribers.Unsubscribe;
            if (rdBtnUnsubscribeAndDelete.Checked)
                ActionOnSubsribers = ActionsOnSubsribers.UnsubscribeAndDelete;
        }

        private void FrmPrerequisiteSubscribers_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Cursor = Cursors.WaitCursor;

            if (updateToDelete != null)
            {
                dgvUpdates.Rows.Clear();
                WsusWrapper wsus = WsusWrapper.GetInstance();
                UpdateScope scope = new UpdateScope();
                scope.UpdateSources = UpdateSources.All;
                UpdateCollection allUpdates = wsus.GetAllUpdates(scope);

                foreach (IUpdate update in allUpdates)
                {
                    if (update.IsEditable)
                    {
                        SoftwareDistributionPackage sdp = wsus.GetMetaData(update);
                        if (sdp != null)
                        {
                            IList<PrerequisiteGroup> prerequisites = sdp.Prerequisites;

                            if (wsus.PrerequisitePresent(prerequisites, updateToDelete.Id.UpdateId))
                            {
                                Logger.Write("adding update : " + update.Title);
                                AddRow(update);
                            }
                        }
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void AddRow(IUpdate update)
        {
            Logger.EnteringMethod(update.Title);
            int index = dgvUpdates.Rows.Add();
            DataGridViewRow row = dgvUpdates.Rows[index];

            if (update.CompanyTitles != null && update.CompanyTitles.Count != 0)
                row.Cells["VendorName"].Value = update.CompanyTitles[0];

            if (update.ProductTitles != null && update.ProductTitles.Count != 0)
                row.Cells["ProductName"].Value = update.ProductTitles[0];

            row.Cells["Title"].Value = update.Title;
            row.Cells["Update"].Value = update;
        }
    }
}
