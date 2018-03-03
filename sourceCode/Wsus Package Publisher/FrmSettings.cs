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
    internal partial class FrmSettings : Form
    {
        internal enum MakeVisibleInWsusPolicy
        {
            Never,
            LetMeChoose,
            Always
        }

        internal enum PingStandards
        {
            IPv4,
            IPv6,
            IPv6IPv4
        }

        private const string ftpUrl = "ftp://s484673217.onlinehome.fr";
        private const string ftpLogin = "u74323746";
        private const string ftpPassword = @"##ed78Qh@ec2qUja()62-eLvPaQ+";
        private const string ftpRemoteFile = "LastRelease/LastRelease.xml";
        private const string ftpRemoteFileForFtpUploadTest = "Upload/";

        private List<WsusServer> _serverlist = new List<WsusServer>();
        bool supportIPv6 = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmSettings).Assembly);

        internal FrmSettings()
        {
            Logger.EnteringMethod("FrmSettings");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            Tools.Utilities _utilities = Tools.Utilities.GetInstance();

            supportIPv6 = System.Net.Sockets.Socket.OSSupportsIPv6;
            InitializeComponent();

            cmbBxUpdateDefaultAction.Items.Clear();
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Approve")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Revise")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Decline")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Expire")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Delete")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("Resign")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("CreateSupersedingUpdate")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("ExportThisUpdate")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("ShowInWsusConsole")));
            cmbBxUpdateDefaultAction.Items.Add(_utilities.GetLocalizedString(("HideInWsusConsole")));
        }

        internal List<WsusServer> ServerList
        {
            get { return _serverlist; }
            private set { _serverlist = value; }
        }

        internal List<WsusServer> LoadServerSettings()
        {
            Logger.EnteringMethod();
            ServerList.Clear();
            cmbBxServerList.Items.Clear();

            if (System.IO.File.Exists("Options.xml"))
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.Load("Options.xml");
                System.Xml.XmlNodeList nodeList = document.GetElementsByTagName("Server");
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    if (node.HasChildNodes)
                    {
                        WsusServer serverWsus = new WsusServer();
                        foreach (System.Xml.XmlNode childNode in node.ChildNodes)
                        {
                            switch (childNode.Name)
                            {
                                case "Name":
                                    serverWsus.Name = childNode.InnerText;
                                    Logger.Write("Name : " + serverWsus.Name);
                                    break;
                                case "IsLocal":
                                    bool isLocal;
                                    if (bool.TryParse(childNode.InnerText, out isLocal))
                                    {
                                        serverWsus.IsLocal = isLocal;
                                        Logger.Write("IsLocal : " + serverWsus.IsLocal.ToString());
                                    }
                                    break;
                                case "Port":
                                    int port;
                                    if (int.TryParse(childNode.InnerText, out port))
                                    {
                                        serverWsus.Port = port;
                                        Logger.Write("Port : " + serverWsus.Port.ToString());
                                    }
                                    break;
                                case "UseSSL":
                                    bool useSSL;
                                    if (bool.TryParse(childNode.InnerText, out useSSL))
                                    {
                                        serverWsus.UseSSL = useSSL;
                                        Logger.Write("UseSSL : " + serverWsus.UseSSL.ToString());
                                    }
                                    break;
                                case "IgnoreCertErrors":
                                    bool ignoreCertErrors;
                                    if (bool.TryParse(childNode.InnerText, out ignoreCertErrors))
                                    {
                                        serverWsus.IgnoreCertificateErrors = ignoreCertErrors;
                                        Logger.Write("ignoreCertErrors : " + serverWsus.IgnoreCertificateErrors.ToString());
                                    }
                                    break;
                                case "DeadLineDaysSpan":
                                    int day;
                                    if (int.TryParse(childNode.InnerText, out day))
                                        serverWsus.DeadLineDaysSpan = day;
                                    break;
                                case "DeadLineHour":
                                    int hour;
                                    if (int.TryParse(childNode.InnerText, out hour))
                                        serverWsus.DeadLineHour = hour;
                                    break;
                                case "DeadLineMinute":
                                    int minute;
                                    if (int.TryParse(childNode.InnerText, out minute))
                                        serverWsus.DeadLineMinute = minute;
                                    break;
                                case "MetaGroup":
                                    serverWsus.MetaGroups.Add(GetMetaGroupFromXml(childNode));
                                    break;
                                case "VisibleInWsusConsole":
                                    string option = "Never";
                                    option = childNode.InnerText;
                                    if (option == "Never")
                                        serverWsus.VisibleInWsusConsole = MakeVisibleInWsusPolicy.Never;
                                    if (option == "Always")
                                        serverWsus.VisibleInWsusConsole = MakeVisibleInWsusPolicy.Always;
                                    if (option == "LetMeChoose")
                                        serverWsus.VisibleInWsusConsole = MakeVisibleInWsusPolicy.LetMeChoose;
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (serverWsus.IsValid())
                        {
                            Logger.Write("Adding server : " + serverWsus.Name);
                            ServerList.Add(serverWsus);
                            cmbBxServerList.Items.Add(serverWsus);
                        }
                    }
                }
            }
            if (ServerList.Count == 0)
            {
                Logger.Write("Server count = 0");

                if (IsWsusInstalledOnLocalMachine())
                {
                    Logger.Write("Local Machine is Wsus.");
                    WsusServer serverWsus = new WsusServer();
                    serverWsus.Name = GetLocalMachineName();
                    serverWsus.IsLocal = true;
                    serverWsus.Port = 80;
                    serverWsus.UseSSL = false;
                    serverWsus.DeadLineDaysSpan = 0;
                    serverWsus.DeadLineHour = 0;
                    serverWsus.DeadLineMinute = 0;
                    serverWsus.VisibleInWsusConsole = MakeVisibleInWsusPolicy.Never;

                    ServerList.Add(serverWsus);
                    cmbBxServerList.Items.Add(serverWsus);
                    SaveSettings(ServerList);
                    MessageBox.Show(resMan.GetString("AddAutomaticallyThisServer"));
                }
                else
                {
                    Logger.Write("No server are configured. Showing UI.");
                    if (this.Visible == false)
                        this.ShowDialog();
                }
            }

            Logger.Write("Returning " + ServerList.Count + " Servers.");
            return ServerList;
        }

        private string GetLocalMachineName()
        {
            Logger.EnteringMethod();
            return Environment.MachineName;
        }

        private bool IsWsusInstalledOnLocalMachine()
        {
            Logger.EnteringMethod();
            try
            {
                Microsoft.Win32.RegistryKey HKLM = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey setupKey = HKLM.OpenSubKey(@"SOFTWARE\Microsoft\Update Services\Server\Setup", false);
                object portNumber = setupKey.GetValue("PortNumber");
                if (portNumber != null)
                {
                    Logger.Write("Yes");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            Logger.Write("No");
            return false;
        }

        private MetaGroup GetMetaGroupFromXml(System.Xml.XmlNode node)
        {
            Logger.EnteringMethod(node.InnerText);
            MetaGroup newMetaGroup = new MetaGroup();

            foreach (System.Xml.XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "Name":
                        newMetaGroup.Name = childNode.InnerText;
                        Logger.Write("Name : " + newMetaGroup.Name);
                        break;
                    case "InnerMetaGroup":
                        MetaGroup innerMetaGroup = new MetaGroup();
                        innerMetaGroup.Name = childNode.InnerText;
                        newMetaGroup.InnerMetaGroups.Add(innerMetaGroup);
                        Logger.Write("InnerMetaGroups : " + innerMetaGroup.Name);
                        break;
                    case "InnerComputerGroup":
                        newMetaGroup.InnerComputerGroups.Add(new ComputerGroup(childNode.InnerText, Guid.NewGuid()));
                        Logger.Write("InnerComputerGroups : " + childNode.InnerText);
                        break;
                    default:
                        break;
                }
            }

            return newMetaGroup;
        }

        internal void SaveSettings(List<WsusServer> wsusServers)
        {
            Logger.EnteringMethod(wsusServers.Count.ToString());
            try
            {
                if (System.IO.File.Exists("Options.xml"))
                    System.IO.File.Move("Options.xml", "Options.xml.bak");
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when backuping options.xml.\r\n" + ex.Message);
                MessageBox.Show(ex.Message);
            }

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement rootElement = (System.Xml.XmlElement)xmlDoc.AppendChild(xmlDoc.CreateElement("WsusPackagePublisher"));

            foreach (WsusServer server in wsusServers)
            {
                Logger.Write(server.Name);
                System.Xml.XmlElement serverElement = (System.Xml.XmlElement)rootElement.AppendChild(xmlDoc.CreateElement("Server"));
                serverElement.AppendChild(xmlDoc.CreateElement("Name")).InnerText = server.Name;
                serverElement.AppendChild(xmlDoc.CreateElement("IsLocal")).InnerText = server.IsLocal.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("Port")).InnerText = server.Port.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("UseSSL")).InnerText = server.UseSSL.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("IgnoreCertErrors")).InnerText = server.IgnoreCertificateErrors.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineDaysSpan")).InnerText = server.DeadLineDaysSpan.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineHour")).InnerText = server.DeadLineHour.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("DeadLineMinute")).InnerText = server.DeadLineMinute.ToString();
                serverElement.AppendChild(xmlDoc.CreateElement("VisibleInWsusConsole")).InnerText = server.VisibleInWsusConsole.ToString();
                if (server.MetaGroups.Count != 0)
                    SaveMetaGroup(xmlDoc, server, serverElement);
            }

            try
            {
                xmlDoc.Save("Options.xml");

                if (System.IO.File.Exists("Options.xml.bak"))
                    System.IO.File.Delete("Options.xml.bak");
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when saving options.xml or deleting option.xml.bak.\r\n" + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveMetaGroup(System.Xml.XmlDocument xmlDoc, WsusServer server, System.Xml.XmlElement serverElement)
        {
            Logger.EnteringMethod(serverElement.ToString());
            foreach (MetaGroup metaGroup in server.MetaGroups)
            {
                Logger.Write(metaGroup.Name);
                System.Xml.XmlElement metaGroupElement = xmlDoc.CreateElement("MetaGroup");
                metaGroupElement.AppendChild(xmlDoc.CreateElement("Name")).InnerText = metaGroup.Name;
                foreach (MetaGroup innerMetaGroup in metaGroup.InnerMetaGroups)
                    metaGroupElement.AppendChild(xmlDoc.CreateElement("InnerMetaGroup")).InnerText = innerMetaGroup.Name;
                foreach (ComputerGroup innerComputerGroup in metaGroup.InnerComputerGroups)
                    metaGroupElement.AppendChild(xmlDoc.CreateElement("InnerComputerGroup")).InnerText = innerComputerGroup.Name;

                serverElement.AppendChild(metaGroupElement);
            }
        }

        private void SaveSettings()
        {
            Logger.EnteringMethod();
            SaveSettings(ServerList);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnEditServer.PerformClick();
            _serverlist.Clear();
            foreach (object obj in cmbBxServerList.Items)
            {
                WsusServer server = (WsusServer)obj;
                _serverlist.Add(server);
            }
            SaveSettings();
            if (rdBtnSameAsApplication.Checked)
                Properties.Settings.Default.Credential = "SameAsApplication";
            if (rdBtnSpecified.Checked)
            {
                Properties.Settings.Default.Credential = "Specified";
                Properties.Settings.Default.RemoteAdminLogin = txtBxLogin.Text;
                Credentials.SetRemoteAdminPassword(txtBxPassword.Text);
            }
            if (rdBtnAsk.Checked)
                Properties.Settings.Default.Credential = "Ask";

            Credentials cred = Credentials.GetInstance();
            cred.UpdateAuthentifactionMethod();

            Properties.Settings.Default.DownloadedColor = lblDownloaded.BackColor;
            Properties.Settings.Default.FailedColor = lblFailed.BackColor;
            Properties.Settings.Default.InstalledColor = lblInstalled.BackColor;
            Properties.Settings.Default.InstalledPendingRebootColor = lblInstalledPendingReboot.BackColor;
            Properties.Settings.Default.NotInstalledColor = lblNotInstalled.BackColor;
            Properties.Settings.Default.UnknownColor = lblUnknown.BackColor;
            Properties.Settings.Default.NotApplicableColor = lblNotApplicable.BackColor;
            Properties.Settings.Default.ShowNonLocallyPublishedUpdates = chkBxShowNonLocallyPublishedUpdates.Checked;
            Properties.Settings.Default.PreventAutoApproval = chkBxPreventAutoApproval.Checked;
            Properties.Settings.Default.OpenWindowsUpdateLogWith = lnkLblOpenWith.Text;
            Properties.Settings.Default.ConnectToLastUsedServer = chkBxConnectToLastUsedServer.Checked;

            if (txtBxDefaultRebootMessage.Text != Properties.Settings.Default.PersonalizedRebootMessage)
                Properties.Settings.Default.PersonalizedRebootMessage = txtBxDefaultRebootMessage.Text;

            if (chkBxConnectToLastUsedServer.Checked)
            {
                WsusWrapper wsus = WsusWrapper.GetInstance();
                if (wsus.IsConnected)
                    Properties.Settings.Default.LastUsedServerName = wsus.CurrentServer.Name;
            }

            if (rdBtnLastUsed.Checked)
            {
                Properties.Settings.Default.UpdateFilePathSetting = 0;
            }
            else
            {
                Properties.Settings.Default.UpdateFilePathSetting = 1;
                Properties.Settings.Default.LastUpdateFolder = txtBxUseThisPath.Text;
                Properties.Settings.Default.AdditionalUpdateFilePathAsMainFile = chkBxSamePathForAdditionnal.Checked;
            }

            string pingStd = "IPv4";
            if (rdBtnIPv4.Checked)
                pingStd = "IPv4";
            if (rdBtnIPv6.Checked)
                pingStd = "IPv6";
            if (rdBtnIPv6IPv4.Checked)
                pingStd = "IPv6IPv4";
            Properties.Settings.Default.PingStandard = pingStd;
            string selectedDefaultAction = cmbBxUpdateDefaultAction.SelectedItem.ToString();

            Properties.Settings.Default.UpdateDefaultAction = "Revise";
            if (selectedDefaultAction == resMan.GetString("Revise"))
                Properties.Settings.Default.UpdateDefaultAction = "Revise";
            if (selectedDefaultAction == resMan.GetString("Approve"))
                Properties.Settings.Default.UpdateDefaultAction = "Approve";
            if (selectedDefaultAction == resMan.GetString("Decline"))
                Properties.Settings.Default.UpdateDefaultAction = "Decline";
            if (selectedDefaultAction == resMan.GetString("Expire"))
                Properties.Settings.Default.UpdateDefaultAction = "Expire";
            if (selectedDefaultAction == resMan.GetString("Delete"))
                Properties.Settings.Default.UpdateDefaultAction = "Delete";
            if (selectedDefaultAction == resMan.GetString("Resign"))
                Properties.Settings.Default.UpdateDefaultAction = "Resign";
            if (selectedDefaultAction == resMan.GetString("CreateSupersedingUpdate"))
                Properties.Settings.Default.UpdateDefaultAction = "CreateSupersedingUpdate";
            if (selectedDefaultAction == resMan.GetString("ExportThisUpdate"))
                Properties.Settings.Default.UpdateDefaultAction = "ExportThisUpdate";
            if (selectedDefaultAction == resMan.GetString("ShowInWsusConsole"))
                Properties.Settings.Default.UpdateDefaultAction = "ShowInWsusConsole";
            if (selectedDefaultAction == resMan.GetString("HideInWsusConsole"))
                Properties.Settings.Default.UpdateDefaultAction = "HideInWsusConsole";

            Properties.Settings.Default.IgnoreVersionMismatch = this.chkBxIgnoreVersionMismatch.Checked;

            SaveProxySettings();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SaveProxySettings()
        {
            if (rdBtnHTTPProxyNoProxy.Checked)
                Properties.Settings.Default.HTTPProxyPolicy = "NoProxy";
            if (rdBtnHTTPProxyCustomSettings.Checked)
            {
                Properties.Settings.Default.HTTPProxyPolicy = "CustomSettings";
                Properties.Settings.Default.HTTPProxyServerName = txtBxHTTPProxyServerName.Text;
                Properties.Settings.Default.HTTPProxyPort = (int)nupHTTPProxyServerPort.Value;
                Properties.Settings.Default.HTTPProxyLogin = txtBxHTTPProxyLogin.Text;
                Properties.Settings.Default.HTTPProxyPassword = txtBxHTTPProxyPassword.Text;
            }

            if (rdBtnFtpProxyNoProxy.Checked)
                Properties.Settings.Default.FTPProxyPolicy = "NoProxy";
            if (rdBtnFtpProxyAsAbove.Checked)
                Properties.Settings.Default.FTPProxyPolicy = "SameAsAbove";

            Properties.Settings.Default.Save();
        }

        private void ValidateData()
        {
            btnAddServer.Enabled = (!string.IsNullOrEmpty(txtBxServerName.Text) && (cmbBxConnectionPort.SelectedItem != null || !string.IsNullOrEmpty(cmbBxConnectionPort.Text) || chkBxConnectToLocalServer.Checked));
            btnEditServer.Enabled = btnAddServer.Enabled;
            btnRemoveServer.Enabled = (cmbBxServerList.Items.Count != 0 && cmbBxServerList.SelectedItem != null && !string.IsNullOrEmpty(cmbBxServerList.SelectedItem.ToString()));
            btnOk.Enabled = (cmbBxServerList.Items.Count != 0);
            rdBtnVisibleAlways.Enabled = chkBxConnectToLocalServer.Checked;
            rdBtnVisibleChoose.Enabled = chkBxConnectToLocalServer.Checked;
            rdBtnVisibleNever.Enabled = chkBxConnectToLocalServer.Checked;
            this.AcceptButton = btnOk;
        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverToRemove = (WsusServer)cmbBxServerList.SelectedItem;
                if (ServerList.Contains(serverToRemove))
                {
                    Logger.Write("Remove server : " + serverToRemove.Name);
                    ServerList.Remove(serverToRemove);
                    cmbBxServerList.Items.Remove(serverToRemove);
                    if (cmbBxServerList.Items.Count != 0)
                        cmbBxServerList.SelectedIndex = 0;
                    ValidateData();
                }
            }
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            int port = 443;
            if (!chkBxConnectToLocalServer.Checked)
            {
                if (cmbBxConnectionPort.SelectedItem != null)
                    int.TryParse(cmbBxConnectionPort.SelectedItem.ToString(), out port);
                else if (!string.IsNullOrEmpty(cmbBxConnectionPort.Text))
                    int.TryParse(cmbBxConnectionPort.Text, out port);
            }
            WsusServer serverToAdd = new WsusServer(txtBxServerName.Text, chkBxConnectToLocalServer.Checked, port, chkBxUseSSL.Checked, (int)nupDeadLineDaysSpan.Value, (int)nupDeadLineHour.Value, (int)nupDeadLineMinute.Value);
            cmbBxServerList.Items.Add(serverToAdd);
            cmbBxServerList.SelectedIndex = cmbBxServerList.Items.Count - 1;
            Logger.Write("Adding server : " + serverToAdd.Name);
            ServerList.Add(serverToAdd);
            ValidateData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            LoadServerSettings();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmbBxConnectionPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(cmbBxConnectionPort);
            if (cmbBxConnectionPort.SelectedItem != null)
            {
                switch (cmbBxConnectionPort.SelectedItem.ToString())
                {
                    case "80":
                    case "8530":
                        chkBxUseSSL.Checked = false;
                        break;
                    case "443":
                    case "8531":
                        chkBxUseSSL.Checked = true;
                        break;
                    default:
                        chkBxUseSSL.Checked = false;
                        break;
                }
            }
            ValidateData();
        }

        private void cmbBxConnectionPort_TextChanged(object sender, EventArgs e)
        {
            int port = 0;
            if (!string.IsNullOrEmpty(cmbBxConnectionPort.Text) && !int.TryParse(cmbBxConnectionPort.Text, out port))
                cmbBxConnectionPort.Text = string.Empty;
            ValidateData();
        }

        private void cmbBxServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(cmbBxServerList);
            btnRemoveServer.Enabled = !string.IsNullOrEmpty(cmbBxServerList.SelectedItem.ToString());

            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverWsus = (WsusServer)cmbBxServerList.SelectedItem;
                txtBxServerName.Text = serverWsus.Name;
                chkBxConnectToLocalServer.Checked = serverWsus.IsLocal;
                if (!cmbBxConnectionPort.Items.Contains(serverWsus.Port.ToString()))
                {
                    cmbBxConnectionPort.Items.Add(serverWsus.Port.ToString());
                }
                cmbBxConnectionPort.SelectedItem = serverWsus.Port.ToString();
                chkBxUseSSL.Checked = serverWsus.UseSSL;
                chkBxIgnoreCertificateErrors.Checked = serverWsus.IgnoreCertificateErrors;
                nupDeadLineDaysSpan.Value = serverWsus.DeadLineDaysSpan;
                nupDeadLineHour.Value = serverWsus.DeadLineHour;
                nupDeadLineMinute.Value = serverWsus.DeadLineMinute;
                switch (serverWsus.VisibleInWsusConsole)
                {
                    case MakeVisibleInWsusPolicy.Never:
                        rdBtnVisibleNever.Checked = true;
                        break;
                    case MakeVisibleInWsusPolicy.LetMeChoose:
                        rdBtnVisibleChoose.Checked = true;
                        break;
                    case MakeVisibleInWsusPolicy.Always:
                        rdBtnVisibleAlways.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void txtBxServerName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void FrmSettings_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            switch (Properties.Settings.Default.Credential)
            {
                case "SameAsApplication":
                    rdBtnSameAsApplication.Checked = true;
                    break;
                case "Specified":
                    rdBtnSpecified.Checked = true;
                    txtBxLogin.Text = Properties.Settings.Default.RemoteAdminLogin;
                    txtBxPassword.Text = Credentials.GetRemoteAdminPassword();
                    break;
                case "Ask":
                    rdBtnAsk.Checked = true;
                    break;
                default:
                    rdBtnSameAsApplication.Checked = true;
                    break;
            }
            chkBxShowNonLocallyPublishedUpdates.Checked = Properties.Settings.Default.ShowNonLocallyPublishedUpdates;
            chkBxPreventAutoApproval.Checked = Properties.Settings.Default.PreventAutoApproval;

            lblDownloaded.Text = resMan.GetString("Downloaded");
            lblDownloaded.BackColor = Properties.Settings.Default.DownloadedColor;

            lblFailed.Text = resMan.GetString("Failed");
            lblFailed.BackColor = Properties.Settings.Default.FailedColor;

            lblInstalled.Text = resMan.GetString("Installed");
            lblInstalled.BackColor = Properties.Settings.Default.InstalledColor;

            lblInstalledPendingReboot.Text = resMan.GetString("InstalledPendingReboot");
            lblInstalledPendingReboot.BackColor = Properties.Settings.Default.InstalledPendingRebootColor;

            lblNotInstalled.Text = resMan.GetString("NotInstalled");
            lblNotInstalled.BackColor = Properties.Settings.Default.NotInstalledColor;

            lblUnknown.Text = resMan.GetString("Unknown");
            lblUnknown.BackColor = Properties.Settings.Default.UnknownColor;

            lblNotApplicable.Text = resMan.GetString("NotApplicable");
            lblNotApplicable.BackColor = Properties.Settings.Default.NotApplicableColor;

            string pingStd = Properties.Settings.Default.PingStandard;
            switch (pingStd)
            {
                case "IPv4":
                    rdBtnIPv4.Checked = true;
                    break;
                case "IPv6":
                    rdBtnIPv6.Checked = true;
                    break;
                case "IPv6IPv4":
                    rdBtnIPv6IPv4.Checked = true;
                    break;
                default:
                    rdBtnIPv4.Checked = true;
                    break;
            }
            if (!supportIPv6)
            {
                rdBtnIPv4.Checked = true;
                rdBtnIPv6.Enabled = false;
                rdBtnIPv6IPv4.Enabled = false;
            }

            ClearServerSettings();
            if (ServerList.Count == 0)
                MessageBox.Show(resMan.GetString("NeedServerSettings"));
            if (cmbBxServerList.Items.Count != 0)
                cmbBxServerList.SelectedIndex = 0;

            if (Properties.Settings.Default.UpdateFilePathSetting == 0)
            {
                rdBtnLastUsed.Checked = true;
                rdBtnSamePath.Checked = false;
                txtBxUseThisPath.Text = string.Empty;
                chkBxSamePathForAdditionnal.Checked = true;
            }
            else
            {
                rdBtnLastUsed.Checked = false;
                rdBtnSamePath.Checked = true;
                txtBxUseThisPath.Text = Properties.Settings.Default.LastUpdateFolder;
                chkBxSamePathForAdditionnal.Checked = Properties.Settings.Default.AdditionalUpdateFilePathAsMainFile;
            }

            cmbBxUpdateDefaultAction.SelectedItem = resMan.GetString(Properties.Settings.Default.UpdateDefaultAction);
            lnkLblOpenWith.Text = Properties.Settings.Default.OpenWindowsUpdateLogWith;
            chkBxConnectToLastUsedServer.Checked = Properties.Settings.Default.ConnectToLastUsedServer;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.PersonalizedRebootMessage))
                txtBxDefaultRebootMessage.Text = Properties.Settings.Default.PersonalizedRebootMessage;

            txtBxHTTPProxyServerName.Text = Properties.Settings.Default.HTTPProxyServerName;
            txtBxHTTPProxyLogin.Text = Properties.Settings.Default.HTTPProxyLogin;
            txtBxHTTPProxyPassword.Text = Properties.Settings.Default.HTTPProxyPassword;
            switch (Properties.Settings.Default.HTTPProxyPolicy)
            {
                case "NoProxy":
                    rdBtnHTTPProxyNoProxy.Checked = true;
                    break;
                case "CustomSettings":
                    rdBtnHTTPProxyCustomSettings.Checked = true;
                    break;
                default:
                    rdBtnHTTPProxyNoProxy.Checked = true;
                    break;
            }

            switch (Properties.Settings.Default.FTPProxyPolicy)
            {
                case "NoProxy":
                    rdBtnFtpProxyNoProxy.Checked = true;
                    break;
                case "SameAsAbove":
                    rdBtnFtpProxyAsAbove.Checked = true;
                    break;
                default:
                    rdBtnHTTPProxyNoProxy.Checked = true;
                    break;
            }

            nupHTTPProxyServerPort.Value = (decimal)Properties.Settings.Default.HTTPProxyPort;

            this.chkBxIgnoreVersionMismatch.Checked = Properties.Settings.Default.IgnoreVersionMismatch;
        }

        private void ClearServerSettings()
        {
            Logger.EnteringMethod();
            txtBxServerName.Text = "";
            chkBxConnectToLocalServer.Checked = false;
            cmbBxConnectionPort.SelectedItem = null;
            chkBxUseSSL.Checked = false;
            nupDeadLineDaysSpan.Value = 0;
            nupDeadLineHour.Value = 0;
            nupDeadLineMinute.Value = 0;
        }

        private void PrefillProxyServerName()
        {
            try
            {
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                Microsoft.Win32.RegistryKey IESettings = regKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
                string proxyServer = IESettings.GetValue("ProxyServer").ToString();
                string serverName = string.Empty;
                Decimal serverPort = 8080;
                if (proxyServer.IndexOf(':') != -1)
                {
                    serverName = proxyServer.Substring(0, proxyServer.IndexOf(':'));
                    serverPort = Convert.ToDecimal(proxyServer.Substring(proxyServer.IndexOf(':') + 1));
                }
                else
                {
                    serverName = proxyServer;
                }
                txtBxHTTPProxyServerName.Text = serverName;
                nupHTTPProxyServerPort.Value = serverPort;
            }
            catch (Exception) { }
        }

        private void btnEditServer_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (cmbBxServerList.SelectedItem != null)
            {
                WsusServer serverToEdit = (WsusServer)cmbBxServerList.SelectedItem;
                int port = 443;
                if (cmbBxConnectionPort.SelectedItem != null)
                {
                    if (int.TryParse(cmbBxConnectionPort.SelectedItem.ToString(), out port))
                        serverToEdit.Port = port;
                }
                else if (!string.IsNullOrEmpty(cmbBxConnectionPort.Text))
                    if (int.TryParse(cmbBxConnectionPort.Text, out port))
                        serverToEdit.Port = port;

                serverToEdit.IsLocal = chkBxConnectToLocalServer.Checked;
                serverToEdit.UseSSL = chkBxUseSSL.Checked;
                serverToEdit.IgnoreCertificateErrors = chkBxIgnoreCertificateErrors.Checked;
                serverToEdit.DeadLineDaysSpan = (int)nupDeadLineDaysSpan.Value;
                serverToEdit.DeadLineHour = (int)nupDeadLineHour.Value;
                serverToEdit.DeadLineMinute = (int)nupDeadLineMinute.Value;
                if (rdBtnVisibleAlways.Checked)
                    serverToEdit.VisibleInWsusConsole = MakeVisibleInWsusPolicy.Always;
                if (rdBtnVisibleChoose.Checked)
                    serverToEdit.VisibleInWsusConsole = MakeVisibleInWsusPolicy.LetMeChoose;
                if (rdBtnVisibleNever.Checked)
                    serverToEdit.VisibleInWsusConsole = MakeVisibleInWsusPolicy.Never;

                ServerList.Clear();
                foreach (object obj in cmbBxServerList.Items)
                {
                    WsusServer server = (WsusServer)obj;
                    ServerList.Add(server);
                }
                cmbBxServerList.Items.Clear();
                cmbBxServerList.Items.AddRange(ServerList.ToArray());
                cmbBxServerList.SelectedItem = serverToEdit;
            }
        }

        private void rdBtnSameThanApplication_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write("Ask : " + rdBtnAsk.Checked.ToString());
            Logger.Write("Same than App : " + rdBtnSameAsApplication.Checked.ToString());
            Logger.Write("Specified : " + rdBtnSpecified.Checked.ToString());
            txtBxLogin.Enabled = rdBtnSpecified.Checked;
            txtBxPassword.Enabled = rdBtnSpecified.Checked;
        }

        private void lblInstalled_DoubleClick(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Label editingLabel = (Label)sender;
            Logger.Write(editingLabel);
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                editingLabel.BackColor = colorDialog1.Color;
        }

        private void btnResetToDefault_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            lblDownloaded.BackColor = Color.GreenYellow;
            lblFailed.BackColor = Color.OrangeRed;
            lblInstalled.BackColor = Color.Lime;
            lblInstalledPendingReboot.BackColor = Color.LawnGreen;
            lblNotInstalled.BackColor = Color.Orange;
            lblUnknown.BackColor = Color.DarkOrange;
            lblNotApplicable.BackColor = Color.DeepSkyBlue;
        }

        private void chkBxConnectToLocalServer_CheckedChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(chkBxConnectToLocalServer);
            if (chkBxConnectToLocalServer.Checked)
            {
                rdBtnVisibleNever.Enabled = true;
                rdBtnVisibleChoose.Enabled = true;
                rdBtnVisibleAlways.Enabled = true;

                cmbBxConnectionPort.Enabled = false;
                chkBxUseSSL.Enabled = false;
            }
            else
            {
                rdBtnVisibleNever.Enabled = false;
                rdBtnVisibleChoose.Enabled = false;
                rdBtnVisibleAlways.Enabled = false;

                cmbBxConnectionPort.Enabled = true;
                chkBxUseSSL.Enabled = true;
            }
            ValidateData();
        }

        private void lnkLblOpenWith_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Logger.EnteringMethod();
            OpenFileDialog openWith = new OpenFileDialog();
            openWith.Multiselect = false;
            if (openWith.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lnkLblOpenWith.Text = openWith.FileName;
                Logger.Write("Open with " + lnkLblOpenWith.Text);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(txtBxUseThisPath.Text) && System.IO.Directory.Exists(txtBxUseThisPath.Text))
                folderBrowser.SelectedPath = txtBxUseThisPath.Text;
            else
                folderBrowser.RootFolder = Environment.SpecialFolder.MyComputer;

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                txtBxUseThisPath.Text = folderBrowser.SelectedPath;
        }

        private void rdBtnLastUsed_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBtnSamePath.Checked)
            {
                txtBxUseThisPath.Enabled = true;
                btnBrowse.Enabled = true;
                chkBxSamePathForAdditionnal.Enabled = true;
            }
            else
            {
                txtBxUseThisPath.Enabled = false;
                btnBrowse.Enabled = false;
                chkBxSamePathForAdditionnal.Enabled = false;
            }
        }

        private void rdBtnCustomSettings_CheckedChanged(object sender, EventArgs e)
        {
            txtBxHTTPProxyServerName.Enabled = rdBtnHTTPProxyCustomSettings.Checked;
            txtBxHTTPProxyLogin.Enabled = rdBtnHTTPProxyCustomSettings.Checked;
            txtBxHTTPProxyPassword.Enabled = rdBtnHTTPProxyCustomSettings.Checked;
            nupHTTPProxyServerPort.Enabled = rdBtnHTTPProxyCustomSettings.Checked;

            if (rdBtnHTTPProxyCustomSettings.Checked && string.IsNullOrEmpty(txtBxHTTPProxyServerName.Text))
                PrefillProxyServerName();
        }

        private void btnTestHTTPDownload_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            SaveProxySettings();

            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                string result;
                this.Cursor = Cursors.WaitCursor;
                client.Proxy = Tools.Utilities.GetHTTPProxy();
                result = client.DownloadString("http://google.com/ncr");
                this.Cursor = Cursors.Default;
                if (!string.IsNullOrEmpty(result))
                    MessageBox.Show(resMan.GetString("SuccessfullHTTPDownload"));
                else
                    MessageBox.Show(resMan.GetString("FailedHTTPDownload"));
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(resMan.GetString("FailedHTTPDownload"));
            }
        }

        private void btnTestFTPDownload_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            SaveProxySettings();
            FtpClient ftpClient = new FtpClient(ftpUrl, ftpLogin, ftpPassword);
            string tempFolder = Tools.Utilities.GetTempFolder();

            this.Cursor = Cursors.WaitCursor;
            if (ftpClient.Download(ftpRemoteFile, tempFolder + "FTPDownloadTestFile"))
            {
                this.Cursor = Cursors.Default;
                System.Windows.Forms.MessageBox.Show(resMan.GetString("SuccessfullFTPDownload"));
            }
            else
            {
                this.Cursor = Cursors.Default;
                System.Windows.Forms.MessageBox.Show(resMan.GetString("FailedFTPDownload"));
            }

            Tools.Utilities.DeleteFolder(tempFolder);
        }

        private void btnTestFTPUpload_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            SaveProxySettings();
            FtpClient ftpClient = new FtpClient(ftpUrl, ftpLogin, ftpPassword);
            string tempFolder = Tools.Utilities.GetTempFolder();
            string tempFilename = Guid.NewGuid().ToString();
            try
            {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(tempFolder + tempFilename);
                writer.Write("Test Upload FTP.");
                writer.Flush();
                writer.Close();
            }
            catch (Exception) { }
            this.Cursor = Cursors.WaitCursor;
            if (ftpClient.Upload(ftpRemoteFileForFtpUploadTest + tempFilename, tempFolder + tempFilename))
            {
                this.Cursor = Cursors.Default;
                System.Windows.Forms.MessageBox.Show(resMan.GetString("SuccessfullFTPUpload"));
            }
            else
            {
                this.Cursor = Cursors.Default;
                System.Windows.Forms.MessageBox.Show(resMan.GetString("FailedFTPUpload"));
            }
            Tools.Utilities.DeleteFolder(tempFolder);
        }
    }
}
