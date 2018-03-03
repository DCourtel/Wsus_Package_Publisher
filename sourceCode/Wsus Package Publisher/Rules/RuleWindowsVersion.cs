using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Wsus_Package_Publisher
{
    internal partial class RuleWindowsVersion : GenericRule
    {
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleWindowsVersion).Assembly);

        public RuleWindowsVersion()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            txtBxDescription.Text = resManager.GetString("DescriptionRuleWindowsVersion");

            cmbBxComparison.Items.Add(resManager.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resManager.GetString("ComparisonGreaterThan"));

            chkBxComparison.Select();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531008.aspx";
            AdjustHelpLinkLocation();
        }

        #region (Properties - Propriétés)

        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        internal bool UseComparison
        {
            get { return chkBxComparison.Checked; }
            set { chkBxComparison.Checked = value; }
        }

        internal string Comparison
        {
            get
            {
                if (cmbBxComparison.SelectedIndex != -1)
                    return Enum.GetNames(typeof(ComparisonType))[cmbBxComparison.SelectedIndex];
                else
                    return "";
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && Enum.GetNames(typeof(ComparisonType)).Contains(value))
                {
                    cmbBxComparison.SelectedItem = resManager.GetString("Comparison" + value);
                    UseComparison = true;
                }
                else
                {
                    UseComparison = false;
                    cmbBxComparison.SelectedIndex = -1;
                }
            }
        }

        internal bool UseMajorVersion
        {
            get { return chkBxMajorVersion.Checked; }
            set { chkBxMajorVersion.Checked = value; }
        }

        internal uint MajorVersion
        {
            get { return (uint)nupMajorVersion.Value; }
            set
            {
                nupMajorVersion.Value = value;
                UseMajorVersion = true;
            }
        }

        internal bool UseMinorVersion
        {
            get { return chkBxMinorVersion.Checked; }
            set { chkBxMinorVersion.Checked = value; }
        }

        internal uint MinorVersion
        {
            get { return (uint)nupMinorVersion.Value; }
            set
            {
                nupMinorVersion.Value = value;
                UseMinorVersion = true;
            }
        }

        internal bool UseBuildNumber
        {
            get { return chkBxBuildNumber.Checked; }
            set { chkBxBuildNumber.Checked = value; }
        }

        internal uint BuildNumber
        {
            get { return (uint)nupBuildNumber.Value; }
            set
            {
                nupBuildNumber.Value = value;
                UseBuildNumber = true;
            }
        }

        internal bool UseServicePackMajor
        {
            get { return chkBxServicePackMajor.Checked; }
            set { chkBxServicePackMajor.Checked = value; }
        }

        internal ushort ServicePackMajor
        {
            get { return (ushort)nupServicePackMajor.Value; }
            set
            {
                nupServicePackMajor.Value = value;
                UseServicePackMajor = true;
            }
        }

        internal bool UseServicePackMinor
        {
            get { return chkBxServicePackMinor.Checked; }
            set { chkBxServicePackMinor.Checked = value; }
        }

        internal ushort ServicePackMinor
        {
            get { return (ushort)nupServicePackMinor.Value; }
            set
            {
                nupServicePackMinor.Value = value;
                UseServicePackMinor = true;
            }
        }

        internal bool UseProductType
        {
            get { return chkBxProductType.Checked; }
            set { chkBxProductType.Checked = value; }
        }

        internal ushort ProductType
        {
            // Product Type :
            // DC = 2
            // Server = 3
            // WorkStation = 1
            // Index :
            // 0 = WorkStation
            // 1 = Server
            // 2 = Domain Controller
            get
            {
                switch (cmbBxProductType.SelectedIndex)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 3;
                    case 2:
                        return 2;
                    default:
                        return 255;
                }
            }

            set
            {
                switch (value)
                {
                    case 1:
                        cmbBxProductType.SelectedIndex = 0;
                        break;
                    case 2:
                        cmbBxProductType.SelectedIndex = 2;
                        break;
                    case 3:
                        cmbBxProductType.SelectedIndex = 1;
                        break;
                    default:
                        UseProductType = false;
                        break;
                }
                UseProductType = true;
            }
        }

        internal override string XmlElementName
        {
            get { return "WindowsVersion"; }
        }

        #endregion

        #region(Responses to events - Réponses aux événements)

        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseComparison = true;
        }

        private void cmbBxOperatingSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBxOperatingSystem.SelectedIndex)
            {
                case 0:
                    SelectOS(6, 3, 0); // Windows 8.1
                    break;
                case 1:
                    SelectOS(6, 3, 1); // Windows Server 2012R2
                    break;
                case 2:
                    SelectOS(6, 2, 0); // Windows 8
                    break;
                case 3:
                    SelectOS(6, 2, 1); // Windows Server 2012
                    break;
                case 4:
                    SelectOS(6, 1, 0); // Windows 7
                    break;
                case 5:
                    SelectOS(6, 1, 1); // Windows Server 2008R2
                    break;
                case 6:
                    SelectOS(6, 0, 0); // Windows Vista
                    break;
                case 7:
                    SelectOS(6, 0, 1); // Windows Server 2008
                    break;
                case 8:
                    SelectOS(5, 2, 1); // Windows Server 2003R1 & R2
                    break;
                case 9:
                    SelectOS(5, 1, 0); // Windows XP
                    break;
                case 10:
                    SelectOS(5, 0, 0); // Windows 2000
                    break;
                default:
                    break;
            }
        }

        private void chkBxMajorVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupMajorVersion.Enabled = chkBxMajorVersion.Checked;
            ValidateData();
        }

        private void chkBxMinorVersion_CheckedChanged(object sender, EventArgs e)
        {
            nupMinorVersion.Enabled = chkBxMinorVersion.Checked;
        }

        private void chkBxBuildNumber_CheckedChanged(object sender, EventArgs e)
        {
            nupBuildNumber.Enabled = chkBxBuildNumber.Checked;
        }

        private void chkBxServicePackMajor_CheckedChanged(object sender, EventArgs e)
        {
            nupServicePackMajor.Enabled = chkBxServicePackMajor.Checked;
        }

        private void chkBxSericePackMinor_CheckedChanged(object sender, EventArgs e)
        {
            nupServicePackMinor.Enabled = chkBxServicePackMinor.Checked;
        }

        private void chkBxProductType_CheckedChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void chkBxComparison_CheckedChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UseProductType = true;
            ValidateData();
        }

        private void nupMajorVersion_Enter(object sender, EventArgs e)
        {
            NumericUpDown nup = (NumericUpDown)sender;
            nup.Select(0, 5);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region(Methods - Méthodes)

        private void SelectOS(int majorVersion, int minorVersion, int productType)
        {
            nupMajorVersion.Value = majorVersion;
            UseMajorVersion = true;
            nupMinorVersion.Value = minorVersion;
            UseMinorVersion = true;
            cmbBxProductType.SelectedIndex = productType;
            UseProductType = true;
            ValidateData();
        }

        private void ValidateData()
        {
            if ((!chkBxComparison.Checked || (chkBxComparison.Checked && cmbBxComparison.SelectedIndex != -1)) && (!chkBxProductType.Checked || (chkBxProductType.Checked && cmbBxProductType.SelectedIndex != -1)))
                if (UseMajorVersion || UseProductType)
                    btnOk.Enabled = true;
                else
                    btnOk.Enabled = false;
        }

        internal override string GetRtfFormattedRule()
        {
            RichTextBox rTxtBx = new RichTextBox();

            if (UseMajorVersion || UseProductType)
            {
                if (ReverseRule)
                {
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">\r\n");
                }

                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<bar:");
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "WindowsVersion");

                if (UseComparison)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Comparison");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Comparison);
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseMajorVersion)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " MajorVersion");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, MajorVersion.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseMinorVersion)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " MinorVersion");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, MinorVersion.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseBuildNumber)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " BuildNumber");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, BuildNumber.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseServicePackMajor)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " ServicePackMajor");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ServicePackMajor.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseServicePackMinor)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " ServicePackMinor");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ServicePackMinor.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                if (UseProductType)
                {
                    print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " ProductType");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ProductType.ToString());
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
                }

                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "/>");

                if (ReverseRule)
                {
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\r\n");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "</lar:");
                    print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                    print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">");
                }
            }
            return rTxtBx.Rtf;
        }

        internal override GenericRule Clone()
        {
            RuleWindowsVersion clone = new RuleWindowsVersion();

            clone.ReverseRule = this.ReverseRule;
            clone.UseComparison = this.UseComparison;
            if (UseComparison)
                clone.Comparison = this.Comparison;
            clone.UseMajorVersion = this.UseMajorVersion;
            if (UseMajorVersion)
                clone.MajorVersion = this.MajorVersion;
            clone.UseMinorVersion = this.UseMinorVersion;
            if (UseMinorVersion)
                clone.MinorVersion = this.MinorVersion;
            clone.UseBuildNumber = this.UseBuildNumber;
            if (UseBuildNumber)
                clone.BuildNumber = this.BuildNumber;
            clone.UseServicePackMajor = this.UseServicePackMajor;
            if (UseServicePackMajor)
                clone.ServicePackMajor = this.ServicePackMajor;
            clone.UseServicePackMinor = this.UseServicePackMinor;
            if (UseServicePackMinor)
                clone.ServicePackMinor = this.ServicePackMinor;
            clone.UseProductType = this.UseProductType;
            if (UseProductType)
                clone.ProductType = this.ProductType;

            return clone;
        }

        public override string ToString()
        {
            return resManager.GetString("WindowsVersion");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Comparison":
                        this.Comparison = pair.Value;
                        break;
                    case "MajorVersion":
                        uint result = 0;
                        if (uint.TryParse(pair.Value, out result))
                            this.MajorVersion = result;
                        break;
                    case "MinorVersion":
                        uint result2 = 0;
                        if (uint.TryParse(pair.Value, out result2))
                            this.MinorVersion = result2;
                        break;
                    case "BuildNumber":
                        uint result3 = 0;
                        if (uint.TryParse(pair.Value, out result3))
                            this.BuildNumber = result3;
                        break;
                    case "ServicePackMajor":
                        ushort result4 = 0;
                        if (ushort.TryParse(pair.Value, out result4))
                            this.ServicePackMajor = result4;
                        break;
                    case "ServicePackMinor":
                        ushort result5 = 0;
                        if (ushort.TryParse(pair.Value, out result5))
                            this.ServicePackMinor = result5;
                        break;
                    case "ProductType":
                        ushort result6 = 0;
                        if (ushort.TryParse(pair.Value, out result6))
                            this.ProductType = result6;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion

    }
}
