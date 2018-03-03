namespace GroupAndRuleViewer
{
    partial class GroupDisplayer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbxStart = new System.Windows.Forms.RichTextBox();
            this.rtbxEnd = new System.Windows.Forms.RichTextBox();
            this.tlpRulesAndGroups = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rtbxStart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbxEnd, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpRulesAndGroups, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(640, 147);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // rtbxStart
            // 
            this.rtbxStart.BackColor = System.Drawing.Color.Silver;
            this.rtbxStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbxStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbxStart.Location = new System.Drawing.Point(3, 3);
            this.rtbxStart.Name = "rtbxStart";
            this.rtbxStart.Size = new System.Drawing.Size(634, 24);
            this.rtbxStart.TabIndex = 0;
            this.rtbxStart.Text = "";
            this.rtbxStart.Click += new System.EventHandler(this.rtbxStart_Click);
            this.rtbxStart.DoubleClick += new System.EventHandler(this.rtbxStart_DoubleClick);
            this.rtbxStart.MouseEnter += new System.EventHandler(this.rtbxStart_MouseEnter);
            this.rtbxStart.MouseLeave += new System.EventHandler(this.rtbxStart_MouseLeave);
            // 
            // rtbxEnd
            // 
            this.rtbxEnd.BackColor = System.Drawing.Color.Silver;
            this.rtbxEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbxEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbxEnd.Location = new System.Drawing.Point(3, 120);
            this.rtbxEnd.Name = "rtbxEnd";
            this.rtbxEnd.Size = new System.Drawing.Size(634, 24);
            this.rtbxEnd.TabIndex = 1;
            this.rtbxEnd.Text = "";
            this.rtbxEnd.Click += new System.EventHandler(this.rtbxStart_Click);
            this.rtbxEnd.DoubleClick += new System.EventHandler(this.rtbxStart_DoubleClick);
            this.rtbxEnd.MouseEnter += new System.EventHandler(this.rtbxStart_MouseEnter);
            this.rtbxEnd.MouseLeave += new System.EventHandler(this.rtbxStart_MouseLeave);
            // 
            // tlpRulesAndGroups
            // 
            this.tlpRulesAndGroups.AutoScroll = true;
            this.tlpRulesAndGroups.AutoSize = true;
            this.tlpRulesAndGroups.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpRulesAndGroups.BackColor = System.Drawing.Color.Gainsboro;
            this.tlpRulesAndGroups.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tlpRulesAndGroups.ColumnCount = 1;
            this.tlpRulesAndGroups.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpRulesAndGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRulesAndGroups.Location = new System.Drawing.Point(3, 33);
            this.tlpRulesAndGroups.Name = "tlpRulesAndGroups";
            this.tlpRulesAndGroups.RowCount = 1;
            this.tlpRulesAndGroups.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRulesAndGroups.Size = new System.Drawing.Size(634, 81);
            this.tlpRulesAndGroups.TabIndex = 2;
            this.tlpRulesAndGroups.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tlpRulesAndGroups_ControlAdded);
            // 
            // GroupDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GroupDisplayer";
            this.Size = new System.Drawing.Size(640, 147);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox rtbxStart;
        private System.Windows.Forms.RichTextBox rtbxEnd;
        private System.Windows.Forms.TableLayoutPanel tlpRulesAndGroups;
    }
}
