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
    public partial class PowerManagementElement : GenericElement
    {
        public enum PowerActions
        {
            Unknown,
            Shutdown,
            Reboot
        }

        private int shownHeight = 100;
        private int hiddenHeight = 53;

        public PowerManagementElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.PowerMgmtElement;
            Description = "Allow to shutdown or reboot the computer.";
            cmbBxPowerAction.Items.Clear();
            foreach (object action in Enum.GetValues(typeof(PowerActions)))
            {
                if ((PowerActions)action != PowerActions.Unknown)
                    cmbBxPowerAction.Items.Add(action);
            }
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public PowerActions PowerAction
        {
            get { return cmbBxPowerAction.SelectedIndex != -1 ? (PowerActions)cmbBxPowerAction.SelectedItem : PowerActions.Unknown; }
            set { cmbBxPowerAction.SelectedItem = value; }
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

            result += "<PowerAction>" + this.PowerAction.ToString() + "</PowerAction>";

            return result + "\r\n</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return GetXMLAction();
#endif
            switch (PowerAction)
            {
                case PowerActions.Shutdown:
                    return "Shutdown the computer.";
                case PowerActions.Reboot:
                    return "Reboot the computer.";
            }
            return string.Empty;
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
        
        private void cmbBxPowerAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigurationState = (cmbBxPowerAction.SelectedIndex != -1) ? ConfigState.Configured : ConfigState.NotConfigured;
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Response to Events - Réponse aux évènements)

    }
}
