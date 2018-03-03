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
    public partial class ServiceElement : GenericElement
    {
        private int shownHeight = 300;
        private int hiddenHeight = 53;

        public enum Actions
        {
            ChangeStartingMode,
            Start,
            Stop,
            Register,
            Unregister,
            Undefined
        }

        public enum StartupModes
        {
            Automatic,
            Disabled,
            Manual,
            Undefined
        }

        public enum StartingAccounts
        {
            LocalService,
            LocalSystem,
            NetworkService,
            User,
            Undefined
        }

        public ServiceElement()
            : base()
        {
            InitializeComponent();
            Image = Properties.Resources.ServiceElement;
            Description = "Allow to Start, Stop, Unregister, change starting mode of Services.";

            foreach (object action in Enum.GetValues(typeof(Actions)))
            {
                if ((Actions)action != Actions.Undefined)
                    cmbBxAction.Items.Add(action);
            }
            ServiceAction = Actions.Undefined;

            foreach (object startup in Enum.GetValues(typeof(StartupModes)))
            {
                if ((StartupModes)startup != StartupModes.Undefined)
                    cmbBxStartupMode.Items.Add(startup);
            }
            StartupMode = StartupModes.Undefined;

            foreach (object starting in Enum.GetValues(typeof(StartingAccounts)))
            {
                if ((StartingAccounts)starting != StartingAccounts.Undefined)
                    cmbBxStartingAccount.Items.Add(starting);
            }
            StartingAccount = StartingAccounts.Undefined;
        }

        #region (Public Properties - Propriétés public)

        public override string ActionDescription
        {
            get { return GetActionDescription(); }
        }

        public Actions ServiceAction
        {
            get { return cmbBxAction.SelectedIndex != -1 ? (Actions)cmbBxAction.SelectedItem : Actions.Undefined; }
            set
            {
                if (value != Actions.Undefined)
                    cmbBxAction.SelectedItem = value;
                else
                    cmbBxAction.SelectedIndex = -1;
            }
        }

        public string ServiceName
        {
            get { return txtBxServiceName.Text; }
            set { txtBxServiceName.Text = value; }
        }

        public string PathToEXE
        {
            get { return txtBxPathToEXE.Text; }
            set { txtBxPathToEXE.Text = value; }
        }

        public StartupModes StartupMode
        {
            get { return cmbBxStartupMode.SelectedIndex != -1 ? (StartupModes)cmbBxStartupMode.SelectedItem : StartupModes.Undefined; }
            set
            {
                if (value != StartupModes.Undefined)
                    cmbBxStartupMode.SelectedItem = value;
                else
                    cmbBxStartupMode.SelectedIndex = -1;
            }
        }

        public StartingAccounts StartingAccount
        {
            get { return cmbBxStartingAccount.SelectedIndex != -1 ? (StartingAccounts)cmbBxStartingAccount.SelectedItem : StartingAccounts.Undefined; }
            set
            {
                if (value != StartingAccounts.Undefined)
                    cmbBxStartingAccount.SelectedItem = value;
                else
                    cmbBxStartingAccount.SelectedIndex = -1;
            }
        }

        public string Login
        {
            get { return txtBxLogin.Text; }
            set { txtBxLogin.Text = value; }
        }

        public string Password
        {
            get { return txtBxPassword.Text; }
            set { txtBxPassword.Text = value; }
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

            result += "<ServiceAction>" + this.ServiceAction.ToString() + "</ServiceAction>\r\n<ServiceName>" + this.ServiceName + "</ServiceName>\r\n<PathToEXE>" + this.PathToEXE + "</PathToEXE>\r\n<StartupMode>" +
                this.StartupMode.ToString() + "</StartupMode>\r\n<StartingAccount>" + this.StartingAccount.ToString() + "</StartingAccount>\r\n<Login>" + this.Login + "</Login>\r\n<Password>" + this.Password + "</Password>\r\n";

            return result + "</Action>";
        }

        #endregion (Public Methods - Méthodes public)

        #region (Private Methods - Méthodes Privées)

        private string GetActionDescription()
        {
#if(DEBUG)
            return ConfigurationState + "\r\n" + GetXMLAction();
#endif
            switch (ServiceAction)
            {
                case Actions.ChangeStartingMode:
                    return "Change the Starting mode of : " + txtBxServiceName.Text;
                case Actions.Start:
                    return "Start service : " + txtBxServiceName.Text;
                case Actions.Stop:
                    return "Stop service : " + txtBxServiceName.Text;
                case Actions.Register:
                    return "Register service : " + txtBxServiceName.Text + " from EXE : " + txtBxPathToEXE.Text + " with Starting Mode : " + StartupMode.ToString();
                case Actions.Unregister:
                    return "Unregister service : " + txtBxServiceName.Text;
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
            switch (ServiceAction)
            {
                case Actions.Undefined:
                    ConfigurationState = ConfigState.Misconfigured;
                    break;
                case Actions.ChangeStartingMode:
                    ConfigurationState = (!string.IsNullOrEmpty(txtBxServiceName.Text) && cmbBxStartupMode.SelectedIndex != -1) ? ConfigState.Configured : ConfigState.Misconfigured;
                    break;
                case Actions.Unregister:
                case Actions.Start:
                case Actions.Stop:
                    ConfigurationState = !string.IsNullOrEmpty(txtBxServiceName.Text) ? ConfigState.Configured : ConfigState.Misconfigured;
                    break;
                case Actions.Register:
                    switch (StartingAccount)
                    {
                        case StartingAccounts.LocalService:
                        case StartingAccounts.LocalSystem:
                        case StartingAccounts.NetworkService:
                            ConfigurationState = (!string.IsNullOrEmpty(txtBxServiceName.Text) && !string.IsNullOrEmpty(txtBxPathToEXE.Text) && cmbBxStartupMode.SelectedIndex != -1) ? ConfigState.Configured : ConfigState.Misconfigured;
                            break;
                        case StartingAccounts.User:
                            ConfigurationState = (!string.IsNullOrEmpty(txtBxServiceName.Text) && !string.IsNullOrEmpty(txtBxPathToEXE.Text) && cmbBxStartupMode.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxLogin.Text) && !string.IsNullOrEmpty(txtBxPassword.Text)) ? ConfigState.Configured : ConfigState.Misconfigured;
                            break;
                    }
                    break;
                default:
                    ConfigurationState = ConfigState.Misconfigured;
                    break;
            }
            btnOk.Enabled = (ConfigurationState == ConfigState.Configured);
        }

        #endregion (Private Methods - Méthodes Privées)

        #region {Responses to events - Réponses aux événements}

        private void cmbBxAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ServiceAction)
            {
                case Actions.ChangeStartingMode:
                    txtBxServiceName.Enabled = true;
                    txtBxPathToEXE.Enabled = false;
                    cmbBxStartupMode.Enabled = true;
                    cmbBxStartingAccount.Enabled = false;
                    txtBxLogin.Enabled = false;
                    txtBxPassword.Enabled = false;
                    break;
                case Actions.Unregister:
                case Actions.Stop:
                case Actions.Start:
                    txtBxServiceName.Enabled = true;
                    txtBxPathToEXE.Enabled = false;
                    cmbBxStartupMode.Enabled = false;
                    cmbBxStartingAccount.Enabled = false;
                    txtBxLogin.Enabled = false;
                    txtBxPassword.Enabled = false;
                    break;
                case Actions.Register:
                    txtBxServiceName.Enabled = true;
                    txtBxPathToEXE.Enabled = true;
                    cmbBxStartupMode.Enabled = true;
                    cmbBxStartingAccount.Enabled = true;
                    txtBxLogin.Enabled = false;
                    txtBxPassword.Enabled = false;
                    break;
            }
            ValidateData();
        }

        private void cmbBxStartingAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (StartingAccount)
            {
                case StartingAccounts.LocalService:
                case StartingAccounts.LocalSystem:
                case StartingAccounts.NetworkService:
                    txtBxLogin.Enabled = false;
                    txtBxPassword.Enabled = false;
                    break;
                case StartingAccounts.User:
                    txtBxLogin.Enabled = true;
                    txtBxPassword.Enabled = true;
                    break;
            }
            ValidateData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.element_DoubleClick(this, e);
        }

        private void txtBxServiceName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxPathToEXE_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void cmbBxStartupMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxLogin_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion {Responses to events - Réponses aux événements}

    }
}
