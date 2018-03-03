namespace CustomUpdateCreator
{
    partial class FrmCustomUpdateCreator
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomUpdateCreator));
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tlpElements = new System.Windows.Forms.TableLayoutPanel();
            this.lblIndication = new System.Windows.Forms.Label();
            this.txtBxDescription = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.customUpdateElementViewer1 = new CustomUpdateElementViewer.CustomUpdateElementViewer();
            this.ctxMnuElement = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlStrpUpElement = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpDownElement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStrpDeleteElement = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lnkLblHelp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.ctxMnuElement.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.splitContainer2, "splitContainer2");
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(this.splitContainer2.Panel1, "splitContainer2.Panel1");
            this.splitContainer2.Panel1.Controls.Add(this.tlpElements);
            this.splitContainer2.Panel1.Controls.Add(this.lblIndication);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtBxDescription);
            // 
            // tlpElements
            // 
            resources.ApplyResources(this.tlpElements, "tlpElements");
            this.tlpElements.Name = "tlpElements";
            this.tlpElements.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.tlpElements_ControlsCountChange);
            this.tlpElements.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tlpElements_ControlsCountChange);
            // 
            // lblIndication
            // 
            resources.ApplyResources(this.lblIndication, "lblIndication");
            this.lblIndication.ForeColor = System.Drawing.Color.Teal;
            this.lblIndication.Name = "lblIndication";
            // 
            // txtBxDescription
            // 
            this.txtBxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txtBxDescription, "txtBxDescription");
            this.txtBxDescription.Name = "txtBxDescription";
            this.txtBxDescription.ReadOnly = true;
            this.txtBxDescription.TabStop = false;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.customUpdateElementViewer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AllowDrop = true;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            // 
            // customUpdateElementViewer1
            // 
            resources.ApplyResources(this.customUpdateElementViewer1, "customUpdateElementViewer1");
            this.customUpdateElementViewer1.Name = "customUpdateElementViewer1";
            // 
            // ctxMnuElement
            // 
            this.ctxMnuElement.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctxMnuElement.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpUpElement,
            this.tlStrpDownElement,
            this.toolStripSeparator1,
            this.tlStrpDeleteElement});
            this.ctxMnuElement.Name = "ctxMnuElement";
            resources.ApplyResources(this.ctxMnuElement, "ctxMnuElement");
            this.ctxMnuElement.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuElement_ItemClicked);
            // 
            // tlStrpUpElement
            // 
            this.tlStrpUpElement.Image = global::CustomUpdateCreator.Properties.Resources.Up;
            this.tlStrpUpElement.Name = "tlStrpUpElement";
            resources.ApplyResources(this.tlStrpUpElement, "tlStrpUpElement");
            // 
            // tlStrpDownElement
            // 
            this.tlStrpDownElement.Image = global::CustomUpdateCreator.Properties.Resources.Down;
            this.tlStrpDownElement.Name = "tlStrpDownElement";
            resources.ApplyResources(this.tlStrpDownElement, "tlStrpDownElement");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tlStrpDeleteElement
            // 
            this.tlStrpDeleteElement.Image = global::CustomUpdateCreator.Properties.Resources.Delete;
            this.tlStrpDeleteElement.Name = "tlStrpDeleteElement";
            resources.ApplyResources(this.tlStrpDeleteElement, "tlStrpDeleteElement");
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lnkLblHelp
            // 
            resources.ApplyResources(this.lnkLblHelp, "lnkLblHelp");
            this.lnkLblHelp.Name = "lnkLblHelp";
            this.lnkLblHelp.TabStop = true;
            this.lnkLblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblHelp_LinkClicked);
            // 
            // FrmCustomUpdateCreator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkLblHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmCustomUpdateCreator";
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ctxMnuElement.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private CustomUpdateElementViewer.CustomUpdateElementViewer customUpdateElementViewer1;
        private System.Windows.Forms.Label lblIndication;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tlpElements;
        private System.Windows.Forms.TextBox txtBxDescription;
        private System.Windows.Forms.ContextMenuStrip ctxMnuElement;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tlStrpUpElement;
        private System.Windows.Forms.ToolStripMenuItem tlStrpDownElement;
        private System.Windows.Forms.ToolStripMenuItem tlStrpDeleteElement;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.LinkLabel lnkLblHelp;
    }
}

