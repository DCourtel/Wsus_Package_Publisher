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
    public partial class RenameFolderAction : GenericAction
    {
        public RenameFolderAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 200;
            this.ActionIcon = Properties.Resources.RenameFolderElement;
            this.Text = GetLocalizedString("DescriptionRenameFolderAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the folder that nedd to be rename.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string FolderPath
        {
            get { return this.txtBxFolderPath.Text.Trim(); }

            set
            {
                this.txtBxFolderPath.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the new name of the folder.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string NewName
        {
            get { return txtBxNewName.Text.Trim(); }

            set
            {
                this.txtBxNewName.Text = value.Trim();
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'FolderPath' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxFolderPath.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        public void ValidateData()
        {
            bool folderPathOK = !String.IsNullOrEmpty(this.FolderPath) && !this.FolderPath.EndsWith(@"\");
            bool newNameOK = !String.IsNullOrEmpty(this.NewName) && !GenericAction.ContainsIllegalCharacters(this.NewName);

            this.txtBxFolderPath.BackColor = folderPathOK ? SystemColors.Window : Color.Orange;
            this.txtBxNewName.BackColor = newNameOK ? SystemColors.Window : Color.Orange;

            if (folderPathOK && newNameOK)
                this.ConfigurationState = ConfigurationStates.Configured;
            else
                this.ConfigurationState = ConfigurationStates.Misconfigured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<FolderPath>" + this.FolderPath + "</FolderPath>\r\n<NewName>" + this.NewName + "</NewName>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("RenameTheFolder") + this.FolderPath + this.GetLocalizedString("Into") + this.NewName + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxFolderPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            this.UpdateUserProfileNotificationStatus(this.txtBxFolderPath.Text);
        }

        private void txtBxNewName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
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
