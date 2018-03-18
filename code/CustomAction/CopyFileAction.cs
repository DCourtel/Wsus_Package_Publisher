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
    public partial class CopyFileAction : GenericAction
    {
        public CopyFileAction() : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 200;
            this.ActionIcon = Properties.Resources.CopyFileElement;
            this.Text = GetLocalizedString("DescriptionCopyFileAction");
            base.RefersToUserProfile = false;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the file that need to be rename.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string SourceFile
        {
            get { return this.txtBxSourceFile.Text.Trim(); }

            set
            {
                this.txtBxSourceFile.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the full path to the destination file.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string DestinationFolder
        {
            get { return this.txtBxDestinationFolder.Text.Trim(); }

            set
            {
                this.txtBxDestinationFolder.Text = value.Trim();
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'SourceFile' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxSourceFile.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool sourceOK = !String.IsNullOrEmpty(this.SourceFile) && !this.SourceFile.EndsWith(@"\");
            bool destinationOK = !String.IsNullOrEmpty(this.DestinationFolder) && !this.DestinationFolder.EndsWith(@"\");

            this.txtBxSourceFile.BackColor = sourceOK ? SystemColors.Window : Color.Orange;
            this.txtBxDestinationFolder.BackColor = destinationOK ? SystemColors.Window : Color.Orange;

            if (sourceOK && destinationOK)
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

            _result += "<SourceFile>" + this.SourceFile + "</SourceFile>\r\n<DestinationFolder>" + this.DestinationFolder + "</DestinationFolder>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("CopyTheFile") + this.SourceFile + this.GetLocalizedString("To") + this.DestinationFolder + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxDestinationFolder_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            base.UpdateUserProfileNotificationStatus(this.txtBxDestinationFolder.Text);
            this.ValidateData();
        }

        private void txtBxSourceFile_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            base.UpdateUserProfileNotificationStatus(this.txtBxSourceFile.Text);
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
