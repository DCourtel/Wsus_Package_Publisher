namespace Wsus_Package_Publisher
{
    partial class FrmPrerequisiteSubscribers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrerequisiteSubscribers));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUpdates = new System.Windows.Forms.DataGridView();
            this.VendorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblUpdateTitle = new System.Windows.Forms.Label();
            this.grpBxOptions = new System.Windows.Forms.GroupBox();
            this.rdBtnUnsubscribe = new System.Windows.Forms.RadioButton();
            this.rdBtnDoNothing = new System.Windows.Forms.RadioButton();
            this.rdBtnUnsubscribeAndDelete = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdates)).BeginInit();
            this.grpBxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // dgvUpdates
            // 
            resources.ApplyResources(this.dgvUpdates, "dgvUpdates");
            this.dgvUpdates.AllowUserToAddRows = false;
            this.dgvUpdates.AllowUserToDeleteRows = false;
            this.dgvUpdates.AllowUserToResizeRows = false;
            this.dgvUpdates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUpdates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VendorName,
            this.ProductName,
            this.Title,
            this.Update});
            this.dgvUpdates.Name = "dgvUpdates";
            this.dgvUpdates.ReadOnly = true;
            this.dgvUpdates.RowHeadersVisible = false;
            this.dgvUpdates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // VendorName
            // 
            this.VendorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VendorName.FillWeight = 20F;
            resources.ApplyResources(this.VendorName, "VendorName");
            this.VendorName.Name = "VendorName";
            this.VendorName.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.FillWeight = 20F;
            resources.ApplyResources(this.ProductName, "ProductName");
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // Title
            // 
            this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Title.FillWeight = 60F;
            resources.ApplyResources(this.Title, "Title");
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // Update
            // 
            resources.ApplyResources(this.Update, "Update");
            this.Update.Name = "Update";
            this.Update.ReadOnly = true;
            // 
            // lblUpdateTitle
            // 
            resources.ApplyResources(this.lblUpdateTitle, "lblUpdateTitle");
            this.lblUpdateTitle.Name = "lblUpdateTitle";
            // 
            // grpBxOptions
            // 
            resources.ApplyResources(this.grpBxOptions, "grpBxOptions");
            this.grpBxOptions.Controls.Add(this.rdBtnUnsubscribe);
            this.grpBxOptions.Controls.Add(this.rdBtnDoNothing);
            this.grpBxOptions.Controls.Add(this.rdBtnUnsubscribeAndDelete);
            this.grpBxOptions.Name = "grpBxOptions";
            this.grpBxOptions.TabStop = false;
            // 
            // rdBtnUnsubscribe
            // 
            resources.ApplyResources(this.rdBtnUnsubscribe, "rdBtnUnsubscribe");
            this.rdBtnUnsubscribe.Name = "rdBtnUnsubscribe";
            this.rdBtnUnsubscribe.TabStop = true;
            this.rdBtnUnsubscribe.UseVisualStyleBackColor = true;
            this.rdBtnUnsubscribe.CheckedChanged += new System.EventHandler(this.OptionChange);
            // 
            // rdBtnDoNothing
            // 
            resources.ApplyResources(this.rdBtnDoNothing, "rdBtnDoNothing");
            this.rdBtnDoNothing.Checked = true;
            this.rdBtnDoNothing.Name = "rdBtnDoNothing";
            this.rdBtnDoNothing.TabStop = true;
            this.rdBtnDoNothing.UseVisualStyleBackColor = true;
            this.rdBtnDoNothing.CheckedChanged += new System.EventHandler(this.OptionChange);
            // 
            // rdBtnUnsubscribeAndDelete
            // 
            resources.ApplyResources(this.rdBtnUnsubscribeAndDelete, "rdBtnUnsubscribeAndDelete");
            this.rdBtnUnsubscribeAndDelete.Name = "rdBtnUnsubscribeAndDelete";
            this.rdBtnUnsubscribeAndDelete.UseVisualStyleBackColor = true;
            this.rdBtnUnsubscribeAndDelete.CheckedChanged += new System.EventHandler(this.OptionChange);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmPrerequisiteSubscribers
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpBxOptions);
            this.Controls.Add(this.lblUpdateTitle);
            this.Controls.Add(this.dgvUpdates);
            this.Controls.Add(this.label1);
            this.Name = "FrmPrerequisiteSubscribers";
            this.Shown += new System.EventHandler(this.FrmPrerequisiteSubscribers_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUpdates)).EndInit();
            this.grpBxOptions.ResumeLayout(false);
            this.grpBxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUpdates;
        private System.Windows.Forms.Label lblUpdateTitle;
        private System.Windows.Forms.GroupBox grpBxOptions;
        private System.Windows.Forms.RadioButton rdBtnUnsubscribe;
        private System.Windows.Forms.RadioButton rdBtnDoNothing;
        private System.Windows.Forms.RadioButton rdBtnUnsubscribeAndDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Update;
    }
}