using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Deployment.Compression;
using Microsoft.Deployment.Compression.Cab;
using Microsoft.UpdateServices.Administration;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Schema;
using System.IO;

namespace Wsus_Package_Publisher
{
    internal class CatalogHelper
    {
        internal enum SourceTypes
        {
            Unknown,
            Xml,
            Cab
        }

        private string _sourcePath = string.Empty;
        private string _destinationPath = string.Empty;
        private SourceTypes _sourceType = SourceTypes.Unknown;
        private Dictionary<string, CatalogVendor> _vendors = new Dictionary<string, CatalogVendor>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(CatalogHelper).Assembly);


        /// <summary>
        /// Constructor for this Class.
        /// </summary>
        /// <param name="catalogFilePath">Full path to the .cab or .xml file.</param>
        internal CatalogHelper(string catalogFilePath)
        {
            Logger.EnteringMethod("CatalogHelper : " + catalogFilePath);

            if (File.Exists(catalogFilePath))
            {
                FileInfo sourceFile = new FileInfo(catalogFilePath);
                if (sourceFile.Extension.ToLower() == ".xml")
                    SourceType = SourceTypes.Xml;
                else
                    if (sourceFile.Extension.ToLower() == ".cab")
                        SourceType = SourceTypes.Cab;
                    else
                        SourceType = SourceTypes.Unknown;
                SourcePath = catalogFilePath;
            }
        }

        #region (Internal Properties - Propriétées Internes)

        internal string SourcePath
        {
            get { return _sourcePath; }
            private set { _sourcePath = value; }
        }

        internal string DestinationPath
        {
            get { return _destinationPath; }
            private set { _destinationPath = value; }
        }

        internal SourceTypes SourceType
        {
            get { return _sourceType; }
            private set { _sourceType = value; }
        }

        internal Dictionary<string, CatalogVendor> Vendors { get { return _vendors; } private set { _vendors = value; } }

        #endregion (Internal Properties - Propriétées Internes)

        #region (Internal Methods - Méthodes Internes)

        internal void OpenCatalog()
        {
            Logger.EnteringMethod();

            DestinationPath = GetTempFolder();

            try
            {
                if (Directory.Exists(DestinationPath))
                    Directory.Delete(DestinationPath, true);
                Directory.CreateDirectory(DestinationPath);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                if (OpenCatalogFinished != null)
                    OpenCatalogFinished();
                return;
            }

            switch (SourceType)
            {
                case SourceTypes.Xml:
                    Logger.Write("Xml File.");
                    CopyFile(SourcePath, DestinationPath);
                    break;
                case SourceTypes.Cab:
                    Logger.Write("Cab File.");
                    if (!ExtractCabToFolder(SourcePath, DestinationPath))
                    {
                        if (OpenCatalogFinished != null)
                            OpenCatalogFinished();
                        return;
                    }
                    break;
                default:
                    Logger.Write("Unknown SourceType");
                    if (OpenCatalogFinished != null)
                        OpenCatalogFinished();
                    return;
            }
            string xmlFile = SearchXMLFile(DestinationPath);

            List<SoftwareDistributionPackage> extractedSDP = new List<SoftwareDistributionPackage>();

            if (File.Exists(xmlFile))
            {
                extractedSDP = ParseXMLFile(xmlFile);
                ParseSDP(extractedSDP);
            }
            else
            {
                Logger.Write("No XMLFile were found.");
                System.Windows.Forms.MessageBox.Show(resMan.GetString("NoXMLInThisCab"));
            }
                DeleteDestinationFolder();

            if (OpenCatalogFinished != null)
                OpenCatalogFinished();
        }

        #endregion (Internal Methods - Méthodes Internes)

        #region (Privates Methods - Méthodes Privées)

        /// <summary>
        /// Expand the %temp% Environnement Variable and add a random folder name (Guid) at the end.
        /// If %temp% can't be resolved, then 'C:\temp\' will be used instead.
        /// </summary>
        /// <returns>A path to a random folder with no ending '\'.</returns>
        private string GetTempFolder()
        {
            Logger.EnteringMethod();

            string result = @"C:\temp\";

            try
            {
                if (Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User).Contains("TempWPP"))
                    result = Environment.GetEnvironmentVariable("TempWPP") + "\\WPP\\";
                else
                    result = Environment.GetEnvironmentVariable("Temp") + "\\WPP\\";
            }
            catch (Exception) { }

            result += Guid.NewGuid();
            Logger.Write("Will return : " + result);
            return result;
        }

        /// <summary>
        /// Extract a .CAB file to the specified folder.
        /// </summary>
        /// <param name="cabFileToExtract">Full path to the .CAB file to extract.</param>
        /// <param name="destinationFolder">Full path to the folder where extract the .cab file.</param>
        /// <returns>True if the extraction succeeded, false otherwise.</returns>
        private bool ExtractCabToFolder(string cabFileToExtract, string destinationFolder)
        {
            Logger.EnteringMethod(cabFileToExtract + " To " + destinationFolder);
            try
            {
                EventHandler<ArchiveProgressEventArgs> unpackArchiveProgression = new EventHandler<ArchiveProgressEventArgs>(UnpackArchiveProgress);
                CabInfo unpacker = new CabInfo(cabFileToExtract);
                unpacker.Unpack(destinationFolder, unpackArchiveProgression);
                Logger.Write("Successfuly extract CAB file.");
                if (UnpackArchiveFinished != null)
                    UnpackArchiveFinished();
                return true;
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            return false;
        }

        /// <summary>
        /// Copy a file to a folder.
        /// </summary>
        /// <param name="sourceFilePath">Full path to the file which need to be copied.</param>
        /// <param name="destinationFolder">Full path to the folder where to copy the file.</param>
        private void CopyFile(string sourceFilePath, string destinationFolder)
        {
            if (File.Exists(sourceFilePath))
                File.Copy(sourceFilePath, destinationFolder + "\\" + new FileInfo(sourceFilePath).Name);
        }

        /// <summary>
        /// Search for a XML file in the specified folder. Return the first found XML file.
        /// </summary>
        /// <param name="folderToSearchInto">Folder where to search the XML file.</param>
        /// <returns>If found, return full path of the found XML file. Otherwise, return String.Empty.</returns>
        private string SearchXMLFile(string folderToSearchInto)
        {
            System.IO.DirectoryInfo folder = new System.IO.DirectoryInfo(folderToSearchInto);
            foreach (System.IO.FileInfo file in folder.GetFiles())
            {
                if (file.Extension.ToLower() == ".xml")
                    return file.FullName;
            }

            return string.Empty;
        }

        /// <summary>
        /// Parse a XML file and create needed SoftwareDistrubutionPackages.
        /// </summary>
        /// <param name="xmlFilePath">Full path to the XML file.</param>
        private List<SoftwareDistributionPackage> ParseXMLFile(string xmlFilePath)
        {
            Logger.EnteringMethod(xmlFilePath);

            List<SoftwareDistributionPackage> extractedSDP = new List<SoftwareDistributionPackage>();
            XmlNamespaceManager namespaceMgr = new XmlNamespaceManager(new NameTable());
            int percentProgress = 0;

            namespaceMgr.AddNamespace("", "http://www.w3.org/2001/XMLSchema");
            namespaceMgr.AddNamespace("smc", "http://schemas.microsoft.com/sms/2005/04/CorporatePublishing/SystemsManagementCatalog.xsd");

            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFilePath);
                XmlNodeList selectedNodes = xmlDoc.SelectNodes("smc:SystemsManagementCatalog/smc:SoftwareDistributionPackage", namespaceMgr);
                int nodeCount = selectedNodes.Count;
                int currentNode = 1;
                List<XmlNode> nodes = new List<XmlNode>();

                foreach (XmlNode node in selectedNodes)
                {
                    nodes.Add(node);
                }

                System.Threading.Tasks.Parallel.ForEach<XmlNode>(nodes, node =>
                    {
                        try
                        {
                            SoftwareDistributionPackage sdp = new SoftwareDistributionPackage(node.CreateNavigator());
                            extractedSDP.Add(sdp);
                        }
                        catch (Exception) { }
                        percentProgress = (int)(currentNode * 100 / nodeCount);
                        if (OpenCatalogProgression != null)
                            OpenCatalogProgression(percentProgress);
                        currentNode++;
                    });
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            Logger.Write("Will return " + extractedSDP.Count + " SDP.");
            return extractedSDP;
        }

        private void ParseSDP(List<SoftwareDistributionPackage> extractedSDP)
        {
            Logger.EnteringMethod(extractedSDP.Count.ToString());

            foreach (SoftwareDistributionPackage sdp in extractedSDP)
            {
                if (sdp != null)
                {
                    CatalogUpdate newUpdate = new CatalogUpdate(sdp);
                    CatalogVendor newVendor;
                    if (Vendors.ContainsKey(sdp.VendorName))
                        newVendor = Vendors[sdp.VendorName];
                    else
                    {
                        newVendor = new CatalogVendor(sdp.VendorName);
                        Vendors.Add(sdp.VendorName, newVendor);
                    }
                    CatalogProduct newProduct;
                    if (newVendor.Products.ContainsKey(sdp.ProductNames[0]))
                        newProduct = newVendor.Products[sdp.ProductNames[0]];
                    else
                    {
                        newProduct = new CatalogProduct(sdp.ProductNames[0]);
                        newVendor.AddProduct(newProduct);
                    }
                    newProduct.AddUpdate(newUpdate);
                }
            }
        }

        private void UnpackArchiveProgress(object sender, ArchiveProgressEventArgs args)
        {
            if (UnpackArchiveProgression != null)
            {
                UnpackArchiveProgression((int)(args.FileBytesProcessed * 100 / args.TotalFileBytes));
            }
        }

        private void DeleteDestinationFolder()
        {
            Logger.EnteringMethod();

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    try
                    {
                        if (Directory.Exists(DestinationPath))
                            Directory.Delete(DestinationPath, true);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                    }
                }));
            t.Start();
        }

        #endregion (Privates Methods - Méthodes Privées)

        #region (Public Event - événement publique)

        public delegate void UnpackArchiveProgressionEventHandler(int percentProgression);
        public event UnpackArchiveProgressionEventHandler UnpackArchiveProgression;

        public delegate void UnpackArchiveFinishedEventHandler();
        public event UnpackArchiveFinishedEventHandler UnpackArchiveFinished;

        public delegate void OpenCatalogProgressionEventHandler(int percentProgression);
        public event OpenCatalogProgressionEventHandler OpenCatalogProgression;

        public delegate void OpenCatalogFinishedEventHandler();
        public event OpenCatalogFinishedEventHandler OpenCatalogFinished;

        #endregion (Public Event - événement publique)
    }
}