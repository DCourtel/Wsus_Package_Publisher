using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Management;
using Microsoft.Win32;

namespace Unit_Tests_CustomeUpdateEngine
{
    public static class Tools
    {
        public static string GetXmlFragment(string filename)
        {
            string templatePath = @"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests";
            System.IO.StreamReader reader = new System.IO.StreamReader(System.IO.Path.Combine(templatePath, filename));
            string xmlFragment = reader.ReadToEnd();
            reader.Close();

            return xmlFragment;
        }

        public static string GetServiceStartType(string serviceName)
        {
            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);
            string startType = String.Empty;

            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

                if (query != null)
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        startType = service.GetPropertyValue("StartMode").ToString();
                    }
                }
            }
            catch (Exception) { }

            return startType;
        }

        public static void StartService(string serviceName)
        {
            if (!IsServiceRunning(serviceName))
            {
                string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);
                ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

                if (query != null)
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        ManagementBaseObject outParams = service.InvokeMethod("StartService", null, null);
                        UInt32 resultCode = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                        if (resultCode != 0)
                            throw new Exception("Can't start service");
                    }
                }
            }
        }

        public static void StopService(string serviceName)
        {
            if (IsServiceRunning(serviceName))
            {
                string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);
                ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

                if (query != null)
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        ManagementBaseObject outParams = service.InvokeMethod("StopService", null, null);
                        UInt32 resultCode = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                        if (resultCode != 0)
                            throw new Exception("Can't stop service");
                    }
                }
            }
        }

        public static bool IsServiceRunning(string serviceName)
        {
            string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", serviceName);
            bool isStarted = false;

            ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

            if (query != null)
            {
                ManagementObjectCollection services = query.Get();

                foreach (ManagementObject service in services)
                {
                    isStarted = (bool)service["Started"];
                }
            }

            return isStarted;
        }

        public static global::CustomUpdateEngine.ReturnCodeAction GetReturnCodeAction()
        {
            return new global::CustomUpdateEngine.ReturnCodeAction("<Action><ElementType>CustomActions.ReturnCode</ElementType><Method>Static</Method><Code>0</Code></Action>");
        }

        public static RegistryKey CreateRegistryKey(RegistryHive hive, string keyname, bool reg32)
        {
            // Open the base key (hive)
            RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, reg32 ? RegistryView.Registry32 : RegistryView.Registry64);

            // Open or Create the subkey
            RegistryKey newKey = baseKey.CreateSubKey(keyname);

            return newKey;
        }

        public static void DeleteRegistryKey(RegistryHive hive, string keyname, bool reg32)
        {
            // Open the base key (hive)
            RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, reg32 ? RegistryView.Registry32 : RegistryView.Registry64);

            // Delete the RegistryKey
            baseKey.DeleteSubKeyTree(keyname, false);
        }

        public static void CreateRegistryValue(RegistryKey key, string valueName, RegistryValueKind valueKind, object data)
        {
            key.SetValue(valueName, data, valueKind);
        }

        public static void DeleteRegistryValue(RegistryKey key, string valueName)
        {
            key.DeleteValue(valueName, false);
        }

        public static void ImportScheduledTask(string filename, string taskName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.FileName = @"C:\Windows\System32\schtasks.exe";
            processInfo.Arguments = "/Create /XML \"" + filename + "\" /F /TN \"" + taskName + "\"";
            processInfo.ErrorDialog = false;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            process.StartInfo = processInfo;
            process.Start();

            if (!process.WaitForExit(3000))
            {
                process.Kill();
            }
        }

        public static bool IsScheduledTaskExist(string taskName)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.FileName = @"C:\Windows\System32\schtasks.exe";
            processInfo.Arguments = "/Query /tn \"" + taskName + "\" /FO csv";
            processInfo.ErrorDialog = false;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardOutput = true;

            process.StartInfo = processInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();

            if (!process.WaitForExit(3000))
            {
                process.Kill();
            }

            return output.Contains(taskName);
        }

        public static void InstallJava(string filepath)
        {
            // /s INSTALL_SILENT=Enable AUTO_UPDATE=Disable WEB_JAVA=Enable REBOOT=Disable NOSTARTMENU=Enable

            Process process = new Process();
            ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();
            processInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            processInfo.FileName = filepath;
            processInfo.Arguments = @"/s INSTALL_SILENT=Enable AUTO_UPDATE=Disable WEB_JAVA=Enable REBOOT=Disable NOSTARTMENU=Enable";
            processInfo.ErrorDialog = false;
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            process.StartInfo = processInfo;
            process.Start();
            process.WaitForExit();
        }

        public static bool ProductsContains(List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> productList, string id)
        {
            foreach (global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct product in productList)
            {
                if (String.Compare(product.ID, id, true) == 0)
                    return true;
            }
            return false;
        }
    }
}
