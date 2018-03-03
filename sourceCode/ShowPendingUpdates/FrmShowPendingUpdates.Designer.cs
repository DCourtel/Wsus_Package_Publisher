namespace ShowPendingUpdates
{
    partial class FrmShowPendingUpdates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowPendingUpdates));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPendingUpdates = new System.Windows.Forms.DataGridView();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblPendingUpdatesOn = new System.Windows.Forms.Label();
            this.lblInformations = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingUpdates)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPendingUpdates
            // 
            resources.ApplyResources(this.dgvPendingUpdates, "dgvPendingUpdates");
            this.dgvPendingUpdates.AllowUserToAddRows = false;
            this.dgvPendingUpdates.AllowUserToDeleteRows = false;
            this.dgvPendingUpdates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvPendingUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Description});
            this.dgvPendingUpdates.Name = "dgvPendingUpdates";
            this.dgvPendingUpdates.ReadOnly = true;
            this.dgvPendingUpdates.RowHeadersVisible = false;
            this.dgvPendingUpdates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendingUpdates.ShowEditingIcon = false;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.Title.DefaultCellStyle = dataGridViewCellStyle1;
            this.Title.FillWeight = 20F;
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.DefaultCellStyle = dataGridViewCellStyle2;
            this.Description.FillWeight = 80F;
            resources.ApplyResources(this.Description, "Description");
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblPendingUpdatesOn
            // 
            resources.ApplyResources(this.lblPendingUpdatesOn, "lblPendingUpdatesOn");
            this.lblPendingUpdatesOn.Name = "lblPendingUpdatesOn";
            // 
            // lblInformations
            // 
            resources.ApplyResources(this.lblInformations, "lblInformations");
            this.lblInformations.Name = "lblInformations";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // FrmShowPendingUpdates
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblInformations);
            this.Controls.Add(this.lblPendingUpdatesOn);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dgvPendingUpdates);
            this.Name = "FrmShowPendingUpdates";
            this.Shown += new System.EventHandler(this.FrmShowPendingUpdates_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingUpdates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPendingUpdates;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPendingUpdatesOn;
        private System.Windows.Forms.Label lblInformations;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}

