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
    internal partial class FrmEventDisplayer : Form
    {
        UpdateEventCollection _eventCollection = new UpdateEventCollection();
        IComputerTarget _computer;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmEventDisplayer).Assembly);

        internal FrmEventDisplayer(UpdateEventCollection eventCollection, IComputerTarget computer)
        {
            InitializeComponent();
            _eventCollection = eventCollection;
            _computer = computer;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnDeleteHistory_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod(_computer.FullDomainName);
            btnDeleteHistory.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            WsusWrapper _wsus = WsusWrapper.GetInstance();
            _wsus.DeleteReportingEvents(_computer);
            this.Cursor = Cursors.Default;
            dgvEventHistory.Rows.Clear();
        }

        private void FrmEventDisplayer_Shown(object sender, EventArgs e)
        {
            dgvEventHistory.Rows.Clear();
            foreach (IUpdateEvent updateEvent in _eventCollection)
            {
                dgvEventHistory.Rows.Add(updateEvent.CreationDate.ToLocalTime().ToString(), updateEvent.ErrorCode.ToString(), updateEvent.Message, updateEvent.Status.ToString());
            }
            dgvEventHistory.ClearSelection();
            btnDeleteHistory.Enabled = (dgvEventHistory.Rows.Count != 0);
            lblNumberOfEvents.Text = dgvEventHistory.Rows.Count + " " + resMan.GetString("EventFor") + " " + _computer.FullDomainName;
        }

        private void dgvEventHistory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEventHistory.SelectedRows.Count != 0)
                dgvEventHistory.ClearSelection();
        }
    }
}
