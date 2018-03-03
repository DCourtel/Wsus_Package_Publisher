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
    internal partial class RuleProcessorArchitecture : GenericRule
    {
        private ushort _processorArchitecture;
        private bool _reverseRule;
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleProcessorArchitecture).Assembly);

        public RuleProcessorArchitecture()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            txtBxDescription.Text = resManager.GetString("DescriptionRuleProcessorArchitecture");
            cmbBxProcessorArchitecture.Select();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531038.aspx";
            AdjustHelpLinkLocation();
        }

        #region Methods - Méthodes

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "Processor");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Architecture");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, ProcessorArchitecture.ToString());
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
            RuleProcessorArchitecture clone = new RuleProcessorArchitecture();

            clone.ProcessorArchitecture = this.ProcessorArchitecture;
            clone.ReverseRule = this.ReverseRule;
            return clone;
        }

        public override string ToString()
        {
            return resManager.GetString("Processor");
        }

        internal override void InitializeWithAttributes(Dictionary<string,string> attributes)
        {
           foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Architecture":                        
                        ushort result = 0;
                        if (ushort.TryParse(pair.Value, out result))
                            this.ProcessorArchitecture = result;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion

        #region Properties - Propriétés

        /// <summary>
        /// Get or Set the processor architecture. 0 : x86, 6 : IA64, 9 : x64
        /// </summary>
        internal ushort ProcessorArchitecture
        {
            get { return _processorArchitecture; }
            set 
            {
                _processorArchitecture = value;
                switch (value)
                {
                    case 0:
                        cmbBxProcessorArchitecture.SelectedIndex = 0;
                        break;
                    case 9:
                        cmbBxProcessorArchitecture.SelectedIndex = 1;
                        break;
                    case 6:
                        cmbBxProcessorArchitecture.SelectedIndex = 2;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Get or set if the rule is reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return _reverseRule; }
            set 
            {
                _reverseRule = value;
                chkBxInverseRule.Checked = value;
            }
        }

        internal override string XmlElementName
        {
            get { return "Processor"; }
        }

        #endregion

        #region Response to Events - Réponses aux évènements

        private void cmbBxProcessorArchitecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbBxProcessorArchitecture.SelectedIndex)
            {
                case 0:
                    ProcessorArchitecture = 0;
                    break;
                case 1:
                    ProcessorArchitecture = 9;
                    break;
                case 2:
                    ProcessorArchitecture = 6;
                    break;
                default:
                    break;
            }
            btnOk.Enabled = true;
        }

        private void chkBxInverseRule_CheckedChanged(object sender, EventArgs e)
        {
            ReverseRule = chkBxInverseRule.Checked;
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
