namespace Wsus_Package_Publisher
{
    partial class FrmCatalogUpdateImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCatalogUpdateImporter));
            this.prgBarOverAll = new System.Windows.Forms.ProgressBar();
            this.prgBarCurrent = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProgression = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxAverageSpeed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // prgBarOverAll
            // 
            resources.ApplyResources(this.prgBarOverAll, "prgBarOverAll");
            this.prgBarOverAll.Name = "prgBarOverAll";
            this.prgBarOverAll.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // prgBarCurrent
            // 
            resources.ApplyResources(this.prgBarCurrent, "prgBarCurrent");
            this.prgBarCurrent.Name = "prgBarCurrent";
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
            // lblProgression
            // 
            resources.ApplyResources(this.lblProgression, "lblProgression");
            this.lblProgression.Name = "lblProgression";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxAverageSpeed
            // 
            resources.ApplyResources(this.txtBxAverageSpeed, "txtBxAverageSpeed");
            this.txtBxAverageSpeed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBxAverageSpeed.Name = "txtBxAverageSpeed";
            this.txtBxAverageSpeed.ReadOnly = true;
            this.txtBxAverageSpeed.TabStop = false;
            // 
            // FrmCatalogUpdateImporter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.txtBxAverageSpeed);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblProgression);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prgBarCurrent);
            this.Controls.Add(this.prgBarOverAll);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCatalogUpdateImporter";
            this.Shown += new System.EventHandler(this.FrmCatalogUpdateImporter_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBarOverAll;
        private System.Windows.Forms.ProgressBar prgBarCurrent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProgression;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxAverageSpeed;
    }
}