using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdateInformationsWizard : Form
    {
        private struct SupersedesUpdate
        {
            private IUpdate _update;

            internal SupersedesUpdate(IUpdate update)
            {
                this._update = update;
            }

            internal IUpdate Update
            {
                get { return _update; }
                set { _update = value; }
            }
            public override string ToString()
            {
                if (Update != null)
                    return Update.Title;
                else
                    return string.Empty;
            }
        }

        private struct Prerequisite
        {
            private IUpdate _update;

            internal Prerequisite(IUpdate update)
            {
                this._update = update;
            }

            internal IUpdate Update
            {
                get { return _update; }
                set { _update = value; }
            }
            public override string ToString()
            {
                if (Update != null)
                    return Update.Title;
                else
                    return string.Empty;
            }
        }

        private Dictionary<Guid, Company> _companies;
        private string _vendorName;
        private string _productName;
        private string _description;
        private IList<ReturnCode> _returnCodes = new List<ReturnCode>();
        private ToolTip toolTipAutoApprovalRules = new ToolTip();
        private WsusWrapper _wsus = WsusWrapper.GetInstance();
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateInformationsWizard).Assembly);

        internal FrmUpdateInformationsWizard(Dictionary<Guid, Company> Companies, Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod("FrmUpdateInformationsWizard");
            IsRevising = (sdp != null);

            InitializeComponent();
            this.VisibleChanged += new EventHandler(FrmUpdateInformationsWizard_VisibleChanged);

            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)dtgrvReturnCodes.Columns["Result"];
            column.Items.Add(resMan.GetString("Failed"));
            column.Items.Add(resMan.GetString("Succeeded"));
            column.Items.Add(resMan.GetString("Cancelled"));

            _companies = Companies;
            FillPrerequisites();
            InitializeComponent(SelectedCompany, SelectedProduct, sdp);
            toolTipAutoApprovalRules.IsBalloon = false;
            toolTipAutoApprovalRules.ToolTipIcon = ToolTipIcon.Info;
            toolTipAutoApprovalRules.ToolTipTitle = resMan.GetString("AutoApprovalRules");
            toolTipAutoApprovalRules.UseAnimation = true;
            toolTipAutoApprovalRules.UseFading = true;
        }

        private void FrmUpdateInformationsWizard_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                toolTipAutoApprovalRules.Hide(cmbBxPackageType);
        }

        internal new void Dispose()
        {
            toolTipAutoApprovalRules.Dispose();
        }

        private void InitializeComponent(Company SelectedCompany, Product SelectedProduct, SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod();
            cmbBxUpdateClassification.Items.Clear();
            cmbBxUpdateClassification.Items.AddRange(Enum.GetNames(typeof(PackageUpdateClassification)));
            cmbBxUpdateClassification.SelectedItem = PackageUpdateClassification.Updates.ToString();
            cmbBxPackageType.Items.Clear();
            cmbBxPackageType.Items.AddRange(Enum.GetNames(typeof(PackageType)));
            cmbBxPackageType.SelectedItem = PackageType.Application.ToString();
            cmbBxImpact.Items.Clear();
            cmbBxImpact.Items.AddRange(Enum.GetNames(typeof(InstallationImpact)));
            cmbBxImpact.SelectedItem = InstallationImpact.Normal.ToString();
            cmbBxRebootBehavior.Items.Clear();
            cmbBxRebootBehavior.Items.AddRange(Enum.GetNames(typeof(RebootBehavior)));
            cmbBxRebootBehavior.SelectedItem = RebootBehavior.CanRequestReboot.ToString();
            cmbBxMsrcSeverity.Items.Clear();
            cmbBxMsrcSeverity.Items.AddRange(Enum.GetNames(typeof(SecurityRating)));
            cmbBxMsrcSeverity.SelectedItem = SecurityRating.None.ToString();
            cmbBxVendorName.Select();
            _description = resMan.GetString("NoDescription");
            cmbBxVendorName.Items.Clear();
            foreach (Company company in _companies.Values)
            {
                cmbBxVendorName.Items.Add(company.CompanyName);
            }

            if (SelectedCompany != null)
            {
                Logger.Write(SelectedCompany.CompanyName);
                cmbBxVendorName.SelectedItem = SelectedCompany.CompanyName;
            }

            if (SelectedProduct != null)
            {
                Logger.Write(SelectedProduct.ProductName);
                cmbBxVendorName.SelectedItem = SelectedProduct.Vendor.CompanyName;
                cmbBxProductName.SelectedItem = SelectedProduct.ProductName;
            }

            if (sdp != null)
            {
                cmbBxPackageType.SelectedItem = sdp.PackageType.ToString();
                txtBxTitle.Text = sdp.Title;
                txtBxDescription.Text = FormatDescription(sdp.Description);
                Logger.Write(txtBxTitle);
                Logger.Write(txtBxDescription);
                if (sdp.AdditionalInformationUrls != null && sdp.AdditionalInformationUrls.Count != 0)
                    txtBxMoreInfoURL.Text = sdp.AdditionalInformationUrls[0].ToString();
                if (sdp.SupportUrl != null)
                    txtBxSupportURL.Text = sdp.SupportUrl.ToString();
                if (sdp.InstallableItems != null && sdp.InstallableItems.Count != 0)
                {
                    chkBxCanRequestUserInput.Checked = sdp.InstallableItems[0].InstallBehavior.CanRequestUserInput;
                    chkBxRequiresNetworkConnectivity.Checked = sdp.InstallableItems[0].InstallBehavior.RequiresNetworkConnectivity;
                    cmbBxImpact.SelectedItem = sdp.InstallableItems[0].InstallBehavior.Impact.ToString();
                    cmbBxRebootBehavior.SelectedItem = sdp.InstallableItems[0].InstallBehavior.RebootBehavior.ToString();
                }
                cmbBxUpdateClassification.SelectedItem = sdp.PackageUpdateClassification.ToString();
                if (!string.IsNullOrEmpty(sdp.SecurityBulletinId))
                    txtBxSecurityBulletinId.Text = sdp.SecurityBulletinId;
                cmbBxMsrcSeverity.SelectedItem = sdp.SecurityRating.ToString();
                if (sdp.CommonVulnerabilitiesIds != null && sdp.CommonVulnerabilitiesIds.Count != 0 && !string.IsNullOrEmpty(sdp.CommonVulnerabilitiesIds[0]))
                {
                    chkCmbBxCveID.ClearItems();
                    List<object> cveIDs = new List<object>();

                    foreach (string cve in sdp.CommonVulnerabilitiesIds)
                    {
                        cveIDs.Add(cve);
                    }
                    chkCmbBxCveID.AddRange(cveIDs.ToArray());
                }
                if (!string.IsNullOrEmpty(sdp.KnowledgebaseArticleId))
                    txtBxKBArticleId.Text = sdp.KnowledgebaseArticleId;
                if (sdp.InstallableItems != null && sdp.InstallableItems.Count != 0)
                {
                    Type updateType = sdp.InstallableItems[0].GetType();
                    if (updateType == typeof(CommandLineItem))
                    {
                        CommandLine = (sdp.InstallableItems[0] as CommandLineItem).Arguments;
                        ReturnCodes = (sdp.InstallableItems[0] as CommandLineItem).ReturnCodes;
                    }
                    else
                        if (updateType == typeof(WindowsInstallerItem))
                            CommandLine = (sdp.InstallableItems[0] as WindowsInstallerItem).InstallCommandLine;
                        else
                            if (updateType == typeof(WindowsInstallerPatchItem))
                                CommandLine = (sdp.InstallableItems[0] as WindowsInstallerPatchItem).InstallCommandLine;
                }
                foreach (Guid id in sdp.SupersededPackages)
                {
                    for (int i = 0; i < chkCmbBxSupersedes.Items.Count; i++)
                    {
                        SupersedesUpdate supersededUpdate = (SupersedesUpdate)chkCmbBxSupersedes.Items[i];
                        if (supersededUpdate.Update.Id.UpdateId == id)
                        {
                            chkCmbBxSupersedes.SelectItem(i, true);
                            break;
                        }
                    }
                }
                foreach (PrerequisiteGroup prerequisiteGrp in sdp.Prerequisites)
                    foreach (Guid id in prerequisiteGrp.Ids)
                        for (int i = 0; i < chkCmbBxPrerequisites.Items.Count; i++)
                        {
                            Prerequisite prerequisiteUpdate = (Prerequisite)chkCmbBxPrerequisites.Items[i];
                            if (prerequisiteUpdate.Update.Id.UpdateId == id)
                            {
                                chkCmbBxPrerequisites.SelectItem(i, true);
                                break;
                            }
                        }
            }
        }

        internal void InitializeVendorName(string vendorName)
        {
            Logger.EnteringMethod(vendorName);
            bool found = false;
            foreach (object obj in cmbBxVendorName.Items)
            {
                if (obj.ToString().ToLower() == vendorName.ToLower())
                {
                    Logger.Write("Found : " + obj.ToString());
                    cmbBxVendorName.SelectedItem = obj;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Logger.Write("Not found : " + vendorName);
                cmbBxVendorName.SelectedIndex = -1;
                cmbBxVendorName.Text = vendorName;
            }
        }

        private string FormatDescription(string textToFormat)
        {
            return textToFormat.Replace("\n", "\r\n");
        }

        private void ValidateData(bool vendorOrProductChange)
        {
            btnNext.Enabled = false;

            if ((cmbBxVendorName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxVendorName.Text)) &&
                (cmbBxProductName.SelectedIndex != -1 || !string.IsNullOrEmpty(cmbBxProductName.Text)) &&
                (string.IsNullOrEmpty(txtBxMoreInfoURL.Text) || IsValideURL(txtBxMoreInfoURL.Text)) &&
              (string.IsNullOrEmpty(txtBxSupportURL.Text) || IsValideURL(txtBxSupportURL.Text)))
            {
                if (vendorOrProductChange)
                    EnableSupersedesControl();
                if (!string.IsNullOrEmpty(txtBxTitle.Text))
                    btnNext.Enabled = true;
            }
        }

        private void EnableSupersedesControl()
        {
            Logger.EnteringMethod();
            chkCmbBxSupersedes.Enabled = false;
            chkCmbBxSupersedes.ClearItems();
            List<object> supersedes = new List<object>();
            if (cmbBxVendorName.SelectedItem != null)
            {
                Company selectedCompany = GetCompanyFromName(cmbBxVendorName.SelectedItem.ToString());

                if (cmbBxVendorName.SelectedIndex != -1 && selectedCompany != null && cmbBxProductName.SelectedIndex != -1)
                {
                    chkCmbBxSupersedes.Enabled = true;
                    foreach (Product product in selectedCompany.Products.Values)
                    {
                        if (product.ProductName == cmbBxProductName.SelectedItem.ToString())
                        {
                            foreach (IUpdate update in product.Updates)
                                supersedes.Add(new SupersedesUpdate(update));
                            break;
                        }
                    }
                    chkCmbBxSupersedes.AddRange(supersedes.ToArray());
                }
            }
        }

        private Company GetCompanyFromName(string companyToFind)
        {
            Logger.EnteringMethod(companyToFind);
            foreach (Company company in _companies.Values)
            {
                if (company.CompanyName.ToLower() == companyToFind.ToLower())
                {
                    Logger.Write("Returning : " + company.CompanyName);
                    return company;
                }
            }
            Logger.Write("**** Returning null");
            return null;
        }

        internal List<Guid> GetSupersedes()
        {
            Logger.EnteringMethod();
            List<Guid> result = new List<Guid>();

            foreach (SupersedesUpdate supersededUpdate in chkCmbBxSupersedes.SelectedItems)
                result.Add(supersededUpdate.Update.Id.UpdateId);

            return result;
        }

        private void FillPrerequisites()
        {
            Logger.EnteringMethod();
            chkCmbBxPrerequisites.Enabled = false;
            chkCmbBxPrerequisites.Items.Clear();
            List<object> prerequisites = new List<object>();

            foreach (Company company in _companies.Values)
            {
                foreach (Product product in company.Products.Values)
                {
                    foreach (IUpdate update in product.Updates)
                    {
                        prerequisites.Add(new Prerequisite(update));
                    }
                }
            }
            chkCmbBxPrerequisites.AddRange(prerequisites.ToArray());
            chkCmbBxPrerequisites.Enabled = true;
        }

        internal List<Guid> GetPrerequisites()
        {
            Logger.EnteringMethod();
            List<Guid> result = new List<Guid>();

            foreach (Prerequisite prerequisite in chkCmbBxPrerequisites.SelectedItems)
                result.Add(prerequisite.Update.Id.UpdateId);

            return result;
        }

        private InstallationResult GetInstallationResult(string p)
        {
            Logger.EnteringMethod(p);
            if (p == resMan.GetString("Failed"))
                return InstallationResult.Failed;
            if (p == resMan.GetString("Succeeded"))
                return InstallationResult.Succeeded;
            if (p == resMan.GetString("Cancelled"))
                return InstallationResult.Cancelled;

            return InstallationResult.Failed;
        }

        private int GetReturnCodeValue(string p)
        {
            Logger.EnteringMethod(p);
            int result;
            int.TryParse(p, out result);
            return result;
        }

        internal string CommandLine
        {
            get { return txtBxCommandLine.Text; }
            set
            {
                Logger.Write("Setting command Line to : " + value);
                txtBxCommandLine.Text = value;
            }
        }

        internal IList<ReturnCode> ReturnCodes
        {
            get
            {
                _returnCodes.Clear();

                foreach (DataGridViewRow row in dtgrvReturnCodes.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["Value"].Value != null && !string.IsNullOrEmpty(row.Cells["Value"].Value.ToString()) && row.Cells["Result"].Value != null && !string.IsNullOrEmpty(row.Cells["Result"].Value.ToString()))
                        {
                            ReturnCode currentReturnCode = new ReturnCode();

                            currentReturnCode.ReturnCodeValue = GetReturnCodeValue(row.Cells["Value"].Value.ToString());
                            currentReturnCode.InstallationResult = GetInstallationResult(row.Cells["Result"].Value.ToString());
                            if (row.Cells["NeedReboot"].Value != null)
                                currentReturnCode.IsRebootRequired = (bool)row.Cells["NeedReboot"].Value;

                            _returnCodes.Add(currentReturnCode);
                        }
                    }
                }
                return _returnCodes;
            }
            private set
            {
                foreach (ReturnCode code in value)
                {
                    int rowIndex = dtgrvReturnCodes.Rows.Add();
                    DataGridViewRow row = dtgrvReturnCodes.Rows[rowIndex];
                    row.Cells["Value"].Value = code.ReturnCodeValue.ToString();
                    switch (code.InstallationResult)
                    {
                        case InstallationResult.Cancelled:
                            row.Cells["Result"].Value = resMan.GetString("Cancelled");
                            break;
                        case InstallationResult.Failed:
                            row.Cells["Result"].Value = resMan.GetString("Failed");
                            break;
                        case InstallationResult.Succeeded:
                            row.Cells["Result"].Value = resMan.GetString("Succeeded");
                            break;
                        default:
                            break;
                    }
                    row.Cells["NeedReboot"].Value = code.IsRebootRequired;
                }
            }
        }

        internal string VendorName
        {
            get { return _vendorName; }
            private set
            {
                Logger.EnteringMethod(value);
                _vendorName = value;
            }
        }

        internal new string ProductName
        {
            get { return _productName; }
            private set
            {
                Logger.EnteringMethod(value);
                _productName = value;
            }
        }

        internal string Title
        {
            get { return txtBxTitle.Text; }
            set
            {
                Logger.EnteringMethod(value);
                txtBxTitle.Text = value;
            }
        }

        internal string Description
        {
            get { return _description; }
            private set
            {
                Logger.EnteringMethod(value);
                _description = value;
            }
        }

        internal string UrlMoreInfo
        {
            get
            {
                if (IsValideURL(txtBxMoreInfoURL.Text))
                    return txtBxMoreInfoURL.Text;
                return string.Empty;
            }
            set
            {
                if (IsValideURL(value))
                {
                    Logger.EnteringMethod(value);
                    txtBxMoreInfoURL.Text = value;
                }
            }
        }

        internal string UrlSupport
        {
            get
            {
                if (IsValideURL(txtBxSupportURL.Text))
                    return txtBxSupportURL.Text;
                return string.Empty;
            }
            set
            {
                if (IsValideURL(value))
                {
                    Logger.EnteringMethod(value);
                    txtBxSupportURL.Text = value;
                }
            }
        }

        internal bool CanRequestUserInput
        {
            get { return chkBxCanRequestUserInput.Checked; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                chkBxCanRequestUserInput.Checked = value;
            }
        }

        internal bool CanRequestNetworkConnectivity
        {
            get { return chkBxRequiresNetworkConnectivity.Checked; }
            set
            {
                Logger.EnteringMethod(value.ToString());
                chkBxRequiresNetworkConnectivity.Checked = value;
            }
        }

        internal PackageUpdateClassification UpdateClassification
        {
            get { return (PackageUpdateClassification)Enum.Parse(typeof(PackageUpdateClassification), cmbBxUpdateClassification.SelectedItem.ToString()); }
            set
            {
                Logger.EnteringMethod(value.ToString());
                cmbBxUpdateClassification.SelectedItem = value;
            }
        }

        internal PackageType UpdateType
        {
            get { return (PackageType)Enum.Parse(typeof(PackageType), cmbBxPackageType.SelectedItem.ToString()); }
            set
            {
                Logger.EnteringMethod(value.ToString());
                cmbBxPackageType.SelectedItem = value;
            }
        }

        internal InstallationImpact Impact
        {
            get { return (InstallationImpact)Enum.Parse(typeof(InstallationImpact), cmbBxImpact.SelectedItem.ToString()); }
            set
            {
                Logger.EnteringMethod(value.ToString());
                cmbBxImpact.SelectedItem = value;
            }
        }

        internal RebootBehavior Behavior
        {
            get { return (RebootBehavior)Enum.Parse(typeof(RebootBehavior), cmbBxRebootBehavior.SelectedItem.ToString()); }
            set
            {
                Logger.EnteringMethod(value.ToString());
                cmbBxRebootBehavior.SelectedItem = value;
            }
        }

        internal string SecurityBulletinId
        {
            get { return txtBxSecurityBulletinId.Text; }
            set
            {
                Logger.EnteringMethod(value);
                txtBxSecurityBulletinId.Text = value;
            }
        }

        internal SecurityRating Severity
        {
            get { return (SecurityRating)Enum.Parse(typeof(SecurityRating), cmbBxMsrcSeverity.SelectedItem.ToString()); }
            set
            {
                Logger.EnteringMethod(value.ToString());
                cmbBxMsrcSeverity.SelectedItem = value;
            }
        }

        internal List<string> Cve
        {
            get
            {
                List<string> result = new List<string>();
                foreach (object item in chkCmbBxCveID.Items)
                {
                    result.Add((string)item);
                }
                return result;
            }
            set
            {
                chkCmbBxCveID.ClearItems();
                foreach (string item in value)
                {
                    Logger.EnteringMethod(item);
                    chkCmbBxCveID.AddItem(item);
                }
            }
        }

        internal string KbArticle
        {
            get { return txtBxKBArticleId.Text; }
            set
            {
                Logger.EnteringMethod(value);
                txtBxKBArticleId.Text = value;
            }
        }

        private bool IsRevising { get; set; }

        internal void CopyInformations(SoftwareDistributionPackage softwareDistributionPackage)
        {
            Logger.EnteringMethod(softwareDistributionPackage.Title);
            Company selectedCompany = null;
            Product selectedProduct = null;
            foreach (Company company in _companies.Values)
            {
                if (company.CompanyName.ToLower() == softwareDistributionPackage.VendorName.ToLower())
                {
                    selectedCompany = company;
                    break;
                }
            }
            if (selectedCompany != null)
            {
                foreach (Product product in selectedCompany.Products.Values)
                {
                    if (product.ProductName.ToLower() == softwareDistributionPackage.ProductNames[0].ToLower())
                    {
                        selectedProduct = product;
                        break;
                    }
                }
            }
            InitializeComponent(selectedCompany, selectedProduct, softwareDistributionPackage);

            txtBxSecurityBulletinId.Text = string.Empty;
            chkCmbBxCveID.ClearItems();
            txtBxKBArticleId.Text = string.Empty;
            foreach (object item in chkCmbBxSupersedes.Items)
            {
                if (item.ToString() != softwareDistributionPackage.Title)
                    chkCmbBxSupersedes.SelectItem(item, false);
                else
                    chkCmbBxSupersedes.SelectItem(item, true);
            }
        }

        private void cmbBxVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(cmbBxVendorName);
            if (cmbBxVendorName.SelectedIndex != -1)
            {
                Company selectedCompany = GetCompanyFromName(cmbBxVendorName.SelectedItem.ToString());

                VendorName = selectedCompany.CompanyName;
                cmbBxProductName.Items.Clear();
                foreach (Product product in selectedCompany.Products.Values)
                {
                    cmbBxProductName.Items.Add(product.ProductName);
                }
                cmbBxProductName.Select();

            }
            ValidateData(true);
        }

        private void cmbBxVendorName_TextChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            Logger.Write(cmbBxVendorName);
            if (!string.IsNullOrEmpty(cmbBxVendorName.Text) && cmbBxVendorName.Text.Length > 200)
                cmbBxVendorName.Text = cmbBxVendorName.Text.Substring(0, 200);
            cmbBxProductName.Text = "";
            cmbBxProductName.Items.Clear();
            VendorName = cmbBxVendorName.Text;
            ValidateData(true);
        }

        private void cmbBxProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Write(cmbBxProductName);
            txtBxTitle.Select();
            ProductName = cmbBxProductName.Text;
            ValidateData(true);
        }

        private void cmbBxProductName_TextChanged(object sender, EventArgs e)
        {
            Logger.Write(cmbBxProductName);
            if (!string.IsNullOrEmpty(cmbBxProductName.Text) && cmbBxProductName.Text.Length > 200)
                cmbBxProductName.Text = cmbBxProductName.Text.Substring(0, 200);
            ProductName = cmbBxProductName.Text;
            ValidateData(true);
        }

        private void txtBxDescription_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxDescription.Text) && txtBxDescription.Text.Length > 1500)
                txtBxDescription.Text = txtBxDescription.Text.Substring(0, 1500);
            if (txtBxDescription.Text.Trim().Length != 0)
                Description = txtBxDescription.Text;
            else
                Description = "Default description";
        }

        private void txtBxTitle_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxTitle.Text) && txtBxTitle.Text.Length > 200)
                txtBxTitle.Text = txtBxTitle.Text.Substring(0, 200);
            ValidateData(false);
        }

        private bool IsValideURL(string URLtoValidate)
        {
            Logger.EnteringMethod(URLtoValidate);
            if (!string.IsNullOrEmpty(URLtoValidate) && (URLtoValidate.ToLower().StartsWith("http://") || URLtoValidate.ToLower().StartsWith("https://")))
            {
                try
                {
                    Uri url = new Uri(URLtoValidate, UriKind.Absolute);
                    Logger.Write("return true");
                    return true;
                }
                catch (Exception)
                { }
            }
            Logger.Write("Returning false");
            return false;
        }

        private void btnAddCveID_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FrmAddCveID frmAddCveID = new FrmAddCveID();
            if (frmAddCveID.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string cve in frmAddCveID.CVEArray)
                {
                    if (!string.IsNullOrEmpty(cve))
                    {
                        Logger.Write(cve);
                        chkCmbBxCveID.AddItem(cve);
                    }
                }
            }
        }

        private void btnDeleteCveID_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            foreach (object item in chkCmbBxCveID.SelectedItems)
            {
                Logger.Write(item.ToString());
                chkCmbBxCveID.RemoveItem(item);
            }
        }

        private void txtBxDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txtBxDescription.SelectAll();
        }

        private void txtBxSecurityBulletinId_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxSecurityBulletinId.Text) && txtBxSecurityBulletinId.Text.Length > 15)
                txtBxSecurityBulletinId.Text = txtBxSecurityBulletinId.Text.Substring(0, 15);
        }

        private void txtBxKBArticleId_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtBxKBArticleId.Text) && txtBxKBArticleId.Text.Length > 15)
                txtBxKBArticleId.Text = txtBxKBArticleId.Text.Substring(0, 15);
        }

        private void txtBxMoreInfoURL_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxMoreInfoURL.Text) && txtBxMoreInfoURL.Text.Length > 2083)
                txtBxMoreInfoURL.Text = txtBxMoreInfoURL.Text.Substring(0, 2083);
            ValidateData(false);
        }

        private void txtBxSupportURL_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBxSupportURL.Text) && txtBxSupportURL.Text.Length > 2083)
                txtBxSupportURL.Text = txtBxSupportURL.Text.Substring(0, 2083);
            ValidateData(false);
        }

        private void cmbBxPackageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBxPackageType.SelectedIndex != -1 && cmbBxPackageType.SelectedItem != null)
            {
                if (cmbBxPackageType.SelectedItem.ToString() == PackageType.Application.ToString())
                {
                    cmbBxUpdateClassification.Enabled = false;
                    cmbBxMsrcSeverity.Enabled = false;
                    chkCmbBxCveID.Enabled = false;
                    btnAddCveID.Enabled = false;
                    btnDeleteCveID.Enabled = false;
                    txtBxSecurityBulletinId.Enabled = false;
                    txtBxKBArticleId.Enabled = false;
                }
                else
                {
                    cmbBxUpdateClassification.Enabled = true;
                    cmbBxMsrcSeverity.Enabled = true;
                    chkCmbBxCveID.Enabled = true;
                    btnAddCveID.Enabled = true;
                    btnDeleteCveID.Enabled = true;
                    txtBxSecurityBulletinId.Enabled = true;
                    txtBxKBArticleId.Enabled = true;
                    if (this.Parent != null && !IsRevising)
                    {
                        string text = resMan.GetString("AutoApprovalRulesWillApply");
                        if (!Properties.Settings.Default.PreventAutoApproval)
                            text += "\r\n" + resMan.GetString("ThisServer");
                        if (_wsus.DownStreamServers.Count != 0)
                            text += "\r\n" + resMan.GetString("NonReplicaDownstreamServers");
                        toolTipAutoApprovalRules.Show(text, cmbBxPackageType, cmbBxPackageType.Width, (int)(cmbBxPackageType.Height / 2), 8000);
                    }
                }
            }
        }

    }
}
