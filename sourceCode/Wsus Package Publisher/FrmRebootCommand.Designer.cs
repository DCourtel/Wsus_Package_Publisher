namespace Wsus_Package_Publisher
{
    partial class FrmRebootCommand
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRebootCommand));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpBxCommand = new System.Windows.Forms.GroupBox();
            this.rdBtnShutdown = new System.Windows.Forms.RadioButton();
            this.rdBtnReboot = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.nupTimer = new System.Windows.Forms.NumericUpDown();
            this.chkBxForceClosing = new System.Windows.Forms.CheckBox();
            this.chkBxIncludeMessage = new System.Windows.Forms.CheckBox();
            this.txtBxMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvComputers = new System.Windows.Forms.DataGridView();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.prgBrCommand = new System.Windows.Forms.ProgressBar();
            this.lblResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpBxCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBxCommand
            // 
            resources.ApplyResources(this.grpBxCommand, "grpBxCommand");
            this.grpBxCommand.Controls.Add(this.rdBtnShutdown);
            this.grpBxCommand.Controls.Add(this.rdBtnReboot);
            this.grpBxCommand.Name = "grpBxCommand";
            this.grpBxCommand.TabStop = false;
            // 
            // rdBtnShutdown
            // 
            resources.ApplyResources(this.rdBtnShutdown, "rdBtnShutdown");
            this.rdBtnShutdown.Name = "rdBtnShutdown";
            this.rdBtnShutdown.UseVisualStyleBackColor = true;
            // 
            // rdBtnReboot
            // 
            resources.ApplyResources(this.rdBtnReboot, "rdBtnReboot");
            this.rdBtnReboot.Checked = true;
            this.rdBtnReboot.Name = "rdBtnReboot";
            this.rdBtnReboot.TabStop = true;
            this.rdBtnReboot.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // nupTimer
            // 
            resources.ApplyResources(this.nupTimer, "nupTimer");
            this.nupTimer.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nupTimer.Name = "nupTimer";
            this.nupTimer.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // chkBxForceClosing
            // 
            resources.ApplyResources(this.chkBxForceClosing, "chkBxForceClosing");
            this.chkBxForceClosing.Checked = true;
            this.chkBxForceClosing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxForceClosing.Name = "chkBxForceClosing";
            this.chkBxForceClosing.UseVisualStyleBackColor = true;
            // 
            // chkBxIncludeMessage
            // 
            resources.ApplyResources(this.chkBxIncludeMessage, "chkBxIncludeMessage");
            this.chkBxIncludeMessage.Name = "chkBxIncludeMessage";
            this.chkBxIncludeMessage.UseVisualStyleBackColor = true;
            this.chkBxIncludeMessage.CheckedChanged += new System.EventHandler(this.chkBxIncludeMessage_CheckedChanged);
            // 
            // txtBxMessage
            // 
            resources.ApplyResources(this.txtBxMessage, "txtBxMessage");
            this.txtBxMessage.Name = "txtBxMessage";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // dgvComputers
            // 
            resources.ApplyResources(this.dgvComputers, "dgvComputers");
            this.dgvComputers.AllowUserToAddRows = false;
            this.dgvComputers.AllowUserToDeleteRows = false;
            this.dgvComputers.AllowUserToOrderColumns = true;
            this.dgvComputers.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvComputers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComputers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComputerName,
            this.Result,
            this.ADComputer});
            this.dgvComputers.EnableHeadersVisualStyles = false;
            this.dgvComputers.MultiSelect = false;
            this.dgvComputers.Name = "dgvComputers";
            this.dgvComputers.ReadOnly = true;
            this.dgvComputers.RowHeadersVisible = false;
            this.dgvComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ComputerName
            // 
            this.ComputerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ComputerName.DefaultCellStyle = dataGridViewCellStyle5;
            this.ComputerName.FillWeight = 75F;
            resources.ApplyResources(this.ComputerName, "ComputerName");
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Result.DefaultCellStyle = dataGridViewCellStyle6;
            this.Result.FillWeight = 25F;
            resources.ApplyResources(this.Result, "Result");
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // ADComputer
            // 
            resources.ApplyResources(this.ADComputer, "ADComputer");
            this.ADComputer.Name = "ADComputer";
            this.ADComputer.ReadOnly = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnAbort
            // 
            resources.ApplyResources(this.btnAbort, "btnAbort");
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // prgBrCommand
            // 
            resources.ApplyResources(this.prgBrCommand, "prgBrCommand");
            this.prgBrCommand.Name = "prgBrCommand";
            // 
            // lblResult
            // 
            resources.ApplyResources(this.lblResult, "lblResult");
            this.lblResult.Name = "lblResult";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FrmRebootCommand
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.prgBrCommand);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvComputers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxMessage);
            this.Controls.Add(this.chkBxIncludeMessage);
            this.Controls.Add(this.chkBxForceClosing);
            this.Controls.Add(this.nupTimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpBxCommand);
            this.Name = "FrmRebootCommand";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRebootCommand_FormClosing);
            this.Shown += new System.EventHandler(this.FrmRebootCommand_Shown);
            this.grpBxCommand.ResumeLayout(false);
            this.grpBxCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBxCommand;
        private System.Windows.Forms.RadioButton rdBtnShutdown;
        private System.Windows.Forms.RadioButton rdBtnReboot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nupTimer;
        private System.Windows.Forms.CheckBox chkBxForceClosing;
        private System.Windows.Forms.CheckBox chkBxIncludeMessage;
        private System.Windows.Forms.TextBox txtBxMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvComputers;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.ProgressBar prgBrCommand;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADComputer;
        private System.Windows.Forms.Label label3;
    }
}