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
    public partial class RenameRegValueAction : GenericAction
    {
        public RenameRegValueAction():base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 280;
            this.ActionIcon = Properties.Resources.RenameRegistryValue;
            this.Text = GetLocalizedString("DescriptionRenameRegValueAction");
            this.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
        }
        
        #region Properties

        /// <summary>
        /// Gets or Sets the registry Hive where to rename the Registry value.
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
        /// Gets or Sets the name of the Registry Key where to rename the value.
        /// </summary>
        /// <exception cref="NullReferenceException">'RegKey' can not be set to null.</exception>
        public string RegKey
        {
            get { return this.txtBxRegKey.Text.Trim(); }

            set
            {
                this.txtBxRegKey.Text = value.Trim();
            }
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
        /// Gets or Sets the name of the value to rename.
        /// </summary>
        /// <exception cref="NullReferenceException">'ValueName' can not be set to null.</exception>
        public string ValueName
        {
            get { return this.txtBxValueName.Text.Trim(); }

            set
            {
                this.txtBxValueName.Text = value.Trim();
            }
        }

        /// <summary>
        /// Gets or Sets the new name of the value.
        /// </summary>
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
            bool valueNameOK = !String.IsNullOrEmpty(this.ValueName);
            bool newNameOK = !String.IsNullOrEmpty(this.NewName);

            this.txtBxRegKey.BackColor = regKeyOK ? SystemColors.Window : Color.Orange;
            this.txtBxValueName.BackColor = valueNameOK ? SystemColors.Window : Color.Orange;
            this.txtBxNewName.BackColor = newNameOK ? SystemColors.Window : Color.Orange;


            if (!regKeyOK || !valueNameOK || !newNameOK || this.cmbBxHive.SelectedItem == null || this.cmbBxHive.SelectedIndex == -1)
                this.ConfigurationState = ConfigurationStates.Misconfigured;
            else
                this.ConfigurationState = ConfigurationStates.Configured;
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns></returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<Hive>" + this.Hive.ToString() + "</Hive>\r\n" + "<RegKey>" + this.RegKey + "</RegKey>\r\n<UseReg32>" + this.UseReg32.ToString() + "</UseReg32>\r\n" +
                "<ValueName>" + this.ValueName + "</ValueName>\r\n<NewName>" + this.NewName + "</NewName>\r\n</Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("RenameThisRegValue") + this.ValueName + this.GetLocalizedString("Into") + this.NewName + this.GetLocalizedString("In") + this.Hive + "\\" + this.RegKey;
        }

        #endregion Methods

        #region Events

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

        private void txtBxValueName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        private void txtBxNewName_TextChanged(object sender, EventArgs e)
        {
            this.OnChange(null);
            this.ValidateData();
        }

        #endregion Events
    }
}
