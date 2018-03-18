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
    public partial class InstallMsiAction : GenericAction, IStoreReturnCodeToVariable
    {
        public InstallMsiAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 350;
            this.ActionIcon = Properties.Resources.InstallMsi;
            this.Text = GetLocalizedString("DescriptionInstallMsi");

            this.cmbBxRestartBehavior.Items.Add(GetLocalizedString("RestartLevel_None"));
            this.cmbBxRestartBehavior.Items.Add(GetLocalizedString("RestartLevel_Prompt"));
            this.cmbBxRestartBehavior.Items.Add(GetLocalizedString("RestartLevel_Force"));
            this.cmbBxRestartBehavior.SelectedIndex = 0;

            this.cmbBxUiLevel.Items.Add(GetLocalizedString("UILevel_None"));
            this.cmbBxUiLevel.Items.Add(GetLocalizedString("UILevel_Base"));
            this.cmbBxUiLevel.Items.Add(GetLocalizedString("UILevel_Reduced"));
            this.cmbBxUiLevel.Items.Add(GetLocalizedString("UILevel_Full"));
            this.cmbBxUiLevel.SelectedIndex = 0;
        }

        #region Properties

        public string MsiName
        {
            get { return this.txtBxMsiName.Text; }
            set { this.txtBxMsiName.Text = value; }
        }

        public string Parameters
        {
            get { return this.txtBxParameters.Text; }
            set { this.txtBxParameters.Text = value; }
        }

        public bool IsLogRequested
        {
            get { return this.chkBxLogTo.Checked; }
            set { this.chkBxLogTo.Checked = value; }
        }

        public string LogPath
        {
            get { return this.txtBxLogTo.Text; }
            set { this.txtBxLogTo.Text = value; }
        }

        public int UiLevel
        {
            get { return this.cmbBxUiLevel.SelectedIndex; }
            set { this.cmbBxUiLevel.SelectedIndex = value; }
        }

        public int RestartBehavior
        {
            get { return this.cmbBxRestartBehavior.SelectedIndex; }
            set { this.cmbBxRestartBehavior.SelectedIndex = value; }
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

        /// <summary>
        /// Gets or Sets if the return code should be stored in a variable.
        /// </summary>
        public bool StoreToVariable
        {
            get { return this.chkBxStoreToVariable.Checked; }
            set { this.chkBxStoreToVariable.Checked = value; }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'Msi Name' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxMsiName.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool isMsiPathValid = !String.IsNullOrWhiteSpace(this.txtBxMsiName.Text);
            bool isLogPathValid = !chkBxLogTo.Checked || !String.IsNullOrWhiteSpace(this.txtBxLogTo.Text);

            this.ConfigurationState = (isMsiPathValid && isLogPathValid) ? ConfigurationStates.Configured : ConfigurationStates.Misconfigured;
            this.txtBxMsiName.BackColor = isMsiPathValid ? SystemColors.Window : Color.Orange;
            this.txtBxLogTo.BackColor = isLogPathValid ? SystemColors.Window : Color.Orange;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<MsiName>" + this.MsiName + "</MsiName>\r\n<Parameters>" + this.Parameters + "</Parameters>\r\n" +
                "<IsLogRequested>" + this.IsLogRequested.ToString() + "</IsLogRequested>\r\n<LogPath>" + this.LogPath + "</LogPath>\r\n" +
                "<UiLevel>" + this.UiLevel + "</UiLevel>\r\n<RestartBehavior>" + this.RestartBehavior + "</RestartBehavior>\r\n<KillProcess>" +
                this.KillProcess.ToString() + "</KillProcess>\r\n<KillAfter>" + this.nupKillProcess.Value.ToString() + "</KillAfter>\r\n" +
                "<StoreToVariable>" + this.StoreToVariable.ToString() + "</StoreToVariable>\r\n</Action>";

            return _result;
        }

        public override void DisplayHelp()
        {
            // ToDo : Implémenter cette méthode pour afficher un message d'aide au paramétrage de cette action.
            //MessageBox.Show("Voici un test ");            
        }

        protected override string GetConfiguratedDescription()
        {
            return GetLocalizedString("Install") + this.MsiName + (!String.IsNullOrWhiteSpace(this.Parameters) ? " " + GetLocalizedString("WithParameters") + this.Parameters : String.Empty) + 
                " (" + cmbBxUiLevel.SelectedItem.ToString() + " / " + cmbBxRestartBehavior.SelectedItem.ToString() + ")"; 
        }

        #endregion Methods

        #region Events

        private void txtBxMsiName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxParameters_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void chkBxLogTo_CheckedChanged(object sender, EventArgs e)
        {
            txtBxLogTo.Enabled = chkBxLogTo.Checked;

            if (chkBxLogTo.Checked && String.IsNullOrWhiteSpace(this.txtBxLogTo.Text))
            {
                this.txtBxLogTo.Text = @"C:\Windows\Temp\" + (!String.IsNullOrWhiteSpace(this.txtBxMsiName.Text) ? this.txtBxMsiName.Text + ".log" : "WPP-InstallMSI-" + Guid.NewGuid().ToString().Substring(0, 8) + ".log");
            }
            this.ValidateData();
        }

        private void txtBxLogTo_TextChanged(object sender, EventArgs e)
        {
            this.ValidateData();
        }

        private void cmbBxUiLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateUserInteractionStatus(this.cmbBxUiLevel.SelectedIndex != 0 || this.cmbBxRestartBehavior.SelectedIndex == 1);
        }

        private void cmbBxRestartBehavior_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateUserInteractionStatus(this.cmbBxUiLevel.SelectedIndex != 0 || this.cmbBxRestartBehavior.SelectedIndex == 1);
        }

        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            nupKillProcess.Enabled = chkBxKillProcess.Checked;
        }

        private void chkBxStoreToVariable_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
