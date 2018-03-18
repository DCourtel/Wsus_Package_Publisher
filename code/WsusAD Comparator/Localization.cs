using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WsusADComparator
{
    public class Localization
    {
        private static Localization _instance = null;
        private System.Resources.ResourceManager _resMan = null;

        private Localization()
        {
            this._resMan = new System.Resources.ResourceManager("WsusADComparator.Localization.Resources", typeof(FrmWsusADComparator).Assembly);
        }

        internal static Localization GetInstance()
        {
            if (_instance == null)
                _instance = new Localization();
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

    }
}
