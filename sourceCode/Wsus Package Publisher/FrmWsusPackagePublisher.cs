using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;


namespace Wsus_Package_Publisher
{
    public partial class FrmWsusPackagePublisher : Form
    {
        private WsusWrapper wsus = WsusWrapper.GetInstance();
        private ComputerGroup computerGroups;
        private Dictionary<string, Guid> computerGroupGuidConverter = new Dictionary<string, Guid>();
        private Dictionary<Guid, Company> companies = new Dictionary<Guid, Company>();
        private UpdateControl updateCtrl;
        private ComputerControl computerCtrl = null;
        private TreeNode serverNode;
        private TreeNode allComputersNode;
        private TreeNode allUpdatesNode;
        private TreeNode allMetaGroupsNode;
        private System.Threading.Thread _waitingThread;
        private FrmWaiting _waitingForm;
        private List<WsusServer> serverList = new List<WsusServer>();
        private WsusServer currentWsusServer = null;
        private FrmSettings settings;
        private Guid microsoftCorporationID = new Guid("56309036-4c77-4dd9-951a-99ee9c246a94");
        private Guid locallyPublishedProduct = new Guid("5cc25303-143f-40f3-a2ff-803a1db69955");
        private Guid locallyPublishedVendor = new Guid("7c40e8c2-01ae-47f5-9af2-6e75a0582518");
        internal bool restart = false;
        private ToolStripMenuItem connectButton = new ToolStripMenuItem();
        FrmCatalogSubscription frmCatalogSubscription;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmWsusPackagePublisher).Assembly);

        internal FrmWsusPackagePublisher()
        {
            bool upgradeSettings = false;
            //string commonAppData = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (Properties.Settings.Default.UpgradeRequired)
            {
                upgradeSettings = true;
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
            if (Properties.Settings.Default.AppID.Equals(Guid.Empty))
            {
                Properties.Settings.Default.AppID = Guid.NewGuid();
                Properties.Settings.Default.Save();
            }
            System.IO.FileInfo logFile = new System.IO.FileInfo(Environment.ExpandEnvironmentVariables("%Temp%") + "\\WPP-" + Properties.Settings.Default.AppID.ToString() + ".log");
            if (logFile.Exists)
                logFile.Delete();
            Logger.Initialize(logFile.DirectoryName, logFile.Name, Logger.Destination.File);
            Logger.Write("=================================================================================================================");
            Logger.Write("Starting. Release : " + Application.ProductVersion.ToString());
            Logger.Write("Upgrade Settings : " + upgradeSettings.ToString());

            System.Globalization.CultureInfo OSCulture = System.Globalization.CultureInfo.InstalledUICulture;
            string OSLanguage = OSCulture.Parent.TwoLetterISOLanguageName.ToLower();
            Logger.Write("Windows was installed in : " + OSLanguage + " (" + OSCulture.Parent.EnglishName + ")");

            string lng = Properties.Settings.Default.Language;
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lng);
            settings = new FrmSettings();
            InitializeComponent();
            switch (lng)
            {
                case "fr":
                    Logger.Write("Language is : Français.");
                    frenchtoolStripMenuItem.Checked = true;
                    englishtoolStripMenuItem.Checked = false;
                    germanToolStripMenuItem.Checked = false;
                    russianToolStripMenuItem.Checked = false;
                    break;
                case "en":
                    Logger.Write("Language is : Anglais.");
                    frenchtoolStripMenuItem.Checked = false;
                    englishtoolStripMenuItem.Checked = true;
                    germanToolStripMenuItem.Checked = false;
                    russianToolStripMenuItem.Checked = false;
                    break;
                case "de":
                    Logger.Write("Language is : Allemand.");
                    frenchtoolStripMenuItem.Checked = false;
                    englishtoolStripMenuItem.Checked = false;
                    germanToolStripMenuItem.Checked = true;
                    russianToolStripMenuItem.Checked = false;
                    break;
                case "ru":
                    Logger.Write("Language is : Russe.");
                    frenchtoolStripMenuItem.Checked = false;
                    englishtoolStripMenuItem.Checked = false;
                    germanToolStripMenuItem.Checked = false;
                    russianToolStripMenuItem.Checked = true;
                    break;
                default:
                    Logger.Write("**** Language is unknown.");
                    break;
            }
            frmCatalogSubscription = new FrmCatalogSubscription();
            updateCtrl = new UpdateControl();
            ClearBeforeConnecting();
            connectButton.Image = Properties.Resources.Lightning;
            connectButton.Text = resMan.GetString("ConnectOrReload");
            connectButton.TextAlign = ContentAlignment.MiddleCenter;
            connectButton.ForeColor = Color.White;
            connectButton.BackColor = Color.LightSlateGray;
            connectButton.Click += new EventHandler(ConnectToServer);
            connectButton.MouseEnter += new EventHandler(buttonItem_MouseEnter);
            mnuMainForm.Items.Add(connectButton);
            wsus.UpdatePublished += new WsusWrapper.UpdatePublisedEventHandler(UpdatePublished);
            wsus.UpdateRevised += new WsusWrapper.UpdateRevisedEventHandler(UpdateRevised);
            wsus.UpdateSuperseded += new WsusWrapper.UpdateSupersededEventHandler(wsus_UpdateSuperseded);
            wsus.UpdateDeleted += new WsusWrapper.UpdateDeletedEventHandler(UpdateDeleted);
            imgLstServer.Images.Add(Properties.Resources.UpStream);
            imgLstServer.Images.Add(Properties.Resources.DownStream);
        }
                
        private void ConnectToServer(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            IComputerTargetGroup allComputerTargetGroup;
            (sender as ToolStripMenuItem).Enabled = false;
            ClearBeforeConnecting();
            if (cmbBxServerList.SelectedItem == null)
            {
                MessageBox.Show(resMan.GetString("SelectaServerFirst"));
                FillServerList();
                (sender as ToolStripMenuItem).Enabled = true;
                return;
            }
            WsusServer serverWsus = (WsusServer)cmbBxServerList.SelectedItem;
            Logger.Write("Try connecting to : " + serverWsus.Name);
            StartWaitingForm(resMan.GetString("connecting"));
            if (wsus.Connect(serverWsus, Properties.Settings.Default.Language))
            {
                Logger.Write("Successfuly connected to Wsus server.");

                if (Properties.Settings.Default.ConnectToLastUsedServer)
                {
                    Properties.Settings.Default.LastUsedServerName = serverWsus.Name;
                    Properties.Settings.Default.Save();
                }
                trvWsus.SuspendLayout();
                string rootComputerGroupName;
                Guid rootComputerGroupId;
                currentWsusServer = serverWsus;

                if (wsus.IsReplica)
                {
                    Logger.Write("This is a Replica server.");
                    serverNode = new TreeNode(serverWsus.Name + " (" + resMan.GetString("ReplicaServer") + ")");
                }
                else
                {
                    Logger.Write("This is not a Replica server.");
                    serverNode = new TreeNode(serverWsus.Name);
                }
                serverNode.Tag = "Server:";
                if (wsus.StreamType == WsusWrapper.StreamTypeServer.UpStream)
                    serverNode.StateImageIndex = 0;
                else
                    serverNode.StateImageIndex = 1;
                trvWsus.Nodes.Add(serverNode);
                allComputerTargetGroup = wsus.GetAllComputerTargetGroup();
                rootComputerGroupName = allComputerTargetGroup.Name;
                rootComputerGroupId = allComputerTargetGroup.Id;
                computerGroupGuidConverter.Add(rootComputerGroupName, rootComputerGroupId);

                allComputersNode = new TreeNode(rootComputerGroupName);
                allComputersNode.Tag = "ComputerGroup:";
                serverNode.Nodes.Add(allComputersNode);

                PopulateTreeviewWithComputerGroups(allComputerTargetGroup, allComputersNode);
                trvWsus.Sort();
                computerGroups = new ComputerGroup(rootComputerGroupName, rootComputerGroupId, rootComputerGroupName);
                PopulateComputerGroups(allComputersNode, computerGroups, 3);

                if (!wsus.IsReplica)
                {
                    FillMetaGroups();
                    allMetaGroupsNode = new TreeNode(resMan.GetString("MetaGroup"));
                    allMetaGroupsNode.Tag = "MetaGroupRoot:";
                    PopulateMetaGroupNode();
                    serverNode.Nodes.Add(allMetaGroupsNode);
                }

                allUpdatesNode = new TreeNode(resMan.GetString("updates"));
                allUpdatesNode.Tag = "Updates:";
                serverNode.Nodes.Add(allUpdatesNode);
                CollectUpdates();
                serverNode.Expand();
                allUpdatesNode.Expand();
                updateCtrl.SetComputerGroups(computerGroups, allComputersNode);
                updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                if (wsus.IsReplica)
                {
                    createUpdatetoolStripMenuItem.Enabled = false;
                    certificatetoolStripMenuItem.Enabled = false;
                    importAnUpdateToolStripMenuItem.Enabled = false;
                    importFromCatalogtoolStripMenuItem.Enabled = false;
                    editMaxCabFileToolStripMenuItem.Enabled = false;
                    manageCatalogSubscriptionsToolStripMenuItem.Enabled = false;
                    updateCtrl.LockFunctionnalities(true);
                }
                else
                {
                    createUpdatetoolStripMenuItem.Enabled = true;
                    createCustomUpdateToolStripMenuItem.Enabled = true;
                    certificatetoolStripMenuItem.Enabled = true;
                    importAnUpdateToolStripMenuItem.Enabled = true;
                    importFromCatalogtoolStripMenuItem.Enabled = true;
                    editMaxCabFileToolStripMenuItem.Enabled = true;
                    updateCtrl.LockFunctionnalities(false);
                    trvWsus.AllowDrop = true;

                    System.Threading.Thread catalogUpdateSearcher = new System.Threading.Thread(new System.Threading.ThreadStart(frmCatalogSubscription.CheckSubscriptions));
                    frmCatalogSubscription.CheckingSubscriptionTerminated += new FrmCatalogSubscription.CheckingSubscriptionTerminatedDelegate(frmCatalogSubscription_CheckingSubscriptionTerminated);
                    catalogUpdateSearcher.Start();
                }
                checkAgainstActiveDirectoryToolStripMenuItem.Enabled = !string.IsNullOrEmpty(ADHelper.GetDomainName());
                StopWaitingForm();
                _waitingForm.TopLevel = false;
                _waitingForm.Hide();

                Version serverVersion = wsus.GetServerVersion();
                Version consoleVersion = wsus.ConsoleVersion;

                Logger.Write("Server Version is : " + serverVersion.ToString());
                Logger.Write("Console Version is : " + consoleVersion.ToString());
                Logger.Write("Local OS is : " + Environment.OSVersion.VersionString);

                if (!wsus.IsConsoleVersionAllowPublication() && !wsus.IsReplica)
                {
                    MessageBox.Show(resMan.GetString("ServerAndConsoleVersionMismatch"));
                }

                switch (wsus.GetCertificateStatus)
                {
                    case CertificateHelper.CertificateStatus.Absent:
                        MessageBox.Show(resMan.GetString("CertificateAbstent"));
                        break;
                    case CertificateHelper.CertificateStatus.NearExpiration:
                        MessageBox.Show(resMan.GetString("CertificateNearExpiration"));
                        break;
                    case CertificateHelper.CertificateStatus.Expired:
                        MessageBox.Show(resMan.GetString("CertificateExpired"));
                        break;
                    case CertificateHelper.CertificateStatus.NotYetValid:
                        MessageBox.Show(resMan.GetString("CertificateNotYetValid"));
                        break;
                    case CertificateHelper.CertificateStatus.Invalid:
                        MessageBox.Show(resMan.GetString("CertificateInvalid"));
                        break;
                    default:
                        break;
                }
                trvWsus.ResumeLayout();
            }
            else
            {
                checkAgainstActiveDirectoryToolStripMenuItem.Enabled = false;
                manageCatalogSubscriptionsToolStripMenuItem.Enabled = false;
                StopWaitingForm();
                _waitingForm.TopLevel = false;
                _waitingForm.Hide();
                MessageBox.Show(this, resMan.GetString("FailedToConnectToServer"));
                (sender as ToolStripMenuItem).Enabled = true;
                return;
            }
            (sender as ToolStripMenuItem).Enabled = true;
        }

        private void PopulateMetaGroupNode()
        {
            Logger.EnteringMethod();
            allMetaGroupsNode.Nodes.Clear();
            foreach (MetaGroup metaGroupToAdd in currentWsusServer.MetaGroups)
            {
                Logger.Write("Adding : " + metaGroupToAdd.Name);
                TreeNode nodeToAdd = new TreeNode(metaGroupToAdd.Name);
                nodeToAdd.Tag = "MetaGroup:";
                allMetaGroupsNode.Nodes.Add(nodeToAdd);
            }
        }

        private void ClearBeforeConnecting()
        {
            Logger.EnteringMethod();
            trvWsus.CollapseAll();
            trvWsus.AllowDrop = false;
            computerGroups = null;
            computerGroupGuidConverter.Clear();
            companies.Clear();
            splitContainer1.Panel2.Controls.Clear();
            if (computerCtrl != null)
                computerCtrl.Dispose();
            computerCtrl = null;
            updateCtrl.ClearDisplay();
            updateCtrl.Dock = DockStyle.Fill;
            computerCtrl = new ComputerControl();
            computerCtrl.Dock = DockStyle.Fill;
            serverNode = null;
            allComputersNode = null;
            allUpdatesNode = null;
            allMetaGroupsNode = null;
            trvWsus.Nodes.Clear();
            currentWsusServer = null;
        }

        private void StartWaitingForm(string description)
        {
            Logger.EnteringMethod();
            _waitingForm = new FrmWaiting();
            _waitingForm.Description = description;
            _waitingForm.GoOn = true;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            _waitingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_waitingForm.ShowForm));
            _waitingThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _waitingThread.Start();
            this.Refresh();
            System.Threading.Thread.Sleep(200);
        }

        private void StopWaitingForm()
        {
            Logger.EnteringMethod();
            _waitingForm.GoOn = false;
            _waitingThread.Join(900);
            _waitingThread = null;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
            this.Focus();
        }

        private void CollectUpdates()
        {
            Logger.EnteringMethod();
            bool showNonLocallyPublishedUpdates = Properties.Settings.Default.ShowNonLocallyPublishedUpdates;

            UpdateCategoryCollection allCategories = wsus.GetAllCategories();

            foreach (IUpdateCategory category in allCategories)
            {
                if (category.Type == UpdateCategoryType.Company)
                {
                    if (category.Id != microsoftCorporationID)
                        if (!companies.ContainsKey(category.Id) && (showNonLocallyPublishedUpdates || category.UpdateSource == UpdateSource.Other))
                            CreateNewCompany(category, showNonLocallyPublishedUpdates);
                }
            }
        }

        private void CreateNewCompany(IUpdateCategory vendor, bool showNonLocallyPublishedUpdates)
        {
            Logger.EnteringMethod();
            Company newCompanyInstance = new Company(vendor.Id, vendor.Title);

            Logger.Write("Adding : " + newCompanyInstance.CompanyName + " With ID : " + vendor.Id.ToString());
            companies.Add(vendor.Id, newCompanyInstance);
            TreeNode companyNode = new TreeNode(newCompanyInstance.CompanyName);
            companyNode.Tag = "Company:" + vendor.Id.ToString();
            companyNode.Name = newCompanyInstance.CompanyName;
            UpdateCategoryCollection subCategories = vendor.GetSubcategories();

            foreach (IUpdateCategory category in subCategories)
            {
                if (category.Type == UpdateCategoryType.ProductFamily)
                {
                    if (showNonLocallyPublishedUpdates || category.UpdateSource == UpdateSource.Other)
                        companyNode.Nodes.Add(GetProductFamilyNode(category, newCompanyInstance));
                }
                if (category.Type == UpdateCategoryType.Product)
                {
                    if (showNonLocallyPublishedUpdates || category.UpdateSource == UpdateSource.Other)
                        companyNode.Nodes.Add(GetProductNode(category, newCompanyInstance));
                }
            }
            companyNode.Text += " (" + companyNode.Nodes.Count + ")";

            allUpdatesNode.Nodes.Add(companyNode);
            trvWsus.Sort();
            trvWsus.Refresh();
            updateCtrl.Companies = companies;

            newCompanyInstance.NoMoreProductsForThisCompany += new Company.NoMoreProductsForThisCompanyEventHandler(CompanyRunOutofProducts);
            newCompanyInstance.ProductRemoved += new Company.ProductRemovedEventHandler(Company_ProductRemoved);
            newCompanyInstance.ProductAdded += new Company.ProductAddedEventHandler(Company_ProductAdded);
            newCompanyInstance.ProductRefreshed += new Company.ProductRefreshedEventHandler(Company_ProductRefreshed);
        }

        private TreeNode GetProductFamilyNode(IUpdateCategory category, Company company)
        {
            Logger.Write("Entering GetProductFamilyNode. For Company : " + company.CompanyName + " and Category : " + category.Title);
            TreeNode productFamilyNode = new TreeNode(category.Title);
            productFamilyNode.Tag = "ProductFamily:" + company.ID;

            foreach (IUpdateCategory product in category.GetSubcategories())
            {
                productFamilyNode.Nodes.Add(GetProductNode(product, company));
            }
            productFamilyNode.Text += " (" + productFamilyNode.Nodes.Count + ")";

            return productFamilyNode;
        }

        private TreeNode GetProductNode(IUpdateCategory category, Company company)
        {
            Logger.Write("Entering GetProductNode. For Company : " + company.CompanyName + " and Category : " + category.Title);
            bool showNonLocallyPublishedUpdates = Properties.Settings.Default.ShowNonLocallyPublishedUpdates;

            company.AddProduct(category.Id, category.Title);
            Product newProduct = company.Products[category.Id];
            TreeNode productNode = new TreeNode(category.Title);
            productNode.Tag = "Product:" + newProduct.ID.ToString();
            productNode.Name = newProduct.ProductName;

            foreach (IUpdate update in category.GetUpdates())
            {
                if (showNonLocallyPublishedUpdates || update.UpdateSource == UpdateSource.Other)
                {
                    Logger.Write("Adding update : " + update.Title);
                    newProduct.Updates.Add(update);
                }
            }
            newProduct.UpdateAdded += new Product.UpdateAddedEventHandler(product_UpdateAdded);
            productNode.Text += " (" + newProduct.Updates.Count + ")";

            return productNode;
        }

        private void CreateNewProduct(IUpdateCategory company, IUpdateCategory product)
        {
            Logger.EnteringMethod();
            Company vendor = companies[company.Id];
            vendor.AddProduct(product.Id, product.Title);
        }

        private void Company_ProductRefreshed(Company company, Product refreshedProduct)
        {
            Logger.EnteringMethod();
            updateCtrl.RefreshDisplay();
        }

        private void Company_ProductAdded(Company company, Product productAdded)
        {
            Logger.Write("Entering Company_ProductAdded. For Company : " + company.CompanyName + " And Product : " + productAdded.ProductName);
            TreeNode companyNode = allUpdatesNode.Nodes.Find(company.CompanyName, false)[0];

            TreeNode productNode = new TreeNode(productAdded.ProductName);
            productNode.Tag = "Product:" + productAdded.ID.ToString();
            productNode.Name = productAdded.ProductName;
            companyNode.Nodes.Add(productNode);
            trvWsus.Sort();
            productAdded.UpdateAdded += new Product.UpdateAddedEventHandler(product_UpdateAdded);
        }

        private void Company_ProductRemoved(Company company, Product productRemoved)
        {
            Logger.Write("Entering Company_ProductRemoved. For Comapny : " + company.CompanyName + " And product : " + productRemoved.ProductName);
            TreeNode companyNode = allUpdatesNode.Nodes.Find(company.CompanyName, false)[0];
            TreeNode productNode = companyNode.Nodes.Find(productRemoved.ProductName, false)[0];

            companyNode.Nodes.Remove(productNode);
            splitContainer1.Panel2.Controls.Clear();
            if (trvWsus.SelectedNode != null)
                trvWsus_AfterSelect(trvWsus, new TreeViewEventArgs(trvWsus.SelectedNode));
        }

        private void CompanyRunOutofProducts(Company companyWithoutProducts)
        {
            Logger.Write("Entering CompanyRunOutofProducts. For company : " + companyWithoutProducts.CompanyName);
            companies.Remove(companyWithoutProducts.ID);
            trvWsus.SuspendLayout();
            allUpdatesNode.Nodes.RemoveByKey(companyWithoutProducts.CompanyName);
            trvWsus.ResumeLayout();
        }

        private void product_UpdateAdded(Product updatedProduct, IUpdate addedUpdate)
        {
            Logger.Write("Entering product_UpdateAdded. For Product : " + updatedProduct.ProductName + " With update : " + addedUpdate.Title);
            if (updateCtrl.Product == updatedProduct)
                updateCtrl.RefreshDisplay();
        }

        private void EmptyProductDeleted(EmptyProductDeleter.EmptyProductDeleterResult deletionResult)
        {
            Logger.EnteringMethod();
            if (deletionResult.DeletedProduct != null)
            {
                Logger.Write("DeletedProduct : " + deletionResult.DeletedProduct.Title);
                DeleteNode("Product:" + deletionResult.DeletedProduct.Id.ToString(), deletionResult.DeletedProduct.Title);
            }
            if (deletionResult.DeletedVendor != null)
            {
                Logger.Write("DeletedVendor : " + deletionResult.DeletedVendor.Title);
                DeleteNode("Company:" + deletionResult.DeletedVendor.Id.ToString(), deletionResult.DeletedVendor.Title);
            }
            if (trvWsus.SelectedNode != null)
                trvWsus_AfterSelect(trvWsus, new TreeViewEventArgs(trvWsus.SelectedNode));
        }

        private void DeleteNode(string label, string title)
        {
            Logger.Write("Entering DeleteNode. With Title : " + title + " And label : " + label);
            TreeNode nodeToDelete = null;

            foreach (TreeNode node in allUpdatesNode.Nodes.Find(title, true))
            {
                if (node.Tag.ToString() == label)
                {
                    nodeToDelete = node;
                    break;
                }
            }
            if (nodeToDelete != null)
                allUpdatesNode.Nodes.Remove(nodeToDelete);
        }

        private void PopulateTreeviewWithComputerGroups(IComputerTargetGroup group, TreeNode node)
        {
            Logger.Write("Entering PopulateTreeviewWithComputerGroups. With group : " + group.Name + " And node : " + node.Text);
            foreach (IComputerTargetGroup childGroup in wsus.GetChildComputerTargetGroupNameAndId(group.Id))
            {
                TreeNode newNode = new TreeNode(childGroup.Name);
                computerGroupGuidConverter.Add(childGroup.Name, childGroup.Id);
                newNode.Tag = "ComputerGroup:";
                node.Nodes.Add(newNode);
                PopulateTreeviewWithComputerGroups(childGroup, newNode);
            }
        }

        private void PopulateComputerGroups(TreeNode node, ComputerGroup computersGroup, int indent)
        {
            Logger.Write("Entering PopulateComputerGroups. With node : " + node.Text + " And computersGroup : " + computersGroup.Name);
            foreach (TreeNode childNode in node.Nodes)
            {
                string tab = new string(' ', indent);
                ComputerGroup newComputerGroup = new ComputerGroup(childNode.Text, computerGroupGuidConverter[childNode.Text], tab + childNode.Text);

                computersGroup.InnerComputerGroup.Add(newComputerGroup);
                PopulateComputerGroups(childNode, newComputerGroup, indent + 3);
            }
        }

        private void BlankContextMenu()
        {
            Logger.EnteringMethod();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            ctxMnuTreeview.Items.Clear();
            trvWsus.ContextMenu = null;
        }

        private void InitializeContextMenuForComputerGroup()
        {
            Logger.EnteringMethod();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            ctxMnuTreeview.Items.Clear();
            ctxMnuTreeview.Items.Add(GetItem("SendDetectNow"));
            ctxMnuTreeview.Items.Add(GetItem("SendReportNow"));
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForCompany()
        {
            Logger.EnteringMethod();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            ctxMnuTreeview.Items.Clear();
            if (!wsus.IsReplica)
            {
                ToolStripItem createUpdateItem = GetItem("CreateUpdate");
                ctxMnuTreeview.Items.Add(createUpdateItem);
                createUpdateItem.Enabled = wsus.IsConsoleVersionAllowPublication();
                ToolStripItem createCustomUpdateItem = GetItem("CreateCustomUpdate");
                ctxMnuTreeview.Items.Add(createCustomUpdateItem);
                createCustomUpdateItem.Enabled = wsus.IsConsoleVersionAllowPublication();
            }
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForProduct(bool allowDeleteProduct)
        {
            Logger.Write("Entering InitializeContextMenuForProduct. With allowDeleteProduct = " + allowDeleteProduct.ToString());
            ctxMnuTreeview.Items.Clear();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            if (!wsus.IsReplica)
            {
                exportAnUpdateToolStripMenuItem.Enabled = true;
                ToolStripItem createUpdateItem = GetItem("CreateUpdate");
                ctxMnuTreeview.Items.Add(createUpdateItem);
                createUpdateItem.Enabled = wsus.IsConsoleVersionAllowPublication();
                ToolStripItem createCustomUpdateItem = GetItem("CreateCustomUpdate");
                ctxMnuTreeview.Items.Add(createCustomUpdateItem);
                createCustomUpdateItem.Enabled = wsus.IsConsoleVersionAllowPublication();
                ctxMnuTreeview.Items.Add(new ToolStripSeparator());
                Guid currentGuid = updateCtrl.Product.Vendor.ID;
                ToolStripMenuItem deleteProductItem = GetItem("DeleteProduct");
                deleteProductItem.Enabled = wsus.IsLocal && allowDeleteProduct && !currentGuid.Equals(microsoftCorporationID) && !currentGuid.Equals(locallyPublishedVendor) && (wsus.CurrentServer.VisibleInWsusConsole != FrmSettings.MakeVisibleInWsusPolicy.Never);
                ctxMnuTreeview.Items.Add(deleteProductItem);
            }
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForMetaGroup(bool isRoot)
        {
            Logger.Write("Entering InitializeContextMenuForMetaGroup. With isRoot : " + isRoot.ToString());
            ctxMnuTreeview.Items.Clear();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            if (!wsus.IsReplica)
            {
                if (isRoot)
                    ctxMnuTreeview.Items.Add(GetItem("ManageMetaGroups"));
                else
                {
                    ctxMnuTreeview.Items.Add(GetItem("ManageThisMetaGroup"));
                    ctxMnuTreeview.Items.Add(GetItem("DeleteThisMetaGroup"));
                }
            }
            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private void InitializeContextMenuForServer()
        {
            Logger.EnteringMethod();
            exportAnUpdateToolStripMenuItem.Enabled = false;
            ctxMnuTreeview.Items.Clear();
            ctxMnuTreeview.Items.Add(GetItem("CleanUpdateServicesPackagesFolder"));

            trvWsus.ContextMenuStrip = ctxMnuTreeview;
        }

        private ToolStripMenuItem GetItem(string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(resMan.GetString(text));
            item.Tag = text;
            return item;
        }

        internal ComputerGroup ComputerGroups
        {
            get { return computerGroups; }
        }

        /// <summary>
        /// Return the Guid which have for name 'targetGroupName'.
        /// </summary>
        /// <param name="targetGroupName">The name of the group</param>
        /// <returns>Return the Guid corresponding to the group.</returns>
        internal Guid GetTargetGroupId(string targetGroupName)
        {
            return computerGroupGuidConverter[targetGroupName];
        }

        private void ChangeLanguage(string newLanguage)
        {
            Logger.Write("Entering ChangeLanguage with newLanguage : " + newLanguage);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(newLanguage);
            MessageBox.Show(resMan.GetString("ChangingLanguage"));
            restart = true;
            this.Close();
        }

        private void FillServerList()
        {
            Logger.EnteringMethod();
            WsusServer oldSelectedServer = null;
            if (cmbBxServerList.SelectedIndex != -1)
                oldSelectedServer = (WsusServer)cmbBxServerList.SelectedItem;
            serverList = settings.LoadServerSettings();

            cmbBxServerList.Items.Clear();
            foreach (WsusServer server in serverList)
            {
                cmbBxServerList.Items.Add((WsusServer)server.Clone());
                Logger.Write("Wsus Server " + server.Name + " added to the Combobox");
            }

            if (cmbBxServerList.Items.Count != 0)
            {
                cmbBxServerList.SelectedIndex = 0;

                if (oldSelectedServer != null)
                {
                    Logger.Write("Old selected server were : " + oldSelectedServer.Name);
                    for (int i = 0; i < cmbBxServerList.Items.Count; i++)
                    {
                        if (((WsusServer)cmbBxServerList.Items[i]).Equals(oldSelectedServer))
                        {
                            Logger.Write("Reselecting the old server");
                            cmbBxServerList.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        private void FillMetaGroups()
        {
            Logger.EnteringMethod();
            foreach (WsusServer wsusServer in serverList)
            {
                Dictionary<string, MetaGroup> metaGroups = new Dictionary<string, MetaGroup>();
                InitializeMetaGroups(wsusServer, metaGroups);
                InitializeComputerGroups(wsusServer, metaGroups);
                for (int i = 0; i < wsusServer.MetaGroups.Count; i++)
                {
                    wsusServer.MetaGroups[i] = metaGroups[wsusServer.MetaGroups[i].Name];
                }
            }
        }

        private void InitializeComputerGroups(WsusServer wsusServer, Dictionary<string, MetaGroup> metaGroups)
        {
            Logger.Write("Entering InitializeComputerGroups With WsusServer : " + wsusServer.Name);
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
            {
                Logger.Write("Processing metagroup : " + metaGroup.Name);
                if (metaGroup.InnerComputerGroups.Count != 0)
                    foreach (ComputerGroup computerGroup in metaGroup.InnerComputerGroups)
                    {
                        Logger.Write("Processing computerGroup : " + computerGroup.Name);
                        ComputerGroup newComputerGroup = GetComputerGroupByName(computerGroups, computerGroup.Name);
                        if (newComputerGroup != null)
                            metaGroups[metaGroup.Name].InnerComputerGroups.Add(newComputerGroup);
                    }
            }
        }

        private ComputerGroup GetComputerGroupByName(ComputerGroup groupToSearch, string nameToFind)
        {
            Logger.Write("Entering GetComputerGroupByName. groupToSearch : " + groupToSearch.Name + " And nameToFind : " + nameToFind);
            if (groupToSearch.Name == nameToFind)
                return groupToSearch;
            else
                foreach (ComputerGroup group in groupToSearch.InnerComputerGroup)
                {
                    ComputerGroup candidate = GetComputerGroupByName(group, nameToFind);
                    if (candidate != null)
                    {
                        Logger.Write("Returning : " + candidate.Name);
                        return candidate;
                    }
                }
            Logger.Write("**** Returning NULL.");

            return null;
        }

        private void InitializeMetaGroups(WsusServer wsusServer, Dictionary<string, MetaGroup> metaGroups)
        {
            Logger.Write("Entering InitializeMetaGroups. With wsusServer : " + wsusServer.Name);
            // Create all root MetaGroups.
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
                if (!metaGroups.ContainsKey(metaGroup.Name))
                    metaGroups.Add(metaGroup.Name, new MetaGroup(metaGroup.Name));

            // Populate InnerMetaGroup for each root MetaGroup.
            foreach (MetaGroup metaGroup in wsusServer.MetaGroups)
            {
                if (metaGroup.InnerMetaGroups.Count != 0)
                    foreach (MetaGroup innerMetaGroup in metaGroup.InnerMetaGroups)
                    {
                        metaGroups[metaGroup.Name].InnerMetaGroups.Add(metaGroups[innerMetaGroup.Name]);
                    }
            }
        }

        private void CleanUpdateServicesPackagesFolder()
        {
            Logger.EnteringMethod();
            List<string> presentId = new List<string>();
            List<System.IO.DirectoryInfo> obsoleteFolders = new List<System.IO.DirectoryInfo>();
            string serverName = currentWsusServer.Name;

            foreach (Company vendor in companies.Values)
            {
                foreach (Product product in vendor.Products.Values)
                {
                    foreach (IUpdate update in product.Updates)
                        presentId.Add(update.Id.UpdateId.ToString().ToLower());
                }
            }
            try
            {
                System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(@"\\" + serverName + @"\UpdateServicesPackages");
                System.IO.DirectoryInfo[] directories = dirInfo.GetDirectories();
                foreach (System.IO.DirectoryInfo directory in directories)
                {
                    if (!presentId.Contains(directory.Name.ToLower()))
                        obsoleteFolders.Add(directory);
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }

            FrmDeleteObsoleteFolders frmDeleteFolders = new FrmDeleteObsoleteFolders(obsoleteFolders);
            frmDeleteFolders.ShowDialog();
        }

        private void UpdatePublished(IUpdate publishedUpdate)
        {
            Logger.Write("Entering UpdatePublished for : " + publishedUpdate.Title);

            Action action = () =>
            {
                IUpdateCategory product = publishedUpdate.GetUpdateCategories()[0];
                IUpdateCategory company = product.GetParentUpdateCategory();
                Logger.Write("Product : " + product.Title);
                Logger.Write("Company : " + company.Title);

                if (!companies.ContainsKey(company.Id))
                    CreateNewCompany(company, Properties.Settings.Default.ShowNonLocallyPublishedUpdates);
                else
                {
                    Company vendor = companies[company.Id];

                    if (!vendor.Products.ContainsKey(product.Id))
                        vendor.AddProduct(product.Id, publishedUpdate.ProductTitles[0]);

                    Product newproduct = vendor.Products[product.Id];
                    newproduct.AddUpdate(publishedUpdate);
                }
                RefreshProductsAndUpdatesCount(company, product);
            };
            this.Invoke(action);
        }

        private void UpdateRevised(IUpdate oldUpdate, IUpdateCategory oldCompanyCategory, IUpdateCategory oldProductCategory, IUpdate revisedUpdate)
        {
            Logger.Write("Entering UpdateRevised with oldUpdate : " + oldUpdate.Title + " and oldCompanyCategory : " + oldCompanyCategory.Title + " and oldProductCategory : " + oldProductCategory.Title + " and revisedUpdate : " + revisedUpdate.Title);
            bool companyCreated = false;
            IUpdateCategory newProductCategory = revisedUpdate.GetUpdateCategories()[0];
            IUpdateCategory newCompanyCategory = newProductCategory.GetParentUpdateCategory();
            Logger.Write("newProductCategory : " + newProductCategory.Title);
            Logger.Write("newCompanyCategory : " + newCompanyCategory.Title);

            if (!companies.ContainsKey(newCompanyCategory.Id))
            {
                CreateNewCompany(newCompanyCategory, Properties.Settings.Default.ShowNonLocallyPublishedUpdates);
                companyCreated = true;
            }

            Company vendor = companies[newCompanyCategory.Id];

            if (!vendor.Products.ContainsKey(newProductCategory.Id))
            {
                vendor.AddProduct(newProductCategory.Id, revisedUpdate.ProductTitles[0]);
            }
            Product product = vendor.Products[newProductCategory.Id];

            if ((oldCompanyCategory.Id != newCompanyCategory.Id) || (oldProductCategory.Id != newProductCategory.Id))
            {
                companies[oldCompanyCategory.Id].Products[oldProductCategory.Id].RemoveUpdate(oldUpdate);
                if (!companyCreated)
                    companies[newCompanyCategory.Id].Products[newProductCategory.Id].AddUpdate(revisedUpdate);
            }

            product.RefreshUpdate(revisedUpdate);

            RefreshProductsAndUpdatesCount(oldCompanyCategory, oldProductCategory);
            RefreshProductsAndUpdatesCount(newCompanyCategory, newProductCategory);
        }

        private void UpdateDeleted(IUpdate deletedUpdate, IUpdateCategory company, IUpdateCategory product)
        {
            Logger.Write("Entering UpdateDeleted with deleteUpdate : " + deletedUpdate.Title + " and company : " + company.Title + " and product : " + product.Title);
            updateCtrl.UpdateDeleted(deletedUpdate);

            RefreshProductsAndUpdatesCount(company, product);
        }

        private void RefreshProductsAndUpdatesCount(IUpdateCategory company, IUpdateCategory product)
        {
            Logger.Write("Entering RefreshProductsAndUpdatesCount with commany : " + company.Title + " and product : " + product.Title);
            Company vendor = null;
            Product newproduct = null;

            if (companies.ContainsKey(company.Id))
            {
                vendor = companies[company.Id];

                TreeNode[] companyNode = trvWsus.Nodes.Find(vendor.CompanyName, true);
                for (int i = 0; i < companyNode.Length; i++)
                {
                    if (companyNode[i].Tag.ToString() == ("Company:" + vendor.ID.ToString()))
                    {
                        companyNode[i].Text = vendor.CompanyName + " (" + vendor.Products.Count + ")";

                        if (vendor.Products.ContainsKey(product.Id))
                        {
                            newproduct = vendor.Products[product.Id];
                            TreeNode[] productNode = companyNode[i].Nodes.Find(newproduct.ProductName, true);
                            for (int j = 0; j < productNode.Length; j++)
                            {
                                if (productNode[i].Tag.ToString() == ("Product:" + newproduct.ID.ToString()))
                                {
                                    productNode[i].Text = newproduct.ProductName + " (" + newproduct.Updates.Count + ")";
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void wsus_UpdateSuperseded(IList<Guid> SupersededUpdates)
        {
            Logger.EnteringMethod();

            Action action = () =>
           {
               try
               {

                   IUpdate supersededUpdate = wsus.GetUpdate(new UpdateRevisionId(SupersededUpdates[0]));

                   foreach (Company company in companies.Values)
                   {
                       foreach (Product product in company.Products.Values)
                       {
                           foreach (IUpdate update in product.Updates)
                           {
                               if (update.Id.UpdateId == supersededUpdate.Id.UpdateId)
                               {
                                   foreach (Guid id in SupersededUpdates)
                                   {
                                       supersededUpdate = wsus.GetUpdate(new UpdateRevisionId(id));
                                       product.RefreshUpdate(supersededUpdate);
                                   }
                                   return;
                               }
                           }
                       }
                   }
               }
               catch (Exception ex) { Logger.Write("**** " + ex.Message); }
           };
            this.Invoke(action);
        }

        /// <summary>
        /// Search the Product which have for name productToFind.
        /// </summary>
        /// <param name="productToFind">Name of the Product to find.</param>
        /// <returns>Return the Product if found. Return Null in other case.</returns>
        private Product GetProduct(Guid productToFind)
        {
            Logger.Write("Entering GetProduct with productToFind : " + productToFind.ToString());
            Product foundProduct = null;

            foreach (Company company in companies.Values)
            {
                if (company.Products.ContainsKey(productToFind))
                {
                    foundProduct = company.Products[productToFind];
                    break;
                }
            }
            Logger.Write("Returning " + foundProduct.ProductName);

            return foundProduct;
        }

        private void CreateNewUpdate(string xmlActions)
        {
            Logger.Write("Entering CreateNewUpdate with xmlAction : " + xmlActions);
            this.Cursor = Cursors.WaitCursor;
            FrmUpdateWizard updateWizard;

            if (trvWsus.SelectedNode != null)
            {
                if (trvWsus.SelectedNode.Tag.ToString().StartsWith("Product:"))
                    updateWizard = new FrmUpdateWizard(companies, updateCtrl.Product.Vendor, updateCtrl.Product);
                else
                    if (trvWsus.SelectedNode.Tag.ToString().StartsWith("Company:"))
                        updateWizard = new FrmUpdateWizard(companies, companies[new Guid(trvWsus.SelectedNode.Tag.ToString().Substring("Company:".Length))]);
                    else
                        updateWizard = new FrmUpdateWizard(companies);
            }
            else
                updateWizard = new FrmUpdateWizard(companies);

            if (!string.IsNullOrEmpty(xmlActions))
            {
                updateWizard.CustomUpdate = true;
                string filePath = Tools.Utilities.GetTempFolder() + Guid.NewGuid().ToString() + ".xml";
                System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath);
                writer.Write(xmlActions);
                writer.Flush();
                writer.Close();
                updateWizard.ActionsFilePath = filePath;
            }

            this.Cursor = Cursors.Default;
            updateWizard.ShowDialog(this);
        }

        private void ImportAnUpdate()
        {
            Logger.EnteringMethod();

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = ".cab";
            openFileDialog.Filter = "CAB files|*.cab";
            openFileDialog.Multiselect = false;
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                System.IO.FileInfo sourceFile = new System.IO.FileInfo(openFileDialog.FileName);
                if (sourceFile.Exists && sourceFile.Extension.ToLower() == ".cab")
                {
                    try
                    {
                        string workingDirectory = Tools.Utilities.GetTempFolder();
                        if (System.IO.Directory.Exists(workingDirectory))
                        {
                            System.IO.Directory.Delete(workingDirectory, true);
                            System.Threading.Thread.Sleep(100);
                        }
                        System.IO.Directory.CreateDirectory(workingDirectory);
                        Microsoft.Deployment.Compression.Cab.CabInfo decompressor = new Microsoft.Deployment.Compression.Cab.CabInfo(sourceFile.FullName);
                        decompressor.Unpack(workingDirectory, null);
                        string sdpFileName = string.Empty;
                        string cabFileName = string.Empty;
                        foreach (System.IO.FileInfo file in new System.IO.DirectoryInfo(workingDirectory).GetFiles())
                        {
                            if (file.Extension.ToLower() == ".xml")
                                sdpFileName = file.Name;
                            if (file.Extension.ToLower() == ".cab")
                                cabFileName = file.Name;
                        }
                        if (!string.IsNullOrEmpty(sdpFileName) && !string.IsNullOrEmpty(cabFileName))
                            if (wsus.ImportAnUpdate(workingDirectory, sdpFileName, cabFileName))
                                MessageBox.Show(resMan.GetString("UpdateSuccessfullyImported"));
                            else
                                MessageBox.Show(resMan.GetString("ErrorImportingUpdate"));

                        Tools.Utilities.DeleteFolder(workingDirectory);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("****" + ex.Message);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                    MessageBox.Show(resMan.GetString("SelectaCabFile"));

                this.Cursor = Cursors.Default;
            }
        }

        private string HKLM_GetString(string path, string key)
        {
            string result = string.Empty;
            Microsoft.Win32.RegistryKey rk = null;

            try
            {
                rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path);

                if (rk != null)
                {
                    result = (string)rk.GetValue(key);
                }
            }
            catch { }
            finally
            {
                if (rk != null)
                    rk.Close();
            }
            return result;
        }

        #region (answers to events - Réponses aux événements)

        private void trvWsus_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Logger.EnteringMethod();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Logger.Write("Selecting " + e.Node.Text);
                trvWsus.SelectedNode = e.Node;
            }
        }

        private void FrmWsusPackagePublisher_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.EnteringMethod();

            frmCatalogSubscription.AbortChecking = true;

            System.Globalization.CultureInfo OSCulture = System.Globalization.CultureInfo.InstalledUICulture;
            string OSLanguage = OSCulture.Parent.TwoLetterISOLanguageName.ToLower();
            double minute = DateTime.Now.Minute;
            if (OSLanguage != "fr" && OSLanguage != "en" && OSLanguage != "de" && (minute / 3) == System.Math.Round(minute / 3))
            {
                Logger.Write("Does the user want to translate WPP ????");
                if (MessageBox.Show("If you like Wsus Package Publisher, please consider helping this project by translating it in your Language.", "Support this project...", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Logger.Write("Yes he want !");
                    System.Diagnostics.ProcessStartInfo sInfo = new System.Diagnostics.ProcessStartInfo("iexplore.exe");
                    sInfo.Arguments = "http://wsuspackagepublisher.codeplex.com/team/view";
                    System.Diagnostics.Process.Start(sInfo);
                }
                else
                    Logger.Write("No he don't want :-(");
            }

            updateCtrl.Dispose();
            computerCtrl.Dispose();
        }

        private void ctxMnuTreeview_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Logger.EnteringMethod();
            ctxMnuTreeview.Hide();

            switch (e.ClickedItem.Tag.ToString())
            {
                case "ManageMetaGroups":
                    Logger.Write("Manage Meta Groups.");
                    frmMetaGroups _frmMetaGroupForCreation = new frmMetaGroups(currentWsusServer.MetaGroups, computerGroups);
                    if (_frmMetaGroupForCreation.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                    }
                    break;
                case "ManageThisMetaGroup":
                    Logger.Write("Manage This MetaGroup");
                    frmMetaGroups _frmMetaGroupForEdition = new frmMetaGroups(currentWsusServer.MetaGroups, computerGroups, trvWsus.SelectedNode.Text);
                    if (_frmMetaGroupForEdition.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                    }
                    break;
                case "DeleteThisMetaGroup":
                    Logger.Write("Delete This MetaGroup.");
                    string metaGroupName = trvWsus.SelectedNode.Text;
                    MetaGroup metaGroupToDelete = new MetaGroup();
                    bool found = false;
                    foreach (MetaGroup metagroup in currentWsusServer.MetaGroups)
                    {
                        if (metagroup.Name == metaGroupName)
                        {
                            metaGroupToDelete = metagroup;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        currentWsusServer.MetaGroups.Remove(metaGroupToDelete);
                        foreach (MetaGroup metaGroup in currentWsusServer.MetaGroups)
                        {
                            if (metaGroup.InnerMetaGroups.Contains(metaGroupToDelete))
                                metaGroup.InnerMetaGroups.Remove(metaGroupToDelete);
                        }
                        PopulateMetaGroupNode();
                        FrmSettings settingsForm = new FrmSettings();
                        settingsForm.SaveSettings(serverList);
                        updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                        trvWsus.SelectedNode = allMetaGroupsNode;
                        trvWsus_AfterSelect(trvWsus, new TreeViewEventArgs(allMetaGroupsNode));
                    }
                    break;
                case "CreateUpdate":
                    Logger.Write("Create Update.");
                    createUpdatetoolStripMenuItem.PerformClick();
                    break;
                case "CreateCustomUpdate":
                    Logger.Write("Create a Custom Update.");
                    createCustomUpdateToolStripMenuItem.PerformClick();
                    break;
                case "ImportAnUpdate":
                    Logger.Write("Import an Update.");
                    importAnUpdateToolStripMenuItem.PerformClick();
                    break;
                case "DeleteProduct":
                    Logger.Write("Delete Product.");
                    this.Cursor = Cursors.WaitCursor;
                    EmptyProductDeleter deleter = new EmptyProductDeleter();
                    EmptyProductDeleted(deleter.DeleteProduct(updateCtrl.Product.ID));
                    this.Cursor = Cursors.Default;
                    break;
                case "SendDetectNow":
                case "SendReportNow":
                    Logger.Write("Send Detect or Report Now");
                    List<ADComputer> targetComputers = new List<ADComputer>();
                    string groupName = trvWsus.SelectedNode.Text;
                    ComputerTargetCollection computerCollection = wsus.GetComputerTargets(computerGroupGuidConverter[groupName]);
                    FrmRemoteExecution remoteExecution = new FrmRemoteExecution();

                    foreach (IComputerTarget computer in computerCollection)
                    {
                        targetComputers.Add(new ADComputer(computer.FullDomainName));
                    }
                    ctxMnuTreeview.Hide();
                    Credentials cred = Credentials.GetInstance();
                    if (targetComputers.Count != 0)
                        if (cred.InitializeCredential() == false)
                            return;

                    remoteExecution.Show(this);
                    switch (e.ClickedItem.Tag.ToString())
                    {
                        case "SendDetectNow":
                            remoteExecution.SendDetectNow(targetComputers, cred.Login, cred.Password);
                            break;
                        case "SendReportNow":
                            remoteExecution.SendReportNow(targetComputers, cred.Login, cred.Password);
                            break;
                        default:
                            break;
                    }
                    break;
                case "CleanUpdateServicesPackagesFolder":
                    CleanUpdateServicesPackagesFolder();
                    break;
                default:
                    Logger.Write("**** Unknown Node clicked. " + e.ClickedItem.Text + " and Tag : " + e.ClickedItem.Tag.ToString());
                    break;
            }
        }

        private void checkAgainstActiveDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FrmAD_WSUSComparer ad_WsusComparer = new FrmAD_WSUSComparer();
            ad_WsusComparer.ShowDialog();
        }

        private void createCustomUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CustomUpdateCreator.FrmCustomUpdateCreator frmCustomUpdate = new CustomUpdateCreator.FrmCustomUpdateCreator();

            if (!wsus.IsConsoleVersionAllowPublication())
            {
                Logger.Write("Server/Console version mismatch");
                MessageBox.Show(resMan.GetString("ServerAndConsoleVersionMismatch"));
            }
            else
            {
                CertificateHelper.CertificateStatus certStatus = wsus.GetCertificateStatus;
                if (certStatus == CertificateHelper.CertificateStatus.Valid || certStatus == CertificateHelper.CertificateStatus.NearExpiration)
                {
                    if (frmCustomUpdate.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        CreateNewUpdate(frmCustomUpdate.GetXmlActions());
                    }
                }
                else
                    MessageBox.Show(resMan.GetString("SolveCertificateProblemBeforePublishing"));
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Close();
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            FrmUpdateSearcher frmUpdateSearcher = new FrmUpdateSearcher();
            if (frmUpdateSearcher.SearchForNewVersion())
                frmUpdateSearcher.ShowDialog();
            else
                MessageBox.Show(resMan.GetString("NoNewVersionAvailable"));
        }

        private void FrmWsusPackagePublisher_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FillServerList();
            Logger.Write("WPP is up and ready");
            string lastUsedServerName = Properties.Settings.Default.LastUsedServerName.ToLower();

            if (Properties.Settings.Default.ConnectToLastUsedServer && !string.IsNullOrEmpty(lastUsedServerName))
            {
                Logger.Write("Will try to connect to the last used server : " + lastUsedServerName);
                foreach (object obj in cmbBxServerList.Items)
                {
                    WsusServer wsusToConnectTo = (WsusServer)obj;
                    if (wsusToConnectTo.Name.ToLower() == lastUsedServerName)
                    {
                        cmbBxServerList.SelectedItem = obj;
                        connectButton.PerformClick();
                        break;
                    }
                }
            }
            DateTime lastVersionCheck = Properties.Settings.Default.LastVersionCheck;
            if (!lastVersionCheck.Date.Equals(DateTime.Now.Date))
            {
                FrmUpdateSearcher updateSearcher = new FrmUpdateSearcher();
                System.Threading.Thread newVersionSearcher = new System.Threading.Thread(new System.Threading.ThreadStart(updateSearcher.SearchForNewVersionAsync));
                newVersionSearcher.Start();
            }
        }

        private void frmCatalogSubscription_CheckingSubscriptionTerminated()
        {
            Action action = () =>
            {
                manageCatalogSubscriptionsToolStripMenuItem.Enabled = wsus.IsConnected && !wsus.IsReplica;
                if (frmCatalogSubscription.IsUpdateAvailable)
                {
                    if (MessageBox.Show(resMan.GetString("UpdatedCatalogAvailable"), resMan.GetString("CatalogUpdate"), MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        frmCatalogDifferenceViewer frmCatalogDiff = new frmCatalogDifferenceViewer(frmCatalogSubscription.UpdatedCatalogs);
                        if (frmCatalogDiff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            frmCatalogSubscription.ShowDialog();
                    }
                }
            };
            if (!this.IsDisposed && !this.Disposing)
                this.Invoke(action);
        }

        private void certificatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (wsus.IsConnected)
            {
                FrmCertificateManagement certificateMgmt = new FrmCertificateManagement();

                certificateMgmt.StartPosition = FormStartPosition.CenterParent;
                certificateMgmt.ShowDialog(this);
            }
            else
            {
                MessageBox.Show(resMan.GetString("ConnectToWsusFirst"));
                certificatetoolStripMenuItem.Enabled = false;
            }
        }

        private void mSIPropertyReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FrmMSIPropertyReader frmMSIReader = new FrmMSIPropertyReader();
            frmMSIReader.ShowDialog(this);
        }

        private void trvWsus_DragEnter(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            DragDropEffects effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array tab = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (tab != null && tab.GetValue(0) != null && tab.Length == 1)
                {
                    string fileName = tab.GetValue(0).ToString();
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                        effect = DragDropEffects.Copy;
                }
            }
            e.Effect = effect;
        }

        private void trvWsus_DragDrop(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            Product selectedProduct = null;
            Company selectedCompany = null;
            FrmUpdateWizard updateWizard = new FrmUpdateWizard(companies);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                Point clientLocation = trvWsus.PointToClient(new Point(e.X, e.Y));
                TreeNode node = trvWsus.GetNodeAt(clientLocation);
                if (node != null && !string.IsNullOrEmpty(node.Text) && node.Tag != null && !string.IsNullOrEmpty(node.Tag.ToString()))
                {
                    if (node.Tag.ToString().ToLower().StartsWith("product:"))
                    {
                        selectedProduct = GetProduct(new Guid(node.Tag.ToString().Substring("product:".Length)));
                        selectedCompany = selectedProduct.Vendor;
                    }
                    if (node.Tag.ToString().ToLower().StartsWith("company:"))
                        selectedCompany = companies[new Guid(node.Tag.ToString().Substring("company:".Length))];
                }
                if (fileNames != null && fileNames.GetValue(0) != null)
                {
                    string fileName = fileNames[0];
                    if (selectedProduct != null && selectedCompany != null)
                        updateWizard = new FrmUpdateWizard(companies, selectedCompany, selectedProduct, fileName);
                    else
                        if (selectedCompany != null)
                            updateWizard = new FrmUpdateWizard(companies, selectedCompany, fileName);
                        else
                            updateWizard = new FrmUpdateWizard(companies, fileName);

                    updateWizard.StartPosition = FormStartPosition.CenterScreen;
                    updateWizard.Show(this);
                }
            }
        }

        private void paramètresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillServerList();
                if (currentWsusServer != null)
                    updateCtrl.SetMetaGroups(currentWsusServer.MetaGroups);
                if (wsus.IsConnected && trvWsus.SelectedNode != null)
                    trvWsus_AfterSelect(trvWsus, new TreeViewEventArgs(trvWsus.SelectedNode));
            }
        }

        private void editMaxCabFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmChangeMaxCabSize changeMaxCabSize = new FrmChangeMaxCabSize(String.Empty, 0);
            changeMaxCabSize.ShowDialog();
        }

        private void françaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            frenchtoolStripMenuItem.Checked = true;
            englishtoolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "fr";
            Properties.Settings.Default.Save();
            ChangeLanguage("fr");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            englishtoolStripMenuItem.Checked = true;
            frenchtoolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "en";
            Properties.Settings.Default.Save();

            ChangeLanguage("en");
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            germanToolStripMenuItem.Checked = true;
            englishtoolStripMenuItem.Checked = false;
            frenchtoolStripMenuItem.Checked = false;
            Properties.Settings.Default.Language = "de";
            Properties.Settings.Default.Save();

            ChangeLanguage("de");
        }

        private void russianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            germanToolStripMenuItem.Checked = false;
            englishtoolStripMenuItem.Checked = false;
            frenchtoolStripMenuItem.Checked = false;
            russianToolStripMenuItem.Checked = true;
            Properties.Settings.Default.Language = "ru";
            Properties.Settings.Default.Save();

            ChangeLanguage("ru");
        }

        private void createUpdatetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (!wsus.IsConsoleVersionAllowPublication())
            {
                Logger.Write("Server/Console version mismatch");
                MessageBox.Show(resMan.GetString("ServerAndConsoleVersionMismatch"));
            }
            else
            {
                CertificateHelper.CertificateStatus certStatus = wsus.GetCertificateStatus;
                if (certStatus == CertificateHelper.CertificateStatus.Valid || certStatus == CertificateHelper.CertificateStatus.NearExpiration)
                {
                    CreateNewUpdate(string.Empty);
                }
                else
                    MessageBox.Show(resMan.GetString("SolveCertificateProblemBeforePublishing"));
            }
        }

        private void trvWsus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Logger.EnteringMethod();
            this.Cursor = Cursors.WaitCursor;
            if (e.Node.Tag != null)
            {
                string label = e.Node.Tag.ToString().Substring(0, e.Node.Tag.ToString().IndexOf(':') + 1);
                switch (label)
                {
                    case "ComputerGroup:":
                        Logger.Write("ComputerGroup");
                        if ((splitContainer1.Panel2.Controls.Count == 1) && (splitContainer1.Panel2.Controls[0].GetType() == typeof(ComputerControl)))
                        {
                            computerCtrl.Display(GetTargetGroupId(e.Node.Text));
                        }
                        else
                        {
                            splitContainer1.Panel2.Controls.Clear();
                            splitContainer1.Panel2.Controls.Add(computerCtrl);
                            computerCtrl.Companies = companies;
                            computerCtrl.Display(GetTargetGroupId(e.Node.Text));
                        }
                        InitializeContextMenuForComputerGroup();
                        break;
                    case "Company:":
                    case "Updates:":
                        Logger.Write("Company or Updates");
                        //Company company = companies[e.Node.Text];
                        InitializeContextMenuForCompany();
                        break;
                    case "Product:":
                        Logger.Write("Product");
                        Guid companyGuid = new Guid(e.Node.Parent.Tag.ToString().Substring(e.Node.Parent.Tag.ToString().IndexOf(':') + 1));
                        Guid companyProduct = new Guid(e.Node.Tag.ToString().Substring(e.Node.Tag.ToString().IndexOf(':') + 1));
                        if (companies.ContainsKey(companyGuid))
                        {
                            Company company = companies[companyGuid];
                            if (company.Products.ContainsKey(companyProduct))
                            {
                                if (splitContainer1.Panel2.Controls.Count == 0 || (splitContainer1.Panel2.Controls[0].GetType() != typeof(UpdateControl)))
                                {
                                    splitContainer1.SuspendLayout();
                                    splitContainer1.Panel2.Controls.Clear();
                                    splitContainer1.Panel2.Controls.Add(updateCtrl);
                                    splitContainer1.ResumeLayout();
                                }
                                updateCtrl.Product = company.Products[companyProduct];
                                InitializeContextMenuForProduct(updateCtrl.Product.UpdatesCount == 0);
                            }
                        }
                        break;
                    case "MetaGroup:":
                        Logger.Write("MetaGroup");
                        InitializeContextMenuForMetaGroup(false);
                        break;
                    case "MetaGroupRoot:":
                        Logger.Write("MetaGroupRoot");
                        InitializeContextMenuForMetaGroup(true);
                        break;
                    case "Server:":
                        Logger.Write("Server");
                        InitializeContextMenuForServer();
                        break;
                    default:
                        Logger.Write("**** Unknown Selection. " + e.Node.Text + " Tag : " + e.Node.Tag.ToString());
                        BlankContextMenu();
                        break;
                }
            }

            this.Cursor = Cursors.Default;
        }

        private void buttonItem_MouseEnter(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Select();
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.ShowDialog();
        }

        private void sendDebugInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSendDebugInfo frmSend = new FrmSendDebugInfo();
            frmSend.SendingReason = FrmSendDebugInfo.SendingReasons.AskForSupport;
            frmSend.ShowDialog();
        }

        private void toolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).ForeColor = Color.Black;
        }

        private void toolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).ForeColor = Color.White;
        }

        private void exportAnUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateCtrl.ExportAnUpdate();
        }

        private void importAnUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportAnUpdate();
        }

        private void importFromCatalogtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmImportCatalog frmCatalog = new FrmImportCatalog();

            frmCatalog.ShowDialog();
        }

        private void manageCatalogSubscriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCatalogSubscription.CheckingTerminated)
                frmCatalogSubscription.ShowDialog();
        }

        #endregion (answers to events - Réponses aux événements)

    }
}
