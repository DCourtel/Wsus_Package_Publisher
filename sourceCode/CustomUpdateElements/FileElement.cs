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
    public partial class FileElement : GenericElement
    {
        public enum ActionType
        {
            Undefined,
            Copy,
            Delete,
            Rename
        }

        private int shownHeight = 250;
        private int hiddenHeight = 53;

        public FileElement()
        {
            InitializeComponent();
            Image = Properties.Resources.FileElement;
            Description = "Allow to Copy, Delete or Rename Files.";

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

        public string FileName
        {
            get { return txtBxFileName.Text; }
            set { txtBxFileName.Text = value; }
        }

        public string Destination
        {
            get { return txtBxDestination.Text; }
            set { txtBxDestination.Text = value; }
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

            result += "<FileAction>" + Action.ToString() + "</FileAction>\r\n<FileName>" + FileName + "</FileName>\r\n<Destination>" + Destination + "</Destination>\r\n<NewName>" + NewName + "</NewName>\r\n";
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
                case ActionType.Copy:
                    return "Copy : " + FileName + "\r\nto : " + Description;
                case ActionType.Delete:
                    return "Delete : " + FileName;
                case ActionType.Rename:
                    return "Rename : " + FileName + "\r\ninto : " + NewName;
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
                case ActionType.Copy:
                    if (!string.IsNullOrEmpty(txtBxFileName.Text) && !string.IsNullOrEmpty(txtBxDestination.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Delete:
                    if (!string.IsNullOrEmpty(txtBxFileName.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Rename:
                    if (!string.IsNullOrEmpty(txtBxFileName.Text) && !string.IsNullOrEmpty(txtBxNewName.Text))
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
        
        private void cmbBxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBxDestination.Enabled = (cmbBxAction.SelectedIndex != -1 && cmbBxAction.SelectedIndex == 0);
            txtBxNewName.Enabled = (cmbBxAction.SelectedIndex != -1 && cmbBxAction.SelectedIndex == 2);

            ValidateData();
        }

        private void txtBxFileName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxNewName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxDestination_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }
    }
}
