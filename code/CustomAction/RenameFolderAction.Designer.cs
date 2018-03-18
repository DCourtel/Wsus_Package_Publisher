namespace CustomActions
{
    partial class RenameFolderAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RenameFolderAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxNewName = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxFolderPath
            // 
            resources.ApplyResources(this.txtBxFolderPath, "txtBxFolderPath");
            this.txtBxFolderPath.BackColor = System.Drawing.Color.Orange;
            this.txtBxFolderPath.Name = "txtBxFolderPath";
            this.txtBxFolderPath.TextChanged += new System.EventHandler(this.txtBxFolderPath_TextChanged);
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
            // txtBxNewName
            // 
            resources.ApplyResources(this.txtBxNewName, "txtBxNewName");
            this.txtBxNewName.BackColor = System.Drawing.Color.Orange;
            this.txtBxNewName.Name = "txtBxNewName";
            this.txtBxNewName.TextChanged += new System.EventHandler(this.txtBxNewName_TextChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // RenameFolderAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkEnvironmentVariables);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxNewName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxFolderPath);
            this.Controls.Add(this.label1);
            this.Name = "RenameFolderAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxFolderPath;
        private EasyCompany.Controls.ExtendedLabel label2;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxNewName;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
    }
}
