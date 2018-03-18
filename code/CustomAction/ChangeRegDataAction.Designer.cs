namespace CustomActions
{
    partial class ChangeRegDataAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeRegDataAction));
            this.txtBxNewData = new System.Windows.Forms.TextBox();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxValue = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxRegKey = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel1 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel2 = new EasyCompany.Controls.ExtendedLabel();
            this.chkBxUseReg32 = new System.Windows.Forms.CheckBox();
            this.chkBxDefaultValue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtBxNewData
            // 
            resources.ApplyResources(this.txtBxNewData, "txtBxNewData");
            this.txtBxNewData.BackColor = System.Drawing.Color.Orange;
            this.txtBxNewData.Name = "txtBxNewData";
            this.txtBxNewData.TextChanged += new System.EventHandler(this.txtBxNewData_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtBxValue
            // 
            resources.ApplyResources(this.txtBxValue, "txtBxValue");
            this.txtBxValue.BackColor = System.Drawing.Color.Orange;
            this.txtBxValue.Name = "txtBxValue";
            this.txtBxValue.TextChanged += new System.EventHandler(this.txtBxValue_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // extendedLabel1
            // 
            resources.ApplyResources(this.extendedLabel1, "extendedLabel1");
            this.extendedLabel1.Name = "extendedLabel1";
            // 
            // extendedLabel2
            // 
            resources.ApplyResources(this.extendedLabel2, "extendedLabel2");
            this.extendedLabel2.Name = "extendedLabel2";
            // 
            // chkBxUseReg32
            // 
            resources.ApplyResources(this.chkBxUseReg32, "chkBxUseReg32");
            this.chkBxUseReg32.Name = "chkBxUseReg32";
            this.chkBxUseReg32.UseVisualStyleBackColor = true;
            this.chkBxUseReg32.CheckedChanged += new System.EventHandler(this.chkBxUseReg32_CheckedChanged);
            // 
            // chkBxDefaultValue
            // 
            resources.ApplyResources(this.chkBxDefaultValue, "chkBxDefaultValue");
            this.chkBxDefaultValue.Name = "chkBxDefaultValue";
            this.chkBxDefaultValue.UseVisualStyleBackColor = true;
            this.chkBxDefaultValue.CheckedChanged += new System.EventHandler(this.chkBxDefaultValue_CheckedChanged);
            // 
            // ChangeRegDataAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxDefaultValue);
            this.Controls.Add(this.chkBxUseReg32);
            this.Controls.Add(this.txtBxNewData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBxValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbBxHive);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxRegKey);
            this.Controls.Add(this.extendedLabel2);
            this.Controls.Add(this.extendedLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChangeRegDataAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxHive;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxRegKey;
        private EasyCompany.Controls.ExtendedLabel label2;
        private EasyCompany.Controls.ExtendedLabel label1;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.TextBox txtBxValue;
        private EasyCompany.Controls.ExtendedLabel label5;
        private System.Windows.Forms.TextBox txtBxNewData;
        private EasyCompany.Controls.ExtendedLabel extendedLabel1;
        private EasyCompany.Controls.ExtendedLabel extendedLabel2;
        private System.Windows.Forms.CheckBox chkBxUseReg32;
        private System.Windows.Forms.CheckBox chkBxDefaultValue;
    }
}
