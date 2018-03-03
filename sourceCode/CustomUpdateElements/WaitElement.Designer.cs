namespace CustomUpdateElements
{
    partial class WaitElement
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
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new EasyCompany.Controls.ExtendedLabel();
            this.nupSeconds = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupSeconds)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(365, 63);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Seconds to wait : ";
            // 
            // nupSeconds
            // 
            this.nupSeconds.Location = new System.Drawing.Point(133, 63);
            this.nupSeconds.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.nupSeconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupSeconds.Name = "nupSeconds";
            this.nupSeconds.Size = new System.Drawing.Size(79, 20);
            this.nupSeconds.TabIndex = 5;
            this.nupSeconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupSeconds.ValueChanged += new System.EventHandler(this.nupSeconds_ValueChanged);
            // 
            // WaitElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nupSeconds);
            this.Controls.Add(this.btnOk);
            this.Name = "WaitElement";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.nupSeconds, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pctBxIcone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupSeconds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private EasyCompany.Controls.ExtendedLabel label1;
        private System.Windows.Forms.NumericUpDown nupSeconds;
    }
}
