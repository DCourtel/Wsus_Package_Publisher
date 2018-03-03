namespace Wsus_Package_Publisher
{
    partial class FrmExportUpdateProgress
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportUpdateProgress));
            this.lblProgression = new System.Windows.Forms.Label();
            this.prgBrExporting = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblProgression
            // 
            resources.ApplyResources(this.lblProgression, "lblProgression");
            this.lblProgression.Name = "lblProgression";
            // 
            // prgBrExporting
            // 
            resources.ApplyResources(this.prgBrExporting, "prgBrExporting");
            this.prgBrExporting.Name = "prgBrExporting";
            // 
            // FrmExportUpdateProgress
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.prgBrExporting);
            this.Controls.Add(this.lblProgression);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExportUpdateProgress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProgression;
        private System.Windows.Forms.ProgressBar prgBrExporting;
    }
}