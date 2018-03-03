namespace Wsus_Package_Publisher
{
    partial class FrmImportCatalog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportCatalog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvCatalog = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label17 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxPackageId = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBxSupportUrl = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBxTitle = new System.Windows.Forms.TextBox();
            this.txtBxSecurityBulletinId = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtBxVendorName = new System.Windows.Forms.TextBox();
            this.txtBxProductName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtCreationDate = new System.Windows.Forms.DateTimePicker();
            this.cmbBxAdditionnalInformation = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbBxCVE = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbBxLanguages = new System.Windows.Forms.ComboBox();
            this.txtBxSecurityRating = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBxSize = new System.Windows.Forms.TextBox();
            this.cmbBxPrerequisites = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxFilepath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOpenCatalog = new System.Windows.Forms.Button();
            this.prgBarProgression = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.chkBxMakeLanguageIndependent = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbBxFilterCriteria = new System.Windows.Forms.ComboBox();
            this.txtBxFilterText = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.chkBxVisibleInWsusConsole = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvCatalog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.TabStop = false;
            // 
            // trvCatalog
            // 
            this.trvCatalog.CheckBoxes = true;
            resources.ApplyResources(this.trvCatalog, "trvCatalog");
            this.trvCatalog.HideSelection = false;
            this.trvCatalog.Name = "trvCatalog";
            this.trvCatalog.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.trvCatalog_AfterCheck);
            this.trvCatalog.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCatalog_AfterSelect);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 15);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtBxPackageId, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtBxSupportUrl, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtBxTitle, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtBxSecurityBulletinId, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.label12, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtBxVendorName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtBxProductName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtBxDescription, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.dtCreationDate, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxAdditionnalInformation, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxCVE, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label14, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxLanguages, 1, 12);
            this.tableLayoutPanel1.Controls.Add(this.txtBxSecurityRating, 1, 11);
            this.tableLayoutPanel1.Controls.Add(this.label15, 1, 13);
            this.tableLayoutPanel1.Controls.Add(this.txtBxSize, 1, 14);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxPrerequisites, 1, 15);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxPackageId
            // 
            resources.ApplyResources(this.txtBxPackageId, "txtBxPackageId");
            this.txtBxPackageId.Name = "txtBxPackageId";
            this.txtBxPackageId.ReadOnly = true;
            this.txtBxPackageId.TabStop = false;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtBxSupportUrl
            // 
            resources.ApplyResources(this.txtBxSupportUrl, "txtBxSupportUrl");
            this.txtBxSupportUrl.Name = "txtBxSupportUrl";
            this.txtBxSupportUrl.ReadOnly = true;
            this.txtBxSupportUrl.TabStop = false;
            this.txtBxSupportUrl.DoubleClick += new System.EventHandler(this.txtBxSupportUrl_DoubleClick);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // txtBxTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBxTitle, 2);
            resources.ApplyResources(this.txtBxTitle, "txtBxTitle");
            this.txtBxTitle.Name = "txtBxTitle";
            this.txtBxTitle.ReadOnly = true;
            this.txtBxTitle.TabStop = false;
            // 
            // txtBxSecurityBulletinId
            // 
            resources.ApplyResources(this.txtBxSecurityBulletinId, "txtBxSecurityBulletinId");
            this.txtBxSecurityBulletinId.Name = "txtBxSecurityBulletinId";
            this.txtBxSecurityBulletinId.ReadOnly = true;
            this.txtBxSecurityBulletinId.TabStop = false;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtBxVendorName
            // 
            resources.ApplyResources(this.txtBxVendorName, "txtBxVendorName");
            this.txtBxVendorName.Name = "txtBxVendorName";
            this.txtBxVendorName.ReadOnly = true;
            this.txtBxVendorName.TabStop = false;
            // 
            // txtBxProductName
            // 
            resources.ApplyResources(this.txtBxProductName, "txtBxProductName");
            this.txtBxProductName.Name = "txtBxProductName";
            this.txtBxProductName.ReadOnly = true;
            this.txtBxProductName.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtBxDescription
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBxDescription, 2);
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dtCreationDate
            // 
            resources.ApplyResources(this.dtCreationDate, "dtCreationDate");
            this.dtCreationDate.Name = "dtCreationDate";
            this.dtCreationDate.TabStop = false;
            // 
            // cmbBxAdditionnalInformation
            // 
            resources.ApplyResources(this.cmbBxAdditionnalInformation, "cmbBxAdditionnalInformation");
            this.cmbBxAdditionnalInformation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxAdditionnalInformation.FormattingEnabled = true;
            this.cmbBxAdditionnalInformation.Name = "cmbBxAdditionnalInformation";
            this.cmbBxAdditionnalInformation.TabStop = false;
            this.cmbBxAdditionnalInformation.Click += new System.EventHandler(this.cmbBxAdditionnalInformation_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbBxCVE
            // 
            resources.ApplyResources(this.cmbBxCVE, "cmbBxCVE");
            this.cmbBxCVE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxCVE.FormattingEnabled = true;
            this.cmbBxCVE.Name = "cmbBxCVE";
            this.cmbBxCVE.TabStop = false;
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // cmbBxLanguages
            // 
            resources.ApplyResources(this.cmbBxLanguages, "cmbBxLanguages");
            this.cmbBxLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxLanguages.FormattingEnabled = true;
            this.cmbBxLanguages.Name = "cmbBxLanguages";
            this.cmbBxLanguages.TabStop = false;
            // 
            // txtBxSecurityRating
            // 
            resources.ApplyResources(this.txtBxSecurityRating, "txtBxSecurityRating");
            this.txtBxSecurityRating.Name = "txtBxSecurityRating";
            this.txtBxSecurityRating.ReadOnly = true;
            this.txtBxSecurityRating.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // txtBxSize
            // 
            resources.ApplyResources(this.txtBxSize, "txtBxSize");
            this.txtBxSize.Name = "txtBxSize";
            this.txtBxSize.ReadOnly = true;
            this.txtBxSize.TabStop = false;
            // 
            // cmbBxPrerequisites
            // 
            resources.ApplyResources(this.cmbBxPrerequisites, "cmbBxPrerequisites");
            this.cmbBxPrerequisites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxPrerequisites.FormattingEnabled = true;
            this.cmbBxPrerequisites.Name = "cmbBxPrerequisites";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxFilepath
            // 
            resources.ApplyResources(this.txtBxFilepath, "txtBxFilepath");
            this.txtBxFilepath.Name = "txtBxFilepath";
            this.txtBxFilepath.TextChanged += new System.EventHandler(this.txtBxFilepath_TextChanged);
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOpenCatalog
            // 
            resources.ApplyResources(this.btnOpenCatalog, "btnOpenCatalog");
            this.btnOpenCatalog.Name = "btnOpenCatalog";
            this.btnOpenCatalog.UseVisualStyleBackColor = true;
            this.btnOpenCatalog.Click += new System.EventHandler(this.btnOpenCatalog_Click);
            // 
            // prgBarProgression
            // 
            resources.ApplyResources(this.prgBarProgression, "prgBarProgression");
            this.prgBarProgression.Name = "prgBarProgression";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImport
            // 
            resources.ApplyResources(this.btnImport, "btnImport");
            this.btnImport.Name = "btnImport";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // chkBxMakeLanguageIndependent
            // 
            resources.ApplyResources(this.chkBxMakeLanguageIndependent, "chkBxMakeLanguageIndependent");
            this.chkBxMakeLanguageIndependent.Checked = true;
            this.chkBxMakeLanguageIndependent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxMakeLanguageIndependent.Name = "chkBxMakeLanguageIndependent";
            this.chkBxMakeLanguageIndependent.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // cmbBxFilterCriteria
            // 
            this.cmbBxFilterCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxFilterCriteria.FormattingEnabled = true;
            this.cmbBxFilterCriteria.Items.AddRange(new object[] {
            resources.GetString("cmbBxFilterCriteria.Items"),
            resources.GetString("cmbBxFilterCriteria.Items1")});
            resources.ApplyResources(this.cmbBxFilterCriteria, "cmbBxFilterCriteria");
            this.cmbBxFilterCriteria.Name = "cmbBxFilterCriteria";
            this.cmbBxFilterCriteria.SelectedIndexChanged += new System.EventHandler(this.cmbBxFilterCriteria_SelectedIndexChanged);
            // 
            // txtBxFilterText
            // 
            resources.ApplyResources(this.txtBxFilterText, "txtBxFilterText");
            this.txtBxFilterText.Name = "txtBxFilterText";
            this.txtBxFilterText.TextChanged += new System.EventHandler(this.txtBxFilterText_TextChanged);
            this.txtBxFilterText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBxFilterText_KeyUp);
            // 
            // btnClearFilter
            // 
            resources.ApplyResources(this.btnClearFilter, "btnClearFilter");
            this.btnClearFilter.Image = global::Wsus_Package_Publisher.Properties.Resources.Delete;
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // chkBxVisibleInWsusConsole
            // 
            resources.ApplyResources(this.chkBxVisibleInWsusConsole, "chkBxVisibleInWsusConsole");
            this.chkBxVisibleInWsusConsole.Name = "chkBxVisibleInWsusConsole";
            this.chkBxVisibleInWsusConsole.UseVisualStyleBackColor = true;
            // 
            // FrmImportCatalog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxVisibleInWsusConsole);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.txtBxFilterText);
            this.Controls.Add(this.cmbBxFilterCriteria);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.chkBxMakeLanguageIndependent);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.prgBarProgression);
            this.Controls.Add(this.btnOpenCatalog);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtBxFilepath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmImportCatalog";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView trvCatalog;
        private System.Windows.Forms.TextBox txtBxTitle;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtBxSupportUrl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBxSecurityBulletinId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBxPackageId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtCreationDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBxCVE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBxAdditionnalInformation;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBxProductName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBxVendorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxFilepath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnOpenCatalog;
        private System.Windows.Forms.ProgressBar prgBarProgression;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbBxLanguages;
        private System.Windows.Forms.TextBox txtBxSecurityRating;
        private System.Windows.Forms.CheckBox chkBxMakeLanguageIndependent;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBxSize;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbBxFilterCriteria;
        private System.Windows.Forms.TextBox txtBxFilterText;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.CheckBox chkBxVisibleInWsusConsole;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbBxPrerequisites;
    }
}