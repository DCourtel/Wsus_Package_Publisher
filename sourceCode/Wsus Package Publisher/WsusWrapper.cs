using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal sealed class WsusWrapper
    {
        internal enum StreamTypeServer
        {
            UpStream,
            DownStream
        }

        private IUpdateServer wsus;
        private static WsusWrapper instance;
        private WsusServer _wsusServer;
        private System.Windows.Forms.Timer _timer;
        private UpdateServerUserRole _userRole = UpdateServerUserRole.Unauthorized;
        private CertificateHelper.CertificateStatus _certificateStatus = CertificateHelper.CertificateStatus.Invalid;
        private int _certificateKeyLength = 0;
        private FrmWaiting _waitingForm;
        private System.Threading.Thread _waitingThread;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(WsusWrapper).Assembly);

        private WsusWrapper()
        {
            Logger.EnteringMethod();
            _timer = new System.Windows.Forms.Timer();
            ConsoleVersion = GetConsoleVersion();
        }

        internal bool IsConnected { get; private set; }

        internal bool IsReplica { get; private set; }

        internal bool IsLocal { get; private set; }

        internal int LocalPublishingMaxCabSize { get; private set; }

        /// <summary>
        /// Get or Set if the Server is UpStream or DownStream.
        /// </summary>
        internal StreamTypeServer StreamType
        {
            get;
            private set;
        }

        internal DownstreamServerCollection DownStreamServers
        {
            get;
            private set;
        }

        internal UpdateServerUserRole UserRole
        {
            get { return _userRole; }
            private set
            {
                Logger.EnteringMethod(value.ToString());
                _userRole = value;
            }
        }

        /// <summary>
        /// Allow to always get the same instance of this Class.
        /// </summary>
        /// <returns>An instance of WsusWrapper.</returns>
        internal static WsusWrapper GetInstance()
        {
            Logger.EnteringMethod();
            if (instance == null)
                instance = new WsusWrapper();
            return instance;
        }

        /// <summary>
        /// Connect to the Wsus server.
        /// </summary>
        /// <param name="serverToConnect">Server to connect to.</param>
        /// <param name="preferredCulture">Culture to use for displaying computer group name.</param>
        /// <returns></returns>
        internal bool Connect(WsusServer serverToConnect, string preferredCulture)
        {
            Logger.EnteringMethod(serverToConnect.ToString() + ", " + preferredCulture);
            IsConnected = false;
            _timer.Stop();
            try
            {
                if (serverToConnect.IsLocal)
                {
                    wsus = AdminProxy.GetUpdateServer();
                    Logger.Write("Connected to local Wsus");
                    IsLocal = true;
                }
                else
                {
                    wsus = AdminProxy.GetUpdateServer(serverToConnect.Name, serverToConnect.UseSSL, serverToConnect.Port);
                    Logger.Write("Connected to remote wsus.");
                    IsLocal = false;
                    _timer.Interval = 10000;
                    _timer.Tick += new EventHandler(_timer_Tick);
                    _timer.Start();
                }
                wsus.PreferredCulture = preferredCulture;
                _wsusServer = serverToConnect;
                IUpdateServerConfiguration wsusConf = wsus.GetConfiguration();
                IsReplica = wsusConf.IsReplicaServer;
                this.LocalPublishingMaxCabSize = wsusConf.LocalPublishingMaxCabSize;
                IsConnected = true;
                ConsoleVersion = GetConsoleVersion();
                if (wsusConf.UpstreamWsusServerName == "")
                    StreamType = StreamTypeServer.UpStream;
                else
                    StreamType = StreamTypeServer.DownStream;
                DownStreamServers = wsus.GetDownstreamServers();
                UserRole = wsus.GetCurrentUserRole();
                if (!IsReplica)
                {
                    GetCertificateStatus = CheckCertificateStatus(wsusConf);
                    GetCertificateKeyLength = CheckCertificateKeyLength(wsusConf);
                }
                else
                    GetCertificateStatus = CertificateHelper.CertificateStatus.NotRequired;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show("Connection failed : \r\n" + ex.Message);
            }
            return IsConnected;
        }

        private CertificateHelper.CertificateStatus CheckCertificateStatus(IUpdateServerConfiguration wsusConfiguration)
        {
            Logger.EnteringMethod();

            string tmpFolder = Tools.Utilities.GetTempFolder();
            string certificateFile = tmpFolder + "certificate.cer";

            try
            {
                Logger.Write("Trying to get certificate from server.");
                wsusConfiguration.GetSigningCertificate(certificateFile);
                Logger.Write("Successfuly got certificate from server.");
            }
            catch (Exception ex)
            { Logger.Write("**** " + ex.GetType() + " : " + ex.Message); }

            if (System.IO.File.Exists(certificateFile))
            {
                CertificateHelper.CertificateStatusResult result = CertificateHelper.GetCertificateStatus(certificateFile, CurrentServer.IgnoreCertificateErrors);
                CertificateExpirationDate = result.ExpirationDate;
                Tools.Utilities.DeleteFolder(tmpFolder);
                return result.Status;
            }
            Logger.Write("Absent");
            Tools.Utilities.DeleteFolder(tmpFolder);
            return CurrentServer.IgnoreCertificateErrors ? CertificateHelper.CertificateStatus.Valid : CertificateHelper.CertificateStatus.Absent;
        }

        private int CheckCertificateKeyLength(IUpdateServerConfiguration wsusConfiguration)
        {
            Logger.EnteringMethod();
            int result = 0;
            string tmpFolder = Tools.Utilities.GetTempFolder();
            string certificateFile = tmpFolder + "certificate.cer";

            try
            {
                wsusConfiguration.GetSigningCertificate(certificateFile);
            }
            catch (Exception ex)
            { Logger.Write("**** " + ex.Message); }

            if (System.IO.File.Exists(certificateFile))
            {
                result = CertificateHelper.GetCertificateKeyLength(certificateFile);
            }
            Logger.Write("Will return : " + result.ToString());
            Tools.Utilities.DeleteFolder(tmpFolder);
            return result;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            GetAllComputerTargetGroup();
            _timer.Start();
        }

        internal WsusServer CurrentServer
        {
            get { return _wsusServer; }
        }

        internal Version ConsoleVersion { get; private set; }

        private Version GetConsoleVersion()
        {
            Logger.EnteringMethod();

            if (IsConnected && IsLocal)
                return GetServerVersion();

            Version consoleVersion = new Version();

            GetVersionFromRegKey(@"SOFTWARE\Microsoft\Update Services\Server\Setup", "VersionString", ref consoleVersion);//3.2.7600.226

            GetVersionFromRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Windows Server Update Services 3.0 SP2 (KB2720211)", "DisplayVersion", ref consoleVersion);//3.2.7600.251

            GetVersionFromRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Windows Server Update Services 3.0 SP2 (KB2734608)", "DisplayVersion", ref consoleVersion);//3.2.7600.256

            GetVersionFromRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Windows Server Update Services 3.0 SP2-KB2828185", "DisplayVersion", ref consoleVersion);//3.2.7600.262

            GetVersionFromRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Windows Server Update Services 3.0 SP2-KB2938066", "DisplayVersion", ref consoleVersion);//3.2.7600.274

            Logger.Write("Returning " + consoleVersion.ToString());
            return consoleVersion;
        }

        private void GetVersionFromRegKey(string keyName, string valueName, ref Version consoleVersion)
        {
            Logger.EnteringMethod(keyName + "\\" + valueName);

            Microsoft.Win32.RegistryKey HKLM = Microsoft.Win32.Registry.LocalMachine;

            try
            {
                Microsoft.Win32.RegistryKey kbKey = HKLM.OpenSubKey(keyName, false);
                if (kbKey != null)
                {
                    object displayVersion = kbKey.GetValue(valueName);
                    if (displayVersion != null)
                    {
                        consoleVersion = new Version(displayVersion.ToString());
                        Logger.Write("Version is at least : " + consoleVersion.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
        }

        internal Version GetServerVersion()
        {
            if (wsus != null)
                return wsus.Version;
            else
                return new Version();
        }

        /// <summary>
        /// Compare Server and Console version to know if a publication is possible.
        /// </summary>
        /// <returns>True if we can publish, otherwise false</returns>
        internal bool IsConsoleVersionAllowPublication()
        {
            if (Properties.Settings.Default.IgnoreVersionMismatch)
                return true;

            if (this.IsConnected)
            {
                Version serverVersion = this.GetServerVersion();

                if (this.ConsoleVersion.Equals(serverVersion))// || (serverVersion.Equals(new Version("6.3.9600.16384")) && this.ConsoleVersion.Equals(new Version("6.3.9600.17477"))))
                    return true;
            }

            return false;
        }

        internal string GetSqlServerName()
        {
            return wsus.GetDatabaseConfiguration().ServerName;
        }

        internal string GetSqlDataBaseName()
        {
            return wsus.GetDatabaseConfiguration().DatabaseName;
        }

        internal CertificateHelper.CertificateStatus GetCertificateStatus
        {
            get { return _certificateStatus; }
            private set
            {
                Logger.EnteringMethod(value.ToString());
                _certificateStatus = value;
            }
        }

        internal int GetCertificateKeyLength
        {
            get
            { return _certificateKeyLength; }
            private set
            {
                Logger.EnteringMethod(value.ToString());
                _certificateKeyLength = value;
            }
        }

        internal DateTime CertificateExpirationDate
        {
            get;
            private set;
        }

        internal void SetLocalPublishingMaxCabSize(int newSize)
        {
            if (IsConnected)
            {
                IUpdateServerConfiguration wsusConf = wsus.GetConfiguration();
                wsusConf.LocalPublishingMaxCabSize = newSize;
                wsusConf.Save(true);
                this.LocalPublishingMaxCabSize = newSize;
            }
        }

        /// <summary>
        /// Get all computers in a target group.
        /// </summary>
        /// <param name="computerGroup">The Guid of the group which contain computers.</param>
        /// <returns>A collection of target computers.</returns>
        internal ComputerTargetCollection GetComputerTargets(Guid computerGroup)
        {
            Logger.EnteringMethod(computerGroup.ToString());
            ComputerTargetScope scope = new ComputerTargetScope();
            scope.IncludeDownstreamComputerTargets = true;
            scope.ComputerTargetGroups.Add(wsus.GetComputerTargetGroup(computerGroup));

            return wsus.GetComputerTargets(scope);
        }

        /// <summary>
        /// Get a computer by his ID.
        /// </summary>
        /// <param name="computerID">ID of the computer</param>
        /// <returns>A IComputerTarget</returns>
        internal IComputerTarget GetComputerTarget(string computerID)
        {
            return wsus.GetComputerTarget(computerID);
        }

        /// <summary>
        /// Return an object representing the 'All Computer' group.
        /// </summary>
        /// <returns>Return a IComputerTargetGroup which represent the 'All Computer' group.</returns>
        internal IComputerTargetGroup GetAllComputerTargetGroup()
        {
            return wsus.GetComputerTargetGroup(ComputerTargetGroupId.AllComputers);
        }

        /// <summary>
        /// Allow to iterate each child computer Target Group of a Computer Target Group.
        /// </summary>
        /// <returns>A KeyValue pair which represent the Name and the Id of a child group.</returns>
        internal ComputerTargetGroupCollection GetChildComputerTargetGroupNameAndId(Guid id)
        {
            return wsus.GetComputerTargetGroup(id).GetChildTargetGroups();
        }

        /// <summary>
        /// Get the name of the computer coresponding to this ID.
        /// </summary>
        /// <param name="computerId">Id of the computer.</param>
        /// <returns>Name of the computer which have this Id.</returns>
        internal string GetComputerName(string computerId)
        {
            return wsus.GetComputerTarget(computerId).FullDomainName;
        }

        /// <summary>
        /// Allow to iterate each udpdate in the server.
        /// </summary>
        /// <returns>Collection of updates.</returns>
        internal UpdateCollection GetAllUpdates()
        {
            Logger.EnteringMethod();
            UpdateScope scope = new UpdateScope();
            if (Properties.Settings.Default.ShowNonLocallyPublishedUpdates)
                scope.UpdateSources = UpdateSources.All;
            else
                scope.UpdateSources = UpdateSources.Other;
            return wsus.GetUpdates(scope);
        }

        internal UpdateCollection GetAllUpdates(UpdateScope scope)
        {
            return wsus.GetUpdates(scope);
        }

        internal IUpdate GetUpdate(UpdateRevisionId updateId)
        {
            return wsus.GetUpdate(updateId);
        }

        internal UpdateCollection GetUpdatesInCategory(IUpdateCategory category)
        {
            return category.GetUpdates();
        }

        internal UpdateCategoryCollection GetAllCategories()
        {
            return wsus.GetUpdateCategories();
        }

        internal bool CategoryExists(Guid categoryId)
        {
            Logger.Write("Checking for Category : " + categoryId.ToString());
            try
            {
                System.Threading.Thread.Sleep(3000);
                wsus.GetUpdateCategory(categoryId);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
            return true;
        }

        internal UpdateInstallationInfoCollection GetUpdateInstallationInfo(string computerName, System.DateTime from, System.DateTime to)
        {
            Logger.EnteringMethod(computerName);
            UpdateScope scope = new UpdateScope();
            scope.FromArrivalDate = from;
            scope.ToArrivalDate = to;
            scope.IncludedInstallationStates = UpdateInstallationStates.Installed;

            return wsus.GetComputerTargetByName(computerName).GetUpdateInstallationInfoPerUpdate(scope);
        }

        internal UpdateInstallationInfoCollection GetUpdateInstallationInfoPerComputerTarget(Guid groupId, IUpdate update)
        {
            Logger.EnteringMethod(update.Title);
            UpdateInstallationInfoCollection updateInstallationInfoCol = new UpdateInstallationInfoCollection();

            ComputerTargetScope scope = new ComputerTargetScope();
            scope.ComputerTargetGroups.Add(GetComputerGroup(groupId));
            scope.IncludeDownstreamComputerTargets = true;
            try { updateInstallationInfoCol = update.GetUpdateInstallationInfoPerComputerTarget(scope); }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            return update.GetUpdateInstallationInfoPerComputerTarget(scope);
        }

        internal IUpdateSummary GetSummaryForComputerTargetGroup(IComputerTargetGroup group, IUpdate update)
        {
            Logger.EnteringMethod(update.Title);
            IUpdateSummary result;
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            result = update.GetSummaryForComputerTargetGroup(group);
            if (!_wsusServer.IsLocal)
                _timer.Start();
            return result;
        }

        internal string GetApplicabilityMetadata(FrmUpdateFilesWizard fileWizard)
        {
            Logger.EnteringMethod();
            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage();
            try
            {
                switch (fileWizard.FileType)
                {
                    case FrmUpdateFilesWizard.UpdateType.WindowsInstaller:
                        sdp.PopulatePackageFromWindowsInstaller(fileWizard.UpdateFileName);
                        break;
                    case FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch:
                        sdp.PopulatePackageFromWindowsInstallerPatch(fileWizard.UpdateFileName);
                        break;
                    case FrmUpdateFilesWizard.UpdateType.Executable:
                        sdp.PopulatePackageFromExe(fileWizard.UpdateFileName);
                        break;
                    default:
                        break;
                }
                return sdp.InstallableItems[0].ApplicabilityMetadata;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
        }

        internal IUpdate PublishUpdate(FrmUpdateFilesWizard filesWizard, FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard, FrmUpdateApplicabilityMetadata updateApplicabilityMetadata)
        {
            Logger.EnteringMethod();
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage();
            Logger.Write("File Type : " + filesWizard.FileType.ToString());
            try
            {
                switch (filesWizard.FileType)
                {
                    case FrmUpdateFilesWizard.UpdateType.WindowsInstaller:
                        sdp.PopulatePackageFromWindowsInstaller(filesWizard.UpdateFileName);
                        break;
                    case FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch:
                        sdp.PopulatePackageFromWindowsInstallerPatch(filesWizard.UpdateFileName);
                        break;
                    case FrmUpdateFilesWizard.UpdateType.Executable:
                        sdp.PopulatePackageFromExe(filesWizard.UpdateFileName);
                        break;
                    default:
                        break;
                }
                if (updateApplicabilityMetadata.EditMetadata)
                {
                    sdp.InstallableItems[0].ApplicabilityMetadata = string.IsNullOrEmpty(updateApplicabilityMetadata.Metadata) ? null : updateApplicabilityMetadata.Metadata;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            sdp = InitializeSdp(sdp, filesWizard, informationsWizard, isInstalledRulesWizard, isInstallableRulesWizard);
            string sdpMetaData = sdp.InstallableItems[0].ApplicabilityMetadata;

            string tmpFolderPath;
            tmpFolderPath = Tools.Utilities.GetTempFolder();

            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Bin\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Bin\\");

            System.IO.FileInfo updateFile = new System.IO.FileInfo(filesWizard.UpdateFileName);
            updateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + updateFile.Name);

            if (filesWizard.AdditionnalFileName.Count != 0)
            {
#if DEBUG
                System.Windows.Forms.MessageBox.Show("Multi file detected : " + filesWizard.AdditionnalFileName.Count);
#endif
                foreach (string fileOrFolder in filesWizard.AdditionnalFileName)
                {
                    if (fileOrFolder.EndsWith(@"\"))
                    {
                        System.IO.DirectoryInfo additionalUPdateFolder = new System.IO.DirectoryInfo(fileOrFolder);
                        DirectoryCopy(fileOrFolder, tmpFolderPath + sdp.PackageId + "\\Bin\\" + additionalUPdateFolder.Name, true);
                    }
                    else
                    {
                        System.IO.FileInfo additionalUpdateFile = new System.IO.FileInfo(fileOrFolder);
                        additionalUpdateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + additionalUpdateFile.Name);
                    }
                }
            }
            MaxCabFileChecker.CheckMaxFileSize(tmpFolderPath + sdp.PackageId + "\\Bin\\");
            try
            {
                sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(resMan.GetString("XmlSchemaValidationException"));
                return null;
            }
            IPublisher publisher = null;
            try
            {
                publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Write("Exception when publishing : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            System.IO.FileInfo sdpFile = new System.IO.FileInfo(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            Logger.Write("Saving Sdp : " + sdpFile.OpenText().ReadToEnd());

            Dictionary<int, bool> rulesState = new Dictionary<int, bool>();
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
            {
                rulesState = SaveAutoApprovalRulesState();
                DisableAutomaticApprovalRules();
            }

            bool successfulyPublished = false;
            try
            {
                publisher.PublishPackage(tmpFolderPath + sdp.PackageId + "\\Bin\\", null);
                successfulyPublished = true;
                Logger.Write("Update Successfuly published");
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.GetType().ToString() + " : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
                RestoreAutomaticApprovalRules(rulesState);

#if !DEBUG
            try
            {
                Tools.Utilities.DeleteFolder(tmpFolderPath);
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when deleting the temp directory : " + ex.Message);
            }
#endif
            if (!_wsusServer.IsLocal)
                _timer.Start();
            if (successfulyPublished)
            {
                IUpdate publishedUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));

                if (UpdateSuperseded != null && sdp.SupersededPackages.Count != 0)
                    UpdateSuperseded(sdp.SupersededPackages);
                if (UpdatePublished != null)
                    UpdatePublished(publishedUpdate);

                return publishedUpdate;
            }
            else
                return null;
        }

        internal IUpdate PublishUpdate(SoftwareDistributionPackage sdp, string folder)
        {
            Logger.EnteringMethod(sdp.Title + " : " + folder);

            if (!_wsusServer.IsLocal)
                _timer.Stop();
            MaxCabFileChecker.CheckMaxFileSize(folder);
            string tmpFolderPath = Tools.Utilities.GetTempFolder();

            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);

            sdp.Save(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");

            IPublisher publisher = null;
            try
            {
                publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Write("Exception when publishing : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            Dictionary<int, bool> rulesState = new Dictionary<int, bool>();
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
            {
                rulesState = SaveAutoApprovalRulesState();
                DisableAutomaticApprovalRules();
            }

            bool successfulyPublished = false;
            try
            {
                publisher.PublishPackage(folder, null);
                successfulyPublished = true;
                Logger.Write("Update Successfuly published");
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.GetType().ToString() + " : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
                RestoreAutomaticApprovalRules(rulesState);

#if !DEBUG
            try
            {
                Tools.Utilities.DeleteFolder(tmpFolderPath);
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when deleting the temp directory : " + ex.Message);
            }
#endif
            if (!_wsusServer.IsLocal)
                _timer.Start();
            if (successfulyPublished)
            {
                IUpdate publishedUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));

                if (UpdateSuperseded != null && sdp.SupersededPackages.Count != 0)
                    UpdateSuperseded(sdp.SupersededPackages);
                if (UpdatePublished != null)
                    UpdatePublished(publishedUpdate);

                return publishedUpdate;
            }
            else
                return null;
        }

        internal IUpdate PublishUpdate(SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod(sdp.Title);

            if (!_wsusServer.IsLocal)
                _timer.Stop();

            string tmpFolderPath = Tools.Utilities.GetTempFolder();

            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);

            sdp.Save(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");

            IPublisher publisher = null;

            try
            {
                wsus.GetPublisher(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Write("Exception when publishing : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }

            publisher.MetadataOnly = false;

            Dictionary<int, bool> rulesState = new Dictionary<int, bool>();
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
            {
                rulesState = SaveAutoApprovalRulesState();
                DisableAutomaticApprovalRules();
            }

            bool successfulyPublished = false;
            try
            {
                publisher.PublishPackage(null, null);
                successfulyPublished = true;
                Logger.Write("Update successfuly published");
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.GetType().ToString() + " : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
                RestoreAutomaticApprovalRules(rulesState);
#if !DEBUG
            try
            {
                Tools.Utilities.DeleteFolder(tmpFolderPath);
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when deleting the temp directory : " + ex.Message);
            }
#endif
            if (!_wsusServer.IsLocal)
                _timer.Start();
            if (successfulyPublished)
            {
                IUpdate publishedUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));

                if (UpdateSuperseded != null && sdp.SupersededPackages.Count != 0)
                    UpdateSuperseded(sdp.SupersededPackages);
                if (UpdatePublished != null)
                    UpdatePublished(publishedUpdate);

                return publishedUpdate;
            }
            else
                return null;
        }

        internal bool PublishDetectoid(SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod(sdp.Title);

            if (!_wsusServer.IsLocal)
                _timer.Stop();

            string tmpFolderPath = Tools.Utilities.GetTempFolder();

            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);

            sdp.Save(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");

            IPublisher publisher = null;

            try
            {
                wsus.GetPublisher(tmpFolderPath + sdp.PackageId + @"\" + sdp.PackageId.ToString() + ".xml");
            }
            catch (Exception ex)
            {
                Logger.Write("Exception when publishing : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }

            publisher.MetadataOnly = false;

            Dictionary<int, bool> rulesState = new Dictionary<int, bool>();
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
            {
                rulesState = SaveAutoApprovalRulesState();
                DisableAutomaticApprovalRules();
            }

            bool isSuccessfulyPublished = false;
            try
            {
                publisher.PublishPackage(null, null);
                isSuccessfulyPublished = true;
                Logger.Write("Detectoid has been successfuly published.");
            }
            catch (System.ArgumentNullException)
            {
                isSuccessfulyPublished = true;
                Logger.Write("Publication of the detectoid has thrown a System.ArgumentNullException. We will assume that the detectoid has been successfuly published.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.GetType().ToString() + " : " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            if (Properties.Settings.Default.PreventAutoApproval && sdp.PackageType == PackageType.Update)
                RestoreAutomaticApprovalRules(rulesState);
#if !DEBUG
            try
            {
                Tools.Utilities.DeleteFolder(tmpFolderPath);
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when deleting the temp directory : " + ex.Message);
            }
#endif
            if (!_wsusServer.IsLocal)
                _timer.Start();

            return isSuccessfulyPublished;
        }

        private Dictionary<int, bool> SaveAutoApprovalRulesState()
        {
            Logger.EnteringMethod();
            Dictionary<int, bool> state = new Dictionary<int, bool>();

            AutomaticUpdateApprovalRuleCollection rules = wsus.GetInstallApprovalRules();

            foreach (IAutomaticUpdateApprovalRule rule in rules)
            {
                Logger.Write("Saving '" + rule.Name + "' with Id= " + rule.Id.ToString() + " which is Enabled= " + rule.Enabled.ToString());
                state.Add(rule.Id, rule.Enabled);
            }

            return state;
        }

        private void DisableAutomaticApprovalRules()
        {
            Logger.EnteringMethod();
            AutomaticUpdateApprovalRuleCollection rules = wsus.GetInstallApprovalRules();

            foreach (IAutomaticUpdateApprovalRule rule in rules)
            {
                rule.Enabled = false;
                rule.Save();
            }
        }

        private void RestoreAutomaticApprovalRules(Dictionary<int, bool> oldState)
        {
            Logger.EnteringMethod();
            AutomaticUpdateApprovalRuleCollection rules = wsus.GetInstallApprovalRules();

            for (int i = 0; i < rules.Count; i++)
            {
                if (oldState.ContainsKey(rules[i].Id))
                {
                    Logger.Write("Rule '" + rules[i].Name + " with Id= " + rules[i].Id.ToString() + " will be set to Enabled= " + oldState[rules[i].Id].ToString());
                    rules[i].Enabled = oldState[rules[i].Id];
                    rules[i].Save();
                }
            }
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            Logger.EnteringMethod(sourceDirName + " to " + destDirName);
            System.IO.DirectoryInfo sourceFolder = new System.IO.DirectoryInfo(sourceDirName);
            System.IO.DirectoryInfo[] sourceSubFolders = sourceFolder.GetDirectories();

            try
            {
                if (sourceFolder.Exists)
                {
                    if (!System.IO.Directory.Exists(destDirName))
                    {
                        System.IO.Directory.CreateDirectory(destDirName);
                    }

                    System.IO.FileInfo[] files = sourceFolder.GetFiles();
                    foreach (System.IO.FileInfo file in files)
                    {
                        string temppath = System.IO.Path.Combine(destDirName, file.Name);
                        file.CopyTo(temppath, true);
                    }

                    if (copySubDirs)
                    {
                        foreach (System.IO.DirectoryInfo subdir in sourceSubFolders)
                        {
                            string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private SoftwareDistributionPackage InitializeSdp(SoftwareDistributionPackage sdp, FrmUpdateFilesWizard filesWizard, FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard)
        {
            Logger.EnteringMethod();
            sdp.Title = informationsWizard.Title;
            sdp.Description = informationsWizard.Description;
            sdp.VendorName = informationsWizard.VendorName;
            sdp.ProductNames.Clear();
            sdp.ProductNames.Add(informationsWizard.ProductName);

            string rules = null;

            rules = isInstalledRulesWizard.GetXmlFormattedUpdateLevelRule();
            if (rules != null)
                sdp.IsInstalled = rules;

            rules = null;
            rules = isInstallableRulesWizard.GetXmlFormattedUpdateLevelRule();
            if (rules != null)
                sdp.IsInstallable = rules;

            rules = null;
            rules = isInstalledRulesWizard.GetXmlFormattedPackageLevelRule();
            if (rules != null)
                sdp.InstallableItems[0].IsInstalledApplicabilityRule = rules;

            rules = null;
            rules = isInstallableRulesWizard.GetXmlFormattedPackageLevelRule();
            if (rules != null)
                sdp.InstallableItems[0].IsInstallableApplicabilityRule = rules;

            sdp.InstallableItems[0].InstallBehavior.CanRequestUserInput = informationsWizard.CanRequestUserInput;
            sdp.InstallableItems[0].InstallBehavior.Impact = informationsWizard.Impact;
            sdp.InstallableItems[0].InstallBehavior.RebootBehavior = informationsWizard.Behavior;
            sdp.InstallableItems[0].InstallBehavior.RequiresNetworkConnectivity = informationsWizard.CanRequestNetworkConnectivity;
            sdp.PackageType = informationsWizard.UpdateType;

            if (filesWizard != null)
            {
                if (!string.IsNullOrEmpty(informationsWizard.CommandLine))
                    switch (filesWizard.FileType)
                    {
                        case FrmUpdateFilesWizard.UpdateType.WindowsInstaller:
                            (sdp.InstallableItems[0] as WindowsInstallerItem).InstallCommandLine = informationsWizard.CommandLine;
                            break;
                        case FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch:
                            (sdp.InstallableItems[0] as WindowsInstallerPatchItem).InstallCommandLine = informationsWizard.CommandLine;
                            break;
                        case FrmUpdateFilesWizard.UpdateType.Executable:
                            (sdp.InstallableItems[0] as CommandLineItem).Arguments = informationsWizard.CommandLine;
                            break;
                        default:
                            break;
                    }
                if (filesWizard.FileType == FrmUpdateFilesWizard.UpdateType.Executable && informationsWizard.ReturnCodes.Count != 0)
                {
                    (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes.Clear();
                    foreach (ReturnCode code in informationsWizard.ReturnCodes)
                    {
                        (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes.Add(code);
                    }
                }
            }
            else
            {
                string commandLine = informationsWizard.CommandLine;
                if (string.IsNullOrEmpty(commandLine))
                    commandLine = " ";
                Type updateType = sdp.InstallableItems[0].GetType();
                if (updateType == typeof(WindowsInstallerItem))
                    (sdp.InstallableItems[0] as WindowsInstallerItem).InstallCommandLine = commandLine;
                else
                    if (updateType == typeof(WindowsInstallerPatchItem))
                        (sdp.InstallableItems[0] as WindowsInstallerPatchItem).InstallCommandLine = commandLine;
                    else
                        if (updateType == typeof(CommandLineItem))
                        {
                            (sdp.InstallableItems[0] as CommandLineItem).Arguments = commandLine;
                            (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes.Clear();
                            foreach (ReturnCode code in informationsWizard.ReturnCodes)
                            {
                                (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes.Add(code);
                            }
                        }
            }

            if (isInstalledRulesWizard.EmptyRuleAtPackageLevel)
                sdp.InstallableItems[0].IsInstalledApplicabilityRule = string.Empty;

            if (isInstallableRulesWizard.EmptyRuleAtPackageLevel)
                sdp.InstallableItems[0].IsInstallableApplicabilityRule = string.Empty;

            if (!string.IsNullOrEmpty(informationsWizard.UrlMoreInfo))
            {
                sdp.AdditionalInformationUrls.Clear();
                sdp.AdditionalInformationUrls.Add(new Uri(informationsWizard.UrlMoreInfo));
            }

            if (!string.IsNullOrEmpty(informationsWizard.UrlSupport))
                sdp.SupportUrl = new Uri(informationsWizard.UrlSupport);

            sdp.PackageUpdateClassification = informationsWizard.UpdateClassification;

            if (!string.IsNullOrEmpty(informationsWizard.SecurityBulletinId))
                sdp.SecurityBulletinId = informationsWizard.SecurityBulletinId;

            sdp.SecurityRating = informationsWizard.Severity;

            if (informationsWizard.Cve.Count != 0)
            {
                sdp.CommonVulnerabilitiesIds.Clear();
                foreach (string cve in informationsWizard.Cve)
                {
                    sdp.CommonVulnerabilitiesIds.Add(cve);
                }
            }

            if (!string.IsNullOrEmpty(informationsWizard.KbArticle))
                sdp.KnowledgebaseArticleId = informationsWizard.KbArticle;

            List<Guid> supersedes = informationsWizard.GetSupersedes();
            sdp.SupersededPackages.Clear();
            if (supersedes.Count != 0)
            {
                foreach (Guid id in supersedes)
                    if (!sdp.SupersededPackages.Contains(id))
                        sdp.SupersededPackages.Add(id);
            }

            sdp.Prerequisites.Clear();
            if (informationsWizard.GetPrerequisites().Count != 0)
            {
                PrerequisiteGroup prerequisiteGrp = new PrerequisiteGroup();
                foreach (Guid id in informationsWizard.GetPrerequisites())
                    if (!PrerequisitePresent(sdp.Prerequisites, id))
                        prerequisiteGrp.Ids.Add(id);
                if (prerequisiteGrp.Ids.Count != 0)
                    sdp.Prerequisites.Add(prerequisiteGrp);
            }

            return sdp;
        }

        internal bool PrerequisitePresent(IList<PrerequisiteGroup> groupOfPrerequisites, Guid id)
        {
            Logger.EnteringMethod();
            foreach (PrerequisiteGroup prerequisiteGrp in groupOfPrerequisites)
            {
                foreach (Guid guid in prerequisiteGrp.Ids)
                {
                    if (guid.Equals(id))
                    {
                        Logger.Write("Return True");
                        return true;
                    }
                }
            }
            Logger.Write("Return false");
            return false;
        }

        internal void ReviseUpate(FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard, SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod();
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            string tmpFolderPath;
            IUpdate oldUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));
            IUpdateCategory oldProductCategory = oldUpdate.GetUpdateCategories()[0];
            IUpdateCategory oldCompanyCategory = oldProductCategory.GetParentUpdateCategory();

            sdp = InitializeSdp(sdp, null, informationsWizard, isInstalledRulesWizard, isInstallableRulesWizard);

            tmpFolderPath = Tools.Utilities.GetTempFolder();
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
            if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");

            sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            System.IO.FileInfo sdpFile = new System.IO.FileInfo(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
            Logger.Write("Saving Sdp : " + sdpFile.OpenText().ReadToEnd());

            publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
            try
            {
                publisher.RevisePackage();

                if (UpdateSuperseded != null && sdp.SupersededPackages.Count != 0)
                    UpdateSuperseded(sdp.SupersededPackages);
                if (UpdateRevised != null)
                    UpdateRevised(oldUpdate, oldCompanyCategory, oldProductCategory, GetUpdate(new UpdateRevisionId(sdp.PackageId)));
            }
            catch (Exception ex)
            {
                Logger.Write("*** Fail to revise update :\r\n" + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
#if !DEBUG
            try
            {
                Tools.Utilities.DeleteFolder(tmpFolderPath);
            }
            catch (Exception)
            { }
#endif
            if (!_wsusServer.IsLocal)
                _timer.Start();
        }

        internal string ResignPackage(IUpdate update)
        {
            Logger.EnteringMethod(update.Title);
            string result = resMan.GetString("SuccessfullyResignPackage");

            try
            {
                string tmpFile = Tools.Utilities.GetTempFolder() + update.Id.UpdateId.ToString() + ".xml";
                wsus.ExportPackageMetadata(update.Id, tmpFile);
                IPublisher publisher = wsus.GetPublisher(tmpFile);
                publisher.ResignPackage();
                System.IO.File.Delete(tmpFile);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                result = resMan.GetString("ResignPackageFailed") + " : " + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// Delete an Update.
        /// </summary>
        /// <param name="updateToDelete">Update to delete.</param>
        internal void DeleteUpdate(IUpdate updateToDelete)
        {
            Logger.EnteringMethod();
            try
            {
                IUpdateCategory productCategory = updateToDelete.GetUpdateCategories()[0];
                IUpdateCategory vendorCategory = productCategory.GetParentUpdateCategory();

                wsus.DeleteUpdate(updateToDelete.Id.UpdateId);
                if (UpdateDeleted != null)
                    UpdateDeleted(updateToDelete, vendorCategory, productCategory);
            }
            catch (System.InvalidOperationException ex)
            {
                Logger.Write("**** " + ex.Message);
                string message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                    message += "\r\n" + ex.InnerException.Message + "\r\n" + resMan.GetString("SearchPrerequisite");

                if (System.Windows.Forms.MessageBox.Show(message, string.Empty, System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    SearchInPrerequisites(updateToDelete);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void SearchInPrerequisites(IUpdate updateToDelete)
        {
            Logger.EnteringMethod(updateToDelete.Title);
            FrmPrerequisiteSubscribers frmPrerequisite = new FrmPrerequisiteSubscribers(updateToDelete);

            frmPrerequisite.ShowDialog();
            switch (frmPrerequisite.ActionOnSubsribers)
            {
                case FrmPrerequisiteSubscribers.ActionsOnSubsribers.UnsubscribeAndDelete:
                    StartWaitingForm(resMan.GetString("Unsubscribing"));
                    ReviseSubscribers(updateToDelete, frmPrerequisite.Subscribers);
                    StopWaitingForm();
                    DeleteUpdate(updateToDelete);
                    break;
                case FrmPrerequisiteSubscribers.ActionsOnSubsribers.Unsubscribe:
                    StartWaitingForm(resMan.GetString("Unsubscribing"));
                    ReviseSubscribers(updateToDelete, frmPrerequisite.Subscribers);
                    StopWaitingForm();
                    break;
                case FrmPrerequisiteSubscribers.ActionsOnSubsribers.DoNothing:
                    break;
                default:
                    break;
            }
        }

        private void ReviseSubscribers(IUpdate updateToDelete, UpdateCollection subscribers)
        {
            Logger.EnteringMethod(updateToDelete.Title);
            if (!_wsusServer.IsLocal)
                _timer.Stop();
            string tmpFolderPath;

            foreach (IUpdate subscriber in subscribers)
            {
                IUpdate oldUpdate = GetUpdate(subscriber.Id);
                IUpdateCategory oldProductCategory = oldUpdate.GetUpdateCategories()[0];
                IUpdateCategory oldCompanyCategory = oldProductCategory.GetParentUpdateCategory();
                SoftwareDistributionPackage sdp = GetMetaData(subscriber);
                if (sdp != null)
                {
                    sdp = Unsubscribe(sdp, updateToDelete.Id.UpdateId);

                    tmpFolderPath = Tools.Utilities.GetTempFolder();
                    if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                        System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
                    if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                        System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");

                    sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
                    System.IO.FileInfo sdpFile = new System.IO.FileInfo(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
                    Logger.Write("Saving Sdp : " + sdpFile.OpenText().ReadToEnd());

                    IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
                    publisher.RevisePackage();
#if !DEBUG
                    Tools.Utilities.DeleteFolder(tmpFolderPath + sdp.PackageId);
#endif
                    if (UpdateRevised != null)
                        UpdateRevised(oldUpdate, oldCompanyCategory, oldProductCategory, GetUpdate(new UpdateRevisionId(sdp.PackageId)));
                }
            }
            System.Threading.Thread.Sleep(1000);
            ICleanupManager cleanupWizard = wsus.GetCleanupManager();
            CleanupScope scope = new CleanupScope();
            scope.CompressUpdates = true;
            cleanupWizard.PerformCleanup(scope);

            if (!_wsusServer.IsLocal)
                _timer.Start();
        }

        private SoftwareDistributionPackage Unsubscribe(SoftwareDistributionPackage sdp, Guid idToRemove)
        {
            Logger.EnteringMethod();
            IList<PrerequisiteGroup> groupOfPrerequisites = sdp.Prerequisites;
            List<PrerequisiteGroup> groupToRemove = new List<PrerequisiteGroup>();

            if (groupOfPrerequisites.Count != 0)
                for (int i = 0; i < groupOfPrerequisites.Count; i++)
                {
                    if (groupOfPrerequisites[i].Ids.Contains(idToRemove))
                    {
                        groupOfPrerequisites[i].Ids.Remove(idToRemove);
                        if (groupOfPrerequisites[i].Ids.Count == 0)
                            groupToRemove.Add(groupOfPrerequisites[i]);
                    }
                }
            foreach (PrerequisiteGroup group in groupToRemove)
            {
                groupOfPrerequisites.Remove(group);
            }

            return sdp;
        }

        private void StartWaitingForm(string description)
        {
            _waitingForm = new FrmWaiting();
            _waitingForm.Description = description;
            _waitingForm.GoOn = true;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            _waitingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_waitingForm.ShowForm));
            _waitingThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _waitingThread.Start();
            System.Threading.Thread.Sleep(200);
        }

        private void StopWaitingForm()
        {
            _waitingForm.GoOn = false;
            _waitingThread.Join(900);
            _waitingThread = null;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
        }

        /// <summary>
        /// Decline the update.
        /// </summary>
        /// <param name="updateToDecline">Update to Decline</param>
        internal void DeclineUpdate(IUpdate updateToDecline)
        {
            Logger.EnteringMethod(updateToDecline.Title);
            if (!updateToDecline.IsDeclined)
                updateToDecline.Decline();
            if (UpdateDeclined != null)
                UpdateDeclined(GetUpdate(updateToDecline.Id));
        }

        internal void ApproveUpdateForInstallation(Guid computerGroupId, IUpdate updateToApprove, DateTime deadLine)
        {
            Logger.EnteringMethod(updateToApprove.Title);
            try
            {
                updateToApprove.Approve(UpdateApprovalAction.Install, GetComputerGroup(computerGroupId), deadLine);
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToApprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void ApproveUpdateForInstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            Logger.EnteringMethod(updateToApprove.Title);
            try
            {
                updateToApprove.Approve(UpdateApprovalAction.Install, GetComputerGroup(computerGroupId));
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToApprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void ApproveUpdateForUninstallation(Guid computerGroupId, IUpdate updateToApprove, DateTime deadLine)
        {
            Logger.EnteringMethod(updateToApprove.Title);
            try
            {
                updateToApprove.Approve(UpdateApprovalAction.Uninstall, GetComputerGroup(computerGroupId), deadLine);
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToApprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void ApproveUpdateForUninstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            Logger.EnteringMethod(updateToApprove.Title);
            try
            {
                updateToApprove.Approve(UpdateApprovalAction.Uninstall, GetComputerGroup(computerGroupId));
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToApprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void ApproveUpdateForOptionalInstallation(Guid computerGroupId, IUpdate updateToApprove)
        {
            Logger.EnteringMethod(updateToApprove.Title);
            try
            {
                updateToApprove.ApproveForOptionalInstall(GetComputerGroup(computerGroupId));
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToApprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void DisapproveUpdate(Guid computerGroupId, IUpdate updateToDisapprove)
        {
            Logger.EnteringMethod(updateToDisapprove.Title);
            try
            {
                updateToDisapprove.Approve(UpdateApprovalAction.NotApproved, GetComputerGroup(computerGroupId));
                if (UpdateApprovalChange != null)
                    UpdateApprovalChange(GetUpdate(updateToDisapprove.Id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal void ExpireUpdate(IUpdate updateToExpire)
        {
            Logger.EnteringMethod(updateToExpire.Title);
            try
            {
                updateToExpire.ExpirePackage();
                UpdateRevisionId id = new UpdateRevisionId(updateToExpire.Id.UpdateId, ++updateToExpire.Id.RevisionNumber);
                if (UpdateExpired != null)
                    UpdateExpired(GetUpdate(id));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        internal UpdateApprovalCollection GetUpdateApprovalStatus(Guid groupId, IUpdate update)
        {
            return update.GetUpdateApprovals(GetComputerGroup(groupId));
        }

        internal SoftwareDistributionPackage GetMetaData(IUpdate update)
        {
            Logger.EnteringMethod(update.Title);
            string tmpFolder = Tools.Utilities.GetTempFolder();
            string tmpFile = tmpFolder + update.Id.UpdateId.ToString() + ".xml";
            System.IO.StreamReader reader = null;
            SoftwareDistributionPackage sdp = null;
            try
            {
                wsus.ExportPackageMetadata(update.Id, tmpFile);
                sdp = new SoftwareDistributionPackage(tmpFile);
                reader = new System.IO.StreamReader(tmpFile);
                Logger.Write(reader.ReadToEnd());
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(resMan.GetString("ErrorGetMetaData"));
            }
            try
            {
                if (reader != null)
                {
                    reader.Close();
                    Tools.Utilities.DeleteFolder(tmpFolder);
                }
            }
            catch (Exception) { }

            return sdp;
        }

        /// <summary>
        /// Return the IComputerTarget of the 'computerName'.
        /// </summary>
        /// <param name="computerName">Name of the computer you want to obtain the IComputerTarget object.</param>
        /// <returns>IComputerTarget of the computer.</returns>
        internal IComputerTarget GetComputerTargetByName(string computerName)
        {
            Logger.EnteringMethod(computerName);
            IComputerTarget computer = null;
            try
            {
                computer = wsus.GetComputerTargetByName(computerName);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            return computer;
        }

        /// <summary>
        /// Get a group of target computers.
        /// </summary>
        /// <param name="groupId">Id of the group you want.</param>
        /// <returns>A IComputerTargetGroup coresponding of the Id.</returns>
        internal IComputerTargetGroup GetComputerGroup(Guid groupId)
        {
            return wsus.GetComputerTargetGroup(groupId);
        }

        internal UpdateEventCollection GetUpdateEventHistory(IComputerTarget computer)
        {
            Logger.EnteringMethod(computer.FullDomainName);
            UpdateEventCollection result = new UpdateEventCollection();
            try
            {
                DateTime from = new DateTime();
                DateTime to = DateTime.Now;

                result = wsus.GetUpdateEventHistory(from, to, computer);
                Logger.Write("Will return " + result.Count + " event(s).");
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            return result;
        }

        internal void DeleteReportingEvents(IComputerTarget computer)
        {
            Logger.EnteringMethod(computer.FullDomainName);
            try
            {
                DateTime from = new DateTime();
                DateTime to = DateTime.Now;

                computer.PurgeAssociatedReportingEvents(from, to);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
        }

        internal bool GenerateSelfSignedCertificate()
        {
            Logger.EnteringMethod();
            Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();

            if (GetServerVersion().CompareTo(new Version(6, 3)) >= 0)
            {
                Logger.Write("Wsus is at least 6.3");
                if (CurrentServer.IsLocal)
                {
                    Logger.Write("Wsus is local.");
                    System.Security.Cryptography.X509Certificates.X509Certificate2 selfSignedCertificate = CertificateHelper.CreateSelfSignedCertificate("WPP Self-Signed");
                    string tmpFolder = Tools.Utilities.GetTempFolder();
                    string tempCertFilePath = tmpFolder + "selfSignedCertificate.pfx";
                    string certPassword = Guid.NewGuid().ToString();
                    byte[] bytes = selfSignedCertificate.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Pfx, certPassword);
                    System.IO.FileStream writer = new System.IO.FileStream(tempCertFilePath, System.IO.FileMode.Create);
                    writer.Write(bytes, 0, bytes.Length);
                    writer.Flush();
                    writer.Close();

                    UseExistingCertificate(tempCertFilePath, certPassword);
                    Tools.Utilities.DeleteFolder(tmpFolder);
                }
                else
                {
                    Logger.Write("Wsus is not local .");
                    System.Windows.Forms.MessageBox.Show(resMan.GetString("MustRunLocallyToGenerateSelfSignedCertificateOn6.3"));
                    return false;
                }

            }
            else
            {
                Logger.Write("Wsus is less than 6.3");
                wsusConfiguration.SetSigningCertificate();
                wsusConfiguration.Save();
                System.Threading.Thread.Sleep(1000);
                if (CurrentServer.IsLocal)
                {
                    Logger.Write("WPP run on local Wsus. Certificate will be imported in the Root Store and TrustedPublisher Store.");
                    string tmpFolder = Tools.Utilities.GetTempFolder();
                    string tempCertFilePath = tmpFolder + "TempCert.cer";
                    wsusConfiguration.GetSigningCertificate(tempCertFilePath);
                    CertificateHelper.InstallCertificate(tempCertFilePath, System.Security.Cryptography.X509Certificates.StoreName.TrustedPublisher);
                    CertificateHelper.InstallCertificate(tempCertFilePath, System.Security.Cryptography.X509Certificates.StoreName.Root);
                    Tools.Utilities.DeleteFolder(tmpFolder);
                }
            }
            return true;
        }

        internal void UseExistingCertificate(string filePath, string certPassword)
        {
            Logger.EnteringMethod();
            Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();

            wsusConfiguration.SetSigningCertificate(filePath, certPassword);
            wsusConfiguration.Save();
            System.Threading.Thread.Sleep(1000);
            if (CurrentServer.IsLocal)
            {
                Logger.Write("WPP run on local Wsus. Certificate will be imported in the TrustedPublisher Store.");
                string tmpFolder = Tools.Utilities.GetTempFolder();
                string tempCertFilePath = tmpFolder + "TempCert.cer";
                wsusConfiguration.GetSigningCertificate(tempCertFilePath);
                CertificateHelper.InstallCertificate(tempCertFilePath, System.Security.Cryptography.X509Certificates.StoreName.TrustedPublisher);
                if (CertificateHelper.IsSelfSigned(filePath, certPassword))
                {
                    Logger.Write("This is a selfsigned certificate. It will be imported in the Root Store.");
                    CertificateHelper.InstallCertificate(tempCertFilePath, System.Security.Cryptography.X509Certificates.StoreName.Root);
                }
                Tools.Utilities.DeleteFolder(tmpFolder);
            }
        }

        internal bool SaveCertificate(string filePath)
        {
            Logger.EnteringMethod(filePath);

            try
            {
                Microsoft.UpdateServices.Administration.IUpdateServerConfiguration wsusConfiguration = wsus.GetConfiguration();
                wsusConfiguration.GetSigningCertificate(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return false;
        }

        internal bool ImportAnUpdate(string workingDirectory, string sdpFileName, string cabFileName)
        {
            Logger.EnteringMethod();
            try
            {
                IPublisher publisher = wsus.GetPublisher(workingDirectory + @"\" + sdpFileName);
                SoftwareDistributionPackage sdp = new SoftwareDistributionPackage(workingDirectory + @"\" + sdpFileName);

                try
                {
                    IUpdate alreadyImportedUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));
                    Logger.Write("The update already exists.");
                }
                catch (Exception)
                {
                    Logger.Write("The udpate doesn't already exists. Continue with import...");
                    publisher.ProgressHandler += new EventHandler<PublishingEventArgs>(publisher_Progress);
                    publisher.PublishSignedPackage(workingDirectory + @"\" + cabFileName, null);
                    Logger.Write("Update Successfuly imported.");

                    IUpdate importedUpdate = GetUpdate(new UpdateRevisionId(sdp.PackageId));
                    Logger.Write("\"" + importedUpdate.Title + "\" have been imported.");

                    if (UpdatePublished != null)
                        UpdatePublished(importedUpdate);
                    return true;
                }
                Logger.Write("There's already an update with this Package ID. No import will be perform.");
                System.Windows.Forms.MessageBox.Show(resMan.GetString("AlreadyAnUpdateWithThisID"));
                return false;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void publisher_Progress(object sender, EventArgs e)
        {
            if (UpdatePublishingProgress != null)
                UpdatePublishingProgress(sender, e);
        }

        ~WsusWrapper()
        {
            _timer.Stop();
            _timer = null;
        }

        public delegate void UpdateDeclinedEventHandler(IUpdate declinedUpdate);
        public event UpdateDeclinedEventHandler UpdateDeclined;

        public delegate void UpdateExpiredEventHandler(IUpdate expiredUpdate);
        public event UpdateExpiredEventHandler UpdateExpired;

        public delegate void UpdateDeletedEventHandler(IUpdate deletedUpdate, IUpdateCategory companyCategory, IUpdateCategory productCategory);
        public event UpdateDeletedEventHandler UpdateDeleted;

        public delegate void UpdatePublishingProgressEventHandler(object sender, EventArgs e);
        public event UpdatePublishingProgressEventHandler UpdatePublishingProgress;

        public delegate void UpdatePublisedEventHandler(IUpdate PublishedUpdate);
        public event UpdatePublisedEventHandler UpdatePublished;

        public delegate void UpdateRevisedEventHandler(IUpdate oldUpdate, IUpdateCategory oldCompanyCategory, IUpdateCategory oldProductCategory, IUpdate RevisedUpdate);
        public event UpdateRevisedEventHandler UpdateRevised;

        public delegate void UpdateSupersededEventHandler(IList<Guid> SupersededUpdates);
        public event UpdateSupersededEventHandler UpdateSuperseded;

        public delegate void UpdateApprovalChangeEventHandler(IUpdate ApprovedUpdate);
        public event UpdateApprovalChangeEventHandler UpdateApprovalChange;

    }
}
