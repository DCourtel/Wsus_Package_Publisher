namespace Wsus_Package_Publisher
{
    partial class FrmCatalogSubscription
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatalogSubscription));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvSubscriptions = new System.Windows.Forms.DataGridView();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckEvery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCheckDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastCheckResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpdateAvailable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastDownloadDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subscription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportUpdates = new System.Windows.Forms.Button();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkBxShareCatalogAddress = new System.Windows.Forms.CheckBox();
            this.btnLoadCatalog = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtBxFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cmbBxCheckEvery = new System.Windows.Forms.ComboBox();
            this.nupCheckEvery = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscriptions)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCheckEvery)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.dgvSubscriptions);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.btnImportUpdates);
            this.splitContainer1.Panel2.Controls.Add(this.btnCheckUpdate);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.btnLoadCatalog);
            this.splitContainer1.Panel2.Controls.Add(this.btnModify);
            this.splitContainer1.Panel2.Controls.Add(this.btnTest);
            this.splitContainer1.Panel2.Controls.Add(this.txtBxFileName);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.btnRemove);
            this.splitContainer1.Panel2.Controls.Add(this.cmbBxCheckEvery);
            this.splitContainer1.Panel2.Controls.Add(this.nupCheckEvery);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.txtBxAddress);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.TabStop = false;
            // 
            // dgvSubscriptions
            // 
            resources.ApplyResources(this.dgvSubscriptions, "dgvSubscriptions");
            this.dgvSubscriptions.AllowUserToAddRows = false;
            this.dgvSubscriptions.AllowUserToDeleteRows = false;
            this.dgvSubscriptions.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSubscriptions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSubscriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubscriptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Active,
            this.Address,
            this.FileName,
            this.CheckEvery,
            this.Unit,
            this.LastCheckDate,
            this.LastCheckResult,
            this.UpdateAvailable,
            this.LastDownloadDate,
            this.Subscription});
            this.dgvSubscriptions.EnableHeadersVisualStyles = false;
            this.dgvSubscriptions.MultiSelect = false;
            this.dgvSubscriptions.Name = "dgvSubscriptions";
            this.dgvSubscriptions.RowHeadersVisible = false;
            this.dgvSubscriptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubscriptions.TabStop = false;
            this.dgvSubscriptions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSubscriptions_CellMouseDoubleClick);
            this.dgvSubscriptions.SelectionChanged += new System.EventHandler(this.dgvSubscriptions_SelectionChanged);
            // 
            // Active
            // 
            this.Active.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Active.DefaultCellStyle = dataGridViewCellStyle2;
            this.Active.FillWeight = 5F;
            resources.ApplyResources(this.Active, "Active");
            this.Active.Name = "Active";
            // 
            // Address
            // 
            this.Address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Address.DefaultCellStyle = dataGridViewCellStyle3;
            this.Address.FillWeight = 30F;
            resources.ApplyResources(this.Address, "Address");
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.FileName.DefaultCellStyle = dataGridViewCellStyle4;
            this.FileName.FillWeight = 20F;
            resources.ApplyResources(this.FileName, "FileName");
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // CheckEvery
            // 
            this.CheckEvery.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.CheckEvery.DefaultCellStyle = dataGridViewCellStyle5;
            this.CheckEvery.FillWeight = 10F;
            resources.ApplyResources(this.CheckEvery, "CheckEvery");
            this.CheckEvery.Name = "CheckEvery";
            this.CheckEvery.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle6;
            this.Unit.FillWeight = 10F;
            resources.ApplyResources(this.Unit, "Unit");
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // LastCheckDate
            // 
            this.LastCheckDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.LastCheckDate.DefaultCellStyle = dataGridViewCellStyle7;
            this.LastCheckDate.FillWeight = 7F;
            resources.ApplyResources(this.LastCheckDate, "LastCheckDate");
            this.LastCheckDate.Name = "LastCheckDate";
            this.LastCheckDate.ReadOnly = true;
            // 
            // LastCheckResult
            // 
            this.LastCheckResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.LastCheckResult.DefaultCellStyle = dataGridViewCellStyle8;
            this.LastCheckResult.FillWeight = 7F;
            resources.ApplyResources(this.LastCheckResult, "LastCheckResult");
            this.LastCheckResult.Name = "LastCheckResult";
            this.LastCheckResult.ReadOnly = true;
            // 
            // UpdateAvailable
            // 
            this.UpdateAvailable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.UpdateAvailable.DefaultCellStyle = dataGridViewCellStyle9;
            this.UpdateAvailable.FillWeight = 7F;
            resources.ApplyResources(this.UpdateAvailable, "UpdateAvailable");
            this.UpdateAvailable.Name = "UpdateAvailable";
            this.UpdateAvailable.ReadOnly = true;
            // 
            // LastDownloadDate
            // 
            this.LastDownloadDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.LastDownloadDate.DefaultCellStyle = dataGridViewCellStyle10;
            this.LastDownloadDate.FillWeight = 10F;
            resources.ApplyResources(this.LastDownloadDate, "LastDownloadDate");
            this.LastDownloadDate.Name = "LastDownloadDate";
            this.LastDownloadDate.ReadOnly = true;
            // 
            // Subscription
            // 
            resources.ApplyResources(this.Subscription, "Subscription");
            this.Subscription.Name = "Subscription";
            // 
            // btnImportUpdates
            // 
            resources.ApplyResources(this.btnImportUpdates, "btnImportUpdates");
            this.btnImportUpdates.Name = "btnImportUpdates";
            this.btnImportUpdates.UseVisualStyleBackColor = true;
            this.btnImportUpdates.Click += new System.EventHandler(this.btnImportUpdates_Click);
            // 
            // btnCheckUpdate
            // 
            resources.ApplyResources(this.btnCheckUpdate, "btnCheckUpdate");
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.chkBxShareCatalogAddress);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkBxShareCatalogAddress
            // 
            resources.ApplyResources(this.chkBxShareCatalogAddress, "chkBxShareCatalogAddress");
            this.chkBxShareCatalogAddress.Checked = true;
            this.chkBxShareCatalogAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxShareCatalogAddress.Name = "chkBxShareCatalogAddress";
            this.chkBxShareCatalogAddress.UseVisualStyleBackColor = true;
            // 
            // btnLoadCatalog
            // 
            resources.ApplyResources(this.btnLoadCatalog, "btnLoadCatalog");
            this.btnLoadCatalog.Name = "btnLoadCatalog";
            this.btnLoadCatalog.UseVisualStyleBackColor = true;
            this.btnLoadCatalog.Click += new System.EventHandler(this.btnLoadCatalog_Click);
            // 
            // btnModify
            // 
            resources.ApplyResources(this.btnModify, "btnModify");
            this.btnModify.Name = "btnModify";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtBxFileName
            // 
            resources.ApplyResources(this.txtBxFileName, "txtBxFileName");
            this.txtBxFileName.Name = "txtBxFileName";
            this.txtBxFileName.TextChanged += new System.EventHandler(this.txtBxFileName_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // btnRemove
            // 
            resources.ApplyResources(this.btnRemove, "btnRemove");
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // cmbBxCheckEvery
            // 
            resources.ApplyResources(this.cmbBxCheckEvery, "cmbBxCheckEvery");
            this.cmbBxCheckEvery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxCheckEvery.FormattingEnabled = true;
            this.cmbBxCheckEvery.Name = "cmbBxCheckEvery";
            this.cmbBxCheckEvery.SelectedIndexChanged += new System.EventHandler(this.cmbBxCheckEvery_SelectedIndexChanged);
            // 
            // nupCheckEvery
            // 
            resources.ApplyResources(this.nupCheckEvery, "nupCheckEvery");
            this.nupCheckEvery.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCheckEvery.Name = "nupCheckEvery";
            this.nupCheckEvery.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxAddress
            // 
            resources.ApplyResources(this.txtBxAddress, "txtBxAddress");
            this.txtBxAddress.Name = "txtBxAddress";
            this.txtBxAddress.TextChanged += new System.EventHandler(this.txtBxAddress_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmCatalogSubscription
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmCatalogSubscription";
            this.Shown += new System.EventHandler(this.FrmCatalogSubscription_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubscriptions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupCheckEvery)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvSubscriptions;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBxCheckEvery;
        private System.Windows.Forms.NumericUpDown nupCheckEvery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtBxFileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.CheckBox chkBxShareCatalogAddress;
        private System.Windows.Forms.Button btnLoadCatalog;
        private System.Windows.Forms.Button btnCheckUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImportUpdates;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckEvery;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCheckDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastCheckResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpdateAvailable;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastDownloadDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subscription;
    }
}