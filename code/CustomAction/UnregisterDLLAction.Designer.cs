namespace CustomActions
{
    partial class UnregisterDLLAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnregisterDLLAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFullPath = new System.Windows.Forms.TextBox();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
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
            // txtBxFullPath
            // 
            resources.ApplyResources(this.txtBxFullPath, "txtBxFullPath");
            this.txtBxFullPath.BackColor = System.Drawing.Color.Orange;
            this.txtBxFullPath.Name = "txtBxFullPath";
            this.txtBxFullPath.TextChanged += new System.EventHandler(this.txtBxFullPath_TextChanged);
            // 
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // UnregisterDLLAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkEnvironmentVariables);
            this.Controls.Add(this.txtBxFullPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UnregisterDLLAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxFullPath;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
    }
}
