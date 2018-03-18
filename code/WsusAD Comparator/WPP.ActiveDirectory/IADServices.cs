using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using WPP.Management;

namespace WPP.ActiveDirectory
{
   public interface IADServices
    {
       string GetDomainName();
       OrganizationalUnit GetRootOU();
       List<WppComputer> GetComputersInOU(string domainName, SearchScope searchScope);
    }
}
