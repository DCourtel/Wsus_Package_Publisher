using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class RenameRegKeyAction : GenericAction
    {
        public RenameRegKeyAction(string xmlFragment)
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
            if (!reader.ReadToFollowing("NewName"))
                throw new ArgumentException("Unable to find token : NewName");
            NewName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string Hive { get; private set; }
        public string RegKey { get; private set; }
        public bool UseReg32 { get; private set; }
        public string NewName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RenameRegKeyAction. Hive = " + this.Hive + " and RegKey = " + this.RegKey + " UseReg32 = " + this.UseReg32.ToString() + " NewName = " + this.NewName);

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

                
                RegistryKey parentKey = hiveKey.OpenSubKey(RegKey.Substring(0, RegKey.LastIndexOf(@"\")), true);
                string subKeyName = RegKey.Substring(RegKey.LastIndexOf(@"\") + 1);

                CopyKey(parentKey, subKeyName, this.NewName);
                parentKey.DeleteSubKeyTree(subKeyName);
                parentKey.Flush();
                parentKey.Close();
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RenameRegKeyAction.");
        }

        private void CopyKey(RegistryKey parentKey, string keyNameToCopy, string newKeyName)
        {
            RegistryKey destinationKey = parentKey.CreateSubKey(newKeyName);

            RegistryKey sourceKey = parentKey.OpenSubKey(keyNameToCopy);

            RecurseCopyKey(sourceKey, destinationKey);
            sourceKey.Flush();
            sourceKey.Close();
        }

        private void RecurseCopyKey(RegistryKey sourceKey, RegistryKey destinationKey)
        {
            foreach (string valueName in sourceKey.GetValueNames())
            {
                object objValue = sourceKey.GetValue(valueName);
                RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                destinationKey.SetValue(valueName, objValue, valKind);
            }

            foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName);
                RegistryKey destSubKey = destinationKey.CreateSubKey(sourceSubKeyName);
                RecurseCopyKey(sourceSubKey, destSubKey);
                sourceSubKey.Flush();
                sourceSubKey.Close();
            }
        }
    }
}
