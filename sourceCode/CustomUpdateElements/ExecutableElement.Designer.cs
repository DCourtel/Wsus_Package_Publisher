namespace CustomUpdateElements
{
    partial class ExecutableElement
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
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxParameters = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxVariable = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkBxKillProcess = new System.Windows.Forms.CheckBox();
            this.nupTimeBeforeKilling = new System.Windows.Forms.NumericUpDown();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeBeforeKilling)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBxIcone
            // 
            this.pctBxIcone.Location = new System.Drawing.Point(3, 2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path to the File : ";
            // 
            // txtBxFilePath
            // 
            this.txtBxFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxFilePath.Location = new System.Drawing.Point(132, 60);
            this.txtBxFilePath.Name = "txtBxFilePath";
            this.txtBxFilePath.Size = new System.Drawing.Size(325, 20);
            this.txtBxFilePath.TabIndex = 0;
            this.txtBxFilePath.TextChanged += new System.EventHandler(this.txtBxFilePath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Parameters (Optionnal) : ";
            // 
            // txtBxParameters
            // 
            this.txtBxParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxParameters.Location = new System.Drawing.Point(132, 86);
            this.txtBxParameters.Name = "txtBxParameters";
            this.txtBxParameters.Size = new System.Drawing.Size(325, 20);
            this.txtBxParameters.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Store Return Code to : ";
            // 
            // cmbBxVariable
            // 
            this.cmbBxVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxVariable.Location = new System.Drawing.Point(132, 150);
            this.cmbBxVariable.Name = "cmbBxVariable";
            this.cmbBxVariable.Size = new System.Drawing.Size(188, 21);
            this.cmbBxVariable.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(382, 169);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkBxKillProcess
            // 
            this.chkBxKillProcess.AutoSize = true;
            this.chkBxKillProcess.Location = new System.Drawing.Point(6, 113);
            this.chkBxKillProcess.Name = "chkBxKillProcess";
            this.chkBxKillProcess.Size = new System.Drawing.Size(172, 17);
            this.chkBxKillProcess.TabIndex = 2;
            this.chkBxKillProcess.Text = "Kill process if it run more than : ";
            this.chkBxKillProcess.UseVisualStyleBackColor = true;
            this.chkBxKillProcess.CheckedChanged += new System.EventHandler(this.chkBxKillProcess_CheckedChanged);
            // 
            // nupTimeBeforeKilling
            // 
            this.nupTimeBeforeKilling.Enabled = false;
            this.nupTimeBeforeKilling.Location = new System.Drawing.Point(184, 112);
            this.nupTimeBeforeKilling.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nupTimeBeforeKilling.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupTimeBeforeKilling.Name = "nupTimeBeforeKilling";
            this.nupTimeBeforeKilling.Size = new System.Drawing.Size(48, 20);
            this.nupTimeBeforeKilling.TabIndex = 3;
            this.nupTimeBeforeKilling.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nupTimeBeforeKilling.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "minutes.";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Image = global::CustomUpdateElements.Properties.Resources.Delete;
            this.btnClear.Location = new System.Drawing.Point(326, 148);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(25, 25);
            this.btnClear.TabIndex = 13;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ExecutableElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxParameters);
            this.Controls.Add(this.nupTimeBeforeKilling);
            this.Controls.Add(this.chkBxKillProcess);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbBxVariable);
            this.Controls.Add(this.txtBxFilePath);
            this.Controls.Add(this.label3);
            this.Name = "ExecutableElement";
            this.DoubleClick += new System.EventHandler(this.ExecutableElement_DoubleClick);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtBxFilePath, 0);
            this.Controls.SetChildIndex(this.cmbBxVariable, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.chkBxKillProcess, 0);
            this.Controls.SetChildIndex(this.nupTimeBeforeKilling, 0);
            this.Controls.SetChildIndex(this.txtBxParameters, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeBeforeKilling)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxFilePath;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxParameters;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.ComboBox cmbBxVariable;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkBxKillProcess;
        private System.Windows.Forms.NumericUpDown nupTimeBeforeKilling;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.Button btnClear;

    }
}
