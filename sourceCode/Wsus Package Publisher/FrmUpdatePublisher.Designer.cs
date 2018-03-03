namespace Wsus_Package_Publisher
{
    partial class FrmUpdatePublisher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdatePublisher));
            this.prgBrPublishing = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.chkBxVisibleInWsusConsole = new System.Windows.Forms.CheckBox();
            this.lblUpdatePublished = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgBrPublishing
            // 
            resources.ApplyResources(this.prgBrPublishing, "prgBrPublishing");
            this.prgBrPublishing.Name = "prgBrPublishing";
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
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // chkBxVisibleInWsusConsole
            // 
            resources.ApplyResources(this.chkBxVisibleInWsusConsole, "chkBxVisibleInWsusConsole");
            this.chkBxVisibleInWsusConsole.Name = "chkBxVisibleInWsusConsole";
            this.chkBxVisibleInWsusConsole.UseVisualStyleBackColor = true;
            // 
            // lblUpdatePublished
            // 
            resources.ApplyResources(this.lblUpdatePublished, "lblUpdatePublished");
            this.lblUpdatePublished.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblUpdatePublished.Name = "lblUpdatePublished";
            // 
            // FrmUpdatePublisher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblUpdatePublished);
            this.Controls.Add(this.chkBxVisibleInWsusConsole);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prgBrPublishing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdatePublisher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBrPublishing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.CheckBox chkBxVisibleInWsusConsole;
        private System.Windows.Forms.Label lblUpdatePublished;
    }
}