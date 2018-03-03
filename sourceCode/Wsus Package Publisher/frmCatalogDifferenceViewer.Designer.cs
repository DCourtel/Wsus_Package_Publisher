namespace Wsus_Package_Publisher
{
    partial class frmCatalogDifferenceViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCatalogDifferenceViewer));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBxDeletedUpdates = new System.Windows.Forms.ComboBox();
            this.cmbBxAddedUpdates = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBxCatalog = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBxVendor = new System.Windows.Forms.TextBox();
            this.txtBxProduct = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBxTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnManage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbBxDeletedUpdates
            // 
            resources.ApplyResources(this.cmbBxDeletedUpdates, "cmbBxDeletedUpdates");
            this.cmbBxDeletedUpdates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxDeletedUpdates.FormattingEnabled = true;
            this.cmbBxDeletedUpdates.Name = "cmbBxDeletedUpdates";
            this.cmbBxDeletedUpdates.SelectedIndexChanged += new System.EventHandler(this.cmbBxDeletedUpdates_SelectedIndexChanged);
            // 
            // cmbBxAddedUpdates
            // 
            resources.ApplyResources(this.cmbBxAddedUpdates, "cmbBxAddedUpdates");
            this.cmbBxAddedUpdates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxAddedUpdates.FormattingEnabled = true;
            this.cmbBxAddedUpdates.Name = "cmbBxAddedUpdates";
            this.cmbBxAddedUpdates.SelectedIndexChanged += new System.EventHandler(this.cmbBxAddedUpdates_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxAddedUpdates, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxDeletedUpdates, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxCatalog, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtBxVendor, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtBxProduct, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtBxTitle, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtBxDescription, 0, 8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbBxCatalog
            // 
            resources.ApplyResources(this.cmbBxCatalog, "cmbBxCatalog");
            this.cmbBxCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxCatalog.FormattingEnabled = true;
            this.cmbBxCatalog.Name = "cmbBxCatalog";
            this.cmbBxCatalog.SelectedIndexChanged += new System.EventHandler(this.cmbBxCatalog_SelectedIndexChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtBxVendor
            // 
            resources.ApplyResources(this.txtBxVendor, "txtBxVendor");
            this.txtBxVendor.Name = "txtBxVendor";
            this.txtBxVendor.ReadOnly = true;
            // 
            // txtBxProduct
            // 
            resources.ApplyResources(this.txtBxProduct, "txtBxProduct");
            this.txtBxProduct.Name = "txtBxProduct";
            this.txtBxProduct.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtBxTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBxTitle, 2);
            resources.ApplyResources(this.txtBxTitle, "txtBxTitle");
            this.txtBxTitle.Name = "txtBxTitle";
            this.txtBxTitle.ReadOnly = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txtBxDescription
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtBxDescription, 2);
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnManage
            // 
            resources.ApplyResources(this.btnManage, "btnManage");
            this.btnManage.Name = "btnManage";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // frmCatalogDifferenceViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmCatalogDifferenceViewer";
            this.Shown += new System.EventHandler(this.frmCatalogDifferenceViewer_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBxDeletedUpdates;
        private System.Windows.Forms.ComboBox cmbBxAddedUpdates;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBxCatalog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBxVendor;
        private System.Windows.Forms.TextBox txtBxProduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBxTitle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBxDescription;
    }
}