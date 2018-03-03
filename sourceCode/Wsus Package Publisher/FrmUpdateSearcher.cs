using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateSearcher : Form
    {
        private const string ftpUrl = "ftp://s484673217.onlinehome.fr/LastRelease";
        private const string ftpLogin = "u74323746";
        private const string ftpPassword = @"##ed78Qh@ec2qUja()62-eLvPaQ+";
        private const string ftpRemoteFile = "LastRelease.xml";
        private Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        private VersionInfo remoteVersion = new VersionInfo();
        private struct VersionInfo
        {
            internal Version VersionNumber { get; set; }
            internal string Description { get; set; }
            internal string Url { get; set; }
        }

        public FrmUpdateSearcher()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            remoteVersion.VersionNumber = new Version();
            remoteVersion.Description = string.Empty;
            remoteVersion.Url = string.Empty;
        }

        #region (Internal methods - Méthodes Internes)

        internal void SearchForNewVersionAsync()
        {
            Logger.EnteringMethod();

            if (SearchForNewVersion())
                this.ShowDialog();
        }

        internal bool SearchForNewVersion()
        {
            Logger.EnteringMethod();
            bool result = false;
            string tempFolder = Tools.Utilities.GetTempFolder();
            string localPath = tempFolder + "LastRelease.xml";
            if (DownloadFileFromFtp(localPath))
            {
                Properties.Settings.Default.LastVersionCheck = DateTime.Now.Date;
                Properties.Settings.Default.Save();
                if (IsNewer(localPath))
                    result = true;
            }
            Tools.Utilities.DeleteFolder(tempFolder);
            return result;
        }

        #endregion (Internal methods - Méthodes Internes)

        #region (Private methods - Méthodes privées)

        private bool DownloadFileFromFtp(string localPath)
        {
            Logger.EnteringMethod(localPath);
            FtpClient ftpClient = new FtpClient(ftpUrl, ftpLogin, ftpPassword);

            try
            {
                if (File.Exists(localPath))
                    File.Delete(localPath);
            }
            catch (Exception)
            { }

            if (ftpClient.Download(ftpRemoteFile, localPath))
                if (File.Exists(localPath))
                    return true;

            Logger.Write("Fail to get file from FTP");
            return false;
        }

        private bool IsNewer(string versionFilePath)
        {
            Logger.EnteringMethod();
            VersionInfo lastReleaseInfo = BuildVersionInfoFromXml(versionFilePath);

            int comparisonResult = lastReleaseInfo.VersionNumber.CompareTo(currentVersion);

            switch (comparisonResult)
            {
                case 0:
                    Logger.Write("Same version found");
                    return false;
                case -1:
                    Logger.Write("Older version found !");
                    return false;
                case 1:
                    Logger.Write("New version found");
                    return true;
                default:
                    Logger.Write("Unknown version found");
                    return false;
            }
        }

        private VersionInfo BuildVersionInfoFromXml(string versionFilePath)
        {
            Logger.EnteringMethod();
            try
            {
                StreamReader fileReader = new StreamReader(versionFilePath, UTF8Encoding.UTF8);
                XmlReader reader = XmlReader.Create(fileReader);

                if (!reader.ReadToFollowing("LastRelease"))
                    return remoteVersion;
                if (reader.ReadToFollowing("Version"))
                    remoteVersion.VersionNumber = new Version(reader.ReadElementContentAsString());
                if (reader.ReadToFollowing("Description"))
                {
                    string description = reader.ReadElementContentAsString().Replace(@"\r\n", "\r\n");
                    remoteVersion.Description = description;
                }
                if (reader.ReadToFollowing("Url"))
                    remoteVersion.Url = reader.ReadElementContentAsString();

                reader.Close();
                fileReader.Close();
                File.Delete(versionFilePath);
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            Logger.Write("Version " + remoteVersion.VersionNumber.ToString() + " found on FTP");
            return remoteVersion;
        }
        
        #endregion (Private methods - Méthodes privées)

        #region (Responses to events - Réponses aux événements)

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.Write("Don't want to download the new release");
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.Write("Want to download the new release");
            try
            {
                System.Diagnostics.Process.Start("IExplore.exe", remoteVersion.Url.ToString());
            }
            catch (Exception) { }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmUpdateSearcher_Shown(object sender, EventArgs e)
        {
            txtBxCurrentVersion.Text = currentVersion.ToString();
            txtBxNewVersion.Text = remoteVersion.VersionNumber.ToString();
            txtBxDescription.Text = remoteVersion.Description.ToString();
            this.BringToFront();
        }

        #endregion (Responses to events - Réponses aux événements)
        
    }
}
