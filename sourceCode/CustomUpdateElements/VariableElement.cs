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
    public partial class VariableElement : GenericElement
    {
        public enum VariableType
        {
            Int,
            String,
            Undefined
        }

        private int shownHeight = 150;
        private int hiddenHeight = 53;

        public VariableElement():base()
        {
            InitializeComponent();
            Image = Properties.Resources.VariableElement;
            Description = "Declare a variable to store data and using it later.";

            foreach (object varType in Enum.GetValues(typeof(VariableType)))
            {
                if ((VariableType)varType != VariableType.Undefined)
                    cmbBxVariableType.Items.Add(varType);
            }
            VarType = VariableType.Undefined;
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        internal string VarName { get { return txtBxVariableName.Text; } set { txtBxVariableName.Text = value; } }

        internal VariableElement.VariableType VarType
        {
            get
            {
                if (cmbBxVariableType.SelectedIndex != -1)
                {
                    foreach (object item in Enum.GetValues(typeof(VariableElement.VariableType)))
                    {
                        if ((VariableElement.VariableType)item == (VariableElement.VariableType)cmbBxVariableType.SelectedItem)
                            return (VariableElement.VariableType)item;
                    }
                }
                return VariableElement.VariableType.Undefined;
            }

            set
            {
                cmbBxVariableType.SelectedItem = value;
            }
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

            result += "<Name>" + VarName + "</Name>\r\n<Type>" + VarType + "</Type>\r\n" + "<ID>" + this.ID + "</ID>\r\n";

            return result + "</Action>";
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(VarName))
                return VarName.ToString();
            else
                return this.ID.ToString();
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            return "Variable : " + VarName + "\r\nType : " + VarType;
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
            ConfigurationState = (!string.IsNullOrEmpty(txtBxVariableName.Text) && cmbBxVariableType.SelectedIndex != -1) ? ConfigState.Configured : ConfigState.Misconfigured;
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Response to events - réponses aux évènements)

        private void txtBxVariableName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxVariableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void VariableElement_DoubleClick(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        #endregion (Response to events - réponses aux évènements)

    }
}
