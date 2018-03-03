using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    internal class RegistryElement : GenericElement
    {
        private enum ActionType
        {
            Undefined,
            Add,
            Delete,
            Modify,
            Read
        }

        private enum ValueType
        {
            Undefined,
            REG_SZ,
            REG_BINARY,
            REG_DWORD,
            REG_QWORD,
            REG_MULTI_SZ,
            REG_EXPAND_SZ
        }

        private List<VariableElement> _variables;

        internal RegistryElement(string xmlFragment)
        {
            Logger.Write("Initializing RegistryElement with : " + xmlFragment);

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
            if (!reader.ReadToFollowing("RegValue"))
                throw new Exception();
            RegValue = reader.ReadString();
            if (!reader.ReadToFollowing("RegType"))
                throw new Exception();
            RegType = (ValueType)Enum.Parse(typeof(ValueType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("RegData"))
                throw new Exception();
            RegData = reader.ReadString();
            if (!reader.ReadToFollowing("RegVariable"))
                throw new Exception();
            if (!reader.IsEmptyElement)
                RegVariable = new Guid(reader.ReadString());

            Logger.Write("End of Initializing of RegistryElement.");
        }

        private ActionType RegAction { get; set; }
        private string RegHive { get; set; }
        private string RegKey { get; set; }
        private string RegValue { get; set; }
        private ValueType RegType { get; set; }
        private string RegData { get; set; }
        private Guid RegVariable { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running RegistryElement.");
            _variables = variables;

            switch (RegAction)
            {
                case ActionType.Undefined:
                    break;
                case ActionType.Add:
                    AddRegistryValue();
                    break;
                case ActionType.Delete:
                    DeleteRegistryValue();
                    break;
                case ActionType.Modify:
                    ModifyRegistry();
                    break;
                case ActionType.Read:
                    ReadRegistryValue();
                    break;
                default:
                    break;
            }
        }

        private void ModifyRegistry()
        {
            Logger.Write("Modifying Registry.");

            RegistryKey regKey = GetRegistryHive();

            try
            {
                Logger.Write("Will try to modifie " + RegHive + "\\" + RegKey + "\\" + RegValue + " and set it to : " + RegData);
                regKey = regKey.OpenSubKey(RegKey, true);
                RegistryValueKind valueKind = regKey.GetValueKind(RegValue);
                switch (valueKind)
                {
                    case RegistryValueKind.Binary:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.Binary);
                        break;
                    case RegistryValueKind.DWord:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.DWord);
                        break;
                    case RegistryValueKind.ExpandString:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.ExpandString);
                        break;
                    case RegistryValueKind.MultiString:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.MultiString);
                        break;
                    case RegistryValueKind.QWord:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.QWord);
                        break;
                    case RegistryValueKind.String:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.String);
                        break;
                    case RegistryValueKind.Unknown:
                        regKey.SetValue(RegValue, RegData, RegistryValueKind.Unknown);
                        break;
                    default:
                        break;
                }
                regKey.Flush();
                regKey.Close();
            }
            catch (Exception ex)
            {
                Logger.Write("Error Modifying Registry Key : " + RegHive + "\\" + RegKey + "\\" + RegValue + "\r\n" + ex.Message);
            }
        }

        private void DeleteRegistryValue()
        {
            Logger.Write("Deleting Registry Value.");

            RegistryKey regKey = GetRegistryHive();

            try
            {
                Logger.Write("Will try to delete " + RegValue + " at " + RegHive + "\\" + RegKey);
                RegistryKey subKey = regKey.OpenSubKey(RegKey, true);
                subKey.DeleteValue(RegValue);
                subKey.Flush();
                subKey.Close();
                Logger.Write("Successfully deleted " + RegValue + " in " + RegKey);
            }
            catch (Exception ex)
            {
                Logger.Write("Error Deleting Registry Value : " + ex.Message);
            }
        }

        private void AddRegistryValue()
        {
            Logger.Write("Adding Registry Value.");

            RegistryKey regKey = GetRegistryHive();

            try
            {
                Logger.Write("Will try to add : " + RegHive + "\\" + RegKey + "\\" + RegValue + " With Data : " + RegData);
                RegistryKey subKey = regKey.OpenSubKey(RegKey, true);

                switch (RegType)
                {
                    case ValueType.Undefined:
                        break;
                    case ValueType.REG_SZ:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.String);
                        break;
                    case ValueType.REG_BINARY:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.Binary);
                        break;
                    case ValueType.REG_DWORD:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.DWord);
                        break;
                    case ValueType.REG_QWORD:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.QWord);
                        break;
                    case ValueType.REG_MULTI_SZ:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.MultiString);
                        break;
                    case ValueType.REG_EXPAND_SZ:
                        subKey.SetValue(RegValue, (object)RegData, RegistryValueKind.ExpandString);
                        break;
                    default:
                        break;
                }
                Logger.Write("Successfully add : " + RegHive + "\\" + RegKey + "\\" + RegValue);
            }
            catch (Exception ex)
            {
                Logger.Write("Error Adding Registry Value : " + ex.Message);
            }
        }

        private void ReadRegistryValue()
        {
            Logger.Write("Reading Registry Value.");

            RegistryKey regKey = GetRegistryHive();

            try
            {
                Logger.Write("Will try to read : " + RegHive + "\\" + RegKey + "\\" + RegValue + " into variable : " + RegVariable);
                RegistryKey subKey = regKey.OpenSubKey(RegKey, true);

                object data = subKey.GetValue(RegValue);
                for (int i = 0; i < _variables.Count; i++)
                {
                    if (_variables[i].VarID.Equals(RegVariable))
                    {
                        if (_variables[i].VarType == VariableElement.VariableType.Int)
                        {
                            int intValue;
                            if (int.TryParse(data.ToString(), out intValue))
                            {
                                _variables[i].IntValue = intValue;
                                Logger.Write("Successfully place " + data.ToString() + " into variable.");
                            }
                            else
                                Logger.Write(data.ToString() + " can not be convert into Integer.");
                        }
                        else
                        {
                            _variables[i].StringValue = data.ToString();
                            Logger.Write("Successfully place " + data.ToString() + " into variable.");
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Error reading Registry Value : " + ex.Message);
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
