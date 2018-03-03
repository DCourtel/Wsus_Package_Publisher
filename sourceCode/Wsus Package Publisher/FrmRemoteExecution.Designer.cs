namespace Wsus_Package_Publisher
{
    partial class FrmRemoteExecution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteExecution));
            this.dtgvRemoteExecution = new System.Windows.Forms.DataGridView();
            this.Computer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSummary = new System.Windows.Forms.Label();
            this.prgBarSendCommand = new System.Windows.Forms.ProgressBar();
            this.chkBxCloseWhenFinished = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRemoteExecution)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgvRemoteExecution
            // 
            resources.ApplyResources(this.dtgvRemoteExecution, "dtgvRemoteExecution");
            this.dtgvRemoteExecution.AllowUserToAddRows = false;
            this.dtgvRemoteExecution.AllowUserToDeleteRows = false;
            this.dtgvRemoteExecution.AllowUserToOrderColumns = true;
            this.dtgvRemoteExecution.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvRemoteExecution.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Computer,
            this.Result});
            this.dtgvRemoteExecution.Name = "dtgvRemoteExecution";
            this.dtgvRemoteExecution.ReadOnly = true;
            this.dtgvRemoteExecution.RowHeadersVisible = false;
            this.dtgvRemoteExecution.ShowEditingIcon = false;
            // 
            // Computer
            // 
            this.Computer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Computer.FillWeight = 60F;
            resources.ApplyResources(this.Computer, "Computer");
            this.Computer.Name = "Computer";
            this.Computer.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Result.FillWeight = 40F;
            resources.ApplyResources(this.Result, "Result");
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            this.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSummary
            // 
            resources.ApplyResources(this.lblSummary, "lblSummary");
            this.lblSummary.Name = "lblSummary";
            // 
            // prgBarSendCommand
            // 
            resources.ApplyResources(this.prgBarSendCommand, "prgBarSendCommand");
            this.prgBarSendCommand.Name = "prgBarSendCommand";
            this.prgBarSendCommand.Step = 1;
            // 
            // chkBxCloseWhenFinished
            // 
            resources.ApplyResources(this.chkBxCloseWhenFinished, "chkBxCloseWhenFinished");
            this.chkBxCloseWhenFinished.Name = "chkBxCloseWhenFinished";
            this.chkBxCloseWhenFinished.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmRemoteExecution
            // 
            this.AcceptButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkBxCloseWhenFinished);
            this.Controls.Add(this.prgBarSendCommand);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtgvRemoteExecution);
            this.Name = "FrmRemoteExecution";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvRemoteExecution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvRemoteExecution;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.ProgressBar prgBarSendCommand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Computer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.CheckBox chkBxCloseWhenFinished;
        private System.Windows.Forms.Button btnCancel;
    }
}