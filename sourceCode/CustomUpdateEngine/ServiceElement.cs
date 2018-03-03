using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CustomUpdateEngine.Exceptions;
using System.ServiceProcess;
using System.Management;

namespace CustomUpdateEngine
{
    internal class ServiceElement : GenericElement
    {
        private enum Actions
        {
            ChangeStartingMode,
            Start,
            Stop,
            Register,
            Unregister,
            Undefined
        }

        private enum StartupModes
        {
            Automatic,
            Disabled,
            Manual,
            Undefined
        }

        private enum StartingAccounts
        {
            LocalService,
            LocalSystem,
            NetworkService,
            User,
            Undefined
        }

        internal ServiceElement(string xmlFragment)
        {
            Logger.Write("Get ServiceElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ServiceAction"))
                throw new Exception();
            ServiceAction = (Actions)Enum.Parse(typeof(Actions), reader.ReadString(), true);
            if (!reader.ReadToFollowing("ServiceName"))
                throw new Exception();
            ServiceName = reader.ReadString();
            if (!reader.ReadToFollowing("PathToEXE"))
                throw new Exception();
            PathToEXE = reader.ReadString();
            if (!reader.ReadToFollowing("StartupMode"))
                throw new Exception();
            StartupMode = (StartupModes)Enum.Parse(typeof(StartupModes), reader.ReadString(), true);
            if (!reader.ReadToFollowing("StartingAccount"))
                throw new Exception();
            StartingAccount = (StartingAccounts)Enum.Parse(typeof(StartingAccounts), reader.ReadString(), true);
            if (!reader.ReadToFollowing("Login"))
                throw new Exception();
            Login = reader.ReadString();
            if (!reader.ReadToFollowing("Password"))
                throw new Exception();
            Password = reader.ReadString();

            Logger.Write("End of Initializing of ServiceElement.");
        }
        
        private Actions ServiceAction { get; set; }
        private string ServiceName { get; set; }
        private string PathToEXE { get; set; }
        private StartupModes StartupMode { get; set; }
        private StartingAccounts StartingAccount { get; set; }
        private string Login { get; set; }
        private string Password { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running ServiceElement for action : " + ServiceAction.ToString());

            switch (ServiceAction)
            {
                case Actions.ChangeStartingMode:
                    ChangeStartingMode();
                    break;
                case Actions.Start:
                    StartService();
                    break;
                case Actions.Stop:
                    StopService();
                    break;
                case Actions.Register:
                    RegisterService();
                    break;
                case Actions.Unregister:
                    UnregisterService();
                    break;
            }
            Logger.Write("End of ServiceElement.");
        }

        private void ChangeStartingMode()
        {
            Logger.Write("Will try to change the startup mode of " + ServiceName);
            try
            {
                uint success = 1;
                string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", ServiceName);

                ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

                if (query != null)
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        ManagementBaseObject inParams = service.GetMethodParameters("ChangeStartMode");
                        inParams["startmode"] = StartupMode;

                        ManagementBaseObject outParams = service.InvokeMethod("ChangeStartMode", inParams, null);
                        success = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                        if (success == 0)
                            Logger.Write("Successfully change the startup mode of " + ServiceName);
                        else
                            Logger.Write("Failed to change the startup mode of " + ServiceName);
                    }
                }
                else
                    Logger.Write("The service " + ServiceName + " was not found.");
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to change the startup mode of " + ServiceName + "\r\n" + ex.Message);
            }
        }

        private void StartService()
        {
            Logger.Write("Will try to start : " + ServiceName);
            try
            {
                ServiceController srvCtrl = new ServiceController(ServiceName);
                srvCtrl.Start();
                Logger.Write("Successfully start " + ServiceName);
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to start " + ServiceName + "\r\n" + ex.Message);
            }
        }

        private void StopService()
        {
            Logger.Write("Will try to stop : " + ServiceName);
            try
            {
                ServiceController srvCtrl = new ServiceController(ServiceName);
                srvCtrl.Stop();
                Logger.Write("Successfully stop " + ServiceName);
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to stop " + ServiceName + "\r\n" + ex.Message);
            }

        }

        private void RegisterService()
        {
            Logger.Write("Will try to Register : " + ServiceName);
            try
            {
                ServiceProcessInstaller ProcesServiceInstaller = new ServiceProcessInstaller();
                switch (StartingAccount)
                {
                    case StartingAccounts.LocalService:
                        ProcesServiceInstaller.Account = ServiceAccount.LocalService;
                        break;
                    case StartingAccounts.LocalSystem:
                        ProcesServiceInstaller.Account = ServiceAccount.LocalSystem;
                        break;
                    case StartingAccounts.NetworkService:
                        ProcesServiceInstaller.Account = ServiceAccount.NetworkService;
                        break;
                    case StartingAccounts.User:
                        ProcesServiceInstaller.Account = ServiceAccount.User;
                        ProcesServiceInstaller.Username = Login;
                        ProcesServiceInstaller.Password = Password;
                        break;
                }

                ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                System.Configuration.Install.InstallContext Context = new System.Configuration.Install.InstallContext();
                String path = PathToEXE;
                String[] cmdline = { path };

                Context = new System.Configuration.Install.InstallContext("", cmdline);
                ServiceInstallerObj.Context = Context;
                ServiceInstallerObj.DisplayName = ServiceName;
                ServiceInstallerObj.ServiceName = ServiceName;
                switch (StartupMode)
                {
                    case StartupModes.Automatic:
                        ServiceInstallerObj.StartType = ServiceStartMode.Automatic;
                        break;
                    case StartupModes.Disabled:
                        ServiceInstallerObj.StartType = ServiceStartMode.Disabled;
                        break;
                    case StartupModes.Manual:
                        ServiceInstallerObj.StartType = ServiceStartMode.Manual;
                        break;
                }

                ServiceInstallerObj.Parent = ProcesServiceInstaller;

                System.Collections.Specialized.ListDictionary state = new System.Collections.Specialized.ListDictionary();
                ServiceInstallerObj.Install(state);

                Logger.Write("Successfully Register " + ServiceName);
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to Register " + ServiceName + "\r\n" + ex.Message);
            }
        }

        private void UnregisterService()
        {
            Logger.Write("Will try to Unregister : " + ServiceName);
            try
            {
                ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                System.Configuration.Install.InstallContext Context = new System.Configuration.Install.InstallContext();
                ServiceInstallerObj.Context = Context;
                ServiceInstallerObj.ServiceName = ServiceName;
                ServiceInstallerObj.Uninstall(null);

                Logger.Write("Successfully Unregister " + ServiceName);
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to Unregister " + ServiceName + "\r\n" + ex.Message);
            }
        }
    }
}
