namespace CustomActions
{
    partial class FrmEnvironmentVariables
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnvironmentVariables));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxVariable = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxTranslation = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.cmbBxUsualVariables = new System.Windows.Forms.ComboBox();
            this.lblReferenceToUserProfile = new EasyCompany.Controls.ExtendedLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxVariable
            // 
            resources.ApplyResources(this.txtBxVariable, "txtBxVariable");
            this.txtBxVariable.Name = "txtBxVariable";
            this.txtBxVariable.TextChanged += new System.EventHandler(this.txtBxVariable_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxTranslation
            // 
            resources.ApplyResources(this.txtBxTranslation, "txtBxTranslation");
            this.txtBxTranslation.Name = "txtBxTranslation";
            this.txtBxTranslation.ReadOnly = true;
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
            // cmbBxUsualVariables
            // 
            resources.ApplyResources(this.cmbBxUsualVariables, "cmbBxUsualVariables");
            this.cmbBxUsualVariables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxUsualVariables.FormattingEnabled = true;
            this.cmbBxUsualVariables.Items.AddRange(new object[] {
            resources.GetString("cmbBxUsualVariables.Items"),
            resources.GetString("cmbBxUsualVariables.Items1"),
            resources.GetString("cmbBxUsualVariables.Items2"),
            resources.GetString("cmbBxUsualVariables.Items3"),
            resources.GetString("cmbBxUsualVariables.Items4"),
            resources.GetString("cmbBxUsualVariables.Items5"),
            resources.GetString("cmbBxUsualVariables.Items6"),
            resources.GetString("cmbBxUsualVariables.Items7"),
            resources.GetString("cmbBxUsualVariables.Items8"),
            resources.GetString("cmbBxUsualVariables.Items9"),
            resources.GetString("cmbBxUsualVariables.Items10"),
            resources.GetString("cmbBxUsualVariables.Items11"),
            resources.GetString("cmbBxUsualVariables.Items12")});
            this.cmbBxUsualVariables.Name = "cmbBxUsualVariables";
            this.cmbBxUsualVariables.Sorted = true;
            this.cmbBxUsualVariables.SelectedIndexChanged += new System.EventHandler(this.cmbBxUsualVariables_SelectedIndexChanged);
            // 
            // lblReferenceToUserProfile
            // 
            resources.ApplyResources(this.lblReferenceToUserProfile, "lblReferenceToUserProfile");
            this.lblReferenceToUserProfile.ForeColor = System.Drawing.Color.Red;
            this.lblReferenceToUserProfile.Name = "lblReferenceToUserProfile";
            // 
            // FrmEnvironmentVariables
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.lblReferenceToUserProfile);
            this.Controls.Add(this.cmbBxUsualVariables);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxTranslation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxVariable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Name = "FrmEnvironmentVariables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxVariable;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxTranslation;
        private EasyCompany.Controls.ExtendedLabel label3;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.ComboBox cmbBxUsualVariables;
        private EasyCompany.Controls.ExtendedLabel lblReferenceToUserProfile;
    }
}