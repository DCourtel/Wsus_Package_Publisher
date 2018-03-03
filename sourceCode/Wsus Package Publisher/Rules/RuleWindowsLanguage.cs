using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleWindowsLanguage : GenericRule
    {
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleWindowsVersion).Assembly);

        public RuleWindowsLanguage()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            foreach (KeyValuePair<string, string> pair in Languages.AllLanguagues)
            {
                cmbBxLanguage.Items.Add(pair.Key);
            }
            txtBxDescription.Text = resManager.GetString("DescriptionWindowsLanguage");
            cmbBxLanguage.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531034.aspx";
            AdjustHelpLinkLocation();
        }

        #region (Properties - Propriétés)

        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        internal string Language
        {
            get
            {
                if (cmbBxLanguage.SelectedIndex != -1)
                    return Languages.GetLanguageCode(cmbBxLanguage.SelectedItem.ToString());
                else
                    return string.Empty;
            }
            set
            {
                cmbBxLanguage.SelectedIndex = Languages.GetLanguageIndex(value);
            }
        }

        internal override string XmlElementName
        {
            get { return "WindowsLanguage"; }
        }

        #endregion

        #region (Methods - Méthodes)

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "WindowsLanguage");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Language");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Language);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

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
            RuleWindowsLanguage clone = new RuleWindowsLanguage();

            clone.ReverseRule = this.ReverseRule;
            clone.Language = this.Language;

            return clone;
        }

        public override string ToString()
        {
            return resManager.GetString("WindowsLanguage");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Language":
                        this.Language = pair.Value;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion

        #region (Responses to Events - Réponses aux évènements)

        private void cmbBxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
                btnOk.Enabled = (cmbBxLanguage.SelectedIndex != -1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        #endregion

    }
}
