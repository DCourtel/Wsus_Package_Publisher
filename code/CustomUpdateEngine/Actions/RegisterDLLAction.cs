using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace CustomUpdateEngine
{
    public class RegisterDLLAction : GenericAction
    {
        public RegisterDLLAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FullPath"))
                throw new ArgumentException("Unable to find token : FullPath");
            FullPath = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string FullPath { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RegisterDLLAction. FullPath = " + this.FullPath);

            try
            {
                this.FullPath = Tools.GetExpandedPath(this.FullPath);
                if(!System.IO.File.Exists(this.FullPath ))
                {
                    Logger.Write("!!!! Warning, the file was not found on this system.");
                }
                Process proc = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo("RegSVR32.exe");
                startInfo.Arguments = "/s " + this.FullPath;
                proc.StartInfo = startInfo;
                proc.Start();
                if(!proc.WaitForExit(10000))
                {
                    Logger.Write("Killing process…");
                    proc.Kill();
                }
                else
                {
                    Logger.Write("Process stop by itself. Returned code : " + proc.ExitCode);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RegisterDLLAction.");
        }
    }
}
