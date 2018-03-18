using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class AddRegValueAction : GenericAction
    {
        public AddRegValueAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Hive"))
                throw new ArgumentException("Unable to find token : Hive");
            Hive = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("RegKey"))
                throw new ArgumentException("Unable to find token : RegKey");
            RegKey = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("ValueName"))
                throw new ArgumentException("Unable to find token : ValueName");
            ValueName = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Data"))
                throw new ArgumentException("Unable to find token : Data");
            Data = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Type"))
                throw new ArgumentException("Unable to find token : Type");
            ValueType = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("UseReg32"))
                throw new ArgumentException("Unable to find token : UseReg32");
            UseReg32 = bool.Parse(reader.ReadElementContentAsString());

            LogCompletion();
        }

        public string Hive { get; private set; }
        public string RegKey { get; private set; }
        public bool UseReg32 { get; private set; }
        public string ValueName { get; private set; }
        public string Data { get; private set; }
        public string ValueType { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running AddRegValueAction. Hive = " + this.Hive + " and RegKey = " + this.RegKey + " UseReg32 = " + this.UseReg32.ToString() + " ValueName = " + ValueName + " Data = " + Data + " valueType = " + ValueType);

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

                targetKey = hiveKey.CreateSubKey(RegKey); // Open or Create if missing

                switch (GetValueType(ValueType))
                {
                    case RegistryValueKind.Binary:
                        targetKey.SetValue(ValueName, this.GetBinaryData(Data), GetValueType(ValueType));
                        break;
                    case RegistryValueKind.MultiString:
                        targetKey.SetValue(ValueName, this.GetStringData(Data), GetValueType(ValueType));
                        break;
                    case RegistryValueKind.DWord:
                    case RegistryValueKind.QWord:
                    case RegistryValueKind.String:
                    case RegistryValueKind.ExpandString:
                        targetKey.SetValue(ValueName, (object)Data, GetValueType(ValueType));
                        break;
                }
                targetKey.Flush();
                object newValue = targetKey.GetValue(ValueName, null);
                targetKey.Close();
                hiveKey.Close();

                Logger.Write(newValue != null ? "Value successfully created." : "**** Value not created.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of AddRegValueAction.");
        }

        private RegistryValueKind GetValueType(string ValueType)
        {
            switch (ValueType)
            {
                case "REG_SZ":
                    return RegistryValueKind.String;
                case "REG_BINARY":
                    return RegistryValueKind.Binary;
                case "REG_DWORD":
                    return RegistryValueKind.DWord;
                case "REG_QWORD":
                    return RegistryValueKind.QWord;
                case "REG_MULTI_SZ":
                    return RegistryValueKind.MultiString;
                case "REG_EXPAND_SZ":
                    return RegistryValueKind.ExpandString;
                default:
                    throw new ArgumentException("Registry Value Kind " + ValueType + " is not recognized.");
            }
        }

        private byte[] GetBinaryData(string data)
        {
            string[] stringData = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] binaryData = new byte[stringData.Length];
            for (int i = 0; i < stringData.Length; i++)
            {
                binaryData[i] = byte.Parse(stringData[i]);
            }

            return binaryData;
        }

        public string[] GetStringData(string data)
        {
            List<string> listOfString = new List<string>();

            data = data.Replace(@"\\", "\0");

            listOfString = data.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList<String>();

            for (int i = 0; i < listOfString.Count; i++)
            {
                if(listOfString[i].Contains("\0"))
                {
                    listOfString[i] = listOfString[i].Replace("\0", "\\");
                }
            }

            return listOfString.ToArray();
        }
    }
}
