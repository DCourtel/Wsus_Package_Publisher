using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPP.Management;
using Microsoft.UpdateServices.Administration;

namespace WPP.Wsus
{
    public interface IWsusServices
    {
        bool IsInWsus(string computerName);
    }
}
