using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    public class StopServiceAction : GenericAction
    {
        public StopServiceAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ServiceName"))
                throw new ArgumentException("Unable to find token : ServiceName");
            ServiceName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string ServiceName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running StopServiceAction. ServiceName = " + this.ServiceName);

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
                        ManagementBaseObject outParams = service.InvokeMethod("StopService", null, null);
                        resultCode = Convert.ToUInt16(outParams.Properties["ReturnValue"].Value);
                        if (resultCode == 0)
                            Logger.Write("Successfully stop " + ServiceName);
                        else
                            Logger.Write("Failed to stop " + ServiceName + ". Result code = " + resultCode + " : " + this.GetResultCodeMeaning(resultCode));
                    }
                }
                else
                    Logger.Write("The service " + ServiceName + " was not found.");
            }
            catch (Exception ex)
            {
                Logger.Write("Failed stop " + ServiceName + "\r\n" + ex.Message);
            }
            Logger.Write("End of StopServiceAction.");
        }

        private string GetResultCodeMeaning(uint resultCode)
        {
            string meaning = "Unknown result code.";

            switch (resultCode)
            {
                case 0:
                    meaning = "The request was accepted";
                    break;
                case 1:
                    meaning = "The request is not supported";
                    break;
                case 2:
                    meaning = "The user did not have the necessary access";
                    break;
                case 3:
                    meaning = "The service cannot be stopped because other services that are running are dependent on it";
                    break;
                case 4:
                    meaning = "The requested control code is not valid, or it is unacceptable to the service";
                    break;
                case 5:
                    meaning = "The requested control code cannot be sent to the service because the state of the service (Win32_BaseService.State property) is equal to 0, 1, or 2";
                    break;
                case 6:
                    meaning = "The service has not been started";
                    break;
                case 7:
                    meaning = "The service did not respond to the start request in a timely fashion";
                    break;
                case 8:
                    meaning = "Unknown failure when starting the service";
                    break;
                case 9:
                    meaning = "The directory path to the service executable file was not found";
                    break;
                case 10:
                    meaning = "Service Already Running";
                    break;
                case 11:
                    meaning = "The database to add a new service is locked";
                    break;
                case 12:
                    meaning = "A dependency this service relies on has been removed from the system";
                    break;
                case 13:
                    meaning = "The service failed to find the service needed from a dependent service";
                    break;
                case 14:
                    meaning = "The service has been disabled from the system";
                    break;
                case 15:
                    meaning = "The service does not have the correct authentication to run on the system";
                    break;
                case 16:
                    meaning = "This service is being removed from the system";
                    break;
                case 17:
                    meaning = "The service has no execution thread";
                    break;
                case 18:
                    meaning = "The service has circular dependencies when it starts";
                    break;
                case 19:
                    meaning = "A service is running under the same name";
                    break;
                case 20:
                    meaning = "The service name has invalid characters";
                    break;
                case 21:
                    meaning = "Invalid parameters have been passed to the service";
                    break;
                case 22:
                    meaning = "The account under which this service runs is either invalid or lacks the permissions to run the service";
                    break;
                case 23:
                    meaning = "The service exists in the database of services available from the system";
                    break;
                case 24:
                    meaning = "The service is currently paused in the system";
                    break;
            }

            return meaning;
        }
    }
}
