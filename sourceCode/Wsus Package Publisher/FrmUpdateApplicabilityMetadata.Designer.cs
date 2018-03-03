namespace Wsus_Package_Publisher
{
    partial class FrmUpdateApplicabilityMetadata
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateApplicabilityMetadata));
            this.btnPublish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.chkBxEditApplicabilityMetadata = new System.Windows.Forms.CheckBox();
            this.txtBxApplicabilityMetadata = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPublish
            // 
            resources.ApplyResources(this.btnPublish, "btnPublish");
            this.btnPublish.Image = global::Wsus_Package_Publisher.Properties.Resources.Arrow_Right;
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Image = global::Wsus_Package_Publisher.Properties.Resources.Arrow_Left;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // chkBxEditApplicabilityMetadata
            // 
            resources.ApplyResources(this.chkBxEditApplicabilityMetadata, "chkBxEditApplicabilityMetadata");
            this.chkBxEditApplicabilityMetadata.Name = "chkBxEditApplicabilityMetadata";
            this.chkBxEditApplicabilityMetadata.UseVisualStyleBackColor = true;
            this.chkBxEditApplicabilityMetadata.CheckedChanged += new System.EventHandler(this.chkBxEditApplicabilityMetadata_CheckedChanged);
            // 
            // txtBxApplicabilityMetadata
            // 
            resources.ApplyResources(this.txtBxApplicabilityMetadata, "txtBxApplicabilityMetadata");
            this.txtBxApplicabilityMetadata.Name = "txtBxApplicabilityMetadata";
            this.txtBxApplicabilityMetadata.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBxApplicabilityMetadata_KeyDown);
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnReplace
            // 
            resources.ApplyResources(this.btnReplace, "btnReplace");
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnValidate
            // 
            resources.ApplyResources(this.btnValidate, "btnValidate");
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.UseVisualStyleBackColor = true;
            this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
            // 
            // FrmUpdateApplicabilityMetadata
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtBxApplicabilityMetadata);
            this.Controls.Add(this.chkBxEditApplicabilityMetadata);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPublish);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateApplicabilityMetadata";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPublish;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.CheckBox chkBxEditApplicabilityMetadata;
        private System.Windows.Forms.TextBox txtBxApplicabilityMetadata;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnValidate;
    }
}