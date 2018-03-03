namespace CustomUpdateElements
{
    partial class GenericElement
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
            this.pctBxIcone = new System.Windows.Forms.PictureBox();
            this.lblDescription = new EasyCompany.Controls.ExtendedLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBxIcone
            // 
            this.pctBxIcone.Location = new System.Drawing.Point(3, 3);
            this.pctBxIcone.Name = "pctBxIcone";
            this.pctBxIcone.Size = new System.Drawing.Size(45, 45);
            this.pctBxIcone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pctBxIcone.TabIndex = 0;
            this.pctBxIcone.TabStop = false;
            this.pctBxIcone.DoubleClick += new System.EventHandler(this.element_DoubleClick);
            this.pctBxIcone.MouseDown += new System.Windows.Forms.MouseEventHandler(this.element_MouseDown);
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Location = new System.Drawing.Point(49, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(407, 43);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.DoubleClick += new System.EventHandler(this.element_DoubleClick);
            this.lblDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.element_MouseDown);
            // 
            // GenericElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pctBxIcone);
            this.Name = "GenericElement";
            this.Size = new System.Drawing.Size(463, 53);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.element_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pctBxIcone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.PictureBox pctBxIcone;
        private EasyCompany.Controls.ExtendedLabel lblDescription;

    }
}
