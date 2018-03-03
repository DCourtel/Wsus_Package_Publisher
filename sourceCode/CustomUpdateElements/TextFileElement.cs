using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomUpdateElements
{
    public partial class TextFileElement : GenericElement
    {
        private int shownHeight = 300;
        private int hiddenHeight = 53;

        public TextFileElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.CreateTextFileElement;
            Description = "Allow to create a text file.";
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public string FilePath
        {
            get { return txtBxFilePath.Text; }
            set { txtBxFilePath.Text = value; }
        }

        public string Filename
        {
            get { return txtBxFilename.Text; }
            set { txtBxFilename.Text = value; }
        }

        public string FileContent
        {
            get { return txtBxFileContent.Text; }
            set { txtBxFileContent.Text = value; }
        }

        #endregion (Public Properties - Propriétés public)

        #region (Public Methods - Méthodes public)

        public override void ShowElement(List<VariableElement> variables)
        {
            AdjusteHeight();
        }

        public override string GetXMLAction()
        {
            string result = base.GetXMLAction();

            result += "<Filepath>" + this.FilePath + "</Filepath>\r\n<Filename>" + this.Filename + "</Filename>\r\n<FileContent>" + this.FileContent + "</FileContent>\r\n<Variable/>";

            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            return "Create a file at : " + this.FilePath + "\r\nWith the name : " + this.Filename + "\r\nAnd content : " + this.FileContent;
        }

        private void AdjusteHeight()
        {
            if (!IsTemplate)
            {
                this.Height = this.IsExpand ? hiddenHeight : shownHeight;
                this.IsExpand = !this.IsExpand;
            }
        }

        private void ValidateData()
        {
            btnOk.Enabled = !string.IsNullOrEmpty(txtBxFileContent.Text) && !string.IsNullOrEmpty(txtBxFilename.Text) && !string.IsNullOrEmpty(txtBxFilePath.Text);
            ConfigurationState = btnOk.Enabled ? ConfigState.Configured : ConfigState.Misconfigured;
        }

        #endregion (Private Methods - Méthodes Privées)

        #region {Responses to events - Réponses aux événements}

        private void btnOk_Click(object sender, EventArgs e)
        {
            AdjusteHeight();
        }

        private void FileElement_DoubleClick(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion {Responses to events - Réponses aux événements}
    }
}
