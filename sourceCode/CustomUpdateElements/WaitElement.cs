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
    public partial class WaitElement : GenericElement
    {
        private int shownHeight = 100;
        private int hiddenHeight = 53;

        public WaitElement():base()
        {
            InitializeComponent();
            Image = Properties.Resources.WaitElement;
            Description = "Allow to pause the process.";
            ConfigurationState = ConfigState.Configured;

        }
        
        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public int SecondToWait
        {
            get { return (int)nupSeconds.Value; }
            set { nupSeconds.Value = (int)value; }
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

            result += "<Wait>" + SecondToWait.ToString() + "</Wait>";

            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            return "Wait " + SecondToWait.ToString() + " seconds.";
               
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

        private void nupSeconds_ValueChanged(object sender, EventArgs e)
        {
            ConfigurationState = ConfigState.Configured;
        }

        #endregion (Response to Events - Réponse aux évènements)

    }
}
