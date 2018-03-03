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
    public partial class RuleMsiPatchInstalledForProduct : GenericRule
    {
        Guid _msiCode;
        Guid _patchCode;
        int _language = -1;
        bool _useVersionMax = false;
        bool _useVersionMin = false;
        System.Text.RegularExpressions.Regex regExp = new System.Text.RegularExpressions.Regex("^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$");
        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleMsiPatchInstalledForProduct).Assembly);

        public RuleMsiPatchInstalledForProduct():base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            txtBxDescription.Text = resManager.GetString("DescriptionRuleMsiPatchInstalledForProduct");
            foreach (KeyValuePair<string,string> pair in Languages.AllLanguagues)
            {
                cmbBxLanguage.Items.Add(pair.Key);
            }
            txtBxPatchCode.Select();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb531064.aspx";
            AdjustHelpLinkLocation();
        }


        #region(Methods - Méthodes)

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

        private void ValidateData()
        {
            btnOk.Enabled = (regExp.IsMatch(txtBxPatchCode.Text) && regExp.IsMatch(txtBxProductCode.Text) && ((chkBxUseLanguage.Checked && cmbBxLanguage.SelectedIndex != -1) || !chkBxUseLanguage.Checked));
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

            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "<msiar:");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "MsiPatchInstalledForProduct");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " PatchCode");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"{");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, PatchCode.ToString());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "}\"");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " ProductCode");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"{");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, MsiProductCode.ToString());
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "}\"");

            if (UseVersionMax)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " VersionMax");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, VersionMax);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            if (UseVersionMin)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " VersionMin");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, VersionMin);
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");
            }

            if (UseLanguage)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Language");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Language.ToString());
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
            RuleMsiPatchInstalledForProduct clone = new RuleMsiPatchInstalledForProduct();

            clone.PatchCode = this.PatchCode;
            clone.MsiProductCode = this.MsiProductCode;
            clone.ReverseRule = this.ReverseRule;
            clone.UseVersionMax = this.UseVersionMax;
            if (UseVersionMax)
                clone.VersionMax = this.VersionMax;
            clone.UseVersionMin = this.UseVersionMin;
            if (UseVersionMin)
                clone.VersionMin = this.VersionMin;
            clone.UseLanguage = this.UseLanguage;
            if (UseLanguage)
                clone.Language = this.Language;

            return clone;
        }

        public override string ToString()
        {
            return resManager.GetString("MsiPatchInstalledForProduct");
        }

        internal override void InitializeWithAttributes(Dictionary<string, string> attributes)
        {
            foreach (KeyValuePair<string, string> pair in attributes)
            {
                switch (pair.Key)
                {
                    case "ProductCode":
                        this.MsiProductCode = new Guid(pair.Value);
                        break;
                    case "PatchCode":
                        this.PatchCode = new Guid(pair.Value);
                        break;
                    case "VersionMax":
                        this.VersionMax = pair.Value;
                        break;
                    case "VersionMin":
                        this.VersionMin = pair.Value;
                        break;
                    case "Language":
                        int result = 0;
                        if (int.TryParse(pair.Value, out result))
                            this.Language = result;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
        }

        #endregion

        #region(Properties - propriétés)

        internal Guid MsiProductCode
        {
            get { return _msiCode; }
            set
            {
                if (value != _msiCode)
                {
                    _msiCode = value;
                    txtBxProductCode.Text = value.ToString();
                }
            }
        }

        internal Guid PatchCode
        {
            get { return _patchCode; }
            set
            {
                if (value != _patchCode)
                {
                    _patchCode = value;
                    txtBxPatchCode.Text = value.ToString();
                }
            }
        }
        
        internal string VersionMax
        {
            get { return nupVersionMax1.Value + "." + nupVersionMax2.Value + "." + nupVersionMax3.Value + "." + nupVersionMax4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformated(value))
                {
                    nupVersionMax1.Value = GetVersionNumber(value, 0);
                    nupVersionMax2.Value = GetVersionNumber(value, 1);
                    nupVersionMax3.Value = GetVersionNumber(value, 2);
                    nupVersionMax4.Value = GetVersionNumber(value, 3);
                    UseVersionMax = true;
                }
                else
                {
                    UseVersionMax = false;
                }
            }
        }

        internal string VersionMin
        {
            get { return nupVersionMin1.Value + "." + nupVersionMin2.Value + "." + nupVersionMin3.Value + "." + nupVersionMin4.Value; }
            set
            {
                if (IsVersionStringCorrectlyformated(value))
                {
                    nupVersionMin1.Value = GetVersionNumber(value, 0);
                    nupVersionMin2.Value = GetVersionNumber(value, 1);
                    nupVersionMin3.Value = GetVersionNumber(value, 2);
                    nupVersionMin4.Value = GetVersionNumber(value, 3);
                    UseVersionMin = true;
                }
                else
                {
                    UseVersionMin = false;
                }
            }
        }

        /// <summary>
        /// Get or Set if the Version Max. Should be include in the MetaData.
        /// </summary>
        internal bool UseVersionMax
        {
            get { return _useVersionMax; }
            set
            {
                _useVersionMax = value;
                chkBxUseVersionMax.Checked = value;
            }
        }

        /// <summary>
        /// Get or Set if the Version Max. Should be include in the MetaData.
        /// </summary>
        internal bool UseVersionMin
        {
            get { return _useVersionMin; }
            set
            {
                _useVersionMin = value;
                chkBxUseVersionMin.Checked = value;
            }
        }

        internal bool UseLanguage
        {
            get { return chkBxUseLanguage.Checked; }
            set { chkBxUseLanguage.Checked = value; }
        }

        /// <summary>
        /// Get or set the language of the update.
        /// </summary>
        internal int Language
        {
            get { return _language; }
            set
            {
                if (Languages.GetLanguageName(value) != string.Empty)
                {
                    _language = value;
                    cmbBxLanguage.SelectedItem = Languages.GetLanguageName(value);
                    UseLanguage = true;
                }
            }
        }

        internal override bool ReverseRule
        {
            get { return chkbxReverseRule.Checked; }
            set { chkbxReverseRule.Checked = value; }
        }

        internal override string XmlElementName
        {
            get { return "MsiPatchInstalledForProduct"; }
        }

        #endregion

        #region(Responses to events - Réponses aux événements)

        private void txtBxProductCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxProductCode.Text))
            {
                string GUIDCandidate = txtBxProductCode.Text;

                GUIDCandidate = GUIDCandidate.TrimStart(new char[] { '{' });
                GUIDCandidate = GUIDCandidate.TrimEnd(new char[] { '}' });

                if (regExp.IsMatch(GUIDCandidate))
                    MsiProductCode = new Guid(GUIDCandidate);
            }
            ValidateData();
        }

        private void txtBxPatchCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxPatchCode.Text))
            {
                string GUIDCandidate = txtBxPatchCode.Text;

                GUIDCandidate = GUIDCandidate.TrimStart(new char[] { '{' });
                GUIDCandidate = GUIDCandidate.TrimEnd(new char[] { '}' });

                if (regExp.IsMatch(GUIDCandidate))
                    PatchCode = new Guid(GUIDCandidate);
            }
            ValidateData();
        }

        private void chkBxUseVersionMax_CheckedChanged(object sender, EventArgs e)
        {
            nupVersionMax1.Enabled = chkBxUseVersionMax.Checked;
            nupVersionMax2.Enabled = chkBxUseVersionMax.Checked;
            nupVersionMax3.Enabled = chkBxUseVersionMax.Checked;
            nupVersionMax4.Enabled = chkBxUseVersionMax.Checked;

            _useVersionMax = chkBxUseVersionMax.Checked;
        }

        private void chkBxUseVersionMin_CheckedChanged(object sender, EventArgs e)
        {
            nupVersionMin1.Enabled = chkBxUseVersionMin.Checked;
            nupVersionMin2.Enabled = chkBxUseVersionMin.Checked;
            nupVersionMin3.Enabled = chkBxUseVersionMin.Checked;
            nupVersionMin4.Enabled = chkBxUseVersionMin.Checked;

            _useVersionMin = chkBxUseVersionMin.Checked;
        }

        private void cmbBxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Language = Languages.GetLanguageMSICode(cmbBxLanguage.SelectedItem.ToString());
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void nupVersionMax1_Enter(object sender, EventArgs e)
        {
            (sender as NumericUpDown).Select(0, 5);
        }

        private void chkBxUseLanguage_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxLanguage.Enabled = chkBxUseLanguage.Checked;
            ValidateData();
        }

        #endregion
    }
}
