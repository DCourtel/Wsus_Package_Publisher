namespace Wsus_Package_Publisher
{
    partial class RuleProcessorArchitecture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleProcessorArchitecture));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbBxProcessorArchitecture = new System.Windows.Forms.ComboBox();
            this.chkBxInverseRule = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
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
            // cmbBxProcessorArchitecture
            // 
            resources.ApplyResources(this.cmbBxProcessorArchitecture, "cmbBxProcessorArchitecture");
            this.cmbBxProcessorArchitecture.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxProcessorArchitecture.FormattingEnabled = true;
            this.cmbBxProcessorArchitecture.Items.AddRange(new object[] {
            resources.GetString("cmbBxProcessorArchitecture.Items"),
            resources.GetString("cmbBxProcessorArchitecture.Items1"),
            resources.GetString("cmbBxProcessorArchitecture.Items2")});
            this.cmbBxProcessorArchitecture.Name = "cmbBxProcessorArchitecture";
            this.cmbBxProcessorArchitecture.SelectedIndexChanged += new System.EventHandler(this.cmbBxProcessorArchitecture_SelectedIndexChanged);
            // 
            // chkBxInverseRule
            // 
            resources.ApplyResources(this.chkBxInverseRule, "chkBxInverseRule");
            this.chkBxInverseRule.Name = "chkBxInverseRule";
            this.chkBxInverseRule.UseVisualStyleBackColor = true;
            this.chkBxInverseRule.CheckedChanged += new System.EventHandler(this.chkBxInverseRule_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // RuleProcessorArchitecture
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkBxInverseRule);
            this.Controls.Add(this.cmbBxProcessorArchitecture);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleProcessorArchitecture";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmbBxProcessorArchitecture;
        private System.Windows.Forms.CheckBox chkBxInverseRule;
        private System.Windows.Forms.Label label1;
    }
}
