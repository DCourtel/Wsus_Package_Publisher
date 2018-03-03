namespace Wsus_Package_Publisher
{
    partial class ComputerListViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerListViewer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGVComputer = new System.Windows.Forms.DataGridView();
            this.ComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IPAdress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BiosName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BiosVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastReportedStatusTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastSyncTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastSyncResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Make = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSArchitecture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OSDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OnLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMnuComputer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxMnuHeader = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblCredentialNotice = new System.Windows.Forms.Label();
            this.chkBxShowOnlineComputersOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBxFilterComparison = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.txtBxFilterCriteria = new System.Windows.Forms.TextBox();
            this.btnClearFilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGVComputer)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVComputer
            // 
            this.dGVComputer.AllowUserToAddRows = false;
            this.dGVComputer.AllowUserToDeleteRows = false;
            this.dGVComputer.AllowUserToOrderColumns = true;
            this.dGVComputer.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dGVComputer, "dGVComputer");
            this.dGVComputer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dGVComputer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dGVComputer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVComputer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComputerName,
            this.IPAdress,
            this.BiosName,
            this.BiosVersion,
            this.LastReportedStatusTime,
            this.LastSyncTime,
            this.LastSyncResult,
            this.Make,
            this.Model,
            this.OSArchitecture,
            this.OSDescription,
            this.OnLine});
            this.dGVComputer.EnableHeadersVisualStyles = false;
            this.dGVComputer.Name = "dGVComputer";
            this.dGVComputer.ReadOnly = true;
            this.dGVComputer.RowHeadersVisible = false;
            this.dGVComputer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dGVComputer.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGVComputer_CellMouseClick);
            this.dGVComputer.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGVComputer_ColumnHeaderMouseClick);
            this.dGVComputer.SelectionChanged += new System.EventHandler(this.dGVComputer_SelectionChanged);
            this.dGVComputer.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dGVComputer_SortCompare);
            // 
            // ComputerName
            // 
            this.ComputerName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.ComputerName.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ComputerName, "ComputerName");
            this.ComputerName.Name = "ComputerName";
            this.ComputerName.ReadOnly = true;
            this.ComputerName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // IPAdress
            // 
            this.IPAdress.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.IPAdress.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.IPAdress, "IPAdress");
            this.IPAdress.Name = "IPAdress";
            this.IPAdress.ReadOnly = true;
            // 
            // BiosName
            // 
            this.BiosName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.BiosName.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.BiosName, "BiosName");
            this.BiosName.Name = "BiosName";
            this.BiosName.ReadOnly = true;
            // 
            // BiosVersion
            // 
            this.BiosVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.BiosVersion.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.BiosVersion, "BiosVersion");
            this.BiosVersion.Name = "BiosVersion";
            this.BiosVersion.ReadOnly = true;
            // 
            // LastReportedStatusTime
            // 
            this.LastReportedStatusTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.LastReportedStatusTime.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.LastReportedStatusTime, "LastReportedStatusTime");
            this.LastReportedStatusTime.Name = "LastReportedStatusTime";
            this.LastReportedStatusTime.ReadOnly = true;
            // 
            // LastSyncTime
            // 
            this.LastSyncTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            this.LastSyncTime.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.LastSyncTime, "LastSyncTime");
            this.LastSyncTime.Name = "LastSyncTime";
            this.LastSyncTime.ReadOnly = true;
            // 
            // LastSyncResult
            // 
            this.LastSyncResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.LastSyncResult.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.LastSyncResult, "LastSyncResult");
            this.LastSyncResult.Name = "LastSyncResult";
            this.LastSyncResult.ReadOnly = true;
            // 
            // Make
            // 
            this.Make.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.Make.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.Make, "Make");
            this.Make.Name = "Make";
            this.Make.ReadOnly = true;
            // 
            // Model
            // 
            this.Model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.Model.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.Model, "Model");
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // OSArchitecture
            // 
            this.OSArchitecture.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            this.OSArchitecture.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.OSArchitecture, "OSArchitecture");
            this.OSArchitecture.Name = "OSArchitecture";
            this.OSArchitecture.ReadOnly = true;
            // 
            // OSDescription
            // 
            this.OSDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.OSDescription.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.OSDescription, "OSDescription");
            this.OSDescription.Name = "OSDescription";
            this.OSDescription.ReadOnly = true;
            // 
            // OnLine
            // 
            resources.ApplyResources(this.OnLine, "OnLine");
            this.OnLine.Name = "OnLine";
            this.OnLine.ReadOnly = true;
            // 
            // ctxMnuComputer
            // 
            this.ctxMnuComputer.Name = "ctxMnuComputer";
            resources.ApplyResources(this.ctxMnuComputer, "ctxMnuComputer");
            this.ctxMnuComputer.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuComputer_ItemClicked);
            // 
            // ctxMnuHeader
            // 
            this.ctxMnuHeader.Name = "ctxMnuHeader";
            resources.ApplyResources(this.ctxMnuHeader, "ctxMnuHeader");
            this.ctxMnuHeader.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuHeader_ItemClicked);
            // 
            // lblCredentialNotice
            // 
            resources.ApplyResources(this.lblCredentialNotice, "lblCredentialNotice");
            this.lblCredentialNotice.Name = "lblCredentialNotice";
            // 
            // chkBxShowOnlineComputersOnly
            // 
            resources.ApplyResources(this.chkBxShowOnlineComputersOnly, "chkBxShowOnlineComputersOnly");
            this.chkBxShowOnlineComputersOnly.Name = "chkBxShowOnlineComputersOnly";
            this.chkBxShowOnlineComputersOnly.UseVisualStyleBackColor = true;
            this.chkBxShowOnlineComputersOnly.CheckedChanged += new System.EventHandler(this.chkBxShowOnlineComputersOnly_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbBxFilterComparison
            // 
            this.cmbBxFilterComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxFilterComparison.FormattingEnabled = true;
            resources.ApplyResources(this.cmbBxFilterComparison, "cmbBxFilterComparison");
            this.cmbBxFilterComparison.Name = "cmbBxFilterComparison";
            // 
            // btnFilter
            // 
            resources.ApplyResources(this.btnFilter, "btnFilter");
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // txtBxFilterCriteria
            // 
            this.txtBxFilterCriteria.AcceptsReturn = true;
            this.txtBxFilterCriteria.AcceptsTab = true;
            this.txtBxFilterCriteria.CausesValidation = false;
            resources.ApplyResources(this.txtBxFilterCriteria, "txtBxFilterCriteria");
            this.txtBxFilterCriteria.Name = "txtBxFilterCriteria";
            this.txtBxFilterCriteria.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBxFilterCriteria_KeyUp);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Image = global::Wsus_Package_Publisher.Properties.Resources.Delete;
            resources.ApplyResources(this.btnClearFilter, "btnClearFilter");
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // ComputerListViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCredentialNotice);
            this.Controls.Add(this.btnClearFilter);
            this.Controls.Add(this.dGVComputer);
            this.Controls.Add(this.txtBxFilterCriteria);
            this.Controls.Add(this.chkBxShowOnlineComputersOnly);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.cmbBxFilterComparison);
            this.Name = "ComputerListViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dGVComputer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVComputer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuComputer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuHeader;
        private System.Windows.Forms.Label lblCredentialNotice;
        private System.Windows.Forms.CheckBox chkBxShowOnlineComputersOnly;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPAdress;
        private System.Windows.Forms.DataGridViewTextBoxColumn BiosName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BiosVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastReportedStatusTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastSyncTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastSyncResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Make;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSArchitecture;
        private System.Windows.Forms.DataGridViewTextBoxColumn OSDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn OnLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBxFilterComparison;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TextBox txtBxFilterCriteria;
        private System.Windows.Forms.Button btnClearFilter;
    }
}
