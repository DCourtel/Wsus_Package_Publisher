using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    public partial class RuleWmiQuery : GenericRule
    {
        FrmBrowseWmiNamespaces frmBrowseWmi = new FrmBrowseWmiNamespaces();
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleWmiQuery).Assembly);

        public RuleWmiQuery()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            System.Threading.Thread thread = new System.Threading.Thread(frmBrowseWmi.ListWmiNamespace);
            thread.Start();
            
            txtBxDescription.Text = resMan.GetString("DescriptionRuleWmiQuery");
            txtBxNamespace.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531017.aspx";
            AdjustHelpLinkLocation();
        }

        #region {methods - Méthodes}

        private bool ValidateData()
        {
            return (!string.IsNullOrEmpty(txtBxWqlquery.Text));
        }

        private string TextToXml(string text)
        {
            Dictionary<string, string> specialCharacters = new Dictionary<string, string>();
            specialCharacters.Add("\"", "&quot;");
            specialCharacters.Add("'", "&apos;");
            specialCharacters.Add("<", "&lt;");
            specialCharacters.Add(">", "&gt;");

            text = text.Replace("&", "&amp;");

            foreach (KeyValuePair<string,string> pair in specialCharacters)
            {
                text = text.Replace(pair.Key, pair.Value);
            }
            return text;
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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "WmiQuery");

            if (!string.IsNullOrEmpty(Namespace))
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Namespace");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Namespace);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " WqlQuery");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, TextToXml(WqlQuery));
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
            RuleWmiQuery clone = new RuleWmiQuery();

            clone.Namespace = this.Namespace;
            clone.WqlQuery = this.WqlQuery;
            clone.ReverseRule = this.ReverseRule;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("WmiQuery");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Namespace":
                        this.Namespace = pair.Value;
                        break;
                    case "WqlQuery":
                        this.WqlQuery = pair.Value;
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
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        /// <summary>
        /// Get or Set the namespace of the query.
        /// </summary>
        internal string Namespace
        {
            get { return txtBxNamespace.Text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    txtBxNamespace.Text = value;
            }
        }

        /// <summary>
        /// Get or Set the Wql Query.
        /// </summary>
        internal string WqlQuery
        {
            get { return txtBxWqlquery.Text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    txtBxWqlquery.Text = value;
            }
        }

        internal override string XmlElementName
        {
            get { return "WmiQuery"; }
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
        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = ValidateData();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (frmBrowseWmi.ShowDialog() == DialogResult.OK)
                txtBxNamespace.Text = frmBrowseWmi.SelectedNamespace;
        }

        #endregion

    }
}
