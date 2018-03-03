using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wsus_Package_Publisher
{
    internal class CatalogSubscription
    {
        private const string _subscribedCatalogsFolder = "Subscribed Catalogs";

        internal enum CheckingUnits
        {
            Days,
            Weeks,
            Months
        }

        internal enum ActionTypes
        {
            NotifyUser,
            DownloadAndNotify
        }

        private List<CatalogUpdate> _deleteUpdates = new List<CatalogUpdate>();
        private List<CatalogUpdate> _addedUpdates = new List<CatalogUpdate>();

        internal CatalogSubscription()
        {
            IsActive = true;
            Address = string.Empty;
            CheckEvery = 1;
            Unit = CheckingUnits.Weeks;
            LastCheckDate = new DateTime();
            LastCheckResult = false;
            CatalogName = string.Empty;
            Hash = string.Empty;
            IsUpdateAvailable = false;
            Closing = false;
        }

        public override string ToString()
        {
            return CatalogName;
        }

        #region {internal Properties - Propriétés internes}

        /// <summary>
        /// Get or Set if this subscription is periodically checked.
        /// </summary>
        internal bool IsActive { get; set; }

        /// <summary>
        /// Get or Set the address where to download the catalog.
        /// </summary>
        internal string Address { get; set; }

        /// <summary>
        /// Get or Set the time between two check.
        /// </summary>
        internal int CheckEvery { get; set; }

        /// <summary>
        /// Get or Set the unit for the check.
        /// </summary>
        internal CheckingUnits Unit { get; set; }

        /// <summary>
        /// Date of the last time check.
        /// </summary>
        internal DateTime LastCheckDate { get; set; }

        /// <summary>
        /// Get or Set if the last check was successfull.
        /// </summary>
        internal bool LastCheckResult { get; set; }

        /// <summary>
        /// Get or Set the name of the catalog file.
        /// </summary>
        internal string CatalogName { get; set; }

        /// <summary>
        /// Get or Set the MD5 hash of the catalog file.
        /// </summary>
        internal string Hash { get; set; }

        /// <summary>
        /// Get or Set the date when the catalog file have been downloaded for the last time.
        /// </summary>
        internal DateTime LastDownloadDate { get; set; }

        /// <summary>
        /// Get or Set if an update is available.
        /// </summary>
        internal bool IsUpdateAvailable { get; set; }

        /// <summary>
        /// List of deleted updates since the last publication of this catalog.
        /// </summary>
        internal List<CatalogUpdate> DeletedUpdates
        {
            get { return _deleteUpdates; }
        }

        /// <summary>
        /// List of added updates since the last publication of this catalog.
        /// </summary>
        internal List<CatalogUpdate> AddedUpdates
        {
            get { return _addedUpdates; }
        }

        internal bool Closing { get; set; }

        #endregion {internal Properties - Propriétés internes}

        #region {internal Methods - Méthodes internes}

        internal bool IsTimeToCheck()
        {
            DateTime today = DateTime.Now.Date;
            DateTime nextTimeChecking = LastCheckDate.Date;

            switch (Unit)
            {
                case CheckingUnits.Days:
                    nextTimeChecking = nextTimeChecking.AddDays(CheckEvery);
                    break;
                case CheckingUnits.Weeks:
                    nextTimeChecking = nextTimeChecking.AddDays(CheckEvery * 7);
                    break;
                case CheckingUnits.Months:
                    nextTimeChecking = nextTimeChecking.AddMonths(CheckEvery);
                    break;
                default:
                    break;
            }
            return (today.CompareTo(nextTimeChecking) >= 0);
        }

        /// <summary>
        /// Check if an updated catalog is available on Internet.
        /// </summary>
        /// <returns>Retrun true if an update is available, otherwise false (can mean that something wrong happened).</returns>
        internal bool CheckUpdateAvailability()
        {
            Logger.EnteringMethod(this.Address + "/" + this.CatalogName);

            this.LastCheckDate = DateTime.Now.Date;
            string addressToCheck = this.Address.ToLower();

            try
            {
                DirectoryInfo destinationFolder = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder);
                FileInfo tempOldCatalog = new FileInfo(Tools.Utilities.GetTempFolder() + this.CatalogName);

                if (File.Exists(destinationFolder.FullName + "\\" + this.CatalogName))
                {
                    File.Copy(destinationFolder.FullName + "\\" + this.CatalogName, tempOldCatalog.FullName);
                }
                DownloadFile(destinationFolder.FullName + "\\" + this.CatalogName);
                string newHash = GetFileHash(destinationFolder.FullName + "\\" + this.CatalogName);
                this.LastCheckResult = true;
                bool newCatalogAvailable = IsNewer(this.Hash, newHash);
                if (newCatalogAvailable)
                {
                    CatalogHelper oldCatalog = new CatalogHelper(tempOldCatalog.FullName);
                    System.Threading.Thread openOldCatalogThread = new System.Threading.Thread(new System.Threading.ThreadStart(() => { oldCatalog.OpenCatalog(); }));
                    if (!Closing)
                        openOldCatalogThread.Start();

                    CatalogHelper newCatalog = new CatalogHelper(destinationFolder.FullName + "\\" + this.CatalogName);
                    System.Threading.Thread openNewCatalogThread = new System.Threading.Thread(new System.Threading.ThreadStart(() => { newCatalog.OpenCatalog(); }));
                    if (!Closing)
                        openNewCatalogThread.Start();

                    if (openOldCatalogThread.ThreadState == System.Threading.ThreadState.Running)
                        openOldCatalogThread.Join();
                    if (openNewCatalogThread.ThreadState == System.Threading.ThreadState.Running)
                        openNewCatalogThread.Join();

                    CompareCatalogs(oldCatalog, newCatalog);
                }
                this.Hash = newHash;
                LastDownloadDate = DateTime.Now.Date;
                IsUpdateAvailable = newCatalogAvailable;

                Tools.Utilities.DeleteFolder(tempOldCatalog.DirectoryName);
                return newCatalogAvailable;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                this.LastCheckResult = false;
            }
            return false;
        }

        /// <summary>
        /// Test if the file is reachable.
        /// </summary>
        /// <returns>True if the file is reachable, otherwise false.</returns>
        internal bool TestConnectivity()
        {
            Logger.EnteringMethod(this.Address + "/" + this.CatalogName);

            string addressToCheck = this.Address.ToLower();

            try
            {
                DirectoryInfo destinationFolder = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder);
                DownloadFile(destinationFolder.FullName + "\\" + this.CatalogName);
                string newHash = GetFileHash(destinationFolder.FullName + "\\" + this.CatalogName);
                this.Hash = newHash;
                LastDownloadDate = DateTime.Now.Date;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Save this catalog to a file for the next time where WPP start.
        /// </summary>
        internal void Save(string baseFolder)
        {
            Logger.EnteringMethod(baseFolder);

            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            System.Xml.XmlElement rootElement = (System.Xml.XmlElement)xmlDoc.AppendChild(xmlDoc.CreateElement("CatalogSubscription"));

            rootElement.AppendChild(xmlDoc.CreateElement("IsActive")).InnerText = IsActive.ToString();
            rootElement.AppendChild(xmlDoc.CreateElement("Address")).InnerText = Address;
            rootElement.AppendChild(xmlDoc.CreateElement("CheckEvery")).InnerText = CheckEvery.ToString();
            rootElement.AppendChild(xmlDoc.CreateElement("Unit")).InnerText = Unit.ToString();
            rootElement.AppendChild(xmlDoc.CreateElement("LastCheck")).InnerText = LastCheckDate.ToString();
            rootElement.AppendChild(xmlDoc.CreateElement("LastCheckResult")).InnerText = LastCheckResult.ToString();
            rootElement.AppendChild(xmlDoc.CreateElement("CatalogName")).InnerText = CatalogName;
            rootElement.AppendChild(xmlDoc.CreateElement("Hash")).InnerText = Hash;
            rootElement.AppendChild(xmlDoc.CreateElement("LastDownloadDate")).InnerText = LastDownloadDate.ToString();

            FileInfo catalogFile = new FileInfo(baseFolder + "\\" + this.CatalogName + ".xml");
            if (!catalogFile.Directory.Exists)
                catalogFile.Directory.Create();

            try
            {
                xmlDoc.Save(catalogFile.FullName);
            }
            catch (Exception ex)
            {
                Logger.Write("**** Error when saving " + catalogFile.FullName + ".\r\n" + ex.Message);

            }
        }

        /// <summary>
        /// Delete the file corresponding to this subscription.
        /// </summary>
        /// <param name="baseFolder">Folder where all Directory's Subscription are.</param>
        internal void DeleteFromDisk(string baseFolder)
        {
            Logger.EnteringMethod(baseFolder);

            try
            {
                if (File.Exists(baseFolder + "\\MetaData\\" + this.CatalogName + ".xml"))
                    File.Delete(baseFolder + "\\MetaData\\" + this.CatalogName + ".xml");
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            try
            {
                if (File.Exists(baseFolder + "\\" + this.CatalogName))
                    File.Delete(baseFolder + "\\" + this.CatalogName);
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }
        }

        /// <summary>
        /// Download the file from the specified url and save it to the disk to the specified path.
        /// </summary>
        /// <param name="destinationFilePath">Fullpath to the file where to save the downloaded file.</param>
        internal void DownloadFile(string destinationFilePath)
        {
            Logger.EnteringMethod(this.Address + "/" + this.CatalogName + " => " + destinationFilePath);

            FileInfo destinationFile = new FileInfo(destinationFilePath);
            if (!destinationFile.Directory.Exists)
                destinationFile.Directory.Create();
            string remoteAddress = this.Address.ToLower().Substring(0, 5);
            switch (remoteAddress)
            {
                case "http:":
                case "https":
                    DownloadFileFromHTTP(destinationFilePath);
                    break;
                case "ftp:/":
                case "ftps:":
                    DownloadFileFromFTP(destinationFilePath);
                    break;
                default:
                    throw new Exception("Unknown protocol.");
            }
        }

        #endregion {internal Methods - Méthodes internes}

        #region {privates Methods - Méthodes privées}

        private void DownloadFileFromHTTP(string destinationFilePath)
        {
            Logger.Write("Will try to download the file from HTTP site to " + destinationFilePath);
            System.Net.WebClient webClient = new System.Net.WebClient();
            webClient.Proxy = Tools.Utilities.GetHTTPProxy();

            webClient.DownloadFile(this.Address + "/" + this.CatalogName, destinationFilePath);
            webClient.Dispose();
            webClient = null;
            Logger.Write("Download finished");
        }

        private void DownloadFileFromFTP(string destinationFilePath)
        {
            Logger.Write("Will try to download the file from FTP site to " + destinationFilePath);

            FtpClient ftpClient = new FtpClient(this.Address, "", "");
            ftpClient.Download(this.CatalogName, destinationFilePath);

            Logger.Write("Download finished");
        }

        /// <summary>
        /// Compute the Hash of the specified file.
        /// </summary>
        /// <param name="fileNameToCheck">Name of the file.</param>
        /// <returns>The Hash of the file.</returns>
        private string GetFileHash(string fileNameToCheck)
        {
            Logger.EnteringMethod(fileNameToCheck);

            if (File.Exists(fileNameToCheck))
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                FileStream fileStream = new FileStream(fileNameToCheck, FileMode.Open);
                byte[] hash = md5.ComputeHash(fileStream);
                fileStream.Close();

                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            Logger.Write("**** Unable to read the file");
            return this.Hash;
        }

        /// <summary>
        /// Get the Creation Date of the specified file at the specified address. Can throw WebException if the file is unreachable.
        /// </summary>
        /// <param name="addressToCheck">Url where the file is.</param>
        /// <param name="fileNameToCheck">Name of the file to get the date.</param>
        /// <returns>Date of the creation of the file.</returns>
        private DateTime GetRemoteFileCreationDateFromFtp(string addressToCheck, string fileNameToCheck)
        {
            Logger.EnteringMethod(addressToCheck + "/" + fileNameToCheck);

            FtpClient clientFTP = new FtpClient(addressToCheck, string.Empty, string.Empty);
            return clientFTP.GetFileCreatedDateTime(fileNameToCheck);
        }

        private bool IsNewer(DateTime oldDate, DateTime newDate)
        {
            return (oldDate.CompareTo(newDate) < 0);
        }

        private bool IsNewer(string oldHash, string newHash)
        {
            return (oldHash.CompareTo(newHash) != 0);
        }

        private void CompareCatalogs(CatalogHelper oldCatalog, CatalogHelper newCatalog)
        {
            _deleteUpdates.Clear();
            _addedUpdates.Clear();
            bool found = false;

            foreach (CatalogVendor oldVendor in oldCatalog.Vendors.Values)
            {
                foreach (CatalogProduct oldProduct in oldVendor.Products.Values)
                {
                    foreach (CatalogUpdate oldUpdate in oldProduct.Updates)
                    {
                        foreach (CatalogVendor newVendor in newCatalog.Vendors.Values)
                        {
                            foreach (CatalogProduct newProduct in newVendor.Products.Values)
                            {
                                int i = 0;
                                found = false;
                                while (i < newProduct.Updates.Count)
                                {
                                    if (oldUpdate.PackageId.ToLower() == newProduct.Updates[i].PackageId.ToLower())
                                    {
                                        found = true;
                                        newProduct.Updates.Remove(newProduct.Updates[i]);
                                        break;
                                    }
                                    i++;
                                }
                                if (found)
                                    break;
                            }
                            if (found)
                                break;
                        }
                        if (!found)
                        {
                            _deleteUpdates.Add(oldUpdate);
                        }
                    }
                }
            }

            foreach (CatalogVendor vendor in newCatalog.Vendors.Values)
            {
                foreach (CatalogProduct product in vendor.Products.Values)
                {
                    foreach (CatalogUpdate update in product.Updates)
                    {
                        _addedUpdates.Add(update);
                    }
                }
            }
        }

        #endregion {privates Methods - Méthodes privées}
    }
}
