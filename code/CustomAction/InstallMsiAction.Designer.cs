namespace CustomActions
{
    partial class InstallMsiAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallMsiAction));
            this.nupKillProcess = new System.Windows.Forms.NumericUpDown();
            this.chkBxKillProcess = new System.Windows.Forms.CheckBox();
            this.txtBxMsiName = new System.Windows.Forms.TextBox();
            this.txtBxParameters = new System.Windows.Forms.TextBox();
            this.cmbBxUiLevel = new System.Windows.Forms.ComboBox();
            this.cmbBxRestartBehavior = new System.Windows.Forms.ComboBox();
            this.chkBxLogTo = new System.Windows.Forms.CheckBox();
            this.txtBxLogTo = new System.Windows.Forms.TextBox();
            this.chkBxStoreToVariable = new System.Windows.Forms.CheckBox();
            this.extendedLabel1 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel2 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel3 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel4 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel5 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel6 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel7 = new EasyCompany.Controls.ExtendedLabel();
            this.extendedLabel8 = new EasyCompany.Controls.ExtendedLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // nupKillProcess
            // 
            resources.ApplyResources(this.nupKillProcess, "nupKillProcess");
            this.nupKillProcess.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nupKillProcess.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupKillProcess.Name = "nupKillProcess";
            this.nupKillProcess.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // chkBxKillProcess
            // 
            resources.ApplyResources(this.chkBxKillProcess, "chkBxKillProcess");
            this.chkBxKillProcess.Name = "chkBxKillProcess";
            this.chkBxKillProcess.UseVisualStyleBackColor = true;
            this.chkBxKillProcess.CheckedChanged += new System.EventHandler(this.chkBxKillProcess_CheckedChanged);
            // 
            // txtBxMsiName
            // 
            resources.ApplyResources(this.txtBxMsiName, "txtBxMsiName");
            this.txtBxMsiName.BackColor = System.Drawing.Color.Orange;
            this.txtBxMsiName.Name = "txtBxMsiName";
            this.txtBxMsiName.TextChanged += new System.EventHandler(this.txtBxMsiName_TextChanged);
            // 
            // txtBxParameters
            // 
            resources.ApplyResources(this.txtBxParameters, "txtBxParameters");
            this.txtBxParameters.Name = "txtBxParameters";
            this.txtBxParameters.TextChanged += new System.EventHandler(this.txtBxParameters_TextChanged);
            // 
            // cmbBxUiLevel
            // 
            resources.ApplyResources(this.cmbBxUiLevel, "cmbBxUiLevel");
            this.cmbBxUiLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxUiLevel.FormattingEnabled = true;
            this.cmbBxUiLevel.Name = "cmbBxUiLevel";
            this.cmbBxUiLevel.SelectedIndexChanged += new System.EventHandler(this.cmbBxUiLevel_SelectedIndexChanged);
            // 
            // cmbBxRestartBehavior
            // 
            resources.ApplyResources(this.cmbBxRestartBehavior, "cmbBxRestartBehavior");
            this.cmbBxRestartBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxRestartBehavior.FormattingEnabled = true;
            this.cmbBxRestartBehavior.Name = "cmbBxRestartBehavior";
            this.cmbBxRestartBehavior.SelectedIndexChanged += new System.EventHandler(this.cmbBxRestartBehavior_SelectedIndexChanged);
            // 
            // chkBxLogTo
            // 
            resources.ApplyResources(this.chkBxLogTo, "chkBxLogTo");
            this.chkBxLogTo.Name = "chkBxLogTo";
            this.chkBxLogTo.UseVisualStyleBackColor = true;
            this.chkBxLogTo.CheckedChanged += new System.EventHandler(this.chkBxLogTo_CheckedChanged);
            // 
            // txtBxLogTo
            // 
            resources.ApplyResources(this.txtBxLogTo, "txtBxLogTo");
            this.txtBxLogTo.Name = "txtBxLogTo";
            this.txtBxLogTo.TextChanged += new System.EventHandler(this.txtBxLogTo_TextChanged);
            // 
            // chkBxStoreToVariable
            // 
            resources.ApplyResources(this.chkBxStoreToVariable, "chkBxStoreToVariable");
            this.chkBxStoreToVariable.Name = "chkBxStoreToVariable";
            this.chkBxStoreToVariable.UseVisualStyleBackColor = true;
            this.chkBxStoreToVariable.CheckedChanged += new System.EventHandler(this.chkBxStoreToVariable_CheckedChanged);
            // 
            // extendedLabel1
            // 
            resources.ApplyResources(this.extendedLabel1, "extendedLabel1");
            this.extendedLabel1.Name = "extendedLabel1";
            // 
            // extendedLabel2
            // 
            resources.ApplyResources(this.extendedLabel2, "extendedLabel2");
            this.extendedLabel2.Name = "extendedLabel2";
            // 
            // extendedLabel3
            // 
            resources.ApplyResources(this.extendedLabel3, "extendedLabel3");
            this.extendedLabel3.Name = "extendedLabel3";
            // 
            // extendedLabel4
            // 
            resources.ApplyResources(this.extendedLabel4, "extendedLabel4");
            this.extendedLabel4.Name = "extendedLabel4";
            // 
            // extendedLabel5
            // 
            resources.ApplyResources(this.extendedLabel5, "extendedLabel5");
            this.extendedLabel5.Name = "extendedLabel5";
            // 
            // extendedLabel6
            // 
            resources.ApplyResources(this.extendedLabel6, "extendedLabel6");
            this.extendedLabel6.Name = "extendedLabel6";
            // 
            // extendedLabel7
            // 
            resources.ApplyResources(this.extendedLabel7, "extendedLabel7");
            this.extendedLabel7.Name = "extendedLabel7";
            // 
            // extendedLabel8
            // 
            resources.ApplyResources(this.extendedLabel8, "extendedLabel8");
            this.extendedLabel8.Name = "extendedLabel8";
            // 
            // InstallMsiAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.extendedLabel8);
            this.Controls.Add(this.extendedLabel7);
            this.Controls.Add(this.extendedLabel6);
            this.Controls.Add(this.extendedLabel5);
            this.Controls.Add(this.extendedLabel4);
            this.Controls.Add(this.extendedLabel3);
            this.Controls.Add(this.extendedLabel2);
            this.Controls.Add(this.extendedLabel1);
            this.Controls.Add(this.chkBxStoreToVariable);
            this.Controls.Add(this.txtBxLogTo);
            this.Controls.Add(this.chkBxLogTo);
            this.Controls.Add(this.cmbBxRestartBehavior);
            this.Controls.Add(this.cmbBxUiLevel);
            this.Controls.Add(this.txtBxParameters);
            this.Controls.Add(this.txtBxMsiName);
            this.Controls.Add(this.nupKillProcess);
            this.Controls.Add(this.chkBxKillProcess);
            this.Name = "InstallMsiAction";
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nupKillProcess;
        private System.Windows.Forms.CheckBox chkBxKillProcess;
        private System.Windows.Forms.TextBox txtBxMsiName;
        private System.Windows.Forms.TextBox txtBxParameters;
        private System.Windows.Forms.ComboBox cmbBxUiLevel;
        private System.Windows.Forms.ComboBox cmbBxRestartBehavior;
        private System.Windows.Forms.CheckBox chkBxLogTo;
        private System.Windows.Forms.TextBox txtBxLogTo;
        private System.Windows.Forms.CheckBox chkBxStoreToVariable;
        private EasyCompany.Controls.ExtendedLabel extendedLabel1;
        private EasyCompany.Controls.ExtendedLabel extendedLabel2;
        private EasyCompany.Controls.ExtendedLabel extendedLabel3;
        private EasyCompany.Controls.ExtendedLabel extendedLabel4;
        private EasyCompany.Controls.ExtendedLabel extendedLabel5;
        private EasyCompany.Controls.ExtendedLabel extendedLabel6;
        private EasyCompany.Controls.ExtendedLabel extendedLabel7;
        private EasyCompany.Controls.ExtendedLabel extendedLabel8;
    }
}
