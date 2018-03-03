namespace Wsus_Package_Publisher
{
    partial class FrmInstallPendingUpdatesNow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInstallPendingUpdatesNow));
            this.btnClose = new System.Windows.Forms.Button();
            this.chkBxPersonalizeSearchString = new System.Windows.Forms.CheckBox();
            this.chkBxIncludeUpdatesthatRequireReboot = new System.Windows.Forms.CheckBox();
            this.txtBxPersonalizeSearchString = new System.Windows.Forms.TextBox();
            this.chkBxCancelIfRebootIsPending = new System.Windows.Forms.CheckBox();
            this.dtGrvComputers = new System.Windows.Forms.DataGridView();
            this.Computer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnStartUpdating = new System.Windows.Forms.Button();
            this.lblRebootWarning = new System.Windows.Forms.Label();
            this.btnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrvComputers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkBxPersonalizeSearchString
            // 
            resources.ApplyResources(this.chkBxPersonalizeSearchString, "chkBxPersonalizeSearchString");
            this.chkBxPersonalizeSearchString.Name = "chkBxPersonalizeSearchString";
            this.chkBxPersonalizeSearchString.UseVisualStyleBackColor = true;
            this.chkBxPersonalizeSearchString.CheckedChanged += new System.EventHandler(this.chkBxPersonalizeSearchString_CheckedChanged);
            // 
            // chkBxIncludeUpdatesthatRequireReboot
            // 
            resources.ApplyResources(this.chkBxIncludeUpdatesthatRequireReboot, "chkBxIncludeUpdatesthatRequireReboot");
            this.chkBxIncludeUpdatesthatRequireReboot.Name = "chkBxIncludeUpdatesthatRequireReboot";
            this.chkBxIncludeUpdatesthatRequireReboot.UseVisualStyleBackColor = true;
            this.chkBxIncludeUpdatesthatRequireReboot.CheckedChanged += new System.EventHandler(this.chkBxIncludeUpdatesthatRequireReboot_CheckedChanged);
            // 
            // txtBxPersonalizeSearchString
            // 
            resources.ApplyResources(this.txtBxPersonalizeSearchString, "txtBxPersonalizeSearchString");
            this.txtBxPersonalizeSearchString.Name = "txtBxPersonalizeSearchString";
            // 
            // chkBxCancelIfRebootIsPending
            // 
            resources.ApplyResources(this.chkBxCancelIfRebootIsPending, "chkBxCancelIfRebootIsPending");
            this.chkBxCancelIfRebootIsPending.Checked = true;
            this.chkBxCancelIfRebootIsPending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxCancelIfRebootIsPending.Name = "chkBxCancelIfRebootIsPending";
            this.chkBxCancelIfRebootIsPending.UseVisualStyleBackColor = true;
            // 
            // dtGrvComputers
            // 
            resources.ApplyResources(this.dtGrvComputers, "dtGrvComputers");
            this.dtGrvComputers.AllowUserToAddRows = false;
            this.dtGrvComputers.AllowUserToDeleteRows = false;
            this.dtGrvComputers.AllowUserToResizeRows = false;
            this.dtGrvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrvComputers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Computer,
            this.ComputerName,
            this.Status});
            this.dtGrvComputers.Name = "dtGrvComputers";
            this.dtGrvComputers.ReadOnly = true;
            this.dtGrvComputers.RowHeadersVisible = false;
            this.dtGrvComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Computer
            // 
            this.Computer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Computer, "Computer");
            this.Computer.Name = "Computer";
            this.Computer.ReadOnly = true;
            // 
            // ComputerName
            // 
            this.ComputerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ComputerName.FillWeight = 75F;
            resources.ApplyResources(this.ComputerName, "ComputerName");
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Status.FillWeight = 25F;
            resources.ApplyResources(this.Status, "Status");
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // btnStartUpdating
            // 
            resources.ApplyResources(this.btnStartUpdating, "btnStartUpdating");
            this.btnStartUpdating.Name = "btnStartUpdating";
            this.btnStartUpdating.UseVisualStyleBackColor = true;
            this.btnStartUpdating.Click += new System.EventHandler(this.btnStartUpdating_Click);
            // 
            // lblRebootWarning
            // 
            resources.ApplyResources(this.lblRebootWarning, "lblRebootWarning");
            this.lblRebootWarning.Image = global::Wsus_Package_Publisher.Properties.Resources.Warning16x16;
            this.lblRebootWarning.Name = "lblRebootWarning";
            // 
            // btnAbort
            // 
            resources.ApplyResources(this.btnAbort, "btnAbort");
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // FrmInstallPendingUpdatesNow
            // 
            this.AcceptButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.lblRebootWarning);
            this.Controls.Add(this.btnStartUpdating);
            this.Controls.Add(this.dtGrvComputers);
            this.Controls.Add(this.chkBxCancelIfRebootIsPending);
            this.Controls.Add(this.txtBxPersonalizeSearchString);
            this.Controls.Add(this.chkBxIncludeUpdatesthatRequireReboot);
            this.Controls.Add(this.chkBxPersonalizeSearchString);
            this.Controls.Add(this.btnClose);
            this.Name = "FrmInstallPendingUpdatesNow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInstallPendingUpdatesNow_FormClosing);
            this.Shown += new System.EventHandler(this.FrmInstallPendingUpdatesNow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrvComputers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkBxPersonalizeSearchString;
        private System.Windows.Forms.CheckBox chkBxIncludeUpdatesthatRequireReboot;
        private System.Windows.Forms.TextBox txtBxPersonalizeSearchString;
        private System.Windows.Forms.CheckBox chkBxCancelIfRebootIsPending;
        private System.Windows.Forms.DataGridView dtGrvComputers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Computer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Button btnStartUpdating;
        private System.Windows.Forms.Label lblRebootWarning;
        private System.Windows.Forms.Button btnAbort;
    }
}