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
    public partial class UninstallMsiProductByGuidAction : GenericAction
    {
        private const string allowedCharacters = "ABCDEFabcdef0123456789-;%_";
        private const string remoteMsiManager = "RemoteMsiManager.exe";

        public UninstallMsiProductByGuidAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 450;
            this.ActionIcon = Properties.Resources.MsiInstallerElement;
            this.Text = GetLocalizedString("DescriptionUinstallMsiProductByGuid");
        }

        #region Properties

        public string MsiProductCodes
        {
            get { return this.txtBxProductCode.Text; }
            set { this.txtBxProductCode.Text = value; }
        }

        public string Exceptions
        {
            get { return this.txtBxExceptions.Text; }
            set { this.txtBxExceptions.Text = value; }
        }

        public bool DontUninstallIfNoException
        {
            get { return this.chkBxDontUninstallIfNoException.Checked; }
            set { this.chkBxDontUninstallIfNoException.Checked = value; }
        }

        public bool KillProcess
        {
            get { return this.chkBxKillProcess.Checked; }
            set { this.chkBxKillProcess.Checked = value; }
        }

        public int KillAfter
        {
            get { return (int)this.nupKillProcess.Value; }
            set { this.nupKillProcess.Value = value; }
        }

        #endregion Properties

        #region Methods

        private string RemoveUnvantedCharacters(string text)
        {
            try
            {
                if (!String.IsNullOrEmpty(text))
                {
                    int index = 0;
                    do
                    {
                        if (!allowedCharacters.Contains(text[index]))
                        {
                            text = text.Remove(index, 1);
                        }
                        index++;
                    } while (text.Length > index);

                    return text;
                }
            }
            catch (Exception) { }

            return String.Empty;
        }

        protected override void StartDragDropOperation()
        {
            DoDragDrop(new CustomActions.DragDropInfo(this), DragDropEffects.Move);
        }

        /// <summary>
        /// When the control expand, set the focus on the 'Registry Key' textbox.
        /// </summary>
        protected override void OnExpand()
        {
            this.txtBxProductCode.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool productCodeConfigured = !string.IsNullOrWhiteSpace(txtBxProductCode.Text);
            bool exceptionConfigured = (!this.chkBxDontUninstallIfNoException.Checked || !String.IsNullOrWhiteSpace(this.txtBxExceptions.Text));

            this.ConfigurationState = (productCodeConfigured && exceptionConfigured) ? ConfigurationStates.Configured : ConfigurationStates.Misconfigured;

            this.txtBxProductCode.BackColor = productCodeConfigured ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
            this.txtBxExceptions.BackColor = exceptionConfigured ? System.Drawing.SystemColors.Window : System.Drawing.Color.Orange;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<MsiProductCodes>" + this.MsiProductCodes + "</MsiProductCodes>\r\n<Exceptions>" + this.txtBxExceptions.Text + "</Exceptions>\r\n" +
                "<DontUninstallIfNoException>" + this.DontUninstallIfNoException.ToString() + "</DontUninstallIfNoException>\r\n<KillProcess>" +
                this.KillProcess.ToString() + "</KillProcess>\r\n<KillAfter>" + this.nupKillProcess.Value.ToString() + "</KillAfter>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return String.Empty;
        }

        #endregion Methods

        #region Events

        private void txtBxProductCode_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.txtBxProductCode.SuspendLayout();
            int carretPosition = this.txtBxProductCode.SelectionStart;
            int textLength = this.txtBxProductCode.TextLength;
            this.txtBxProductCode.Text = this.RemoveUnvantedCharacters(this.txtBxProductCode.Text);
            carretPosition -= textLength - this.txtBxProductCode.TextLength;
            this.txtBxProductCode.SelectionStart = System.Math.Max(0, System.Math.Min(carretPosition, this.txtBxProductCode.TextLength));
            this.txtBxProductCode.ResumeLayout();

            this.ValidateData();
        }

        private void txtBxExceptions_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.txtBxExceptions.SuspendLayout();
            int carretPosition = this.txtBxExceptions.SelectionStart;
            int textLength = this.txtBxExceptions.TextLength;
            this.txtBxExceptions.Text = this.RemoveUnvantedCharacters(this.txtBxExceptions.Text);
            carretPosition -= textLength - this.txtBxExceptions.TextLength;
            this.txtBxExceptions.SelectionStart = System.Math.Max(0, System.Math.Min(carretPosition, this.txtBxExceptions.TextLength));
            this.txtBxExceptions.ResumeLayout();

            this.ValidateData();
        }

        private void chkBxDontUninstallIfNoException_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void nupKillProcess_ValueChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
        }

        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.nupKillProcess.Enabled = this.chkBxKillProcess.Checked;
        }

        private void btnLaunchRemoteMsiManager_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(remoteMsiManager))
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(remoteMsiManager, this.txtBxProductCode.Text + this.txtBxExceptions.Text);
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    procInfo.Arguments = (this.MsiProductCodes == String.Empty ? "-I=%" : "-I=" + this.MsiProductCodes) + (this.Exceptions == String.Empty ? String.Empty : " -X=" + this.Exceptions);
                    p.StartInfo = procInfo;
                    p.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Can't find " + remoteMsiManager);
        }

        #endregion Events
    }
}
