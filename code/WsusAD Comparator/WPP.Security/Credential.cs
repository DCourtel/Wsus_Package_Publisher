using System;

namespace WPP.Security
{

    /// <summary>
    /// The purpose of this class is to store credential (one Login and one password). The password is secured with SecureString.
    /// </summary>
    public class Credential
    {
        private string _username = String.Empty;
        private System.Security.SecureString _password = Crypto.GetSecureString(String.Empty);

        /// <summary>
        /// Gets or Sets the login
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Gets or Sets the password
        /// </summary>
        public string Password
        {
            get { return Crypto.GetUnsecureString(_password); }
            set { _password = Crypto.GetSecureString(value); }
        }

        /// <summary>
        /// Gets whether or not, if the <see cref="Username"/> of this credential is empty.
        /// </summary>
        public bool IsEmpty { get { return String.IsNullOrEmpty(this._username); } }
    }
}
