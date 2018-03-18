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
    public partial class RenameFileAction : GenericAction
    {
        public RenameFileAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 180;
            this.ActionIcon = Properties.Resources.RenameFileElement;
            this.Text = GetLocalizedString("DescriptionRenameFileAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the file that will be renamed.
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

        /// <summary>
        /// Gets or Sets the new name of the file.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string NewName
        {
            get { return this.txtBxNewName.Text.Trim(); }

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
            bool pathOK = !String.IsNullOrEmpty(this.FullPath) && !this.FullPath.EndsWith(@"\");
            bool newNameOK = !String.IsNullOrEmpty(this.NewName) && GenericAction.IsValidFileOrFolderName(this.NewName) && !GenericAction.ContainsIllegalCharacters(this.NewName);

            this.txtBxFullPath.BackColor = pathOK ? SystemColors.Window : Color.Orange;
            this.txtBxNewName.BackColor = newNameOK ? SystemColors.Window : Color.Orange;

            this.ConfigurationState = (pathOK && newNameOK) ? ConfigurationStates.Configured : ConfigurationStates.Misconfigured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<FullPath>" + this.FullPath + "</FullPath>\r\n<NewName>" + this.NewName + "</NewName>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("RenameTheFile") + this.FullPath + this.GetLocalizedString("Into") + this.NewName + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxFullPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            this.UpdateUserProfileNotificationStatus(this.txtBxFullPath.Text);
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
