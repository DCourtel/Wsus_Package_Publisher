namespace CustomActions
{
    partial class CopyFileAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyFileAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxSourceFile = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxDestinationFolder = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxSourceFile
            // 
            resources.ApplyResources(this.txtBxSourceFile, "txtBxSourceFile");
            this.txtBxSourceFile.BackColor = System.Drawing.Color.Orange;
            this.txtBxSourceFile.Name = "txtBxSourceFile";
            this.txtBxSourceFile.TextChanged += new System.EventHandler(this.txtBxSourceFile_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxDestinationFolder
            // 
            resources.ApplyResources(this.txtBxDestinationFolder, "txtBxDestinationFolder");
            this.txtBxDestinationFolder.BackColor = System.Drawing.Color.Orange;
            this.txtBxDestinationFolder.Name = "txtBxDestinationFolder";
            this.txtBxDestinationFolder.TextChanged += new System.EventHandler(this.txtBxDestinationFolder_TextChanged);
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
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // CopyFileAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkEnvironmentVariables);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxDestinationFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxSourceFile);
            this.Controls.Add(this.label1);
            this.Name = "CopyFileAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxSourceFile;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxDestinationFolder;
        private EasyCompany.Controls.ExtendedLabel label3;
        private EasyCompany.Controls.ExtendedLabel label4;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
    }
}
