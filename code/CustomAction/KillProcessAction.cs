using System;
using System.Windows.Forms;

namespace CustomActions
{
    public partial class KillProcessAction : GenericAction
    {
        public KillProcessAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 120;
            this.ActionIcon = Properties.Resources.KillProcess;
            this.Text = GetLocalizedString("DescriptionKillProcessAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the name of the process to kill.
        /// </summary>
        /// <exception cref="NullReferenceException">'ProcessName' can not be set to null.</exception>
        public string ProcessName
        {
            get { return this.txtBxProcessName.Text.Trim(); }

            set
            {
                this.txtBxProcessName.Text = value.Trim();
            }
        }

        #endregion Properties

        #region Methods

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'ProcessName' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxProcessName.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            if (String.IsNullOrEmpty(this.ProcessName))
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxProcessName.BackColor = System.Drawing.Color.Orange;
            }
            else
            { this.ConfigurationState = ConfigurationStates.Configured;
            this.txtBxProcessName.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<ProcessName>" + this.ProcessName + "</ProcessName>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("KillProcess") + this.ProcessName + ".";
        }

        #endregion Methods

        #region Events

        private void txtBxProcessName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
