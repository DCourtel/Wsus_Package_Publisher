using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class UninstallMsiProductByNameAction : GenericAction
    {
        private const string remoteMsiManager = "RemoteMsiManager.exe";

        public UninstallMsiProductByNameAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 380;
            this.ActionIcon = Properties.Resources.MsiInstallerElement;
            this.Text = GetLocalizedString("DescriptionUninstallMsiProductByNameAction");
        }

        #region Properties

        public string ApplicationName
        {
            get { return this.txtBxApplicationName.Text; }
            set { this.txtBxApplicationName.Text = value; }
        }

        public string Exceptions
        {
            get { return this.txtBxExceptions.Text; }
            set { this.txtBxExceptions.Text = value; }
        }

        public string Parameters
        {
            get { return this.txtBxParameters.Text; }
            set { this.txtBxParameters.Text = value; }
        }

        public bool KillProcess
        {
            get { return this.chkBxKillProcess.Checked; }
            set { this.chkBxKillProcess.Checked = value; }
        }

        public int KillAfter
        {
            get { return (int)this.nupKillProcess.Value; }
            set { this.nupKillProcess.Value = value; }
        }

        public bool DontUninstallIfNoException
        {
            get { return this.chkBxDontUninstallIfNoException.Checked; }
            set { this.chkBxDontUninstallIfNoException.Checked = value; }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'Registry Key' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxApplicationName.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool productCodeConfigured = !string.IsNullOrWhiteSpace(txtBxApplicationName.Text);
            bool exceptionConfigured = (!this.chkBxDontUninstallIfNoException.Checked || !String.IsNullOrWhiteSpace(this.txtBxExceptions.Text));

            this.ConfigurationState = (productCodeConfigured && exceptionConfigured) ? ConfigurationStates.Configured : ConfigurationStates.Misconfigured;

            this.txtBxApplicationName.BackColor = productCodeConfigured ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
            this.txtBxExceptions.BackColor = exceptionConfigured ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<ApplicationName>" + this.ApplicationName + "</ApplicationName>\r\n<Exceptions>" + this.txtBxExceptions.Text + "</Exceptions>\r\n" + 
                "<Parameters>" + this.Parameters + "</Parameters>\r\n" + "<DontUninstallIfNoException>" + this.DontUninstallIfNoException.ToString() + "</DontUninstallIfNoException>\r\n" + 
            "<KillProcess>" + this.KillProcess.ToString() + "</KillProcess>\r\n<KillAfter>" + this.nupKillProcess.Value.ToString() + "</KillAfter>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return String.Format( GetLocalizedString("UninstallThisMsiApplication"), this.txtBxApplicationName.Text, this.txtBxParameters.Text);
        }

        #endregion Methods

        #region Events

        private void txtBxApplicationName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxExceptions_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxParameters_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.nupKillProcess.Enabled = this.chkBxKillProcess.Checked;
        }

        private void nupKillProcess_ValueChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        private void chkBxDontUninstallIfNoException_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void btnLaunchRemoteMsiManager_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(remoteMsiManager))
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(remoteMsiManager);
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    procInfo.Arguments = "-I=%";
                    p.StartInfo = procInfo;
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Can't find " + remoteMsiManager);
        }

        #endregion Events
    }
}
