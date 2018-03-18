using System;
using System.Windows.Forms;
using System.Xml;

namespace CustomActions
{
    public partial class CreateShortcutAction : GenericAction
    {
        public CreateShortcutAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 450;
            this.ActionIcon = Properties.Resources.CreateShortcutElement;
            this.Text = GetLocalizedString("DescriptionCreateShortcutAction");
            this.cmbBxDesktop.SelectedIndex = 0;
            this.cmbBxWindowStyle.SelectedIndex = 0;
        }

        #region Properties

        public string Target
        {
            get { return this.txtBxTarget.Text.Trim(); }
            set { this.txtBxTarget.Text = value.Trim(); }
        }

        public string ShortcutName
        {
            get { return this.txtBxName.Text.Trim(); }
            set { this.txtBxName.Text = value.Trim(); }
        }

        public bool IsDesktopLocation
        {
            get { return rdBtnDesktop.Checked; }
            set { rdBtnDesktop.Checked = value; }
        }

        public int DesktopTarget
        {
            get { return cmbBxDesktop.SelectedIndex; }
            set { cmbBxDesktop.SelectedIndex = value; }
        }

        public bool IsPersoLocation
        {
            get { return rdBtnPersonnalized.Checked; }
            set { rdBtnPersonnalized.Checked = value; }
        }

        public string PersoLocation
        {
            get { return txtBxDirectory.Text.Trim(); }
            set { txtBxDirectory.Text = value.Trim(); }
        }

        public string Description
        {
            get { return this.txtBxDescription.Text.Trim(); }
            set { this.txtBxDescription.Text = value.Trim(); }
        }

        public string Icon
        {
            get { return this.txtBxIcon.Text.Trim(); }
            set { this.txtBxIcon.Text = value.Trim(); }
        }

        public string Arguments
        {
            get { return this.txtBxArguments.Text.Trim(); }
            set { this.txtBxArguments.Text = value.Trim(); }
        }

        public string WorkingDirectory
        {
            get { return this.txtBxWorkingDirectory.Text.Trim(); }
            set { this.txtBxWorkingDirectory.Text = value.Trim(); }
        }

        public int WindowStyle
        {
            get { return this.cmbBxWindowStyle.SelectedIndex; }
            set { this.cmbBxWindowStyle.SelectedIndex = value; }
        }

        public bool AbortIfTargetDontExist
        {
            get { return this.chkBxAbortIfDontExists.Checked; }
            set { this.chkBxAbortIfDontExists.Checked = value; }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'Target' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxTarget.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool targetOK = !String.IsNullOrEmpty(this.Target) && !this.Target.EndsWith(@"\");
            bool nameOK = !String.IsNullOrEmpty(this.ShortcutName);
            bool directoryOK = !String.IsNullOrEmpty(this.PersoLocation) && !this.PersoLocation.EndsWith(@"\");

            this.txtBxTarget.BackColor = targetOK ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
            this.txtBxName.BackColor = nameOK ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
            this.txtBxDirectory.BackColor = rdBtnPersonnalized.Checked ? (directoryOK ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange) : System.Drawing.SystemColors.Window;

            if (targetOK && nameOK && (rdBtnDesktop.Checked || (rdBtnPersonnalized.Checked && directoryOK)))
                this.ConfigurationState = ConfigurationStates.Configured;
            else
                this.ConfigurationState = ConfigurationStates.Misconfigured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string result = base.GetXMLAction();

            //switch (WindowStyle)
            //{
            //    case 0:
            //        windowStyle = 1;
            //        break;
            //    case 1:
            //        windowStyle = 3;
            //        break;
            //    case 2:
            //        windowStyle = 7;
            //        break;
            //}

            result += "<Target>" + this.Target + "</Target>\r\n<ShortcutName>" + this.ShortcutName + "</ShortcutName>\r\n<Description>" + this.Description + "</Description>\r\n<Icon>" + this.Icon + "</Icon>\r\n<Arguments>" +
                this.Arguments + "</Arguments>\r\n<WorkingDirectory>" + this.WorkingDirectory + "</WorkingDirectory>\r\n<WindowStyle>" + this.WindowStyle.ToString() + "</WindowStyle>\r\n<DesktopTarget>" +
                this.DesktopTarget.ToString() + "</DesktopTarget>\r\n<IsDesktopLocation>" + this.IsDesktopLocation.ToString() + "</IsDesktopLocation>\r\n<IsPersoLocation>" +
                this.IsPersoLocation.ToString() + "</IsPersoLocation>\r\n<PersoLocation>" + this.PersoLocation + "</PersoLocation>\r\n<AbortIfTargetDontExist>" + this.AbortIfTargetDontExist.ToString() + "</AbortIfTargetDontExist>";

            return result + "\r\n</Action>";
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("CreateThisShortcut") + this.ShortcutName + this.GetLocalizedString("WhichTarget") + this.Target;
        }

        #endregion Methods

        #region Events

        private void txtBxTarget_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            ValidateData();
        }

        private void txtBxName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            ValidateData();
        }

        private void txtBxPerso_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        private void txtBxDirectory_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            base.UpdateUserProfileNotificationStatus(this.txtBxDirectory.Text);
            ValidateData();
        }

        private void lnkEnvironmentVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEnvironmentVariables frmEnvironmentVariables = new FrmEnvironmentVariables();
            frmEnvironmentVariables.ShowDialog();
        }

        private void rdBtnDesktop_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            if (rdBtnDesktop.Checked)
            {
                cmbBxDesktop.Enabled = true;
                txtBxDirectory.Enabled = false;
                lnkEnvironmentVariables.Enabled = false;
                cmbBxDesktop_SelectedIndexChanged(null, null);
            }
            else
            {
                cmbBxDesktop.Enabled = false;
                txtBxDirectory.Enabled = true;
                lnkEnvironmentVariables.Enabled = true;
                base.UpdateUserProfileNotificationStatus(String.Empty);
            }

            ValidateData();
        }

        private void cmbBxDesktop_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.UpdateUserProfileNotificationStatus(this.cmbBxDesktop.SelectedIndex == 0 ? "%AllUsersProfile%" : "%UserProfile%");
            this.OnChange(null);
        }

        private void cmbBxWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        private void optionnalTxtBxData_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        private void chkBxAbortIfDontExists_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        #endregion Events
    }
}
