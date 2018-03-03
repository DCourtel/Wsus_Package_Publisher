using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    class KillProcessElement:GenericElement
    {

        internal KillProcessElement(string xmlFragment)
        {
            Logger.Write("Initializing KillProcess with : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ProcessName"))
                throw new Exception();
            ProcessName = reader.ReadString();

            Logger.Write("End of Initializing of KillProcess.");
        }
       
        private string ProcessName { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running KillProcess.");
            Logger.Write("Will try to kill : " + ProcessName);

            try
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();

                foreach (System.Diagnostics.Process process in processes)
                {
                    if (process.ProcessName.ToLower() == ProcessName.ToLower())
                    {
                        Logger.Write("Killing " + process.ProcessName + " with PID : " + process.Id.ToString());
                        process.Kill();
                    }
                }
                Logger.Write("End of killing session.");
            }
            catch (Exception ex) { Logger.Write("Failed to kill the process.\r\n" + ex.Message); }

        }
    }
}
