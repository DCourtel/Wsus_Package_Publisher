namespace CustomActions
{
    partial class AddRegKeyAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRegKeyAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxRegKey = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.chkBxUseReg32 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxRegKey
            // 
            resources.ApplyResources(this.txtBxRegKey, "txtBxRegKey");
            this.txtBxRegKey.BackColor = System.Drawing.Color.Orange;
            this.txtBxRegKey.Name = "txtBxRegKey";
            this.txtBxRegKey.TextChanged += new System.EventHandler(this.txtBxRegKey_TextChanged);
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
            // cmbBxHive
            // 
            this.cmbBxHive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxHive.FormattingEnabled = true;
            this.cmbBxHive.Items.AddRange(new object[] {
            resources.GetString("cmbBxHive.Items"),
            resources.GetString("cmbBxHive.Items1")});
            resources.ApplyResources(this.cmbBxHive, "cmbBxHive");
            this.cmbBxHive.Name = "cmbBxHive";
            this.cmbBxHive.SelectedIndexChanged += new System.EventHandler(this.cmbBxHive_SelectedIndexChanged);
            // 
            // chkBxUseReg32
            // 
            resources.ApplyResources(this.chkBxUseReg32, "chkBxUseReg32");
            this.chkBxUseReg32.Name = "chkBxUseReg32";
            this.chkBxUseReg32.UseVisualStyleBackColor = true;
            this.chkBxUseReg32.CheckedChanged += new System.EventHandler(this.chkBxUseReg32_CheckedChanged);
            // 
            // AddRegKeyAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxUseReg32);
            this.Controls.Add(this.cmbBxHive);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxRegKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "AddRegKeyAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxRegKey;
        private EasyCompany.Controls.ExtendedLabel label2;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.ComboBox cmbBxHive;
        private System.Windows.Forms.CheckBox chkBxUseReg32;
    }
}
