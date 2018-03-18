using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    public class InstallMsiAction : GenericAction
    {
        public InstallMsiAction(string xmlFragment)
        {
            Logger.Write("Get InstallMsiAction from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("MsiName"))
                throw new ArgumentException("Unable to find token : MsiName");
            MsiName = reader.ReadString();
            if (!reader.ReadToFollowing("Parameters"))
                throw new ArgumentException("Unable to find token : Parameters");
            Parameters = reader.ReadString();
            if (!reader.ReadToFollowing("IsLogRequested"))
                throw new ArgumentException("Unable to find token : IsLogRequested");
            IsLogRequested = Convert.ToBoolean(reader.ReadString());
            if (!reader.ReadToFollowing("LogPath"))
                throw new ArgumentException("Unable to find token : LogPath");
            LogPath = reader.ReadString();
            if (!reader.ReadToFollowing("UiLevel"))
                throw new ArgumentException("Unable to find token : UiLevel");
            UiLevel = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("RestartBehavior"))
                throw new ArgumentException("Unable to find token : RestartBehavior");
            RestartBehavior = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("KillProcess"))
                throw new ArgumentException("Unable to find token : KillProcess");
            KillProcess = Convert.ToBoolean(reader.ReadString());
            if (!reader.ReadToFollowing("KillAfter"))
                throw new ArgumentException("Unable to find token : KillAfter");
            KillAfter = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("StoreToVariable"))
                throw new ArgumentException("Unable to find token : StoreToVariable");
            StoreToVariable = Convert.ToBoolean(reader.ReadString());

            Logger.Write("End of Initializing of InstallMsiAction.");
        }

        public string MsiName { get; set; }
        public string Parameters { get; set; }
        public bool IsLogRequested { get; set; }
        public string LogPath { get; set; }
        public int UiLevel { get; set; }
        public int RestartBehavior { get; set; }
        public bool KillProcess { get; set; }
        public int KillAfter { get; set; }
        public bool StoreToVariable { get; set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running InstallMsiAction. MsiName= " + this.MsiName + " Parameters= " + this.Parameters);

            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.Arguments = "/package \"" + this.MsiName + "\" " + GetUiLevel(this.UiLevel) + " " + GetRestartBehavior(RestartBehavior) + (IsLogRequested ? " /log \"" + this.LogPath + "\"" : String.Empty);
                proc.StartInfo.FileName = Tools.GetExpandedPath(@"%windir%\system32\msiexec.exe");

                proc.Start();

                try
                {
                    if (this.KillProcess)
                    {
                        if (!proc.WaitForExit(this.KillAfter * 60 * 1000))
                        {
                            proc.Kill();
                            Logger.Write("Process killed.");
                        }
                    }
                    else
                        proc.WaitForExit(int.MaxValue);

                    Logger.Write("Exiting process. With Exite code : " + proc.ExitCode.ToString());
                }
                catch (Exception)
                {
                    Logger.Write("The process is already stopped or doesn't have start.");
                }

                switch (proc.ExitCode)
                {
                    case 0:
                        Logger.Write("Successfully installed " + this.MsiName);
                        break;
                    case 1641:
                        Logger.Write("Successfully installed " + this.MsiName + " (A restart have been initiated)");
                        break;
                    case 3010:
                        Logger.Write("Successfully installed " + this.MsiName + " (A restart is required)");
                        break;
                    default:
                        Logger.Write("An error occurs while installing " + this.MsiName + " (MsiError : " + proc.ExitCode + ")");
                        break;
                }

            if (returnCode.ReturnMethod == ReturnCodeAction.ReturnCodeMethod.Variable && this.StoreToVariable)
            {
                returnCode.ReturnValue = proc.ExitCode;
            }
            }
            catch (Exception ex)
            {
                Logger.Write("An error occurs while preparing installation : " + ex.Message);
                throw;
            }

            Logger.Write("End of InstallMsiAction.");
        }

        private string GetUiLevel(int uiLevel)
        {
            switch (uiLevel)
            {
                case 0:
                    return "/qn";
                case 1:
                    return "/qb";
                case 2:
                    return "/qr";
                case 3:
                    return "/qf";
            }
            return "/qn";
        }

        private string GetRestartBehavior(int restart)
        {
            switch (restart)
            {
                case 0:
                    return "/norestart";
                case 1:
                    return "/promptrestart";
                case 2:
                    return "/forcerestart";
            }
            return "/norestart";
        }
    }
}
