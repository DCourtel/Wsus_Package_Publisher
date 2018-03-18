namespace CustomActions
{
    partial class UninstallMsiProductByNameAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallMsiProductByNameAction));
            this.label6 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.nupKillProcess = new System.Windows.Forms.NumericUpDown();
            this.chkBxKillProcess = new System.Windows.Forms.CheckBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxParameters = new System.Windows.Forms.TextBox();
            this.txtBxApplicationName = new System.Windows.Forms.TextBox();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.chkBxDontUninstallIfNoException = new System.Windows.Forms.CheckBox();
            this.btnLaunchRemoteMsiManager = new System.Windows.Forms.Button();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxExceptions = new System.Windows.Forms.TextBox();
            this.label7 = new EasyCompany.Controls.ExtendedLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
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
            7,
            0,
            0,
            0});
            this.nupKillProcess.ValueChanged += new System.EventHandler(this.nupKillProcess_ValueChanged);
            // 
            // chkBxKillProcess
            // 
            resources.ApplyResources(this.chkBxKillProcess, "chkBxKillProcess");
            this.chkBxKillProcess.Name = "chkBxKillProcess";
            this.chkBxKillProcess.UseVisualStyleBackColor = true;
            this.chkBxKillProcess.CheckedChanged += new System.EventHandler(this.chkBxKillProcess_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxParameters
            // 
            resources.ApplyResources(this.txtBxParameters, "txtBxParameters");
            this.txtBxParameters.Name = "txtBxParameters";
            this.txtBxParameters.TextChanged += new System.EventHandler(this.txtBxParameters_TextChanged);
            // 
            // txtBxApplicationName
            // 
            resources.ApplyResources(this.txtBxApplicationName, "txtBxApplicationName");
            this.txtBxApplicationName.BackColor = System.Drawing.Color.Orange;
            this.txtBxApplicationName.Name = "txtBxApplicationName";
            this.txtBxApplicationName.TextChanged += new System.EventHandler(this.txtBxApplicationName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkBxDontUninstallIfNoException
            // 
            resources.ApplyResources(this.chkBxDontUninstallIfNoException, "chkBxDontUninstallIfNoException");
            this.chkBxDontUninstallIfNoException.Name = "chkBxDontUninstallIfNoException";
            this.chkBxDontUninstallIfNoException.UseVisualStyleBackColor = true;
            this.chkBxDontUninstallIfNoException.CheckedChanged += new System.EventHandler(this.chkBxDontUninstallIfNoException_CheckedChanged);
            // 
            // btnLaunchRemoteMsiManager
            // 
            resources.ApplyResources(this.btnLaunchRemoteMsiManager, "btnLaunchRemoteMsiManager");
            this.btnLaunchRemoteMsiManager.Name = "btnLaunchRemoteMsiManager";
            this.btnLaunchRemoteMsiManager.UseVisualStyleBackColor = true;
            this.btnLaunchRemoteMsiManager.Click += new System.EventHandler(this.btnLaunchRemoteMsiManager_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxExceptions
            // 
            resources.ApplyResources(this.txtBxExceptions, "txtBxExceptions");
            this.txtBxExceptions.Name = "txtBxExceptions";
            this.txtBxExceptions.TextChanged += new System.EventHandler(this.txtBxExceptions_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // UninstallMsiProductByNameAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLaunchRemoteMsiManager);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkBxDontUninstallIfNoException);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nupKillProcess);
            this.Controls.Add(this.chkBxKillProcess);
            this.Controls.Add(this.txtBxExceptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxParameters);
            this.Controls.Add(this.txtBxApplicationName);
            this.Controls.Add(this.label1);
            this.Name = "UninstallMsiProductByNameAction";
            ((System.ComponentModel.ISupportInitialize)(this.nupKillProcess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label6;
        private EasyCompany.Controls.ExtendedLabel label5;
        private EasyCompany.Controls.ExtendedLabel label4;
        public System.Windows.Forms.NumericUpDown nupKillProcess;
        public System.Windows.Forms.CheckBox chkBxKillProcess;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxParameters;
        private System.Windows.Forms.TextBox txtBxApplicationName;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.CheckBox chkBxDontUninstallIfNoException;
        private System.Windows.Forms.Button btnLaunchRemoteMsiManager;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxExceptions;
        private EasyCompany.Controls.ExtendedLabel label7;
    }
}
