using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CustomUpdateEngine.Exceptions;

namespace CustomUpdateEngine
{
    class ScriptElement : GenericElement
    {
        private enum ScriptTypes
        {
            Undefined,
            Vbscript,
            Powershell
        }

        internal ScriptElement(string xmlFragment)
        {
            Logger.Write("Get ScriptElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ScriptType"))
                throw new Exception();
            ScriptType = (ScriptTypes)Enum.Parse(typeof(ScriptTypes), reader.ReadString(), true);
            if (!reader.ReadToFollowing("Filename"))
                throw new Exception();
            Filename = reader.ReadString();
            if (!reader.ReadToFollowing("Arguments"))
                throw new Exception();
            Arguments = reader.ReadString();
            if (!reader.ReadToFollowing("KillProcess"))
                throw new Exception();
            KillProcess = Convert.ToBoolean(reader.ReadString());
            if (!reader.ReadToFollowing("TimeBeforeKilling"))
                throw new Exception();
            TimeBeforeKilling = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("Variable"))
                throw new Exception();
            if (!reader.IsEmptyElement)
                Variable = new Guid(reader.ReadString());

            Logger.Write("End of Initializing of ScriptElement.");
        }

        private ScriptTypes ScriptType { get; set; }
        private string Filename { get; set; }
        private string Arguments { get; set; }
        private Guid Variable { get; set; }
        private bool KillProcess { get; set; }
        private int TimeBeforeKilling { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running ScriptElement.");

            switch (ScriptType)
            {
                case ScriptTypes.Vbscript:
                    RunScript(@"C:\Windows\system32\Cscript.exe", variables);
                    break;
                case ScriptTypes.Powershell:
                    RunScript(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe", variables);
                    break;
            }

            Logger.Write("End of ScriptElement.");
        }

        private void RunScript(string scriptEngine, List<VariableElement> variables)
        {
            try
            {
                SetExitCode(65535, variables);

                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                string currentPath = Environment.CurrentDirectory;
                processInfo.FileName = scriptEngine;
                processInfo.Arguments = "\"" + Filename + "\" " + Arguments;
                processInfo.WorkingDirectory = currentPath;
                processInfo.ErrorDialog = false;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                Logger.Write("Running : " + processInfo.FileName + " With arguments : " + processInfo.Arguments);

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                if (System.IO.File.Exists(scriptEngine))
                {
                    if (System.IO.File.Exists(Filename))
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
                            if (!process.WaitForExit(this.TimeBeforeKilling * 60 * 1000))
                            {
                                Logger.Write("Killing process.");
                                process.Kill();
                            }
                        }
                        else
                            process.WaitForExit(int.MaxValue);

                        Logger.Write("Exiting process with ExitCode = " + process.ExitCode.ToString());
                        SetExitCode(process.ExitCode, variables);
                    }
                    else
                        Logger.Write(Filename + ", not found !");
                }
                else
                    Logger.Write(scriptEngine + ", not found !");
            }
            catch (Exception ex)
            {
                Logger.Write("Error when launching the script. \r\n" + ex.Message);
            }
        }

        private void SetExitCode(int exitCode, List<VariableElement> variables)
        {
            if (Variable.CompareTo(new Guid()) != 0)
                for (int i = 0; i < variables.Count; i++)
                {
                    if (((VariableElement)variables[i]).VarID.CompareTo(Variable) == 0)
                    {
                        (variables[i] as VariableElement).IntValue = exitCode;
                        break;
                    }
                }
        }
    }
}
