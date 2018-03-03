using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleRegDword : GenericRule
    {
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleRegDword).Assembly);

        public RuleRegDword():base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));

            nupData.Maximum = uint.MaxValue;
            txtBxDescription.Text = resMan.GetString("DescriptionRuleRegDword");
            txtBxSubKey.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531102.aspx";
            AdjustHelpLinkLocation();
        }

        #region {methods - Méthodes}

        private void ValidateData()
        {
            btnOk.Enabled = (!string.IsNullOrEmpty(txtBxSubKey.Text) &&
                txtBxSubKey.Text.Length >= 1 &&
                txtBxSubKey.Text.Length <= 255 &&
                txtBxValue.Text != null &&
                txtBxValue.Text.Length <= 16383 &&
                cmbBxComparison.SelectedIndex != -1);
        }

        internal override string GetRtfFormattedRule()
        {
            RichTextBox rTxtBx = new RichTextBox();

            if (ReverseRule)
            {
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, "<lar:");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "Not");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.green, ">\r\n");
            }

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<bar:");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "RegDword");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Key");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "HKEY_LOCAL_MACHINE");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Subkey");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, SubKey);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");


            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Value");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Value);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");


            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Comparison");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Comparison);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");


            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Data");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Data.ToString());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            if (RegType32)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " RegType32");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, RegType32.ToString().ToLower());
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

            return rTxtBx.Rtf;
        }

        internal override GenericRule Clone()
        {
            RuleRegDword clone = new RuleRegDword();

            clone.SubKey = this.SubKey;
            clone.RegType32 = this.RegType32;
            clone.ReverseRule = this.ReverseRule;
            clone.Value = this.Value;
            clone.Comparison = this.Comparison;
            clone.Data = this.Data;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("RegDword");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Key":
                        break;
                    case "Subkey":
                        this.SubKey = pair.Value;
                        break;
                    case "RegType32":
                        if (pair.Value.ToLower() == "true")
                            this.RegType32 = true;
                        if (pair.Value.ToLower() == "false")
                            this.RegType32 = false;
                        break;
                    case "Value":
                        this.Value = pair.Value;
                        break;
                    case "Comparison":
                        foreach (string item in Enum.GetNames(typeof(ComparisonType)))
                        {
                            if (item.ToLower() == pair.Value.ToLower())
                            {
                                cmbBxComparison.SelectedItem = item;
                                this.Comparison = pair.Value;
                                break;
                            }
                        }
                        break;
                    case "Data":
                        uint data = 0;
                        if (uint.TryParse(pair.Value, out data))
                            this.Data = data;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion {methods - Méthodes}

        #region {Properties - Propriétés}

        /// <summary>
        /// Get or Set the SubKey
        /// </summary>
        internal string SubKey
        {
            get { return txtBxSubKey.Text; }
            set { txtBxSubKey.Text = value; }
        }

        /// <summary>
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        /// <summary>
        /// Get or Set if the Registry key is 32 bit
        /// </summary>
        internal bool RegType32
        {
            get { return chkBxRegType32.Checked; }
            set { chkBxRegType32.Checked = value; }

        }

        /// <summary>
        /// Get or Set the name of the Value key.
        /// </summary>
        internal string Value
        {
            get { return txtBxValue.Text; }
            set { txtBxValue.Text = value; }
        }

        /// <summary>
        /// Get or Set the comarison operator.
        /// </summary>
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
                    cmbBxComparison.SelectedItem = resMan.GetString("Comparison" + value);
                else
                    cmbBxComparison.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Get or Set the data of the Value Key.
        /// </summary>
        internal uint Data
        {
            get { return (uint)nupData.Value; }
            set { nupData.Value = value; }
        }

        internal override string XmlElementName
        {
            get { return "RegDword"; }
        }

        #endregion {Properties - Propriétés}

        #region {Response to Events - Réponses aux évènements}

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void txtBxSubKey_TextChanged(object sender, EventArgs e)
        {
            if (txtBxSubKey.Text.ToLower().StartsWith(@"HKEY_LOCAL_MACHINE\".ToLower()))
            {
                txtBxSubKey.Text = txtBxSubKey.Text.Substring(@"HKEY_LOCAL_MACHINE\".Length);
            }

            if (!string.IsNullOrEmpty(txtBxSubKey.Text) && txtBxSubKey.Text.Length > 255)
                txtBxSubKey.Text = txtBxSubKey.Text.Substring(0, 255);
            ValidateData();
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxValue.Text) && txtBxValue.Text.Length > 16383)
                txtBxValue.Text = txtBxValue.Text.Substring(0, 16383);
            ValidateData();
        }
        
        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion
    }
}
