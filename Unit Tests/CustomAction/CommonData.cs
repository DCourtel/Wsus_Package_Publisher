using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unit_Tests_CustomAction
{
    internal class CommonData
    {
        // %HomePath%
        // %LocalAppData%
        // %UserProfile%
        // %AppData%
        // %UserName%

        internal static string[] userProfileRelatedFolders = new string[] { @"C:\Users\Courtel\Downloads", 
            "%HomePath%", 
            @"%HomePath%\Downloads", 
            "%LocalAppData%", 
            @"%LocalAppData%\", 
            @"%LocalAppData%\Microsoft\Outlook",
            "%UserProfile%",
            @"%UserProfile%\Pictures",
            "%AppData%",
            @"%AppData%\Microsoft\Outlook",
            @"C:\Users\c:\Users\%UserName%\AppData\LocalLow\Microsoft"
        };

        internal static string[] otherFolders = new string[] { @"C:\Temp", @"C:\Windows", @"C:\Windows\System32", @"C:\Program Files (x86)", @"C:\Program Files (x86)\Microsoft", @"C:\ProgramData\Microsoft", @"C:\Program Files\Microsoft Office" };
    }
}
