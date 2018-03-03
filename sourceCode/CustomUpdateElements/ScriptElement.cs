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
    public partial class ScriptElement : GenericElement
    {
        private int shownHeight = 350;
        private int hiddenHeight = 53;

        public enum ScriptTypes
        {
            Undefined,
            Vbscript,
            Powershell
        }

        public ScriptElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.ScriptElement;
            Description = "Allow to execute VbScript, Powershell script.";
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public ScriptTypes ScriptType
        {
            get
            {
                if (rdBtnVbscript.Checked)
                    return ScriptTypes.Vbscript;
                if(rdBtnPowershell.Checked)
                    return ScriptTypes.Powershell;
                return ScriptTypes.Undefined;
            }
            set
            {
                switch (value)
                {
                    case ScriptTypes.Vbscript:
                        rdBtnVbscript.Checked = true;
                        rdBtnPowershell.Checked = false;
                        break;
                    case ScriptTypes.Powershell:                        
                        rdBtnVbscript.Checked = false;
                        rdBtnPowershell.Checked = true;
                        break;
                    default:
                        rdBtnVbscript.Checked = false;
                        rdBtnPowershell.Checked = false;
                        break;
                }

            }
        }

        public string Filename
        {
            get { return txtBxFilename.Text; }
            set { txtBxFilename.Text = value; }
        }

        public string Arguments
        {
            get { return txtBxArguments.Text; }
            set { txtBxArguments.Text = value; }
        }

        public bool KillProcess
        {
            get { return chkBxKillProcess.Checked; }
            set { chkBxKillProcess.Checked = value; }
        }

        public int TimeBeforeKilling
        {
            get { return (int)nupTimeBeforeKilling.Value; }
            set
            {
                if (value >= 1 && value <= 120)
                    nupTimeBeforeKilling.Value = value;
                else
                    nupTimeBeforeKilling.Value = 10;
                chkBxKillProcess.Checked = true;
            }
        }

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

            result += "<ScriptType>" + this.ScriptType.ToString() + "</ScriptType>\r\n<Filename>" + this.Filename + "</Filename>\r\n<Arguments>" + this.Arguments + "</Arguments>\r\n<KillProcess>" + 
                this.KillProcess.ToString() + "</KillProcess>\r\n<TimeBeforeKilling>" + this.TimeBeforeKilling.ToString() + "</TimeBeforeKilling>\r\n";
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
            switch (ScriptType)
            {
                case ScriptTypes.Vbscript:
                    return "Run the Vbscript : " + Filename + " with arguments : " + Arguments + "\r\nKill the Process if it run more than : " + this.TimeBeforeKilling.ToString() + " minutes. : " + this.KillProcess.ToString() +
                "\r\nAnd store the return code in this variable : " + (Variable != null ? Variable.ToString() : "***Not_Configured***");
                case ScriptTypes.Powershell:
                    return "Run the Powershell script : " + Filename + " with arguments : " + Arguments + "\r\nKill the Process if it run more than : " + this.TimeBeforeKilling.ToString() + " minutes. : " + this.KillProcess.ToString() +
                "\r\nAnd store the return code in this variable : " + (Variable != null ? Variable.ToString() : "***Not_Configured***");
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
            btnOk.Enabled = (rdBtnPowershell.Checked || rdBtnVbscript.Checked) && !string.IsNullOrEmpty(txtBxFilename.Text);
            if (btnOk.Enabled)
                ConfigurationState = ConfigState.Configured;
            else
                ConfigurationState = ConfigState.Misconfigured;
        }

        #endregion (Private Methods - Méthodes Privées)

        #region {Responses to events - Réponses aux événements}

        private void rdBtnVbscript_CheckedChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxFilename_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxArguments_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }
        
        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            nupTimeBeforeKilling.Enabled = chkBxKillProcess.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.cmbBxVariable.SelectedIndex = -1;
        }

        #endregion {Responses to events - Réponses aux événements}

    }
}
