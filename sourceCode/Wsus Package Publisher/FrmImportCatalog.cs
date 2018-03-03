using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmImportCatalog : Form
    {
        private enum FilterCriterias
        {
            Title,
            Description
        }

        private WsusWrapper _wsus;
        private CatalogHelper catalogHelper;
        private List<CatalogUpdate> _selectedUpdates = new List<CatalogUpdate>();
        private Dictionary<Guid, CatalogUpdate> _allUpdates = new Dictionary<Guid, CatalogUpdate>();
        private bool _disableFilterFunction = true;
        private bool _programaticallyChangeCheckState = false;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmImportCatalog).Assembly);

        public FrmImportCatalog()
        {
            InitializeComponent();
            txtBxFilepath.Select();
            _wsus = WsusWrapper.GetInstance();
            PresetVisibleInWsusConsoleChkBx();
            cmbBxFilterCriteria.SelectedIndex = 0;
        }

        public FrmImportCatalog(string catalogFullPath)
        {
            InitializeComponent();
            txtBxFilepath.Select();
            _wsus = WsusWrapper.GetInstance();
            PresetVisibleInWsusConsoleChkBx();
            cmbBxFilterCriteria.SelectedIndex = 0;
            txtBxFilepath.Text = catalogFullPath;
        }

        #region {Internal Properties - Propriétés Internes}

        internal bool ShowInWsusConsole
        {
            get { return chkBxVisibleInWsusConsole.Checked; }
        }

        #endregion {Internal Properties - Propriétés Internes}

        #region (Private Methods - Méthodes privées)

        private void DisplayData()
        {
            trvCatalog.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Root");
            rootNode.Tag = new CatalogItem();
            trvCatalog.Nodes.Add(rootNode);

            foreach (CatalogVendor vendor in catalogHelper.Vendors.Values)
            {
                TreeNode vendorNode = new TreeNode(vendor.VendorName);
                vendorNode.Tag = new CatalogItem(vendor);
                rootNode.Nodes.Add(vendorNode);
                AddProductToVendor(vendorNode, vendor);
            }
            trvCatalog.Sort();
            if (trvCatalog.Nodes.Count != 0)
                trvCatalog.Nodes[0].ExpandAll();
        }

        private void AddProductToVendor(TreeNode vendorNode, CatalogVendor vendor)
        {
            foreach (CatalogProduct product in vendor.Products.Values)
            {
                TreeNode productNode = new TreeNode(product.ProductName);
                productNode.Tag = new CatalogItem(product);
                vendorNode.Nodes.Add(productNode);
                AddUpdateToProduct(productNode, product);
            }
        }

        private void AddUpdateToProduct(TreeNode productNode, CatalogProduct product)
        {
            foreach (CatalogUpdate update in product.Updates)
            {
                if (string.IsNullOrEmpty(txtBxFilterText.Text))
                {
                    TreeNode updateNode = new TreeNode(update.Title);
                    updateNode.Tag = new CatalogItem(update);
                    productNode.Nodes.Add(updateNode);
                }
                else
                {
                    FilterCriterias criteria = (cmbBxFilterCriteria.SelectedIndex == 0) ? FilterCriterias.Title : FilterCriterias.Description;
                    switch (criteria)
                    {
                        case FilterCriterias.Title:
                            if (System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(update.Title, txtBxFilterText.Text, System.Globalization.CompareOptions.IgnoreCase) >= 0)
                            {
                                TreeNode updateNode = new TreeNode(update.Title);
                                updateNode.Tag = new CatalogItem(update);
                                productNode.Nodes.Add(updateNode);
                            }
                            break;
                        case FilterCriterias.Description:
                            if (System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(update.Description, txtBxFilterText.Text, System.Globalization.CompareOptions.IgnoreCase) >= 0)
                            {
                                TreeNode updateNode = new TreeNode(update.Title);
                                updateNode.Tag = new CatalogItem(update);
                                productNode.Nodes.Add(updateNode);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Align recusively childs checkbox of the currentNode, with the provided state.
        /// </summary>
        /// <param name="currentNode">First node to look into.</param>
        /// <param name="isChecked">State to align with.</param>
        private void ChangeCheckState(TreeNode currentNode, bool isChecked)
        {
            _programaticallyChangeCheckState = true;
            foreach (TreeNode node in currentNode.Nodes)
            {
                ChangeCheckState(node, isChecked);
                node.Checked = isChecked;
            }
            _programaticallyChangeCheckState = false;
        }

        /// <summary>
        /// Search recusively in the provided TreeNode, node with checked Update and add them to the _selectedUpdates List.
        /// </summary>
        /// <param name="rootNode">First node where to start the search.</param>
        private void GetSelectedUpdates(TreeNode rootNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                GetSelectedUpdates(node);
                if (node.Checked)
                {
                    CatalogItem item = (CatalogItem)node.Tag;
                    if (item.ItemType == CatalogItem.CatalogItemTypes.Update) // && item.Update.SDP.PackageUpdateType != Microsoft.UpdateServices.Administration.PackageUpdateType.Detectoid)
                        _selectedUpdates.Add(item.Update);
                }
            }
        }

        /// <summary>
        /// Clear all fields in the right pane.
        /// </summary>
        private void ClearUpdateInformations()
        {
            txtBxVendorName.Text = string.Empty;
            txtBxProductName.Text = string.Empty;
            txtBxTitle.Text = string.Empty;
            txtBxDescription.Text = string.Empty;
            txtBxSupportUrl.Text = string.Empty;
            txtBxSecurityBulletinId.Text = string.Empty;
            txtBxSecurityRating.Text = string.Empty;
            txtBxPackageId.Text = string.Empty;
            txtBxSize.Text = string.Empty;

            cmbBxAdditionnalInformation.Items.Clear();
            cmbBxCVE.Items.Clear();
            cmbBxLanguages.Items.Clear();
            cmbBxPrerequisites.Items.Clear();
        }

        private void FillUpdateInformations(CatalogUpdate updateToDisplay)
        {
            txtBxVendorName.Text = updateToDisplay.VendorName;
            txtBxProductName.Text = updateToDisplay.ProductName;
            txtBxTitle.Text = updateToDisplay.Title;
            txtBxDescription.Text = updateToDisplay.Description;
            txtBxSupportUrl.Text = updateToDisplay.SupportUrl;
            txtBxSecurityBulletinId.Text = updateToDisplay.SecurityBulletinId;
            txtBxSecurityRating.Text = updateToDisplay.SecurityRating;
            txtBxPackageId.Text = updateToDisplay.PackageId;
            txtBxSize.Text = GetPackageSize(updateToDisplay);
            dtCreationDate.Value = updateToDisplay.CreationDate;

            cmbBxAdditionnalInformation.Items.Clear();
            foreach (Uri url in updateToDisplay.AdditionnalInformationsUrls)
            {
                cmbBxAdditionnalInformation.Items.Add(url.ToString());
            }
            if (cmbBxAdditionnalInformation.Items.Count != 0)
                cmbBxAdditionnalInformation.SelectedIndex = 0;
            cmbBxCVE.Items.Clear();
            foreach (string cve in updateToDisplay.CveIds)
            {
                cmbBxCVE.Items.Add(cve);
            }
            if (cmbBxCVE.Items.Count != 0)
                cmbBxCVE.SelectedIndex = 0;
            cmbBxLanguages.Items.Clear();
            foreach (string langue in updateToDisplay.Languages)
            {
                cmbBxLanguages.Items.Add(langue);
            }
            if (cmbBxLanguages.Items.Count != 0)
                cmbBxLanguages.SelectedIndex = 0;

            cmbBxPrerequisites.Items.Clear();
            if (updateToDisplay.SDP.Prerequisites != null && updateToDisplay.SDP.Prerequisites.Count != 0)
            {
                string prerequisiteName = string.Empty;
                foreach (Guid prerequisite in updateToDisplay.SDP.Prerequisites[0].Ids)
                {
                    prerequisiteName = GetPrerequisiteName(prerequisite);
                    cmbBxPrerequisites.Items.Add(prerequisiteName == string.Empty ? prerequisite.ToString() : prerequisiteName);
                }
                if (cmbBxPrerequisites.Items.Count != 0)
                    cmbBxPrerequisites.SelectedIndex = 0;
            }
        }

        private string GetPackageSize(CatalogUpdate package)
        {
            string unit = string.Empty;

            if (package.SDP.InstallableItems != null && package.SDP.InstallableItems.Count != 0)
            {
                long size = package.SDP.InstallableItems[0].OriginalSourceFile.Size;
                if (size > 1024)
                {
                    size /= 1024;
                    unit = " KB";
                }
                if (size > 1024)
                {
                    size /= 1024;
                    unit = " MB";
                }
                if (size > 1024)
                {
                    size /= 1024;
                    unit = " GB";
                }

                return size.ToString() + unit;
            }

            return string.Empty;
        }

        private string GetPrerequisiteName(Guid prerequisiteId)
        {
            if (_allUpdates.ContainsKey(prerequisiteId))
                return _allUpdates[prerequisiteId].Title;

            return string.Empty;
        }

        private void ValidateData()
        {
            btnFilter.Enabled = !_disableFilterFunction && cmbBxFilterCriteria.SelectedIndex != -1 && !string.IsNullOrEmpty(txtBxFilterText.Text);
        }

        private void UnFilterNodes()
        {
            txtBxFilterText.Text = string.Empty;
            DisplayData();
        }

        private void ListAllUpdates()
        {
            _allUpdates.Clear();

            foreach (CatalogVendor vendor in catalogHelper.Vendors.Values)
            {
                foreach (CatalogProduct product in vendor.Products.Values)
                {
                    foreach (CatalogUpdate update in product.Updates)
                    {
                        if (!_allUpdates.ContainsKey(update.SDP.PackageId))
                            _allUpdates.Add(update.SDP.PackageId, update);
                    }
                }
            }
        }

        private void IncludePrerequisites()
        {
            List<CatalogUpdate> updatesToInclude = new List<CatalogUpdate>();

            foreach (CatalogUpdate update in _selectedUpdates)
            {
                if (update.SDP.Prerequisites != null && update.SDP.Prerequisites.Count != 0)
                    foreach (Guid prerequisite in update.SDP.Prerequisites[0].Ids)
                    {
                        if (_allUpdates.ContainsKey(prerequisite) && !IsPrerequisiteAlreadySelected(prerequisite) && !updatesToInclude.Contains(_allUpdates[prerequisite]))
                            updatesToInclude.Add(_allUpdates[prerequisite]);
                    }
            }
            _selectedUpdates.AddRange(updatesToInclude);
        }

        private bool IsPrerequisiteAlreadySelected(Guid prerequisite)
        {
            foreach (CatalogUpdate update in _selectedUpdates)
            {
                if (update.SDP.PackageId == prerequisite)
                    return true;
            }
            return false;
        }

        private void SortSelectedUpdates()
        {
            Queue<CatalogUpdate> orderedUpdates = new Queue<CatalogUpdate>();

            for (int i = 0; i < _selectedUpdates.Count; i++)
            {
                Microsoft.UpdateServices.Administration.SoftwareDistributionPackage sdp = _selectedUpdates[i].SDP;

                if (sdp.Prerequisites == null || sdp.Prerequisites.Count == 0 || sdp.Prerequisites[0].Ids.Count == 0)
                {
                    orderedUpdates.Enqueue(_selectedUpdates[i]);
                    _selectedUpdates.Remove(_selectedUpdates[i]);
                    i--;
                }
                else
                {
                    bool hasKnownPrerequisite = false;
                    foreach (Guid prerequisite in sdp.Prerequisites[0].Ids)
                    {
                        if (GetPrerequisiteName(prerequisite) != string.Empty)
                        {
                            hasKnownPrerequisite = true;
                            break;
                        }
                    }
                    if (!hasKnownPrerequisite)
                    {
                        orderedUpdates.Enqueue(_selectedUpdates[i]);
                        _selectedUpdates.Remove(_selectedUpdates[i]);
                        i--;
                    }
                }
            }
            bool hasDecreased = true;
            while (_selectedUpdates.Count != 0 && hasDecreased)
            {
                hasDecreased = false;
                for (int i = 0; i < _selectedUpdates.Count; i++)
                {
                    bool isReadyToEnqueue = true;
                    foreach (Guid prerequisite in _selectedUpdates[i].SDP.Prerequisites[0].Ids)
                    {
                        if (_allUpdates.ContainsKey(prerequisite) && !orderedUpdates.Contains(_allUpdates[prerequisite]))
                        {
                            isReadyToEnqueue = false;
                            break;
                        }
                    }
                    if (isReadyToEnqueue)
                    {
                        orderedUpdates.Enqueue(_selectedUpdates[i]);
                        _selectedUpdates.Remove(_selectedUpdates[i]);
                        i--;
                        hasDecreased = true;
                    }
                }
            }
            _selectedUpdates.Clear();
            _selectedUpdates.AddRange(orderedUpdates);
        }

        private void PresetVisibleInWsusConsoleChkBx()
        {
            Logger.EnteringMethod();
            Logger.Write("Wsus Is Local : " + _wsus.IsLocal.ToString());
            if (_wsus.IsLocal)
            {
                switch (_wsus.CurrentServer.VisibleInWsusConsole)
                {
                    case FrmSettings.MakeVisibleInWsusPolicy.Never:
                        chkBxVisibleInWsusConsole.Checked = false;
                        chkBxVisibleInWsusConsole.Enabled = false;
                        break;
                    case FrmSettings.MakeVisibleInWsusPolicy.LetMeChoose:
                        chkBxVisibleInWsusConsole.Checked = false;
                        chkBxVisibleInWsusConsole.Enabled = true;
                        break;
                    case FrmSettings.MakeVisibleInWsusPolicy.Always:
                        chkBxVisibleInWsusConsole.Checked = true;
                        chkBxVisibleInWsusConsole.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                chkBxVisibleInWsusConsole.Checked = false;
                chkBxVisibleInWsusConsole.Enabled = false;
            }
            Logger.Write(chkBxVisibleInWsusConsole);
        }

        #endregion (Private Methods - Méthodes privées)

        #region (Responses to events - Réponses aux événements)

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            _disableFilterFunction = true;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.AddExtension = true;
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.DefaultExt = ".cab";
            openFile.Filter = "CAB files|*.cab|XML Files|*.xml";
            openFile.FilterIndex = 0;
            openFile.Multiselect = false;
            openFile.ValidateNames = true;

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && System.IO.File.Exists(openFile.FileName))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(openFile.FileName);
                if (fileInfo.Extension.ToLower() == ".xml" || fileInfo.Extension.ToLower() == ".cab")
                {
                    txtBxFilepath.Text = openFile.FileName;
                    btnOpenCatalog.PerformClick();
                }
            }
            else
                _disableFilterFunction = false;
        }

        private void txtBxFilepath_TextChanged(object sender, EventArgs e)
        {
            btnOpenCatalog.Enabled = false;
            prgBarProgression.Value = 0;

            if (System.IO.File.Exists(txtBxFilepath.Text))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtBxFilepath.Text);
                btnOpenCatalog.Enabled = (fileInfo.Extension.ToLower() == ".xml" || fileInfo.Extension.ToLower() == ".cab");
            }
        }

        private void btnOpenCatalog_Click(object sender, EventArgs e)
        {
            _disableFilterFunction = true;
            btnClearFilter.Enabled = false;
            btnFilter.Enabled = false;
            trvCatalog.Nodes.Clear();
            ClearUpdateInformations();
            lblProgress.Text = resMan.GetString("UncompressingFile");
            prgBarProgression.Value = 0;
            btnBrowse.Enabled = false;
            btnOpenCatalog.Enabled = false;
            btnImport.Enabled = false;
            btnClose.Enabled = false;
            _selectedUpdates.Clear();

            catalogHelper = new CatalogHelper(txtBxFilepath.Text);
            catalogHelper.UnpackArchiveProgression += new CatalogHelper.UnpackArchiveProgressionEventHandler(catalogHelper_UnpackArchiveProgression);
            catalogHelper.UnpackArchiveFinished += new CatalogHelper.UnpackArchiveFinishedEventHandler(catalogHelper_UnpackArchiveFinished);
            catalogHelper.OpenCatalogProgression += new CatalogHelper.OpenCatalogProgressionEventHandler(catalogHelper_OpenCatalogProgression);
            catalogHelper.OpenCatalogFinished += new CatalogHelper.OpenCatalogFinishedEventHandler(catalogHelper_OpenCatalogFinished);

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(catalogHelper.OpenCatalog));
            t.Start();
        }

        private void catalogHelper_UnpackArchiveProgression(int percentProgression)
        {
            if (!this.IsDisposed)
            {
                Action action = () =>
                {
                    prgBarProgression.Value = percentProgression;
                };
                if (!this.IsDisposed)
                    this.Invoke(action);
            }
        }

        private void catalogHelper_UnpackArchiveFinished()
        {
            if (!this.IsDisposed)
            {
                Action action = () =>
                    {
                        lblProgress.Text = resMan.GetString("ParsingXMLFile");
                        prgBarProgression.Value = 0;
                        prgBarProgression.Refresh();
                    };
                if (!this.IsDisposed)
                    this.Invoke(action);
            }
        }

        private void catalogHelper_OpenCatalogFinished()
        {
            if (!this.IsDisposed)
            {
                Action action = () =>
                {
                    prgBarProgression.Value = 100;
                    lblProgress.Text = string.Empty;
                    DisplayData();
                    ListAllUpdates();
                    btnBrowse.Enabled = true;
                    btnClose.Enabled = true;
                    btnImport.Enabled = true;
                    btnClearFilter.Enabled = true;
                    _disableFilterFunction = false;
                    if (trvCatalog.Nodes.Count != 0)
                        trvCatalog.SelectedNode = trvCatalog.Nodes[0];
                    ValidateData();
                };
                if (!this.IsDisposed)
                    this.Invoke(action);
            }
        }

        private void catalogHelper_OpenCatalogProgression(int percentProgression)
        {
            if (!this.IsDisposed)
            {
                Action action = () =>
                    {
                        prgBarProgression.Value = percentProgression;
                    };
                if (!this.IsDisposed)
                    this.Invoke(action);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trvCatalog_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;
            bool isChecked = currentNode.Checked;

            if (!_programaticallyChangeCheckState)
                ChangeCheckState(currentNode, isChecked);
        }

        private void trvCatalog_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Logger.EnteringMethod();

            if (e.Node != null && !string.IsNullOrEmpty(e.Node.Text))
            {
                Logger.Write(e.Node.Text + " : " + e.Node.Tag.ToString());
                ClearUpdateInformations();
                CatalogItem item = (CatalogItem)e.Node.Tag;
                if (item.ItemType == CatalogItem.CatalogItemTypes.Update)
                    FillUpdateInformations(item.Update);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            WsusWrapper wsus = WsusWrapper.GetInstance();

            if (!wsus.IsConsoleVersionAllowPublication())
            {
                Logger.Write("Server/Console version mismatch");
                MessageBox.Show(resMan.GetString("ServerAndConsoleVersionMismatch"));
            }
            else
            {
                _disableFilterFunction = true;
                _selectedUpdates.Clear();
                if (trvCatalog.Nodes.Count != 0)
                {
                    GetSelectedUpdates(trvCatalog.Nodes[0]);

                    int totalSelectedUpdates;
                    do
                    {
                        totalSelectedUpdates = _selectedUpdates.Count;
                        IncludePrerequisites();
                    } while (totalSelectedUpdates != _selectedUpdates.Count);

                    SortSelectedUpdates();

                    if (_selectedUpdates.Count != 0)
                    {
                        FrmCatalogUpdateImporter importer = new FrmCatalogUpdateImporter(_selectedUpdates, chkBxMakeLanguageIndependent.Checked, catalogHelper.DestinationPath);
                        importer.ShowInWsusConsole = this.ShowInWsusConsole;
                        importer.ShowDialog();
                    }
                    else
                        MessageBox.Show(resMan.GetString("SelectAtLeastOnePackage"));
                }
                _disableFilterFunction = false;
            }
        }

        private void cmbBxFilterCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void txtBxFilterText_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            UnFilterNodes();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DisplayData();
        }

        private void txtBxFilterText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnFilter.PerformClick();
            }
        }

        private void cmbBxAdditionnalInformation_Click(object sender, EventArgs e)
        {
            try
            {
                string url = this.cmbBxAdditionnalInformation.SelectedItem.ToString();
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(url);
                System.Diagnostics.Process.Start(procInfo);
            }
            catch (Exception) { }
        }

        private void txtBxSupportUrl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string url = this.txtBxSupportUrl.Text;
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(url);
                System.Diagnostics.Process.Start(procInfo);
            }
            catch (Exception) { }
        }

        #endregion (Responses to events - Réponses aux événements)
    }
}
