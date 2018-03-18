namespace CustomUpdateCreator
{
    partial class frmCustomUpdateCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomUpdateCreator));
            this.ribbon1 = new System.Windows.Forms.Ribbon();
            this.ribTabMsi = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel15 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnUninstallMsiByGuid = new System.Windows.Forms.RibbonButton();
            this.ribBtnUinstallMsiByName = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel16 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnInstallMsi = new System.Windows.Forms.RibbonButton();
            this.rbTabFiles = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnCreateTextFile = new System.Windows.Forms.RibbonButton();
            this.ribBtnDeleteFile = new System.Windows.Forms.RibbonButton();
            this.ribBtnRenameFile = new System.Windows.Forms.RibbonButton();
            this.ribBtnCopyFile = new System.Windows.Forms.RibbonButton();
            this.rbTabFolders = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel2 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnCreateFolder = new System.Windows.Forms.RibbonButton();
            this.ribBtnDeleteFolder = new System.Windows.Forms.RibbonButton();
            this.ribBtnRenameFolder = new System.Windows.Forms.RibbonButton();
            this.rbTabExecute = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel3 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnRunFile = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel4 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnRunVbScript = new System.Windows.Forms.RibbonButton();
            this.ribBtnRunPowershellScript = new System.Windows.Forms.RibbonButton();
            this.rbTabServices = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel5 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnStartService = new System.Windows.Forms.RibbonButton();
            this.ribBtnStopService = new System.Windows.Forms.RibbonButton();
            this.ribBtnUnregisterService = new System.Windows.Forms.RibbonButton();
            this.ribBtnServiceStartingMode = new System.Windows.Forms.RibbonButton();
            this.rbTabRegistry = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel6 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnAddRegKey = new System.Windows.Forms.RibbonButton();
            this.ribBtnDeleteRegKey = new System.Windows.Forms.RibbonButton();
            this.ribBtnRenameRegKey = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel7 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnAddRegValue = new System.Windows.Forms.RibbonButton();
            this.ribBtnDeleteRegValue = new System.Windows.Forms.RibbonButton();
            this.ribBtnRenameRegValue = new System.Windows.Forms.RibbonButton();
            this.ribPanRegData = new System.Windows.Forms.RibbonPanel();
            this.ribBtnChangeData = new System.Windows.Forms.RibbonButton();
            this.ribPanRegFile = new System.Windows.Forms.RibbonPanel();
            this.ribBtnMergeRegFile = new System.Windows.Forms.RibbonButton();
            this.rbTabPower = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel8 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnShutdown = new System.Windows.Forms.RibbonButton();
            this.ribBtnReboot = new System.Windows.Forms.RibbonButton();
            this.rbTabProcesses = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel9 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnKillProcess = new System.Windows.Forms.RibbonButton();
            this.rbTabDll = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel12 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnRegisterDll = new System.Windows.Forms.RibbonButton();
            this.ribBtnUnregisterDll = new System.Windows.Forms.RibbonButton();
            this.rbTabShortcuts = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel13 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnCreateShortcut = new System.Windows.Forms.RibbonButton();
            this.rbTabMisc = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel10 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnWait = new System.Windows.Forms.RibbonButton();
            this.ribbonPanel14 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnDeleteTask = new System.Windows.Forms.RibbonButton();
            this.rbTabTemplates = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel11 = new System.Windows.Forms.RibbonPanel();
            this.ribBtnLoadTemplate = new System.Windows.Forms.RibbonButton();
            this.ribBtnSaveTemplate = new System.Windows.Forms.RibbonButton();
            this.ribBtnSaveAsTemplate = new System.Windows.Forms.RibbonButton();
            this.tlpCustomActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nupStaticCode = new System.Windows.Forms.NumericUpDown();
            this.rdBtnReturnStaticCode = new System.Windows.Forms.RadioButton();
            this.rdBtnReturnVariable = new System.Windows.Forms.RadioButton();
            this.ctxMnuRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssSaved = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupStaticCode)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbon1
            // 
            resources.ApplyResources(this.ribbon1, "ribbon1");
            this.ribbon1.CaptionBarVisible = false;
            this.ribbon1.Minimized = false;
            this.ribbon1.Name = "ribbon1";
            // 
            // 
            // 
            this.ribbon1.OrbDropDown.AccessibleDescription = resources.GetString("ribbon1.OrbDropDown.AccessibleDescription");
            this.ribbon1.OrbDropDown.AccessibleName = resources.GetString("ribbon1.OrbDropDown.AccessibleName");
            this.ribbon1.OrbDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("ribbon1.OrbDropDown.Anchor")));
            this.ribbon1.OrbDropDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ribbon1.OrbDropDown.BackgroundImage")));
            this.ribbon1.OrbDropDown.BackgroundImageLayout = ((System.Windows.Forms.ImageLayout)(resources.GetObject("ribbon1.OrbDropDown.BackgroundImageLayout")));
            this.ribbon1.OrbDropDown.BorderRoundness = 8;
            this.ribbon1.OrbDropDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("ribbon1.OrbDropDown.Dock")));
            this.ribbon1.OrbDropDown.Font = ((System.Drawing.Font)(resources.GetObject("ribbon1.OrbDropDown.Font")));
            this.ribbon1.OrbDropDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("ribbon1.OrbDropDown.ImeMode")));
            this.ribbon1.OrbDropDown.Location = ((System.Drawing.Point)(resources.GetObject("ribbon1.OrbDropDown.Location")));
            this.ribbon1.OrbDropDown.MaximumSize = ((System.Drawing.Size)(resources.GetObject("ribbon1.OrbDropDown.MaximumSize")));
            this.ribbon1.OrbDropDown.Name = "";
            this.ribbon1.OrbDropDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("ribbon1.OrbDropDown.RightToLeft")));
            this.ribbon1.OrbDropDown.Size = ((System.Drawing.Size)(resources.GetObject("ribbon1.OrbDropDown.Size")));
            this.ribbon1.OrbDropDown.TabIndex = ((int)(resources.GetObject("ribbon1.OrbDropDown.TabIndex")));
            this.ribbon1.OrbImage = null;
            // 
            // 
            // 
            this.ribbon1.QuickAcessToolbar.Text = resources.GetString("ribbon1.QuickAcessToolbar.Text");
            this.ribbon1.QuickAcessToolbar.ToolTip = resources.GetString("ribbon1.QuickAcessToolbar.ToolTip");
            this.ribbon1.QuickAcessToolbar.ToolTipImage = ((System.Drawing.Image)(resources.GetObject("ribbon1.QuickAcessToolbar.ToolTipImage")));
            this.ribbon1.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbon1.Tabs.Add(this.ribTabMsi);
            this.ribbon1.Tabs.Add(this.rbTabFiles);
            this.ribbon1.Tabs.Add(this.rbTabFolders);
            this.ribbon1.Tabs.Add(this.rbTabExecute);
            this.ribbon1.Tabs.Add(this.rbTabServices);
            this.ribbon1.Tabs.Add(this.rbTabRegistry);
            this.ribbon1.Tabs.Add(this.rbTabPower);
            this.ribbon1.Tabs.Add(this.rbTabProcesses);
            this.ribbon1.Tabs.Add(this.rbTabDll);
            this.ribbon1.Tabs.Add(this.rbTabShortcuts);
            this.ribbon1.Tabs.Add(this.rbTabMisc);
            this.ribbon1.Tabs.Add(this.rbTabTemplates);
            this.ribbon1.TabsMargin = new System.Windows.Forms.Padding(12, 2, 20, 0);
            this.ribbon1.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            // 
            // ribTabMsi
            // 
            this.ribTabMsi.Panels.Add(this.ribbonPanel15);
            this.ribTabMsi.Panels.Add(this.ribbonPanel16);
            resources.ApplyResources(this.ribTabMsi, "ribTabMsi");
            // 
            // ribbonPanel15
            // 
            this.ribbonPanel15.ButtonMoreEnabled = false;
            this.ribbonPanel15.ButtonMoreVisible = false;
            this.ribbonPanel15.Items.Add(this.ribBtnUninstallMsiByGuid);
            this.ribbonPanel15.Items.Add(this.ribBtnUinstallMsiByName);
            resources.ApplyResources(this.ribbonPanel15, "ribbonPanel15");
            // 
            // ribBtnUninstallMsiByGuid
            // 
            this.ribBtnUninstallMsiByGuid.Image = global::CustomUpdateCreator.Properties.Resources.MsiUninstall48x48;
            this.ribBtnUninstallMsiByGuid.SmallImage = global::CustomUpdateCreator.Properties.Resources.MsiUninstall32x32;
            this.ribBtnUninstallMsiByGuid.Tag = "UninstallMsiProductByGuidAction";
            resources.ApplyResources(this.ribBtnUninstallMsiByGuid, "ribBtnUninstallMsiByGuid");
            this.ribBtnUninstallMsiByGuid.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnUinstallMsiByName
            // 
            this.ribBtnUinstallMsiByName.Image = global::CustomUpdateCreator.Properties.Resources.MsiUninstall48x48;
            this.ribBtnUinstallMsiByName.SmallImage = global::CustomUpdateCreator.Properties.Resources.MsiUninstall32x32;
            this.ribBtnUinstallMsiByName.Tag = "UninstallMsiProductByNameAction";
            resources.ApplyResources(this.ribBtnUinstallMsiByName, "ribBtnUinstallMsiByName");
            this.ribBtnUinstallMsiByName.Click += new System.EventHandler(this.AddAction);
            // 
            // ribbonPanel16
            // 
            this.ribbonPanel16.ButtonMoreEnabled = false;
            this.ribbonPanel16.ButtonMoreVisible = false;
            this.ribbonPanel16.Items.Add(this.ribBtnInstallMsi);
            resources.ApplyResources(this.ribbonPanel16, "ribbonPanel16");
            // 
            // ribBtnInstallMsi
            // 
            this.ribBtnInstallMsi.Image = global::CustomUpdateCreator.Properties.Resources.InstallMsi_48x48;
            this.ribBtnInstallMsi.SmallImage = global::CustomUpdateCreator.Properties.Resources.InstallMsi_32x32;
            this.ribBtnInstallMsi.Tag = "InstallMsiAction";
            resources.ApplyResources(this.ribBtnInstallMsi, "ribBtnInstallMsi");
            this.ribBtnInstallMsi.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabFiles
            // 
            this.rbTabFiles.Panels.Add(this.ribbonPanel1);
            resources.ApplyResources(this.rbTabFiles, "rbTabFiles");
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreEnabled = false;
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.ribBtnCreateTextFile);
            this.ribbonPanel1.Items.Add(this.ribBtnDeleteFile);
            this.ribbonPanel1.Items.Add(this.ribBtnRenameFile);
            this.ribbonPanel1.Items.Add(this.ribBtnCopyFile);
            resources.ApplyResources(this.ribbonPanel1, "ribbonPanel1");
            // 
            // ribBtnCreateTextFile
            // 
            this.ribBtnCreateTextFile.Image = global::CustomUpdateCreator.Properties.Resources.TextFileAction48x48;
            this.ribBtnCreateTextFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.TextFileAction32x32;
            this.ribBtnCreateTextFile.Tag = "CreateTextFileAction";
            resources.ApplyResources(this.ribBtnCreateTextFile, "ribBtnCreateTextFile");
            this.ribBtnCreateTextFile.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnDeleteFile
            // 
            this.ribBtnDeleteFile.Image = global::CustomUpdateCreator.Properties.Resources.DeleteFile48x48;
            this.ribBtnDeleteFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.DeleteFile32x32;
            this.ribBtnDeleteFile.Tag = "DeleteFileAction";
            resources.ApplyResources(this.ribBtnDeleteFile, "ribBtnDeleteFile");
            this.ribBtnDeleteFile.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnRenameFile
            // 
            this.ribBtnRenameFile.Image = global::CustomUpdateCreator.Properties.Resources.RenameFile48x48;
            this.ribBtnRenameFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.RenameFile32x32;
            this.ribBtnRenameFile.Tag = "RenameFileAction";
            resources.ApplyResources(this.ribBtnRenameFile, "ribBtnRenameFile");
            this.ribBtnRenameFile.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnCopyFile
            // 
            this.ribBtnCopyFile.Image = global::CustomUpdateCreator.Properties.Resources.CopyFile48x48;
            this.ribBtnCopyFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.CopyFile32x32;
            this.ribBtnCopyFile.Tag = "CopyFileAction";
            resources.ApplyResources(this.ribBtnCopyFile, "ribBtnCopyFile");
            this.ribBtnCopyFile.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabFolders
            // 
            this.rbTabFolders.Panels.Add(this.ribbonPanel2);
            resources.ApplyResources(this.rbTabFolders, "rbTabFolders");
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ButtonMoreEnabled = false;
            this.ribbonPanel2.ButtonMoreVisible = false;
            this.ribbonPanel2.Items.Add(this.ribBtnCreateFolder);
            this.ribbonPanel2.Items.Add(this.ribBtnDeleteFolder);
            this.ribbonPanel2.Items.Add(this.ribBtnRenameFolder);
            resources.ApplyResources(this.ribbonPanel2, "ribbonPanel2");
            // 
            // ribBtnCreateFolder
            // 
            this.ribBtnCreateFolder.Image = ((System.Drawing.Image)(resources.GetObject("ribBtnCreateFolder.Image")));
            this.ribBtnCreateFolder.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribBtnCreateFolder.SmallImage")));
            this.ribBtnCreateFolder.Tag = "CreateFolderAction";
            resources.ApplyResources(this.ribBtnCreateFolder, "ribBtnCreateFolder");
            this.ribBtnCreateFolder.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnDeleteFolder
            // 
            this.ribBtnDeleteFolder.Image = global::CustomUpdateCreator.Properties.Resources.DeleteFolder48x48;
            this.ribBtnDeleteFolder.SmallImage = global::CustomUpdateCreator.Properties.Resources.DeleteFolder32x32;
            this.ribBtnDeleteFolder.Tag = "DeleteFolderAction";
            resources.ApplyResources(this.ribBtnDeleteFolder, "ribBtnDeleteFolder");
            this.ribBtnDeleteFolder.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnRenameFolder
            // 
            this.ribBtnRenameFolder.Image = global::CustomUpdateCreator.Properties.Resources.RenameFolder48x48;
            this.ribBtnRenameFolder.SmallImage = global::CustomUpdateCreator.Properties.Resources.RenameFolder32x32;
            this.ribBtnRenameFolder.Tag = "RenameFolderAction";
            resources.ApplyResources(this.ribBtnRenameFolder, "ribBtnRenameFolder");
            this.ribBtnRenameFolder.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabExecute
            // 
            this.rbTabExecute.Panels.Add(this.ribbonPanel3);
            this.rbTabExecute.Panels.Add(this.ribbonPanel4);
            resources.ApplyResources(this.rbTabExecute, "rbTabExecute");
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ButtonMoreEnabled = false;
            this.ribbonPanel3.ButtonMoreVisible = false;
            this.ribbonPanel3.Items.Add(this.ribBtnRunFile);
            resources.ApplyResources(this.ribbonPanel3, "ribbonPanel3");
            // 
            // ribBtnRunFile
            // 
            this.ribBtnRunFile.Image = global::CustomUpdateCreator.Properties.Resources.ExecutableAction48x48;
            this.ribBtnRunFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.ExecutableAction32x32;
            this.ribBtnRunFile.Tag = "ExecutableAction";
            resources.ApplyResources(this.ribBtnRunFile, "ribBtnRunFile");
            this.ribBtnRunFile.Click += new System.EventHandler(this.AddAction);
            // 
            // ribbonPanel4
            // 
            this.ribbonPanel4.ButtonMoreEnabled = false;
            this.ribbonPanel4.ButtonMoreVisible = false;
            this.ribbonPanel4.Items.Add(this.ribBtnRunVbScript);
            this.ribbonPanel4.Items.Add(this.ribBtnRunPowershellScript);
            resources.ApplyResources(this.ribbonPanel4, "ribbonPanel4");
            // 
            // ribBtnRunVbScript
            // 
            this.ribBtnRunVbScript.Image = global::CustomUpdateCreator.Properties.Resources.ScriptAction48x48;
            this.ribBtnRunVbScript.SmallImage = global::CustomUpdateCreator.Properties.Resources.ScriptAction32x32;
            this.ribBtnRunVbScript.Tag = "RunVbScriptAction";
            resources.ApplyResources(this.ribBtnRunVbScript, "ribBtnRunVbScript");
            this.ribBtnRunVbScript.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnRunPowershellScript
            // 
            this.ribBtnRunPowershellScript.Image = ((System.Drawing.Image)(resources.GetObject("ribBtnRunPowershellScript.Image")));
            this.ribBtnRunPowershellScript.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribBtnRunPowershellScript.SmallImage")));
            this.ribBtnRunPowershellScript.Tag = "RunPowershellScriptAction";
            resources.ApplyResources(this.ribBtnRunPowershellScript, "ribBtnRunPowershellScript");
            this.ribBtnRunPowershellScript.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabServices
            // 
            this.rbTabServices.Panels.Add(this.ribbonPanel5);
            resources.ApplyResources(this.rbTabServices, "rbTabServices");
            // 
            // ribbonPanel5
            // 
            this.ribbonPanel5.ButtonMoreEnabled = false;
            this.ribbonPanel5.ButtonMoreVisible = false;
            this.ribbonPanel5.Items.Add(this.ribBtnStartService);
            this.ribbonPanel5.Items.Add(this.ribBtnStopService);
            this.ribbonPanel5.Items.Add(this.ribBtnUnregisterService);
            this.ribbonPanel5.Items.Add(this.ribBtnServiceStartingMode);
            resources.ApplyResources(this.ribbonPanel5, "ribbonPanel5");
            // 
            // ribBtnStartService
            // 
            this.ribBtnStartService.Image = global::CustomUpdateCreator.Properties.Resources.StartServices48x48;
            this.ribBtnStartService.SmallImage = global::CustomUpdateCreator.Properties.Resources.StartServices32x32;
            this.ribBtnStartService.Tag = "StartServiceAction";
            resources.ApplyResources(this.ribBtnStartService, "ribBtnStartService");
            this.ribBtnStartService.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnStopService
            // 
            this.ribBtnStopService.Image = global::CustomUpdateCreator.Properties.Resources.StopServices48x48;
            this.ribBtnStopService.SmallImage = global::CustomUpdateCreator.Properties.Resources.StopServices32x32;
            this.ribBtnStopService.Tag = "StopServiceAction";
            resources.ApplyResources(this.ribBtnStopService, "ribBtnStopService");
            this.ribBtnStopService.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnUnregisterService
            // 
            this.ribBtnUnregisterService.Image = global::CustomUpdateCreator.Properties.Resources.UnregisterServices48x48;
            this.ribBtnUnregisterService.SmallImage = global::CustomUpdateCreator.Properties.Resources.UnregisterServices32x32;
            this.ribBtnUnregisterService.Tag = "UnregisterServiceAction";
            resources.ApplyResources(this.ribBtnUnregisterService, "ribBtnUnregisterService");
            this.ribBtnUnregisterService.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnServiceStartingMode
            // 
            this.ribBtnServiceStartingMode.Image = global::CustomUpdateCreator.Properties.Resources.ChangeService48x48;
            this.ribBtnServiceStartingMode.SmallImage = global::CustomUpdateCreator.Properties.Resources.ChangeService32x32;
            this.ribBtnServiceStartingMode.Tag = "ChangeServiceAction";
            resources.ApplyResources(this.ribBtnServiceStartingMode, "ribBtnServiceStartingMode");
            this.ribBtnServiceStartingMode.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabRegistry
            // 
            this.rbTabRegistry.Panels.Add(this.ribbonPanel6);
            this.rbTabRegistry.Panels.Add(this.ribbonPanel7);
            this.rbTabRegistry.Panels.Add(this.ribPanRegData);
            this.rbTabRegistry.Panels.Add(this.ribPanRegFile);
            resources.ApplyResources(this.rbTabRegistry, "rbTabRegistry");
            // 
            // ribbonPanel6
            // 
            this.ribbonPanel6.ButtonMoreEnabled = false;
            this.ribbonPanel6.ButtonMoreVisible = false;
            this.ribbonPanel6.Items.Add(this.ribBtnAddRegKey);
            this.ribbonPanel6.Items.Add(this.ribBtnDeleteRegKey);
            this.ribbonPanel6.Items.Add(this.ribBtnRenameRegKey);
            resources.ApplyResources(this.ribbonPanel6, "ribbonPanel6");
            // 
            // ribBtnAddRegKey
            // 
            this.ribBtnAddRegKey.Image = global::CustomUpdateCreator.Properties.Resources.AddRegistryKey48x48;
            this.ribBtnAddRegKey.SmallImage = global::CustomUpdateCreator.Properties.Resources.AddRegistryKey32x32;
            this.ribBtnAddRegKey.Tag = "AddRegKeyAction";
            resources.ApplyResources(this.ribBtnAddRegKey, "ribBtnAddRegKey");
            this.ribBtnAddRegKey.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnDeleteRegKey
            // 
            this.ribBtnDeleteRegKey.Image = global::CustomUpdateCreator.Properties.Resources.DeleteRegistryKey48x48;
            this.ribBtnDeleteRegKey.SmallImage = global::CustomUpdateCreator.Properties.Resources.DeleteRegistryKey32x32;
            this.ribBtnDeleteRegKey.Tag = "DeleteRegKeyAction";
            resources.ApplyResources(this.ribBtnDeleteRegKey, "ribBtnDeleteRegKey");
            this.ribBtnDeleteRegKey.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnRenameRegKey
            // 
            this.ribBtnRenameRegKey.Image = global::CustomUpdateCreator.Properties.Resources.RenameRegistryKey48x48;
            this.ribBtnRenameRegKey.SmallImage = global::CustomUpdateCreator.Properties.Resources.RenameRegistryKey32x32;
            this.ribBtnRenameRegKey.Tag = "RenameRegKeyAction";
            resources.ApplyResources(this.ribBtnRenameRegKey, "ribBtnRenameRegKey");
            this.ribBtnRenameRegKey.Click += new System.EventHandler(this.AddAction);
            // 
            // ribbonPanel7
            // 
            this.ribbonPanel7.ButtonMoreEnabled = false;
            this.ribbonPanel7.ButtonMoreVisible = false;
            this.ribbonPanel7.Items.Add(this.ribBtnAddRegValue);
            this.ribbonPanel7.Items.Add(this.ribBtnDeleteRegValue);
            this.ribbonPanel7.Items.Add(this.ribBtnRenameRegValue);
            resources.ApplyResources(this.ribbonPanel7, "ribbonPanel7");
            // 
            // ribBtnAddRegValue
            // 
            this.ribBtnAddRegValue.Image = ((System.Drawing.Image)(resources.GetObject("ribBtnAddRegValue.Image")));
            this.ribBtnAddRegValue.SmallImage = ((System.Drawing.Image)(resources.GetObject("ribBtnAddRegValue.SmallImage")));
            this.ribBtnAddRegValue.Tag = "AddRegValueAction";
            resources.ApplyResources(this.ribBtnAddRegValue, "ribBtnAddRegValue");
            this.ribBtnAddRegValue.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnDeleteRegValue
            // 
            this.ribBtnDeleteRegValue.Image = global::CustomUpdateCreator.Properties.Resources.DeleteValue48x48;
            this.ribBtnDeleteRegValue.SmallImage = global::CustomUpdateCreator.Properties.Resources.DeleteValue32x32;
            this.ribBtnDeleteRegValue.Tag = "DeleteRegValueAction";
            resources.ApplyResources(this.ribBtnDeleteRegValue, "ribBtnDeleteRegValue");
            this.ribBtnDeleteRegValue.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnRenameRegValue
            // 
            this.ribBtnRenameRegValue.Image = global::CustomUpdateCreator.Properties.Resources.ChangeValue48x48;
            this.ribBtnRenameRegValue.SmallImage = global::CustomUpdateCreator.Properties.Resources.ChangeValue32x32;
            this.ribBtnRenameRegValue.Tag = "RenameRegValueAction";
            resources.ApplyResources(this.ribBtnRenameRegValue, "ribBtnRenameRegValue");
            this.ribBtnRenameRegValue.Click += new System.EventHandler(this.AddAction);
            // 
            // ribPanRegData
            // 
            this.ribPanRegData.ButtonMoreEnabled = false;
            this.ribPanRegData.ButtonMoreVisible = false;
            this.ribPanRegData.Items.Add(this.ribBtnChangeData);
            resources.ApplyResources(this.ribPanRegData, "ribPanRegData");
            // 
            // ribBtnChangeData
            // 
            this.ribBtnChangeData.Image = global::CustomUpdateCreator.Properties.Resources.ChangeRegData48x48;
            this.ribBtnChangeData.SmallImage = global::CustomUpdateCreator.Properties.Resources.ChangeRegData32x32;
            this.ribBtnChangeData.Tag = "ChangeRegDataAction";
            resources.ApplyResources(this.ribBtnChangeData, "ribBtnChangeData");
            this.ribBtnChangeData.Click += new System.EventHandler(this.AddAction);
            // 
            // ribPanRegFile
            // 
            this.ribPanRegFile.ButtonMoreEnabled = false;
            this.ribPanRegFile.ButtonMoreVisible = false;
            this.ribPanRegFile.Items.Add(this.ribBtnMergeRegFile);
            resources.ApplyResources(this.ribPanRegFile, "ribPanRegFile");
            // 
            // ribBtnMergeRegFile
            // 
            this.ribBtnMergeRegFile.Image = global::CustomUpdateCreator.Properties.Resources.MergeRegFile48x48;
            this.ribBtnMergeRegFile.SmallImage = global::CustomUpdateCreator.Properties.Resources.MergeRegFile32x32;
            this.ribBtnMergeRegFile.Tag = "ImportRegFileAction";
            resources.ApplyResources(this.ribBtnMergeRegFile, "ribBtnMergeRegFile");
            this.ribBtnMergeRegFile.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabPower
            // 
            this.rbTabPower.Panels.Add(this.ribbonPanel8);
            resources.ApplyResources(this.rbTabPower, "rbTabPower");
            // 
            // ribbonPanel8
            // 
            this.ribbonPanel8.ButtonMoreEnabled = false;
            this.ribbonPanel8.ButtonMoreVisible = false;
            this.ribbonPanel8.Items.Add(this.ribBtnShutdown);
            this.ribbonPanel8.Items.Add(this.ribBtnReboot);
            resources.ApplyResources(this.ribbonPanel8, "ribbonPanel8");
            // 
            // ribBtnShutdown
            // 
            this.ribBtnShutdown.Image = global::CustomUpdateCreator.Properties.Resources.Shutdown48x48;
            this.ribBtnShutdown.SmallImage = global::CustomUpdateCreator.Properties.Resources.Shutdown32x32;
            this.ribBtnShutdown.Tag = "ShutdownAction";
            resources.ApplyResources(this.ribBtnShutdown, "ribBtnShutdown");
            this.ribBtnShutdown.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnReboot
            // 
            this.ribBtnReboot.Image = global::CustomUpdateCreator.Properties.Resources.Reboot48x48;
            this.ribBtnReboot.SmallImage = global::CustomUpdateCreator.Properties.Resources.Reboot32x32;
            this.ribBtnReboot.Tag = "RebootAction";
            resources.ApplyResources(this.ribBtnReboot, "ribBtnReboot");
            this.ribBtnReboot.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabProcesses
            // 
            this.rbTabProcesses.Panels.Add(this.ribbonPanel9);
            resources.ApplyResources(this.rbTabProcesses, "rbTabProcesses");
            // 
            // ribbonPanel9
            // 
            this.ribbonPanel9.ButtonMoreEnabled = false;
            this.ribbonPanel9.ButtonMoreVisible = false;
            this.ribbonPanel9.Items.Add(this.ribBtnKillProcess);
            resources.ApplyResources(this.ribbonPanel9, "ribbonPanel9");
            // 
            // ribBtnKillProcess
            // 
            this.ribBtnKillProcess.Image = global::CustomUpdateCreator.Properties.Resources.KillProcess48x48;
            this.ribBtnKillProcess.SmallImage = global::CustomUpdateCreator.Properties.Resources.KillProcess32x32;
            this.ribBtnKillProcess.Tag = "KillProcessAction";
            resources.ApplyResources(this.ribBtnKillProcess, "ribBtnKillProcess");
            this.ribBtnKillProcess.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabDll
            // 
            this.rbTabDll.Panels.Add(this.ribbonPanel12);
            resources.ApplyResources(this.rbTabDll, "rbTabDll");
            // 
            // ribbonPanel12
            // 
            this.ribbonPanel12.ButtonMoreEnabled = false;
            this.ribbonPanel12.ButtonMoreVisible = false;
            this.ribbonPanel12.Items.Add(this.ribBtnRegisterDll);
            this.ribbonPanel12.Items.Add(this.ribBtnUnregisterDll);
            resources.ApplyResources(this.ribbonPanel12, "ribbonPanel12");
            // 
            // ribBtnRegisterDll
            // 
            this.ribBtnRegisterDll.Image = global::CustomUpdateCreator.Properties.Resources.RegisterDLL48x48;
            this.ribBtnRegisterDll.SmallImage = global::CustomUpdateCreator.Properties.Resources.RegisterDLL32x32;
            this.ribBtnRegisterDll.Tag = "RegisterDLLAction";
            resources.ApplyResources(this.ribBtnRegisterDll, "ribBtnRegisterDll");
            this.ribBtnRegisterDll.Click += new System.EventHandler(this.AddAction);
            // 
            // ribBtnUnregisterDll
            // 
            this.ribBtnUnregisterDll.Image = global::CustomUpdateCreator.Properties.Resources.UnRegisterDLL48x48;
            this.ribBtnUnregisterDll.SmallImage = global::CustomUpdateCreator.Properties.Resources.UnRegisterDLL32x32;
            this.ribBtnUnregisterDll.Tag = "UnregisterDLLAction";
            resources.ApplyResources(this.ribBtnUnregisterDll, "ribBtnUnregisterDll");
            this.ribBtnUnregisterDll.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabShortcuts
            // 
            this.rbTabShortcuts.Panels.Add(this.ribbonPanel13);
            this.rbTabShortcuts.Tag = "CreateShortcutAction";
            resources.ApplyResources(this.rbTabShortcuts, "rbTabShortcuts");
            // 
            // ribbonPanel13
            // 
            this.ribbonPanel13.ButtonMoreEnabled = false;
            this.ribbonPanel13.ButtonMoreVisible = false;
            this.ribbonPanel13.Items.Add(this.ribBtnCreateShortcut);
            resources.ApplyResources(this.ribbonPanel13, "ribbonPanel13");
            // 
            // ribBtnCreateShortcut
            // 
            this.ribBtnCreateShortcut.Image = global::CustomUpdateCreator.Properties.Resources.CreateShortcut48x48;
            this.ribBtnCreateShortcut.SmallImage = global::CustomUpdateCreator.Properties.Resources.CreateShortcut32x32;
            this.ribBtnCreateShortcut.Tag = "CreateShortcutAction";
            resources.ApplyResources(this.ribBtnCreateShortcut, "ribBtnCreateShortcut");
            this.ribBtnCreateShortcut.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabMisc
            // 
            this.rbTabMisc.Panels.Add(this.ribbonPanel10);
            this.rbTabMisc.Panels.Add(this.ribbonPanel14);
            resources.ApplyResources(this.rbTabMisc, "rbTabMisc");
            // 
            // ribbonPanel10
            // 
            this.ribbonPanel10.ButtonMoreEnabled = false;
            this.ribbonPanel10.ButtonMoreVisible = false;
            this.ribbonPanel10.Items.Add(this.ribBtnWait);
            resources.ApplyResources(this.ribbonPanel10, "ribbonPanel10");
            // 
            // ribBtnWait
            // 
            this.ribBtnWait.Image = global::CustomUpdateCreator.Properties.Resources.Wait48x48;
            this.ribBtnWait.SmallImage = global::CustomUpdateCreator.Properties.Resources.Wait32x32;
            this.ribBtnWait.Tag = "WaitAction";
            resources.ApplyResources(this.ribBtnWait, "ribBtnWait");
            this.ribBtnWait.Click += new System.EventHandler(this.AddAction);
            // 
            // ribbonPanel14
            // 
            this.ribbonPanel14.ButtonMoreEnabled = false;
            this.ribbonPanel14.ButtonMoreVisible = false;
            this.ribbonPanel14.Items.Add(this.ribBtnDeleteTask);
            resources.ApplyResources(this.ribbonPanel14, "ribbonPanel14");
            // 
            // ribBtnDeleteTask
            // 
            this.ribBtnDeleteTask.Image = global::CustomUpdateCreator.Properties.Resources.Scheduled48x48;
            this.ribBtnDeleteTask.SmallImage = global::CustomUpdateCreator.Properties.Resources.Scheduled32x32;
            this.ribBtnDeleteTask.Tag = "DeleteTaskAction";
            resources.ApplyResources(this.ribBtnDeleteTask, "ribBtnDeleteTask");
            this.ribBtnDeleteTask.Click += new System.EventHandler(this.AddAction);
            // 
            // rbTabTemplates
            // 
            this.rbTabTemplates.Panels.Add(this.ribbonPanel11);
            resources.ApplyResources(this.rbTabTemplates, "rbTabTemplates");
            // 
            // ribbonPanel11
            // 
            this.ribbonPanel11.ButtonMoreEnabled = false;
            this.ribbonPanel11.ButtonMoreVisible = false;
            this.ribbonPanel11.Items.Add(this.ribBtnLoadTemplate);
            this.ribbonPanel11.Items.Add(this.ribBtnSaveTemplate);
            this.ribbonPanel11.Items.Add(this.ribBtnSaveAsTemplate);
            resources.ApplyResources(this.ribbonPanel11, "ribbonPanel11");
            // 
            // ribBtnLoadTemplate
            // 
            this.ribBtnLoadTemplate.Image = global::CustomUpdateCreator.Properties.Resources.Open48x48;
            this.ribBtnLoadTemplate.SmallImage = global::CustomUpdateCreator.Properties.Resources.Open32x32;
            resources.ApplyResources(this.ribBtnLoadTemplate, "ribBtnLoadTemplate");
            this.ribBtnLoadTemplate.Click += new System.EventHandler(this.ribBtnLoadTemplate_Click);
            // 
            // ribBtnSaveTemplate
            // 
            this.ribBtnSaveTemplate.Image = global::CustomUpdateCreator.Properties.Resources.Save48x48;
            this.ribBtnSaveTemplate.SmallImage = global::CustomUpdateCreator.Properties.Resources.Save32x32;
            resources.ApplyResources(this.ribBtnSaveTemplate, "ribBtnSaveTemplate");
            this.ribBtnSaveTemplate.Click += new System.EventHandler(this.ribBtnSaveTemplate_Click);
            // 
            // ribBtnSaveAsTemplate
            // 
            this.ribBtnSaveAsTemplate.Image = global::CustomUpdateCreator.Properties.Resources.SaveAs48x48;
            this.ribBtnSaveAsTemplate.SmallImage = global::CustomUpdateCreator.Properties.Resources.SaveAs32x32;
            resources.ApplyResources(this.ribBtnSaveAsTemplate, "ribBtnSaveAsTemplate");
            this.ribBtnSaveAsTemplate.Click += new System.EventHandler(this.ribBtnSaveAsTemplate_Click);
            // 
            // tlpCustomActions
            // 
            resources.ApplyResources(this.tlpCustomActions, "tlpCustomActions");
            this.tlpCustomActions.AllowDrop = true;
            this.tlpCustomActions.Name = "tlpCustomActions";
            this.tlpCustomActions.DragDrop += new System.Windows.Forms.DragEventHandler(this.tlpCustomActions_DragDrop);
            this.tlpCustomActions.DragEnter += new System.Windows.Forms.DragEventHandler(this.tlpCustomActions_DragEnter);
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
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.nupStaticCode);
            this.groupBox1.Controls.Add(this.rdBtnReturnStaticCode);
            this.groupBox1.Controls.Add(this.rdBtnReturnVariable);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // nupStaticCode
            // 
            resources.ApplyResources(this.nupStaticCode, "nupStaticCode");
            this.nupStaticCode.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupStaticCode.Minimum = new decimal(new int[] {
            65535,
            0,
            0,
            -2147483648});
            this.nupStaticCode.Name = "nupStaticCode";
            // 
            // rdBtnReturnStaticCode
            // 
            resources.ApplyResources(this.rdBtnReturnStaticCode, "rdBtnReturnStaticCode");
            this.rdBtnReturnStaticCode.Checked = true;
            this.rdBtnReturnStaticCode.Name = "rdBtnReturnStaticCode";
            this.rdBtnReturnStaticCode.TabStop = true;
            this.rdBtnReturnStaticCode.UseVisualStyleBackColor = true;
            this.rdBtnReturnStaticCode.CheckedChanged += new System.EventHandler(this.returnCodeMethodChanged);
            // 
            // rdBtnReturnVariable
            // 
            resources.ApplyResources(this.rdBtnReturnVariable, "rdBtnReturnVariable");
            this.rdBtnReturnVariable.Name = "rdBtnReturnVariable";
            this.rdBtnReturnVariable.UseVisualStyleBackColor = true;
            this.rdBtnReturnVariable.CheckedChanged += new System.EventHandler(this.returnCodeMethodChanged);
            // 
            // ctxMnuRightClick
            // 
            resources.ApplyResources(this.ctxMnuRightClick, "ctxMnuRightClick");
            this.ctxMnuRightClick.Name = "ctxMnuRightClick";
            this.ctxMnuRightClick.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ctxMnuRightClick_ItemClicked);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssFilename,
            this.tssSaved});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // tssFilename
            // 
            resources.ApplyResources(this.tssFilename, "tssFilename");
            this.tssFilename.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssFilename.Name = "tssFilename";
            // 
            // tssSaved
            // 
            resources.ApplyResources(this.tssSaved, "tssSaved");
            this.tssSaved.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssSaved.Name = "tssSaved";
            // 
            // frmCustomUpdateCreator
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tlpCustomActions);
            this.Controls.Add(this.ribbon1);
            this.KeyPreview = true;
            this.Name = "frmCustomUpdateCreator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupStaticCode)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbon1;
        private System.Windows.Forms.RibbonTab rbTabFiles;
        private System.Windows.Forms.RibbonPanel ribbonPanel1;
        private System.Windows.Forms.RibbonButton ribBtnCreateTextFile;
        private System.Windows.Forms.RibbonButton ribBtnDeleteFile;
        private System.Windows.Forms.RibbonButton ribBtnRenameFile;
        private System.Windows.Forms.RibbonButton ribBtnCopyFile;
        private System.Windows.Forms.RibbonTab rbTabFolders;
        private System.Windows.Forms.RibbonTab rbTabExecute;
        private System.Windows.Forms.RibbonTab rbTabServices;
        private System.Windows.Forms.RibbonTab rbTabRegistry;
        private System.Windows.Forms.RibbonTab rbTabPower;
        private System.Windows.Forms.RibbonTab rbTabProcesses;
        private System.Windows.Forms.RibbonTab rbTabMisc;
        private System.Windows.Forms.RibbonTab rbTabTemplates;
        private System.Windows.Forms.RibbonPanel ribbonPanel2;
        private System.Windows.Forms.RibbonButton ribBtnCreateFolder;
        private System.Windows.Forms.RibbonButton ribBtnDeleteFolder;
        private System.Windows.Forms.RibbonButton ribBtnRenameFolder;
        private System.Windows.Forms.RibbonPanel ribbonPanel3;
        private System.Windows.Forms.RibbonButton ribBtnRunFile;
        private System.Windows.Forms.RibbonPanel ribbonPanel4;
        private System.Windows.Forms.RibbonButton ribBtnRunVbScript;
        private System.Windows.Forms.RibbonButton ribBtnRunPowershellScript;
        private System.Windows.Forms.RibbonPanel ribbonPanel5;
        private System.Windows.Forms.RibbonButton ribBtnStartService;
        private System.Windows.Forms.RibbonButton ribBtnStopService;
        private System.Windows.Forms.RibbonButton ribBtnUnregisterService;
        private System.Windows.Forms.RibbonButton ribBtnServiceStartingMode;
        private System.Windows.Forms.RibbonPanel ribbonPanel6;
        private System.Windows.Forms.RibbonButton ribBtnAddRegKey;
        private System.Windows.Forms.RibbonButton ribBtnDeleteRegKey;
        private System.Windows.Forms.RibbonButton ribBtnRenameRegKey;
        private System.Windows.Forms.RibbonPanel ribbonPanel7;
        private System.Windows.Forms.RibbonButton ribBtnAddRegValue;
        private System.Windows.Forms.RibbonButton ribBtnDeleteRegValue;
        private System.Windows.Forms.RibbonButton ribBtnRenameRegValue;
        private System.Windows.Forms.RibbonPanel ribbonPanel8;
        private System.Windows.Forms.RibbonButton ribBtnShutdown;
        private System.Windows.Forms.RibbonButton ribBtnReboot;
        private System.Windows.Forms.RibbonPanel ribbonPanel9;
        private System.Windows.Forms.RibbonButton ribBtnKillProcess;
        private System.Windows.Forms.RibbonPanel ribbonPanel10;
        private System.Windows.Forms.RibbonButton ribBtnWait;
        private System.Windows.Forms.RibbonPanel ribbonPanel11;
        private System.Windows.Forms.RibbonButton ribBtnLoadTemplate;
        private System.Windows.Forms.RibbonButton ribBtnSaveTemplate;
        private System.Windows.Forms.RibbonButton ribBtnSaveAsTemplate;
        private System.Windows.Forms.RibbonPanel ribPanRegFile;
        private System.Windows.Forms.RibbonButton ribBtnMergeRegFile;
        private System.Windows.Forms.RibbonTab rbTabDll;
        private System.Windows.Forms.RibbonPanel ribbonPanel12;
        private System.Windows.Forms.RibbonButton ribBtnRegisterDll;
        private System.Windows.Forms.RibbonButton ribBtnUnregisterDll;
        private System.Windows.Forms.TableLayoutPanel tlpCustomActions;
        private System.Windows.Forms.RibbonPanel ribPanRegData;
        private System.Windows.Forms.RibbonButton ribBtnChangeData;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nupStaticCode;
        private System.Windows.Forms.RadioButton rdBtnReturnStaticCode;
        private System.Windows.Forms.RadioButton rdBtnReturnVariable;
        private System.Windows.Forms.ContextMenuStrip ctxMnuRightClick;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssFilename;
        private System.Windows.Forms.ToolStripStatusLabel tssSaved;
        private System.Windows.Forms.RibbonTab rbTabShortcuts;
        private System.Windows.Forms.RibbonPanel ribbonPanel13;
        private System.Windows.Forms.RibbonButton ribBtnCreateShortcut;
        private System.Windows.Forms.RibbonPanel ribbonPanel14;
        private System.Windows.Forms.RibbonButton ribBtnDeleteTask;
        private System.Windows.Forms.RibbonTab ribTabMsi;
        private System.Windows.Forms.RibbonPanel ribbonPanel15;
        private System.Windows.Forms.RibbonButton ribBtnUninstallMsiByGuid;
        private System.Windows.Forms.RibbonButton ribBtnUinstallMsiByName;
        private System.Windows.Forms.RibbonPanel ribbonPanel16;
        private System.Windows.Forms.RibbonButton ribBtnInstallMsi;
    }
}

