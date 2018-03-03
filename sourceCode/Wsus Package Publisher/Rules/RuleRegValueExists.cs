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
    public partial class RuleRegValueExists : GenericRule
    {
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleRegValueExists).Assembly);
        internal enum RegistryValueType
        {
            NotSpecify,
            REG_BINARY,
            REG_DWORD,
            REG_EXPAND_SZ,
            REG_SZ,
        }

        public RuleRegValueExists():base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            foreach (RegistryValueType valueType in Enum.GetValues(typeof(RegistryValueType)))
            {
                cmbBxType.Items.Add(valueType);
            }

            txtBxDescription.Text = resMan.GetString("DescriptionRegValueExists");
            txtBxSubKey.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531094.aspx";
            AdjustHelpLinkLocation();
        }
        
        #region {methods - Méthodes}

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "RegValueExists");
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

            if (SpecifyType)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Type");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ValueType.ToString());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

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
            RuleRegValueExists clone = new RuleRegValueExists();

            clone.SubKey = this.SubKey;
            clone.RegType32 = this.RegType32;
            clone.ReverseRule = this.ReverseRule;
            clone.Value = this.Value;
            clone.SpecifyType = this.SpecifyType;
            clone.ValueType = this.ValueType;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("RegValueExists");
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
                    case "Type":
                        this.SpecifyType = true;
                        this.ValueType = GetValueType(pair.Value);
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        private RegistryValueType GetValueType(string p)
        {
            foreach (RegistryValueType type in Enum.GetValues(typeof(RegistryValueType)))
            {
                if (type.ToString() == p)
                    return type;
            }
            return RegistryValueType.NotSpecify;
        }

        private void ValidateData()
        {
            btnOk.Enabled = (!string.IsNullOrEmpty(txtBxSubKey.Text) && 
                txtBxSubKey.Text.Length >= 1 && 
                txtBxSubKey.Text.Length <= 255 && 
                !string.IsNullOrEmpty(txtBxValue.Text) && 
                (!chkBxSpecifyType.Checked || (chkBxSpecifyType.Checked && cmbBxType.SelectedIndex != -1)));
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
        /// Get or Set the registry value to check.
        /// </summary>
        internal string Value
        {
            get { return txtBxValue.Text; }
            set { txtBxValue.Text = value; }
        }

        /// <summary>
        /// Get or Set if the Type is specify.
        /// </summary>
        internal bool SpecifyType
        {
            get { return chkBxSpecifyType.Checked; }
            set { chkBxSpecifyType.Checked = value; }
        }

        /// <summary>
        /// Get or Set the type of the RegValue.
        /// </summary>
        internal RegistryValueType ValueType
        {
            get
            {
                if (SpecifyType)
                    return (RegistryValueType)cmbBxType.SelectedItem;
                else
                    return RegistryValueType.NotSpecify;
            }
            set
            {
                if (value == RegistryValueType.NotSpecify)
                {
                    SpecifyType = false;
                    cmbBxType.SelectedIndex = -1;
                }
                else
                {
                    SpecifyType = true;
                    cmbBxType.SelectedItem = value;
                }
            }
        }

        internal override string XmlElementName
        {
            get { return "RegValueExists"; }
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

            ValidateData();
        }

        private void chkBxSpecifyType_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxType.Enabled = chkBxSpecifyType.Checked;
            ValidateData();
        }

        private void cmbBxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxType.SelectedIndex != -1 && cmbBxType.SelectedItem.ToString() == RegistryValueType.NotSpecify.ToString())
            {
                chkBxSpecifyType.Checked = false;
                cmbBxType.SelectedIndex = -1;
            }
            ValidateData();
        }

        #endregion
    }
}
