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
    internal partial class RuleRegKeyExists : GenericRule
    {
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleRegKeyExists).Assembly);

        public RuleRegKeyExists()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            txtBxDescription.Text = resMan.GetString("DescriptionRegKeyExists");
            txtBxSubKey.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb530983.aspx";
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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "RegKeyExists");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Key");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, "HKEY_LOCAL_MACHINE");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Subkey");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, SubKey);
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
            RuleRegKeyExists clone = new RuleRegKeyExists();

            clone.SubKey = this.SubKey;
            clone.RegType32 = this.RegType32;
            clone.ReverseRule = this.ReverseRule;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("RegKeyExists");
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

        internal override string XmlElementName
        {
            get { return "RegKeyExists"; }
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

            btnOk.Enabled = (!string.IsNullOrEmpty(txtBxSubKey.Text) && txtBxSubKey.Text.Length >= 1 && txtBxSubKey.Text.Length <= 255);
        }
        #endregion
    }
}
