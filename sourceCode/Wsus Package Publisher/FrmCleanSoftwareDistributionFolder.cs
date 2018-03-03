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
    internal partial class FrmCleanSoftwareDistributionFolder : Form
    {
        private List<ADComputer> remoteComputers = new List<ADComputer>();
        private string _username = string.Empty;
        private string _password = string.Empty;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmCleanSoftwareDistributionFolder).Assembly);

        internal FrmCleanSoftwareDistributionFolder(List<ADComputer> computers, string username, string password)
        {
            Logger.EnteringMethod("FrmCleanSoftwareDistributionFolder");
            InitializeComponent();
            remoteComputers = computers;
            _username = username;
            _password = password;
        }
        
        #region (response to events - Réponses aux événements)

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnStart.Enabled = false;
            btnClose.Enabled = false;
            bool result = false;
            this.Cursor = Cursors.WaitCursor;
            foreach (DataGridViewRow row in dgvComputers.Rows)
            {
                ADComputer computer = (ADComputer)row.Cells["Computer"].Value;
                result = computer.CleanSoftwareDistributionFolder(_username, _password);
                Logger.Write(computer.Name + " : " + result);
                if (result)
                    row.Cells["Result"].Value = resMan.GetString("Succeeded");
                else
                    row.Cells["Result"].Value = resMan.GetString("Failed");
            }
            this.Cursor = Cursors.Default;
            btnStart.Enabled = true;
            btnClose.Enabled = true;
        }

        private void FrmCleanSoftwareDistributionFolder_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            dgvComputers.Rows.Clear();
            foreach (ADComputer computer in remoteComputers)
            {
                Logger.Write("Adding " + computer.Name);
                dgvComputers.Rows.Add(computer);
            }
        }

        #endregion (response to events - Réponses aux événements)
    }
}
