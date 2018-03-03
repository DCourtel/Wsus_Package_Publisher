namespace Wsus_Package_Publisher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupDisplayer));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbxStart = new System.Windows.Forms.RichTextBox();
            this.rtbxEnd = new System.Windows.Forms.RichTextBox();
            this.tlpRulesAndGroups = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.rtbxStart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtbxEnd, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tlpRulesAndGroups, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // rtbxStart
            // 
            resources.ApplyResources(this.rtbxStart, "rtbxStart");
            this.rtbxStart.BackColor = System.Drawing.Color.Silver;
            this.rtbxStart.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbxStart.Name = "rtbxStart";
            this.rtbxStart.Click += new System.EventHandler(this.rtbxStart_Click);
            this.rtbxStart.DoubleClick += new System.EventHandler(this.rtbxStart_DoubleClick);
            this.rtbxStart.MouseEnter += new System.EventHandler(this.rtbxStart_MouseEnter);
            this.rtbxStart.MouseLeave += new System.EventHandler(this.rtbxStart_MouseLeave);
            // 
            // rtbxEnd
            // 
            resources.ApplyResources(this.rtbxEnd, "rtbxEnd");
            this.rtbxEnd.BackColor = System.Drawing.Color.Silver;
            this.rtbxEnd.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbxEnd.Name = "rtbxEnd";
            this.rtbxEnd.Click += new System.EventHandler(this.rtbxStart_Click);
            this.rtbxEnd.DoubleClick += new System.EventHandler(this.rtbxStart_DoubleClick);
            this.rtbxEnd.MouseEnter += new System.EventHandler(this.rtbxStart_MouseEnter);
            this.rtbxEnd.MouseLeave += new System.EventHandler(this.rtbxStart_MouseLeave);
            // 
            // tlpRulesAndGroups
            // 
            resources.ApplyResources(this.tlpRulesAndGroups, "tlpRulesAndGroups");
            this.tlpRulesAndGroups.BackColor = System.Drawing.Color.Gainsboro;
            this.tlpRulesAndGroups.Name = "tlpRulesAndGroups";
            this.tlpRulesAndGroups.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tlpRulesAndGroups_ControlAdded);
            // 
            // GroupDisplayer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GroupDisplayer";
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
