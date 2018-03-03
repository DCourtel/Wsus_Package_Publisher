using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateWizard : Form
    {
        private FrmUpdateRulesWizard updateIsInstalledRulesWizard = new FrmUpdateRulesWizard();
        private Dictionary<Guid, Company> _companies;
        private FrmUpdateFilesWizard updateFilesWizard = new FrmUpdateFilesWizard();
        private FrmUpdateInformationsWizard updateInformationsWizard;
        private FrmUpdateApplicabilityMetadata updateApplicabilityMetadata = new FrmUpdateApplicabilityMetadata();
        private SoftwareDistributionPackage _sdp;
        private bool _revising = false;
        private bool _inferFromMsiProperties = true;
        private bool _creatingSupersedingUpdate = false;
        private FrmUpdateRulesWizard updateIsInstallableRulesWizard = new FrmUpdateRulesWizard();

        private System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateWizard).Assembly);

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();
            InitializeComponent(Companies, null, null, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, string updateFile)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, null, null, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, Company SelectedCompany, Product SelectedProduct, string updateFile)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();
            InferFromMsiProperties = false;
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, SelectedCompany, SelectedProduct, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, Company SelectedCompany, string updateFile)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();
            InferFromMsiProperties = false;
            updateFilesWizard = new FrmUpdateFilesWizard(updateFile);
            InitializeComponent(Companies, SelectedCompany, null, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, Company SelectedCompany, Product SelectedProduct)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();

            InitializeComponent(Companies, SelectedCompany, SelectedProduct, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, Company SelectedCompany)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();

            InitializeComponent(Companies, SelectedCompany, null, null);
        }

        internal FrmUpdateWizard(Dictionary<Guid, Company> Companies, IUpdate updateToRevise)
        {
            Logger.EnteringMethod("FrmUpdateWizard");
            InitializeComponent();
            WsusWrapper _wsus = WsusWrapper.GetInstance();
            Revising = true;
            this.Sdp = _wsus.GetMetaData(updateToRevise);
            this.Companies = Companies;
            IUpdateCategory productCategory = updateToRevise.GetUpdateCategories()[0];
            IUpdateCategory companyCategory = productCategory.GetParentUpdateCategory();

            InitializeComponent(this.Companies, Companies[companyCategory.Id], Companies[companyCategory.Id].Products[productCategory.Id], Sdp);

        }

        internal bool CustomUpdate { private get; set; }

        internal string ActionsFilePath { private get; set; }

        private void InitializeComponent(Dictionary<Guid, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod();

            this.Companies = Companies;
            updateInformationsWizard = new FrmUpdateInformationsWizard(this.Companies, SelectedCompany, SelectedProduct, sdp);

            // UpdateFilesWizard :
            updateFilesWizard.TopLevel = false;
            updateFilesWizard.Controls["btnNext"].Click += new EventHandler(updateFilesWizard_btnNext_Click);
            updateFilesWizard.Controls["btnCancel"].Click += new EventHandler(updateFilesWizard_btnCancel_Click);

            //UpdateInformationsWizard :
            updateInformationsWizard.TopLevel = false;
            updateInformationsWizard.Controls["btnNext"].Click += new EventHandler(updateInformationsWizard_btnNext_Click);
            updateInformationsWizard.Controls["btnCancel"].Click += new EventHandler(updateInformationsWizard_btnCancel_Click);
            updateInformationsWizard.Controls["btnPrevious"].Click += new EventHandler(updateInformationsWizard_btnPrevious_Click);


            //updateIsInstalledRulesWizard :
            updateIsInstalledRulesWizard.TopLevel = false;
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Click += new EventHandler(updateIsInstalledRulesWizard_btnNext_Click);
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnCancel"].Click += new EventHandler(updateIsInstalledRulesWizard_btnCancel_Click);
            updateIsInstalledRulesWizard.Controls["tableLayoutPanel1"].Controls["btnPrevious"].Click += new EventHandler(updateIsInstalledRulesWizard_btnPrevious_Click);

            //updateIsInstallableRulesWizard :
            updateIsInstallableRulesWizard.TopLevel = false;
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnNext"].Click += new EventHandler(updateIsInstallableRulesWizard_btnNext_Click);
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnCancel"].Click += new EventHandler(updateIsInstallableRulesWizard_btnCancel_Click);
            updateIsInstallableRulesWizard.Controls["tableLayoutPanel1"].Controls["btnPrevious"].Click += new EventHandler(updateIsInstallableRulesWizard_btnPrevious_Click);

            //updateApplicabilityMetadata :
            updateApplicabilityMetadata.TopLevel = false;
            updateApplicabilityMetadata.Controls["btnPublish"].Click += new EventHandler(updateApplicabilityMetadata_btnPublish_Click);
            updateApplicabilityMetadata.Controls["btnCancel"].Click += new EventHandler(updateApplicabilityMetadata_btnCancel_Click);
            updateApplicabilityMetadata.Controls["btnPrevious"].Click += new EventHandler(updateApplicabilityMetadata_btnPrevious_Click);

            if (Revising)
                InitializeInformationsWizard();
            else
                InitializeUpdateFilesWizard();
        }

        internal void CreateSupersedingUpdate(IUpdate updateToSupersed)
        {
            Logger.EnteringMethod(updateToSupersed.Title);
            CreatingSupersedingUpdate = true;
            this.Sdp = WsusWrapper.GetInstance().GetMetaData(updateToSupersed);
            InitializeUpdateFilesWizard();
        }

        private void InitializeUpdateFilesWizard()
        {
            Logger.EnteringMethod();
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionUpdateFileWizard");

            updateFilesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateFilesWizard);
            updateFilesWizard.Show();
            this.Size = new System.Drawing.Size(updateFilesWizard.Width + 20, txtBxDescription.Height + updateFilesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateFilesWizard.Dock = DockStyle.Fill;
            updateFilesWizard.Select();
            updateApplicabilityMetadata.OriginalText = string.Empty;
        }

        private void updateFilesWizard_btnNext_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateFilesWizard.Hide();
            if (updateFilesWizard.FileType == FrmUpdateFilesWizard.UpdateType.WindowsInstaller)
            {
                MsiReader.MsiReader msiReader = new MsiReader.MsiReader();
                msiReader.MsiFilePath = updateFilesWizard.UpdateFileName;
                string msiCode = msiReader.GetProductCode();
                string MsiVendorName = msiReader.GetManufacturer();
                string MsiProductName = msiReader.GetProductName();
                Logger.Write("Msi Code : " + msiCode);
                Logger.Write("Msi Vendor : " + MsiVendorName);
                Logger.Write("MSi Product : " + MsiProductName);
                if (!string.IsNullOrEmpty(msiCode))
                {
                    try
                    {
                        Clipboard.SetText(msiCode);
                    }
                    catch (Exception) { }

                    if (!CreatingSupersedingUpdate)
                    {
                        updateIsInstalledRulesWizard.InitializeUpdateLevelwithXml("<msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/>");
                        updateIsInstallableRulesWizard.InitializeUpdateLevelwithXml("<lar:Not><msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/></lar:Not>");
                    }
                    else
                    {
                        updateIsInstalledRulesWizard.InitializeWithXml("<msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/>", Sdp.IsInstalled, true);
                        updateIsInstallableRulesWizard.InitializeWithXml("<lar:Not><msiar:MsiProductInstalled ProductCode=\"" + msiCode + "\"/></lar:Not>", Sdp.IsInstallable, true);
                    }
                }
                bool foundVendor = false;

                if (InferFromMsiProperties && !CreatingSupersedingUpdate)
                {
                    foreach (Company company in _companies.Values)
                    {
                        if (company.CompanyName.ToLower() == MsiVendorName.ToLower())
                        {
                            Logger.Write("Vendor found : " + company.CompanyName);
                            updateInformationsWizard.InitializeVendorName(company.CompanyName);
                            foundVendor = true;
                            break;
                        }
                    }
                    if (!foundVendor)
                        updateInformationsWizard.InitializeVendorName(MsiVendorName);
                }
                if (!CreatingSupersedingUpdate)
                    updateInformationsWizard.Title = MsiProductName;
                RemoveTransformsCommand();
                if (updateFilesWizard.HaveMSTFile)
                {
                    Logger.Write("Have MST file ! ");
                    string command = string.Empty;
                    command = "TRANSFORMS=";
                    foreach (string file in updateFilesWizard.AdditionnalFileName)
                    {
                        if (file.ToLower().EndsWith(".mst"))
                        {
                            command += "\"" + GetShortFile(file) + "\";";
                        }
                    }
                    if (command.EndsWith(";"))
                        command = command.Substring(0, command.Length - 1);
                    updateInformationsWizard.CommandLine = (updateInformationsWizard.CommandLine != string.Empty) ? command + " " + updateInformationsWizard.CommandLine : command;
                }
            }
            else
            {
                if (!CreatingSupersedingUpdate)
                {
                    updateIsInstalledRulesWizard.InitializeUpdateLevelwithXml("");
                    updateIsInstallableRulesWizard.InitializeUpdateLevelwithXml("");
                }
                else
                {
                    updateIsInstalledRulesWizard.InitializeUpdateLevelwithXml(Sdp.IsInstalled);
                    updateIsInstallableRulesWizard.InitializeUpdateLevelwithXml(Sdp.IsInstallable);
                }
            }
            InitializeInformationsWizard();
        }

        private void RemoveTransformsCommand()
        {
            Logger.EnteringMethod();
            if (updateInformationsWizard.CommandLine.Contains("TRANSFORMS=\""))
            {
                System.Text.RegularExpressions.Regex regexp = new System.Text.RegularExpressions.Regex("TRANSFORMS=\".+\\.mst\"", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.MatchCollection matches = regexp.Matches(updateInformationsWizard.CommandLine);
                if (matches != null)
                {
                    foreach (System.Text.RegularExpressions.Match match in matches)
                    {
                        updateInformationsWizard.CommandLine = updateInformationsWizard.CommandLine.Replace(match.Groups[0].Captures[0].Value, string.Empty);
                    }
                    updateInformationsWizard.CommandLine = updateInformationsWizard.CommandLine.TrimStart();
                    updateInformationsWizard.CommandLine = updateInformationsWizard.CommandLine.TrimEnd();
                }
            }
        }

        private string GetShortFile(string file)
        {
            Logger.EnteringMethod(file);
            return file.Substring(file.LastIndexOf('\\') + 1);
        }

        private void updateFilesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CleanAndClose();
        }

        private void InitializeInformationsWizard()
        {
            Logger.EnteringMethod(Revising.ToString());
            if (Revising)
                updateInformationsWizard.Controls["btnPrevious"].Enabled = false;
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionInformationsWizard");

            updateInformationsWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateInformationsWizard);
            if (CreatingSupersedingUpdate)
                updateInformationsWizard.CopyInformations(this.Sdp);
            updateInformationsWizard.Show();
            this.Size = new System.Drawing.Size(updateInformationsWizard.Width + 20, txtBxDescription.Height + updateInformationsWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateInformationsWizard.Dock = DockStyle.Fill;
            if (CustomUpdate)
            {
                string filename = ActionsFilePath.Substring(ActionsFilePath.LastIndexOf('\\') + 1);
                updateInformationsWizard.CommandLine = @"\actionfile=" + filename;
            }
            updateInformationsWizard.Select();
        }

        private void updateInformationsWizard_btnNext_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateInformationsWizard.Hide();
            InitializeUpdateIsInstalledRulesWizard();
        }

        private void updateInformationsWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateInformationsWizard.Hide();
            InitializeUpdateFilesWizard();
        }

        private void updateInformationsWizard_btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CleanAndClose();
        }

        private void InitializeUpdateIsInstalledRulesWizard()
        {
            Logger.EnteringMethod();
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionIsInstalledWizard");

            updateIsInstalledRulesWizard.Dock = DockStyle.Top;
            splitContainer1.Panel2.Controls.Add(updateIsInstalledRulesWizard);
            if (Revising && !updateIsInstalledRulesWizard.IsAlreadyInitialized)
            {
                updateIsInstalledRulesWizard.InitializeUpdateLevelwithXml(Sdp.IsInstalled);
                if (Sdp.InstallableItems.Count != 0)
                    updateIsInstalledRulesWizard.InitializePackageLevelwithXml(Sdp.InstallableItems[0].IsInstalledApplicabilityRule);
            }
            updateIsInstalledRulesWizard.IsAlreadyInitialized = true;
            updateIsInstalledRulesWizard.Show();
            this.Size = new System.Drawing.Size(updateIsInstalledRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstalledRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateIsInstalledRulesWizard.Dock = DockStyle.Fill;
            updateIsInstalledRulesWizard.Select();
        }

        private void updateIsInstalledRulesWizard_btnNext_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateIsInstalledRulesWizard.Hide();
            InitializeUpdateIsInstallableRulesWizard();
        }

        private void updateIsInstalledRulesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CleanAndClose();
        }

        private void updateIsInstalledRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateIsInstalledRulesWizard.Hide();
            InitializeInformationsWizard();
        }

        private void InitializeUpdateIsInstallableRulesWizard()
        {
            Logger.EnteringMethod();
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionIsInstallableWizard");

            if (Revising)
            {
                if (!updateIsInstallableRulesWizard.IsAlreadyInitialized)
                {
                    updateIsInstallableRulesWizard.InitializeUpdateLevelwithXml(Sdp.IsInstallable);
                    if (Sdp.InstallableItems.Count != 0)
                        updateIsInstallableRulesWizard.InitializePackageLevelwithXml(Sdp.InstallableItems[0].IsInstallableApplicabilityRule);
                }
            }
            updateIsInstallableRulesWizard.IsAlreadyInitialized = true;
            updateIsInstallableRulesWizard.Dock = DockStyle.None;
            splitContainer1.Panel2.Controls.Add(updateIsInstallableRulesWizard);
            updateIsInstallableRulesWizard.Show();
            this.Size = new System.Drawing.Size(updateIsInstallableRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstallableRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updateIsInstallableRulesWizard.Dock = DockStyle.Fill;
            updateIsInstallableRulesWizard.Select();
        }

        private void updateIsInstallableRulesWizard_btnNext_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateIsInstallableRulesWizard.Hide();
            InitializeUpdateApplicabilityMetadata();
        }

        private void updateIsInstallableRulesWizard_btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CleanAndClose();
        }

        private void updateIsInstallableRulesWizard_btnPrevious_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateIsInstallableRulesWizard.Hide();
            InitializeUpdateIsInstalledRulesWizard();
        }

        private void InitializeUpdateApplicabilityMetadata()
        {
            Logger.EnteringMethod();
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = resManager.GetString("DescriptionApplicabilityMetadata");
            updateApplicabilityMetadata.Dock = DockStyle.Top;
            splitContainer1.Panel2.Controls.Add(updateApplicabilityMetadata);
            if (Revising)
            {
                updateApplicabilityMetadata.Controls["btnPublish"].Text = resManager.GetString("Revise");
            }
            else
            {
                updateApplicabilityMetadata.Controls["btnPublish"].Text = resManager.GetString("Publish");
            }
            if (string.IsNullOrEmpty(updateApplicabilityMetadata.Metadata))
            {
                WsusWrapper _wsus = WsusWrapper.GetInstance();
                if (!Revising)
                {
                    updateApplicabilityMetadata.OriginalText = _wsus.GetApplicabilityMetadata(updateFilesWizard);
                }
                else
                    updateApplicabilityMetadata.OriginalText = Sdp.InstallableItems[0].ApplicabilityMetadata;
            }
            updateApplicabilityMetadata.Show();
            this.Size = new System.Drawing.Size(updateApplicabilityMetadata.Width + 20, txtBxDescription.Height + updateApplicabilityMetadata.Height + 2 * SystemInformation.CaptionHeight);
            updateApplicabilityMetadata.Dock = DockStyle.Fill;
            updateApplicabilityMetadata.Select();
        }

        private void updateApplicabilityMetadata_btnPublish_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2.Controls.Clear();
            txtBxDescription.Text = string.Empty;
            FrmUpdatePublisher updatePublisher = new FrmUpdatePublisher(updateFilesWizard, updateInformationsWizard, updateIsInstalledRulesWizard, updateIsInstallableRulesWizard, updateApplicabilityMetadata);
            updatePublisher.Controls["btnOk"].Click += new EventHandler(this.Close);
            updatePublisher.TopLevel = false;
            splitContainer1.Panel2.Controls.Add(updatePublisher);
            updatePublisher.Show();
            this.Size = new System.Drawing.Size(updateIsInstalledRulesWizard.Width + 20, txtBxDescription.Height + updateIsInstalledRulesWizard.Height + 2 * SystemInformation.CaptionHeight);
            updatePublisher.Dock = DockStyle.Fill;
            updatePublisher.Select();
            if (Revising)
            {
                if (updateApplicabilityMetadata.EditMetadata)
                    this.Sdp.InstallableItems[0].ApplicabilityMetadata = string.IsNullOrEmpty(updateApplicabilityMetadata.Metadata) ? null : updateApplicabilityMetadata.Metadata;
                updatePublisher.Revise(this.Sdp);
            }
            else
                updatePublisher.Publish();
        }

        private void updateApplicabilityMetadata_btnCancel_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            CleanAndClose();
        }

        private void updateApplicabilityMetadata_btnPrevious_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            updateApplicabilityMetadata.Hide();
            InitializeUpdateIsInstallableRulesWizard();
        }

        private void Close(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            base.Close();
        }

        internal Dictionary<Guid, Company> Companies
        {
            get { return _companies; }
            set { _companies = value; }
        }

        private bool Revising
        {
            get { return _revising; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _revising = value;
            }
        }

        private bool CreatingSupersedingUpdate
        {
            get { return _creatingSupersedingUpdate; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _creatingSupersedingUpdate = value;
            }
        }

        private bool InferFromMsiProperties
        {
            get { return _inferFromMsiProperties; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                _inferFromMsiProperties = value;
            }
        }

        private SoftwareDistributionPackage Sdp
        {
            get { return _sdp; }
            set { _sdp = value; }
        }

        private void FrmUpdateWizard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CleanAndClose();
        }

        private void CleanAndClose()
        {
            Logger.EnteringMethod();
            updateFilesWizard.Dispose();
            updateFilesWizard = null;
            if (updateInformationsWizard != null)
                updateInformationsWizard.Dispose();
            updateInformationsWizard = null;
            updateIsInstalledRulesWizard.Dispose();
            updateIsInstalledRulesWizard = null;
            updateIsInstallableRulesWizard.Dispose();
            updateIsInstallableRulesWizard = null;
            this.Close();
        }

        private void FrmUpdateWizard_Shown(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            this.Location = new Point(this.Location.X, 10);
            string currentFolder = System.Environment.CurrentDirectory;
            Logger.Write("CurrentDirectory is : " + currentFolder);
            if (CustomUpdate && System.IO.File.Exists(currentFolder + @"\CustomUpdateEngine.exe"))
            {
                updateFilesWizard = new FrmUpdateFilesWizard(currentFolder + @"\CustomUpdateEngine.exe");
                updateFilesWizard.AddAdditionnalFile(ActionsFilePath);

                updateFilesWizard.TopLevel = false;
                updateFilesWizard.Controls["btnNext"].Click += new EventHandler(updateFilesWizard_btnNext_Click);
                updateFilesWizard.Controls["btnCancel"].Click += new EventHandler(updateFilesWizard_btnCancel_Click);
                InitializeUpdateFilesWizard();
            }
        }
    }
}
