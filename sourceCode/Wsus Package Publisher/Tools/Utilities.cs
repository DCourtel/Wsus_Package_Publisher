using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher.Tools
{
    internal class Utilities
    {
        private static Utilities _instance = null;
        private System.Resources.ResourceManager _resMan = null;

        private Utilities()
        {            
            this._resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmWsusPackagePublisher).Assembly);
        }

        internal static Utilities GetInstance()
        {
            if (_instance == null)
                _instance = new Utilities();
            return _instance;
        }

        /// <summary>
        /// Returns a localized string depending of the current culture.
        /// </summary>
        /// <param name="unlocalizedString">string to localized.</param>
        /// <returns>the localized string.</returns>
        internal string GetLocalizedString(string unlocalizedString)
        {
            string result = string.Empty;

            try
            {
                result = this._resMan.GetString(unlocalizedString);
                if (!string.IsNullOrEmpty(result))
                    return result;
            }
            catch (Exception) { }

            return "Missing_Localized_String_For(" + ((unlocalizedString != null) ? unlocalizedString : "null") + ")";
        }

        /// <summary>
        /// Allows to obtain a proxy object to download/upload from/to Internet, based on the Proxy Policy set into settings.
        /// </summary>
        /// <returns>A proxy object based on the Proxy Policy set into settings.</returns>
        static internal System.Net.IWebProxy GetHTTPProxy()
        {
            Logger.EnteringMethod();
            Logger.Write(Properties.Settings.Default.HTTPProxyPolicy);

            switch (Properties.Settings.Default.HTTPProxyPolicy)
            {                  
                case "NoProxy":
                    return null;
                case "CustomSettings":
                    System.Net.WebProxy customProxy = new System.Net.WebProxy(Properties.Settings.Default.HTTPProxyServerName, Properties.Settings.Default.HTTPProxyPort);
                    customProxy.UseDefaultCredentials = false;
                    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(Properties.Settings.Default.HTTPProxyLogin, Properties.Settings.Default.HTTPProxyPassword);
                    customProxy.Credentials = cred;
                    return customProxy;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Allows to obtain a proxy object to download/upload from/to Internet, based on the Proxy Policy set into settings.
        /// </summary>
        /// <returns>A proxy object based on the Proxy Policy set into settings.</returns>
        static internal System.Net.IWebProxy GetFTPProxy()
        {
            Logger.EnteringMethod();
            Logger.Write(Properties.Settings.Default.FTPProxyPolicy);

            switch (Properties.Settings.Default.FTPProxyPolicy)
            {
                case "NoProxy":
                    return null;
                case "SameAsAbove":
                    return GetHTTPProxy();
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// Expand the %temp% Environnement Variable and add a random folder name at the end.
        /// If %temp% can't be resolved, then 'C:\temp\' will be used instead.
        /// Create the directory if it doesn't exists.
        /// </summary>
        /// <returns>A path to a random folder with ending '\'.</returns>
        static internal string GetTempFolder()
        {
            Logger.EnteringMethod();

            string result = @"C:\temp\";

            try
            {
                if (Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User).Contains("TempWPP"))
                    result = Environment.GetEnvironmentVariable("TempWPP");
                else
                    result = System.IO.Path.GetTempPath();

                result = System.IO.Path.Combine(result, "WPP", System.IO.Path.GetRandomFileName()) + "\\";
            }
            catch (Exception) { }

            if (!System.IO.Directory.Exists(result))
                System.IO.Directory.CreateDirectory(result);
            Logger.Write("Will return : " + result);
            return result;
        }

        /// <summary>
        /// Delete a folder with subfolders and files.
        /// </summary>
        /// <param name="folderToDelete">Full path to the folder to delete.</param>
        static internal void DeleteFolder(string folderToDelete)
        {
            try
            {
                System.IO.Directory.Delete(folderToDelete, true);
            }
            catch (Exception) { }
        }
    }
}
