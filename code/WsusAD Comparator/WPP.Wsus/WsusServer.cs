using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPP.Wsus
{
    public class WsusServer
    {
        /// <summary>
        /// Create an instance of a remote Wsus Server, used to get a WsusServices proxy.
        /// </summary>
        /// <param name="serverName">Name of the Wsus Server.</param>
        /// <param name="serverPort">Port on which to connect to the Wsus Server.</param>
        /// <param name="useSSL">Define whether or not we need to use SSL.</param>
        public WsusServer(string serverName, int serverPort, bool useSSL)
        {
            this.ServerName = serverName;
            this.ServerPort = serverPort;
            this.UseSSL = useSSL;
        }

        /// <summary>
        /// Create an instance of a local Wsus Server, used to get a WsusServices proxy.
        /// </summary>
        public WsusServer()
        {
            this.ServerName = String.Empty;
            this.ServerPort = 0;
            this.UseSSL = false;
            this.IsLocal = true;
        }

        #region (properties)

        public string ServerName { get; private set; }

        public int ServerPort { get; private set; }

        public bool UseSSL { get; private set; }

        public bool IsLocal { get; private set; }

        #endregion (properties)

        #region (methods)

        /// <summary>
        /// Connect to the Wsus target.
        /// </summary>
        /// <param name="preferredCulture">The culture to use to get localized answers.</param>
        public IWsusServices Connect()
        {
            return new WsusServices(this);
        }

        #endregion (methods)
    }
}
