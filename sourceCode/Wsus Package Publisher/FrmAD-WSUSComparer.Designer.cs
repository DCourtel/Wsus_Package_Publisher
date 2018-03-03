namespace Wsus_Package_Publisher
{
    partial class FrmAD_WSUSComparer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAD_WSUSComparer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxDomainName = new System.Windows.Forms.TextBox();
            this.btnSearchComputers = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.prgBrSearch = new System.Windows.Forms.ProgressBar();
            this.dtGrdVResult = new System.Windows.Forms.DataGridView();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastLogon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SusClientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSServicePack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMnuComputer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.saveExport = new System.Windows.Forms.SaveFileDialog();
            this.btnGetSusClientID = new System.Windows.Forms.Button();
            this.btnResetWsusClientID = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nupPingTimeout = new System.Windows.Forms.NumericUpDown();
            this.chkBxDontPing = new System.Windows.Forms.CheckBox();
            this.ctxMnuHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblCredentials = new System.Windows.Forms.Label();
            this.chkBxShowOnlyMissingsComputers = new System.Windows.Forms.CheckBox();
            this.btnSelectOU = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupPingTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxDomainName
            // 
            resources.ApplyResources(this.txtBxDomainName, "txtBxDomainName");
            this.txtBxDomainName.Name = "txtBxDomainName";
            this.txtBxDomainName.ReadOnly = true;
            // 
            // btnSearchComputers
            // 
            resources.ApplyResources(this.btnSearchComputers, "btnSearchComputers");
            this.btnSearchComputers.Name = "btnSearchComputers";
            this.btnSearchComputers.UseVisualStyleBackColor = true;
            this.btnSearchComputers.Click += new System.EventHandler(this.btnSearchComputers_Click);
            // 
            // BtnClose
            // 
            resources.ApplyResources(this.BtnClose, "BtnClose");
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // prgBrSearch
            // 
            resources.ApplyResources(this.prgBrSearch, "prgBrSearch");
            this.prgBrSearch.Name = "prgBrSearch";
            this.prgBrSearch.Step = 1;
            // 
            // dtGrdVResult
            // 
            resources.ApplyResources(this.dtGrdVResult, "dtGrdVResult");
            this.dtGrdVResult.AllowUserToAddRows = false;
            this.dtGrdVResult.AllowUserToDeleteRows = false;
            this.dtGrdVResult.AllowUserToOrderColumns = true;
            this.dtGrdVResult.AllowUserToResizeRows = false;
            this.dtGrdVResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdVResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComputerName,
            this.OU,
            this.LastLogon,
            this.SusClientID,
            this.OSName,
            this.OSServicePack,
            this.OSVersion,
            this.ServiceStatus});
            this.dtGrdVResult.Name = "dtGrdVResult";
            this.dtGrdVResult.ReadOnly = true;
            this.dtGrdVResult.RowHeadersVisible = false;
            this.dtGrdVResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdVResult.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGrdVResult_CellMouseClick);
            this.dtGrdVResult.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtGrdVResult_ColumnHeaderMouseClick);
            this.dtGrdVResult.SelectionChanged += new System.EventHandler(this.dtGrdVResult_SelectionChanged);
            // 
            // ComputerName
            // 
            this.ComputerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ComputerName.DefaultCellStyle = dataGridViewCellStyle1;
            this.ComputerName.FillWeight = 7F;
            resources.ApplyResources(this.ComputerName, "ComputerName");
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            // 
            // OU
            // 
            this.OU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.OU.DefaultCellStyle = dataGridViewCellStyle2;
            this.OU.FillWeight = 30F;
            resources.ApplyResources(this.OU, "OU");
            this.OU.Name = "OU";
            this.OU.ReadOnly = true;
            // 
            // LastLogon
            // 
            this.LastLogon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.LastLogon.DefaultCellStyle = dataGridViewCellStyle3;
            this.LastLogon.FillWeight = 10F;
            resources.ApplyResources(this.LastLogon, "LastLogon");
            this.LastLogon.Name = "LastLogon";
            this.LastLogon.ReadOnly = true;
            // 
            // SusClientID
            // 
            this.SusClientID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.SusClientID.DefaultCellStyle = dataGridViewCellStyle4;
            this.SusClientID.FillWeight = 15F;
            resources.ApplyResources(this.SusClientID, "SusClientID");
            this.SusClientID.Name = "SusClientID";
            this.SusClientID.ReadOnly = true;
            // 
            // OSName
            // 
            this.OSName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.OSName.DefaultCellStyle = dataGridViewCellStyle5;
            this.OSName.FillWeight = 10F;
            resources.ApplyResources(this.OSName, "OSName");
            this.OSName.Name = "OSName";
            this.OSName.ReadOnly = true;
            // 
            // OSServicePack
            // 
            this.OSServicePack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.OSServicePack.DefaultCellStyle = dataGridViewCellStyle6;
            this.OSServicePack.FillWeight = 10F;
            resources.ApplyResources(this.OSServicePack, "OSServicePack");
            this.OSServicePack.Name = "OSServicePack";
            this.OSServicePack.ReadOnly = true;
            // 
            // OSVersion
            // 
            this.OSVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.OSVersion.DefaultCellStyle = dataGridViewCellStyle7;
            this.OSVersion.FillWeight = 7F;
            resources.ApplyResources(this.OSVersion, "OSVersion");
            this.OSVersion.Name = "OSVersion";
            this.OSVersion.ReadOnly = true;
            // 
            // ServiceStatus
            // 
            this.ServiceStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ServiceStatus.DefaultCellStyle = dataGridViewCellStyle8;
            this.ServiceStatus.FillWeight = 10F;
            resources.ApplyResources(this.ServiceStatus, "ServiceStatus");
            this.ServiceStatus.Name = "ServiceStatus";
            this.ServiceStatus.ReadOnly = true;
            // 
            // ctxMnuComputer
            // 
            resources.ApplyResources(this.ctxMnuComputer, "ctxMnuComputer");
            this.ctxMnuComputer.Name = "ctxMnuResult";
            this.ctxMnuComputer.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuComputer_ItemClicked);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // saveExport
            // 
            this.saveExport.DefaultExt = "csv";
            resources.ApplyResources(this.saveExport, "saveExport");
            // 
            // btnGetSusClientID
            // 
            resources.ApplyResources(this.btnGetSusClientID, "btnGetSusClientID");
            this.btnGetSusClientID.Name = "btnGetSusClientID";
            this.btnGetSusClientID.UseVisualStyleBackColor = true;
            this.btnGetSusClientID.Click += new System.EventHandler(this.btnGetSusClientID_Click);
            // 
            // btnResetWsusClientID
            // 
            resources.ApplyResources(this.btnResetWsusClientID, "btnResetWsusClientID");
            this.btnResetWsusClientID.Name = "btnResetWsusClientID";
            this.btnResetWsusClientID.UseVisualStyleBackColor = true;
            this.btnResetWsusClientID.Click += new System.EventHandler(this.btnResetWsusClientID_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // nupPingTimeout
            // 
            resources.ApplyResources(this.nupPingTimeout, "nupPingTimeout");
            this.nupPingTimeout.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nupPingTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupPingTimeout.Name = "nupPingTimeout";
            this.nupPingTimeout.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // chkBxDontPing
            // 
            resources.ApplyResources(this.chkBxDontPing, "chkBxDontPing");
            this.chkBxDontPing.Name = "chkBxDontPing";
            this.chkBxDontPing.UseVisualStyleBackColor = true;
            this.chkBxDontPing.CheckedChanged += new System.EventHandler(this.chkBxDontPing_CheckedChanged);
            // 
            // ctxMnuHeader
            // 
            resources.ApplyResources(this.ctxMnuHeader, "ctxMnuHeader");
            this.ctxMnuHeader.Name = "ctxMnuHeader";
            this.ctxMnuHeader.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuHeader_ItemClicked);
            // 
            // lblCredentials
            // 
            resources.ApplyResources(this.lblCredentials, "lblCredentials");
            this.lblCredentials.Name = "lblCredentials";
            // 
            // chkBxShowOnlyMissingsComputers
            // 
            resources.ApplyResources(this.chkBxShowOnlyMissingsComputers, "chkBxShowOnlyMissingsComputers");
            this.chkBxShowOnlyMissingsComputers.Name = "chkBxShowOnlyMissingsComputers";
            this.chkBxShowOnlyMissingsComputers.UseVisualStyleBackColor = true;
            this.chkBxShowOnlyMissingsComputers.CheckedChanged += new System.EventHandler(this.chkBxShowOnlyMissingsComputers_CheckedChanged);
            // 
            // btnSelectOU
            // 
            resources.ApplyResources(this.btnSelectOU, "btnSelectOU");
            this.btnSelectOU.Name = "btnSelectOU";
            this.btnSelectOU.UseVisualStyleBackColor = true;
            this.btnSelectOU.Click += new System.EventHandler(this.btnSelectOU_Click);
            // 
            // btnAbort
            // 
            resources.ApplyResources(this.btnAbort, "btnAbort");
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // FrmAD_WSUSComparer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCredentials);
            this.Controls.Add(this.chkBxDontPing);
            this.Controls.Add(this.nupPingTimeout);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnSelectOU);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkBxShowOnlyMissingsComputers);
            this.Controls.Add(this.btnResetWsusClientID);
            this.Controls.Add(this.btnGetSusClientID);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.dtGrdVResult);
            this.Controls.Add(this.prgBrSearch);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.txtBxDomainName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearchComputers);
            this.Name = "FrmAD_WSUSComparer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAD_WSUSComparer_FormClosing);
            this.Load += new System.EventHandler(this.FrmAD_WSUSComparer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupPingTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxDomainName;
        private System.Windows.Forms.Button btnSearchComputers;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ProgressBar prgBrSearch;
        private System.Windows.Forms.DataGridView dtGrdVResult;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.SaveFileDialog saveExport;
        private System.Windows.Forms.Button btnGetSusClientID;
        private System.Windows.Forms.Button btnResetWsusClientID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nupPingTimeout;
        private System.Windows.Forms.CheckBox chkBxDontPing;
        private System.Windows.Forms.ContextMenuStrip ctxMnuComputer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuHeader;
        private System.Windows.Forms.Label lblCredentials;
        private System.Windows.Forms.CheckBox chkBxShowOnlyMissingsComputers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OU;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastLogon;
        private System.Windows.Forms.DataGridViewTextBoxColumn SusClientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSServicePack;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceStatus;
        private System.Windows.Forms.Button btnSelectOU;
        private System.Windows.Forms.Button btnAbort;
    }
}