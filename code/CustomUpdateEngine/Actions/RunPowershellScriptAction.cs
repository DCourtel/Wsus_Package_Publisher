using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class RunPowershellScriptAction : GenericAction
    {
        public RunPowershellScriptAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FullPath"))
                throw new ArgumentException("Unable to find token : FullPath");
            FullPath = reader.ReadElementContentAsString();
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

        public string FullPath { get; private set; }
        public string Parameters { get; private set; }
        public bool KillProcess { get; private set; }
        public int DelayBeforeKilling { get; private set; }
        public bool StoreToVariable { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RunPowershellScriptAction. FullPath= " + this.FullPath + " Parameters= " + this.Parameters + " KillProcess= " + this.KillProcess +
                " DelayBeforeKilling = " + this.DelayBeforeKilling + "StoreToVariable= " + this.StoreToVariable);

            try
            {
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                string currentPath = Environment.CurrentDirectory;
                processInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
                processInfo.Arguments = "-ExecutionPolicy Bypass -file \"" + Tools.GetExpandedPath(FullPath) + "\" " + Parameters;
                processInfo.WorkingDirectory = currentPath;
                processInfo.ErrorDialog = false;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                Logger.Write("Running : " + processInfo.FileName + " With arguments : " + processInfo.Arguments);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (System.IO.File.Exists(processInfo.FileName))
                {
                    if (System.IO.File.Exists(Tools.GetExpandedPath(FullPath)))
                    {
                        try
                        {
                            process.StartInfo = processInfo;
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            Logger.Write("Error running :  " + processInfo.FileName + ". \r\n" + ex.Message);
                        }

                        if (this.KillProcess)
                        {
                            if (!process.WaitForExit(this.DelayBeforeKilling * 60 * 1000))
                            {
                                Logger.Write("Killing process.");
                                process.Kill();
                            }
                        }
                        else
                            process.WaitForExit(int.MaxValue);

                        Logger.Write("Exiting process with ExitCode = " + process.ExitCode.ToString());

                        if (returnCode.ReturnMethod == ReturnCodeAction.ReturnCodeMethod.Variable && this.StoreToVariable)
                        {
                            returnCode.ReturnValue = process.ExitCode;
                        }
                    }
                    else
                        Logger.Write(FullPath + ", not found !");
                }
                else
                    Logger.Write(processInfo.FileName + ", not found !");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RunPowershellScript.");
        }
    }
}
