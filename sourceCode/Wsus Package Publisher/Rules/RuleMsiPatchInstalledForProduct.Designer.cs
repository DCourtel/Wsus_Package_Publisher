namespace Wsus_Package_Publisher
{
    partial class RuleMsiPatchInstalledForProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleMsiPatchInstalledForProduct));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxPatchCode = new System.Windows.Forms.TextBox();
            this.txtBxProductCode = new System.Windows.Forms.TextBox();
            this.chkbxReverseRule = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkBxUseVersionMax = new System.Windows.Forms.CheckBox();
            this.chkBxUseVersionMin = new System.Windows.Forms.CheckBox();
            this.chkBxUseLanguage = new System.Windows.Forms.CheckBox();
            this.cmbBxLanguage = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.nupVersionMin1 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMin2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMin3 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMin4 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMax1 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMax2 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMax3 = new System.Windows.Forms.NumericUpDown();
            this.nupVersionMax4 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax4)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxPatchCode
            // 
            resources.ApplyResources(this.txtBxPatchCode, "txtBxPatchCode");
            this.txtBxPatchCode.Name = "txtBxPatchCode";
            this.txtBxPatchCode.TextChanged += new System.EventHandler(this.txtBxPatchCode_TextChanged);
            // 
            // txtBxProductCode
            // 
            resources.ApplyResources(this.txtBxProductCode, "txtBxProductCode");
            this.txtBxProductCode.Name = "txtBxProductCode";
            this.txtBxProductCode.TextChanged += new System.EventHandler(this.txtBxProductCode_TextChanged);
            // 
            // chkbxReverseRule
            // 
            resources.ApplyResources(this.chkbxReverseRule, "chkbxReverseRule");
            this.chkbxReverseRule.Name = "chkbxReverseRule";
            this.chkbxReverseRule.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // chkBxUseVersionMax
            // 
            resources.ApplyResources(this.chkBxUseVersionMax, "chkBxUseVersionMax");
            this.chkBxUseVersionMax.Name = "chkBxUseVersionMax";
            this.chkBxUseVersionMax.UseVisualStyleBackColor = true;
            this.chkBxUseVersionMax.CheckedChanged += new System.EventHandler(this.chkBxUseVersionMax_CheckedChanged);
            // 
            // chkBxUseVersionMin
            // 
            resources.ApplyResources(this.chkBxUseVersionMin, "chkBxUseVersionMin");
            this.chkBxUseVersionMin.Name = "chkBxUseVersionMin";
            this.chkBxUseVersionMin.UseVisualStyleBackColor = true;
            this.chkBxUseVersionMin.CheckedChanged += new System.EventHandler(this.chkBxUseVersionMin_CheckedChanged);
            // 
            // chkBxUseLanguage
            // 
            resources.ApplyResources(this.chkBxUseLanguage, "chkBxUseLanguage");
            this.chkBxUseLanguage.Name = "chkBxUseLanguage";
            this.chkBxUseLanguage.UseVisualStyleBackColor = true;
            this.chkBxUseLanguage.CheckedChanged += new System.EventHandler(this.chkBxUseLanguage_CheckedChanged);
            // 
            // cmbBxLanguage
            // 
            resources.ApplyResources(this.cmbBxLanguage, "cmbBxLanguage");
            this.cmbBxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxLanguage.FormattingEnabled = true;
            this.cmbBxLanguage.Name = "cmbBxLanguage";
            this.cmbBxLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbBxLanguage_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // nupVersionMin1
            // 
            resources.ApplyResources(this.nupVersionMin1, "nupVersionMin1");
            this.nupVersionMin1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMin1.Name = "nupVersionMin1";
            this.nupVersionMin1.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMin2
            // 
            resources.ApplyResources(this.nupVersionMin2, "nupVersionMin2");
            this.nupVersionMin2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMin2.Name = "nupVersionMin2";
            this.nupVersionMin2.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // numericUpDown3
            // 
            resources.ApplyResources(this.numericUpDown3, "numericUpDown3");
            this.numericUpDown3.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            // 
            // numericUpDown4
            // 
            resources.ApplyResources(this.numericUpDown4, "numericUpDown4");
            this.numericUpDown4.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            // 
            // nupVersionMin3
            // 
            resources.ApplyResources(this.nupVersionMin3, "nupVersionMin3");
            this.nupVersionMin3.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMin3.Name = "nupVersionMin3";
            this.nupVersionMin3.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMin4
            // 
            resources.ApplyResources(this.nupVersionMin4, "nupVersionMin4");
            this.nupVersionMin4.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMin4.Name = "nupVersionMin4";
            this.nupVersionMin4.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMax1
            // 
            resources.ApplyResources(this.nupVersionMax1, "nupVersionMax1");
            this.nupVersionMax1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMax1.Name = "nupVersionMax1";
            this.nupVersionMax1.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMax2
            // 
            resources.ApplyResources(this.nupVersionMax2, "nupVersionMax2");
            this.nupVersionMax2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMax2.Name = "nupVersionMax2";
            this.nupVersionMax2.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMax3
            // 
            resources.ApplyResources(this.nupVersionMax3, "nupVersionMax3");
            this.nupVersionMax3.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMax3.Name = "nupVersionMax3";
            this.nupVersionMax3.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // nupVersionMax4
            // 
            resources.ApplyResources(this.nupVersionMax4, "nupVersionMax4");
            this.nupVersionMax4.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nupVersionMax4.Name = "nupVersionMax4";
            this.nupVersionMax4.Enter += new System.EventHandler(this.nupVersionMax1_Enter);
            // 
            // RuleMsiPatchInstalledForProduct
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nupVersionMax4);
            this.Controls.Add(this.nupVersionMin4);
            this.Controls.Add(this.numericUpDown4);
            this.Controls.Add(this.nupVersionMax3);
            this.Controls.Add(this.nupVersionMin3);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.nupVersionMax2);
            this.Controls.Add(this.nupVersionMax1);
            this.Controls.Add(this.nupVersionMin2);
            this.Controls.Add(this.nupVersionMin1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbBxLanguage);
            this.Controls.Add(this.chkBxUseLanguage);
            this.Controls.Add(this.chkBxUseVersionMin);
            this.Controls.Add(this.chkBxUseVersionMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkbxReverseRule);
            this.Controls.Add(this.txtBxProductCode);
            this.Controls.Add(this.txtBxPatchCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleMsiPatchInstalledForProduct";
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMin4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupVersionMax4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxPatchCode;
        private System.Windows.Forms.TextBox txtBxProductCode;
        private System.Windows.Forms.CheckBox chkbxReverseRule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkBxUseVersionMax;
        private System.Windows.Forms.CheckBox chkBxUseVersionMin;
        private System.Windows.Forms.CheckBox chkBxUseLanguage;
        private System.Windows.Forms.ComboBox cmbBxLanguage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.NumericUpDown nupVersionMin1;
        private System.Windows.Forms.NumericUpDown nupVersionMin2;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown nupVersionMin3;
        private System.Windows.Forms.NumericUpDown nupVersionMin4;
        private System.Windows.Forms.NumericUpDown nupVersionMax1;
        private System.Windows.Forms.NumericUpDown nupVersionMax2;
        private System.Windows.Forms.NumericUpDown nupVersionMax3;
        private System.Windows.Forms.NumericUpDown nupVersionMax4;
    }
}
