namespace Wsus_Package_Publisher
{
    partial class frmMetaGroupCreation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMetaGroupCreation));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxMetaGroupName = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCmbBxComputerGroups = new EasyCompany.Controls.CheckComboBox();
            this.chkCmbBxMetaGroups = new EasyCompany.Controls.CheckComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxMetaGroupName
            // 
            resources.ApplyResources(this.txtBxMetaGroupName, "txtBxMetaGroupName");
            this.txtBxMetaGroupName.Name = "txtBxMetaGroupName";
            this.txtBxMetaGroupName.TextChanged += new System.EventHandler(this.txtBxMetaGroupName_TextChanged);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // chkCmbBxComputerGroups
            // 
            resources.ApplyResources(this.chkCmbBxComputerGroups, "chkCmbBxComputerGroups");
            this.chkCmbBxComputerGroups.BackColor = System.Drawing.Color.AliceBlue;
            this.chkCmbBxComputerGroups.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkCmbBxComputerGroups.Name = "chkCmbBxComputerGroups";
            this.chkCmbBxComputerGroups.SelectionChanged += new EasyCompany.Controls.CheckComboBox.SelectionChangedEventHandler(this.chkCmbBxComputerGroups_SelectionChanged);
            // 
            // chkCmbBxMetaGroups
            // 
            resources.ApplyResources(this.chkCmbBxMetaGroups, "chkCmbBxMetaGroups");
            this.chkCmbBxMetaGroups.BackColor = System.Drawing.Color.AliceBlue;
            this.chkCmbBxMetaGroups.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkCmbBxMetaGroups.Name = "chkCmbBxMetaGroups";
            this.chkCmbBxMetaGroups.SelectionChanged += new EasyCompany.Controls.CheckComboBox.SelectionChangedEventHandler(this.chkCmbBxMetaGroup_SelectionChanged);
            // 
            // frmMetaGroupCreation
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ControlBox = false;
            this.Controls.Add(this.chkCmbBxMetaGroups);
            this.Controls.Add(this.chkCmbBxComputerGroups);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtBxMetaGroupName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmMetaGroupCreation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxMetaGroupName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private EasyCompany.Controls.CheckComboBox chkCmbBxComputerGroups;
        private EasyCompany.Controls.CheckComboBox chkCmbBxMetaGroups;
    }
}