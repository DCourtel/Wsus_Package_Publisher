using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CustomActions
{
    public partial class ExecutableAction : GenericAction, IStoreReturnCodeToVariable
    {
        public ExecutableAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 200;
            this.ActionIcon = Properties.Resources.ExecutableElement;
            this.Text = GetLocalizedString("DescriptionExecutableAction");
            this.HelpMessage = "This is the help text for 'ExecutableAction'.";
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the file where live the file to run. (Ex. C:\Program Files\Internet Explorer\iexplore.exe)
        /// </summary>
        public string PathToTheFile
        {
            get { return this.txtBxPath.Text.Trim(); }

            set
            {
                    this.txtBxPath.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets parameters associate to the executed file.
        /// </summary>
        /// <exception cref="NullReferenceException">'Parameters' can not be set to null.</exception>
        public string Parameters
        {
            get { return txtBxParameters.Text.Trim(); }

            set 
            {
                if (value == null)
                {
                    throw new NullReferenceException("This property ca not be set to null.");
                }
                else
                {
                    txtBxParameters.Text = value.Trim();
                }
            }
        }

        /// <summary>
        /// Define if the process must be kill if running more than the time define by 'DelayBeforeKilling' property.
        /// </summary>
        public bool KillProcess
        {
            get { return chkBxKillProcess.Checked; }
            set { chkBxKillProcess.Checked = value; }
        }

        /// <summary>
        /// Define the time to wait before killing the process.
        /// </summary>
        public int DelayBeforeKilling
        {
            get { return (int)nupKillProcess.Value; }
            set
            {
                if (value >= (int)nupKillProcess.Minimum && value <= (int)nupKillProcess.Maximum)
                    nupKillProcess.Value = value;
            }
        }

        /// <summary>
        /// Gets or Sets if the return code should be stored in a variable.
        /// </summary>
        public bool StoreToVariable
        {
            get { return this.chkBxStoreToVariable.Checked; }
            set { this.chkBxStoreToVariable.Checked = value; }
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
            this.txtBxPath.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            if (String.IsNullOrEmpty(this.PathToTheFile) || this.PathToTheFile.EndsWith(@"\"))
            { this.ConfigurationState = ConfigurationStates.Misconfigured;
            this.txtBxPath.BackColor = System.Drawing.Color.Orange;
            }
            else
            { this.ConfigurationState = ConfigurationStates.Configured;
            this.txtBxPath.BackColor = System.Drawing.SystemColors.Window;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<PathToTheFile>" + this.PathToTheFile +
                "</PathToTheFile>\r\n<Parameters>" + this.Parameters +
                "</Parameters>\r\n<KillProcess>" + this.KillProcess.ToString() +
                "</KillProcess>\r\n<DelayBeforeKilling>" + this.DelayBeforeKilling.ToString() +
                "</DelayBeforeKilling>\r\n<StoreToVariable>" + this.chkBxStoreToVariable.Checked.ToString() + "</StoreToVariable>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            string result = string.Empty;

            result = this.GetLocalizedString("ExecuteTheFile") + this.PathToTheFile;
            if (!String.IsNullOrEmpty(this.Parameters))
                result += " " + this.GetLocalizedString("WithParameters") + this.Parameters;
            if (this.KillProcess)
                result += " " + this.GetLocalizedString("AndKillProcessIfItRunMoreThan") + this.DelayBeforeKilling.ToString() + " " + this.GetLocalizedString("Minutes");
            if (this.StoreToVariable)
                result += " " + this.GetLocalizedString("AndStoreReturnCodeToVariable");

            return result;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Enable the 'KillProcess' NumericUpDown control accordingly to the state of the 'KillProcess' checkbox.
        /// </summary>
        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            nupKillProcess.Enabled = chkBxKillProcess.Checked;
            this.ValidateData();
        }

        /// <summary>
        /// Validate data each time text change.
        /// </summary>
        private void txtBxPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            this.UpdateUserProfileNotificationStatus(this.txtBxPath.Text);
        }

        private void chkBxStoreToVariable_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void nupKillProcess_ValueChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxParameters_TextChanged(object sender, EventArgs e)
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
