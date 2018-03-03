namespace Wsus_Package_Publisher
{
    partial class RuleFileCreated
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleFileCreated));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBxFilePath = new System.Windows.Forms.TextBox();
            this.chkBxUseCsidl = new System.Windows.Forms.CheckBox();
            this.cmbBxCsidl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBxComparison = new System.Windows.Forms.ComboBox();
            this.dtpCreationDate = new System.Windows.Forms.DateTimePicker();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nupHour = new System.Windows.Forms.NumericUpDown();
            this.nupMinute = new System.Windows.Forms.NumericUpDown();
            this.nupSecond = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nupHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupSecond)).BeginInit();
            this.SuspendLayout();
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
            this.btnCancel.Click += new System.EventHandler(this.bntCancel_Click);
            // 
            // txtBxFilePath
            // 
            resources.ApplyResources(this.txtBxFilePath, "txtBxFilePath");
            this.txtBxFilePath.Name = "txtBxFilePath";
            this.txtBxFilePath.TextChanged += new System.EventHandler(this.txtBxFilePath_TextChanged);
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
            this.cmbBxCsidl.SelectedValueChanged += new System.EventHandler(this.cmbBxCsidl_SelectedValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbBxComparison
            // 
            resources.ApplyResources(this.cmbBxComparison, "cmbBxComparison");
            this.cmbBxComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxComparison.FormattingEnabled = true;
            this.cmbBxComparison.Name = "cmbBxComparison";
            this.cmbBxComparison.SelectedValueChanged += new System.EventHandler(this.cmbBxCsidl_SelectedValueChanged);
            // 
            // dtpCreationDate
            // 
            resources.ApplyResources(this.dtpCreationDate, "dtpCreationDate");
            this.dtpCreationDate.Name = "dtpCreationDate";
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // nupHour
            // 
            resources.ApplyResources(this.nupHour, "nupHour");
            this.nupHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nupHour.Name = "nupHour";
            this.nupHour.Enter += new System.EventHandler(this.nupHour_Enter);
            // 
            // nupMinute
            // 
            resources.ApplyResources(this.nupMinute, "nupMinute");
            this.nupMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nupMinute.Name = "nupMinute";
            this.nupMinute.Enter += new System.EventHandler(this.nupHour_Enter);
            // 
            // nupSecond
            // 
            resources.ApplyResources(this.nupSecond, "nupSecond");
            this.nupSecond.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nupSecond.Name = "nupSecond";
            this.nupSecond.Enter += new System.EventHandler(this.nupHour_Enter);
            // 
            // RuleFileCreated
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nupSecond);
            this.Controls.Add(this.nupMinute);
            this.Controls.Add(this.nupHour);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxDescription);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.dtpCreationDate);
            this.Controls.Add(this.cmbBxComparison);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxCsidl);
            this.Controls.Add(this.chkBxUseCsidl);
            this.Controls.Add(this.txtBxFilePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "RuleFileCreated";
            ((System.ComponentModel.ISupportInitialize)(this.nupHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupSecond)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBxFilePath;
        private System.Windows.Forms.CheckBox chkBxUseCsidl;
        private System.Windows.Forms.ComboBox cmbBxCsidl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBxComparison;
        private System.Windows.Forms.DateTimePicker dtpCreationDate;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nupHour;
        private System.Windows.Forms.NumericUpDown nupMinute;
        private System.Windows.Forms.NumericUpDown nupSecond;
    }
}
