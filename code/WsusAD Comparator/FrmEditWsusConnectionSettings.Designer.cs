namespace WsusADComparator
{
    partial class FrmEditWsusConnectionSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditWsusConnectionSettings));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxServerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBxUseSSL = new System.Windows.Forms.CheckBox();
            this.nupServerPort = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nupServerPort)).BeginInit();
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
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxServerName
            // 
            resources.ApplyResources(this.txtBxServerName, "txtBxServerName");
            this.txtBxServerName.Name = "txtBxServerName";
            this.txtBxServerName.TextChanged += new System.EventHandler(this.txtBxServerName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkBxUseSSL
            // 
            resources.ApplyResources(this.chkBxUseSSL, "chkBxUseSSL");
            this.chkBxUseSSL.Name = "chkBxUseSSL";
            this.chkBxUseSSL.UseVisualStyleBackColor = true;
            // 
            // nupServerPort
            // 
            resources.ApplyResources(this.nupServerPort, "nupServerPort");
            this.nupServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupServerPort.Name = "nupServerPort";
            this.nupServerPort.Value = new decimal(new int[] {
            8530,
            0,
            0,
            0});
            // 
            // FrmEditWsusConnectionSettings
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.nupServerPort);
            this.Controls.Add(this.chkBxUseSSL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxServerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmEditWsusConnectionSettings";
            ((System.ComponentModel.ISupportInitialize)(this.nupServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxServerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBxUseSSL;
        private System.Windows.Forms.NumericUpDown nupServerPort;
    }
}