using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace Wsus_Package_Publisher
{
    internal partial class FrmCatalogSubscription : Form
    {
        private const string _subscribedCatalogsFolder = "Subscribed Catalogs";
        private const string _sharedCatalogsFolder = "Shared Catalogs";
        private List<CatalogSubscription> _catalogSubscriptions = new List<CatalogSubscription>();
        private List<CatalogSubscription> _updatedCatalogs = new List<CatalogSubscription>();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmWsusPackagePublisher).Assembly);

        internal FrmCatalogSubscription()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            Tools.Utilities _utilities = Tools.Utilities.GetInstance();

            InitializeComponent();

            cmbBxCheckEvery.Items.Add(_utilities.GetLocalizedString("Days"));
            cmbBxCheckEvery.Items.Add(_utilities.GetLocalizedString("Weeks"));
            cmbBxCheckEvery.Items.Add(_utilities.GetLocalizedString("Months"));

            CheckingTerminated = true;
            AbortChecking = false;
        }

        #region (internal Properties - Propriétées internes)

        internal bool IsUpdateAvailable { get; private set; }

        internal bool CheckingTerminated { get; private set; }

        internal bool AbortChecking { get; set; }

        internal List<CatalogSubscription> UpdatedCatalogs { get { return _updatedCatalogs; } }

        #endregion (internal Properties - Propriétées internes)

        #region {internal Methods - Méthodes internes}

        internal void CheckSubscriptions()
        {
            Logger.EnteringMethod();

            _catalogSubscriptions.Clear();
            _updatedCatalogs.Clear();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            try
            {
                CheckingTerminated = false;
                IsUpdateAvailable = false;

                DirectoryInfo folderToSearch = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder + "\\MetaData");
                if (!folderToSearch.Exists)
                    folderToSearch.Create();
                foreach (FileInfo file in folderToSearch.GetFiles())
                {
                    if (file.Extension.ToLower() == ".xml")
                        _catalogSubscriptions.Add(GetCatalogSubscription(file));
                }

                System.Threading.Tasks.Parallel.ForEach<CatalogSubscription>(_catalogSubscriptions, subscription =>
                {
                    if (!AbortChecking && subscription.IsActive && subscription.IsTimeToCheck())
                    {
                        if (subscription.CheckUpdateAvailability())
                        {
                            Logger.Write("An update is available for the catalog : " + subscription.Address + "/" + subscription.CatalogName);
                            lock (_updatedCatalogs)
                            {
                                IsUpdateAvailable = true;
                                _updatedCatalogs.Add(subscription);
                            }
                        }
                        subscription.Save(folderToSearch.FullName);
                    }
                });

                CheckingTerminated = true;
                
                if (CheckingSubscriptionTerminated != null && !AbortChecking)
                    CheckingSubscriptionTerminated();
            }
            catch (System.Threading.ThreadAbortException)
            {
                AbortChecking = true;
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
        }

        #endregion {internal Methods - Méthodes internes}

        #region {private Methods - Méthodes privées}

        /// <summary>
        /// Parse the XML file and create a CatalogSubscription object.
        /// </summary>
        /// <param name="subscriptionFile">The file with informations to create the CatalogSubscription object.</param>
        /// <returns>If the file contains all informations for creating the CatalogSubscription, return this CatalogSubscription. Otherwise return an Default CatalogSubscription.</returns>
        private CatalogSubscription GetCatalogSubscription(FileInfo subscriptionFile)
        {
            Logger.EnteringMethod(subscriptionFile.FullName);

            CatalogSubscription catalogSubscription = new CatalogSubscription();

            StreamReader streamReader = null;

            try
            {
                streamReader = new StreamReader(subscriptionFile.FullName);
                XmlReader reader = XmlReader.Create(streamReader);

                if (reader.ReadToFollowing("CatalogSubscription"))
                {
                    if (reader.ReadToFollowing("IsActive"))
                        catalogSubscription.IsActive = Convert.ToBoolean(reader.ReadString());
                    if (reader.ReadToFollowing("Address"))
                        catalogSubscription.Address = reader.ReadString();
                    if (reader.ReadToFollowing("CheckEvery"))
                        catalogSubscription.CheckEvery = reader.ReadElementContentAsInt();
                    if (reader.ReadToFollowing("Unit"))
                        catalogSubscription.Unit = (CatalogSubscription.CheckingUnits)Enum.Parse(typeof(CatalogSubscription.CheckingUnits), reader.ReadString(), true);
                    if (reader.ReadToFollowing("LastCheck"))
                        catalogSubscription.LastCheckDate = Convert.ToDateTime(reader.ReadString());
                    if (reader.ReadToFollowing("LastCheckResult"))
                        catalogSubscription.LastCheckResult = Convert.ToBoolean(reader.ReadString());
                    if (reader.ReadToFollowing("CatalogName"))
                        catalogSubscription.CatalogName = reader.ReadString();
                    if (reader.ReadToFollowing("Hash"))
                        catalogSubscription.Hash = reader.ReadString();
                    if (reader.ReadToFollowing("LastDownloadDate"))
                        catalogSubscription.LastDownloadDate = Convert.ToDateTime(reader.ReadString());
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
            finally
            {
                if (streamReader != null)
                    streamReader.Close();
            }

            return catalogSubscription;
        }

        private void ValidateData()
        {
            string addr = txtBxAddress.Text.ToLower();

            btnAdd.Enabled = !string.IsNullOrEmpty(txtBxAddress.Text) && cmbBxCheckEvery.SelectedIndex != -1 && (addr.StartsWith("http://") || addr.StartsWith("https://") || addr.StartsWith("ftp://") || addr.StartsWith("ftps://")) && !string.IsNullOrEmpty(txtBxFileName.Text) &&
                (txtBxFileName.Text.ToLower().EndsWith(".xml") || txtBxFileName.Text.ToLower().EndsWith(".cab"));

            btnTest.Enabled = (addr.StartsWith("http://") || addr.StartsWith("https://") || addr.StartsWith("ftp://") || addr.StartsWith("ftps://")) && !string.IsNullOrEmpty(txtBxFileName.Text) &&
                (txtBxFileName.Text.ToLower().EndsWith(".xml") || txtBxFileName.Text.ToLower().EndsWith(".cab"));

            btnCheckUpdate.Enabled = btnTest.Enabled && dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1;

            btnModify.Enabled = btnAdd.Enabled && dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1;

            btnRemove.Enabled = dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1;

            btnImportUpdates.Enabled = btnCheckUpdate.Enabled;
        }

        private void AddCatalogToDataGridView(CatalogSubscription catalogToAdd)
        {

            int index = dgvSubscriptions.Rows.Add();
            DataGridViewRow row = dgvSubscriptions.Rows[index];

            row.Cells["Active"].Value = catalogToAdd.IsActive;
            row.Cells["Address"].Value = catalogToAdd.Address;
            row.Cells["FileName"].Value = catalogToAdd.CatalogName;
            row.Cells["CheckEvery"].Value = catalogToAdd.CheckEvery.ToString();
            row.Cells["Unit"].Value = resMan.GetString(catalogToAdd.Unit.ToString());
            row.Cells["Subscription"].Value = catalogToAdd;

            dgvSubscriptions.ClearSelection();
            row.Selected = true;

            ValidateData();
        }

        private void ShareCatalog(object catalogFullPath)
        {
            string fullPath = (string)catalogFullPath;
            Logger.EnteringMethod(fullPath);
            FileInfo fileToUpload = new FileInfo(fullPath);
            FtpClient ftpClient = new FtpClient("ftp://s484673217.onlinehome.fr/SharedCatalog", "u74323746", @"##ed78Qh@ec2qUja()62-eLvPaQ+");

            try
            {
                DateTime date = ftpClient.GetFileCreatedDateTime(fileToUpload.Name);
                Logger.Write("The file is already shared.");
            }
            catch (Exception) 
            {
                if (fileToUpload.Exists)
                {
                    Logger.Write("Uploading file.");
                    ftpClient.Upload(fileToUpload.Name, fullPath);
                    Tools.Utilities.DeleteFolder(fileToUpload.DirectoryName);
                }
            }
        }

        #endregion {private Methods - Méthodes privées}

        #region {Responses to Event - Réponses aux événements}

        private void FrmCatalogSubscription_Shown(object sender, EventArgs e)
        {
            Action action = () =>
                {
                    dgvSubscriptions.Rows.Clear();
                    foreach (CatalogSubscription subscription in _catalogSubscriptions)
                    {
                        dgvSubscriptions.Rows.Add(
                        subscription.IsActive,
                        subscription.Address,
                        subscription.CatalogName,
                        subscription.CheckEvery.ToString(),
                        resMan.GetString(subscription.Unit.ToString()),
                        subscription.LastCheckDate.Date.ToString("d", System.Globalization.CultureInfo.CurrentUICulture),
                        subscription.LastCheckResult ? resMan.GetString("Succeeded") : resMan.GetString("Failed"),
                        subscription.IsUpdateAvailable ? resMan.GetString("Yes") : resMan.GetString("No"),
                        subscription.LastDownloadDate.Date.ToString("d", System.Globalization.CultureInfo.CurrentUICulture),
                        subscription
                        );
                    }
                    txtBxAddress.SelectAll();
                };
            if (!this.IsDisposed && !this.Disposing)
                this.Invoke(action);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder + "\\MetaData";

            foreach (DataGridViewRow row in dgvSubscriptions.Rows)
            {
                CatalogSubscription subscriptionToSave = (CatalogSubscription)row.Cells["Subscription"].Value;

                subscriptionToSave.IsActive = (bool)row.Cells["Active"].Value;
                subscriptionToSave.Save(folder);
            }

            this.Close();
        }

        private void btnLoadCatalog_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            DirectoryInfo folderToSearch = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _sharedCatalogsFolder);
            if (!folderToSearch.Exists)
                folderToSearch.Create();
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.AddExtension = true;
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            openFile.DefaultExt = ".xml";
            openFile.FileName = "";
            openFile.Filter = "Xml files|*.xml";
            openFile.InitialDirectory = folderToSearch.FullName;
            openFile.Multiselect = false;
            openFile.ValidateNames = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CatalogSubscription sharedCatalog = GetCatalogSubscription(new FileInfo(openFile.FileName));
                AddCatalogToDataGridView(sharedCatalog);
                _catalogSubscriptions.Add(sharedCatalog);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CatalogSubscription newCatalog = new CatalogSubscription();

            newCatalog.Address = txtBxAddress.Text;
            newCatalog.CatalogName = txtBxFileName.Text;
            newCatalog.CheckEvery = (int)nupCheckEvery.Value;

            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Days"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Days;
            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Weeks"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Weeks;
            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Months"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Months;

            AddCatalogToDataGridView(newCatalog);
            _catalogSubscriptions.Add(newCatalog);

            if (chkBxShareCatalogAddress.Checked)
            {
                string tempFileName = Tools.Utilities.GetTempFolder() + txtBxFileName.Text + ".txt";
                FileInfo fileToUpload = new FileInfo(tempFileName);
                if (fileToUpload.Exists)
                    fileToUpload.Delete();
                StreamWriter writer = fileToUpload.CreateText();
                writer.WriteLine(txtBxAddress.Text);
                writer.WriteLine(txtBxFileName.Text);
                writer.Flush();
                writer.Close();
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.ShareCatalog));
                t.Start(tempFileName);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CatalogSubscription newCatalog = new CatalogSubscription();
            DataGridViewRow row = dgvSubscriptions.SelectedRows[0];

            newCatalog.Address = txtBxAddress.Text;
            newCatalog.CatalogName = txtBxFileName.Text;
            newCatalog.CheckEvery = (int)nupCheckEvery.Value;

            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Days"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Days;
            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Weeks"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Weeks;
            if (cmbBxCheckEvery.SelectedItem.ToString() == resMan.GetString("Months"))
                newCatalog.Unit = CatalogSubscription.CheckingUnits.Months;

            row.Cells["Address"].Value = txtBxAddress.Text;
            row.Cells["FileName"].Value = txtBxFileName.Text;
            row.Cells["CheckEvery"].Value = nupCheckEvery.Value.ToString();
            row.Cells["Unit"].Value = cmbBxCheckEvery.SelectedItem.ToString();
            row.Cells["Subscription"].Value = newCatalog;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1)
            {
                if (MessageBox.Show(resMan.GetString("DeletionCannotBeCancel"), string.Empty, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    CatalogSubscription subscriptionToDelete = (CatalogSubscription)dgvSubscriptions.SelectedRows[0].Cells["Subscription"].Value;
                    string folder = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder;

                    subscriptionToDelete.DeleteFromDisk(folder);
                    _catalogSubscriptions.Remove(subscriptionToDelete);
                    dgvSubscriptions.Rows.Remove(dgvSubscriptions.SelectedRows[0]);
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            this.Cursor = Cursors.WaitCursor;

            try
            {
                btnAdd.Enabled = false;
                btnModify.Enabled = false;
                btnRemove.Enabled = false;
                btnTest.Enabled = false;
                btnCancel.Enabled = false;
                btnOk.Enabled = false;

                CatalogSubscription subscription = new CatalogSubscription();

                subscription.Address = txtBxAddress.Text;
                subscription.CatalogName = txtBxFileName.Text;

                Logger.Write("Will test : " + subscription.Address + "/" + subscription.CatalogName);

                if (subscription.TestConnectivity())
                    MessageBox.Show(resMan.GetString("Succeeded"));
                else
                    MessageBox.Show(resMan.GetString("Failed"));
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                MessageBox.Show(resMan.GetString("Failed"));
            }

            ValidateData();
            btnCancel.Enabled = true;
            btnOk.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            btnCheckUpdate.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1)
            {
                if ((dgvSubscriptions.SelectedRows[0].Cells["Subscription"].Value as CatalogSubscription).CheckUpdateAvailability())
                {
                    MessageBox.Show(resMan.GetString("UpdateAvailable"));
                    dgvSubscriptions.SelectedRows[0].Cells["UpdateAvailable"].Value = resMan.GetString("Yes");
                    dgvSubscriptions.SelectedRows[0].Cells["LastCheckDate"].Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    dgvSubscriptions.SelectedRows[0].Cells["LastDownloadDate"].Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    dgvSubscriptions.SelectedRows[0].Cells["LastCheckResult"].Value = resMan.GetString("Succeeded");
                }
                else
                {
                    MessageBox.Show(resMan.GetString("NoUpdateAvailable"));
                    dgvSubscriptions.SelectedRows[0].Cells["UpdateAvailable"].Value = resMan.GetString("No");
                    dgvSubscriptions.SelectedRows[0].Cells["LastCheckDate"].Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    dgvSubscriptions.SelectedRows[0].Cells["LastCheckResult"].Value = resMan.GetString("Succeeded");
                }
            }

            btnCheckUpdate.Enabled = true;
            this.Cursor = Cursors.Default;
        }

        private void btnImportUpdates_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();

            this.Cursor = Cursors.WaitCursor;

            if (dgvSubscriptions.SelectedRows != null && dgvSubscriptions.SelectedRows.Count == 1)
            {
                CatalogSubscription tempSubscription = new CatalogSubscription();
                string catalogFullPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\" + _subscribedCatalogsFolder + "\\" + txtBxFileName.Text;

                tempSubscription.Address = txtBxAddress.Text;
                tempSubscription.CatalogName = txtBxFileName.Text;

                if (!File.Exists(catalogFullPath))
                {
                    try
                    {
                        tempSubscription.DownloadFile(catalogFullPath);
                    }
                    catch (Exception ex)
                    {
                        Logger.Write("**** " + ex.Message);
                        System.Windows.Forms.MessageBox.Show(resMan.GetString("UnableToDownloadTheFile"));
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                FrmImportCatalog frmImportCatalog = new FrmImportCatalog(catalogFullPath);
                frmImportCatalog.ShowDialog();
            }
            this.Cursor = Cursors.Default;
        }

        private void dgvSubscriptions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnImportUpdates.PerformClick();
        }

        private void txtBxAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxAddress.Text) && (txtBxAddress.Text.ToLower().EndsWith(".xml") || txtBxAddress.Text.ToLower().EndsWith(".cab")) && txtBxAddress.Text.LastIndexOf('/') != -1)
            {
                txtBxFileName.Text = txtBxAddress.Text.Substring(txtBxAddress.Text.LastIndexOf('/') + 1);
                txtBxAddress.Text = txtBxAddress.Text.Substring(0, txtBxAddress.Text.LastIndexOf('/'));
            }
            ValidateData();
        }

        private void txtBxFileName_TextChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        private void dgvSubscriptions_SelectionChanged(object sender, EventArgs e)
        {
            if ((dgvSubscriptions.SelectedRows != null) && (dgvSubscriptions.SelectedRows.Count == 1) && dgvSubscriptions.SelectedRows[0].Cells["Address"].Value != null)
            {
                btnRemove.Enabled = true;
                btnModify.Enabled = true;
                DataGridViewRow selectedRow = dgvSubscriptions.SelectedRows[0];

                txtBxAddress.Text = selectedRow.Cells["Address"].Value.ToString();
                txtBxFileName.Text = selectedRow.Cells["FileName"].Value.ToString();
                nupCheckEvery.Value = Convert.ToDecimal(selectedRow.Cells["CheckEvery"].Value.ToString());
                cmbBxCheckEvery.SelectedItem = selectedRow.Cells["Unit"].Value.ToString();
            }
            else
            {
                btnRemove.Enabled = false;
                btnModify.Enabled = false;
            }
        }

        private void cmbBxCheckEvery_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateData();
        }

        #endregion {Responses to Event - Réponses aux événements}

        #region (Events - Evenements)

        public delegate void CheckingSubscriptionTerminatedDelegate();
        public event CheckingSubscriptionTerminatedDelegate CheckingSubscriptionTerminated;

        #endregion (Events - Evenements)

    }
}
