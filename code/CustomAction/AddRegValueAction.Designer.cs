namespace CustomActions
{
    partial class AddRegValueAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddRegValueAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxRegKey = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxValueName = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxData = new System.Windows.Forms.TextBox();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxValueType = new System.Windows.Forms.ComboBox();
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.label8 = new EasyCompany.Controls.ExtendedLabel();
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
            // txtBxValueName
            // 
            resources.ApplyResources(this.txtBxValueName, "txtBxValueName");
            this.txtBxValueName.BackColor = System.Drawing.Color.Orange;
            this.txtBxValueName.Name = "txtBxValueName";
            this.txtBxValueName.TextChanged += new System.EventHandler(this.txtBxValueName_TextChanged);
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
            // txtBxData
            // 
            resources.ApplyResources(this.txtBxData, "txtBxData");
            this.txtBxData.BackColor = System.Drawing.Color.Orange;
            this.txtBxData.Name = "txtBxData";
            this.txtBxData.TextChanged += new System.EventHandler(this.txtBxData_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // cmbBxValueType
            // 
            this.cmbBxValueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxValueType.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxValueType, "cmbBxValueType");
            this.cmbBxValueType.Name = "cmbBxValueType";
            this.cmbBxValueType.SelectedIndexChanged += new System.EventHandler(this.cmbBxValueType_SelectedIndexChanged);
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
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // chkBxUseReg32
            // 
            resources.ApplyResources(this.chkBxUseReg32, "chkBxUseReg32");
            this.chkBxUseReg32.Name = "chkBxUseReg32";
            this.chkBxUseReg32.UseVisualStyleBackColor = true;
            this.chkBxUseReg32.CheckedChanged += new System.EventHandler(this.chkBxUseReg32_CheckedChanged);
            // 
            // AddRegValueAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkBxUseReg32);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBxHive);
            this.Controls.Add(this.cmbBxValueType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBxValueName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxRegKey);
            this.Controls.Add(this.label1);
            this.Name = "AddRegValueAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxRegKey;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxValueName;
        private EasyCompany.Controls.ExtendedLabel label3;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private System.Windows.Forms.TextBox txtBxData;
        private EasyCompany.Controls.ExtendedLabel label6;
        private EasyCompany.Controls.ExtendedLabel label7;
        private System.Windows.Forms.ComboBox cmbBxValueType;
        private System.Windows.Forms.ComboBox cmbBxHive;
        private EasyCompany.Controls.ExtendedLabel label8;
        private System.Windows.Forms.CheckBox chkBxUseReg32;
    }
}
