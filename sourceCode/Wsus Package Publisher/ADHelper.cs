using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Management;

namespace Wsus_Package_Publisher
{
    internal class ADHelper
    {
        internal static string GetDomainName()
        {
#if DEBUG
            return "ad.fr";
#endif
#if !DEBUG
            return System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
#endif
        }

        internal static List<ADComputer> GetComputers(string DomainName)
        {
            return GetComputersInOU(DomainName, SearchScope.Subtree);
        }

        internal static List<ADComputer> GetComputers(List<string> OUList)
        {
            Logger.EnteringMethod();

            List<ADComputer> computers = new List<ADComputer>();

            if (OUList != null)
            {
                foreach (string ou in OUList)
                {
                    computers.AddRange(GetComputersInOU(ou, SearchScope.OneLevel));
                }
            }

            return computers;
        }

        internal static System.Windows.Forms.TreeNode GetOUList(System.Windows.Forms.TreeNode rootNode)
        {
            Logger.EnteringMethod();

            try
            {
                DirectoryEntry dirEntry = new DirectoryEntry(rootNode.Tag.ToString());
                DirectorySearcher ouSearch = new DirectorySearcher(dirEntry);
                ouSearch.Filter = "(objectCategory=organizationalUnit)";
                ouSearch.SearchScope = SearchScope.OneLevel;

                SearchResultCollection ouList = ouSearch.FindAll();
                if (ouList.Count == 0)
                    return null;
                
                foreach (SearchResult ou in ouList)
                {
                    DirectoryEntry entry = ou.GetDirectoryEntry();
                    System.Windows.Forms.TreeNode subNode = new System.Windows.Forms.TreeNode(entry.Name);
                    subNode.Text += " (" + GetComputerCountInOU(entry) + ")";
                    subNode.Tag = ou.Path;
                    System.Windows.Forms.TreeNode tempNode = GetOUList(subNode);
                    if (tempNode != null)
                        rootNode.Nodes.Add(tempNode);
                    else
                        rootNode.Nodes.Add(subNode);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return rootNode;
        }

        private static int GetComputerCountInOU(DirectoryEntry OUPath)
        {
            try
            {
                DirectorySearcher searcher = new DirectorySearcher(OUPath);
                searcher.Filter = "(ObjectCategory=computer)";
                searcher.SearchScope = SearchScope.OneLevel;

                SearchResultCollection computers = searcher.FindAll();

                return computers.Count;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private static List<ADComputer> GetComputersInOU(string OUName, SearchScope scope)
        {
            List<ADComputer> computers = new List<ADComputer>();
            int userAccountControlFlag = 0;
            const int DisabledAccountFlag = 2;
            Int64 date;
            DateTime Timestamp;
            string osName;
            string osServicePack;
            string osVersion;

            if (!string.IsNullOrEmpty(OUName))
            {
                Logger.Write("Searching ADComputers into " + OUName);

                DirectoryEntry de = new DirectoryEntry(OUName);
                DirectorySearcher ouSearch = new DirectorySearcher(de);
                ouSearch.SearchScope = scope;
                ouSearch.Tombstone = false;
                ouSearch.Filter = "(objectClass=Computer)";
                ouSearch.SizeLimit = int.MaxValue;
                ouSearch.PageSize = int.MaxValue;

                try
                {
                    SearchResultCollection collectedResult = ouSearch.FindAll();
                    foreach (SearchResult temp in collectedResult)
                    {
                        if (temp.Properties["dNSHostName"] != null && temp.Properties["dNSHostName"].Count != 0 &&
                            temp.Properties["userAccountControl"] != null && temp.Properties["userAccountControl"].Count != 0)
                        {
                            if (int.TryParse(temp.Properties["userAccountControl"][0].ToString(), out userAccountControlFlag))
                                if ((userAccountControlFlag & DisabledAccountFlag) != 2)
                                {
                                    if (temp.Properties["lastLogonTimestamp"] != null && temp.Properties["lastLogonTimestamp"].Count != 0)
                                    {
                                        date = Convert.ToInt64(temp.Properties["lastLogonTimestamp"][0].ToString());
                                        Timestamp = DateTime.FromFileTime(date);
                                    }
                                    else
                                        Timestamp = new DateTime();
                                    if (temp.Properties["operatingSystem"] != null && temp.Properties["operatingSystem"].Count != 0)
                                        osName = temp.Properties["operatingSystem"][0].ToString();
                                    else
                                        osName = string.Empty;
                                    if (temp.Properties["operatingSystemServicePack"] != null && temp.Properties["operatingSystemServicePack"].Count != 0)
                                        osServicePack = temp.Properties["operatingSystemServicePack"][0].ToString();
                                    else
                                        osServicePack = string.Empty;
                                    if (temp.Properties["operatingSystemVersion"] != null && temp.Properties["operatingSystemVersion"].Count != 0)
                                        osVersion = temp.Properties["operatingSystemVersion"][0].ToString();
                                    else
                                        osVersion = string.Empty;

                                    computers.Add(new ADComputer(temp.Properties["dNSHostName"][0].ToString(),
                                        GetOU(temp.Properties["distinguishedName"][0].ToString()),
                                        Timestamp,
                                        osName,
                                        osServicePack,
                                        osVersion));
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return computers;
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
    }
}
