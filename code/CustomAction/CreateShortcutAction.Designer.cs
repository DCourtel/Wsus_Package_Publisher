namespace CustomActions
{
    partial class CreateShortcutAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateShortcutAction));
            this.txtBxTarget = new System.Windows.Forms.TextBox();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.txtBxIcon = new System.Windows.Forms.TextBox();
            this.txtBxArguments = new System.Windows.Forms.TextBox();
            this.txtBxWorkingDirectory = new System.Windows.Forms.TextBox();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxWindowStyle = new System.Windows.Forms.ComboBox();
            this.chkBxAbortIfDontExists = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
            this.label10 = new EasyCompany.Controls.ExtendedLabel();
            this.label9 = new EasyCompany.Controls.ExtendedLabel();
            this.rdBtnPersonnalized = new System.Windows.Forms.RadioButton();
            this.rdBtnDesktop = new System.Windows.Forms.RadioButton();
            this.cmbBxDesktop = new System.Windows.Forms.ComboBox();
            this.txtBxDirectory = new System.Windows.Forms.TextBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            this.label8 = new EasyCompany.Controls.ExtendedLabel();
            this.label11 = new EasyCompany.Controls.ExtendedLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBxTarget
            // 
            resources.ApplyResources(this.txtBxTarget, "txtBxTarget");
            this.txtBxTarget.BackColor = System.Drawing.Color.Orange;
            this.txtBxTarget.Name = "txtBxTarget";
            this.txtBxTarget.TextChanged += new System.EventHandler(this.txtBxTarget_TextChanged);
            // 
            // txtBxName
            // 
            resources.ApplyResources(this.txtBxName, "txtBxName");
            this.txtBxName.BackColor = System.Drawing.Color.Orange;
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            // 
            // txtBxDescription
            // 
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.TextChanged += new System.EventHandler(this.optionnalTxtBxData_TextChanged);
            // 
            // txtBxIcon
            // 
            resources.ApplyResources(this.txtBxIcon, "txtBxIcon");
            this.txtBxIcon.Name = "txtBxIcon";
            this.txtBxIcon.TextChanged += new System.EventHandler(this.optionnalTxtBxData_TextChanged);
            // 
            // txtBxArguments
            // 
            resources.ApplyResources(this.txtBxArguments, "txtBxArguments");
            this.txtBxArguments.Name = "txtBxArguments";
            this.txtBxArguments.TextChanged += new System.EventHandler(this.optionnalTxtBxData_TextChanged);
            // 
            // txtBxWorkingDirectory
            // 
            resources.ApplyResources(this.txtBxWorkingDirectory, "txtBxWorkingDirectory");
            this.txtBxWorkingDirectory.Name = "txtBxWorkingDirectory";
            this.txtBxWorkingDirectory.TextChanged += new System.EventHandler(this.optionnalTxtBxData_TextChanged);
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cmbBxWindowStyle
            // 
            this.cmbBxWindowStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxWindowStyle.FormattingEnabled = true;
            this.cmbBxWindowStyle.Items.AddRange(new object[] {
            resources.GetString("cmbBxWindowStyle.Items"),
            resources.GetString("cmbBxWindowStyle.Items1"),
            resources.GetString("cmbBxWindowStyle.Items2")});
            resources.ApplyResources(this.cmbBxWindowStyle, "cmbBxWindowStyle");
            this.cmbBxWindowStyle.Name = "cmbBxWindowStyle";
            this.cmbBxWindowStyle.SelectedIndexChanged += new System.EventHandler(this.cmbBxWindowStyle_SelectedIndexChanged);
            // 
            // chkBxAbortIfDontExists
            // 
            resources.ApplyResources(this.chkBxAbortIfDontExists, "chkBxAbortIfDontExists");
            this.chkBxAbortIfDontExists.Checked = true;
            this.chkBxAbortIfDontExists.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxAbortIfDontExists.Name = "chkBxAbortIfDontExists";
            this.chkBxAbortIfDontExists.UseVisualStyleBackColor = true;
            this.chkBxAbortIfDontExists.CheckedChanged += new System.EventHandler(this.chkBxAbortIfDontExists_CheckedChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.lnkEnvironmentVariables);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rdBtnPersonnalized);
            this.groupBox1.Controls.Add(this.rdBtnDesktop);
            this.groupBox1.Controls.Add(this.cmbBxDesktop);
            this.groupBox1.Controls.Add(this.txtBxDirectory);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // rdBtnPersonnalized
            // 
            resources.ApplyResources(this.rdBtnPersonnalized, "rdBtnPersonnalized");
            this.rdBtnPersonnalized.Name = "rdBtnPersonnalized";
            this.rdBtnPersonnalized.UseVisualStyleBackColor = true;
            this.rdBtnPersonnalized.CheckedChanged += new System.EventHandler(this.rdBtnDesktop_CheckedChanged);
            // 
            // rdBtnDesktop
            // 
            resources.ApplyResources(this.rdBtnDesktop, "rdBtnDesktop");
            this.rdBtnDesktop.Checked = true;
            this.rdBtnDesktop.Name = "rdBtnDesktop";
            this.rdBtnDesktop.TabStop = true;
            this.rdBtnDesktop.UseVisualStyleBackColor = true;
            this.rdBtnDesktop.CheckedChanged += new System.EventHandler(this.rdBtnDesktop_CheckedChanged);
            // 
            // cmbBxDesktop
            // 
            this.cmbBxDesktop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbBxDesktop, "cmbBxDesktop");
            this.cmbBxDesktop.FormattingEnabled = true;
            this.cmbBxDesktop.Items.AddRange(new object[] {
            resources.GetString("cmbBxDesktop.Items"),
            resources.GetString("cmbBxDesktop.Items1")});
            this.cmbBxDesktop.Name = "cmbBxDesktop";
            this.cmbBxDesktop.SelectedIndexChanged += new System.EventHandler(this.cmbBxDesktop_SelectedIndexChanged);
            // 
            // txtBxDirectory
            // 
            resources.ApplyResources(this.txtBxDirectory, "txtBxDirectory");
            this.txtBxDirectory.Name = "txtBxDirectory";
            this.txtBxDirectory.TextChanged += new System.EventHandler(this.txtBxDirectory_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // CreateShortcutAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkBxAbortIfDontExists);
            this.Controls.Add(this.cmbBxWindowStyle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBxWorkingDirectory);
            this.Controls.Add(this.txtBxArguments);
            this.Controls.Add(this.txtBxIcon);
            this.Controls.Add(this.txtBxDescription);
            this.Controls.Add(this.txtBxName);
            this.Controls.Add(this.txtBxTarget);
            this.Name = "CreateShortcutAction";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBxTarget;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.TextBox txtBxIcon;
        private System.Windows.Forms.TextBox txtBxArguments;
        private System.Windows.Forms.TextBox txtBxWorkingDirectory;
        private EasyCompany.Controls.ExtendedLabel label1;
        private EasyCompany.Controls.ExtendedLabel label2;
        private EasyCompany.Controls.ExtendedLabel label3;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private EasyCompany.Controls.ExtendedLabel label6;
        private System.Windows.Forms.ComboBox cmbBxWindowStyle;
        private System.Windows.Forms.CheckBox chkBxAbortIfDontExists;
        private System.Windows.Forms.GroupBox groupBox1;
        private EasyCompany.Controls.ExtendedLabel label10;
        private EasyCompany.Controls.ExtendedLabel label9;
        private System.Windows.Forms.RadioButton rdBtnPersonnalized;
        private System.Windows.Forms.RadioButton rdBtnDesktop;
        private System.Windows.Forms.ComboBox cmbBxDesktop;
        private System.Windows.Forms.TextBox txtBxDirectory;
        private EasyCompany.Controls.ExtendedLabel label7;
        private EasyCompany.Controls.ExtendedLabel label8;
        private EasyCompany.Controls.ExtendedLabel label11;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
    }
}
