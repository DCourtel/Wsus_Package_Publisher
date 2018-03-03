namespace CustomUpdateElementViewer
{
    partial class CustomUpdateElementViewer
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
            this.tlpElementViewer = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tlpElementViewer
            // 
            this.tlpElementViewer.AutoSize = true;
            this.tlpElementViewer.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpElementViewer.ColumnCount = 1;
            this.tlpElementViewer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpElementViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpElementViewer.Location = new System.Drawing.Point(0, 0);
            this.tlpElementViewer.Name = "tlpElementViewer";
            this.tlpElementViewer.RowCount = 2;
            this.tlpElementViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpElementViewer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpElementViewer.Size = new System.Drawing.Size(320, 3);
            this.tlpElementViewer.TabIndex = 0;
            // 
            // CustomUpdateElementViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tlpElementViewer);
            this.Name = "CustomUpdateElementViewer";
            this.Size = new System.Drawing.Size(320, 605);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpElementViewer;
    }
}
