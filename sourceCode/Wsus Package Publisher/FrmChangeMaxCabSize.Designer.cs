namespace Wsus_Package_Publisher
{
    partial class FrmChangeMaxCabSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChangeMaxCabSize));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nupNewSetting = new System.Windows.Forms.NumericUpDown();
            this.lblCurrentSetting = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFilename = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.nupFileSize = new System.Windows.Forms.NumericUpDown();
            this.txtBxFilename = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupNewSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // nupNewSetting
            // 
            resources.ApplyResources(this.nupNewSetting, "nupNewSetting");
            this.nupNewSetting.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.nupNewSetting.Minimum = new decimal(new int[] {
            384,
            0,
            0,
            0});
            this.nupNewSetting.Name = "nupNewSetting";
            this.nupNewSetting.Value = new decimal(new int[] {
            384,
            0,
            0,
            0});
            // 
            // lblCurrentSetting
            // 
            resources.ApplyResources(this.lblCurrentSetting, "lblCurrentSetting");
            this.lblCurrentSetting.Name = "lblCurrentSetting";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lblFilename
            // 
            resources.ApplyResources(this.lblFilename, "lblFilename");
            this.lblFilename.Name = "lblFilename";
            // 
            // lblSize
            // 
            resources.ApplyResources(this.lblSize, "lblSize");
            this.lblSize.Name = "lblSize";
            // 
            // nupFileSize
            // 
            resources.ApplyResources(this.nupFileSize, "nupFileSize");
            this.nupFileSize.Name = "nupFileSize";
            this.nupFileSize.ReadOnly = true;
            // 
            // txtBxFilename
            // 
            resources.ApplyResources(this.txtBxFilename, "txtBxFilename");
            this.txtBxFilename.Name = "txtBxFilename";
            this.txtBxFilename.ReadOnly = true;
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // FrmChangeMaxCabSize
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.txtBxFilename);
            this.Controls.Add(this.nupFileSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCurrentSetting);
            this.Controls.Add(this.nupNewSetting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangeMaxCabSize";
            ((System.ComponentModel.ISupportInitialize)(this.nupNewSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupFileSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nupNewSetting;
        private System.Windows.Forms.Label lblCurrentSetting;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.NumericUpDown nupFileSize;
        private System.Windows.Forms.TextBox txtBxFilename;
        private System.Windows.Forms.Button btnReset;
    }
}