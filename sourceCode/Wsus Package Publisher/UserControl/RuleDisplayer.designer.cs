namespace Wsus_Package_Publisher
{
    partial class RuleDisplayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RuleDisplayer));
            this.SuspendLayout();
            // 
            // RuleDisplayer
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this, "$this");
            this.ReadOnly = true;
            this.Click += new System.EventHandler(this.RuleDisplayer_Click);
            this.SizeChanged += new System.EventHandler(this.RuleDisplayer_SizeChanged);
            this.DoubleClick += new System.EventHandler(this.RuleDisplayer_DoubleClick);
            this.MouseEnter += new System.EventHandler(this.RuleDisplayer_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.RuleDisplayer_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
