using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace CustomUpdateEngine
{
    public class KillProcessAction:GenericAction
    {
        public KillProcessAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ProcessName"))
                throw new ArgumentException("Unable to find token : ProcessName");
            ProcessName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string ProcessName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running KillProcessAction. Hive = " + this.ProcessName);

            try
            {
                Process[] processes = Process.GetProcessesByName(this.ProcessName);
                
                foreach (Process process in processes)
                {
                    try
                    {
                        process.Kill();
                        Logger.Write("Process killed");
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("Unable to kill process : " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of KillProcessAction.");
        }
    }
}
