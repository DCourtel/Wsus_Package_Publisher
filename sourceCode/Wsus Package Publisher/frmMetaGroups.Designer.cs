namespace Wsus_Package_Publisher
{
    partial class frmMetaGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMetaGroups));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtGrdVwMetaGroups = new System.Windows.Forms.DataGridView();
            this.MetaGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtGrdVwComputerGroups = new System.Windows.Forms.DataGridView();
            this.ComputerGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBxMetaGroups = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwMetaGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwComputerGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.splitContainer1.Panel1.Controls.Add(this.dtGrdVwMetaGroups);
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.Panel2.Controls.Add(this.dtGrdVwComputerGroups);
            // 
            // dtGrdVwMetaGroups
            // 
            resources.ApplyResources(this.dtGrdVwMetaGroups, "dtGrdVwMetaGroups");
            this.dtGrdVwMetaGroups.AllowUserToAddRows = false;
            this.dtGrdVwMetaGroups.AllowUserToDeleteRows = false;
            this.dtGrdVwMetaGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdVwMetaGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MetaGroupName});
            this.dtGrdVwMetaGroups.Name = "dtGrdVwMetaGroups";
            this.dtGrdVwMetaGroups.ReadOnly = true;
            this.dtGrdVwMetaGroups.RowHeadersVisible = false;
            this.dtGrdVwMetaGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdVwMetaGroups.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtGrdVwMetaGroups_CellDoubleClick);
            // 
            // MetaGroupName
            // 
            this.MetaGroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.MetaGroupName.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.MetaGroupName, "MetaGroupName");
            this.MetaGroupName.Name = "MetaGroupName";
            this.MetaGroupName.ReadOnly = true;
            // 
            // dtGrdVwComputerGroups
            // 
            resources.ApplyResources(this.dtGrdVwComputerGroups, "dtGrdVwComputerGroups");
            this.dtGrdVwComputerGroups.AllowUserToAddRows = false;
            this.dtGrdVwComputerGroups.AllowUserToDeleteRows = false;
            this.dtGrdVwComputerGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdVwComputerGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ComputerGroupName});
            this.dtGrdVwComputerGroups.Name = "dtGrdVwComputerGroups";
            this.dtGrdVwComputerGroups.ReadOnly = true;
            this.dtGrdVwComputerGroups.RowHeadersVisible = false;
            this.dtGrdVwComputerGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // ComputerGroupName
            // 
            this.ComputerGroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.ComputerGroupName.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.ComputerGroupName, "ComputerGroupName");
            this.ComputerGroupName.Name = "ComputerGroupName";
            this.ComputerGroupName.ReadOnly = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCreate
            // 
            resources.ApplyResources(this.btnCreate, "btnCreate");
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cmbBxMetaGroups
            // 
            resources.ApplyResources(this.cmbBxMetaGroups, "cmbBxMetaGroups");
            this.cmbBxMetaGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxMetaGroups.FormattingEnabled = true;
            this.cmbBxMetaGroups.Name = "cmbBxMetaGroups";
            this.cmbBxMetaGroups.SelectedIndexChanged += new System.EventHandler(this.cmbBxMetaGroups_SelectedIndexChanged);
            // 
            // frmMetaGroups
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cmbBxMetaGroups);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Name = "frmMetaGroups";
            this.Shown += new System.EventHandler(this.frmMetaGroups_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwMetaGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVwComputerGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dtGrdVwMetaGroups;
        private System.Windows.Forms.DataGridView dtGrdVwComputerGroups;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBxMetaGroups;
        private System.Windows.Forms.DataGridViewTextBoxColumn MetaGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComputerGroupName;
    }
}