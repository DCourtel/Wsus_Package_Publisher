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
    public partial class ExecutableElement : GenericElement
    {
        private int shownHeight = 200;
        private int hiddenHeight = 53;

        public ExecutableElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.ExecutableElement;
            Description = "Allow to execute a file.";
            TimeBeforeKilling = 10;
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

        public string Parameters
        {
            get { return txtBxParameters.Text; }
            set { txtBxParameters.Text = value; }
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

            result += "<PathToExecutable>" + this.FilePath + "</PathToExecutable>\r\n<Parameters>" + this.Parameters + "</Parameters>\r\n<KillProcess>" + this.KillProcess.ToString() + "</KillProcess>\r\n<TimeBeforeKilling>" + 
                this.TimeBeforeKilling.ToString() + "</TimeBeforeKilling>\r\n";
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
            return GetXMLAction();
#else
            return "Execute the file : \r\n" + this.FilePath + 
                "\r\nWith parameters : \r\n" + this.Parameters + 
                "\r\nKill the Process if it run more than : " + this.TimeBeforeKilling.ToString() + " minutes. : " + this.KillProcess.ToString() +
                "\r\nAnd store the return code in this variable : " + (Variable != null ? Variable.ToString() : "***Not_Configured***");
#endif
        }

        private void AdjusteHeight()
        {
            if (!IsTemplate)
            {
                this.Height = this.IsExpand ? hiddenHeight : shownHeight;
                this.IsExpand = !this.IsExpand;
            }
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Response to Events - Réponse aux évènements)

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void txtBxFilePath_TextChanged(object sender, EventArgs e)
        {
            ConfigurationState = !string.IsNullOrEmpty(txtBxFilePath.Text) ? ConfigState.Configured : ConfigState.Misconfigured;
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        private void ExecutableElement_DoubleClick(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void txtBxFilePath_Enter(object sender, EventArgs e)
        {
            this.IsSelected = true;
        }

        private void chkBxKillProcess_CheckedChanged(object sender, EventArgs e)
        {
            nupTimeBeforeKilling.Enabled = chkBxKillProcess.Checked;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.cmbBxVariable.SelectedIndex = -1;
        }

        #endregion (Response to Events - Réponse aux évènements)
    }
}
