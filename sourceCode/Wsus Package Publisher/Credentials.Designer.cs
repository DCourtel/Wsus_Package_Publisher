namespace Wsus_Package_Publisher
{
    partial class Credentials
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credentials));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxPassword = new System.Windows.Forms.TextBox();
            this.txtBxLogin = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.chkBxRememberCredentials = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // txtBxPassword
            // 
            resources.ApplyResources(this.txtBxPassword, "txtBxPassword");
            this.txtBxPassword.Name = "txtBxPassword";
            this.txtBxPassword.UseSystemPasswordChar = true;
            // 
            // txtBxLogin
            // 
            resources.ApplyResources(this.txtBxLogin, "txtBxLogin");
            this.txtBxLogin.Name = "txtBxLogin";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // chkBxRememberCredentials
            // 
            resources.ApplyResources(this.chkBxRememberCredentials, "chkBxRememberCredentials");
            this.chkBxRememberCredentials.Name = "chkBxRememberCredentials";
            this.chkBxRememberCredentials.UseVisualStyleBackColor = true;
            this.chkBxRememberCredentials.CheckedChanged += new System.EventHandler(this.chkBxRememberCredentials_CheckedChanged);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Credentials
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkBxRememberCredentials);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBxLogin);
            this.Controls.Add(this.txtBxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Credentials";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxPassword;
        private System.Windows.Forms.TextBox txtBxLogin;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkBxRememberCredentials;
        private System.Windows.Forms.Button btnCancel;
    }
}