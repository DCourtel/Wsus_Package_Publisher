namespace Wsus_Package_Publisher
{
    partial class FrmEventDisplayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEventDisplayer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvEventHistory = new System.Windows.Forms.DataGridView();
            this.CreationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDeleteHistory = new System.Windows.Forms.Button();
            this.lblNumberOfEvents = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEventHistory
            // 
            resources.ApplyResources(this.dgvEventHistory, "dgvEventHistory");
            this.dgvEventHistory.AllowUserToAddRows = false;
            this.dgvEventHistory.AllowUserToDeleteRows = false;
            this.dgvEventHistory.AllowUserToOrderColumns = true;
            this.dgvEventHistory.AllowUserToResizeRows = false;
            this.dgvEventHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CreationDate,
            this.ErrorCode,
            this.Message,
            this.Status});
            this.dgvEventHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvEventHistory.MultiSelect = false;
            this.dgvEventHistory.Name = "dgvEventHistory";
            this.dgvEventHistory.ReadOnly = true;
            this.dgvEventHistory.RowHeadersVisible = false;
            this.dgvEventHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEventHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEventHistory.SelectionChanged += new System.EventHandler(this.dgvEventHistory_SelectionChanged);
            // 
            // CreationDate
            // 
            this.CreationDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.CreationDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.CreationDate.FillWeight = 15F;
            resources.ApplyResources(this.CreationDate, "CreationDate");
            this.CreationDate.Name = "CreationDate";
            this.CreationDate.ReadOnly = true;
            // 
            // ErrorCode
            // 
            this.ErrorCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ErrorCode.DefaultCellStyle = dataGridViewCellStyle2;
            this.ErrorCode.FillWeight = 10F;
            resources.ApplyResources(this.ErrorCode, "ErrorCode");
            this.ErrorCode.Name = "ErrorCode";
            this.ErrorCode.ReadOnly = true;
            // 
            // Message
            // 
            this.Message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Message.DefaultCellStyle = dataGridViewCellStyle3;
            this.Message.FillWeight = 55F;
            resources.ApplyResources(this.Message, "Message");
            this.Message.Name = "Message";
            this.Message.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Status.DefaultCellStyle = dataGridViewCellStyle4;
            this.Status.FillWeight = 20F;
            resources.ApplyResources(this.Status, "Status");
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDeleteHistory
            // 
            resources.ApplyResources(this.btnDeleteHistory, "btnDeleteHistory");
            this.btnDeleteHistory.Name = "btnDeleteHistory";
            this.btnDeleteHistory.UseVisualStyleBackColor = true;
            this.btnDeleteHistory.Click += new System.EventHandler(this.btnDeleteHistory_Click);
            // 
            // lblNumberOfEvents
            // 
            resources.ApplyResources(this.lblNumberOfEvents, "lblNumberOfEvents");
            this.lblNumberOfEvents.Name = "lblNumberOfEvents";
            // 
            // FrmEventDisplayer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNumberOfEvents);
            this.Controls.Add(this.btnDeleteHistory);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvEventHistory);
            this.Name = "FrmEventDisplayer";
            this.Shown += new System.EventHandler(this.FrmEventDisplayer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEventHistory;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDeleteHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label lblNumberOfEvents;
    }
}