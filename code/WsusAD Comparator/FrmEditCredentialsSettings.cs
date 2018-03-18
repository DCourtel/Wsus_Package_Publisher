using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WsusADComparator
{
    public partial class FrmEditCredentialsSettings : Form
    {
        public FrmEditCredentialsSettings()
        {
            InitializeComponent();
            this.Credential = new WPP.Security.Credential();
        }

        public FrmEditCredentialsSettings(string login):this()
        {
            this.txtBxLogin.Text = login;
        }

        #region (Properties)

        public WPP.Security.Credential Credential { get; private set; }
        
        #endregion (Properties)

        #region (Answer to Events)

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Credential.Username = this.txtBxLogin.Text;
            this.Credential.Password = this.txtBxPassword.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtBxPassword.UseSystemPasswordChar = false;
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtBxPassword.UseSystemPasswordChar = true;
        }

        #endregion (Answer to Events)
    }
}
