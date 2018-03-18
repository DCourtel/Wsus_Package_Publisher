using System;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class UnregisterDLLAction : GenericAction
    {
        public UnregisterDLLAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 130;
            this.ActionIcon = Properties.Resources.UnRegisterDLL;
            this.Text = GetLocalizedString("DescriptionUnregisterDLLAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the DLL file to unregister.
        /// </summary>
        /// <exception cref="NullReferenceException">'FullPath' can not be set to null.</exception>
        public string FullPath
        {
            get { return this.txtBxFullPath.Text.Trim(); }

            set
            {
                this.txtBxFullPath.Text = value.Trim();
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'FullPath' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxFullPath.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            if (String.IsNullOrEmpty(this.FullPath) || this.FullPath.EndsWith(@"\"))
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxFullPath.BackColor = System.Drawing.Color.Orange;
            }
            else
            { this.ConfigurationState = ConfigurationStates.Configured;
            this.txtBxFullPath.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<FullPath>" + this.FullPath + "</FullPath>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("UnregisterThisDLL") + this.FullPath;
        }

        #endregion Methods

        #region Events

        private void txtBxFullPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            base.UpdateUserProfileNotificationStatus(this.txtBxFullPath.Text);
        }

        private void lnkEnvironmentVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEnvironmentVariables frmEnvironmentVariables = new FrmEnvironmentVariables();
            frmEnvironmentVariables.ShowDialog();
        }

        #endregion Events
    }
}
