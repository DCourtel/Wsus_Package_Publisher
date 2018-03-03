using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal static class Languages
    {
        private static Dictionary<string, string> _languagesByName = new Dictionary<string, string>
        {   {"Arabic", "ar"},
            {"Chinese_HK_SAR", "zh-HK"},
            {"Chinese_simplified", "zh-CHS"},
            {"Chinese_traditional","zh-CHT"},
            {"Czech", "cs"},
            {"Danish", "da"}, 
            {"Dutch", "nl"},
            {"English", "en"},
            {"Finnish", "fi"}, 
            {"French", "fr"},    
            {"German", "de"},   
            {"Greek", "el"},   
            {"Hebrew", "he"},            
            {"Hungarian", "hu"},   
            {"Italian", "it"},              
            {"Japanese", "ja"},   
            {"Korean", "ko"}, 
            {"Norwegian", "no"},
            {"Polish", "pl"},
            {"Portugese", "pt"}, 
            {"Portugese_Brazil", "pt-br"}, 
            {"Russian", "ru"},
            {"Spanish", "es"}, 
            {"Swedish", "sv"}, 
            {"Turkish", "tr"}
        };

        private static Dictionary<string, int> _languagesByLCID = new Dictionary<string, int>
        {   {"ar",0},
            {"zh-HK", 1},
            {"zh-CHS", 2},
            {"zh-CHT", 3},
            {"cs", 4},
            {"da", 5},
            {"nl", 6},
            {"en", 7},
            {"fi", 8},
            {"fr", 9},   
            {"de", 10},  
            {"el", 11},  
            {"he", 12},           
            {"hu", 13},  
            {"it", 14},             
            {"ja", 15},  
            {"ko", 16},
            {"no", 17},
            {"pl", 18},
            {"pt", 19},
            {"pt-br", 20},
            {"ru", 21},
            {"es", 22},
            {"sv", 23},
            {"tr", 24}
        };

        private static Dictionary<string, int> _languagesByString = new Dictionary<string, int>
        {   {"Arabic", 1},
            {"Chinese_HK_SAR", 3076},
            {"Chinese_simplified", 4},
            {"Chinese_traditional",31748},
            {"Czech", 5},
            {"Danish", 6}, 
            {"Dutch", 19},
            {"English", 9},
            {"Finnish", 11}, 
            {"French", 12},    
            {"German", 7},   
            {"Greek", 8},   
            {"Hebrew", 13},            
            {"Hungarian", 14},   
            {"Italian", 16},              
            {"Japanese", 17},   
            {"Korean", 18}, 
            {"Norwegian", 20},
            {"Polish", 21},
            {"Portugese", 22}, 
            {"Portugese_Brazil", 1046}, 
            {"Russian", 25},
            {"Spanish", 10}, 
            {"Swedish", 29}, 
            {"Turkish" , 31}
        };

        private static Dictionary<int, string> _languaguesByInt = new Dictionary<int, string>
        {
            {1, "Arabic"},
            {3076, "Chinese_HK_SAR"},
            {4, "Chinese_simplified"},
            {31748, "Chinese_traditional"},
            {5, "Czech"},
            {6, "Danish"}, 
            {19, "Dutch"},
            {9, "English"},
            {11, "Finnish"}, 
            {12, "French"},    
            {7, "German"},   
            {8, "Greek"},   
            {13, "Hebrew"},            
            {14, "Hungarian"},   
            {16, "Italian"},              
            {17, "Japanese"},   
            {18, "Korean"}, 
            {20, "Norwegian"},
            {21, "Polish"},
            {22, "Portugese"}, 
            {1046, "Portugese_Brazil"}, 
            {25, "Russian"},
            {10, "Spanish"}, 
            {29, "Swedish"}, 
            {31, "Turkish"}
        };

        #region {Methods - Méthodes}

        /// <summary>
        /// Get the Language Code as definied in ISO 639 from the full language name.
        /// </summary>
        /// <param name="language">Full language name.</param>
        /// <returns>Language code as definied in ISO 639.</returns>
        internal static string GetLanguageCode(string language)
        {
            if (_languagesByName.ContainsKey(language))
                return _languagesByName[language];
            else
                return string.Empty;
        }

        /// <summary>
        /// Get the index of the language from his language code as definied in ISO 639.
        /// </summary>
        /// <param name="languageCode">Language code as definied in ISO 639.</param>
        /// <returns>Index of the language code.</returns>
        internal static int GetLanguageIndex(string languageCode)
        {
            if(_languagesByLCID.ContainsKey(languageCode))
                return _languagesByLCID[languageCode];
            else
                return -1;
        }

        /// <summary>
        /// Get the MSI Language Code from the Language Name.
        /// </summary>
        /// <param name="language">Name of the language.</param>
        /// <returns>MSI Language Code.</returns>
        internal static int GetLanguageMSICode(string language)
        {
            if (_languagesByString.ContainsKey(language))
                return _languagesByString[language];
            else
                return -1;
        }

        /// <summary>
        /// Get the full language name from a MSI Language Code.
        /// </summary>
        /// <param name="msiLanguageCode">MSI Language Code.</param>
        /// <returns>Full Language Name.</returns>
        internal static string GetLanguageName(int msiLanguageCode)
        {
            if (_languaguesByInt.ContainsKey(msiLanguageCode))
                return _languaguesByInt[msiLanguageCode];
            else
                return string.Empty;
        }

        #endregion {Methods - Méthodes}

        #region {Properties - Propriétés}

        internal static Dictionary<string, string> AllLanguagues { get { return _languagesByName; } }

        #endregion {Properties - Propriétés}
    }
}
