namespace Wsus_Package_Publisher
{
    partial class FrmMSIPropertyReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMSIPropertyReader));
            this.btnLoadMSIFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dtGrvProperties = new System.Windows.Forms.DataGridView();
            this.PropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbBxTables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrvProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadMSIFile
            // 
            resources.ApplyResources(this.btnLoadMSIFile, "btnLoadMSIFile");
            this.btnLoadMSIFile.Name = "btnLoadMSIFile";
            this.btnLoadMSIFile.UseVisualStyleBackColor = true;
            this.btnLoadMSIFile.Click += new System.EventHandler(this.btnLoadMSIFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // dtGrvProperties
            // 
            this.dtGrvProperties.AllowUserToAddRows = false;
            this.dtGrvProperties.AllowUserToDeleteRows = false;
            this.dtGrvProperties.AllowUserToOrderColumns = true;
            this.dtGrvProperties.AllowUserToResizeRows = false;
            resources.ApplyResources(this.dtGrvProperties, "dtGrvProperties");
            this.dtGrvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrvProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PropertyName,
            this.PropertyValue});
            this.dtGrvProperties.Name = "dtGrvProperties";
            this.dtGrvProperties.RowHeadersVisible = false;
            this.dtGrvProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // PropertyName
            // 
            this.PropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyName.FillWeight = 30F;
            resources.ApplyResources(this.PropertyName, "PropertyName");
            this.PropertyName.Name = "PropertyName";
            // 
            // PropertyValue
            // 
            this.PropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyValue.FillWeight = 70F;
            resources.ApplyResources(this.PropertyValue, "PropertyValue");
            this.PropertyValue.Name = "PropertyValue";
            // 
            // cmbBxTables
            // 
            resources.ApplyResources(this.cmbBxTables, "cmbBxTables");
            this.cmbBxTables.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cmbBxTables.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBxTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxTables.FormattingEnabled = true;
            this.cmbBxTables.Name = "cmbBxTables";
            this.cmbBxTables.SelectedIndexChanged += new System.EventHandler(this.cmbBxTables_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // FrmMSIPropertyReader
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbBxTables);
            this.Controls.Add(this.dtGrvProperties);
            this.Controls.Add(this.btnLoadMSIFile);
            this.Name = "FrmMSIPropertyReader";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrvProperties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadMSIFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dtGrvProperties;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyValue;
        private System.Windows.Forms.ComboBox cmbBxTables;
        private System.Windows.Forms.Label label1;
    }
}