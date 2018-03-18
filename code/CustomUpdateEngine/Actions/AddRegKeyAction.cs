using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class AddRegKeyAction : GenericAction
    {
        public AddRegKeyAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Hive"))
                throw new ArgumentException("Unable to find token : Hive");
            Hive = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("RegKey"))
                throw new ArgumentException("Unable to find token : RegKey");
            RegKey = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("UseReg32"))
                throw new ArgumentException("Unable to find token : UseReg32");
            UseReg32 = bool.Parse(reader.ReadElementContentAsString());

            LogCompletion();
        }

        public string Hive { get; private set; }
        public string RegKey { get; private set; }
        public bool UseReg32 { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running AddRegKeyAction. Hive = " + this.Hive + " and RegKey = " + this.RegKey + " UseReg32 = " + this.UseReg32.ToString());

            try
            {
                RegistryKey hiveKey;
                switch (this.Hive)
                {
                    case "HKey_Current_User":
                        hiveKey = Registry.CurrentUser; // Il n'y a pas de notion de registre 32bit ou 64bit dans la ruche CURRENT_USER
                        break;
                    case "HKey_Local_Machine":
                        hiveKey = this.UseReg32 ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32) : RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                        break;
                    default:
                        throw new Exception("The Hive is not recognized.");
                }
                RegistryKey newKey;
                newKey = hiveKey.CreateSubKey(this.RegKey);
                newKey.Close();
                hiveKey.Close();

                Logger.Write(newKey != null ? "Key successfully created." : "**** Key not created.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of AddRegKeyAction.");
        }
    }
}
