namespace CustomActions
{
    partial class StartServiceAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartServiceAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.txtBxServiceName = new System.Windows.Forms.TextBox();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBxServiceName
            // 
            resources.ApplyResources(this.txtBxServiceName, "txtBxServiceName");
            this.txtBxServiceName.BackColor = System.Drawing.Color.Orange;
            this.txtBxServiceName.Name = "txtBxServiceName";
            this.txtBxServiceName.TextChanged += new System.EventHandler(this.txtBxServiceName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // StartServiceAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBxServiceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "StartServiceAction";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.TextBox txtBxServiceName;
        private EasyCompany.Controls.ExtendedLabel label2;
    }
}
