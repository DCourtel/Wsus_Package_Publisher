using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using WUApiLib;


namespace Wsus_Package_Publisher
{
    public class ADComputer
    {
        internal enum NetworkStatus
        {
            Connected,
            Unreachable,
            Unknown
        }

        internal enum InstallPendingUpdatesResult
        {
            ComputerUnreachable,
            CommandSent,
            FailToCopyFile,
            FailToSendCommand
        }

        private const UInt32 HKLM = 0x80000002;
        private string _name;
        private string _ouName;
        private DateTime _lastLogon;
        private string _susClientID;
        private string _wuAuSrvStatus;
        private string _OSName;
        private string _OSServicePack;
        private string _OSVersion;
        private bool _isInWsus;
        private bool _hasDuplicateWsusClientID;
        private NetworkStatus _networkState;

        internal ADComputer(string computerName, string ouName, DateTime lastLogon, string osName, string osServicePack, string osVersion)
        {
            this._name = computerName;
            this._ouName = ouName;
            this._lastLogon = lastLogon;
            this._susClientID = string.Empty;
            this._wuAuSrvStatus = string.Empty;
            this._OSName = osName;
            this._OSServicePack = osServicePack;
            this._OSVersion = osVersion;
            this._isInWsus = false;
            this._hasDuplicateWsusClientID = false;
            this._networkState = NetworkStatus.Unknown;
        }

        public ADComputer(string computerName)
        {
            this._name = computerName;
            this._ouName = string.Empty;
            this._lastLogon = DateTime.Now;
            this._susClientID = string.Empty;
            this._wuAuSrvStatus = string.Empty;
            this._OSName = string.Empty;
            this._OSServicePack = string.Empty;
            this._OSVersion = string.Empty;
            this._isInWsus = false;
            this._hasDuplicateWsusClientID = false;
            this._networkState = NetworkStatus.Unknown;
        }

        internal string Name
        {
            get { return _name; }
        }

        internal string OUName
        {
            get { return _ouName; }
        }

        internal DateTime LastLogon
        {
            get { return _lastLogon; }
        }

        internal string SusClientID
        {
            get
            {
                return _susClientID;
            }
        }

        internal string WuAuServiceStatus { get { return _wuAuSrvStatus; } }

        internal string OSName { get { return _OSName; } }

        internal string OSServicePack { get { return _OSServicePack; } }

        internal string OSVersion { get { return _OSVersion; } }

        internal bool IsInWsus
        {
            get { return _isInWsus; }
            set { _isInWsus = value; }
        }

        internal bool HasDuplicateWsusClientID
        {
            get { return _hasDuplicateWsusClientID; }
            set { _hasDuplicateWsusClientID = value; }
        }

        internal NetworkStatus NetworkState
        {
            get { return _networkState; }
            set { _networkState = value; }
        }

        internal bool Ping(int timeout)
        {
            string pingStd = Properties.Settings.Default.PingStandard;
            bool pingResult = false;

            switch (pingStd)
            {
                case "IPv4":
                    pingResult = Ping(timeout, GetIPv4Address());
                    break;
                case "IPv6":
                    pingResult = Ping(timeout, GetIPv6Address());
                    break;
                case "IPv6IPv4":
                    pingResult = Ping(timeout, GetIPv6Address());
                    if (!pingResult)
                        pingResult = Ping(timeout, GetIPv4Address());
                    break;
                default:
                    pingResult = Ping(timeout, GetIPv4Address());
                    break;
            }
            return pingResult;
        }

        private bool Ping(int timeout, System.Net.IPAddress addressToPing)
        {
            Logger.EnteringMethod();
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply reply = null;

            try
            {
                if (addressToPing != null)
                {
                    Logger.Write("Will Ping : " + addressToPing.ToString());
                    reply = ping.Send(addressToPing, timeout);
                    if (reply != null && reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        NetworkState = NetworkStatus.Connected;
                        Logger.Write("Will return true, first try");
                        return true;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(100);
                        reply = ping.Send(addressToPing, timeout);
                        if (reply != null && reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        {
                            NetworkState = NetworkStatus.Connected;
                            Logger.Write("Will return true, second try");
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            NetworkState = NetworkStatus.Unreachable;
            Logger.Write("Will return false");
            return false;
        }

        private System.Net.IPAddress GetIPv4Address()
        {
            Logger.EnteringMethod();
            try
            {
                System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(this.Name);
                foreach (System.Net.IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        Logger.Write("will return : " + address.ToString());
                        return address;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            Logger.Write("Will return null");
            return null;
        }

        private System.Net.IPAddress GetIPv6Address()
        {
            Logger.EnteringMethod();
            try
            {
                System.Net.IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(this.Name);
                foreach (System.Net.IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        Logger.Write("will return : " + address.ToString());
                        return address;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            Logger.Write("Will return null");
            return null;
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void DeclareHasDuplicate()
        {
            HasDuplicateWsusClientID = true;
        }

        internal void QuerySusClientID(string username, string password)
        {
            Logger.EnteringMethod();
            _susClientID = "Failed to Read Key";

            ConnectionOptions connectoptions = new ConnectionOptions();
            if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
            {
                Logger.Write("Usename & password are provided");
                connectoptions.Username = username;
                connectoptions.Password = password;
            }
            ManagementScope scope = new ManagementScope("\\\\" + this.Name + "\\root\\default", connectoptions);
            ManagementPath path = new ManagementPath("StdRegProv");
            ManagementClass mgmtClass = new ManagementClass(scope, path, null);

            ManagementBaseObject mgmtBaseObject = mgmtClass.GetMethodParameters("GetStringValue");
            mgmtBaseObject["hDefKey"] = HKLM;
            mgmtBaseObject["sSubKeyName"] = @"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate";
            mgmtBaseObject["sValueName"] = "SusClientId";
            ManagementBaseObject outParams = mgmtClass.InvokeMethod("GetStringValue", mgmtBaseObject, null);
            if (Convert.ToUInt32(outParams["ReturnValue"]) == 0)
                _susClientID = outParams["sValue"].ToString();
        }

        internal string QueryWuAuSrvStatus(string username, string password)
        {
            Logger.EnteringMethod();
            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
                {
                    Logger.Write("Username & password are provided");
                    connectoptions.Username = username;
                    connectoptions.Password = password;
                }
                ManagementScope scope = new ManagementScope(@"\\" + this.Name + @"\root\cimv2");
                scope.Options = connectoptions;
                SelectQuery query = new SelectQuery("select * from Win32_Service where name = 'wuauserv'");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject service in collection)
                        _wuAuSrvStatus = service.GetPropertyValue("State").ToString() + ";(" + service.GetPropertyValue("StartMode").ToString() + ")";
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                _wuAuSrvStatus = "Failed";
            }
            return _wuAuSrvStatus;
        }

        internal bool ResetWsusClientID(string username, string password)
        {
            Logger.EnteringMethod();
            if (ManageService("wuauserv", "stopservice", username, password))
                if (DeleteWsusClientID(username, password))
                    if (ManageService("wuauserv", "startservice", username, password))
                        if (SendCommand(@"wuauclt /ResetAuthorization /DetectNow", username, password))
                            return true;
            return false;
        }

        internal bool DeleteWsusClientID(string username, string password)
        {
            Logger.EnteringMethod();
            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
                {
                    Logger.Write("Username & password are provided");
                    connectoptions.Username = username;
                    connectoptions.Password = password;
                }
                ManagementScope scope = new ManagementScope("\\\\" + this.Name + "\\root\\default", connectoptions);
                ManagementPath path = new ManagementPath("StdRegProv");
                ManagementClass mgmtClass = new ManagementClass(scope, path, null);

                ManagementBaseObject mgmtBaseObject = mgmtClass.GetMethodParameters("DeleteValue");
                mgmtBaseObject["hDefKey"] = HKLM;
                mgmtBaseObject["sSubKeyName"] = @"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate";
                mgmtBaseObject["sValueName"] = "SusClientId";
                mgmtClass.InvokeMethod("DeleteValue", mgmtBaseObject, null);

            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
            return true;
        }

        internal bool CleanSoftwareDistributionFolder(string username, string password)
        {
            Logger.EnteringMethod(this.Name);
            bool result = false;
            if (Ping(100))
            {
                ManageService("wuauserv", "StopService", username, password);
                ManageService("bits", "StopService", username, password);
                result = DeleteFolder("C", "Windows", "SoftwareDistribution", username, password);
                ManageService("wuauserv", "StartService", username, password);
                ManageService("bits", "StartService", username, password);
                System.Threading.Thread.Sleep(2000);
                SendCommand("wuauClt /DetectNow", username, password);
            }
            Logger.Write("Returning " + result);
            return result;
        }

        internal bool ManageService(string serviceName, string command, string username, string password)
        {
            Logger.EnteringMethod(serviceName + ", " + command);
            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
                {
                    Logger.Write("Username & password will be used");
                    connectoptions.Username = username;
                    connectoptions.Password = password;
                }
                ManagementScope scope = new ManagementScope(@"\\" + this.Name + @"\root\cimv2");
                scope.Options = connectoptions;
                SelectQuery query = new SelectQuery("select * from Win32_Service where name = '" + serviceName + "'");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject service in collection)
                        service.InvokeMethod(command, null);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Command to service : " + serviceName + " FAILED\r\n**** " + ex.Message);
                return false;
            }
            Logger.Write("Command to service : " + serviceName + " Successfuly sent");
            return true;
        }

        internal string GetCurrentLogonUser(string username, string password)
        {
            Logger.EnteringMethod(this.Name);
            string result = string.Empty;

            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
                {
                    Logger.Write("Usename & password are provided");
                    connectoptions.Username = username;
                    connectoptions.Password = password;
                }
                ManagementScope scope = new ManagementScope("\\\\" + this.Name + "\\root\\CIMV2", connectoptions);
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject computer in collection)
                        result = computer["username"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }

            return result;
        }

        internal void OpenWindowsUpdateLog(string username, string password)
        {
            Logger.EnteringMethod();
            string uncPath = @"\\" + this.Name + @"\C$\Windows";
            string remoteFile = "WindowsUpdate.log";
            string localPath = GetTemporaryFolder() + this.Name + "-" + Guid.NewGuid() + ".txt";

            NetUse netUse = new NetUse();
            if (netUse.Mount(string.Empty, uncPath, username, password))
            {
                if (System.IO.File.Exists(uncPath + "\\" + remoteFile))
                {
                    try
                    {
                        string remotePath = uncPath + "\\" + remoteFile;

                        System.IO.File.Copy(remotePath, localPath);

                        System.Diagnostics.ProcessStartInfo process = new System.Diagnostics.ProcessStartInfo(Properties.Settings.Default.OpenWindowsUpdateLogWith, "\"" + localPath + "\"");
                        System.Diagnostics.Process.Start(process);
                        System.Threading.Thread.Sleep(3000);
                        System.IO.File.Delete(localPath);
                        netUse.UnMount(uncPath);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                    }
                }
            }
        }

        internal InstallPendingUpdatesResult InstallPendingUpdates(string[] options, string username, string password)
        {
            Logger.EnteringMethod();
            if (!Ping(100))
                return InstallPendingUpdatesResult.ComputerUnreachable;

            if (CheckTargetDirectory(@"C$\Windows", username, password))
            {
                string commandLine = "sc delete InstallPendingUpdates";
                SendCommand(commandLine, username, password);
                commandLine = "cmd /c start sc.exe create InstallPendingUpdates binPath= \"cmd /c start InstallPendingUpdates.exe " + GetOptions(options) + "\" type= own & net start InstallPendingUpdates & sc delete InstallPendingUpdates";
                if (!SendCommand(commandLine, username, password))
                    return InstallPendingUpdatesResult.FailToSendCommand;
            }
            else
                return InstallPendingUpdatesResult.FailToCopyFile;

            return InstallPendingUpdatesResult.CommandSent;
        }

        private bool CheckTargetDirectory(string remoteDirectory, string username, string password)
        {
            Logger.EnteringMethod(remoteDirectory);
            NetUse netUse = new NetUse();

            try
            {
                if (netUse.Mount(string.Empty, @"\\" + this.Name + @"\" + remoteDirectory, username, password))
                {
                    if (System.IO.File.Exists(@"\\" + this.Name + @"\" + remoteDirectory + @"\InstallPendingUpdates.exe"))
                    {
                        System.Diagnostics.FileVersionInfo remoteFileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(@"\\" + this.Name + @"\" + remoteDirectory + @"\InstallPendingUpdates.exe");
                        System.Diagnostics.FileVersionInfo localFileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo("InstallPendingUpdates.exe");
                        if (remoteFileInfo.FileVersion != localFileInfo.FileVersion)
                        {
                            CopyFile(remoteDirectory, "InstallPendingUpdates.exe");
                        }
                    }
                    else
                        CopyFile(remoteDirectory, "InstallPendingUpdates.exe");

                    if (System.IO.File.Exists(@"\\" + this.Name + @"\" + remoteDirectory + @"\Interop.WUApiLib.dll"))
                    {
                        System.Diagnostics.FileVersionInfo remoteFileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(@"\\" + this.Name + @"\" + remoteDirectory + @"\Interop.WUApiLib.dll");
                        System.Diagnostics.FileVersionInfo localFileInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo("Interop.WUApiLib.dll");
                        if (remoteFileInfo.FileVersion != localFileInfo.FileVersion)
                        {
                            CopyFile(remoteDirectory, "Interop.WUApiLib.dll");
                        }
                    }
                    else
                        CopyFile(remoteDirectory, "Interop.WUApiLib.dll");

                    netUse.UnMount(@"\\" + this.Name + @"\" + remoteDirectory);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }

            return false;
        }

        private bool CopyFile(string remoteDirectory, string filename)
        {
            Logger.EnteringMethod();
            try
            {
                System.IO.File.Copy(filename, @"\\" + this.Name + @"\" + remoteDirectory + @"\" + filename, true);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
            return true;
        }

        private bool SendCommand(string command, string username, string password)
        {
            Logger.EnteringMethod(command);
            try
            {
                ConnectionOptions connectoptions = new ConnectionOptions();
                if (username != null && password != null && this.Name.ToLower() != GetFullyQualifiedDomainName().ToLower())
                {
                    connectoptions.Username = username;
                    connectoptions.Password = password;
                }
                connectoptions.Impersonation = System.Management.ImpersonationLevel.Impersonate;

                System.Management.ManagementScope mgmtScope = new System.Management.ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", this.Name), connectoptions);
                mgmtScope.Connect();
                System.Management.ObjectGetOptions objectGetOptions = new System.Management.ObjectGetOptions();
                System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath("Win32_Process");
                System.Management.ManagementClass processClass = new System.Management.ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                System.Management.ManagementBaseObject inParams = processClass.GetMethodParameters("Create");
                ManagementClass startupInfo = new ManagementClass("Win32_ProcessStartup");
                startupInfo.Properties["ShowWindow"].Value = 0;

                inParams["CommandLine"] = command;
                inParams["ProcessStartupInformation"] = startupInfo;

                processClass.InvokeMethod("Create", inParams, null);
                Logger.Write("Command Sent Successfuly on : " + this.Name);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
            return true;
        }

        private string GetTemporaryFolder()
        {
            Logger.EnteringMethod();
            string tmpFolderPath = Environment.GetEnvironmentVariable("Temp") + "\\Wsus Package Publisher\\";
            if (!System.IO.Directory.Exists(tmpFolderPath))
                System.IO.Directory.CreateDirectory(tmpFolderPath);

            Logger.Write("Returing : " + tmpFolderPath);
            return tmpFolderPath;
        }

        private System.Security.SecureString GetSecureString(string unsecureString)
        {
            System.Security.SecureString secString = new System.Security.SecureString();
            foreach (char letter in unsecureString)
            {
                secString.AppendChar(letter);
            }
            return secString;
        }

        private string GetOptions(string[] options)
        {
            Logger.EnteringMethod();
            string result = String.Empty;

            if (options != null && options.Length > 0)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    result += "\\\"" + options[i] + "\\\" ";
                }
            }
            Logger.Write("Returning : " + result);

            return result;
        }

        private bool DeleteFolder(string drive, string parentFolderName, string foldername, string username, string password)
        {
            Logger.EnteringMethod(drive + ":\\" + parentFolderName + "\\" + foldername);
            NetUse netUse = new NetUse();

            try
            {
                if (netUse.Mount(string.Empty, @"\\" + this.Name + @"\" + drive + "$\\" + parentFolderName, username, password))
                {
                    System.IO.DirectoryInfo folderInfo = new System.IO.DirectoryInfo(@"\\" + this.Name + @"\" + drive + "$\\" + parentFolderName + "\\" + foldername);
                    if (folderInfo.Exists)
                    {
                        int attempt = 0;

                        do
                        {
                            try
                            {
                                attempt++;
                                Logger.Write("Trying to delete : " + foldername);
                                folderInfo.Delete(true);
                                Logger.Write(foldername + " deleted");
                                return true;
                            }
                            catch (Exception ex)
                            {
                                Logger.Write("Failed to delete : " + foldername + ". " + ex.Message);
                                System.Threading.Thread.Sleep(1000);
                            }

                        } while (attempt < 3);

                        Logger.Write("Unable to delete the folder.");
                        return false;
                    }
                    else
                    {
                        Logger.Write("Can't find the folder");
                        return false;
                    }
                }
                else
                {
                    Logger.Write("**** Unable to Mount the folder");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
        }

        private string GetFullyQualifiedDomainName()
        {
            System.Net.NetworkInformation.IPGlobalProperties ipProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            return string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
        }
    }
}
