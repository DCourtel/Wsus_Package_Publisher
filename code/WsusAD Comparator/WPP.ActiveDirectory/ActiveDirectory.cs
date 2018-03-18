using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using WPP.Management;

namespace WPP.ActiveDirectory
{
    public class ActiveDirectory
    {
        private IADServices _adServices;

        public ActiveDirectory()
        {
            this._adServices = new ActiveDirectoryServices();
        }

        public ActiveDirectory(IADServices adServices)
        {
            this._adServices = adServices;
        }

        public string GetDomainName()
        {
            return _adServices.GetDomainName();
        }

        public OrganizationalUnit GetRootOU()
        {
            return this._adServices.GetRootOU();
        }

        public List<WppComputer> GetAdComputers(string domainName)
        {
            return this._adServices.GetComputersInOU("LDAP://" +  domainName, SearchScope.Subtree);
        }

        public List<WppComputer> GetAdComputers(List<OrganizationalUnit> OUList)
        {
            List<WppComputer> computers = new List<WppComputer>();

            foreach (OrganizationalUnit ou in OUList)
            {
                computers.AddRange(this._adServices.GetComputersInOU(ou.Path, SearchScope.OneLevel));
            }

            return computers;
        }
    }
}
