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
    public partial class FrmEditWsusConnectionSettings : Form
    {
        public FrmEditWsusConnectionSettings()
        {
            InitializeComponent();
        }

        public FrmEditWsusConnectionSettings(string serverName, int serverPort, bool useSSL)
            : this()
        {
            this.txtBxServerName.Text = serverName;
            this.nupServerPort.Value = (decimal)serverPort;
            this.chkBxUseSSL.Checked = useSSL;
        }

        #region (Properties)

        /// <summary>
        /// Gets or Sets the name of the server.
        /// </summary>
        public string ServerName
        {
            get { return this.txtBxServerName.Text; }
            set { this.txtBxServerName.Text = value; }
        }

        /// <summary>
        /// Gets or Sets the port of the server.
        /// </summary>
        public int ServerPort
        {
            get { return (int)this.nupServerPort.Value; }
            set { this.nupServerPort.Value = (decimal)value; }
        }

        /// <summary>
        /// Gets or Sets, whether or not, we need to use SSL to connect to this server.
        /// </summary>
        public bool UseSSL
        {
            get { return this.chkBxUseSSL.Checked; }
            set { this.chkBxUseSSL.Checked = value; }
        }

        #endregion (Properties)

        #region (Answer to Events)

        private void txtBxServerName_TextChanged(object sender, EventArgs e)
        {
            bool emptyServerName = String.IsNullOrEmpty(this.txtBxServerName.Text);
            this.nupServerPort.Enabled = !emptyServerName;
            this.chkBxUseSSL.Enabled = !emptyServerName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        #endregion (Answer to Events)
    }
}
