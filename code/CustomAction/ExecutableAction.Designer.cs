namespace CustomActions
{
    partial class ExecutableAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecutableAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxPath = new System.Windows.Forms.TextBox();
            this.txtBxParameters = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.chkBxKillProcess = new System.Windows.Forms.CheckBox();
            this.nupKillProcess = new System.Windows.Forms.NumericUpDown();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.chkBxStoreToVariable = new System.Windows.Forms.CheckBox();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxPath
            // 
            resources.ApplyResources(this.txtBxPath, "txtBxPath");
            this.txtBxPath.BackColor = System.Drawing.Color.Orange;
            this.txtBxPath.Name = "txtBxPath";
            this.txtBxPath.TextChanged += new System.EventHandler(this.txtBxPath_TextChanged);
            // 
            // txtBxParameters
            // 
            resources.ApplyResources(this.txtBxParameters, "txtBxParameters");
            this.txtBxParameters.Name = "txtBxParameters";
            this.txtBxParameters.TextChanged += new System.EventHandler(this.txtBxParameters_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkBxKillProcess
            // 
            resources.ApplyResources(this.chkBxKillProcess, "chkBxKillProcess");
            this.chkBxKillProcess.Name = "chkBxKillProcess";
            this.chkBxKillProcess.UseVisualStyleBackColor = true;
            this.chkBxKillProcess.CheckedChanged += new System.EventHandler(this.chkBxKillProcess_CheckedChanged);
            // 
            // nupKillProcess
            // 
            resources.ApplyResources(this.nupKillProcess, "nupKillProcess");
            this.nupKillProcess.Maximum = new decimal(new int[] {
            120,
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
            10,
            0,
            0,
            0});
            this.nupKillProcess.ValueChanged += new System.EventHandler(this.nupKillProcess_ValueChanged);
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
            // chkBxStoreToVariable
            // 
            resources.ApplyResources(this.chkBxStoreToVariable, "chkBxStoreToVariable");
            this.chkBxStoreToVariable.Name = "chkBxStoreToVariable";
            this.chkBxStoreToVariable.UseVisualStyleBackColor = true;
            this.chkBxStoreToVariable.CheckedChanged += new System.EventHandler(this.chkBxStoreToVariable_CheckedChanged);
            // 
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // ExecutableAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkEnvironmentVariables);
            this.Controls.Add(this.chkBxStoreToVariable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nupKillProcess);
            this.Controls.Add(this.chkBxKillProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxParameters);
            this.Controls.Add(this.txtBxPath);
            this.Controls.Add(this.label1);
            this.Name = "ExecutableAction";
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxPath;
        private System.Windows.Forms.TextBox txtBxParameters;
        private EasyCompany.Controls.ExtendedLabel label2;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private EasyCompany.Controls.ExtendedLabel label6;
        private System.Windows.Forms.CheckBox chkBxStoreToVariable;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
        public System.Windows.Forms.NumericUpDown nupKillProcess;
        public System.Windows.Forms.CheckBox chkBxKillProcess;
    }
}
