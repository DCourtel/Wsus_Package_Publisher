namespace Wsus_Package_Publisher
{
    partial class FrmUpdateRulesWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateRulesWizard));
            this.cmbBxRules = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnAddAndGroup = new System.Windows.Forms.Button();
            this.btnAddOrGroup = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpDspUpdateLevel = new Wsus_Package_Publisher.GroupDisplayer();
            this.btnLoadRules = new System.Windows.Forms.Button();
            this.btnSaveRules = new System.Windows.Forms.Button();
            this.chkBxEmptyInstallableItemRule = new System.Windows.Forms.CheckBox();
            this.grpDspPackageLevel = new Wsus_Package_Publisher.GroupDisplayer();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdBtnUpdateLevel = new System.Windows.Forms.RadioButton();
            this.rdBtnPackageLevel = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbBxRules
            // 
            resources.ApplyResources(this.cmbBxRules, "cmbBxRules");
            this.tableLayoutPanel1.SetColumnSpan(this.cmbBxRules, 2);
            this.cmbBxRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxRules.Name = "cmbBxRules";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnAddRule
            // 
            resources.ApplyResources(this.btnAddRule, "btnAddRule");
            this.btnAddRule.Image = global::Wsus_Package_Publisher.Properties.Resources.Add_16x16;
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnAddAndGroup
            // 
            resources.ApplyResources(this.btnAddAndGroup, "btnAddAndGroup");
            this.btnAddAndGroup.Name = "btnAddAndGroup";
            this.btnAddAndGroup.UseVisualStyleBackColor = true;
            this.btnAddAndGroup.Click += new System.EventHandler(this.btnAddAndGroup_Click);
            // 
            // btnAddOrGroup
            // 
            resources.ApplyResources(this.btnAddOrGroup, "btnAddOrGroup");
            this.btnAddOrGroup.Name = "btnAddOrGroup";
            this.btnAddOrGroup.UseVisualStyleBackColor = true;
            this.btnAddOrGroup.Click += new System.EventHandler(this.btnAddOrGroup_Click);
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
            this.btnDelete.Image = global::Wsus_Package_Publisher.Properties.Resources.Delete_16x16;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNext
            // 
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Image = global::Wsus_Package_Publisher.Properties.Resources.Arrow_Right;
            this.btnNext.Name = "btnNext";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            resources.ApplyResources(this.btnPrevious, "btnPrevious");
            this.btnPrevious.Image = global::Wsus_Package_Publisher.Properties.Resources.Arrow_Left;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btnAddRule, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpDspUpdateLevel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmbBxRules, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddAndGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAddOrGroup, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDelete, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnEdit, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnNext, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnPrevious, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.btnLoadRules, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.btnSaveRules, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.chkBxEmptyInstallableItemRule, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.grpDspPackageLevel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.rdBtnUpdateLevel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.rdBtnPackageLevel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // grpDspUpdateLevel
            // 
            resources.ApplyResources(this.grpDspUpdateLevel, "grpDspUpdateLevel");
            this.tableLayoutPanel1.SetColumnSpan(this.grpDspUpdateLevel, 4);
            this.grpDspUpdateLevel.Name = "grpDspUpdateLevel";
            // 
            // btnLoadRules
            // 
            resources.ApplyResources(this.btnLoadRules, "btnLoadRules");
            this.btnLoadRules.Name = "btnLoadRules";
            this.btnLoadRules.UseVisualStyleBackColor = true;
            this.btnLoadRules.Click += new System.EventHandler(this.btnLoadRules_Click);
            // 
            // btnSaveRules
            // 
            resources.ApplyResources(this.btnSaveRules, "btnSaveRules");
            this.btnSaveRules.Image = global::Wsus_Package_Publisher.Properties.Resources.Disk7;
            this.btnSaveRules.Name = "btnSaveRules";
            this.btnSaveRules.UseVisualStyleBackColor = true;
            this.btnSaveRules.Click += new System.EventHandler(this.btnSaveRules_Click);
            // 
            // chkBxEmptyInstallableItemRule
            // 
            resources.ApplyResources(this.chkBxEmptyInstallableItemRule, "chkBxEmptyInstallableItemRule");
            this.tableLayoutPanel1.SetColumnSpan(this.chkBxEmptyInstallableItemRule, 2);
            this.chkBxEmptyInstallableItemRule.Name = "chkBxEmptyInstallableItemRule";
            this.chkBxEmptyInstallableItemRule.UseVisualStyleBackColor = true;
            this.chkBxEmptyInstallableItemRule.CheckedChanged += new System.EventHandler(this.chkBxEmptyInstallableItemRule_CheckedChanged);
            // 
            // grpDspPackageLevel
            // 
            resources.ApplyResources(this.grpDspPackageLevel, "grpDspPackageLevel");
            this.tableLayoutPanel1.SetColumnSpan(this.grpDspPackageLevel, 4);
            this.grpDspPackageLevel.Name = "grpDspPackageLevel";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.LightBlue;
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.LightBlue;
            this.label3.Name = "label3";
            // 
            // rdBtnUpdateLevel
            // 
            resources.ApplyResources(this.rdBtnUpdateLevel, "rdBtnUpdateLevel");
            this.rdBtnUpdateLevel.Checked = true;
            this.rdBtnUpdateLevel.Name = "rdBtnUpdateLevel";
            this.rdBtnUpdateLevel.TabStop = true;
            this.rdBtnUpdateLevel.UseVisualStyleBackColor = true;
            this.rdBtnUpdateLevel.CheckedChanged += new System.EventHandler(this.rdBtnUpdateLevel_CheckedChanged);
            // 
            // rdBtnPackageLevel
            // 
            resources.ApplyResources(this.rdBtnPackageLevel, "rdBtnPackageLevel");
            this.rdBtnPackageLevel.Name = "rdBtnPackageLevel";
            this.rdBtnPackageLevel.UseVisualStyleBackColor = true;
            this.rdBtnPackageLevel.CheckedChanged += new System.EventHandler(this.rdBtnUpdateLevel_CheckedChanged);
            // 
            // FrmUpdateRulesWizard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUpdateRulesWizard";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbBxRules;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Button btnAddAndGroup;
        private System.Windows.Forms.Button btnAddOrGroup;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPrevious;
        private GroupDisplayer grpDspUpdateLevel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkBxEmptyInstallableItemRule;
        private System.Windows.Forms.Button btnSaveRules;
        private System.Windows.Forms.Button btnLoadRules;
        private GroupDisplayer grpDspPackageLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdBtnUpdateLevel;
        private System.Windows.Forms.RadioButton rdBtnPackageLevel;
    }
}