namespace Wsus_Package_Publisher
{
    partial class FrmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.pctBxLogo = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblWsusPackagePublisher = new System.Windows.Forms.Label();
            this.lblRelease = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblServerVersion = new System.Windows.Forms.Label();
            this.txtBxServers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxUserRole = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxCertificateStatus = new System.Windows.Forms.TextBox();
            this.txtBxCredits = new System.Windows.Forms.TextBox();
            this.lblConsoleVersion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBxExpirationDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBxKeyLength = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBxLogo
            // 
            resources.ApplyResources(this.pctBxLogo, "pctBxLogo");
            this.pctBxLogo.Name = "pctBxLogo";
            this.pctBxLogo.TabStop = false;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblWsusPackagePublisher
            // 
            resources.ApplyResources(this.lblWsusPackagePublisher, "lblWsusPackagePublisher");
            this.lblWsusPackagePublisher.Name = "lblWsusPackagePublisher";
            // 
            // lblRelease
            // 
            resources.ApplyResources(this.lblRelease, "lblRelease");
            this.lblRelease.Name = "lblRelease";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblServerVersion
            // 
            resources.ApplyResources(this.lblServerVersion, "lblServerVersion");
            this.lblServerVersion.Name = "lblServerVersion";
            // 
            // txtBxServers
            // 
            resources.ApplyResources(this.txtBxServers, "txtBxServers");
            this.txtBxServers.Name = "txtBxServers";
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
            // txtBxUserRole
            // 
            resources.ApplyResources(this.txtBxUserRole, "txtBxUserRole");
            this.txtBxUserRole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxUserRole.Name = "txtBxUserRole";
            this.txtBxUserRole.ReadOnly = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxCertificateStatus
            // 
            resources.ApplyResources(this.txtBxCertificateStatus, "txtBxCertificateStatus");
            this.txtBxCertificateStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxCertificateStatus.Name = "txtBxCertificateStatus";
            this.txtBxCertificateStatus.ReadOnly = true;
            // 
            // txtBxCredits
            // 
            resources.ApplyResources(this.txtBxCredits, "txtBxCredits");
            this.txtBxCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBxCredits.Name = "txtBxCredits";
            this.txtBxCredits.ReadOnly = true;
            // 
            // lblConsoleVersion
            // 
            resources.ApplyResources(this.lblConsoleVersion, "lblConsoleVersion");
            this.lblConsoleVersion.Name = "lblConsoleVersion";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtBxExpirationDate
            // 
            resources.ApplyResources(this.txtBxExpirationDate, "txtBxExpirationDate");
            this.txtBxExpirationDate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxExpirationDate.Name = "txtBxExpirationDate";
            this.txtBxExpirationDate.ReadOnly = true;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtBxKeyLength
            // 
            resources.ApplyResources(this.txtBxKeyLength, "txtBxKeyLength");
            this.txtBxKeyLength.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxKeyLength.Name = "txtBxKeyLength";
            this.txtBxKeyLength.ReadOnly = true;
            // 
            // FrmAbout
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblConsoleVersion);
            this.Controls.Add(this.txtBxCredits);
            this.Controls.Add(this.txtBxExpirationDate);
            this.Controls.Add(this.txtBxKeyLength);
            this.Controls.Add(this.txtBxCertificateStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxUserRole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxServers);
            this.Controls.Add(this.lblServerVersion);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblRelease);
            this.Controls.Add(this.lblWsusPackagePublisher);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pctBxLogo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            ((System.ComponentModel.ISupportInitialize)(this.pctBxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBxLogo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblWsusPackagePublisher;
        private System.Windows.Forms.Label lblRelease;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblServerVersion;
        private System.Windows.Forms.TextBox txtBxServers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxUserRole;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxCertificateStatus;
        private System.Windows.Forms.TextBox txtBxCredits;
        private System.Windows.Forms.Label lblConsoleVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBxExpirationDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBxKeyLength;
    }
}