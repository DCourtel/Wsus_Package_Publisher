namespace Wsus_Package_Publisher
{
    partial class FrmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabServer = new System.Windows.Forms.TabPage();
            this.chkBxIgnoreVersionMismatch = new System.Windows.Forms.CheckBox();
            this.chkBxIgnoreCertificateErrors = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdBtnVisibleAlways = new System.Windows.Forms.RadioButton();
            this.rdBtnVisibleChoose = new System.Windows.Forms.RadioButton();
            this.rdBtnVisibleNever = new System.Windows.Forms.RadioButton();
            this.chkBxConnectToLocalServer = new System.Windows.Forms.CheckBox();
            this.btnEditServer = new System.Windows.Forms.Button();
            this.nupDeadLineMinute = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nupDeadLineHour = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nupDeadLineDaysSpan = new System.Windows.Forms.NumericUpDown();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.cmbBxServerList = new System.Windows.Forms.ComboBox();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.chkBxUseSSL = new System.Windows.Forms.CheckBox();
            this.cmbBxConnectionPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabCommonSettings = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBxPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBxLogin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.rdBtnAsk = new System.Windows.Forms.RadioButton();
            this.rdBtnSpecified = new System.Windows.Forms.RadioButton();
            this.rdBtnSameAsApplication = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.tabColors = new System.Windows.Forms.TabPage();
            this.btnResetToDefault = new System.Windows.Forms.Button();
            this.lblFailed = new System.Windows.Forms.Label();
            this.lblUnknown = new System.Windows.Forms.Label();
            this.lblNotInstalled = new System.Windows.Forms.Label();
            this.lblNotApplicable = new System.Windows.Forms.Label();
            this.lblDownloaded = new System.Windows.Forms.Label();
            this.lblInstalledPendingReboot = new System.Windows.Forms.Label();
            this.lblInstalled = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.cmbBxUpdateDefaultAction = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chkBxPreventAutoApproval = new System.Windows.Forms.CheckBox();
            this.grpBxUpdateFilesPath = new System.Windows.Forms.GroupBox();
            this.chkBxSamePathForAdditionnal = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBxUseThisPath = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rdBtnLastUsed = new System.Windows.Forms.RadioButton();
            this.rdBtnSamePath = new System.Windows.Forms.RadioButton();
            this.chkBxShowNonLocallyPublishedUpdates = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabProxy = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdBtnHTTPProxyNoProxy = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnTestFTPUpload = new System.Windows.Forms.Button();
            this.btnTestFTPDownload = new System.Windows.Forms.Button();
            this.rdBtnFtpProxyNoProxy = new System.Windows.Forms.RadioButton();
            this.rdBtnFtpProxyAsAbove = new System.Windows.Forms.RadioButton();
            this.rdBtnHTTPProxyCustomSettings = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnTestHTTPDownload = new System.Windows.Forms.Button();
            this.txtBxHTTPProxyServerName = new System.Windows.Forms.TextBox();
            this.txtBxHTTPProxyLogin = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtBxHTTPProxyPassword = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.nupHTTPProxyServerPort = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.tabMisc = new System.Windows.Forms.TabPage();
            this.txtBxDefaultRebootMessage = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.grpBxPing = new System.Windows.Forms.GroupBox();
            this.rdBtnIPv6IPv4 = new System.Windows.Forms.RadioButton();
            this.rdBtnIPv6 = new System.Windows.Forms.RadioButton();
            this.rdBtnIPv4 = new System.Windows.Forms.RadioButton();
            this.chkBxConnectToLastUsedServer = new System.Windows.Forms.CheckBox();
            this.lnkLblOpenWith = new System.Windows.Forms.LinkLabel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabSettings.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineMinute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineDaysSpan)).BeginInit();
            this.tabCommonSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabColors.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            this.grpBxUpdateFilesPath.SuspendLayout();
            this.tabProxy.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupHTTPProxyServerPort)).BeginInit();
            this.tabMisc.SuspendLayout();
            this.grpBxPing.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSettings
            // 
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Controls.Add(this.tabServer);
            this.tabSettings.Controls.Add(this.tabCommonSettings);
            this.tabSettings.Controls.Add(this.tabColors);
            this.tabSettings.Controls.Add(this.tabUpdates);
            this.tabSettings.Controls.Add(this.tabProxy);
            this.tabSettings.Controls.Add(this.tabMisc);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            // 
            // tabServer
            // 
            resources.ApplyResources(this.tabServer, "tabServer");
            this.tabServer.Controls.Add(this.chkBxIgnoreVersionMismatch);
            this.tabServer.Controls.Add(this.chkBxIgnoreCertificateErrors);
            this.tabServer.Controls.Add(this.label11);
            this.tabServer.Controls.Add(this.groupBox2);
            this.tabServer.Controls.Add(this.chkBxConnectToLocalServer);
            this.tabServer.Controls.Add(this.btnEditServer);
            this.tabServer.Controls.Add(this.nupDeadLineMinute);
            this.tabServer.Controls.Add(this.label5);
            this.tabServer.Controls.Add(this.nupDeadLineHour);
            this.tabServer.Controls.Add(this.label4);
            this.tabServer.Controls.Add(this.label3);
            this.tabServer.Controls.Add(this.nupDeadLineDaysSpan);
            this.tabServer.Controls.Add(this.btnRemoveServer);
            this.tabServer.Controls.Add(this.cmbBxServerList);
            this.tabServer.Controls.Add(this.btnAddServer);
            this.tabServer.Controls.Add(this.chkBxUseSSL);
            this.tabServer.Controls.Add(this.cmbBxConnectionPort);
            this.tabServer.Controls.Add(this.label2);
            this.tabServer.Controls.Add(this.txtBxServerName);
            this.tabServer.Controls.Add(this.label1);
            this.tabServer.Name = "tabServer";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // chkBxIgnoreVersionMismatch
            // 
            resources.ApplyResources(this.chkBxIgnoreVersionMismatch, "chkBxIgnoreVersionMismatch");
            this.chkBxIgnoreVersionMismatch.Name = "chkBxIgnoreVersionMismatch";
            this.chkBxIgnoreVersionMismatch.UseVisualStyleBackColor = true;
            // 
            // chkBxIgnoreCertificateErrors
            // 
            resources.ApplyResources(this.chkBxIgnoreCertificateErrors, "chkBxIgnoreCertificateErrors");
            this.chkBxIgnoreCertificateErrors.Name = "chkBxIgnoreCertificateErrors";
            this.chkBxIgnoreCertificateErrors.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.rdBtnVisibleAlways);
            this.groupBox2.Controls.Add(this.rdBtnVisibleChoose);
            this.groupBox2.Controls.Add(this.rdBtnVisibleNever);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rdBtnVisibleAlways
            // 
            resources.ApplyResources(this.rdBtnVisibleAlways, "rdBtnVisibleAlways");
            this.rdBtnVisibleAlways.Name = "rdBtnVisibleAlways";
            this.rdBtnVisibleAlways.UseVisualStyleBackColor = true;
            // 
            // rdBtnVisibleChoose
            // 
            resources.ApplyResources(this.rdBtnVisibleChoose, "rdBtnVisibleChoose");
            this.rdBtnVisibleChoose.Name = "rdBtnVisibleChoose";
            this.rdBtnVisibleChoose.UseVisualStyleBackColor = true;
            // 
            // rdBtnVisibleNever
            // 
            resources.ApplyResources(this.rdBtnVisibleNever, "rdBtnVisibleNever");
            this.rdBtnVisibleNever.Checked = true;
            this.rdBtnVisibleNever.Name = "rdBtnVisibleNever";
            this.rdBtnVisibleNever.TabStop = true;
            this.rdBtnVisibleNever.UseVisualStyleBackColor = true;
            // 
            // chkBxConnectToLocalServer
            // 
            resources.ApplyResources(this.chkBxConnectToLocalServer, "chkBxConnectToLocalServer");
            this.chkBxConnectToLocalServer.Name = "chkBxConnectToLocalServer";
            this.chkBxConnectToLocalServer.UseVisualStyleBackColor = true;
            this.chkBxConnectToLocalServer.CheckedChanged += new System.EventHandler(this.chkBxConnectToLocalServer_CheckedChanged);
            // 
            // btnEditServer
            // 
            resources.ApplyResources(this.btnEditServer, "btnEditServer");
            this.btnEditServer.Name = "btnEditServer";
            this.btnEditServer.UseVisualStyleBackColor = true;
            this.btnEditServer.Click += new System.EventHandler(this.btnEditServer_Click);
            // 
            // nupDeadLineMinute
            // 
            resources.ApplyResources(this.nupDeadLineMinute, "nupDeadLineMinute");
            this.nupDeadLineMinute.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nupDeadLineMinute.Name = "nupDeadLineMinute";
            this.nupDeadLineMinute.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // nupDeadLineHour
            // 
            resources.ApplyResources(this.nupDeadLineHour, "nupDeadLineHour");
            this.nupDeadLineHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nupDeadLineHour.Name = "nupDeadLineHour";
            this.nupDeadLineHour.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nupDeadLineDaysSpan
            // 
            resources.ApplyResources(this.nupDeadLineDaysSpan, "nupDeadLineDaysSpan");
            this.nupDeadLineDaysSpan.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nupDeadLineDaysSpan.Name = "nupDeadLineDaysSpan";
            this.nupDeadLineDaysSpan.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // btnRemoveServer
            // 
            resources.ApplyResources(this.btnRemoveServer, "btnRemoveServer");
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // cmbBxServerList
            // 
            resources.ApplyResources(this.cmbBxServerList, "cmbBxServerList");
            this.cmbBxServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxServerList.FormattingEnabled = true;
            this.cmbBxServerList.Name = "cmbBxServerList";
            this.cmbBxServerList.SelectedIndexChanged += new System.EventHandler(this.cmbBxServerList_SelectedIndexChanged);
            // 
            // btnAddServer
            // 
            resources.ApplyResources(this.btnAddServer, "btnAddServer");
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // chkBxUseSSL
            // 
            resources.ApplyResources(this.chkBxUseSSL, "chkBxUseSSL");
            this.chkBxUseSSL.Name = "chkBxUseSSL";
            this.chkBxUseSSL.UseVisualStyleBackColor = true;
            // 
            // cmbBxConnectionPort
            // 
            resources.ApplyResources(this.cmbBxConnectionPort, "cmbBxConnectionPort");
            this.cmbBxConnectionPort.FormattingEnabled = true;
            this.cmbBxConnectionPort.Items.AddRange(new object[] {
            resources.GetString("cmbBxConnectionPort.Items"),
            resources.GetString("cmbBxConnectionPort.Items1"),
            resources.GetString("cmbBxConnectionPort.Items2"),
            resources.GetString("cmbBxConnectionPort.Items3")});
            this.cmbBxConnectionPort.Name = "cmbBxConnectionPort";
            this.cmbBxConnectionPort.SelectedIndexChanged += new System.EventHandler(this.cmbBxConnectionPort_SelectedIndexChanged);
            this.cmbBxConnectionPort.TextChanged += new System.EventHandler(this.cmbBxConnectionPort_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtBxServerName
            // 
            resources.ApplyResources(this.txtBxServerName, "txtBxServerName");
            this.txtBxServerName.Name = "txtBxServerName";
            this.txtBxServerName.TextChanged += new System.EventHandler(this.txtBxServerName_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabCommonSettings
            // 
            resources.ApplyResources(this.tabCommonSettings, "tabCommonSettings");
            this.tabCommonSettings.Controls.Add(this.groupBox1);
            this.tabCommonSettings.Controls.Add(this.label6);
            this.tabCommonSettings.Name = "tabCommonSettings";
            this.tabCommonSettings.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txtBxPassword);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtBxLogin);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.rdBtnAsk);
            this.groupBox1.Controls.Add(this.rdBtnSpecified);
            this.groupBox1.Controls.Add(this.rdBtnSameAsApplication);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txtBxPassword
            // 
            resources.ApplyResources(this.txtBxPassword, "txtBxPassword");
            this.txtBxPassword.Name = "txtBxPassword";
            this.txtBxPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtBxLogin
            // 
            resources.ApplyResources(this.txtBxLogin, "txtBxLogin");
            this.txtBxLogin.Name = "txtBxLogin";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // rdBtnAsk
            // 
            resources.ApplyResources(this.rdBtnAsk, "rdBtnAsk");
            this.rdBtnAsk.Name = "rdBtnAsk";
            this.rdBtnAsk.UseVisualStyleBackColor = true;
            this.rdBtnAsk.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // rdBtnSpecified
            // 
            resources.ApplyResources(this.rdBtnSpecified, "rdBtnSpecified");
            this.rdBtnSpecified.Name = "rdBtnSpecified";
            this.rdBtnSpecified.UseVisualStyleBackColor = true;
            this.rdBtnSpecified.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // rdBtnSameAsApplication
            // 
            resources.ApplyResources(this.rdBtnSameAsApplication, "rdBtnSameAsApplication");
            this.rdBtnSameAsApplication.Checked = true;
            this.rdBtnSameAsApplication.Name = "rdBtnSameAsApplication";
            this.rdBtnSameAsApplication.TabStop = true;
            this.rdBtnSameAsApplication.UseVisualStyleBackColor = true;
            this.rdBtnSameAsApplication.CheckedChanged += new System.EventHandler(this.rdBtnSameThanApplication_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // tabColors
            // 
            resources.ApplyResources(this.tabColors, "tabColors");
            this.tabColors.Controls.Add(this.btnResetToDefault);
            this.tabColors.Controls.Add(this.lblFailed);
            this.tabColors.Controls.Add(this.lblUnknown);
            this.tabColors.Controls.Add(this.lblNotInstalled);
            this.tabColors.Controls.Add(this.lblNotApplicable);
            this.tabColors.Controls.Add(this.lblDownloaded);
            this.tabColors.Controls.Add(this.lblInstalledPendingReboot);
            this.tabColors.Controls.Add(this.lblInstalled);
            this.tabColors.Controls.Add(this.label9);
            this.tabColors.Name = "tabColors";
            this.tabColors.UseVisualStyleBackColor = true;
            // 
            // btnResetToDefault
            // 
            resources.ApplyResources(this.btnResetToDefault, "btnResetToDefault");
            this.btnResetToDefault.Name = "btnResetToDefault";
            this.btnResetToDefault.UseVisualStyleBackColor = true;
            this.btnResetToDefault.Click += new System.EventHandler(this.btnResetToDefault_Click);
            // 
            // lblFailed
            // 
            resources.ApplyResources(this.lblFailed, "lblFailed");
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblUnknown
            // 
            resources.ApplyResources(this.lblUnknown, "lblUnknown");
            this.lblUnknown.Name = "lblUnknown";
            this.lblUnknown.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblNotInstalled
            // 
            resources.ApplyResources(this.lblNotInstalled, "lblNotInstalled");
            this.lblNotInstalled.Name = "lblNotInstalled";
            this.lblNotInstalled.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblNotApplicable
            // 
            resources.ApplyResources(this.lblNotApplicable, "lblNotApplicable");
            this.lblNotApplicable.Name = "lblNotApplicable";
            this.lblNotApplicable.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblDownloaded
            // 
            resources.ApplyResources(this.lblDownloaded, "lblDownloaded");
            this.lblDownloaded.Name = "lblDownloaded";
            this.lblDownloaded.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblInstalledPendingReboot
            // 
            resources.ApplyResources(this.lblInstalledPendingReboot, "lblInstalledPendingReboot");
            this.lblInstalledPendingReboot.Name = "lblInstalledPendingReboot";
            this.lblInstalledPendingReboot.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // lblInstalled
            // 
            resources.ApplyResources(this.lblInstalled, "lblInstalled");
            this.lblInstalled.Name = "lblInstalled";
            this.lblInstalled.DoubleClick += new System.EventHandler(this.lblInstalled_DoubleClick);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // tabUpdates
            // 
            resources.ApplyResources(this.tabUpdates, "tabUpdates");
            this.tabUpdates.Controls.Add(this.cmbBxUpdateDefaultAction);
            this.tabUpdates.Controls.Add(this.label14);
            this.tabUpdates.Controls.Add(this.chkBxPreventAutoApproval);
            this.tabUpdates.Controls.Add(this.grpBxUpdateFilesPath);
            this.tabUpdates.Controls.Add(this.chkBxShowNonLocallyPublishedUpdates);
            this.tabUpdates.Controls.Add(this.label10);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // cmbBxUpdateDefaultAction
            // 
            resources.ApplyResources(this.cmbBxUpdateDefaultAction, "cmbBxUpdateDefaultAction");
            this.cmbBxUpdateDefaultAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxUpdateDefaultAction.FormattingEnabled = true;
            this.cmbBxUpdateDefaultAction.Name = "cmbBxUpdateDefaultAction";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // chkBxPreventAutoApproval
            // 
            resources.ApplyResources(this.chkBxPreventAutoApproval, "chkBxPreventAutoApproval");
            this.chkBxPreventAutoApproval.Checked = true;
            this.chkBxPreventAutoApproval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxPreventAutoApproval.Name = "chkBxPreventAutoApproval";
            this.chkBxPreventAutoApproval.UseVisualStyleBackColor = true;
            // 
            // grpBxUpdateFilesPath
            // 
            resources.ApplyResources(this.grpBxUpdateFilesPath, "grpBxUpdateFilesPath");
            this.grpBxUpdateFilesPath.Controls.Add(this.chkBxSamePathForAdditionnal);
            this.grpBxUpdateFilesPath.Controls.Add(this.btnBrowse);
            this.grpBxUpdateFilesPath.Controls.Add(this.txtBxUseThisPath);
            this.grpBxUpdateFilesPath.Controls.Add(this.label13);
            this.grpBxUpdateFilesPath.Controls.Add(this.rdBtnLastUsed);
            this.grpBxUpdateFilesPath.Controls.Add(this.rdBtnSamePath);
            this.grpBxUpdateFilesPath.Name = "grpBxUpdateFilesPath";
            this.grpBxUpdateFilesPath.TabStop = false;
            // 
            // chkBxSamePathForAdditionnal
            // 
            resources.ApplyResources(this.chkBxSamePathForAdditionnal, "chkBxSamePathForAdditionnal");
            this.chkBxSamePathForAdditionnal.Checked = true;
            this.chkBxSamePathForAdditionnal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxSamePathForAdditionnal.Name = "chkBxSamePathForAdditionnal";
            this.chkBxSamePathForAdditionnal.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBxUseThisPath
            // 
            resources.ApplyResources(this.txtBxUseThisPath, "txtBxUseThisPath");
            this.txtBxUseThisPath.Name = "txtBxUseThisPath";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // rdBtnLastUsed
            // 
            resources.ApplyResources(this.rdBtnLastUsed, "rdBtnLastUsed");
            this.rdBtnLastUsed.Checked = true;
            this.rdBtnLastUsed.Name = "rdBtnLastUsed";
            this.rdBtnLastUsed.TabStop = true;
            this.rdBtnLastUsed.UseVisualStyleBackColor = true;
            this.rdBtnLastUsed.CheckedChanged += new System.EventHandler(this.rdBtnLastUsed_CheckedChanged);
            // 
            // rdBtnSamePath
            // 
            resources.ApplyResources(this.rdBtnSamePath, "rdBtnSamePath");
            this.rdBtnSamePath.Name = "rdBtnSamePath";
            this.rdBtnSamePath.UseVisualStyleBackColor = true;
            this.rdBtnSamePath.CheckedChanged += new System.EventHandler(this.rdBtnLastUsed_CheckedChanged);
            // 
            // chkBxShowNonLocallyPublishedUpdates
            // 
            resources.ApplyResources(this.chkBxShowNonLocallyPublishedUpdates, "chkBxShowNonLocallyPublishedUpdates");
            this.chkBxShowNonLocallyPublishedUpdates.Checked = true;
            this.chkBxShowNonLocallyPublishedUpdates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBxShowNonLocallyPublishedUpdates.Name = "chkBxShowNonLocallyPublishedUpdates";
            this.chkBxShowNonLocallyPublishedUpdates.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tabProxy
            // 
            resources.ApplyResources(this.tabProxy, "tabProxy");
            this.tabProxy.Controls.Add(this.groupBox3);
            this.tabProxy.Controls.Add(this.label16);
            this.tabProxy.Name = "tabProxy";
            this.tabProxy.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.rdBtnHTTPProxyNoProxy);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.rdBtnHTTPProxyCustomSettings);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // rdBtnHTTPProxyNoProxy
            // 
            resources.ApplyResources(this.rdBtnHTTPProxyNoProxy, "rdBtnHTTPProxyNoProxy");
            this.rdBtnHTTPProxyNoProxy.Checked = true;
            this.rdBtnHTTPProxyNoProxy.Name = "rdBtnHTTPProxyNoProxy";
            this.rdBtnHTTPProxyNoProxy.TabStop = true;
            this.rdBtnHTTPProxyNoProxy.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.btnTestFTPUpload);
            this.groupBox5.Controls.Add(this.btnTestFTPDownload);
            this.groupBox5.Controls.Add(this.rdBtnFtpProxyNoProxy);
            this.groupBox5.Controls.Add(this.rdBtnFtpProxyAsAbove);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // btnTestFTPUpload
            // 
            resources.ApplyResources(this.btnTestFTPUpload, "btnTestFTPUpload");
            this.btnTestFTPUpload.Name = "btnTestFTPUpload";
            this.btnTestFTPUpload.UseVisualStyleBackColor = true;
            this.btnTestFTPUpload.Click += new System.EventHandler(this.btnTestFTPUpload_Click);
            // 
            // btnTestFTPDownload
            // 
            resources.ApplyResources(this.btnTestFTPDownload, "btnTestFTPDownload");
            this.btnTestFTPDownload.Name = "btnTestFTPDownload";
            this.btnTestFTPDownload.UseVisualStyleBackColor = true;
            this.btnTestFTPDownload.Click += new System.EventHandler(this.btnTestFTPDownload_Click);
            // 
            // rdBtnFtpProxyNoProxy
            // 
            resources.ApplyResources(this.rdBtnFtpProxyNoProxy, "rdBtnFtpProxyNoProxy");
            this.rdBtnFtpProxyNoProxy.Name = "rdBtnFtpProxyNoProxy";
            this.rdBtnFtpProxyNoProxy.UseVisualStyleBackColor = true;
            // 
            // rdBtnFtpProxyAsAbove
            // 
            resources.ApplyResources(this.rdBtnFtpProxyAsAbove, "rdBtnFtpProxyAsAbove");
            this.rdBtnFtpProxyAsAbove.Checked = true;
            this.rdBtnFtpProxyAsAbove.Name = "rdBtnFtpProxyAsAbove";
            this.rdBtnFtpProxyAsAbove.TabStop = true;
            this.rdBtnFtpProxyAsAbove.UseVisualStyleBackColor = true;
            // 
            // rdBtnHTTPProxyCustomSettings
            // 
            resources.ApplyResources(this.rdBtnHTTPProxyCustomSettings, "rdBtnHTTPProxyCustomSettings");
            this.rdBtnHTTPProxyCustomSettings.Name = "rdBtnHTTPProxyCustomSettings";
            this.rdBtnHTTPProxyCustomSettings.UseVisualStyleBackColor = true;
            this.rdBtnHTTPProxyCustomSettings.CheckedChanged += new System.EventHandler(this.rdBtnCustomSettings_CheckedChanged);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.btnTestHTTPDownload);
            this.groupBox4.Controls.Add(this.txtBxHTTPProxyServerName);
            this.groupBox4.Controls.Add(this.txtBxHTTPProxyLogin);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.txtBxHTTPProxyPassword);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.nupHTTPProxyServerPort);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // btnTestHTTPDownload
            // 
            resources.ApplyResources(this.btnTestHTTPDownload, "btnTestHTTPDownload");
            this.btnTestHTTPDownload.Name = "btnTestHTTPDownload";
            this.btnTestHTTPDownload.UseVisualStyleBackColor = true;
            this.btnTestHTTPDownload.Click += new System.EventHandler(this.btnTestHTTPDownload_Click);
            // 
            // txtBxHTTPProxyServerName
            // 
            resources.ApplyResources(this.txtBxHTTPProxyServerName, "txtBxHTTPProxyServerName");
            this.txtBxHTTPProxyServerName.Name = "txtBxHTTPProxyServerName";
            // 
            // txtBxHTTPProxyLogin
            // 
            resources.ApplyResources(this.txtBxHTTPProxyLogin, "txtBxHTTPProxyLogin");
            this.txtBxHTTPProxyLogin.Name = "txtBxHTTPProxyLogin";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // txtBxHTTPProxyPassword
            // 
            resources.ApplyResources(this.txtBxHTTPProxyPassword, "txtBxHTTPProxyPassword");
            this.txtBxHTTPProxyPassword.Name = "txtBxHTTPProxyPassword";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // nupHTTPProxyServerPort
            // 
            resources.ApplyResources(this.nupHTTPProxyServerPort, "nupHTTPProxyServerPort");
            this.nupHTTPProxyServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nupHTTPProxyServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupHTTPProxyServerPort.Name = "nupHTTPProxyServerPort";
            this.nupHTTPProxyServerPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // tabMisc
            // 
            resources.ApplyResources(this.tabMisc, "tabMisc");
            this.tabMisc.Controls.Add(this.txtBxDefaultRebootMessage);
            this.tabMisc.Controls.Add(this.label15);
            this.tabMisc.Controls.Add(this.grpBxPing);
            this.tabMisc.Controls.Add(this.chkBxConnectToLastUsedServer);
            this.tabMisc.Controls.Add(this.lnkLblOpenWith);
            this.tabMisc.Controls.Add(this.label12);
            this.tabMisc.Name = "tabMisc";
            this.tabMisc.UseVisualStyleBackColor = true;
            // 
            // txtBxDefaultRebootMessage
            // 
            resources.ApplyResources(this.txtBxDefaultRebootMessage, "txtBxDefaultRebootMessage");
            this.txtBxDefaultRebootMessage.Name = "txtBxDefaultRebootMessage";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // grpBxPing
            // 
            resources.ApplyResources(this.grpBxPing, "grpBxPing");
            this.grpBxPing.Controls.Add(this.rdBtnIPv6IPv4);
            this.grpBxPing.Controls.Add(this.rdBtnIPv6);
            this.grpBxPing.Controls.Add(this.rdBtnIPv4);
            this.grpBxPing.Name = "grpBxPing";
            this.grpBxPing.TabStop = false;
            // 
            // rdBtnIPv6IPv4
            // 
            resources.ApplyResources(this.rdBtnIPv6IPv4, "rdBtnIPv6IPv4");
            this.rdBtnIPv6IPv4.Name = "rdBtnIPv6IPv4";
            this.rdBtnIPv6IPv4.TabStop = true;
            this.rdBtnIPv6IPv4.UseVisualStyleBackColor = true;
            // 
            // rdBtnIPv6
            // 
            resources.ApplyResources(this.rdBtnIPv6, "rdBtnIPv6");
            this.rdBtnIPv6.Name = "rdBtnIPv6";
            this.rdBtnIPv6.TabStop = true;
            this.rdBtnIPv6.UseVisualStyleBackColor = true;
            // 
            // rdBtnIPv4
            // 
            resources.ApplyResources(this.rdBtnIPv4, "rdBtnIPv4");
            this.rdBtnIPv4.Checked = true;
            this.rdBtnIPv4.Name = "rdBtnIPv4";
            this.rdBtnIPv4.TabStop = true;
            this.rdBtnIPv4.UseVisualStyleBackColor = true;
            // 
            // chkBxConnectToLastUsedServer
            // 
            resources.ApplyResources(this.chkBxConnectToLastUsedServer, "chkBxConnectToLastUsedServer");
            this.chkBxConnectToLastUsedServer.Name = "chkBxConnectToLastUsedServer";
            this.chkBxConnectToLastUsedServer.UseVisualStyleBackColor = true;
            // 
            // lnkLblOpenWith
            // 
            resources.ApplyResources(this.lnkLblOpenWith, "lnkLblOpenWith");
            this.lnkLblOpenWith.Name = "lnkLblOpenWith";
            this.lnkLblOpenWith.TabStop = true;
            this.lnkLblOpenWith.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLblOpenWith_LinkClicked);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.tabSettings);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Shown += new System.EventHandler(this.FrmSettings_Shown);
            this.tabSettings.ResumeLayout(false);
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineMinute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupDeadLineDaysSpan)).EndInit();
            this.tabCommonSettings.ResumeLayout(false);
            this.tabCommonSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabColors.ResumeLayout(false);
            this.tabColors.PerformLayout();
            this.tabUpdates.ResumeLayout(false);
            this.tabUpdates.PerformLayout();
            this.grpBxUpdateFilesPath.ResumeLayout(false);
            this.grpBxUpdateFilesPath.PerformLayout();
            this.tabProxy.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupHTTPProxyServerPort)).EndInit();
            this.tabMisc.ResumeLayout(false);
            this.tabMisc.PerformLayout();
            this.grpBxPing.ResumeLayout(false);
            this.grpBxPing.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox chkBxUseSSL;
        private System.Windows.Forms.ComboBox cmbBxConnectionPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBxServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabCommonSettings;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.ComboBox cmbBxServerList;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.NumericUpDown nupDeadLineMinute;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nupDeadLineHour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nupDeadLineDaysSpan;
        private System.Windows.Forms.Button btnEditServer;
        private System.Windows.Forms.CheckBox chkBxConnectToLocalServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBxPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBxLogin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rdBtnAsk;
        private System.Windows.Forms.RadioButton rdBtnSpecified;
        private System.Windows.Forms.RadioButton rdBtnSameAsApplication;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabColors;
        private System.Windows.Forms.Label lblInstalledPendingReboot;
        private System.Windows.Forms.Label lblInstalled;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Label lblUnknown;
        private System.Windows.Forms.Label lblNotInstalled;
        private System.Windows.Forms.Label lblNotApplicable;
        private System.Windows.Forms.Label lblDownloaded;
        private System.Windows.Forms.Button btnResetToDefault;
        private System.Windows.Forms.TabPage tabUpdates;
        private System.Windows.Forms.CheckBox chkBxShowNonLocallyPublishedUpdates;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabMisc;
        private System.Windows.Forms.LinkLabel lnkLblOpenWith;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdBtnVisibleAlways;
        private System.Windows.Forms.RadioButton rdBtnVisibleChoose;
        private System.Windows.Forms.RadioButton rdBtnVisibleNever;
        private System.Windows.Forms.GroupBox grpBxUpdateFilesPath;
        private System.Windows.Forms.CheckBox chkBxSamePathForAdditionnal;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBxUseThisPath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton rdBtnLastUsed;
        private System.Windows.Forms.RadioButton rdBtnSamePath;
        private System.Windows.Forms.CheckBox chkBxIgnoreCertificateErrors;
        private System.Windows.Forms.CheckBox chkBxConnectToLastUsedServer;
        private System.Windows.Forms.CheckBox chkBxPreventAutoApproval;
        private System.Windows.Forms.GroupBox grpBxPing;
        private System.Windows.Forms.RadioButton rdBtnIPv6IPv4;
        private System.Windows.Forms.RadioButton rdBtnIPv6;
        private System.Windows.Forms.RadioButton rdBtnIPv4;
        private System.Windows.Forms.ComboBox cmbBxUpdateDefaultAction;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBxDefaultRebootMessage;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabProxy;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtBxHTTPProxyPassword;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtBxHTTPProxyLogin;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nupHTTPProxyServerPort;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtBxHTTPProxyServerName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton rdBtnHTTPProxyCustomSettings;
        private System.Windows.Forms.RadioButton rdBtnHTTPProxyNoProxy;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdBtnFtpProxyNoProxy;
        private System.Windows.Forms.RadioButton rdBtnFtpProxyAsAbove;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnTestFTPUpload;
        private System.Windows.Forms.Button btnTestFTPDownload;
        private System.Windows.Forms.Button btnTestHTTPDownload;
        private System.Windows.Forms.CheckBox chkBxIgnoreVersionMismatch;
    }
}