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
    public partial class KillProcessElement : GenericElement
    {
        private int shownHeight = 120;
        private int hiddenHeight = 53;

        public KillProcessElement():base()
        {
            InitializeComponent();

            Image = Properties.Resources.KillProcessElement;
            Description = "Allow to kill a process by his name.";

        }
        
        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }
                
        public string ProcessName
        {
            get { return txtBxProcessName.Text; }
            set { txtBxProcessName.Text = value; }
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

            result += "<ProcessName>" + ProcessName + "</ProcessName>";
            
            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            return "Kill the process : " + ProcessName;            
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
            if (!string.IsNullOrEmpty(txtBxProcessName.Text))
            {
                ConfigurationState = ConfigState.Configured;
                btnOk.Enabled = true;
            }
            else
            {
                ConfigurationState = ConfigState.Misconfigured;
                btnOk.Enabled = false;
            }
        }

        #endregion (Private Methods - Méthodes Privées)

        #region (Response to Events - Réponse aux évènements)

        private void txtBxProcessName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        #endregion (Response to Events - Réponse aux évènements)


    }
}
