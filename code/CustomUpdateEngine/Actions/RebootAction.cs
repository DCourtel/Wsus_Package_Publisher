using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace CustomUpdateEngine
{
    public class RebootAction : GenericAction
    {
        public RebootAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);
            
            LogCompletion();
        }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RebootAction.");

            try
            {
                Process proc = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo("Shutdown.exe");

                startInfo.Arguments = "/r /c \"Reboot initiated by CustomUpdateEngine\" /f /t 5";
                proc.StartInfo = startInfo;
                proc.Start();
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RebootAction.");
        }
    }
}
