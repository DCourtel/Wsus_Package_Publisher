using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    public class ChangeServiceAction : GenericAction
    {
        public ChangeServiceAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ServiceName"))
                throw new ArgumentException("Unable to find token : ServiceName");
            ServiceName = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Mode"))
                throw new ArgumentException("Unable to find token : Mode");
            Mode = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string ServiceName { get; private set; }
        public string Mode { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running ChangeServiceAction. ServiceName = " + this.ServiceName + " and Mode = " + this.Mode);

            try
            {
                uint resultCode = 1;
                string filter = String.Format("SELECT * FROM Win32_Service WHERE Name = '{0}'", this.ServiceName);

                ManagementObjectSearcher query = new ManagementObjectSearcher(filter);

                if (query != null)
                {
                    ManagementObjectCollection services = query.Get();

                    foreach (ManagementObject service in services)
                    {
                        ManagementBaseObject inParams = service.GetMethodParameters("ChangeStartMode");
                        inParams["startmode"] = this.Mode;

                        ManagementBaseObject outParams = service.InvokeMethod("ChangeStartMode", inParams, null);
                        resultCode = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                        if (resultCode == 0)
                            Logger.Write("Successfully changed the startup mode of " + ServiceName);
                        else
                            Logger.Write("Failed to change the startup mode of " + ServiceName + ". Result code = " + resultCode + " : " + this.GetResultCodeMeaning(resultCode));
                    }
                }
                else
                    Logger.Write("The service " + ServiceName + " was not found.");
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to change the startup mode of " + ServiceName + "\r\n" + ex.Message);
            }
            Logger.Write("End of ChangeServiceAction.");
        }

        private string GetResultCodeMeaning(uint resultCode)
        {
            string meaning = "Unknown result code.";

            switch (resultCode)
            {
                case 0:
                    meaning = "Success";
                    break;
                case 1:
                    meaning = "Not Supported";
                    break;
                case 2:
                    meaning = "Access Denied";
                    break;
                case 3:
                    meaning = "Dependent Services Running";
                    break;
                case 4:
                    meaning = "Invalid Service Control";
                    break;
                case 5:
                    meaning = "Service Cannot Accept Control";
                    break;
                case 6:
                    meaning = "Service Not Active";
                    break;
                case 7:
                    meaning = "Service Request Timeout";
                    break;
                case 8:
                    meaning = "Unknown Failure";
                    break;
                case 9:
                    meaning = "Path Not Found";
                    break;
                case 10:
                    meaning = "Service Already Running";
                    break;
                case 11:
                    meaning = "Service Database Locked";
                    break;
                case 12:
                    meaning = "Service Dependency Deleted";
                    break;
                case 13:
                    meaning = "Service Dependency Failure";
                    break;
                case 14:
                    meaning = "Service Disabled";
                    break;
                case 15:
                    meaning = "Service Logon Failed";
                    break;
                case 16:
                    meaning = "Service Marked For Deletion";
                    break;
                case 17:
                    meaning = "Service Not Thread";
                    break;
                case 18:
                    meaning = "Status Circular Dependency";
                    break;
                case 19:
                    meaning = "Status Duplicate Name";
                    break;
                case 20:
                    meaning = "Status Invalid Name";
                    break;
                case 21:
                    meaning = "Status Invalid Parameter";
                    break;
                case 22:
                    meaning = "Status Invalid Service Account";
                    break;
                case 23:
                    meaning = "Status Service Exists";
                    break;
                case 24:
                    meaning = "Service Already Paused";
                    break;
            }

            return meaning;
        }
    }
}
