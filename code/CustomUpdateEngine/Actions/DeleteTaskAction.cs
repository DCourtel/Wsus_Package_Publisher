using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class DeleteTaskAction : GenericAction
    {
        public DeleteTaskAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("TaskName"))
                throw new ArgumentException("Unable to find token : TaskName");
            TaskName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string TaskName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            // schtasks /Delete [/S <system> [/U <username> [/P [<password>]]]] /TN <taskname> [/F]

            Logger.Write("Running DeleteTaskAction. TaskName = " + this.TaskName);
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            try
            {
                System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
                processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                processInfo.FileName = @"C:\Windows\System32\schtasks.exe";
                processInfo.Arguments = "/Delete /TN \"" + this.TaskName + "\" /F";
                processInfo.ErrorDialog = false;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;

                process.StartInfo = processInfo;
                process.Start();

                if (!process.WaitForExit(3000))
                {
                    process.Kill();
                }
                Logger.Write("Successfuly delete task : " + this.TaskName);
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of DeleteTaskAction.");
        }
    }
}
