namespace CustomUpdateElements
{
    partial class RegistryKeyElement
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
            this.txtBxKey = new System.Windows.Forms.TextBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxHive = new System.Windows.Forms.ComboBox();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxActions = new System.Windows.Forms.ComboBox();
            this.txtBxKeyName = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBxKey
            // 
            this.txtBxKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxKey.Location = new System.Drawing.Point(84, 129);
            this.txtBxKey.Name = "txtBxKey";
            this.txtBxKey.Size = new System.Drawing.Size(370, 20);
            this.txtBxKey.TabIndex = 17;
            this.txtBxKey.TextChanged += new System.EventHandler(this.txtBxKey_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 132);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Key : ";
            // 
            // cmbBxHive
            // 
            this.cmbBxHive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxHive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxHive.FormattingEnabled = true;
            this.cmbBxHive.Items.AddRange(new object[] {
            "HKEY_LOCAL_MACHINE",
            "HKEY_CURRENT_USER"});
            this.cmbBxHive.Location = new System.Drawing.Point(84, 102);
            this.cmbBxHive.Name = "cmbBxHive";
            this.cmbBxHive.Size = new System.Drawing.Size(183, 21);
            this.cmbBxHive.TabIndex = 16;
            this.cmbBxHive.SelectedIndexChanged += new System.EventHandler(this.cmbBxHive_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Hive : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Action : ";
            // 
            // cmbBxActions
            // 
            this.cmbBxActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxActions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxActions.FormattingEnabled = true;
            this.cmbBxActions.Location = new System.Drawing.Point(84, 75);
            this.cmbBxActions.Name = "cmbBxActions";
            this.cmbBxActions.Size = new System.Drawing.Size(371, 21);
            this.cmbBxActions.TabIndex = 15;
            this.cmbBxActions.SelectedIndexChanged += new System.EventHandler(this.cmbBxActions_SelectedIndexChanged);
            // 
            // txtBxKeyName
            // 
            this.txtBxKeyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxKeyName.Enabled = false;
            this.txtBxKeyName.Location = new System.Drawing.Point(83, 155);
            this.txtBxKeyName.Name = "txtBxKeyName";
            this.txtBxKeyName.Size = new System.Drawing.Size(371, 20);
            this.txtBxKeyName.TabIndex = 19;
            this.txtBxKeyName.TextChanged += new System.EventHandler(this.txtBxKeyName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "New name : ";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(379, 214);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // RegistryKeyElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBxKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbBxHive);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxActions);
            this.Controls.Add(this.txtBxKeyName);
            this.Controls.Add(this.label2);
            this.Name = "RegistryKeyElement";
            this.DoubleClick += new System.EventHandler(this.RegistryElement_DoubleClick);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtBxKeyName, 0);
            this.Controls.SetChildIndex(this.cmbBxActions, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbBxHive, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtBxKey, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxKey;
        private EasyCompany.Controls.ExtendedLabel label7;
        private System.Windows.Forms.ComboBox cmbBxHive;
        private EasyCompany.Controls.ExtendedLabel label6;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.ComboBox cmbBxActions;
        private System.Windows.Forms.TextBox txtBxKeyName;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.Button btnOk;
    }
}
