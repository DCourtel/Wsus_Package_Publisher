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
    public partial class CreateTextFileAction : GenericAction
    {
        public CreateTextFileAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 300;
            this.ActionIcon = Properties.Resources.CreateTextFileElement;
            this.Text = GetLocalizedString("DescriptionCreateTextFileAction");
            base.RefersToUserProfile = false;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the path to the file that will be create.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string FilePath
        {
            get { return this.txtBxFilePath.Text.Trim(); }

            set
            {
                this.txtBxFilePath.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the name of the file that will be create.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string Filename
        {
            get { return this.txtBxFilename.Text.Trim(); }

            set
            {
                this.txtBxFilename.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the content of the file that will be create.
        /// </summary>
        /// <exception cref="NullReferenceException">This property can not be set to null.</exception>
        public string Content
        {
            get { return this.txtBxContent.Text; }

            set
            {
                if (value == null)
                    throw new NullReferenceException("'Content' can not be null.");
                else
                {
                    value = value.Replace("\n", "\r\n");
                    this.txtBxContent.Text = value; }
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'PathToTheFile' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxFilePath.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool pathOk = !String.IsNullOrEmpty(this.FilePath);
            bool filenameOk = !String.IsNullOrEmpty(this.Filename) && !GenericAction.ContainsIllegalCharacters(this.txtBxFilename.Text) && GenericAction.IsValidFileOrFolderName(this.txtBxFilename.Text);

            this.txtBxFilePath.BackColor = pathOk ? SystemColors.Window : Color.Orange;
            this.txtBxFilename.BackColor = filenameOk ? SystemColors.Window : Color.Orange;
            this.ConfigurationState = (pathOk && filenameOk) ? ConfigurationStates.Configured : ConfigurationStates.Misconfigured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<FilePath>" + this.FilePath +
                "</FilePath>\r\n<Filename>" + this.Filename +
                "</Filename>\r\n<Content>" + this.Content +
                "</Content>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("CreateTheTextFile") + this.Filename + this.GetLocalizedString("In") + this.FilePath + (!String.IsNullOrEmpty(this.Content) ? (this.GetLocalizedString("WithContent") + this.Content.Substring(0, System.Math.Min(100, this.Content.Length)) + "...") : string.Empty);
        }

        #endregion Methods

        #region Events

        private void txtBxFilePath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            base.UpdateUserProfileNotificationStatus(this.txtBxFilePath.Text);
            this.ValidateData();
        }

        private void txtBxFilename_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxContent_TextChanged(object sender, EventArgs e)
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
