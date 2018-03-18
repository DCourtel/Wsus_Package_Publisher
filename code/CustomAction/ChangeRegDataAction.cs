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
    public partial class ChangeRegDataAction : GenericAction
    {
        public ChangeRegDataAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 270;
            this.ActionIcon = Properties.Resources.ChangeRegData;
            this.Text = GetLocalizedString("DescriptionChangeRegDataAction");
            this.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
        }

        #region Properties

        /// <summary>
        /// Gets or Sets the registry Hive where to add the Registry key.
        /// </summary>
        public RegistryHelper.RegistryHive Hive
        {
            get
            {
                switch (this.cmbBxHive.SelectedIndex)
                {
                    case 0:
                        return RegistryHelper.RegistryHive.HKey_Local_Machine;
                    case 1:
                        return RegistryHelper.RegistryHive.HKey_Current_User;
                    default:
                        return RegistryHelper.RegistryHive.Undefined;
                }
            }

            set
            {
                switch (value)
                {
                    case RegistryHelper.RegistryHive.HKey_Local_Machine:
                        this.cmbBxHive.SelectedIndex = 0;
                        break;
                    case RegistryHelper.RegistryHive.HKey_Current_User:
                        this.cmbBxHive.SelectedIndex = 1;
                        break;
                    default:
                        this.cmbBxHive.SelectedIndex = -1;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the name of the Registry Key where the value is.
        /// </summary>
        /// <exception cref="NullReferenceException">'RegKey' can not be set to null.</exception>
        public string RegKey
        {
            get { return this.txtBxRegKey.Text.Trim(); }
            set { this.txtBxRegKey.Text = value.Trim(); }
        }

        /// <summary>
        /// Gets or Sets if the 32bit registry is used on a 64bit systems.
        /// </summary>
        public bool UseReg32
        {
            get { return this.chkBxUseReg32.Checked; }
            set { this.chkBxUseReg32.Checked = value; }
        }

        /// <summary>
        /// Gets or Sets the Registry Value where the data to change is.
        /// </summary>
        /// <exception cref="NullReferenceException">'RegKey' can not be set to null.</exception>
        public string RegValue
        {
            get { return this.txtBxValue.Text.Trim(); }
            set { this.txtBxValue.Text = value.Trim(); }
        }

        /// <summary>
        /// Gets or Sets if the data need to be write to the default value.
        /// </summary>
        public bool DefaultValue
        {
            get { return this.chkBxDefaultValue.Checked; }
            set { this.chkBxDefaultValue.Checked = value; }
        }

        /// <summary>
        /// Gets or Sets the new data to set.
        /// </summary>
        /// <exception cref="NullReferenceException">'RegKey' can not be set to null.</exception>
        public string NewData
        {
            get { return this.txtBxNewData.Text.Trim(); }
            set { this.txtBxNewData.Text = value.Trim(); }
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
            this.txtBxRegKey.Focus();
        }

        /// <summary>
        /// Align the configuration State of this Action accordingly to the Data.
        /// </summary>
        private void ValidateData()
        {
            bool regKeyOK = !String.IsNullOrEmpty(this.RegKey) && !this.RegKey.EndsWith(@"\");
            bool regValueOK = !String.IsNullOrEmpty(this.RegValue) || this.chkBxDefaultValue.Checked;
            bool newDataOK = !String.IsNullOrEmpty(this.NewData);

            this.txtBxRegKey.BackColor = regKeyOK ? SystemColors.Window : Color.Orange;
            this.txtBxValue.BackColor = regValueOK ? SystemColors.Window : Color.Orange;
            this.txtBxNewData.BackColor = newDataOK ? SystemColors.Window : Color.Orange;

            if (regKeyOK && regValueOK && newDataOK && this.cmbBxHive.SelectedItem != null && this.cmbBxHive.SelectedIndex != -1)
                this.ConfigurationState = ConfigurationStates.Configured;
            else
                this.ConfigurationState = ConfigurationStates.Misconfigured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<Hive>" + this.Hive.ToString() + "</Hive>\r\n" + 
            "<RegKey>" + this.RegKey + "</RegKey>\r\n" + 
            "<RegValue>" + this.RegValue + "</RegValue>\r\n" + 
            "<DefaultValue>" + this.DefaultValue.ToString() + "</DefaultValue>\r\n" + 
            "<NewData>" + this.NewData + "</NewData>\r\n" +
            "<UseReg32>" + this.UseReg32.ToString() + "</UseReg32></Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("ChangeRegData") + this.Hive.ToString() + "\\" + this.RegKey + "\\" + this.RegValue + this.GetLocalizedString("WithTheData") + this.NewData;
        }

        #endregion Methods

        #region Events

        private void txtBxRegKey_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            RegistryHelper.RegKey cleanRegKey = RegistryHelper.GetCleanRegKey(this.txtBxRegKey.Text, this.Hive, this.UseReg32);

            this.txtBxRegKey.Text = cleanRegKey.RegKeyName;
            this.Hive = cleanRegKey.RegHive;
            if (this.cmbBxHive.SelectedIndex == 0 && this.txtBxRegKey.Text.StartsWith(@"SOFTWARE\Wow6432Node", StringComparison.InvariantCultureIgnoreCase))
            { this.UseReg32 = true; }
            this.ValidateData();
        }

        private void chkBxUseReg32_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            RegistryHelper.RegKey cleanRegKey = RegistryHelper.GetCleanRegKey(this.txtBxRegKey.Text, this.Hive, this.UseReg32);

            this.txtBxRegKey.Text = cleanRegKey.RegKeyName;
            this.ValidateData();
        }

        private void cmbBxHive_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkBxUseReg32.Visible = this.cmbBxHive.SelectedIndex == 0;
            if (this.cmbBxHive.SelectedIndex == 0)
            { this.chkBxUseReg32.Checked = false; }
            this.txtBxRegKey_TextChanged(null, null);
            this.OnChange(null);
            this.ValidateData();
            base.UpdateHiveNotificationStatus(this.Hive);
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxNewData_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void chkBxDefaultValue_CheckedChanged(object sender, EventArgs e)
        {
            this.txtBxValue.Enabled = !this.chkBxDefaultValue.Checked;
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
