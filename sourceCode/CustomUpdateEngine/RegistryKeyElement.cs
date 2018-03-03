using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    class RegistryKeyElement : GenericElement
    {
        private enum ActionType
        {
            Undefined,
            Add,
            Delete,
            Rename
        }

        internal RegistryKeyElement(string xmlFragment)
        {
            Logger.Write("Initializing RegistryKeyElement with : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("RegAction"))
                throw new Exception();
            RegAction = (ActionType)Enum.Parse(typeof(ActionType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("RegHive"))
                throw new Exception();
            RegHive = reader.ReadString();
            if (!reader.ReadToFollowing("RegKey"))
                throw new Exception();
            RegKey = reader.ReadString();
            if (!reader.ReadToFollowing("RegName"))
                throw new Exception();
            RegName = reader.ReadString();

            Logger.Write("End of Initializing of RegistryKeyElement.");
        }

        private ActionType RegAction { get; set; }
        private string RegHive { get; set; }
        private string RegKey { get; set; }
        private string RegName { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running RegistryKeyElement.");

            switch (RegAction)
            {
                case ActionType.Undefined:
                    break;
                case ActionType.Add:
                    AddRegistryKey();
                    break;
                case ActionType.Delete:
                    DeleteRegistryKey();
                    break;
                case ActionType.Rename:
                    RenameRegistryKey();
                    break;
                default:
                    break;
            }
        }

        private void RenameRegistryKey()
        {
            Logger.Write("Rename Registry.");

            try
            {
                if (RegKey.EndsWith(@"\"))
                    RegKey = RegKey.Substring(0, RegKey.Length - 1);

                Logger.Write("Will try to rename " + RegHive + "\\" + RegKey + " into : " + RegName);

                RegistryKey parentKey = GetRegistryHive().OpenSubKey(RegKey.Substring(0, RegKey.LastIndexOf(@"\")), true);
                string subKeyName = RegKey.Substring(RegKey.LastIndexOf(@"\")+1);

                CopyKey(parentKey, subKeyName, RegName);
                parentKey.DeleteSubKeyTree(subKeyName);
                parentKey.Flush();
                parentKey.Close();
            }
            catch (Exception ex)
            {
                Logger.Write("Error renaming Registry Key : " + RegHive + "\\" + RegKey + "\r\n" + ex.Message);
            }
        }

        internal void CopyKey(RegistryKey parentKey, string keyNameToCopy, string newKeyName)
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
        
        private void DeleteRegistryKey()
        {
            Logger.Write("Deleting Registry Key.");

            try
            {
                Logger.Write("Will try to delete " + RegHive + "\\" + RegKey);

                RegistryKey parentKey = GetRegistryHive().OpenSubKey(RegKey.Substring(0, RegKey.LastIndexOf(@"\")), true);
                string subKeyName = RegKey.Substring(RegKey.LastIndexOf(@"\") + 1);
                parentKey.DeleteSubKeyTree(subKeyName);
                parentKey.Flush();
                parentKey.Close();

                Logger.Write("Successfully deleted " + RegHive + "\\" + RegKey);
            }
            catch (Exception ex)
            {
                Logger.Write("Error Deleting Registry Value : " + ex.Message);
            }
        }

        private void AddRegistryKey()
        {
            Logger.Write("Adding Registry Key.");
            
            try
            {
                Logger.Write("Will try to add : " + RegHive + "\\" + RegKey);

                string parentKeyName = RegKey.Substring(0, RegKey.LastIndexOf(@"\"));

                RegistryKey parentKey = GetRegistryHive().OpenSubKey(parentKeyName, true);
                string subKeyName = RegKey.Substring(RegKey.LastIndexOf(@"\") + 1);
                parentKey.CreateSubKey(subKeyName);
                parentKey.Flush();
                parentKey.Close();

                Logger.Write("Successfully add : " + RegHive + "\\" + RegKey);
            }
            catch (Exception ex)
            {
                Logger.Write("Error Adding Registry Value : " + ex.Message);
            }
        }

        private RegistryKey GetRegistryHive()
        {
            switch (RegHive)
            {
                case "HKEY_LOCAL_MACHINE":
                    return Registry.LocalMachine;
                case "HKEY_CURRENT_USER":
                    return Registry.CurrentUser;
                default:
                    throw new ArgumentException("The Registry hive : " + RegHive + " is unknown.");
            }
        }
    }
}
