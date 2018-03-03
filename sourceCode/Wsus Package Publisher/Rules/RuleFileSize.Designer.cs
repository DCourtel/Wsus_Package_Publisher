namespace Wsus_Package_Publisher
{
    partial class RuleFileSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleFileSize));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.chkBxUseCsidl = new System.Windows.Forms.CheckBox();
            this.cmbBxCsidl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxFilePath = new System.Windows.Forms.TextBox();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.cmbBxComparison = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nupFileSize = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // chkBxUseCsidl
            // 
            resources.ApplyResources(this.chkBxUseCsidl, "chkBxUseCsidl");
            this.chkBxUseCsidl.Name = "chkBxUseCsidl";
            this.chkBxUseCsidl.UseVisualStyleBackColor = true;
            this.chkBxUseCsidl.CheckedChanged += new System.EventHandler(this.chkBxUseCsidl_CheckedChanged);
            // 
            // cmbBxCsidl
            // 
            resources.ApplyResources(this.cmbBxCsidl, "cmbBxCsidl");
            this.cmbBxCsidl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxCsidl.FormattingEnabled = true;
            this.cmbBxCsidl.Name = "cmbBxCsidl";
            this.cmbBxCsidl.SelectedIndexChanged += new System.EventHandler(this.cmbBxCsidl_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxFilePath
            // 
            resources.ApplyResources(this.txtBxFilePath, "txtBxFilePath");
            this.txtBxFilePath.Name = "txtBxFilePath";
            this.txtBxFilePath.TextChanged += new System.EventHandler(this.txtBxFilePath_TextChanged);
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // cmbBxComparison
            // 
            resources.ApplyResources(this.cmbBxComparison, "cmbBxComparison");
            this.cmbBxComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxComparison.FormattingEnabled = true;
            this.cmbBxComparison.Name = "cmbBxComparison";
            this.cmbBxComparison.SelectedIndexChanged += new System.EventHandler(this.cmbBxComparison_SelectedIndexChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nupFileSize
            // 
            resources.ApplyResources(this.nupFileSize, "nupFileSize");
            this.nupFileSize.Name = "nupFileSize";
            this.nupFileSize.Enter += new System.EventHandler(this.nupFileSize_Enter);
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
            // RuleFileSize
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.nupFileSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbBxComparison);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.txtBxFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxCsidl);
            this.Controls.Add(this.chkBxUseCsidl);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleFileSize";
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.CheckBox chkBxUseCsidl;
        private System.Windows.Forms.ComboBox cmbBxCsidl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxFilePath;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.ComboBox cmbBxComparison;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupFileSize;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
