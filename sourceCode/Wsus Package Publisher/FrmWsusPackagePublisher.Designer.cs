namespace Wsus_Package_Publisher
{
    partial class FrmWsusPackagePublisher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWsusPackagePublisher));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trvWsus = new System.Windows.Forms.TreeView();
            this.imgLstServer = new System.Windows.Forms.ImageList(this.components);
            this.mnuMainForm = new System.Windows.Forms.MenuStrip();
            this.filetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quittoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificatetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugueseBrazilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingstoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msiReadertoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAgainstActiveDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMaxCabFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatestoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUpdatetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createCustomUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportAnUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAnUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importFromCatalogtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageCatalogSubscriptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helptoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abouttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDebugInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbBxServerList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMnuTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.editMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMetaGroup = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mnuMainForm.SuspendLayout();
            this.ctxMnuTreeview.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trvWsus);
            // 
            // trvWsus
            // 
            this.trvWsus.AllowDrop = true;
            resources.ApplyResources(this.trvWsus, "trvWsus");
            this.trvWsus.HideSelection = false;
            this.trvWsus.ItemHeight = 16;
            this.trvWsus.Name = "trvWsus";
            this.trvWsus.StateImageList = this.imgLstServer;
            this.trvWsus.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvWsus_AfterSelect);
            this.trvWsus.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvWsus_NodeMouseClick);
            this.trvWsus.DragDrop += new System.Windows.Forms.DragEventHandler(this.trvWsus_DragDrop);
            this.trvWsus.DragEnter += new System.Windows.Forms.DragEventHandler(this.trvWsus_DragEnter);
            // 
            // imgLstServer
            // 
            this.imgLstServer.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imgLstServer, "imgLstServer");
            this.imgLstServer.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mnuMainForm
            // 
            this.mnuMainForm.BackColor = System.Drawing.Color.SteelBlue;
            this.mnuMainForm.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filetoolStripMenuItem,
            this.toolstoolStripMenuItem,
            this.updatestoolStripMenuItem,
            this.helptoolStripMenuItem,
            this.cmbBxServerList,
            this.toolStripMenuItem1});
            this.mnuMainForm.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.mnuMainForm, "mnuMainForm");
            this.mnuMainForm.Name = "mnuMainForm";
            // 
            // filetoolStripMenuItem
            // 
            this.filetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quittoolStripMenuItem});
            this.filetoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.filetoolStripMenuItem.Name = "filetoolStripMenuItem";
            resources.ApplyResources(this.filetoolStripMenuItem, "filetoolStripMenuItem");
            this.filetoolStripMenuItem.DropDownClosed += new System.EventHandler(this.toolStripMenuItem_DropDownClosed);
            this.filetoolStripMenuItem.DropDownOpened += new System.EventHandler(this.toolStripMenuItem_DropDownOpened);
            // 
            // quittoolStripMenuItem
            // 
            this.quittoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Log_Out_48;
            this.quittoolStripMenuItem.Name = "quittoolStripMenuItem";
            resources.ApplyResources(this.quittoolStripMenuItem, "quittoolStripMenuItem");
            this.quittoolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // toolstoolStripMenuItem
            // 
            this.toolstoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.certificatetoolStripMenuItem,
            this.languagetoolStripMenuItem,
            this.settingstoolStripMenuItem,
            this.msiReadertoolStripMenuItem,
            this.checkAgainstActiveDirectoryToolStripMenuItem,
            this.editMaxCabFileToolStripMenuItem});
            this.toolstoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolstoolStripMenuItem.Name = "toolstoolStripMenuItem";
            resources.ApplyResources(this.toolstoolStripMenuItem, "toolstoolStripMenuItem");
            this.toolstoolStripMenuItem.DropDownClosed += new System.EventHandler(this.toolStripMenuItem_DropDownClosed);
            this.toolstoolStripMenuItem.DropDownOpened += new System.EventHandler(this.toolStripMenuItem_DropDownOpened);
            // 
            // certificatetoolStripMenuItem
            // 
            resources.ApplyResources(this.certificatetoolStripMenuItem, "certificatetoolStripMenuItem");
            this.certificatetoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Certificate_64;
            this.certificatetoolStripMenuItem.Name = "certificatetoolStripMenuItem";
            this.certificatetoolStripMenuItem.Click += new System.EventHandler(this.certificatToolStripMenuItem_Click);
            // 
            // languagetoolStripMenuItem
            // 
            this.languagetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frenchtoolStripMenuItem,
            this.englishtoolStripMenuItem,
            this.germanToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.italianToolStripMenuItem,
            this.polandToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.portugueseBrazilToolStripMenuItem});
            this.languagetoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Bleu;
            this.languagetoolStripMenuItem.Name = "languagetoolStripMenuItem";
            resources.ApplyResources(this.languagetoolStripMenuItem, "languagetoolStripMenuItem");
            // 
            // frenchtoolStripMenuItem
            // 
            this.frenchtoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.drapeau_france;
            this.frenchtoolStripMenuItem.Name = "frenchtoolStripMenuItem";
            resources.ApplyResources(this.frenchtoolStripMenuItem, "frenchtoolStripMenuItem");
            this.frenchtoolStripMenuItem.Click += new System.EventHandler(this.françaisToolStripMenuItem_Click);
            // 
            // englishtoolStripMenuItem
            // 
            this.englishtoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.drapeau_grande_bretagne;
            this.englishtoolStripMenuItem.Name = "englishtoolStripMenuItem";
            resources.ApplyResources(this.englishtoolStripMenuItem, "englishtoolStripMenuItem");
            this.englishtoolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.drapeau_allemagne;
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            resources.ApplyResources(this.germanToolStripMenuItem, "germanToolStripMenuItem");
            this.germanToolStripMenuItem.Click += new System.EventHandler(this.germanToolStripMenuItem_Click);
            // 
            // spanishToolStripMenuItem
            // 
            resources.ApplyResources(this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
            this.spanishToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Spain;
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            // 
            // italianToolStripMenuItem
            // 
            resources.ApplyResources(this.italianToolStripMenuItem, "italianToolStripMenuItem");
            this.italianToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Italien;
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            // 
            // polandToolStripMenuItem
            // 
            resources.ApplyResources(this.polandToolStripMenuItem, "polandToolStripMenuItem");
            this.polandToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Polonais;
            this.polandToolStripMenuItem.Name = "polandToolStripMenuItem";
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Russe;
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.Click += new System.EventHandler(this.russianToolStripMenuItem_Click);
            // 
            // portugueseBrazilToolStripMenuItem
            // 
            resources.ApplyResources(this.portugueseBrazilToolStripMenuItem, "portugueseBrazilToolStripMenuItem");
            this.portugueseBrazilToolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Drapeau_Brazil;
            this.portugueseBrazilToolStripMenuItem.Name = "portugueseBrazilToolStripMenuItem";
            // 
            // settingstoolStripMenuItem
            // 
            this.settingstoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Settings_64;
            this.settingstoolStripMenuItem.Name = "settingstoolStripMenuItem";
            resources.ApplyResources(this.settingstoolStripMenuItem, "settingstoolStripMenuItem");
            this.settingstoolStripMenuItem.Click += new System.EventHandler(this.paramètresToolStripMenuItem_Click);
            // 
            // msiReadertoolStripMenuItem
            // 
            this.msiReadertoolStripMenuItem.Name = "msiReadertoolStripMenuItem";
            resources.ApplyResources(this.msiReadertoolStripMenuItem, "msiReadertoolStripMenuItem");
            this.msiReadertoolStripMenuItem.Click += new System.EventHandler(this.mSIPropertyReaderToolStripMenuItem_Click);
            // 
            // checkAgainstActiveDirectoryToolStripMenuItem
            // 
            resources.ApplyResources(this.checkAgainstActiveDirectoryToolStripMenuItem, "checkAgainstActiveDirectoryToolStripMenuItem");
            this.checkAgainstActiveDirectoryToolStripMenuItem.Name = "checkAgainstActiveDirectoryToolStripMenuItem";
            this.checkAgainstActiveDirectoryToolStripMenuItem.Click += new System.EventHandler(this.checkAgainstActiveDirectoryToolStripMenuItem_Click);
            // 
            // editMaxCabFileToolStripMenuItem
            // 
            resources.ApplyResources(this.editMaxCabFileToolStripMenuItem, "editMaxCabFileToolStripMenuItem");
            this.editMaxCabFileToolStripMenuItem.Name = "editMaxCabFileToolStripMenuItem";
            this.editMaxCabFileToolStripMenuItem.Click += new System.EventHandler(this.editMaxCabFileToolStripMenuItem_Click);
            // 
            // updatestoolStripMenuItem
            // 
            this.updatestoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUpdatetoolStripMenuItem,
            this.createCustomUpdateToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportAnUpdateToolStripMenuItem,
            this.importAnUpdateToolStripMenuItem,
            this.toolStripSeparator2,
            this.importFromCatalogtoolStripMenuItem,
            this.manageCatalogSubscriptionsToolStripMenuItem});
            this.updatestoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.updatestoolStripMenuItem.Name = "updatestoolStripMenuItem";
            resources.ApplyResources(this.updatestoolStripMenuItem, "updatestoolStripMenuItem");
            this.updatestoolStripMenuItem.DropDownClosed += new System.EventHandler(this.toolStripMenuItem_DropDownClosed);
            this.updatestoolStripMenuItem.DropDownOpened += new System.EventHandler(this.toolStripMenuItem_DropDownOpened);
            // 
            // createUpdatetoolStripMenuItem
            // 
            resources.ApplyResources(this.createUpdatetoolStripMenuItem, "createUpdatetoolStripMenuItem");
            this.createUpdatetoolStripMenuItem.Name = "createUpdatetoolStripMenuItem";
            this.createUpdatetoolStripMenuItem.Click += new System.EventHandler(this.createUpdatetoolStripMenuItem_Click);
            // 
            // createCustomUpdateToolStripMenuItem
            // 
            resources.ApplyResources(this.createCustomUpdateToolStripMenuItem, "createCustomUpdateToolStripMenuItem");
            this.createCustomUpdateToolStripMenuItem.Name = "createCustomUpdateToolStripMenuItem";
            this.createCustomUpdateToolStripMenuItem.Click += new System.EventHandler(this.createCustomUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exportAnUpdateToolStripMenuItem
            // 
            resources.ApplyResources(this.exportAnUpdateToolStripMenuItem, "exportAnUpdateToolStripMenuItem");
            this.exportAnUpdateToolStripMenuItem.Name = "exportAnUpdateToolStripMenuItem";
            this.exportAnUpdateToolStripMenuItem.Click += new System.EventHandler(this.exportAnUpdateToolStripMenuItem_Click);
            // 
            // importAnUpdateToolStripMenuItem
            // 
            resources.ApplyResources(this.importAnUpdateToolStripMenuItem, "importAnUpdateToolStripMenuItem");
            this.importAnUpdateToolStripMenuItem.Name = "importAnUpdateToolStripMenuItem";
            this.importAnUpdateToolStripMenuItem.Click += new System.EventHandler(this.importAnUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // importFromCatalogtoolStripMenuItem
            // 
            resources.ApplyResources(this.importFromCatalogtoolStripMenuItem, "importFromCatalogtoolStripMenuItem");
            this.importFromCatalogtoolStripMenuItem.Name = "importFromCatalogtoolStripMenuItem";
            this.importFromCatalogtoolStripMenuItem.Click += new System.EventHandler(this.importFromCatalogtoolStripMenuItem_Click);
            // 
            // manageCatalogSubscriptionsToolStripMenuItem
            // 
            resources.ApplyResources(this.manageCatalogSubscriptionsToolStripMenuItem, "manageCatalogSubscriptionsToolStripMenuItem");
            this.manageCatalogSubscriptionsToolStripMenuItem.Name = "manageCatalogSubscriptionsToolStripMenuItem";
            this.manageCatalogSubscriptionsToolStripMenuItem.Click += new System.EventHandler(this.manageCatalogSubscriptionsToolStripMenuItem_Click);
            // 
            // helptoolStripMenuItem
            // 
            this.helptoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abouttoolStripMenuItem,
            this.checkForUpdateToolStripMenuItem,
            this.sendDebugInfoToolStripMenuItem});
            this.helptoolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helptoolStripMenuItem.Name = "helptoolStripMenuItem";
            resources.ApplyResources(this.helptoolStripMenuItem, "helptoolStripMenuItem");
            this.helptoolStripMenuItem.DropDownClosed += new System.EventHandler(this.toolStripMenuItem_DropDownClosed);
            this.helptoolStripMenuItem.DropDownOpened += new System.EventHandler(this.toolStripMenuItem_DropDownOpened);
            // 
            // abouttoolStripMenuItem
            // 
            this.abouttoolStripMenuItem.Image = global::Wsus_Package_Publisher.Properties.Resources.Help_48;
            this.abouttoolStripMenuItem.Name = "abouttoolStripMenuItem";
            resources.ApplyResources(this.abouttoolStripMenuItem, "abouttoolStripMenuItem");
            this.abouttoolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdateToolStripMenuItem, "checkForUpdateToolStripMenuItem");
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // sendDebugInfoToolStripMenuItem
            // 
            this.sendDebugInfoToolStripMenuItem.Name = "sendDebugInfoToolStripMenuItem";
            resources.ApplyResources(this.sendDebugInfoToolStripMenuItem, "sendDebugInfoToolStripMenuItem");
            this.sendDebugInfoToolStripMenuItem.Click += new System.EventHandler(this.sendDebugInfoToolStripMenuItem_Click);
            // 
            // cmbBxServerList
            // 
            this.cmbBxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxServerList.Name = "cmbBxServerList";
            resources.ApplyResources(this.cmbBxServerList, "cmbBxServerList");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // ctxMnuTreeview
            // 
            this.ctxMnuTreeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createMetaGroup,
            this.editMetaGroup,
            this.deleteMetaGroup});
            this.ctxMnuTreeview.Name = "ctxMnuTreeview";
            this.ctxMnuTreeview.ShowImageMargin = false;
            resources.ApplyResources(this.ctxMnuTreeview, "ctxMnuTreeview");
            this.ctxMnuTreeview.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuTreeview_ItemClicked);
            // 
            // createMetaGroup
            // 
            this.createMetaGroup.Name = "createMetaGroup";
            resources.ApplyResources(this.createMetaGroup, "createMetaGroup");
            // 
            // editMetaGroup
            // 
            this.editMetaGroup.Name = "editMetaGroup";
            resources.ApplyResources(this.editMetaGroup, "editMetaGroup");
            // 
            // deleteMetaGroup
            // 
            this.deleteMetaGroup.Name = "deleteMetaGroup";
            resources.ApplyResources(this.deleteMetaGroup, "deleteMetaGroup");
            // 
            // FrmWsusPackagePublisher
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mnuMainForm);
            this.MainMenuStrip = this.mnuMainForm;
            this.Name = "FrmWsusPackagePublisher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWsusPackagePublisher_FormClosing);
            this.Shown += new System.EventHandler(this.FrmWsusPackagePublisher_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mnuMainForm.ResumeLayout(false);
            this.mnuMainForm.PerformLayout();
            this.ctxMnuTreeview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView trvWsus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip mnuMainForm;
        private System.Windows.Forms.ToolStripMenuItem filetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quittoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificatetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languagetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingstoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msiReadertoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updatestoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUpdatetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helptoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abouttoolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmbBxServerList;
        private System.Windows.Forms.ImageList imgLstServer;
        private System.Windows.Forms.ContextMenuStrip ctxMnuTreeview;
        private System.Windows.Forms.ToolStripMenuItem createMetaGroup;
        private System.Windows.Forms.ToolStripMenuItem editMetaGroup;
        private System.Windows.Forms.ToolStripMenuItem deleteMetaGroup;
        private System.Windows.Forms.ToolStripMenuItem checkAgainstActiveDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createCustomUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendDebugInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exportAnUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAnUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem importFromCatalogtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageCatalogSubscriptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spanishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem italianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugueseBrazilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMaxCabFileToolStripMenuItem;
    }
}