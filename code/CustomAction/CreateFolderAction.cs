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
    public partial class CreateFolderAction : GenericAction
    {
        public CreateFolderAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.CreateFolderElement;
            this.Text = GetLocalizedString("DescriptionCreateFolderAction");
            base.RefersToUserProfile = false;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the folder to create.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
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
        public void ValidateData()
        {
            if (String.IsNullOrEmpty(this.FullPath) || this.FullPath.EndsWith(@"\"))
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxFullPath.BackColor = Color.Orange;
            }
            else
            {
                this.ConfigurationState = ConfigurationStates.Configured;
                this.txtBxFullPath.BackColor = SystemColors.Window;
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
            return this.GetLocalizedString("CreateTheFolder") + this.FullPath + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxFullPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            base.UpdateUserProfileNotificationStatus(this.txtBxFullPath.Text);
            this.ValidateData();
        }

        private void lnkEnvironmentVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEnvironmentVariables frmEnvironmentVariables = new FrmEnvironmentVariables();
            frmEnvironmentVariables.ShowDialog();
        }

        #endregion Events
    }
}
