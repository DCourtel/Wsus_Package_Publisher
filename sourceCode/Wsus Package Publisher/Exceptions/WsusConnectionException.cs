using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class WsusConnectionException:Exception
    {
        System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(WsusConnectionException).Assembly);

        public override string Message
        {
            get
            {
                return resMan.GetString("WsusConnectionException");
            }
        }
    }
}
