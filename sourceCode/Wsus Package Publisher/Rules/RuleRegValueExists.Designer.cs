namespace Wsus_Package_Publisher
{
    partial class RuleRegValueExists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleRegValueExists));
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxSubKey = new System.Windows.Forms.TextBox();
            this.chkBxReverseRule = new System.Windows.Forms.CheckBox();
            this.chkBxRegType32 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxValue = new System.Windows.Forms.TextBox();
            this.cmbBxType = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBxSpecifyType = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            this.txtBxDescription.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxSubKey
            // 
            resources.ApplyResources(this.txtBxSubKey, "txtBxSubKey");
            this.txtBxSubKey.Name = "txtBxSubKey";
            this.txtBxSubKey.TextChanged += new System.EventHandler(this.txtBxSubKey_TextChanged);
            // 
            // chkBxReverseRule
            // 
            resources.ApplyResources(this.chkBxReverseRule, "chkBxReverseRule");
            this.chkBxReverseRule.Name = "chkBxReverseRule";
            this.chkBxReverseRule.UseVisualStyleBackColor = true;
            // 
            // chkBxRegType32
            // 
            resources.ApplyResources(this.chkBxRegType32, "chkBxRegType32");
            this.chkBxRegType32.Name = "chkBxRegType32";
            this.chkBxRegType32.UseVisualStyleBackColor = true;
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
            // txtBxValue
            // 
            resources.ApplyResources(this.txtBxValue, "txtBxValue");
            this.txtBxValue.Name = "txtBxValue";
            // 
            // cmbBxType
            // 
            resources.ApplyResources(this.cmbBxType, "cmbBxType");
            this.cmbBxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxType.FormattingEnabled = true;
            this.cmbBxType.Name = "cmbBxType";
            this.cmbBxType.SelectedIndexChanged += new System.EventHandler(this.cmbBxType_SelectedIndexChanged);
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
            // chkBxSpecifyType
            // 
            resources.ApplyResources(this.chkBxSpecifyType, "chkBxSpecifyType");
            this.chkBxSpecifyType.Name = "chkBxSpecifyType";
            this.chkBxSpecifyType.UseVisualStyleBackColor = true;
            this.chkBxSpecifyType.CheckedChanged += new System.EventHandler(this.chkBxSpecifyType_CheckedChanged);
            // 
            // RuleRegValueExists
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxSpecifyType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbBxType);
            this.Controls.Add(this.txtBxValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBxRegType32);
            this.Controls.Add(this.chkBxReverseRule);
            this.Controls.Add(this.txtBxSubKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxDescription);
            this.Name = "RuleRegValueExists";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxSubKey;
        private System.Windows.Forms.CheckBox chkBxReverseRule;
        private System.Windows.Forms.CheckBox chkBxRegType32;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxValue;
        private System.Windows.Forms.ComboBox cmbBxType;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBxSpecifyType;
    }
}
