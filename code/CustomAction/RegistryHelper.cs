using System;
using System.Collections.Generic;
using System.Text;

namespace CustomActions
{
    public class RegistryHelper
    {
        public enum RegistryHive
        {
            HKey_Local_Machine,
            HKey_Current_User,
            Undefined
        }

        public enum ValueType
        {
            REG_SZ,
            REG_BINARY,
            REG_DWORD,
            REG_QWORD,
            REG_MULTI_SZ,
            REG_EXPAND_SZ
        }

        public struct RegKey
        {
            internal string RegKeyName
            {
                get;
                set;
            }

            internal RegistryHive RegHive
            {
                get;
                set;
            }
        }

        #region (Internal methods)

        /// <summary>
        /// Remove leading backslash and reference to HKLM, HKCU and Wow6432Node.
        /// </summary>
        /// <param name="dirtyRegKey">A string that contains, eventually, leading backslash and reference to HKLM and HKCU.</param>
        /// <param name="currentHive">The current Registry Hive from the <see cref="dirtyRegKey"/> make reference to./></param>
        /// <returns>Returns a <see cref="RegKey"/> that contains the clean Registry Key name and the Registry hive.</returns>
        internal static RegKey GetCleanRegKey(string dirtyRegKey, RegistryHive currentHive, bool useReg32)
        {
            RegKey cleanRegKey = new RegKey();
            cleanRegKey.RegKeyName = dirtyRegKey;
            cleanRegKey.RegHive = currentHive;

            dirtyRegKey = RemoveLeadingBackSlash(dirtyRegKey);
            cleanRegKey = RemoveRegistryHiveReference(dirtyRegKey, currentHive);
            cleanRegKey.RegKeyName = RemoveLeadingBackSlash(cleanRegKey.RegKeyName);
            if(useReg32)
            {
                cleanRegKey.RegKeyName = RemoveWowReference(cleanRegKey.RegKeyName);
            }

            return cleanRegKey;
        }

        #endregion (Internal methods)

        #region (Private methods)

        /// <summary>
        /// Remove all instance of '\' character at the start of the string. If the string doesn't contains '\' at the begin, or the dirtyString param is null or empty, the same param is returns.
        /// </summary>
        /// <param name="dirtyString">A string that, eventually, contains '\' characters at the begining.</param>
        /// <returns>The same string without '\' at the begining.</returns>
        public static string RemoveLeadingBackSlash(string dirtyString)
        {
            if (!String.IsNullOrEmpty(dirtyString) && dirtyString.StartsWith(@"\"))
                dirtyString = dirtyString.TrimStart(new char[] { '\\' });

            return dirtyString;
        }

        /// <summary>
        /// Remove reference to HKLM or HKCU at the begining of the <see cref="dirtyRegKey"/>
        /// </summary>
        /// <param name="dirtyRegKey">A string that, eventually, contains a reference to HKLM or HKCU.</param>
        /// <param name="currentHive">The current Registry Hive.</param>
        /// <returns>Returns a <see cref="RegKey"/> that contains the clean RegistryKey name with the referenced Hive.</returns>
        public static RegKey RemoveRegistryHiveReference(string dirtyRegKey, RegistryHive currentHive)
        {
            const string HKLM = "HKEY_LOCAL_MACHINE";
            const string HKCU = "HKEY_CURRENT_USER";
            RegKey cleanRegKey = new RegKey();
            cleanRegKey.RegHive = currentHive;
            cleanRegKey.RegKeyName = dirtyRegKey;

            string tempTxt = dirtyRegKey.ToUpper();
            if (tempTxt.StartsWith(HKLM))
            {
                cleanRegKey.RegKeyName = dirtyRegKey.Substring(HKLM.Length);
                cleanRegKey.RegHive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            }

            if (tempTxt.StartsWith(HKCU))
            {
                cleanRegKey.RegKeyName = dirtyRegKey.Substring(HKCU.Length);
                cleanRegKey.RegHive = RegistryHelper.RegistryHive.HKey_Current_User;
            }

            return cleanRegKey;
        }

        /// <summary>
        /// Remove reference to Wow6432Node if found.
        /// </summary>
        /// <param name="dirtyRegKey">A string that, eventually, start by «SOFTWARE\Wow6432Node».</param>
        /// <returns>The same string without «\Wow6432Node»</returns>
        public static string RemoveWowReference(string dirtyRegKey)
        {
            string cleanRegKey = dirtyRegKey;

            if (dirtyRegKey.StartsWith(@"SOFTWARE\Wow6432Node", StringComparison.CurrentCultureIgnoreCase))
            {
                cleanRegKey = dirtyRegKey.Remove(8, 12);
            }

            return cleanRegKey;
        }

        #endregion (Private methods)
    }
}
