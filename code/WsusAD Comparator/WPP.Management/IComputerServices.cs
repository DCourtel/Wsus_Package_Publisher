using WPP.Security;

namespace WPP.Management
{
    public interface IComputerServices
    {
        /// <summary>
        /// Send a SNMP Echo request to a remote computer.
        /// </summary>
        /// <param name="addressToPing">IP address of the computer to PING.</param>
        /// <param name="timeout">Milliseconds to wait after sending request before fail.</param>
        /// <returns>Return the response to the Ping request.</returns>
        System.Net.NetworkInformation.IPStatus Ping(System.Net.IPAddress addressToPing, int timeout);
        System.Net.IPHostEntry GetHostEntry(string hostname);
        string GetRemoteFileContent(string uncPath, string filename, Credential credential);
        string GetWsusClientID(string hostname, Credential credential);
        string GetWindowsUpdateServiceStatus(string hostname, Credential credential);
        void SendCommand(string hostname, string command, Credential credential);
        void RebootComputer(string hostname, Credential credential, bool force, string message);
        void ManageService(string hostname, Credential credential, string command);
        void DeleteWsusClientID(string hostname, Credential credential);
    }
}
