using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ServiceProcess;
using System.Management;

namespace CustomUpdateEngine
{
    public class UnregisterServiceAction:GenericAction
    {
        public UnregisterServiceAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ServiceName"))
                throw new ArgumentException("Unable to find token : ServiceName");
            ServiceName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string ServiceName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running UnregisterServiceAction.");

            try
            {
                ServiceInstaller ServiceInstallerObj = new ServiceInstaller();
                System.Configuration.Install.InstallContext Context = new System.Configuration.Install.InstallContext();
                ServiceInstallerObj.Context = Context;
                ServiceInstallerObj.ServiceName = ServiceName;
                ServiceInstallerObj.Uninstall(null);

                Logger.Write("Service successfully unregistered.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of UnregisterServiceAction.");
        }
    }
}
