namespace CustomActions
{
    partial class RenameRegValueAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameRegValueAction));
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxRegKey = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxValueName = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxNewName = new System.Windows.Forms.TextBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.chkBxUseReg32 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
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
            // txtBxRegKey
            // 
            resources.ApplyResources(this.txtBxRegKey, "txtBxRegKey");
            this.txtBxRegKey.BackColor = System.Drawing.Color.Orange;
            this.txtBxRegKey.Name = "txtBxRegKey";
            this.txtBxRegKey.TextChanged += new System.EventHandler(this.txtBxRegKey_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxValueName
            // 
            resources.ApplyResources(this.txtBxValueName, "txtBxValueName");
            this.txtBxValueName.BackColor = System.Drawing.Color.Orange;
            this.txtBxValueName.Name = "txtBxValueName";
            this.txtBxValueName.TextChanged += new System.EventHandler(this.txtBxValueName_TextChanged);
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
            // txtBxNewName
            // 
            resources.ApplyResources(this.txtBxNewName, "txtBxNewName");
            this.txtBxNewName.BackColor = System.Drawing.Color.Orange;
            this.txtBxNewName.Name = "txtBxNewName";
            this.txtBxNewName.TextChanged += new System.EventHandler(this.txtBxNewName_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // chkBxUseReg32
            // 
            resources.ApplyResources(this.chkBxUseReg32, "chkBxUseReg32");
            this.chkBxUseReg32.Name = "chkBxUseReg32";
            this.chkBxUseReg32.UseVisualStyleBackColor = true;
            this.chkBxUseReg32.CheckedChanged += new System.EventHandler(this.chkBxUseReg32_CheckedChanged);
            // 
            // RenameRegValueAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxUseReg32);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBxNewName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtBxValueName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxRegKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxHive);
            this.Name = "RenameRegValueAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxHive;
        private EasyCompany.Controls.ExtendedLabel label1;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxRegKey;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxValueName;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private EasyCompany.Controls.ExtendedLabel label6;
        private System.Windows.Forms.TextBox txtBxNewName;
        private EasyCompany.Controls.ExtendedLabel label7;
        private System.Windows.Forms.CheckBox chkBxUseReg32;
    }
}
