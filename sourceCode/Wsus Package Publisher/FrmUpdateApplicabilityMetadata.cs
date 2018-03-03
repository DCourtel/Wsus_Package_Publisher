using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Xml;

namespace Wsus_Package_Publisher
{
    public partial class FrmUpdateApplicabilityMetadata : Form
    {
        private struct XmlError
        {
            private string _message;
            private int _line;
            private int _pos;

            internal XmlError(string message, int line, int pos)
            {
                _message = message;
                _line = line;
                _pos = pos;
            }

            internal string Message { get { return _message; } set { _message = value; } }
            internal int Line { get { return _line; } set { _line = value; } }
            internal int Pos { get { return _pos; } set { _pos = value; } }
        }

        private List<XmlError> _xmlErrors = new List<XmlError>();
        private System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateApplicabilityMetadata).Assembly);

        private string _originalText = string.Empty;

        public FrmUpdateApplicabilityMetadata()
        {
            InitializeComponent();
        }

        internal string OriginalText
        {
            set
            {
                if (value != null)
                {
                    this._originalText = value;
                    this.txtBxApplicabilityMetadata.Text = value;
                    if (value == string.Empty)
                    {
                        this.chkBxEditApplicabilityMetadata.Checked = false;
                    }
                    this.chkBxEditApplicabilityMetadata.Enabled = !string.IsNullOrEmpty(value);
                }
            }
        }

        internal string Metadata
        {
            get { return this.txtBxApplicabilityMetadata.Text; }
        }

        internal bool EditMetadata
        {
            get { return this.chkBxEditApplicabilityMetadata.Checked; }
        }

        private void chkBxEditApplicabilityMetadata_CheckedChanged(object sender, EventArgs e)
        {
            this.txtBxApplicabilityMetadata.Enabled = this.chkBxEditApplicabilityMetadata.Checked;
            this.btnReset.Enabled = this.chkBxEditApplicabilityMetadata.Checked;
            this.btnReplace.Enabled = this.chkBxEditApplicabilityMetadata.Checked;
            this.btnValidate.Enabled = this.chkBxEditApplicabilityMetadata.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtBxApplicabilityMetadata.Text = this._originalText;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            EasyCompany.Controls.SearchAndReplace searchAndReplace = new EasyCompany.Controls.SearchAndReplace();

            searchAndReplace.TextToEdit = this.txtBxApplicabilityMetadata.Text;
            if (searchAndReplace.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtBxApplicabilityMetadata.Text = searchAndReplace.TextToEdit;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            this._xmlErrors.Clear();
            try
            {
                string strXml = this.txtBxApplicabilityMetadata.Text;
                if (strXml.Contains("<msiar:MsiPatchMetadata>"))
                    strXml = strXml.Replace("<msiar:MsiPatchMetadata>", "<msiar:MsiPatchMetadata xmlns:msiar=\"http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/MsiApplicabilityRules.xsd\">");
                if (strXml.Contains("<msiar:MsiApplicationMetadata>"))
                    strXml = strXml.Replace("<msiar:MsiApplicationMetadata>", "<msiar:MsiApplicationMetadata xmlns:msiar=\"http://schemas.microsoft.com/wsus/2005/04/CorporatePublishing/MsiApplicabilityRules.xsd\">");
                System.IO.StringReader strReader = new System.IO.StringReader(Properties.Settings.Default.XmlSchema);
                XmlReader reader = XmlReader.Create(strReader);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(strXml);
                xmlDoc.Schemas.Add(null, reader);
                xmlDoc.Validate(new ValidationEventHandler(XmlValidationFailed));
                if (this._xmlErrors.Count != 0)
                {
                    MessageBox.Show(this._xmlErrors.Count.ToString() + resManager.GetString("ErrorsHasBeenFound"));
                    foreach (XmlError xmlError in _xmlErrors)
                    {
                        if (MessageBox.Show(xmlError.Message, "", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                            break;
                    }
                }
                else
                    MessageBox.Show(resManager.GetString("NoErrorFound"));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void XmlValidationFailed(object sender, ValidationEventArgs e)
        {
            this._xmlErrors.Add(new XmlError(e.Message, e.Exception.LineNumber, e.Exception.LinePosition));
        }

        private string SearchAndReplaceText(string textToSearch, string replacementText)
        {
            return this.txtBxApplicabilityMetadata.Text.Replace(textToSearch, replacementText);
        }

        private void txtBxApplicabilityMetadata_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == (Keys.Control | Keys.A))
            {
                this.txtBxApplicabilityMetadata.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
