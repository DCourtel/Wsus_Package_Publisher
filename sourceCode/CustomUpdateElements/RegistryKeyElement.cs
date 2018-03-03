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
    public partial class RegistryKeyElement : GenericElement
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

        public RegistryKeyElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.RegistryKeyElement;
            Description = "Allow to Add, Delete, Rename a registry key.";

            foreach (object action in Enum.GetValues(typeof(ActionType)))
            {
                if ((ActionType)action != ActionType.Undefined)
                    cmbBxActions.Items.Add(action);
            }
            Action = ActionType.Undefined;

            cmbBxHive.SelectedIndex = 0;
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public ActionType Action { get { return cmbBxActions.SelectedIndex != -1 ? (ActionType)cmbBxActions.SelectedItem : ActionType.Undefined; } set { cmbBxActions.SelectedItem = value; } }

        public string Hive
        {
            get { return cmbBxHive.SelectedItem.ToString(); }
            set { cmbBxHive.SelectedItem = value; }
        }

        public string Key
        {
            get { return txtBxKey.Text; }
            set { txtBxKey.Text = value; }
        }

        public string KeyName
        {
            get { return txtBxKeyName.Text; }
            set { txtBxKeyName.Text = value; }
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

            result += "<RegAction>" + Action.ToString() + "</RegAction>\r\n<RegHive>" + Hive + "</RegHive>\r\n<RegKey>" + Key + "</RegKey>\r\n<RegName>" + KeyName + "</RegName>\r\n";

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
                    return "Add this Registry Key :\r\n" + cmbBxHive.SelectedItem.ToString() + '\\' + txtBxKey.Text + '\\' + KeyName;
                case ActionType.Delete:
                    return "Delete this Registry Key :\r\n" + cmbBxHive.SelectedItem.ToString() + '\\' + txtBxKey.Text + '\\' + KeyName;
                case ActionType.Rename:
                    return "Rename this Registry Key :\r\n" + cmbBxHive.SelectedItem.ToString() + '\\' + txtBxKey.Text + '\\' + KeyName + "\r\n into :\r\n" + KeyName;
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
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Delete:
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Rename:
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text) && !string.IsNullOrEmpty(KeyName))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                default:
                    break;
            }
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Response to Events - Réponse aux évènements)

        private void btnOk_Click(object sender, EventArgs e)
        {
            char[] backslash = new char[] { '\\' };

            txtBxKey.Text = txtBxKey.Text.Trim();
            txtBxKey.Text = txtBxKey.Text.TrimStart(backslash);
            txtBxKey.Text = txtBxKey.Text.TrimEnd(backslash);

            txtBxKeyName.Text = txtBxKeyName.Text.Trim();
            txtBxKeyName.Text = txtBxKeyName.Text.TrimStart(backslash);
            txtBxKeyName.Text = txtBxKeyName.Text.TrimEnd(backslash);

            base.element_DoubleClick(this, e);
        }

        private void txtBxKey_TextChanged(object sender, EventArgs e)
        {
            if (txtBxKey.Text.ToLower().StartsWith(@"HKEY_LOCAL_MACHINE\".ToLower()))
            {
                txtBxKey.Text = txtBxKey.Text.Substring(@"HKEY_LOCAL_MACHINE\".Length);
                cmbBxHive.SelectedIndex = 0;
            }
            if (txtBxKey.Text.ToLower().StartsWith(@"HKEY_CURRENT_USER\".ToLower()))
            {
                txtBxKey.Text = txtBxKey.Text.Substring(@"HKEY_CURRENT_USER\".Length);
                cmbBxHive.SelectedIndex = 1;
            }
            ValidateData();
        }

        private void txtBxKeyName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxActions.SelectedIndex != -1)
            {
                txtBxKeyName.Enabled = ((ActionType)cmbBxActions.SelectedItem == ActionType.Rename);
            }
            else
                txtBxKeyName.Enabled = false;
            ValidateData();
        }

        private void RegistryElement_DoubleClick(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void cmbBxHive_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion (Response to Events - Réponse aux évènements)

    }
}
