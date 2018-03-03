namespace Wsus_Package_Publisher
{
    partial class FrmSendDebugInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSendDebugInfo));
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.txtBxSendDebugInfo = new System.Windows.Forms.TextBox();
            this.lnkLblShowInformations = new System.Windows.Forms.LinkLabel();
            this.pctBxIcone = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxComments = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            resources.ApplyResources(this.btnYes, "btnYes");
            this.btnYes.Name = "btnYes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Name = "btnNo";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // txtBxSendDebugInfo
            // 
            resources.ApplyResources(this.txtBxSendDebugInfo, "txtBxSendDebugInfo");
            this.txtBxSendDebugInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBxSendDebugInfo.Name = "txtBxSendDebugInfo";
            this.txtBxSendDebugInfo.ReadOnly = true;
            this.txtBxSendDebugInfo.TabStop = false;
            // 
            // lnkLblShowInformations
            // 
            resources.ApplyResources(this.lnkLblShowInformations, "lnkLblShowInformations");
            this.lnkLblShowInformations.Name = "lnkLblShowInformations";
            this.lnkLblShowInformations.TabStop = true;
            this.lnkLblShowInformations.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblShowInformations_LinkClicked);
            // 
            // pctBxIcone
            // 
            this.pctBxIcone.Image = global::Wsus_Package_Publisher.Properties.Resources.WentWrong;
            resources.ApplyResources(this.pctBxIcone, "pctBxIcone");
            this.pctBxIcone.Name = "pctBxIcone";
            this.pctBxIcone.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxMail
            // 
            resources.ApplyResources(this.txtBxMail, "txtBxMail");
            this.txtBxMail.Name = "txtBxMail";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxComments
            // 
            resources.ApplyResources(this.txtBxComments, "txtBxComments");
            this.txtBxComments.Name = "txtBxComments";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FrmSendDebugInfo
            // 
            this.AcceptButton = this.btnYes;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnNo;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxComments);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxMail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pctBxIcone);
            this.Controls.Add(this.lnkLblShowInformations);
            this.Controls.Add(this.txtBxSendDebugInfo);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSendDebugInfo";
            this.Shown += new System.EventHandler(this.FrmSendDebugInfo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.TextBox txtBxSendDebugInfo;
        private System.Windows.Forms.LinkLabel lnkLblShowInformations;
        private System.Windows.Forms.PictureBox pctBxIcone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxComments;
        private System.Windows.Forms.Label label3;
    }
}