using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Wsus_Package_Publisher
{
    public partial class FrmSendDebugInfo : Form
    {
        private const string ftpUrl = "ftp://s484673217.onlinehome.fr/Support";
        private const string ftpLogin = "u74323746";
        private const string ftpPassword = @"##ed78Qh@ec2qUja()62-eLvPaQ+";
        internal enum SendingReasons
        {
            UnexpectedError,
            AskForSupport
        }

        private SendingReasons _reason = SendingReasons.UnexpectedError;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmSendDebugInfo).Assembly);

        public FrmSendDebugInfo()
        {
            Logger.EnteringMethod("FrmSendDebugInfo");
            InitializeComponent();
        }

        internal SendingReasons SendingReason
        {
            get { return _reason; }
            set
            {
                switch (value)
                {
                    case SendingReasons.UnexpectedError:
                        pctBxIcone.Image = Properties.Resources.WentWrong;
                        txtBxSendDebugInfo.Text = resMan.GetString("WentWrong");
                        break;
                    case SendingReasons.AskForSupport:
                        pctBxIcone.Image = Properties.Resources.AskingSupport;
                        txtBxSendDebugInfo.Text = resMan.GetString("AskForSupport");
                        break;
                }
                _reason = value;
            }
        }

        internal string ErrorMessage
        {
            private get;
            set;
        }

        internal string EMail
        {
            get { return txtBxMail.Text; }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(SendingReason.ToString());
            Logger.Write("Comments : " + txtBxComments.Text + "\r\n");
            txtBxComments.Enabled = false;
            txtBxMail.Enabled = false;
            lnkLblShowInformations.Enabled = false;
            btnNo.Enabled = false;
            btnYes.Enabled = false;
            txtBxComments.Text = resMan.GetString("SendingInformations");
            txtBxComments.Refresh();
            txtBxMail.Refresh();

            if (!string.IsNullOrEmpty(EMail))
                Logger.Write(EMail);

            string sourceFile = Logger.FullPath;
            FileInfo fileInfo = new FileInfo(sourceFile);

            try
            {
                using (FileStream inFile = fileInfo.OpenRead())
                {
                    using (FileStream outFile = File.Create(sourceFile + ".gz"))
                    {
                        using (System.IO.Compression.GZipStream Compress = new System.IO.Compression.GZipStream(outFile, System.IO.Compression.CompressionMode.Compress))
                        {
                            inFile.CopyTo(Compress);
                        }
                    }
                }

                FtpClient ftp = new FtpClient(ftpUrl, ftpLogin, ftpPassword);
                string date = DateTime.Now.ToString().Replace('/', '-');

                if (!ftp.Upload(fileInfo.Name + "-" + date + ".log.gz", sourceFile + ".gz"))
                {
                    MessageBox.Show("Failed to send Debug Informations to " + ftpUrl +
                        "\r\nPlease send this file\r\n" + sourceFile + ".gz" + "\r\nto\r\npackage.publisher@free.fr", "Network error occured");
                    Logger.Write("Failed to send Debug Info !");
                }
                else
                {
                    MessageBox.Show(resMan.GetString("InformationsSent"));
                    File.Delete(sourceFile + ".gz");
                }

            }
            catch (Exception ex)
            {
                Logger.Write("Failed to send Debug Info !\r\n" + ex.Message);
            }

            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void lnkLblShowInformations_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.EnteringMethod();
            System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("notepad.exe");

            sInfo.Arguments = Logger.FullPath;
            System.Diagnostics.Process.Start(sInfo);
        }

        private void FrmSendDebugInfo_Shown(object sender, EventArgs e)
        {
            txtBxSendDebugInfo.Select(0, 0);
            if (!String.IsNullOrEmpty(this.ErrorMessage))
                txtBxComments.Text = this.ErrorMessage;
            txtBxComments.Select();
        }
    }
}
