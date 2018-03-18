using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class ChangeRegDataAction : GenericAction
    {
        public ChangeRegDataAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Hive"))
                throw new ArgumentException("Unable to find token : Hive");
            Hive = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("RegKey"))
                throw new ArgumentException("Unable to find token : RegKey");
            RegKey = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("RegValue"))
                throw new ArgumentException("Unable to find token : RegValue");
            RegValue = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("DefaultValue"))
                throw new ArgumentException("Unable to find token : DefaultValue");
            DefaultValue = bool.Parse(reader.ReadElementContentAsString());
            if (!reader.ReadToFollowing("NewData"))
                throw new ArgumentException("Unable to find token : NewData");
            NewData = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("UseReg32"))
                throw new ArgumentException("Unable to find token : UseReg32");
            UseReg32 = bool.Parse(reader.ReadElementContentAsString());

            LogCompletion();
        }

        public string Hive { get; private set; }
        public string RegKey { get; private set; }
        public string RegValue { get; private set; }
        public bool DefaultValue { get; private set; }
        public string NewData { get; private set; }
        public bool UseReg32 { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running ChangeRegDataAction. Hive = " + this.Hive + " and RegKey = " + this.RegKey + " RegValue = " + (this.DefaultValue ? "DefaultValue" : this.RegValue) + " NewData = " + this.NewData + " UseReg32 = " + this.UseReg32.ToString());

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
                if (this.DefaultValue)
                { this.RegValue = String.Empty; }
                RegistryKey targetKey;
                targetKey = hiveKey.CreateSubKey(this.RegKey);
                targetKey.SetValue(RegValue, (object)NewData);
                object targetValueContent = targetKey.GetValue(RegValue, null);
                targetKey.Close();
                hiveKey.Close();

                Logger.Write((targetValueContent != null && (object)targetValueContent == (object)NewData) ? "Value successfully modified." : "**** Value not modified.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of ChangeRegDataAction.");
        }
    }
}
