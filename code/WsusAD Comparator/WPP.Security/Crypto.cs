using System;
using System.Security;

namespace WPP.Security
{
    public static class Crypto
    {
        /// <summary>
        /// Convert a regular string to a ReadOnly SecureString
        /// </summary>
        /// <param name="unsecureString">A regular string to crypte.</param>
        /// <returns>A ReadOnly crypted string.</returns>
        public static SecureString GetSecureString(string unsecureString)
        {
            SecureString secureStr = new SecureString();
            foreach (char character in unsecureString)
            {
                secureStr.AppendChar(character);
            }
            secureStr.MakeReadOnly();

            return secureStr;
        }

        /// <summary>
        /// Convert a SecureString to a regular string
        /// </summary>
        /// <param name="secureString">An encrypted string.</param>
        /// <returns>A regular unencrypted string.</returns>
        public static string GetUnsecureString(SecureString secureString)
        {
            string returnValue = String.Empty;

            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
    }
}
