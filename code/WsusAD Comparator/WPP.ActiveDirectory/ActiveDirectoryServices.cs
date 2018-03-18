using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using WPP.Management;

namespace WPP.ActiveDirectory
{
    public class ActiveDirectoryServices : IADServices
    {
        public List<WppComputer> GetComputersInOU(string ouPath, SearchScope searchScope)
        {
            List<WppComputer> computers = new List<WppComputer>();
            int userAccountControlFlag = 0;
            const int DisabledAccountFlag = 2;
            string hostname;
            string dn;
            string timestamp;
            string osName;
            string osServicePack;
            string osVersion;

            if (!string.IsNullOrEmpty(ouPath))
            {
                DirectoryEntry de = new DirectoryEntry(ouPath);
                DirectorySearcher ouSearch = new DirectorySearcher(de);
                ouSearch.SearchScope = searchScope;
                ouSearch.Tombstone = false;
                ouSearch.Filter = "(objectClass=Computer)";
                ouSearch.SizeLimit = int.MaxValue;
                ouSearch.PageSize = int.MaxValue;
                
                try
                {
                    SearchResultCollection collectedResult = ouSearch.FindAll();
                    foreach (SearchResult temp in collectedResult)
                    {
                        try
                        {
                            if (temp.Properties["dNSHostName"] != null && temp.Properties["dNSHostName"].Count != 0 &&
                                temp.Properties["userAccountControl"] != null && temp.Properties["userAccountControl"].Count != 0)
                            {
                                if (int.TryParse(GetADProperty(temp, "userAccountControl"), out userAccountControlFlag))
                                    if ((userAccountControlFlag & DisabledAccountFlag) != 2)
                                    {
                                        hostname = GetADProperty(temp, "dNSHostName");
                                        dn = GetADProperty(temp, "distinguishedName");
                                        timestamp = GetADProperty(temp, "lastLogonTimestamp");
                                        osName = GetADProperty(temp, "operatingSystem");
                                        osServicePack = GetADProperty(temp, "operatingSystemServicePack");
                                        osVersion = GetADProperty(temp, "operatingSystemVersion");

                                        computers.Add(new WppComputer(hostname, GetOU(dn), GetDateFromString(timestamp), osName, osServicePack, osVersion));
                                    }
                            }
                        }
                        catch (Exception) { }
                    }
                }
                catch (Exception) { }
            }
            return computers;
        }

        public string GetDomainName()
        {
            try
            {
                System.Net.NetworkInformation.IPGlobalProperties ipProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
                return ipProperties.DomainName;
            }
            catch (Exception) { }

            return String.Empty;
        }

        public OrganizationalUnit GetRootOU()
        {
            OrganizationalUnit rootOU = new OrganizationalUnit(GetDomainName());
            rootOU.Path = "LDAP://" + rootOU.Name;

            this.GetChildsOU(rootOU);

            return rootOU;
        }

        private void GetChildsOU(OrganizationalUnit parentOU)
        {
            DirectoryEntry dirEntry = new DirectoryEntry(parentOU.Path);
            DirectorySearcher ouSearch = new DirectorySearcher(dirEntry);
            ouSearch.Filter = "(objectCategory=organizationalUnit)";
            ouSearch.SearchScope = SearchScope.OneLevel;

            SearchResultCollection ouList = ouSearch.FindAll();
            if (ouList.Count > 0)
            {
                foreach (SearchResult ou in ouList)
                {
                    try
                    {
                        DirectoryEntry entry = ou.GetDirectoryEntry();
                        OrganizationalUnit childOU = new OrganizationalUnit(entry.Name);
                        childOU.ComputerCount = GetComputerCountInOU(entry);
                        childOU.Path = ou.Path;
                        parentOU.Childs.Add(childOU);
                        GetChildsOU(childOU);
                    }
                    catch (Exception) { }
                }
            }
        }

        private int GetComputerCountInOU(DirectoryEntry OUPath)
        {
            try
            {
                DirectorySearcher searcher = new DirectorySearcher(OUPath);
                searcher.Filter = "(ObjectCategory=computer)";
                searcher.SearchScope = SearchScope.OneLevel;

                SearchResultCollection computers = searcher.FindAll();

                return computers.Count;
            }
            catch (Exception) { }

            return 0;
        }

        private static string GetOU(string distinguishedName)
        {
            int index = distinguishedName.IndexOf(',');

            if (index != -1)
                return GetReverseOU(distinguishedName.Substring(index + 1));
            else
                return GetReverseOU(distinguishedName);
        }

        private static string GetReverseOU(string orderedOU)
        {
            string[] elements = orderedOU.Split(new char[] { ',' });
            string result = string.Empty;

            for (int i = elements.Length - 1; i >= 0; i--)
            {
                result += elements[i] + ",";
            }

            result = result.Substring(0, result.Length - 1);
            return result;
        }

        private static string GetADProperty(SearchResult result, string propertyName)
        {
            if (result.Properties[propertyName] != null && result.Properties[propertyName].Count != 0)
            {
                return result.Properties[propertyName][0].ToString();
            }

            return String.Empty;
        }

        private static DateTime GetDateFromString(string fileFormatedDateTime)
        {
            try
            {
                return DateTime.FromFileTime(Convert.ToInt64(fileFormatedDateTime));
            }
            catch (Exception) { }

            return new DateTime();
        }
    }
}
