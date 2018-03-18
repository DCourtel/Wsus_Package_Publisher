using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.Security;

namespace WPP.Management
{
    public class WppComputer
    {
        private IComputerServices _computerServices;


        public WppComputer(string name)
        {
            _computerServices = new WppComputerServices();
            this.ComputerName = name;
            this.SetDefaultValue();
        }

        public WppComputer(string name, string ouName, DateTime lastLogon, string osName, string osServicePack, string osVersion)
        {
            _computerServices = new WppComputerServices();
            this.ComputerName = name;
            this.OUName = ouName;
            this.LastLogon = lastLogon;
            this.WsusClientID = String.Empty;
            this.OSName = osName;
            this.OSServicePack = osServicePack;
            this.OSVersion = osVersion;
            this.ServiceStatus = String.Empty;
            this.ServiceStartingMode = String.Empty;
            this.IsInWsus = String.Empty;
            this.HasDuplicateWsusClientID = String.Empty;
        }

        public WppComputer(string name, IComputerServices fakeComputerServices)
        {
            _computerServices = fakeComputerServices;
            this.ComputerName = name;
            this.SetDefaultValue();
        }

        #region (properties)

        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // Properties with "ExportableAttribute" set to "true", must appears into resources files to allow translation
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        [DataGridViewDataAttribute(true, 5f, true, false)]
        public string ComputerName { get; set; }

        [DataGridViewDataAttribute(true, 15f, false, true)]
        public string OUName { get; set; }

        [DataGridViewDataAttribute(true, 7f, true, true)]
        public DateTime LastLogon { get; set; }

        [DataGridViewDataAttribute(true, 15f, true, true)]
        public string WsusClientID { get; set; }

        [DataGridViewDataAttribute(true, 13f, true, true)]
        public string OSName { get; set; }

        [DataGridViewDataAttribute(true, 7f, false, true)]
        public string OSServicePack { get; set; }

        [DataGridViewDataAttribute(true, 7f, false, true)]
        public string OSVersion { get; set; }

        [DataGridViewDataAttribute(true, 5f, true, true)]
        public string ServiceStatus { get; set; }

        [DataGridViewDataAttribute(true, 5f, true, true)]
        public string ServiceStartingMode { get; set; }

        [DataGridViewDataAttribute(true, 5f, true, true)]
        public string IsInWsus { get; set; }

        [DataGridViewDataAttribute(true, 5f, true, true)]
        public string HasDuplicateWsusClientID { get; set; }

        [DataGridViewDataAttribute(false, 1f, false, true)]
        public WppComputer ComputerObj { get { return this; } }

        //[DataGridViewDataAttribute(false, 1f, false,true)]
        public int DatatableIndex { get; set; }


        #endregion (properties)

        #region (methods)

        public bool Ping(int timeout)
        {
            try
            {
                if (!Ping(GetIPv4Address(this.ComputerName), timeout))
                    return Ping(GetIPv6Address(this.ComputerName), timeout);

                return true;
            }
            catch (Exception) { }

            return false;
        }

        private System.Net.IPAddress GetIPv4Address(string hostname)
        {
            try
            {
                System.Net.IPHostEntry hostEntry = _computerServices.GetHostEntry(hostname);

                foreach (System.Net.IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address;
                    }
                }
            }
            catch (Exception) { }

            return null;
        }

        private System.Net.IPAddress GetIPv6Address(string hostname)
        {
            try
            {
                System.Net.IPHostEntry hostEntry = _computerServices.GetHostEntry(hostname);

                foreach (System.Net.IPAddress address in hostEntry.AddressList)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        return address;
                    }
                }
            }
            catch (Exception) { }

            return null;
        }

        private bool Ping(System.Net.IPAddress addressToPing, int timeout)
        {
            try
            {
                return _computerServices.Ping(addressToPing, timeout) == System.Net.NetworkInformation.IPStatus.Success;
            }
            catch (Exception) { }

            return false;
        }

        public string GetWindowsUpdateLog(Credential credential)
        {
            string uncPath = @"\\" + this.ComputerName + @"\C$\Windows";
            string remoteFilename = "WindowsUpdate.log";
            return _computerServices.GetRemoteFileContent(uncPath, remoteFilename, credential);
        }

        public string GetWsusClientID(Credential credential)
        {
            this.WsusClientID = _computerServices.GetWsusClientID(this.ComputerName, credential);
            return this.WsusClientID;
        }

        public string GetWindowsUpdateServiceStatus(Credential credential)
        {
            return _computerServices.GetWindowsUpdateServiceStatus(this.ComputerName, credential);
        }

        public void SendDetectNow(Credential credential)
        {
            _computerServices.SendCommand(this.ComputerName, "DetectNow", credential);
        }

        public void SendReportNow(Credential credential)
        {
            _computerServices.SendCommand(this.ComputerName, "ReportNow", credential);
        }

        public void RebootComputer(Credential credential, bool force, string message)
        {
            _computerServices.RebootComputer(this.ComputerName, credential, force, message);
        }

        public void StopWindowsUpateService(Credential credential)
        {
            _computerServices.ManageService(this.ComputerName, credential, "StopService");
        }

        public void StartWindowsUpdateService(Credential credential)
        {
            _computerServices.ManageService(this.ComputerName, credential, "StartService");
        }

        public void DeleteWsusClientID(Credential credential)
        {
            _computerServices.DeleteWsusClientID(this.ComputerName, credential);
        }

        public static string GetFullyQualifiedDomainName()
        {
            return WppComputerServices.GetFullyQualifiedDomainName();
        }

        public static List<System.Reflection.PropertyInfo> GetDataGridViewProperties()
        {
            List<System.Reflection.PropertyInfo> dataGridViewProperties = new List<System.Reflection.PropertyInfo>();

            System.Reflection.PropertyInfo[] properties = typeof(WppComputer).GetProperties();
            foreach (System.Reflection.PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(typeof(WPP.Management.DataGridViewDataAttribute), false);
                if (attributes.Length != 0)
                {
                    dataGridViewProperties.Add(property);
                }
            }

            return dataGridViewProperties;
        }

        private void SetDefaultValue()
        {
            this.OUName = String.Empty;
            this.LastLogon = new DateTime();
            this.WsusClientID = String.Empty;
            this.OSName = String.Empty;
            this.OSServicePack = String.Empty;
            this.OSVersion = String.Empty;
            this.ServiceStatus = String.Empty;
            this.ServiceStartingMode = String.Empty;
            this.IsInWsus = String.Empty;
            this.HasDuplicateWsusClientID = String.Empty;
        }

        public override string ToString()
        {
            return this.ComputerName;
        }

        #endregion (methods)
    }
}
