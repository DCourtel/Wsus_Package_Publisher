namespace CustomUpdateElements
{
    partial class ServiceElement
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
            this.cmbBxAction = new System.Windows.Forms.ComboBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxServiceName = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxPathToEXE = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxStartupMode = new System.Windows.Forms.ComboBox();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxStartingAccount = new System.Windows.Forms.ComboBox();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxLogin = new System.Windows.Forms.TextBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxPassword = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Action : ";
            // 
            // cmbBxAction
            // 
            this.cmbBxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxAction.FormattingEnabled = true;
            this.cmbBxAction.Location = new System.Drawing.Point(115, 60);
            this.cmbBxAction.Name = "cmbBxAction";
            this.cmbBxAction.Size = new System.Drawing.Size(326, 21);
            this.cmbBxAction.TabIndex = 0;
            this.cmbBxAction.SelectedIndexChanged += new System.EventHandler(this.cmbBxAction_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Service Name : ";
            // 
            // txtBxServiceName
            // 
            this.txtBxServiceName.Location = new System.Drawing.Point(115, 103);
            this.txtBxServiceName.Name = "txtBxServiceName";
            this.txtBxServiceName.Size = new System.Drawing.Size(326, 20);
            this.txtBxServiceName.TabIndex = 1;
            this.txtBxServiceName.TextChanged += new System.EventHandler(this.txtBxServiceName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Path to EXE : ";
            // 
            // txtBxPathToEXE
            // 
            this.txtBxPathToEXE.Enabled = false;
            this.txtBxPathToEXE.Location = new System.Drawing.Point(115, 129);
            this.txtBxPathToEXE.Name = "txtBxPathToEXE";
            this.txtBxPathToEXE.Size = new System.Drawing.Size(326, 20);
            this.txtBxPathToEXE.TabIndex = 2;
            this.txtBxPathToEXE.TextChanged += new System.EventHandler(this.txtBxPathToEXE_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Startup Mode : ";
            // 
            // cmbBxStartupMode
            // 
            this.cmbBxStartupMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxStartupMode.Enabled = false;
            this.cmbBxStartupMode.FormattingEnabled = true;
            this.cmbBxStartupMode.Location = new System.Drawing.Point(115, 155);
            this.cmbBxStartupMode.Name = "cmbBxStartupMode";
            this.cmbBxStartupMode.Size = new System.Drawing.Size(326, 21);
            this.cmbBxStartupMode.TabIndex = 3;
            this.cmbBxStartupMode.SelectedIndexChanged += new System.EventHandler(this.cmbBxStartupMode_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Starting Account : ";
            // 
            // cmbBxStartingAccount
            // 
            this.cmbBxStartingAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxStartingAccount.Enabled = false;
            this.cmbBxStartingAccount.FormattingEnabled = true;
            this.cmbBxStartingAccount.Location = new System.Drawing.Point(115, 182);
            this.cmbBxStartingAccount.Name = "cmbBxStartingAccount";
            this.cmbBxStartingAccount.Size = new System.Drawing.Size(326, 21);
            this.cmbBxStartingAccount.TabIndex = 4;
            this.cmbBxStartingAccount.SelectedIndexChanged += new System.EventHandler(this.cmbBxStartingAccount_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Login : ";
            // 
            // txtBxLogin
            // 
            this.txtBxLogin.Enabled = false;
            this.txtBxLogin.Location = new System.Drawing.Point(115, 209);
            this.txtBxLogin.Name = "txtBxLogin";
            this.txtBxLogin.Size = new System.Drawing.Size(326, 20);
            this.txtBxLogin.TabIndex = 5;
            this.txtBxLogin.TextChanged += new System.EventHandler(this.txtBxLogin_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Password : ";
            // 
            // txtBxPassword
            // 
            this.txtBxPassword.Enabled = false;
            this.txtBxPassword.Location = new System.Drawing.Point(115, 235);
            this.txtBxPassword.Name = "txtBxPassword";
            this.txtBxPassword.PasswordChar = '*';
            this.txtBxPassword.Size = new System.Drawing.Size(326, 20);
            this.txtBxPassword.TabIndex = 6;
            this.txtBxPassword.TextChanged += new System.EventHandler(this.txtBxPassword_TextChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(381, 262);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ServiceElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtBxLogin);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbBxStartupMode);
            this.Controls.Add(this.cmbBxStartingAccount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxPathToEXE);
            this.Controls.Add(this.txtBxServiceName);
            this.Controls.Add(this.cmbBxAction);
            this.Controls.Add(this.label3);
            this.Name = "ServiceElement";
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbBxAction, 0);
            this.Controls.SetChildIndex(this.txtBxServiceName, 0);
            this.Controls.SetChildIndex(this.txtBxPathToEXE, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbBxStartingAccount, 0);
            this.Controls.SetChildIndex(this.cmbBxStartupMode, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.txtBxLogin, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtBxPassword, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.ComboBox cmbBxAction;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxServiceName;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxPathToEXE;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.ComboBox cmbBxStartupMode;
        private EasyCompany.Controls.ExtendedLabel label5;
        private System.Windows.Forms.ComboBox cmbBxStartingAccount;
        private EasyCompany.Controls.ExtendedLabel label6;
        private System.Windows.Forms.TextBox txtBxLogin;
        private EasyCompany.Controls.ExtendedLabel label7;
        private System.Windows.Forms.TextBox txtBxPassword;
        private System.Windows.Forms.Button btnOk;
    }
}
