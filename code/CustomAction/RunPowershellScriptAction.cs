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
    public partial class RunPowershellScriptAction : GenericAction, IStoreReturnCodeToVariable
    {
        public RunPowershellScriptAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 200;
            this.ActionIcon = Properties.Resources.PowershellElement;
            this.Text = GetLocalizedString("DescriptionRunPowershellScriptAction");
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the full path to the VbScript.
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
                    if (value == null)
                        throw new NullReferenceException("This property can not be set to null.");
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

            _result += "<FullPath>" + this.FullPath +
                "</FullPath>\r\n<Parameters>" + this.Parameters +
                "</Parameters>\r\n<KillProcess>" + this.KillProcess.ToString() +
                "</KillProcess>\r\n<DelayBeforeKilling>" + this.DelayBeforeKilling.ToString() +
                "</DelayBeforeKilling>\r\n<StoreToVariable>" + this.chkBxStoreToVariable.Checked.ToString() + "</StoreToVariable>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            string result = string.Empty;

            result = this.GetLocalizedString("ExecuteThePowershellScript") + this.FullPath;
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

        private void txtBxFullPath_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
            this.UpdateUserProfileNotificationStatus(this.txtBxFullPath.Text);
        }

        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.nupKillProcess.Enabled = this.chkBxKillProcess.Checked;
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

        private void lnkEnvironmentVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmEnvironmentVariables frmEnvironmentVariables = new FrmEnvironmentVariables();
            frmEnvironmentVariables.ShowDialog();
        }

        #endregion Events
    }
}
