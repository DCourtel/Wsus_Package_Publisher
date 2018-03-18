using System;
using System.Windows.Forms;
using System.Xml;


namespace CustomActions
{
    public partial class AddRegKeyAction : GenericAction
    {
        public AddRegKeyAction()
            : base()
        {
            InitializeComponent();
            this.ExpandedHeigth = 170;
            this.ActionIcon = Properties.Resources.AddRegistryKeyElement;
            this.Text = GetLocalizedString("DescriptionAddRegKeyAction");
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
        /// Gets or Sets the name of the Registry Key to Add.
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
            if (!String.IsNullOrEmpty(this.RegKey) && !this.RegKey.EndsWith(@"\") && this.cmbBxHive.SelectedItem != null && this.cmbBxHive.SelectedIndex != -1)
            {
                this.ConfigurationState = ConfigurationStates.Configured;
                this.txtBxRegKey.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                this.ConfigurationState = ConfigurationStates.Misconfigured;
                this.txtBxRegKey.BackColor = System.Drawing.Color.Orange;
            }
        }

        /// <summary>
        /// Get a XML formatted string corresponding to this Action.
        /// </summary>
        /// <returns>A XML formatted string corresponding to this Action.</returns>
        public override string GetXMLAction()
        {
            string _result = base.GetXMLAction();

            _result += "<Hive>" + this.Hive.ToString() + "</Hive>\r\n<RegKey>" + this.RegKey + "</RegKey>\r\n<UseReg32>" + this.UseReg32.ToString() + "</UseReg32></Action>";

            return _result;
        }

        protected override string GetConfiguratedDescription()
        {
            return this.GetLocalizedString("AddThisRegKey") + this.Hive.ToString() + "\\" + this.RegKey;
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

        #endregion Events
    }
}
