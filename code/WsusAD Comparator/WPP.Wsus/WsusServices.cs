using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.Management;
using Microsoft.UpdateServices.Administration;
using System.Windows.Forms;

namespace WPP.Wsus
{
    public class WsusServices : IWsusServices
    {
        private const int _keepAliveInterval = 10000;           // Query remote Wsus Server every x milliseconds to keep the connection alive.

        private IUpdateServer _updateServer;
        private Timer _timer = new Timer();

        /// <summary>
        /// Gets an instance of this WsusServices and connect to the targeted Wsus Server.
        /// </summary>
        /// <param name="wsusTarget">The Wsus Server to target for queries.</param>
        /// <param name="preferredCulture">The culture we which to use to get localized answers.</param>
        internal WsusServices(WsusServer wsusTarget)
        {
            this.WsusTarget = wsusTarget;
            this.Connect();
        }

        ~WsusServices()
        {
            try
            {
                this._timer.Stop();
                this._timer = null;
            }
            catch (Exception) { }
        }

        #region (properties)

        /// <summary>
        /// Gets or Sets the instance of WsusServer to connect to.
        /// </summary>
        public WsusServer WsusTarget { get; private set; }



        #endregion (properties)

        #region (methods)

        /// <summary>
        /// Determine, whether or not, a computer is in Wsus.
        /// </summary>
        /// <param name="computerName">Name of the computer to search in Wsus.</param>
        /// <returns>true if the computer have been found in Wsas.</returns>
        public bool IsInWsus(string computerName)
        {
            return this.GetComputerTargetByName(computerName) != null;
        }

        /// <summary>
        /// Retrieve a computer by his name in the Wsus Server
        /// </summary>
        /// <param name="computerName">Name of the computer to retrieve.</param>
        /// <returns>An instance of IComputerTarget or null if the computer has not been found.</returns>
        private IComputerTarget GetComputerTargetByName(string computerName)
        {
            IComputerTarget computer = null;
            try
            {
                computer = this._updateServer.GetComputerTargetByName(computerName);
            }
            catch (Exception) { }

            return computer;
        }


        /// <summary>
        /// Connect to the Wsus Server and define the Preferred Culture.
        /// </summary>
        private void Connect()
        {
            if (this.WsusTarget.IsLocal)
            {
                this._updateServer = AdminProxy.GetUpdateServer();
            }
            else
            {
                this._updateServer = AdminProxy.GetUpdateServer(this.WsusTarget.ServerName, this.WsusTarget.UseSSL, this.WsusTarget.ServerPort);
                _timer.Interval = _keepAliveInterval;
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Start();
            }
        }

        /// <summary>
        /// Set the culture used by Wsus to return strings.
        /// </summary>
        /// <param name="culture">Culture to use.</param>
        /// <returns>true if the operation successed.</returns>
        public bool SetPreferredCulture(string culture)
        {
            try
            {
                if (this.IsCultureSupported(culture))
                {
                    this._updateServer.PreferredCulture = culture;
                    return true;
                }
            }
            catch (Exception) { }

            return false;
        }

        /// <summary>
        /// Determine, whether or not, a culture is supported by Wsus.
        /// </summary>
        /// <param name="culture">Culture to look for.</param>
        /// <returns>true if the culture is supported, false otherwise.</returns>
        private bool IsCultureSupported(string culture)
        {
            try
            {
                IUpdateServerConfiguration wsusConf = this._updateServer.GetConfiguration();
                System.Collections.Specialized.StringCollection supportedLanguages = wsusConf.SupportedUpdateLanguages;
                return supportedLanguages.Contains(culture);
            }
            catch (Exception) { }

            return false;
        }
        
        /// <summary>
        /// Return an object representing the 'All Computer' group.
        /// </summary>
        /// <returns>Return a IComputerTargetGroup which represent the 'All Computer' group, or null if Wsus Server is unreachable.</returns>
        internal IComputerTargetGroup GetAllComputerTargetGroup()
        {
            IComputerTargetGroup computerGroup = null;

            try
            {
                computerGroup = this._updateServer.GetComputerTargetGroup(ComputerTargetGroupId.AllComputers);
            }
            catch (Exception) { }

            return computerGroup;
        }

        /// <summary>
        /// This method is regularly call to keep alive the connection with the remote Wsus server. Not used when connecting to a local Wsus Server.
        /// </summary>
        private void _timer_Tick(object sender, EventArgs e)
        {
            this._timer.Stop();
            this.GetAllComputerTargetGroup();
            this._timer.Start();
        }

        #endregion (methods)
    }
}
