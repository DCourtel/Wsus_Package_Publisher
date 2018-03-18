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
    public partial class DeleteFolderAction : GenericAction
    {
        public DeleteFolderAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 130;
            this.ActionIcon = Properties.Resources.DeleteFolderElement;
            this.Text = GetLocalizedString("DescriptionDeleteFolderAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the folder to delete.
        /// </summary>
        /// <exception cref="NullReferenceException">'FolderPath' can not be set to null.</exception>
        public string FolderPath
        {
            get { return this.txtBxFolderPath.Text.Trim(); }

            set
            {
                    this.txtBxFolderPath.Text = value.Trim();
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
        private void ValidateData()
        {
            if (String.IsNullOrEmpty(this.FolderPath) || this.FolderPath.EndsWith(@"\"))
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxFolderPath.BackColor = Color.Orange;
            }
            else
            { this.ConfigurationState = ConfigurationStates.Configured;
            this.txtBxFolderPath.BackColor = SystemColors.Window;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<FolderPath>" + this.FolderPath + "</FolderPath>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("DeleteTheFolder") + this.FolderPath + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxFolderPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            this.UpdateUserProfileNotificationStatus(this.txtBxFolderPath.Text);
        }

        private void lnkEnvironmentVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEnvironmentVariables frmEnvironmentVariables = new FrmEnvironmentVariables();
            frmEnvironmentVariables.ShowDialog();
        }

        #endregion Events
    }
}
