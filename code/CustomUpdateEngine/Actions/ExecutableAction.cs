using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class ExecutableAction : GenericAction
    {
        public ExecutableAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("PathToTheFile"))
                throw new ArgumentException("Unable to find token : PathToTheFile");
            PathToTheFile = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Parameters"))
                throw new ArgumentException("Unable to find token : Parameters");
            Parameters = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("KillProcess"))
                throw new ArgumentException("Unable to find token : KillProcess");
            KillProcess = bool.Parse(reader.ReadElementContentAsString());
            if (!reader.ReadToFollowing("DelayBeforeKilling"))
                throw new ArgumentException("Unable to find token : DelayBeforeKilling");
            DelayBeforeKilling = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("StoreToVariable"))
                throw new ArgumentException("Unable to find token : StoreToVariable");
            StoreToVariable = bool.Parse(reader.ReadElementContentAsString());

            LogCompletion();
        }

        public string PathToTheFile { get; private set; }
        public string Parameters { get; private set; }
        public bool KillProcess { get; private set; }
        public int DelayBeforeKilling { get; private set; }
        public bool StoreToVariable { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running ExecutableAction. PathToTheFile = " + this.PathToTheFile + " and Parameters = " + this.Parameters + " KillProcess = " + this.KillProcess.ToString());

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            try
            {
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                processInfo.FileName = Tools.GetExpandedPath(PathToTheFile);
                if (!string.IsNullOrEmpty(Parameters))
                    processInfo.Arguments = Parameters;
                processInfo.ErrorDialog = false;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                process.StartInfo = processInfo;
                process.Start();

                if (this.KillProcess)
                {
                    if (!process.WaitForExit(this.DelayBeforeKilling * 60 * 1000))
                    {
                        Logger.Write("Process running for too long. Killing Process…");
                        process.Kill();
                        Logger.Write("Process killed.");
                    }
                    else
                    {
                        Logger.Write("Process has finnish running.");
                    }
                }
                else
                {
                    process.WaitForExit(int.MaxValue);
                    Logger.Write("Process has finnish running.");
                }

                Logger.Write("Exite code : " + process.ExitCode.ToString());
            }
            catch (Exception)
            {
                Logger.Write("The process is already stopped or doesn't have start. No ExitCode can be set.");
            }

            if (returnCode.ReturnMethod == ReturnCodeAction.ReturnCodeMethod.Variable && this.StoreToVariable)
            {
                returnCode.ReturnValue = process.ExitCode;
            }

            Logger.Write("End of ExecutableAction.");
        }
    }
}
