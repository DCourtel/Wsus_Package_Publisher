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
    public partial class FolderElement : GenericElement
    {
        public enum ActionType
        {
            Undefined,
            Add,
            Delete,
            Rename
        }

        private int shownHeight = 250;
        private int hiddenHeight = 53;

        public FolderElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.FolderElement;
            Description = "Allow to Add, Delete or Rename folders.";

            foreach (object action in Enum.GetValues(typeof(ActionType)))
            {
                if ((ActionType)action != ActionType.Undefined)
                    cmbBxAction.Items.Add(action);
            }
            Action = ActionType.Undefined;
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public ActionType Action { get { return cmbBxAction.SelectedIndex != -1 ? (ActionType)cmbBxAction.SelectedItem : ActionType.Undefined; } set { cmbBxAction.SelectedItem = value; } }

        public string FolderName
        {
            get { return txtBxFolderName.Text; }
            set { txtBxFolderName.Text = value; }
        }

        public string NewName
        {
            get { return txtBxNewName.Text; }
            set { txtBxNewName.Text = value; }
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

            result += "<FolderAction>" + Action.ToString() + "</FolderAction>\r\n<FolderName>" + FolderName + "</FolderName>\r\n<NewName>" + NewName + "</NewName>\r\n";
            result += "<RegVariable/>";

            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            switch (Action)
            {
                case ActionType.Add:
                    return "Create the folder : " + FolderName;                    
                case ActionType.Delete:
                    return "Delete the folder : " + FolderName;
                case ActionType.Rename:
                    return "Rename the folder : " + FolderName + "\r\ninto : " + NewName;
                default:
                    return Description;
            }
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
            switch (Action)
            {
                case ActionType.Undefined:
                    ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Add:
                    if (!string.IsNullOrEmpty(txtBxFolderName.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Delete:
                    if (!string.IsNullOrEmpty(txtBxFolderName.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Rename:
                    if (!string.IsNullOrEmpty(txtBxFolderName.Text) && !string.IsNullOrEmpty(txtBxNewName.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                default:
                    ConfigurationState = ConfigState.Misconfigured;
                    break;
            }
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Private Methods - Méthodes Privées)

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBxNewName.Enabled = (cmbBxAction.SelectedIndex != -1 && cmbBxAction.SelectedIndex == 2);
            ValidateData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            char[] backslash = new char[] { '\\' };
                        
            txtBxFolderName.Text = txtBxFolderName.Text.TrimEnd(backslash);

            txtBxNewName.Text = txtBxNewName.Text.TrimStart(backslash);
            txtBxNewName.Text = txtBxNewName.Text.TrimEnd(backslash);

            base.element_DoubleClick(this, e);
        }
    }
}
