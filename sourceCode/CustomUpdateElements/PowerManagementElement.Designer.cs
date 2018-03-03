namespace CustomUpdateElements
{
    partial class PowerManagementElement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerManagementElement));
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxPowerAction = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBxIcone
            // 
            this.pctBxIcone.Image = ((System.Drawing.Image)(resources.GetObject("pctBxIcone.Image")));
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(380, 65);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Action : ";
            // 
            // cmbBxPowerAction
            // 
            this.cmbBxPowerAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxPowerAction.FormattingEnabled = true;
            this.cmbBxPowerAction.Location = new System.Drawing.Point(74, 65);
            this.cmbBxPowerAction.Name = "cmbBxPowerAction";
            this.cmbBxPowerAction.Size = new System.Drawing.Size(193, 21);
            this.cmbBxPowerAction.TabIndex = 5;
            this.cmbBxPowerAction.SelectedIndexChanged += new System.EventHandler(this.cmbBxPowerAction_SelectedIndexChanged);
            // 
            // PowerManagementElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxPowerAction);
            this.Controls.Add(this.btnOk);
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Name = "PowerManagementElement";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.cmbBxPowerAction, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.ComboBox cmbBxPowerAction;
    }
}
