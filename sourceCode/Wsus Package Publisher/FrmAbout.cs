using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    public partial class FrmAbout : Form
    {
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmAbout).Assembly);

        public FrmAbout()
        {
            Logger.EnteringMethod("FrmAbout");
            InitializeComponent();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblRelease.Text = "Release : " + version.ToString();
            Logger.Write(lblRelease.Text);
            WsusWrapper _wsus = WsusWrapper.GetInstance();
            lblConsoleVersion.Text = "Console Version : " + _wsus.ConsoleVersion.ToString();
            Logger.Write(lblConsoleVersion);
            if (_wsus.IsConnected)
            {
                lblServerVersion.Text = "Server Version : " + _wsus.GetServerVersion().ToString();
                Logger.Write(lblServerVersion);
                txtBxServers.Text = "";
                foreach (Microsoft.UpdateServices.Administration.IDownstreamServer server in _wsus.DownStreamServers)
                    txtBxServers.Text += server.FullDomainName + "\r\n";
                Logger.Write(txtBxServers);
                txtBxUserRole.Text = _wsus.UserRole.ToString();
                Logger.Write(txtBxUserRole);
                SetCertificateStatusInformation(_wsus);
                txtBxExpirationDate.Text = _wsus.CertificateExpirationDate.ToString();
                txtBxKeyLength.Text = _wsus.GetCertificateKeyLength.ToString();
            }
            else
                lblServerVersion.Text = "Server Version : Please, connect first to the server.";
        }

        private void SetCertificateStatusInformation(WsusWrapper _wsus)
        {
            CertificateHelper.CertificateStatus certStatus = _wsus.GetCertificateStatus;
            txtBxCertificateStatus.Text = resMan.GetString(certStatus.ToString());
            Logger.Write(txtBxCertificateStatus);
            switch (certStatus)
            {
                case CertificateHelper.CertificateStatus.Valid:
                    txtBxCertificateStatus.BackColor = SystemColors.Control;
                    break;
                case CertificateHelper.CertificateStatus.NearExpiration:
                    txtBxCertificateStatus.BackColor = Color.Orange;
                    break;
                case CertificateHelper.CertificateStatus.Absent:
                case CertificateHelper.CertificateStatus.Expired:
                case CertificateHelper.CertificateStatus.NotYetValid:
                case CertificateHelper.CertificateStatus.Invalid:
                    txtBxCertificateStatus.BackColor = Color.Red;
                    break;
                default:
                    txtBxCertificateStatus.BackColor = SystemColors.Control;
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo((sender as LinkLabel).Text);
            System.Diagnostics.Process.Start(sInfo);
        }

    }
}
