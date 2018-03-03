namespace CustomUpdateElements
{
    partial class ScriptElement
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
            this.grpBxScriptEngine = new System.Windows.Forms.GroupBox();
            this.rdBtnPowershell = new System.Windows.Forms.RadioButton();
            this.rdBtnVbscript = new System.Windows.Forms.RadioButton();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFilename = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxArguments = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxVariable = new System.Windows.Forms.ComboBox();
            this.chkBxKillProcess = new System.Windows.Forms.CheckBox();
            this.nupTimeBeforeKilling = new System.Windows.Forms.NumericUpDown();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.grpBxScriptEngine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeBeforeKilling)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(380, 319);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpBxScriptEngine
            // 
            this.grpBxScriptEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBxScriptEngine.Controls.Add(this.rdBtnPowershell);
            this.grpBxScriptEngine.Controls.Add(this.rdBtnVbscript);
            this.grpBxScriptEngine.Location = new System.Drawing.Point(13, 79);
            this.grpBxScriptEngine.Name = "grpBxScriptEngine";
            this.grpBxScriptEngine.Size = new System.Drawing.Size(442, 100);
            this.grpBxScriptEngine.TabIndex = 0;
            this.grpBxScriptEngine.TabStop = false;
            this.grpBxScriptEngine.Text = "Script Engine ";
            // 
            // rdBtnPowershell
            // 
            this.rdBtnPowershell.AutoSize = true;
            this.rdBtnPowershell.Location = new System.Drawing.Point(6, 63);
            this.rdBtnPowershell.Name = "rdBtnPowershell";
            this.rdBtnPowershell.Size = new System.Drawing.Size(76, 17);
            this.rdBtnPowershell.TabIndex = 1;
            this.rdBtnPowershell.Text = "Powershell";
            this.rdBtnPowershell.UseVisualStyleBackColor = true;
            this.rdBtnPowershell.CheckedChanged += new System.EventHandler(this.rdBtnVbscript_CheckedChanged);
            // 
            // rdBtnVbscript
            // 
            this.rdBtnVbscript.AutoSize = true;
            this.rdBtnVbscript.Checked = true;
            this.rdBtnVbscript.Location = new System.Drawing.Point(6, 27);
            this.rdBtnVbscript.Name = "rdBtnVbscript";
            this.rdBtnVbscript.Size = new System.Drawing.Size(66, 17);
            this.rdBtnVbscript.TabIndex = 0;
            this.rdBtnVbscript.TabStop = true;
            this.rdBtnVbscript.Text = "VBScript";
            this.rdBtnVbscript.UseVisualStyleBackColor = true;
            this.rdBtnVbscript.CheckedChanged += new System.EventHandler(this.rdBtnVbscript_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filename : ";
            // 
            // txtBxFilename
            // 
            this.txtBxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxFilename.Location = new System.Drawing.Point(91, 194);
            this.txtBxFilename.Name = "txtBxFilename";
            this.txtBxFilename.Size = new System.Drawing.Size(364, 20);
            this.txtBxFilename.TabIndex = 1;
            this.txtBxFilename.TextChanged += new System.EventHandler(this.txtBxFilename_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Arguments : ";
            // 
            // txtBxArguments
            // 
            this.txtBxArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxArguments.Location = new System.Drawing.Point(91, 220);
            this.txtBxArguments.Name = "txtBxArguments";
            this.txtBxArguments.Size = new System.Drawing.Size(364, 20);
            this.txtBxArguments.TabIndex = 2;
            this.txtBxArguments.TextChanged += new System.EventHandler(this.txtBxArguments_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Store Return Code to : ";
            // 
            // cmbBxVariable
            // 
            this.cmbBxVariable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBxVariable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxVariable.Location = new System.Drawing.Point(136, 297);
            this.cmbBxVariable.Name = "cmbBxVariable";
            this.cmbBxVariable.Size = new System.Drawing.Size(117, 21);
            this.cmbBxVariable.TabIndex = 5;
            // 
            // chkBxKillProcess
            // 
            this.chkBxKillProcess.AutoSize = true;
            this.chkBxKillProcess.Location = new System.Drawing.Point(19, 254);
            this.chkBxKillProcess.Name = "chkBxKillProcess";
            this.chkBxKillProcess.Size = new System.Drawing.Size(172, 17);
            this.chkBxKillProcess.TabIndex = 3;
            this.chkBxKillProcess.Text = "Kill process if it run more than : ";
            this.chkBxKillProcess.UseVisualStyleBackColor = true;
            this.chkBxKillProcess.CheckedChanged += new System.EventHandler(this.chkBxKillProcess_CheckedChanged);
            // 
            // nupTimeBeforeKilling
            // 
            this.nupTimeBeforeKilling.Enabled = false;
            this.nupTimeBeforeKilling.Location = new System.Drawing.Point(197, 253);
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
            this.nupTimeBeforeKilling.Size = new System.Drawing.Size(56, 20);
            this.nupTimeBeforeKilling.TabIndex = 4;
            this.nupTimeBeforeKilling.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Minutes.";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Image = global::CustomUpdateElements.Properties.Resources.Delete;
            this.btnClear.Location = new System.Drawing.Point(259, 297);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(25, 25);
            this.btnClear.TabIndex = 14;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ScriptElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.chkBxKillProcess);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nupTimeBeforeKilling);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grpBxScriptEngine);
            this.Controls.Add(this.cmbBxVariable);
            this.Controls.Add(this.txtBxArguments);
            this.Controls.Add(this.txtBxFilename);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "ScriptElement";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtBxFilename, 0);
            this.Controls.SetChildIndex(this.txtBxArguments, 0);
            this.Controls.SetChildIndex(this.cmbBxVariable, 0);
            this.Controls.SetChildIndex(this.grpBxScriptEngine, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.nupTimeBeforeKilling, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.chkBxKillProcess, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.grpBxScriptEngine.ResumeLayout(false);
            this.grpBxScriptEngine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeBeforeKilling)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpBxScriptEngine;
        private System.Windows.Forms.RadioButton rdBtnPowershell;
        private System.Windows.Forms.RadioButton rdBtnVbscript;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxFilename;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxArguments;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.ComboBox cmbBxVariable;
        private System.Windows.Forms.CheckBox chkBxKillProcess;
        private System.Windows.Forms.NumericUpDown nupTimeBeforeKilling;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.Button btnClear;
    }
}
