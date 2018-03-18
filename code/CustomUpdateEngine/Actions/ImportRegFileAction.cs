using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class ImportRegFileAction:GenericAction
    {
        public ImportRegFileAction(string xmlFragment)
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
            Logger.Write("Running ImportRegFileAction. FullPath = " + this.FullPath);

            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            FullPath = Tools.GetExpandedPath(FullPath);
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.FileName = "cmd.exe";
            processInfo.Arguments = "/C reg import \"" + this.FullPath + "\"";
            processInfo.ErrorDialog = false;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            process.StartInfo = processInfo;
            process.Start();

            if (!process.WaitForExit(10000))
            {
                Logger.Write("Process running for too long. Killing Process…");
                process.Kill();
                Logger.Write("Process killed.");
            }
            else
            {
                Logger.Write("Process has finnish running.");
            }
            Logger.Write("Exite code : " + process.ExitCode.ToString());

            Logger.Write("End of ImportRegFileAction.");
        }
    }
}
