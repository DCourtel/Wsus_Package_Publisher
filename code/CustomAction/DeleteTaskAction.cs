using System;
using System.Windows.Forms;
using System.Xml;

namespace CustomActions
{
    public partial class DeleteTaskAction : GenericAction
    {
        public DeleteTaskAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.DeleteTaskElement;
            this.Text = GetLocalizedString("DescriptionDeleteTask");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the name of the task to delete.
        /// </summary>
        /// <exception cref="NullReferenceException">'RegKey' can not be set to null.</exception>
        public string TaskName
        {
            get { return this.txtBxTaskName.Text.Trim(); }

            set { this.txtBxTaskName.Text = value.Trim(); }
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
            this.txtBxTaskName.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            if (!String.IsNullOrWhiteSpace(txtBxTaskName.Text))
            {
                this.ConfigurationState = ConfigurationStates.Configured;
                this.txtBxTaskName.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxTaskName.BackColor = System.Drawing.Color.Orange;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<TaskName>" + this.txtBxTaskName.Text + "</TaskName></Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("DeleteThisTask") + this.txtBxTaskName.Text;
        }

        #endregion Methods

        #region Events

        private void txtBxTaskName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
