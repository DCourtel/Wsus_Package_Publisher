using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleRegSzToVersion : GenericRule
    {
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleRegSzToVersion).Assembly);

        public RuleRegSzToVersion():base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));

            txtBxDescription.Text = resMan.GetString("DescriptionRuleRegSzToVersion");
            txtBxSubKey.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb530968.aspx";
            AdjustHelpLinkLocation();
        }

        #region {methods - Méthodes}

        private bool ValidateData()
        {
            return (!string.IsNullOrEmpty(txtBxSubKey.Text) &&
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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "RegSzToVersion");
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
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Data);
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
            RuleRegSzToVersion clone = new RuleRegSzToVersion();

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
            return resMan.GetString("RegSzToVersion");
        }

        /// <summary>
        /// Determines whether or not the string passed in parameters is compliant with the RegExp : "^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$}"
        /// </summary>
        /// <param name="version">The string to check against Regexp.</param>
        /// <returns>True if the string match, else false.</returns>
        private bool IsVersionStringCorrectlyformated(string version)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,5}.\d{1,5}.\d{1,5}.\d{1,5}$");

            if (regex.IsMatch(version))
                return true;
            return false;
        }

        /// <summary>
        /// Return a part of the string, corresponding of a sub-version number. Eg : For version = "2.12.0.202" and rank = 3, return "202"
        /// </summary>
        /// <param name="version">The full version string.</param>
        /// <param name="rank">Determine witch sub-version number to return (starting at 0).</param>
        /// <returns>Return a Integer corresponding to the sub-version number</returns>
        private int GetVersionNumber(string version, int rank)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\d{1,5}");
            string number;
            int result;

            number = (regex.Matches(version)[rank]).ToString();
            if (int.TryParse(number, out result))
                return result;
            else
                return 0;
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
                        this.Data = pair.Value;
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
        internal string Data
        {
            get { return nupVersion1.Value + "." + nupVersion2.Value + "." + nupVersion3.Value + "." + nupVersion4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformated(value))
                {
                    nupVersion1.Value = GetVersionNumber(value, 0);
                    nupVersion2.Value = GetVersionNumber(value, 1);
                    nupVersion3.Value = GetVersionNumber(value, 2);
                    nupVersion4.Value = GetVersionNumber(value, 3);
                }
            }
        }

        internal override string XmlElementName
        {
            get { return "RegSzToVersion"; }
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

            btnOk.Enabled = ValidateData();
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = ValidateData();
        }

        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = ValidateData();
        }

        private void nupVersion1_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, 5);
        }

        #endregion

    }
}
