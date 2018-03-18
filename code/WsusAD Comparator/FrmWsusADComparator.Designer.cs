namespace WsusADComparator
{
    partial class FrmWsusADComparator
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWsusADComparator));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxDomainName = new System.Windows.Forms.TextBox();
            this.btnSearchComputers = new System.Windows.Forms.Button();
            this.btnSelectOU = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnGetWsusClientID = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.nupTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new System.Data.DataSet();
            this.ctxMnuColumnHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblProgress = new System.Windows.Forms.Label();
            this.adgvComputer = new ADGV.AdvancedDataGridView();
            this.ctxMnuComputers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtBxWsusServer = new System.Windows.Forms.TextBox();
            this.chkBxUseSSL = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBxCredentials = new System.Windows.Forms.TextBox();
            this.btnEditCredentials = new System.Windows.Forms.Button();
            this.grpBxWsusServer = new System.Windows.Forms.GroupBox();
            this.nupServerPort = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEditWsus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adgvComputer)).BeginInit();
            this.grpBxWsusServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupServerPort)).BeginInit();
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
            // btnSelectOU
            // 
            resources.ApplyResources(this.btnSelectOU, "btnSelectOU");
            this.btnSelectOU.Name = "btnSelectOU";
            this.btnSelectOU.UseVisualStyleBackColor = true;
            this.btnSelectOU.Click += new System.EventHandler(this.btnSelectOU_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            resources.ApplyResources(this.btnExport, "btnExport");
            this.btnExport.Name = "btnExport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnGetWsusClientID
            // 
            resources.ApplyResources(this.btnGetWsusClientID, "btnGetWsusClientID");
            this.btnGetWsusClientID.Name = "btnGetWsusClientID";
            this.btnGetWsusClientID.UseVisualStyleBackColor = true;
            // 
            // btnAbort
            // 
            resources.ApplyResources(this.btnAbort, "btnAbort");
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // progBar
            // 
            resources.ApplyResources(this.progBar, "progBar");
            this.progBar.Name = "progBar";
            // 
            // nupTimeout
            // 
            resources.ApplyResources(this.nupTimeout, "nupTimeout");
            this.nupTimeout.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nupTimeout.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nupTimeout.Name = "nupTimeout";
            this.nupTimeout.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // bindingSource1
            // 
            this.bindingSource1.AllowNew = true;
            this.bindingSource1.DataSource = this.dataSet1;
            this.bindingSource1.Position = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // ctxMnuColumnHeader
            // 
            this.ctxMnuColumnHeader.Name = "ctxMnuColumnHeader";
            resources.ApplyResources(this.ctxMnuColumnHeader, "ctxMnuColumnHeader");
            this.ctxMnuColumnHeader.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuColumnHeader_ItemClicked);
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // adgvComputer
            // 
            this.adgvComputer.AllowUserToAddRows = false;
            this.adgvComputer.AllowUserToDeleteRows = false;
            this.adgvComputer.AllowUserToOrderColumns = true;
            this.adgvComputer.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.adgvComputer.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.adgvComputer, "adgvComputer");
            this.adgvComputer.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.adgvComputer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.adgvComputer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.adgvComputer.DataSource = this.bindingSource1;
            this.adgvComputer.DefaultCellBehavior = ADGV.ADGVColumnHeaderCellBehavior.SortingFiltering;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.adgvComputer.DefaultCellStyle = dataGridViewCellStyle3;
            this.adgvComputer.DefaultDateTimeGrouping = ADGV.ADGVFilterMenuDateTimeGrouping.Month;
            this.adgvComputer.EnableHeadersVisualStyles = false;
            this.adgvComputer.Name = "adgvComputer";
            this.adgvComputer.ReadOnly = true;
            this.adgvComputer.RowHeadersVisible = false;
            this.adgvComputer.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.adgvComputer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.adgvComputer.SortStringChanged += new System.EventHandler(this.adgvComputer_SortStringChanged);
            this.adgvComputer.FilterStringChanged += new System.EventHandler(this.adgvComputer_FilterStringChanged);
            this.adgvComputer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.adgvComputer_CellMouseClick);
            this.adgvComputer.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.adgvComputer_ColumnHeaderMouseClick);
            // 
            // ctxMnuComputers
            // 
            this.ctxMnuComputers.Name = "ctxMnuComputers";
            resources.ApplyResources(this.ctxMnuComputers, "ctxMnuComputers");
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxWsusServer
            // 
            this.txtBxWsusServer.BackColor = System.Drawing.Color.Bisque;
            resources.ApplyResources(this.txtBxWsusServer, "txtBxWsusServer");
            this.txtBxWsusServer.Name = "txtBxWsusServer";
            this.txtBxWsusServer.ReadOnly = true;
            // 
            // chkBxUseSSL
            // 
            resources.ApplyResources(this.chkBxUseSSL, "chkBxUseSSL");
            this.chkBxUseSSL.BackColor = System.Drawing.Color.Bisque;
            this.chkBxUseSSL.Name = "chkBxUseSSL";
            this.chkBxUseSSL.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtBxCredentials
            // 
            this.txtBxCredentials.BackColor = System.Drawing.Color.Bisque;
            resources.ApplyResources(this.txtBxCredentials, "txtBxCredentials");
            this.txtBxCredentials.Name = "txtBxCredentials";
            this.txtBxCredentials.ReadOnly = true;
            // 
            // btnEditCredentials
            // 
            resources.ApplyResources(this.btnEditCredentials, "btnEditCredentials");
            this.btnEditCredentials.Name = "btnEditCredentials";
            this.btnEditCredentials.UseVisualStyleBackColor = true;
            this.btnEditCredentials.Click += new System.EventHandler(this.btnEditCredentials_Click);
            // 
            // grpBxWsusServer
            // 
            this.grpBxWsusServer.Controls.Add(this.nupServerPort);
            this.grpBxWsusServer.Controls.Add(this.label5);
            this.grpBxWsusServer.Controls.Add(this.btnEditWsus);
            this.grpBxWsusServer.Controls.Add(this.label3);
            this.grpBxWsusServer.Controls.Add(this.txtBxWsusServer);
            this.grpBxWsusServer.Controls.Add(this.chkBxUseSSL);
            resources.ApplyResources(this.grpBxWsusServer, "grpBxWsusServer");
            this.grpBxWsusServer.Name = "grpBxWsusServer";
            this.grpBxWsusServer.TabStop = false;
            // 
            // nupServerPort
            // 
            this.nupServerPort.BackColor = System.Drawing.Color.Bisque;
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // btnEditWsus
            // 
            resources.ApplyResources(this.btnEditWsus, "btnEditWsus");
            this.btnEditWsus.Name = "btnEditWsus";
            this.btnEditWsus.UseVisualStyleBackColor = true;
            this.btnEditWsus.Click += new System.EventHandler(this.btnEditWsus_Click);
            // 
            // FrmWsusADComparator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBxWsusServer);
            this.Controls.Add(this.btnEditCredentials);
            this.Controls.Add(this.txtBxCredentials);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.adgvComputer);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nupTimeout);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnGetWsusClientID);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnSelectOU);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearchComputers);
            this.Controls.Add(this.txtBxDomainName);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "FrmWsusADComparator";
            this.Shown += new System.EventHandler(this.FrmWsusADComparator_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.nupTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adgvComputer)).EndInit();
            this.grpBxWsusServer.ResumeLayout(false);
            this.grpBxWsusServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxDomainName;
        private System.Windows.Forms.Button btnSearchComputers;
        private System.Windows.Forms.Button btnSelectOU;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnGetWsusClientID;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.NumericUpDown nupTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.ContextMenuStrip ctxMnuColumnHeader;
        private System.Windows.Forms.Label lblProgress;
        private ADGV.AdvancedDataGridView adgvComputer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuComputers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBxWsusServer;
        private System.Windows.Forms.CheckBox chkBxUseSSL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBxCredentials;
        private System.Windows.Forms.Button btnEditCredentials;
        private System.Windows.Forms.GroupBox grpBxWsusServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEditWsus;
        private System.Windows.Forms.NumericUpDown nupServerPort;
    }
}

