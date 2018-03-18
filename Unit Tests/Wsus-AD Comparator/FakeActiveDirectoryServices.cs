using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using WPP.ActiveDirectory;
using WPP.Management;

namespace Unit_Tests_Wsus_AD_Comparator
{
    class FakeActiveDirectoryServices:IADServices
    {        
        public List<WppComputer> GetComputersInOU(string domainName, SearchScope searchScope)
        {
            List<WppComputer> fakeComputers = new List<WppComputer>();
            FakeComputerServices fakeComputerServices = new FakeComputerServices();
            for (int i = 0; i < 10; i++)
            {
                fakeComputers.Add(new WppComputer("fakeComputer" + i, fakeComputerServices));
            }

            return fakeComputers;
        }

        public string GetDomainName()
        {
            return "WPP.local";
        }

        public OrganizationalUnit GetRootOU()
        {
            return new OrganizationalUnit("Mcsa.local");
        }
    }
}
