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
    public partial class ReturnCodeElement : GenericElement
    {
        private int shownHeight = 200;
        private int hiddenHeight = 53;

        public enum MethodType
        {
            Undefined,
            Static,
            Variable
        }

        public ReturnCodeElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.ReturnCodeElement;
            Description = "Allow to define the return code for this Custom Update.";
            nupReturnCode.Maximum = int.MaxValue;
            nupReturnCode.Minimum = int.MinValue;
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public VariableElement Variable { get { return cmbBxVariable.SelectedIndex != -1 ? (VariableElement)cmbBxVariable.SelectedItem : null; } set { cmbBxVariable.SelectedItem = value; } }

        public MethodType Method
        {
            get
            {
                return rdBtnStatic.Checked ? MethodType.Static : MethodType.Variable;
            }
            set
            {
                switch (value)
                {
                    case MethodType.Static:
                        rdBtnStatic.Checked = true;
                        rdBtnVariable.Checked = false;
                        break;
                    case MethodType.Variable:
                        rdBtnStatic.Checked = false;
                        rdBtnVariable.Checked = true;
                        break;
                    default:
                        rdBtnStatic.Checked = false;
                        rdBtnVariable.Checked = false;
                        break;
                }
            }
        }

        public int StaticValue
        {
            get { return (int)nupReturnCode.Value; }
            set { nupReturnCode.Value = value; }
        }

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
                if (variable.VarType == VariableElement.VariableType.Int)
                    cmbBxVariable.Items.Add(variable);
            }
            AdjusteHeight();
            if (tempVariable != null)
                cmbBxVariable.SelectedItem = tempVariable;
        }

        public override string GetXMLAction()
        {
            string result = base.GetXMLAction();

            result += "<ReturnCodeMethod>" + Method + "</ReturnCodeMethod>\r\n<StaticReturnCode>" + StaticValue + "</StaticReturnCode>\r\n";
            if (Variable != null)
                result += "<Variable>" + Variable.ID.ToString() + "</Variable>";
            else
                result += "<Variable/>";

            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            if (Method == MethodType.Static)
                return "Return the Code : \r\n" + StaticValue;
            else
                return "Return the Code contains in the Variable : " + Variable;
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
            ConfigurationState = ConfigState.NotConfigured;

            if (rdBtnStatic.Checked && !rdBtnVariable.Checked)
            {
                ConfigurationState = ConfigState.Configured;
            }
            if (!rdBtnStatic.Checked && rdBtnVariable.Checked)
            {
                ConfigurationState = cmbBxVariable.SelectedIndex != -1 ? ConfigState.Configured : ConfigState.Misconfigured;
            }
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Response to Events - Réponse aux évènements)

        private void rdBtnStatic_CheckedChanged(object sender, EventArgs e)
        {
            nupReturnCode.Enabled = rdBtnStatic.Checked;
            ValidateData();
        }

        private void rdBtnVariable_CheckedChanged(object sender, EventArgs e)
        {
            cmbBxVariable.Enabled = rdBtnVariable.Checked;
            ValidateData();
        }

        private void nupReturnCode_ValueChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxVariable_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void ReturnCodeElement_DoubleClick(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        #endregion (Response to Events - Réponse aux évènements)

    }
}
