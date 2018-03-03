namespace Wsus_Package_Publisher
{
    partial class RuleFileExistsPrependRegSz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleFileExistsPrependRegSz));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxFolderPath = new System.Windows.Forms.TextBox();
            this.chkBxFileVersion = new System.Windows.Forms.CheckBox();
            this.nupVersion1 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion2 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion3 = new System.Windows.Forms.NumericUpDown();
            this.nupVersion4 = new System.Windows.Forms.NumericUpDown();
            this.chkBxCreationDate = new System.Windows.Forms.CheckBox();
            this.dtPCreationDate = new System.Windows.Forms.DateTimePicker();
            this.chkBxModifiedDate = new System.Windows.Forms.CheckBox();
            this.dtPModifiedDate = new System.Windows.Forms.DateTimePicker();
            this.chkBxFileSize = new System.Windows.Forms.CheckBox();
            this.nupFileSize = new System.Windows.Forms.NumericUpDown();
            this.chkBxLanguage = new System.Windows.Forms.CheckBox();
            this.cmbBxLanguage = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.txtBxSubKey = new System.Windows.Forms.TextBox();
            this.chkBxRegType32 = new System.Windows.Forms.CheckBox();
            this.txtBxValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            this.txtBxDescription.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxFolderPath
            // 
            resources.ApplyResources(this.txtBxFolderPath, "txtBxFolderPath");
            this.txtBxFolderPath.Name = "txtBxFolderPath";
            this.txtBxFolderPath.TextChanged += new System.EventHandler(this.txtBxFolderPath_TextChanged_1);
            // 
            // chkBxFileVersion
            // 
            resources.ApplyResources(this.chkBxFileVersion, "chkBxFileVersion");
            this.chkBxFileVersion.Name = "chkBxFileVersion";
            this.chkBxFileVersion.UseVisualStyleBackColor = true;
            this.chkBxFileVersion.CheckedChanged += new System.EventHandler(this.chkBxFileVersion_CheckedChanged);
            // 
            // nupVersion1
            // 
            resources.ApplyResources(this.nupVersion1, "nupVersion1");
            this.nupVersion1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion1.Name = "nupVersion1";
            this.nupVersion1.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion2
            // 
            resources.ApplyResources(this.nupVersion2, "nupVersion2");
            this.nupVersion2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion2.Name = "nupVersion2";
            this.nupVersion2.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion3
            // 
            resources.ApplyResources(this.nupVersion3, "nupVersion3");
            this.nupVersion3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion3.Name = "nupVersion3";
            this.nupVersion3.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // nupVersion4
            // 
            resources.ApplyResources(this.nupVersion4, "nupVersion4");
            this.nupVersion4.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupVersion4.Name = "nupVersion4";
            this.nupVersion4.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // chkBxCreationDate
            // 
            resources.ApplyResources(this.chkBxCreationDate, "chkBxCreationDate");
            this.chkBxCreationDate.Name = "chkBxCreationDate";
            this.chkBxCreationDate.UseVisualStyleBackColor = true;
            this.chkBxCreationDate.CheckedChanged += new System.EventHandler(this.chkBxCreationDate_CheckedChanged);
            // 
            // dtPCreationDate
            // 
            resources.ApplyResources(this.dtPCreationDate, "dtPCreationDate");
            this.dtPCreationDate.Name = "dtPCreationDate";
            // 
            // chkBxModifiedDate
            // 
            resources.ApplyResources(this.chkBxModifiedDate, "chkBxModifiedDate");
            this.chkBxModifiedDate.Name = "chkBxModifiedDate";
            this.chkBxModifiedDate.UseVisualStyleBackColor = true;
            this.chkBxModifiedDate.CheckedChanged += new System.EventHandler(this.chkBxModifiedDate_CheckedChanged);
            // 
            // dtPModifiedDate
            // 
            resources.ApplyResources(this.dtPModifiedDate, "dtPModifiedDate");
            this.dtPModifiedDate.Name = "dtPModifiedDate";
            // 
            // chkBxFileSize
            // 
            resources.ApplyResources(this.chkBxFileSize, "chkBxFileSize");
            this.chkBxFileSize.Name = "chkBxFileSize";
            this.chkBxFileSize.UseVisualStyleBackColor = true;
            this.chkBxFileSize.CheckedChanged += new System.EventHandler(this.chkBxFileSize_CheckedChanged);
            // 
            // nupFileSize
            // 
            resources.ApplyResources(this.nupFileSize, "nupFileSize");
            this.nupFileSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupFileSize.Name = "nupFileSize";
            this.nupFileSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupFileSize.Enter += new System.EventHandler(this.nupVersion1_Enter);
            // 
            // chkBxLanguage
            // 
            resources.ApplyResources(this.chkBxLanguage, "chkBxLanguage");
            this.chkBxLanguage.Name = "chkBxLanguage";
            this.chkBxLanguage.UseVisualStyleBackColor = true;
            this.chkBxLanguage.CheckedChanged += new System.EventHandler(this.chkBxLanguage_CheckedChanged);
            // 
            // cmbBxLanguage
            // 
            resources.ApplyResources(this.cmbBxLanguage, "cmbBxLanguage");
            this.cmbBxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxLanguage.FormattingEnabled = true;
            this.cmbBxLanguage.Name = "cmbBxLanguage";
            this.cmbBxLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbBxLanguage_SelectedIndexChanged);
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
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // txtBxSubKey
            // 
            resources.ApplyResources(this.txtBxSubKey, "txtBxSubKey");
            this.txtBxSubKey.Name = "txtBxSubKey";
            this.txtBxSubKey.TextChanged += new System.EventHandler(this.txtBxSubKey_TextChanged);
            // 
            // chkBxRegType32
            // 
            resources.ApplyResources(this.chkBxRegType32, "chkBxRegType32");
            this.chkBxRegType32.Name = "chkBxRegType32";
            this.chkBxRegType32.UseVisualStyleBackColor = true;
            // 
            // txtBxValue
            // 
            resources.ApplyResources(this.txtBxValue, "txtBxValue");
            this.txtBxValue.Name = "txtBxValue";
            this.txtBxValue.TextChanged += new System.EventHandler(this.txtBxValue_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RuleFileExistsPrependRegSz
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxValue);
            this.Controls.Add(this.chkBxRegType32);
            this.Controls.Add(this.txtBxSubKey);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbBxLanguage);
            this.Controls.Add(this.chkBxLanguage);
            this.Controls.Add(this.nupFileSize);
            this.Controls.Add(this.chkBxFileSize);
            this.Controls.Add(this.dtPModifiedDate);
            this.Controls.Add(this.chkBxModifiedDate);
            this.Controls.Add(this.dtPCreationDate);
            this.Controls.Add(this.chkBxCreationDate);
            this.Controls.Add(this.nupVersion4);
            this.Controls.Add(this.nupVersion3);
            this.Controls.Add(this.nupVersion2);
            this.Controls.Add(this.nupVersion1);
            this.Controls.Add(this.chkBxFileVersion);
            this.Controls.Add(this.txtBxFolderPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxDescription);
            this.MinimumSize = new System.Drawing.Size(500, 570);
            this.Name = "RuleFileExistsPrependRegSz";
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersion4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxFolderPath;
        private System.Windows.Forms.CheckBox chkBxFileVersion;
        private System.Windows.Forms.NumericUpDown nupVersion1;
        private System.Windows.Forms.NumericUpDown nupVersion2;
        private System.Windows.Forms.NumericUpDown nupVersion3;
        private System.Windows.Forms.NumericUpDown nupVersion4;
        private System.Windows.Forms.CheckBox chkBxCreationDate;
        private System.Windows.Forms.DateTimePicker dtPCreationDate;
        private System.Windows.Forms.CheckBox chkBxModifiedDate;
        private System.Windows.Forms.DateTimePicker dtPModifiedDate;
        private System.Windows.Forms.CheckBox chkBxFileSize;
        private System.Windows.Forms.NumericUpDown nupFileSize;
        private System.Windows.Forms.CheckBox chkBxLanguage;
        private System.Windows.Forms.ComboBox cmbBxLanguage;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.TextBox txtBxSubKey;
        private System.Windows.Forms.CheckBox chkBxRegType32;
        private System.Windows.Forms.TextBox txtBxValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}
