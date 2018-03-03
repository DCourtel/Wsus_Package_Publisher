using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WUApiLib;

namespace ShowPendingUpdates
{
    public partial class FrmShowPendingUpdates : Form
    {
        private string computerName = string.Empty;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("ShowPendingUpdates.Resources.Resources", typeof(FrmShowPendingUpdates).Assembly);

        public FrmShowPendingUpdates()
        {
            InitializeComponent();
            try
            {
                btnOk.Enabled = false;
                lblInformations.Text = resMan.GetString("Searching");
                string args = Environment.CommandLine.Substring(Environment.CommandLine.LastIndexOf(' ') + 1);
                if (!string.IsNullOrEmpty(args))
                    computerName = args;
                else
                    computerName = "localhost";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ShowPendingUpdates()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                lblPendingUpdatesOn.Text += computerName;

                ShowUpdates(GetPendingUpdates());
                btnOk.Enabled = true;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private UpdateCollection GetPendingUpdates()
        {
            UpdateCollection pendingUpdates = new UpdateCollection();

            try
            {
                Type t = Type.GetTypeFromProgID("Microsoft.Update.Session", computerName);
                UpdateSession uSession = (UpdateSession)Activator.CreateInstance(t);

                IUpdateSearcher uSearcher = uSession.CreateUpdateSearcher();
                uSearcher.ServerSelection = ServerSelection.ssManagedServer;
                uSearcher.IncludePotentiallySupersededUpdates = false;
                uSearcher.Online = false;

                ISearchResult sResult = uSearcher.Search("IsInstalled=0 And IsHidden=0 And Type='Software'");
                if (sResult.ResultCode == OperationResultCode.orcSucceeded && sResult.Updates.Count != 0)
                    pendingUpdates = sResult.Updates;
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.Forms.MessageBox.Show("Unauthorized Access Exception ! Ensure you have admin privileges on the remote computer.");
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                System.Windows.Forms.MessageBox.Show("COM Exception ! Verify the firewall settings on the remote computer.");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.GetType().ToString() + "\r\n" + ex.Message);
            }

            return pendingUpdates;
        }

        private void ShowUpdates(WUApiLib.UpdateCollection updates)
        {
            dgvPendingUpdates.Rows.Clear();

            try
            {
                foreach (WUApiLib.IUpdate update in updates)
                {
                    int index = dgvPendingUpdates.Rows.Add();
                    DataGridViewRow row = dgvPendingUpdates.Rows[index];
                    row.Cells["Title"].Value = update.Title;
                    row.Cells["Description"].Value = update.Description;
                }
                lblInformations.Text = resMan.GetString("PendingUpdates") + " " + updates.Count;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmShowPendingUpdates_Shown(object sender, EventArgs e)
        {
            ShowPendingUpdates();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("http://msdn.microsoft.com/en-us/library/windows/desktop/aa387288(v=vs.85).aspx");
            System.Diagnostics.Process.Start(sInfo);
        }
    }
}
