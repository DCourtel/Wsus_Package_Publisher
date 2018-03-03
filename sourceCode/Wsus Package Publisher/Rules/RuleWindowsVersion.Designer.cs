namespace Wsus_Package_Publisher
{
    partial class RuleWindowsVersion
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleWindowsVersion));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.cmbBxComparison = new System.Windows.Forms.ComboBox();
            this.nupMajorVersion = new System.Windows.Forms.NumericUpDown();
            this.nupMinorVersion = new System.Windows.Forms.NumericUpDown();
            this.nupBuildNumber = new System.Windows.Forms.NumericUpDown();
            this.nupServicePackMinor = new System.Windows.Forms.NumericUpDown();
            this.nupServicePackMajor = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbBxProductType = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBxOperatingSystem = new System.Windows.Forms.ComboBox();
            this.chkBxMajorVersion = new System.Windows.Forms.CheckBox();
            this.chkBxMinorVersion = new System.Windows.Forms.CheckBox();
            this.chkBxServicePackMajor = new System.Windows.Forms.CheckBox();
            this.chkBxBuildNumber = new System.Windows.Forms.CheckBox();
            this.chkBxProductType = new System.Windows.Forms.CheckBox();
            this.chkBxServicePackMinor = new System.Windows.Forms.CheckBox();
            this.chkBxComparison = new System.Windows.Forms.CheckBox();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nupMajorVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinorVersion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBuildNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupServicePackMinor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupServicePackMajor)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // cmbBxComparison
            // 
            resources.ApplyResources(this.cmbBxComparison, "cmbBxComparison");
            this.cmbBxComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxComparison.FormattingEnabled = true;
            this.cmbBxComparison.Name = "cmbBxComparison";
            this.cmbBxComparison.SelectedIndexChanged += new System.EventHandler(this.cmbBxComparison_SelectedIndexChanged);
            // 
            // nupMajorVersion
            // 
            resources.ApplyResources(this.nupMajorVersion, "nupMajorVersion");
            this.nupMajorVersion.Name = "nupMajorVersion";
            this.nupMajorVersion.Enter += new System.EventHandler(this.nupMajorVersion_Enter);
            // 
            // nupMinorVersion
            // 
            resources.ApplyResources(this.nupMinorVersion, "nupMinorVersion");
            this.nupMinorVersion.Name = "nupMinorVersion";
            this.nupMinorVersion.Enter += new System.EventHandler(this.nupMajorVersion_Enter);
            // 
            // nupBuildNumber
            // 
            resources.ApplyResources(this.nupBuildNumber, "nupBuildNumber");
            this.nupBuildNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupBuildNumber.Name = "nupBuildNumber";
            this.nupBuildNumber.Enter += new System.EventHandler(this.nupMajorVersion_Enter);
            // 
            // nupServicePackMinor
            // 
            resources.ApplyResources(this.nupServicePackMinor, "nupServicePackMinor");
            this.nupServicePackMinor.Name = "nupServicePackMinor";
            this.nupServicePackMinor.Enter += new System.EventHandler(this.nupMajorVersion_Enter);
            // 
            // nupServicePackMajor
            // 
            resources.ApplyResources(this.nupServicePackMajor, "nupServicePackMajor");
            this.nupServicePackMajor.Name = "nupServicePackMajor";
            this.nupServicePackMajor.Enter += new System.EventHandler(this.nupMajorVersion_Enter);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbBxProductType
            // 
            resources.ApplyResources(this.cmbBxProductType, "cmbBxProductType");
            this.cmbBxProductType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxProductType.FormattingEnabled = true;
            this.cmbBxProductType.Items.AddRange(new object[] {
            resources.GetString("cmbBxProductType.Items"),
            resources.GetString("cmbBxProductType.Items1"),
            resources.GetString("cmbBxProductType.Items2")});
            this.cmbBxProductType.Name = "cmbBxProductType";
            this.cmbBxProductType.SelectedIndexChanged += new System.EventHandler(this.cmbBxProductType_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // cmbBxOperatingSystem
            // 
            resources.ApplyResources(this.cmbBxOperatingSystem, "cmbBxOperatingSystem");
            this.cmbBxOperatingSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxOperatingSystem.FormattingEnabled = true;
            this.cmbBxOperatingSystem.Items.AddRange(new object[] {
            resources.GetString("cmbBxOperatingSystem.Items"),
            resources.GetString("cmbBxOperatingSystem.Items1"),
            resources.GetString("cmbBxOperatingSystem.Items2"),
            resources.GetString("cmbBxOperatingSystem.Items3"),
            resources.GetString("cmbBxOperatingSystem.Items4"),
            resources.GetString("cmbBxOperatingSystem.Items5"),
            resources.GetString("cmbBxOperatingSystem.Items6"),
            resources.GetString("cmbBxOperatingSystem.Items7"),
            resources.GetString("cmbBxOperatingSystem.Items8"),
            resources.GetString("cmbBxOperatingSystem.Items9"),
            resources.GetString("cmbBxOperatingSystem.Items10")});
            this.cmbBxOperatingSystem.Name = "cmbBxOperatingSystem";
            this.cmbBxOperatingSystem.SelectedIndexChanged += new System.EventHandler(this.cmbBxOperatingSystem_SelectedIndexChanged);
            // 
            // chkBxMajorVersion
            // 
            resources.ApplyResources(this.chkBxMajorVersion, "chkBxMajorVersion");
            this.chkBxMajorVersion.Name = "chkBxMajorVersion";
            this.chkBxMajorVersion.UseVisualStyleBackColor = true;
            this.chkBxMajorVersion.CheckedChanged += new System.EventHandler(this.chkBxMajorVersion_CheckedChanged);
            // 
            // chkBxMinorVersion
            // 
            resources.ApplyResources(this.chkBxMinorVersion, "chkBxMinorVersion");
            this.chkBxMinorVersion.Name = "chkBxMinorVersion";
            this.chkBxMinorVersion.UseVisualStyleBackColor = true;
            this.chkBxMinorVersion.CheckedChanged += new System.EventHandler(this.chkBxMinorVersion_CheckedChanged);
            // 
            // chkBxServicePackMajor
            // 
            resources.ApplyResources(this.chkBxServicePackMajor, "chkBxServicePackMajor");
            this.chkBxServicePackMajor.Name = "chkBxServicePackMajor";
            this.chkBxServicePackMajor.UseVisualStyleBackColor = true;
            this.chkBxServicePackMajor.CheckedChanged += new System.EventHandler(this.chkBxServicePackMajor_CheckedChanged);
            // 
            // chkBxBuildNumber
            // 
            resources.ApplyResources(this.chkBxBuildNumber, "chkBxBuildNumber");
            this.chkBxBuildNumber.Name = "chkBxBuildNumber";
            this.chkBxBuildNumber.UseVisualStyleBackColor = true;
            this.chkBxBuildNumber.CheckedChanged += new System.EventHandler(this.chkBxBuildNumber_CheckedChanged);
            // 
            // chkBxProductType
            // 
            resources.ApplyResources(this.chkBxProductType, "chkBxProductType");
            this.chkBxProductType.Name = "chkBxProductType";
            this.chkBxProductType.UseVisualStyleBackColor = true;
            this.chkBxProductType.CheckedChanged += new System.EventHandler(this.chkBxProductType_CheckedChanged);
            // 
            // chkBxServicePackMinor
            // 
            resources.ApplyResources(this.chkBxServicePackMinor, "chkBxServicePackMinor");
            this.chkBxServicePackMinor.Name = "chkBxServicePackMinor";
            this.chkBxServicePackMinor.UseVisualStyleBackColor = true;
            this.chkBxServicePackMinor.CheckedChanged += new System.EventHandler(this.chkBxSericePackMinor_CheckedChanged);
            // 
            // chkBxComparison
            // 
            resources.ApplyResources(this.chkBxComparison, "chkBxComparison");
            this.chkBxComparison.Name = "chkBxComparison";
            this.chkBxComparison.UseVisualStyleBackColor = true;
            this.chkBxComparison.CheckedChanged += new System.EventHandler(this.chkBxComparison_CheckedChanged);
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // RuleWindowsVersion
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.chkBxComparison);
            this.Controls.Add(this.chkBxServicePackMinor);
            this.Controls.Add(this.chkBxServicePackMajor);
            this.Controls.Add(this.chkBxProductType);
            this.Controls.Add(this.chkBxMinorVersion);
            this.Controls.Add(this.chkBxBuildNumber);
            this.Controls.Add(this.chkBxMajorVersion);
            this.Controls.Add(this.cmbBxOperatingSystem);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbBxProductType);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.nupServicePackMajor);
            this.Controls.Add(this.nupServicePackMinor);
            this.Controls.Add(this.nupBuildNumber);
            this.Controls.Add(this.nupMinorVersion);
            this.Controls.Add(this.nupMajorVersion);
            this.Controls.Add(this.cmbBxComparison);
            this.Controls.Add(this.txtBxDescription);
            this.MinimumSize = new System.Drawing.Size(488, 385);
            this.Name = "RuleWindowsVersion";
            ((System.ComponentModel.ISupportInitialize)(this.nupMajorVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinorVersion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupBuildNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupServicePackMinor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupServicePackMajor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.ComboBox cmbBxComparison;
        private System.Windows.Forms.NumericUpDown nupMajorVersion;
        private System.Windows.Forms.NumericUpDown nupMinorVersion;
        private System.Windows.Forms.NumericUpDown nupBuildNumber;
        private System.Windows.Forms.NumericUpDown nupServicePackMinor;
        private System.Windows.Forms.NumericUpDown nupServicePackMajor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbBxProductType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBxOperatingSystem;
        private System.Windows.Forms.CheckBox chkBxMajorVersion;
        private System.Windows.Forms.CheckBox chkBxMinorVersion;
        private System.Windows.Forms.CheckBox chkBxServicePackMajor;
        private System.Windows.Forms.CheckBox chkBxBuildNumber;
        private System.Windows.Forms.CheckBox chkBxProductType;
        private System.Windows.Forms.CheckBox chkBxServicePackMinor;
        private System.Windows.Forms.CheckBox chkBxComparison;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
    }
}
