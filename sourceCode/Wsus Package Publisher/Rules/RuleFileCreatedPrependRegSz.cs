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
    public partial class RuleFileCreatedPrependRegSz : GenericRule
    {
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileCreatedPrependRegSz).Assembly);

        public RuleFileCreatedPrependRegSz()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            txtBxDescription.Text = resMan.GetString("DescriptionFileCreatedPrependRegSz");
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));
            txtBxSubKey.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531091.aspx";
            AdjustHelpLinkLocation();
        }

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "FileCreatedPrependRegSz");

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

            if (RegType32)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " RegType32");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, RegType32.ToString().ToLower());
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Path");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, FilePath);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Comparison");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Comparison);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Created");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, GetFormatedDate(CreationDate.Date, Hour, Minute, Second));
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

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "Path":
                        this.FilePath = pair.Value;
                        break;
                    case "Comparison":
                        this.Comparison = pair.Value;
                        break;
                    case "Created":
                        DateTime newdate;
                        if (DateTime.TryParse(pair.Value, out newdate))
                        {
                            newdate = newdate.ToLocalTime();
                            this.CreationDate = newdate;
                            InitializeHour(newdate);
                        }
                        break;
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
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        internal override GenericRule Clone()
        {
            RuleFileCreatedPrependRegSz clone = new RuleFileCreatedPrependRegSz();

            clone.SubKey = this.SubKey;
            clone.Value = this.Value;
            clone.RegType32 = this.RegType32;
            clone.FilePath = this.FilePath;
            clone.ReverseRule = this.ReverseRule;
            clone.Comparison = this.Comparison;
            clone.CreationDate = this.CreationDate;
            clone.Hour = this.Hour;
            clone.Minute = this.Minute;
            clone.Second = this.Second;
            return clone;
        }

        private string GetFormatedDate(DateTime dateToFormat, int hour, int minute, int second)
        {
            DateTime tempDate = new DateTime(dateToFormat.Year, dateToFormat.Month, dateToFormat.Day, hour, minute, second);

            return string.Format("{0:s}", tempDate.ToUniversalTime());
        }

        public override string ToString()
        {
            return resMan.GetString("FileCreatedPrependRegSz");
        }

        private void ValidateData()
        {
            btnOk.Enabled = (!string.IsNullOrEmpty(txtBxSubKey.Text) &&
                txtBxValue.Text != null &&
                txtBxValue.Text.Length <= 16383 &&
                !string.IsNullOrEmpty(txtBxFilePath.Text) &&
                txtBxFilePath.Text.Length >= 1 &&
                txtBxFilePath.Text.Length <= 260 &&
                cmbBxComparison.SelectedIndex != -1);
        }

        private void InitializeHour(DateTime time)
        {
            nupHour.Value = time.Hour;
            nupMinute.Value = time.Minute;
            nupSecond.Value = time.Second;
        }

        #endregion

        #region (Properties - Propriétés)

        /// <summary>
        /// Get or Set the SubKey
        /// </summary>
        internal string SubKey
        {
            get { return txtBxSubKey.Text; }
            set { txtBxSubKey.Text = value; }
        }

        /// <summary>
        /// Get or Set the Registry Key value
        /// </summary>
        internal string Value
        {
            get { return txtBxValue.Text; }
            set { txtBxValue.Text = value; }
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
        /// Get or Set the file path.
        /// </summary>
        internal string FilePath
        {
            get { return txtBxFilePath.Text; }
            set { txtBxFilePath.Text = value; }
        }

        /// <summary>
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
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
                    cmbBxComparison.SelectedItem = resMan.GetString("Comparison" + value);
                else
                    cmbBxComparison.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Get or Set the Creation Date of the file.
        /// </summary>
        internal DateTime CreationDate
        {
            get { return dtpCreationDate.Value; }
            set { dtpCreationDate.Value = value; }
        }

        internal int Hour
        {
            get { return (int)nupHour.Value; }
            set { nupHour.Value = value; }
        }

        internal int Minute
        {
            get { return (int)nupMinute.Value; }
            set { nupMinute.Value = value; }
        }

        internal int Second
        {
            get { return (int)nupSecond.Value; }
            set { nupSecond.Value = value; }
        }

        internal override string XmlElementName
        {
            get { return "FileCreatedPrependRegSz"; }
        }

        #endregion

        #region {responses to event - Réponses aux évènements}

        private void txtBxSubKey_TextChanged(object sender, EventArgs e)
        {
            if (txtBxSubKey.Text.ToLower().StartsWith(@"HKEY_LOCAL_MACHINE\".ToLower()))
            {
                txtBxSubKey.Text = txtBxSubKey.Text.Substring(@"HKEY_LOCAL_MACHINE\".Length);
            }

            ValidateData();
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxFilePath_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.OK;
        }

        #endregion {responses to event - Réponses aux évènements}

    }
}
