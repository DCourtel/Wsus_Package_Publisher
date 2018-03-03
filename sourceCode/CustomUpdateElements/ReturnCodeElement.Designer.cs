namespace CustomUpdateElements
{
    partial class ReturnCodeElement
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
            this.btnOk = new System.Windows.Forms.Button();
            this.grpBxReturnCode = new System.Windows.Forms.GroupBox();
            this.cmbBxVariable = new System.Windows.Forms.ComboBox();
            this.nupReturnCode = new System.Windows.Forms.NumericUpDown();
            this.rdBtnVariable = new System.Windows.Forms.RadioButton();
            this.rdBtnStatic = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.grpBxReturnCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupReturnCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(380, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpBxReturnCode
            // 
            this.grpBxReturnCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBxReturnCode.Controls.Add(this.cmbBxVariable);
            this.grpBxReturnCode.Controls.Add(this.nupReturnCode);
            this.grpBxReturnCode.Controls.Add(this.rdBtnVariable);
            this.grpBxReturnCode.Controls.Add(this.rdBtnStatic);
            this.grpBxReturnCode.Location = new System.Drawing.Point(8, 57);
            this.grpBxReturnCode.Name = "grpBxReturnCode";
            this.grpBxReturnCode.Size = new System.Drawing.Size(447, 106);
            this.grpBxReturnCode.TabIndex = 0;
            this.grpBxReturnCode.TabStop = false;
            this.grpBxReturnCode.Text = "Method ";
            // 
            // cmbBxVariable
            // 
            this.cmbBxVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxVariable.Enabled = false;
            this.cmbBxVariable.Location = new System.Drawing.Point(84, 67);
            this.cmbBxVariable.Name = "cmbBxVariable";
            this.cmbBxVariable.Size = new System.Drawing.Size(236, 21);
            this.cmbBxVariable.TabIndex = 3;
            this.cmbBxVariable.SelectedIndexChanged += new System.EventHandler(this.cmbBxVariable_SelectedIndexChanged);
            // 
            // nupReturnCode
            // 
            this.nupReturnCode.Enabled = false;
            this.nupReturnCode.Location = new System.Drawing.Point(84, 29);
            this.nupReturnCode.Name = "nupReturnCode";
            this.nupReturnCode.Size = new System.Drawing.Size(121, 20);
            this.nupReturnCode.TabIndex = 1;
            this.nupReturnCode.ValueChanged += new System.EventHandler(this.nupReturnCode_ValueChanged);
            // 
            // rdBtnVariable
            // 
            this.rdBtnVariable.AutoSize = true;
            this.rdBtnVariable.Location = new System.Drawing.Point(6, 68);
            this.rdBtnVariable.Name = "rdBtnVariable";
            this.rdBtnVariable.Size = new System.Drawing.Size(72, 17);
            this.rdBtnVariable.TabIndex = 2;
            this.rdBtnVariable.TabStop = true;
            this.rdBtnVariable.Text = "Variable : ";
            this.rdBtnVariable.UseVisualStyleBackColor = true;
            this.rdBtnVariable.CheckedChanged += new System.EventHandler(this.rdBtnVariable_CheckedChanged);
            // 
            // rdBtnStatic
            // 
            this.rdBtnStatic.AutoSize = true;
            this.rdBtnStatic.Location = new System.Drawing.Point(6, 29);
            this.rdBtnStatic.Name = "rdBtnStatic";
            this.rdBtnStatic.Size = new System.Drawing.Size(61, 17);
            this.rdBtnStatic.TabIndex = 0;
            this.rdBtnStatic.TabStop = true;
            this.rdBtnStatic.Text = "Static : ";
            this.rdBtnStatic.UseVisualStyleBackColor = true;
            this.rdBtnStatic.CheckedChanged += new System.EventHandler(this.rdBtnStatic_CheckedChanged);
            // 
            // ReturnCodeElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.grpBxReturnCode);
            this.Name = "ReturnCodeElement";
            this.DoubleClick += new System.EventHandler(this.ReturnCodeElement_DoubleClick);
            this.Controls.SetChildIndex(this.grpBxReturnCode, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.grpBxReturnCode.ResumeLayout(false);
            this.grpBxReturnCode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupReturnCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpBxReturnCode;
        private System.Windows.Forms.ComboBox cmbBxVariable;
        private System.Windows.Forms.NumericUpDown nupReturnCode;
        private System.Windows.Forms.RadioButton rdBtnVariable;
        private System.Windows.Forms.RadioButton rdBtnStatic;
    }
}
