using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomUpdateCreator
{
    internal class Localizator
    {
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("CustomUpdateCreator.Localization.CustomUpdateCreatorStrings", typeof(frmCustomUpdateCreator).Assembly);
        private static Localizator _instance = new Localizator();

        private Localizator() { }

        /// <summary>
        /// Gets an instance of this class
        /// </summary>
        /// <returns>Returns an instance of this class</returns>
        internal static Localizator Getinstance()
        {
            return _instance;
        }
        
        /// <summary>
        /// Returns a localized string depending of the current culture.
        /// Never throws exception.
        /// </summary>
        /// <param name="unlocalizedString">String to localize.</param>
        /// <returns>The localized string.</returns>
        internal string GetLocalizedString(string unlocalizedString)
        {
            string result = string.Empty;

            try
            {
                result = resMan.GetString(unlocalizedString);
                if (!string.IsNullOrEmpty(result))
                    return result;
            }
            catch (Exception) { }

            return "Missing_Localized_String_For [" + ((unlocalizedString != null) ? unlocalizedString : "null") + "]";
        }
    }
}
