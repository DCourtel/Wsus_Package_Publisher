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
    public partial class RegistryElement : GenericElement
    {
        public enum ActionType
        {
            Undefined,
            Add,
            Delete,
            Modify,
            Read
        }

        public enum ValueType
        {
            Undefined,
            REG_SZ,
            REG_BINARY,
            REG_DWORD,
            REG_QWORD,
            REG_MULTI_SZ,
            REG_EXPAND_SZ
        }

        private int shownHeight = 250;
        private int hiddenHeight = 53;

        public RegistryElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.RegistryElement;
            Description = "Allow to Add, Delete, Modify or read registry value.";

            foreach (object action in Enum.GetValues(typeof(ActionType)))
            {
                if ((ActionType)action != ActionType.Undefined)
                    cmbBxActions.Items.Add(action);
            }
            Action = ActionType.Undefined;

            foreach (object type in Enum.GetValues(typeof(ValueType)))
            {
                if ((ValueType)type != ValueType.Undefined)
                    cmbBxValueType.Items.Add(type);
            }
            cmbBxValueType.SelectedIndex = 0;
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

        public string Value
        {
            get { return txtBxValue.Text; }
            set { txtBxValue.Text = value; }
        }

        public ValueType Type { get { return cmbBxValueType.SelectedIndex != -1 ? (ValueType)cmbBxValueType.SelectedItem : ValueType.Undefined; } set { cmbBxValueType.SelectedItem = value; } }

        public string Data { get { return txtBxNewData.Text; } set { txtBxNewData.Text = value; } }

        public VariableElement Variable { get { return cmbBxVariable.SelectedIndex != -1 ? (VariableElement)cmbBxVariable.SelectedItem : null; } set { cmbBxVariable.SelectedItem = value; } }

        #endregion (Public Properties - Propriétés public)

        #region (Public Methods - Méthodes public)

        public override void ShowElement(List<VariableElement> variables)
        {
            VariableElement tempVariable = null;
            if (Variable != null)
                tempVariable = Variable;

            cmbBxVariable.Items.Clear();
            foreach (VariableElement variable in variables)
            {
                cmbBxVariable.Items.Add(variable);
            }
            AdjusteHeight();
            if (tempVariable != null)
                cmbBxVariable.SelectedItem = tempVariable;
        }

        public override string GetXMLAction()
        {
            string result = base.GetXMLAction();

            result += "<RegAction>" + Action.ToString() + "</RegAction>\r\n<RegHive>" + Hive + "</RegHive>\r\n<RegKey>" + Key + "</RegKey>\r\n<RegValue>" + Value + "</RegValue>\r\n<RegType>" + Type.ToString() + "</RegType>\r\n<RegData>" + Data + "</RegData>\r\n";

            if (Variable != null)
                result += "<RegVariable>" + Variable.ID.ToString() + "</RegVariable>";
            else
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
                    return "Add this Registry Value :\r\n" + cmbBxHive.SelectedItem.ToString() + txtBxKey.Text + Value + "\r\n of this Type : " + Type.ToString() + "\r\n With this Data : " + Data;
                case ActionType.Delete:
                    return "Delete this Registry Value :\r\n" + cmbBxHive.SelectedItem.ToString() + txtBxKey.Text + Value;
                case ActionType.Modify:
                    return "Modifie this Registry Value :\r\n" + cmbBxHive.SelectedItem.ToString() + txtBxKey.Text + Value + "\r\n With this Data :\r\n" + Data;
                case ActionType.Read:
                    return "Read this Registry Value :\r\n" + cmbBxHive.SelectedItem.ToString() + txtBxKey.Text + Value + "\r\n to this Variable :\r\n" + Variable;
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
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text) && !string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(Data) && cmbBxValueType.SelectedIndex != -1)
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Delete:
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text) && !string.IsNullOrEmpty(Value))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Modify:
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text) && !string.IsNullOrEmpty(Value) && !string.IsNullOrEmpty(Data))
                        ConfigurationState = ConfigState.Configured;
                    else
                        ConfigurationState = ConfigState.Misconfigured;
                    break;
                case ActionType.Read:
                    if (cmbBxHive.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxKey.Text) && !string.IsNullOrEmpty(Value) && cmbBxVariable.SelectedIndex != -1)
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

            txtBxKey.Text = txtBxKey.Text.TrimStart(backslash);
            txtBxKey.Text = txtBxKey.Text.TrimEnd(backslash);

            txtBxValue.Text = txtBxValue.Text.TrimStart(backslash);
            txtBxValue.Text = txtBxValue.Text.TrimEnd(backslash);

            base.element_DoubleClick(this, e);
        }

        private void cmbBxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Action)
            {
                case ActionType.Add:
                    txtBxValue.Enabled = true;
                    cmbBxValueType.Enabled = true;
                    txtBxNewData.Enabled = true;
                    cmbBxVariable.Enabled = false;
                    break;
                case ActionType.Delete:
                    txtBxValue.Enabled = true;
                    cmbBxValueType.Enabled = false;
                    txtBxNewData.Enabled = false;
                    cmbBxVariable.Enabled = false;
                    break;
                case ActionType.Modify:
                    txtBxValue.Enabled = true;
                    cmbBxValueType.Enabled = false;
                    txtBxNewData.Enabled = true;
                    cmbBxVariable.Enabled = false;
                    break;
                case ActionType.Read:
                    txtBxValue.Enabled = true;
                    cmbBxValueType.Enabled = false;
                    txtBxNewData.Enabled = false;
                    cmbBxVariable.Enabled = true;
                    break;
                default:
                    break;
            }
            ValidateData();
        }

        private void txtBxValue_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxValueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxNewData_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        #endregion (Response to Events - Réponse aux évènements)

    }
}
