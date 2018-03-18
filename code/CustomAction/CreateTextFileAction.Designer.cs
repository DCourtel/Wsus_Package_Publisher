namespace CustomActions
{
    partial class CreateTextFileAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateTextFileAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxFilename = new System.Windows.Forms.TextBox();
            this.label3 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxContent = new System.Windows.Forms.TextBox();
            this.label4 = new EasyCompany.Controls.ExtendedLabel();
            this.label5 = new EasyCompany.Controls.ExtendedLabel();
            this.lnkEnvironmentVariables = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxFilePath
            // 
            resources.ApplyResources(this.txtBxFilePath, "txtBxFilePath");
            this.txtBxFilePath.BackColor = System.Drawing.Color.Orange;
            this.txtBxFilePath.Name = "txtBxFilePath";
            this.txtBxFilePath.TextChanged += new System.EventHandler(this.txtBxFilePath_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxFilename
            // 
            resources.ApplyResources(this.txtBxFilename, "txtBxFilename");
            this.txtBxFilename.BackColor = System.Drawing.Color.Orange;
            this.txtBxFilename.Name = "txtBxFilename";
            this.txtBxFilename.TextChanged += new System.EventHandler(this.txtBxFilename_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtBxContent
            // 
            resources.ApplyResources(this.txtBxContent, "txtBxContent");
            this.txtBxContent.Name = "txtBxContent";
            this.txtBxContent.TextChanged += new System.EventHandler(this.txtBxContent_TextChanged);
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
            // lnkEnvironmentVariables
            // 
            resources.ApplyResources(this.lnkEnvironmentVariables, "lnkEnvironmentVariables");
            this.lnkEnvironmentVariables.Name = "lnkEnvironmentVariables";
            this.lnkEnvironmentVariables.TabStop = true;
            this.lnkEnvironmentVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEnvironmentVariables_LinkClicked);
            // 
            // CreateTextFileAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkEnvironmentVariables);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBxContent);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBxFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxFilePath);
            this.Controls.Add(this.label1);
            this.Name = "CreateTextFileAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxFilePath;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.TextBox txtBxFilename;
        private EasyCompany.Controls.ExtendedLabel label3;
        private System.Windows.Forms.TextBox txtBxContent;
        private EasyCompany.Controls.ExtendedLabel label4;
        private EasyCompany.Controls.ExtendedLabel label5;
        private System.Windows.Forms.LinkLabel lnkEnvironmentVariables;
    }
}
