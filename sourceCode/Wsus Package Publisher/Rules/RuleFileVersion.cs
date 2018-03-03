using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class RuleFileVersion : GenericRule
    {
        private enum ComparisonType
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        private Dictionary<string, int> _csidlByName = new Dictionary<string, int>()
        {
            {"COMMON_ADMINTOOLS" , 0x2F},
            {"COMMON_ALTSTARTUP", 0x1E},
            {"COMMON_APPDATA", 0x23},
            {"COMMON_DESKTOPDIRECTORY", 0x19},
            {"COMMON_DOCUMENTS", 0x2E},
            {"COMMON_FAVORITES", 0x1F},
            {"COMMON_PROGRAMS", 0x17},
            {"COMMON_STARTMENU", 0x16},
            {"COMMON_STARTUP", 0x18},
            {"COMMON_TEMPLATES", 0x2D},
            {"CONTROLS", 0x3},
            {"DRIVES", 0x11},
            {"FONTS", 0x14},
            {"PRINTERS", 0x4},
            {"PROGRAM_FILES", 0x26},
            {"PROGRAM_FILES_COMMON", 0x2B},
            {"PROGRAM_FILES_COMMONX86", 0x2C},
            {"PROGRAM_FILESX86", 0x2A},
            {"PROGRAMS", 0x2},
            {"SYSTEM", 0x25},
            {"SYSTEMX86", 0x29},
            {"WINDOWS", 0x24}
        };

        private Dictionary<int, string> _csidlByCode = new Dictionary<int, string>()
        {
            {0x2F,"COMMON_ADMINTOOLS"},
            {0x1E ,"COMMON_ALTSTARTUP" },
            {0x23,"COMMON_APPDATA" },
            {0x19 ,"COMMON_DESKTOPDIRECTORY" },
            {0x2E ,"COMMON_DOCUMENTS" },
            {0x1F ,"COMMON_FAVORITES" },
            {0x17 ,"COMMON_PROGRAMS" },
            {0x16 ,"COMMON_STARTMENU" },
            {0x18 ,"COMMON_STARTUP" },
            {0x2D ,"COMMON_TEMPLATES" },
            {0x3 ,"CONTROLS" },
            {0x11 ,"DRIVES" },
            {0x14 ,"FONTS" },
            {0x4 ,"PRINTERS" },
            {0x26 ,"PROGRAM_FILES" },
            {0x2B ,"PROGRAM_FILES_COMMON" },
            {0x2C ,"PROGRAM_FILES_COMMONX86" },
            {0x2A ,"PROGRAM_FILESX86" },
            {0x2 ,"PROGRAMS" },
            {0x25 ,"SYSTEM" },
            {0x29 ,"SYSTEMX86" },
            {0x24 ,"WINDOWS" }
        };

        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(RuleFileVersion).Assembly);

        public RuleFileVersion()
            : base()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();

            foreach (string csidl in _csidlByName.Keys)
            {
                cmbBxCsidl.Items.Add(csidl);
            }

            txtBxDescription.Text = resMan.GetString("DescriptionFileVersion");
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThan"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonLessThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThanOrEqualTo"));
            cmbBxComparison.Items.Add(resMan.GetString("ComparisonGreaterThan"));
            chkBxWellknownDirectory.Focus();
            base.HelpLink = "http://technet.microsoft.com/en-us/library/bb530986.aspx";
            AdjustHelpLinkLocation();
        }

        #region {Methods - Méthodes}

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
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.red, "FileVersion");
            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Path");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, FilePath);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");


            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Comparison");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Comparison);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");


            print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Version");
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
            print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Version);
            print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "\"");

            if (UseCsidl)
            {
                print(rTxtBx, GroupDisplayer.elementAndAttributeFont, GroupDisplayer.blue, " Csidl");
                print(rTxtBx, GroupDisplayer.normalFont, GroupDisplayer.black, "=\"");
                print(rTxtBx, GroupDisplayer.boldFont, GroupDisplayer.black, Csidl.ToString());
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
            RuleFileVersion clone = new RuleFileVersion();

            clone.FilePath = this.FilePath;
            clone.Comparison = this.Comparison;
            clone.ReverseRule = this.ReverseRule;
            clone.Version = this.Version;
            clone.UseCsidl = this.UseCsidl;
            if (UseCsidl)
                clone.Csidl = this.Csidl;

            return clone;
        }

        public override string ToString()
        {
            return resMan.GetString("FileVersion");
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
                    case "Version":
                        this.Version = pair.Value;
                        break;
                    case "Comparison":
                        this.Comparison = pair.Value;
                        break;
                    case "Csidl":
                        int result = 0;
                        if (int.TryParse(pair.Value, out result))
                            this.Csidl = result;
                        break;
                    default:
                        UnsupportedAttributes.Add(pair.Key, pair.Value);
                        break;
                }
            }
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

        private void ValidateData()
        {
             btnOk.Enabled = (!string.IsNullOrEmpty(txtBxFilePath.Text) && cmbBxComparison.SelectedIndex != -1 && (!chkBxWellknownDirectory.Checked || (chkBxWellknownDirectory.Checked && cmbBxCsidl.SelectedIndex != -1)));                
        }

        #endregion

        #region {Properties - Propriétés}

        /// <summary>
        /// Get or Set the file path.
        /// </summary>
        internal string FilePath
        {
            get { return txtBxFilePath.Text; }
            set { txtBxFilePath.Text = value; }
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
        /// Get or Set if the rule should be reverse.
        /// </summary>
        internal override bool ReverseRule
        {
            get { return chkBxReverseRule.Checked; }
            set { chkBxReverseRule.Checked = value; }
        }

        /// <summary>
        /// Get or Set the file version informations. Format "xxxxx.xxxxx.xxxxx.xxxxx" where 'x' is a digit.
        /// </summary>
        internal string Version
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

        internal bool UseCsidl
        {
            get { return chkBxWellknownDirectory.Checked; }
            set { chkBxWellknownDirectory.Checked = value; }
        }

        internal int Csidl
        {
            get
            {
                return _csidlByName[cmbBxCsidl.SelectedItem.ToString()];
            }

            set
            {
                cmbBxCsidl.SelectedItem = _csidlByCode[value];
                chkBxWellknownDirectory.Checked = true;
            }
        }

        internal override string XmlElementName
        {
            get { return "FileVersion"; }
        }

        #endregion

        #region {Response to Events - Réponses aux évènements}

        private void chkBxWellknownDirectory_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxCsidl.Enabled = chkBxWellknownDirectory.Checked;
            ValidateData();
        }

        private void txtBxFilePath_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxFilePath.Text) && txtBxFilePath.Text.Length > 260)
                txtBxFilePath.Text = txtBxFilePath.Text.Substring(0, 260);
            ValidateData();
        }

        private void cmbBxComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void nupVersion1_ValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void nupVersion1_Enter(object sender, EventArgs e)
        {
            NumericUpDown nup = (NumericUpDown)sender;
            nup.Select(0, nup.Value.ToString().Length);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ParentForm.DialogResult = DialogResult.Cancel;
        }

        private void cmbBxCsidl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion
    }
}
