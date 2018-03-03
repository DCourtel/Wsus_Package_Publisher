using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal partial class FrmUpdatePublisher : Form
    {
        private WsusWrapper _wsus;
        private FrmUpdateFilesWizard _filesWizard;
        private FrmUpdateInformationsWizard _informationsWizard;
        private FrmUpdateRulesWizard _isInstalledRulesWizard;
        private FrmUpdateRulesWizard _isInstallableRulesWizard;
        private FrmUpdateApplicabilityMetadata _updateApplicabilityMetadata;
        private IUpdate _publishedUpdate = null;

        internal FrmUpdatePublisher(FrmUpdateFilesWizard filesWizard, FrmUpdateInformationsWizard informationsWizard, FrmUpdateRulesWizard isInstalledRulesWizard, FrmUpdateRulesWizard isInstallableRulesWizard, FrmUpdateApplicabilityMetadata updateApplicabilityMetadata)
        {
            Logger.EnteringMethod("FrmUpdatePublisher");
            InitializeComponent();
            this._wsus = WsusWrapper.GetInstance();
            this._filesWizard = filesWizard;
            this._informationsWizard = informationsWizard;
            this._isInstalledRulesWizard = isInstalledRulesWizard;
            this._isInstallableRulesWizard = isInstallableRulesWizard;
            this._updateApplicabilityMetadata = updateApplicabilityMetadata;
        }

        private IUpdate PublishedUpdate
        {
            get { return _publishedUpdate; }
            set
            {
                if (value != null)
                {
                    Logger.EnteringMethod(value.Title);
                    _publishedUpdate = value;
                }
            }
        }

        internal void Publish()
        {
            Logger.EnteringMethod();
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdatePublisher).Assembly);

            btnOk.Enabled = false;
            prgBrPublishing.Value = 0;
            PresetVisibleInWsusConsoleChkBx();
            this.Refresh();

            _wsus.UpdatePublishingProgress += new WsusWrapper.UpdatePublishingProgressEventHandler(publisher_Progress);
            PublishedUpdate = _wsus.PublishUpdate(_filesWizard, _informationsWizard, _isInstalledRulesWizard, _isInstallableRulesWizard, _updateApplicabilityMetadata);

            if (PublishedUpdate != null)
            {
                lblUpdatePublished.ForeColor = Color.MediumSeaGreen;
                lblUpdatePublished.Text = resManager.GetString("UpdatePublished");
            }
            else
            {
                lblUpdatePublished.ForeColor = Color.OrangeRed;
                lblUpdatePublished.Text = resManager.GetString("FailedToPublish");
            }
            btnOk.Enabled = true;
        }

        internal void Revise(SoftwareDistributionPackage sdp)
        {
            Logger.EnteringMethod();
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdatePublisher).Assembly);

            btnOk.Enabled = false;
            prgBrPublishing.Value = 0;
            PresetVisibleInWsusConsoleChkBx();
            this.Refresh();

            _wsus.UpdatePublishingProgress += new WsusWrapper.UpdatePublishingProgressEventHandler(publisher_Progress);
            _wsus.ReviseUpate(_informationsWizard, _isInstalledRulesWizard, _isInstallableRulesWizard, sdp);

            lblUpdatePublished.Text = resManager.GetString("UpdateRevised");
            btnOk.Enabled = true;
        }

        private void PresetVisibleInWsusConsoleChkBx()
        {
            Logger.EnteringMethod();
            Logger.Write("Wsus Is Local : " + _wsus.IsLocal.ToString());
            if (_wsus.IsLocal)
            {
                switch (_wsus.CurrentServer.VisibleInWsusConsole)
                {
                    case FrmSettings.MakeVisibleInWsusPolicy.Never:
                        chkBxVisibleInWsusConsole.Checked = false;
                        chkBxVisibleInWsusConsole.Enabled = false;
                        break;
                    case FrmSettings.MakeVisibleInWsusPolicy.LetMeChoose:
                        chkBxVisibleInWsusConsole.Checked = false;
                        chkBxVisibleInWsusConsole.Enabled = true;
                        break;
                    case FrmSettings.MakeVisibleInWsusPolicy.Always:
                        chkBxVisibleInWsusConsole.Checked = true;
                        chkBxVisibleInWsusConsole.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                chkBxVisibleInWsusConsole.Checked = false;
                chkBxVisibleInWsusConsole.Enabled = false;
            }
            Logger.Write(chkBxVisibleInWsusConsole);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (chkBxVisibleInWsusConsole.Checked)
            {
                btnOk.Enabled = false;
                this.Cursor = Cursors.WaitCursor;
                MakeVisibleInWsusConsole(PublishedUpdate);
                this.Cursor = Cursors.Default;
            }
            this.Close();
        }

        private void MakeVisibleInWsusConsole(IUpdate PublishedUpdate)
        {
            Logger.EnteringMethod();
            SqlHelper sqlHelper = SqlHelper.GetInstance();
            string sqlServerName = _wsus.GetSqlServerName();
            string sqlDataBaseName = _wsus.GetSqlDataBaseName();
            System.Version wsusVersion = _wsus.GetServerVersion();

            if (PublishedUpdate == null)
                return;

            if (sqlServerName.Contains("MICROSOFT##SSEE") || sqlServerName.Contains("MICROSOFT##WID"))
            {
                if (wsusVersion.Major == 3)
                    sqlHelper.ServerName = @"\\.\pipe\MSSQL$MICROSOFT##SSEE\sql\query";
                if (wsusVersion.Major == 6)
                    sqlHelper.ServerName = @"\\.\pipe\Microsoft##WID\tsql\query";
                sqlHelper.DataBaseName = "SUSDB";
            }
            else
            {
                sqlHelper.ServerName = _wsus.GetSqlServerName();
                sqlHelper.DataBaseName = _wsus.GetSqlDataBaseName();
            }
            Logger.Write(sqlHelper.ServerName);
            Logger.Write(sqlHelper.DataBaseName);
            if (sqlHelper.Connect(string.Empty, string.Empty))
            {
                Logger.Write("Connected to SQL.");
                List<Guid> updateIDs = new List<Guid>();

                updateIDs.Add(PublishedUpdate.Id.UpdateId);
                UpdateCategoryCollection categories = PublishedUpdate.GetUpdateCategories();
                foreach (IUpdateCategory category in categories)
                {
                    if (!updateIDs.Contains(category.Id))
                        updateIDs.Add(category.Id);
                    if (category.ProhibitsSubcategories && !category.ProhibitsUpdates)
                    {
                        IUpdateCategory parentCategory = category.GetParentUpdateCategory();
                        if (!updateIDs.Contains(parentCategory.Id))
                            updateIDs.Add(parentCategory.Id);
                    }
                }

                sqlHelper.ShowUpdatesInConsole(updateIDs);
                sqlHelper.Disconnect();
            }
        }

        private void publisher_Progress(object sender, EventArgs e)
        {
            PublishingEventArgs eventArgs = (PublishingEventArgs)e;

            if (eventArgs.UpperProgressBound > int.MaxValue)
            {
                prgBrPublishing.Maximum = int.MaxValue;
                prgBrPublishing.Value = (int)(eventArgs.CurrentProgress * (int.MaxValue / eventArgs.CurrentProgress));
            }
            else
            {
                prgBrPublishing.Maximum = (int)eventArgs.UpperProgressBound;
                prgBrPublishing.Value = (int)eventArgs.CurrentProgress;
            }
            lblProgress.Text = eventArgs.ProgressStep.ToString() + " : " + eventArgs.ProgressInfo;
            prgBrPublishing.Refresh();
            lblProgress.Refresh();
        }

    }
}
