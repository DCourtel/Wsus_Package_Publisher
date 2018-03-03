using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Management;

namespace CustomUpdateEngine
{
    class PowerManagementElement:GenericElement
    {
        private enum PowerActions
        {
            Unknown,
            Shutdown,
            Reboot
        }

        internal PowerManagementElement(string xmlFragment)
        {
            Logger.Write("Initializing PowerManagementElement with : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("PowerAction"))
                throw new Exception();
            PoawerAction = (PowerActions)Enum.Parse(typeof(PowerActions), reader.ReadString(), true);

            Logger.Write("End of Initializing of PowerManagementElement.");
        }

        private PowerActions PoawerAction { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running PowerManagementElement.");

            switch (PoawerAction)
            {
                case PowerActions.Reboot:
                    Reboot();
                    break;
                case PowerActions.Shutdown:
                    Shutdown();
                    break;

            }
            Logger.Write("End of PowerManagementElement");
        }

        private void Reboot()
        {
            string command = @"Shutdown /r /t 10 /d p:2:17";
            DoAction(command);
        }

        private void Shutdown()
        {
            string command = @"Shutdown /s /t 10 /d p:2:17";
            DoAction(command);
        }

        private void DoAction(string command)
        {
            Logger.Write("DoAction : " + command);
            try
            {
                ManagementScope mgmtScope = new ManagementScope(@"\\.\ROOT\CIMV2");
                mgmtScope.Connect();
                ObjectGetOptions objectGetOptions = new ObjectGetOptions();
                ManagementPath mgmtPath = new ManagementPath("Win32_Process");
                ManagementClass processClass = new ManagementClass(mgmtScope, mgmtPath, objectGetOptions);
                ManagementBaseObject inParams = processClass.GetMethodParameters("Create");

                inParams["CommandLine"] = command;
                processClass.InvokeMethod("Create", inParams, null);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            Logger.Write("Action launched.");
        }
    }
}
