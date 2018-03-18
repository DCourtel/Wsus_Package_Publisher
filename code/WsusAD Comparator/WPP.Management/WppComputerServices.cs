using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.Security;
using System.Management;

namespace WPP.Management
{
    class WppComputerServices : IComputerServices
    {
        private const UInt32 HKLM = 0x80000002;


        public System.Net.NetworkInformation.IPStatus Ping(System.Net.IPAddress addressToPing, int timeout)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            return ping.Send(addressToPing, timeout).Status;
        }

        public System.Net.IPHostEntry GetHostEntry(string hostname)
        {
            return System.Net.Dns.GetHostEntry(hostname);
        }

        public string GetRemoteFileContent(string uncPath, string filename, Credential credential)
        {
            string remoteFileContent = String.Empty;
            NetUse netUse = new NetUse();
            if (netUse.Mount(string.Empty, uncPath, credential))
            {
                string remoteFile = System.IO.Path.Combine(uncPath, filename);

                System.IO.StreamReader reader = new System.IO.StreamReader(remoteFile);
                remoteFileContent = reader.ReadToEnd();
                try
                {
                    reader.Close();
                    netUse.UnMount(uncPath);
                }
                catch (Exception) { }
            }

            return remoteFileContent;
        }

        public string GetWsusClientID(string hostname, Credential credential)
        {
            string wsusClientID = String.Empty;

            ConnectionOptions connectoptions = GetConnectionOptions(hostname, credential);
            ManagementScope scope = new ManagementScope("\\\\" + hostname + "\\root\\default", connectoptions);
            ManagementPath path = new ManagementPath("StdRegProv");
            ManagementClass mgmtClass = new ManagementClass(scope, path, null);

            ManagementBaseObject mgmtBaseObject = mgmtClass.GetMethodParameters("GetStringValue");
            mgmtBaseObject["hDefKey"] = HKLM;
            mgmtBaseObject["sSubKeyName"] = @"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate";
            mgmtBaseObject["sValueName"] = "SusClientId";
            ManagementBaseObject outParams = mgmtClass.InvokeMethod("GetStringValue", mgmtBaseObject, null);
            if (Convert.ToUInt32(outParams["ReturnValue"]) == 0)
                wsusClientID = outParams["sValue"].ToString();

            return wsusClientID;
        }

        public string GetWindowsUpdateServiceStatus(string hostname, Credential credential)
        {
            string result = String.Empty;

            try
            {
                ConnectionOptions connectoptions = GetConnectionOptions(hostname, credential);
                ManagementScope scope = new ManagementScope(@"\\" + hostname + @"\root\cimv2");
                scope.Options = connectoptions;
                SelectQuery query = new SelectQuery("select * from Win32_Service where name = 'wuauserv'");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    ManagementObjectCollection collection = searcher.Get();
                    foreach (ManagementObject service in collection)
                        result = service.GetPropertyValue("State").ToString() + ";(" + service.GetPropertyValue("StartMode").ToString() + ")";
                }
            }
            catch (Exception) { result = "Failed"; }

            return result;
        }

        public void SendCommand(string hostname, string command, Credential credential)
        {
                ConnectionOptions connectoptions = GetConnectionOptions(hostname, credential);
                connectoptions.Impersonation = System.Management.ImpersonationLevel.Impersonate;

                System.Management.ManagementScope mgmtScope = new System.Management.ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", hostname), connectoptions);
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
        }

        public void RebootComputer(string hostname, Credential credential, bool force, string message)
        {
                ConnectionOptions connOptions = GetConnectionOptions(hostname, credential);
                connOptions.Impersonation = ImpersonationLevel.Impersonate;
                connOptions.EnablePrivileges = true;
                ManagementScope mgmtScope = new ManagementScope(String.Format(@"\\{0}\ROOT\CIMV2", hostname), connOptions);

                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                string commandLine = "Shutdown -r";

                if (force) { commandLine += " -f"; }
                if (!String.IsNullOrEmpty(message)) { commandLine += " -c \"" + message + "\""; }
                inParams["CommandLine"] = commandLine;

                processClass.InvokeMethod("Create", inParams, null);
                processClass.Dispose();
                mgmtPath = null;
                objectGetOptions = null;
                mgmtScope = null;
                connOptions = null;
        }

        public void ManageService(string hostname, Credential credential, string command)
        {
            ConnectionOptions connectoptions = GetConnectionOptions(hostname, credential);
            ManagementScope scope = new ManagementScope(@"\\" + hostname + @"\root\cimv2");
            scope.Options = connectoptions;
            SelectQuery query = new SelectQuery("select * from Win32_Service where name = 'wuauserv'");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
            {
                ManagementObjectCollection collection = searcher.Get();
                foreach (ManagementObject service in collection)
                    service.InvokeMethod(command, null);
            }
        }

        public void DeleteWsusClientID(string hostname, Credential credential)
        {
            ConnectionOptions connectoptions = GetConnectionOptions(hostname, credential);
            ManagementScope scope = new ManagementScope("\\\\" + hostname + "\\root\\default", connectoptions);
            ManagementPath path = new ManagementPath("StdRegProv");
            ManagementClass mgmtClass = new ManagementClass(scope, path, null);

            ManagementBaseObject mgmtBaseObject = mgmtClass.GetMethodParameters("DeleteValue");
            mgmtBaseObject["hDefKey"] = HKLM;
            mgmtBaseObject["sSubKeyName"] = @"SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate";
            mgmtBaseObject["sValueName"] = "SusClientId";
            mgmtClass.InvokeMethod("DeleteValue", mgmtBaseObject, null);
        }

        /// <summary>
        /// Gets a new instance of ConnectionOptions with Username and Password properties appropriately filled
        /// </summary>
        /// <param name="hostname">Name of the remote computer.</param>
        /// <param name="credential">Credential that will be used to connect to the remote computer.</param>
        /// <returns>A new instance of ConnectionOptions</returns>
        private ConnectionOptions GetConnectionOptions(string hostname, Credential credential)
        {
            ConnectionOptions connectionOpt = new ConnectionOptions();
            if (!credential.IsEmpty && hostname.ToLower() != GetFullyQualifiedDomainName().ToLower())
            {
                connectionOpt.Username = credential.Username;
                connectionOpt.Password = credential.Password;
            }
            return connectionOpt;
        }

        /// <summary>
        /// Gets the fully qualified domain name of the computer where this app runs.
        /// </summary>
        /// <returns>The fully qualified domain name of the computer where this app runs.</returns>
        public static string GetFullyQualifiedDomainName()
        {
            System.Net.NetworkInformation.IPGlobalProperties ipProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            return string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);
        }
    }
}
