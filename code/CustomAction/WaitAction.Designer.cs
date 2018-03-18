namespace CustomActions
{
    partial class WaitAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitAction));
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.label2 = new EasyCompany.Controls.ExtendedLabel();
            this.nupAmountOfTime = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nupAmountOfTime)).BeginInit();
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
            // nupAmountOfTime
            // 
            resources.ApplyResources(this.nupAmountOfTime, "nupAmountOfTime");
            this.nupAmountOfTime.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nupAmountOfTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupAmountOfTime.Name = "nupAmountOfTime";
            this.nupAmountOfTime.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nupAmountOfTime.ValueChanged += new System.EventHandler(this.nupAmountOfTime_ValueChanged);
            // 
            // WaitAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nupAmountOfTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "WaitAction";
            ((System.ComponentModel.ISupportInitialize)(this.nupAmountOfTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EasyCompany.Controls.ExtendedLabel label1;
        private EasyCompany.Controls.ExtendedLabel label2;
        private System.Windows.Forms.NumericUpDown nupAmountOfTime;
    }
}
