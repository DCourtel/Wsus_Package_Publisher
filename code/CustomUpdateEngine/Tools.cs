using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomUpdateEngine
{
    public static class Tools
    {
        internal static string GetExpandedPath(string PathToExpand)
        {
            Logger.Write("Path To expand is : " + PathToExpand);

            try
            {
                if (PathToExpand.ToLower().Contains("%programfiles(x86)%"))
                {
                    PathToExpand = PathToExpand.ToLower().Replace("%programfiles(x86)%", System.Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"));
                }

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"%\w+%", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Match match = regex.Match(PathToExpand);
                while (match.Success)
                {
                    PathToExpand = PathToExpand.Replace(match.Value, System.Environment.ExpandEnvironmentVariables(match.Value));
                    match = match.NextMatch();
                }

            }
            catch (Exception ex)
            {
                Logger.Write("Error with PathToExecutable : " + ex.Message);

                throw ex;
            }

            Logger.Write("Expanded Path is : " + PathToExpand);

            return PathToExpand;
        }
    }
}
