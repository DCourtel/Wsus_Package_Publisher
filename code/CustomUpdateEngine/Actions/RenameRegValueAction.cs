using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class RenameRegValueAction : GenericAction
    {
        public RenameRegValueAction(string xmlFragment)
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
            if (!reader.ReadToFollowing("ValueName"))
                throw new ArgumentException("Unable to find token : ValueName");
            ValueName = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("NewName"))
                throw new ArgumentException("Unable to find token : NewName");
            NewName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string Hive { get; private set; }
        public string RegKey { get; private set; }
        public bool UseReg32 { get; private set; }
        public string ValueName { get; private set; }
        public string NewName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RenameRegValueAction. Hive = " + this.Hive + " and RegKey = " + this.RegKey + " UseReg32 = " + this.UseReg32.ToString() + " ValueName = " + ValueName + " NewName = " + NewName);

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
                RegistryKey targetKey;

                targetKey = hiveKey.OpenSubKey(RegKey, true);
                if(targetKey !=null)
                {
                    RegistryValueKind valueKind = targetKey.GetValueKind(this.ValueName);
                    object data = targetKey.GetValue(this.ValueName);
                    targetKey.SetValue(this.NewName, data, valueKind);  // Creating the new value
                    targetKey.DeleteValue(this.ValueName);  // Deleting the old value
                    targetKey.Close();
                    hiveKey.Close();
                }
                else
                {
                    Logger.Write("The registryKey " + this.RegKey + " doesn't exists.");
                }                
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RenameRegValueAction.");
        }
    }
}
