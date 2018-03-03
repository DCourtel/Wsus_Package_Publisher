using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CustomUpdateEngine.Exceptions;

namespace CustomUpdateEngine
{
    internal class ExecutableElement : GenericElement
    {
        internal ExecutableElement(string xmlFragment)
        {
            Logger.Write("Get ExecutableElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("PathToExecutable"))
                throw new Exception();
            PathToExecutable = reader.ReadString();
            if (!reader.ReadToFollowing("Parameters"))
                throw new Exception();
            if (!reader.IsEmptyElement)
                Parameters = reader.ReadString();
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

            Logger.Write("Instance ExecutableElement successful.");

        }

        private string PathToExecutable { get; set; }
        private string Parameters { get; set; }
        private Guid Variable { get; set; }
        private bool KillProcess { get; set; }
        private int TimeBeforeKilling { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running ExecutableElement.");

            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.FileName = Utilities.GetExpandedPath(PathToExecutable);
            if (!string.IsNullOrEmpty(Parameters))
                processInfo.Arguments = Parameters;
            processInfo.ErrorDialog = false;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            if (!System.IO.File.Exists(processInfo.FileName))
            {
                Logger.Write(processInfo.FileName + " doesn't exists.");

                throw new FileNotFoundException("Unable to find : " + PathToExecutable + " (" + processInfo.FileName + ")");
            }

            Logger.Write("Running : " + processInfo.FileName + " With arguments : " + processInfo.Arguments);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            try
            {
                process.StartInfo = processInfo;
                    process.Start();
            }
            catch (Exception ex)
            {
                Logger.Write("Error running :  " + processInfo.FileName + ". " + ex.Message);

                throw new FileNotFoundException("Unable to find : " + PathToExecutable + " (" + processInfo.FileName + ")");
            }

            try
            {
                int id = process.Id;

                if (this.KillProcess)
                {
                    if (!process.WaitForExit(this.TimeBeforeKilling * 60 * 1000))
                    {
                        process.Kill();
                        Logger.Write("Process killed.");
                    }
                }
                else
                    process.WaitForExit(int.MaxValue);

                Logger.Write("Exiting process.");
                SetExitCode(process.ExitCode, variables);
            }
            catch (Exception)
            {
                Logger.Write("The process is already stopped or doesn't have start. No ExitCode can be set.");
            }

            Logger.Write("End of running ExecutableElement.");
        }
        
        private void SetExitCode(int exitcode, List<VariableElement> variables)
        {
            if (Variable.CompareTo(new Guid()) != 0)
                for (int i = 0; i < variables.Count; i++)
                {
                    if (((VariableElement)variables[i]).VarID.CompareTo(Variable) == 0)
                    {
                        (variables[i] as VariableElement).IntValue = exitcode;
                        break;
                    }
                }
        }
    }
}
