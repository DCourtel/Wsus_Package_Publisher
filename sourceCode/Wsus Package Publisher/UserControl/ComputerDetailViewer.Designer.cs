namespace Wsus_Package_Publisher
{
    partial class ComputerDetailViewer
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerDetailViewer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblAfter = new System.Windows.Forms.Label();
            this.dtpInstalledAfter = new System.Windows.Forms.DateTimePicker();
            this.lblBefore = new System.Windows.Forms.Label();
            this.dtpInstalledBefore = new System.Windows.Forms.DateTimePicker();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.chkBxShowSupersededUpdates = new System.Windows.Forms.CheckBox();
            this.dtGvReport = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApprovalAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Installed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InstalledPendingReboot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Downloaded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotApplicable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotInstalled = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unknown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Failed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabLastInstalled = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtBxDetail = new System.Windows.Forms.TextBox();
            this.tabCtrlComputerDetail = new System.Windows.Forms.TabControl();
            this.tabReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGvReport)).BeginInit();
            this.tabLastInstalled.SuspendLayout();
            this.tabCtrlComputerDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAfter
            // 
            resources.ApplyResources(this.lblAfter, "lblAfter");
            this.lblAfter.Name = "lblAfter";
            // 
            // dtpInstalledAfter
            // 
            resources.ApplyResources(this.dtpInstalledAfter, "dtpInstalledAfter");
            this.dtpInstalledAfter.Name = "dtpInstalledAfter";
            this.dtpInstalledAfter.ValueChanged += new System.EventHandler(this.dtpInstalledAfter_ValueChanged);
            // 
            // lblBefore
            // 
            resources.ApplyResources(this.lblBefore, "lblBefore");
            this.lblBefore.Name = "lblBefore";
            // 
            // dtpInstalledBefore
            // 
            resources.ApplyResources(this.dtpInstalledBefore, "dtpInstalledBefore");
            this.dtpInstalledBefore.Name = "dtpInstalledBefore";
            this.dtpInstalledBefore.ValueChanged += new System.EventHandler(this.dtpInstalledAfter_ValueChanged);
            // 
            // tabReport
            // 
            resources.ApplyResources(this.tabReport, "tabReport");
            this.tabReport.Controls.Add(this.chkBxShowSupersededUpdates);
            this.tabReport.Controls.Add(this.dtGvReport);
            this.tabReport.Name = "tabReport";
            this.tabReport.UseVisualStyleBackColor = true;
            // 
            // chkBxShowSupersededUpdates
            // 
            resources.ApplyResources(this.chkBxShowSupersededUpdates, "chkBxShowSupersededUpdates");
            this.chkBxShowSupersededUpdates.Name = "chkBxShowSupersededUpdates";
            this.chkBxShowSupersededUpdates.UseVisualStyleBackColor = true;
            this.chkBxShowSupersededUpdates.CheckedChanged += new System.EventHandler(this.chkBxShowSupersededUpdates_CheckedChanged);
            // 
            // dtGvReport
            // 
            resources.ApplyResources(this.dtGvReport, "dtGvReport");
            this.dtGvReport.AllowUserToAddRows = false;
            this.dtGvReport.AllowUserToDeleteRows = false;
            this.dtGvReport.AllowUserToOrderColumns = true;
            this.dtGvReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGvReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGvReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.ApprovalAction,
            this.Installed,
            this.InstalledPendingReboot,
            this.Downloaded,
            this.NotApplicable,
            this.NotInstalled,
            this.Unknown,
            this.Failed});
            this.dtGvReport.EnableHeadersVisualStyles = false;
            this.dtGvReport.Name = "dtGvReport";
            this.dtGvReport.ReadOnly = true;
            this.dtGvReport.RowHeadersVisible = false;
            this.dtGvReport.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dtGvReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Title.FillWeight = 20F;
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // ApprovalAction
            // 
            this.ApprovalAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ApprovalAction.FillWeight = 10F;
            resources.ApplyResources(this.ApprovalAction, "ApprovalAction");
            this.ApprovalAction.Name = "ApprovalAction";
            this.ApprovalAction.ReadOnly = true;
            // 
            // Installed
            // 
            this.Installed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Installed.FillWeight = 10F;
            resources.ApplyResources(this.Installed, "Installed");
            this.Installed.Name = "Installed";
            this.Installed.ReadOnly = true;
            // 
            // InstalledPendingReboot
            // 
            this.InstalledPendingReboot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InstalledPendingReboot.FillWeight = 10F;
            resources.ApplyResources(this.InstalledPendingReboot, "InstalledPendingReboot");
            this.InstalledPendingReboot.Name = "InstalledPendingReboot";
            this.InstalledPendingReboot.ReadOnly = true;
            // 
            // Downloaded
            // 
            this.Downloaded.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Downloaded.FillWeight = 10F;
            resources.ApplyResources(this.Downloaded, "Downloaded");
            this.Downloaded.Name = "Downloaded";
            this.Downloaded.ReadOnly = true;
            // 
            // NotApplicable
            // 
            this.NotApplicable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NotApplicable.FillWeight = 10F;
            resources.ApplyResources(this.NotApplicable, "NotApplicable");
            this.NotApplicable.Name = "NotApplicable";
            this.NotApplicable.ReadOnly = true;
            // 
            // NotInstalled
            // 
            this.NotInstalled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NotInstalled.FillWeight = 10F;
            resources.ApplyResources(this.NotInstalled, "NotInstalled");
            this.NotInstalled.Name = "NotInstalled";
            this.NotInstalled.ReadOnly = true;
            // 
            // Unknown
            // 
            this.Unknown.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Unknown.FillWeight = 10F;
            resources.ApplyResources(this.Unknown, "Unknown");
            this.Unknown.Name = "Unknown";
            this.Unknown.ReadOnly = true;
            // 
            // Failed
            // 
            this.Failed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Failed.FillWeight = 10F;
            resources.ApplyResources(this.Failed, "Failed");
            this.Failed.Name = "Failed";
            this.Failed.ReadOnly = true;
            // 
            // tabLastInstalled
            // 
            resources.ApplyResources(this.tabLastInstalled, "tabLastInstalled");
            this.tabLastInstalled.Controls.Add(this.lblAfter);
            this.tabLastInstalled.Controls.Add(this.lblBefore);
            this.tabLastInstalled.Controls.Add(this.dtpInstalledBefore);
            this.tabLastInstalled.Controls.Add(this.btnRefresh);
            this.tabLastInstalled.Controls.Add(this.txtBxDetail);
            this.tabLastInstalled.Controls.Add(this.dtpInstalledAfter);
            this.tabLastInstalled.Name = "tabLastInstalled";
            this.tabLastInstalled.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtBxDetail
            // 
            resources.ApplyResources(this.txtBxDetail, "txtBxDetail");
            this.txtBxDetail.Name = "txtBxDetail";
            this.txtBxDetail.ReadOnly = true;
            // 
            // tabCtrlComputerDetail
            // 
            resources.ApplyResources(this.tabCtrlComputerDetail, "tabCtrlComputerDetail");
            this.tabCtrlComputerDetail.Controls.Add(this.tabLastInstalled);
            this.tabCtrlComputerDetail.Controls.Add(this.tabReport);
            this.tabCtrlComputerDetail.Name = "tabCtrlComputerDetail";
            this.tabCtrlComputerDetail.SelectedIndex = 0;
            this.tabCtrlComputerDetail.SelectedIndexChanged += new System.EventHandler(this.tabCtrlComputerDetail_SelectedIndexChanged);
            // 
            // ComputerDetailViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tabCtrlComputerDetail);
            this.Name = "ComputerDetailViewer";
            this.tabReport.ResumeLayout(false);
            this.tabReport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGvReport)).EndInit();
            this.tabLastInstalled.ResumeLayout(false);
            this.tabLastInstalled.PerformLayout();
            this.tabCtrlComputerDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAfter;
        private System.Windows.Forms.DateTimePicker dtpInstalledAfter;
        private System.Windows.Forms.Label lblBefore;
        private System.Windows.Forms.DateTimePicker dtpInstalledBefore;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.TabPage tabLastInstalled;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtBxDetail;
        private System.Windows.Forms.TabControl tabCtrlComputerDetail;
        private System.Windows.Forms.DataGridView dtGvReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApprovalAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Installed;
        private System.Windows.Forms.DataGridViewTextBoxColumn InstalledPendingReboot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Downloaded;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotApplicable;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotInstalled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unknown;
        private System.Windows.Forms.DataGridViewTextBoxColumn Failed;
        private System.Windows.Forms.CheckBox chkBxShowSupersededUpdates;
    }
}
