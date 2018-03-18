using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.Security;
using WPP.Management;

namespace Unit_Tests_Wsus_AD_Comparator
{
    internal class FakeComputerServices : IComputerServices
    {
        public System.Net.NetworkInformation.IPStatus Ping(System.Net.IPAddress addressToPing, int timeout)
        {
            if (addressToPing == null)
                throw new ArgumentException("Invalid IPAddress");
            if (addressToPing.Equals(System.Net.IPAddress.Parse("0.0.0.0")))
            { return System.Net.NetworkInformation.IPStatus.TimedOut; }
            else
            { return System.Net.NetworkInformation.IPStatus.Success; }
        }

        public System.Net.IPHostEntry GetHostEntry(string hostname)
        {
            System.Net.IPHostEntry hostEntries = new System.Net.IPHostEntry();
            System.Net.IPAddress[] ipAddress;

            switch (hostname)
            {
                case "IPv4":
                    ipAddress = new System.Net.IPAddress[] { System.Net.IPAddress.Parse("192.168.10.210"), System.Net.IPAddress.Parse("192.168.10.12") };
                    break;
                case "IPv6":
                    ipAddress = new System.Net.IPAddress[] { System.Net.IPAddress.Parse("fe80::f487:4d2c:de32:bbc1%11"), System.Net.IPAddress.Parse("fe80::f798:4edc:d1b2:bbff%11") };
                    break;
                case "IPv46":
                    ipAddress = new System.Net.IPAddress[] { System.Net.IPAddress.Parse("192.168.10.210"), System.Net.IPAddress.Parse("fe80::f798:4edc:d1b2:bbff%11") };
                    break;
                case "Empty":
                    ipAddress = new System.Net.IPAddress[] { };
                    break;
                case "TimeOut":
                    ipAddress = new System.Net.IPAddress[] { System.Net.IPAddress.Parse("0.0.0.0") };
                    break;
                default:
                    throw new ArgumentException("Unable to resolve : " + hostname);
            }
            hostEntries.AddressList = ipAddress;

            return hostEntries;
        }

        public string GetRemoteFileContent(string uncPath, string filename, Credential credential)
        {
            string remoteFileContent = "My Test Content";

            if (credential.Password == "BadPa55w0rd")
                throw new Exception();

            return remoteFileContent;
        }

        public string GetWsusClientID(string hostname, Credential credential)
        {
            if (credential.Password == "BadPa55w0rd")
                throw new Exception();

            return "E818BBED-1659-4513-A98F-A113FF4522FD";
        }

        public string GetWindowsUpdateServiceStatus(string hostname, Credential credential)
        {
            if (credential.Password == "BadPa55w0rd")
                return "Failed";

            return "Running";
        }

        public void SendCommand(string hostname, string command, Credential credential){}
        public void RebootComputer(string hostname, Credential credential, bool force, string message){}
        public void ManageService(string hostname, Credential credential, string command){}
        public void DeleteWsusClientID(string hostname, Credential credential){}
    }
}
