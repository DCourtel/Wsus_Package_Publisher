using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Wsus_Package_Publisher
{
    internal partial class Credentials : Form
    {
        internal enum Authentification
        {
            SameAsApplication,
            Specified,
            Ask
        }

        private static byte[] entropy = new byte[] { 14, 28, 65, 124, 45, 68, 201, 39, 47, 35, 124, 165, 223, 0, 45, 198, 234, 222, 204, 106 };
        private string _login = null;
        private string _password = null;
        private bool _rememberCredentials = false;
        private static Credentials instance = null;
        private Authentification _authentificationMethod = Authentification.SameAsApplication;
        private string _credentialNotice = string.Empty;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(Credentials).Assembly);

        private Credentials()
        {
            InitializeComponent();
            UpdateAuthentifactionMethod();
            UpdateCredentialNotice();
        }

        internal static Credentials GetInstance()
        {
            Logger.EnteringMethod();
            if (instance == null)
                instance = new Credentials();

            return instance;
        }

        internal string Login
        {
            get { return _login; }
            private set { _login = value; }
        }

        internal string Password
        {
            get { return _password; }
            private set { _password = value; }
        }

        internal string CredentialNotice
        {
            get { return _credentialNotice; }
            private set
            {
                Logger.Write("CredentialNotice set to : " + value);
                _credentialNotice = value;
            }
        }

        internal Authentification AuthentificationMethod
        {
            get { return _authentificationMethod; }
            private set
            {
                switch (value)
                {
                    case Authentification.SameAsApplication:
                        Login = null;
                        Password = null;
                        break;
                    case Authentification.Specified:
                        Login = Properties.Settings.Default.RemoteAdminLogin;
                        Password = GetRemoteAdminPassword();
                        break;
                    case Authentification.Ask:
                        RememberCredentials = false;
                        break;
                    default:
                        break;
                }
                _authentificationMethod = value;
            }
        }

        private void UpdateCredentialNotice()
        {
            switch (AuthentificationMethod)
            {
                case Authentification.SameAsApplication:
                    CredentialNotice = resMan.GetString("CredentialUseSameAsApplication");
                    break;
                case Authentification.Specified:
                    CredentialNotice = resMan.GetString("CredentialUseSpecified") + " (" + this.Login + ")";
                    break;
                case Authentification.Ask:
                    if (this.RememberCredentials)
                        CredentialNotice = resMan.GetString("CredentialWillUse") + this.Login;
                    else
                        CredentialNotice = resMan.GetString("CredentialWillBeAsk");
                    break;
                default:
                    break;
            }
        }

        internal bool RememberCredentials
        {
            get { return _rememberCredentials; }
            private set { _rememberCredentials = value; }
        }

        internal bool InitializeCredential()
        {
            switch (AuthentificationMethod)
            {
                case Authentification.Ask:
                    if (!RememberCredentials)
                        if (this.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                            return false;
                    break;
                case Authentification.Specified:
                    Login = Properties.Settings.Default.RemoteAdminLogin;
                    Password = GetRemoteAdminPassword();
                    break;
                case Authentification.SameAsApplication:
                    Login = null;
                    Password = null;
                    break;
                default:
                    break;
            }
            return true;
        }

        internal void UpdateAuthentifactionMethod()
        {
            switch (Properties.Settings.Default.Credential)
            {
                case "SameAsApplication":
                    AuthentificationMethod = Authentification.SameAsApplication;
                    break;
                case "Specified":
                    AuthentificationMethod = Authentification.Specified;
                    break;
                case "Ask":
                    AuthentificationMethod = Authentification.Ask;
                    break;
                default:
                    break;
            }
            UpdateCredentialNotice();
            if (AuthentificationMethodChange != null)
                AuthentificationMethodChange();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Login = txtBxLogin.Text;
            Password = txtBxPassword.Text;
            UpdateCredentialNotice();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void chkBxRememberCredentials_CheckedChanged(object sender, EventArgs e)
        {
            RememberCredentials = chkBxRememberCredentials.Checked;
        }

        internal static string GetRemoteAdminPassword()
        {
            string uncryptedPassword = string.Empty;

            try
            {
                string encryptedPasword = Properties.Settings.Default.RemoteAdminPassword;

                string[] encryptedCharacters = encryptedPasword.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                byte[] encryptedBytes = new byte[encryptedCharacters.Length];
                for (int i = 0; i < encryptedCharacters.Length; i++)
                {
                    encryptedBytes[i] = byte.Parse(encryptedCharacters[i]);
                }

                byte[] clearText = ProtectedData.Unprotect(encryptedBytes, entropy, DataProtectionScope.CurrentUser);

                uncryptedPassword = System.Text.Encoding.Unicode.GetString(clearText);
            }
            catch (Exception) { }

            return uncryptedPassword;
        }

        internal static void SetRemoteAdminPassword(string password)
        {
            try
            {
                byte[] unEncryptedPassword = System.Text.Encoding.Unicode.GetBytes(password);
                byte[] ciphertext = ProtectedData.Protect(unEncryptedPassword, entropy, DataProtectionScope.CurrentUser);

                StringBuilder strBuilder = new StringBuilder();
                foreach (byte character in ciphertext)
                {
                    strBuilder.Append(string.Concat(character, ";"));
                }
                Properties.Settings.Default.RemoteAdminPassword = strBuilder.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception) { }
        }

        public delegate void AuthentificationMethodChangeEventHandler();
        public event AuthentificationMethodChangeEventHandler AuthentificationMethodChange;

    }
}
