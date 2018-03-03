using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Microsoft.UpdateServices.Administration;
using System.IO;

namespace Wsus_Package_Publisher
{
    internal class CatalogUpdateImporter
    {
        private List<CatalogUpdate> _packageToImport;
        private bool _makeLanguageIndependent;
        private string _sourceFolder;
        private string _downloadedFile;
        private SoftwareDistributionPackage _currentSDP;
        private bool _cancel = false;
        private int _overAllProgression = 0;
        private int _overAllCurrentPosition = 0;
        private bool _downloadCompleted = true;
        private System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();
        private WebClient webClient;
        private WsusWrapper _wsus = WsusWrapper.GetInstance();
        private System.Globalization.CultureInfo _currentCulture;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmImportCatalog).Assembly);

        internal CatalogUpdateImporter(List<CatalogUpdate> packageToImport, bool makeLanguageIndependent, string sourceFolder)
        {
            _currentCulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            _packageToImport = packageToImport;
            _makeLanguageIndependent = makeLanguageIndependent;
            _sourceFolder = sourceFolder;
            ShowInWsusConsole = false;
        }

        #region (Internal Properties - Propriétés Internes)

        internal bool Cancel { get { return _cancel; } set { _cancel = value; } }

        #endregion (Internal Properties - Propriétés Internes)

        #region (Private Properties - Propriétés Privées)

        private bool ShowInWsusConsole { get; set; }

        #endregion (Private Properties - Propriétés Privées)

        #region (Internal Methods - Méthodes Internes)

        internal void Import(object showInWsusConsole)
        {
            try
            {
                ShowInWsusConsole = (bool)showInWsusConsole;
                _overAllCurrentPosition = 0;
                foreach (CatalogUpdate update in _packageToImport)
                {
                    while (!_downloadCompleted)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    _currentSDP = update.SDP;
                    _overAllProgression = (int)(_overAllCurrentPosition * 100 / _packageToImport.Count);
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 1, 0, resMan.GetString("Importing", _currentCulture) + " " + _currentSDP.Title);
                    if (_currentSDP.PackageUpdateType == PackageUpdateType.Detectoid)
                    {
                        PublishDetectoid();
                    }
                    else
                        if (_currentSDP.InstallableItems != null && _currentSDP.InstallableItems.Count != 0)
                        {
                            if (_makeLanguageIndependent)
                                MakeLanguageIndependent(_currentSDP);
                            if (_currentSDP.InstallableItems[0].OriginalSourceFile.OriginUri != null)
                            {
                                _downloadedFile = Tools.Utilities.GetTempFolder() + _currentSDP.InstallableItems[0].OriginalSourceFile.FileName;
                                DownloadFile(_currentSDP.InstallableItems[0].OriginalSourceFile.OriginUri, _downloadedFile);
                            }
                        }
                        else
                            PublishMetaDataOnly();
                }
                while (!_downloadCompleted)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(100, 100, 0, resMan.GetString("AllTasksFinished", _currentCulture));
                if (CatalogUpdateImporterFinish != null)
                    CatalogUpdateImporterFinish();
            }
            catch (System.Threading.ThreadAbortException abort)
            {
                _cancel = true;
                Logger.Write("Abort operation : " + abort.Message);
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(0, 0, 0, resMan.GetString("Aborting", _currentCulture));
                if (webClient != null)
                {
                    webClient.CancelAsync();
                    webClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                    webClient.DownloadFileCompleted -= new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
                }
                System.Threading.Thread.Sleep(1000);
                webClient = null;
                _downloadCompleted = true;
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(0, 0, 0, resMan.GetString("Aborted", _currentCulture));
                AbortOperation();
                return;
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                if (webClient != null)
                    webClient.CancelAsync();
                _downloadCompleted = true;
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(100, 100, 0, resMan.GetString("ErrorImportingUpdate", _currentCulture));
                AbortOperation();
            }
        }

        #endregion (Internal Methods - Méthodes Internes)

        #region (Private Methods - Méthodes privées)

        private void MakeLanguageIndependent(SoftwareDistributionPackage sdp)
        {
            sdp.InstallableItems[0].Languages.Clear();
        }

        private void DownloadFile(Uri url, string destinationFilePath)
        {
            Logger.EnteringMethod(url.ToString() + " => " + destinationFilePath);

            try
            {
                FileInfo destinationFile = new FileInfo(destinationFilePath);
                if (!destinationFile.Directory.Exists)
                    destinationFile.Directory.Create();

                webClient = new WebClient();
                webClient.Proxy = Tools.Utilities.GetHTTPProxy();
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);
                _downloadCompleted = false;                
                webClient.DownloadFileAsync(url, destinationFilePath);
                chrono.Restart();
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
        }

        private void DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Logger.EnteringMethod(_currentSDP.Title);

            if (!_cancel)
            {
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(_overAllProgression, 10, 0, resMan.GetString("Publishing", _currentCulture) + " " + _currentSDP.Title);

                if (File.Exists(_downloadedFile))
                {
                    PublishSDP();

                    _overAllCurrentPosition++;
                    _overAllProgression = (int)(_overAllCurrentPosition * 100 / _packageToImport.Count);
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 100, 0, resMan.GetString("Published", _currentCulture) + " ");
                }
            }
            _downloadCompleted = true;
        }

        private void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs args)
        {
            double averageSpeed = ((double)args.BytesReceived / 1024) / ((double)chrono.ElapsedMilliseconds / 1000);

            if (!_cancel)
            {
                if (CatalogUpdateImporterProgress != null)
                    if (args.TotalBytesToReceive != -1)
                        CatalogUpdateImporterProgress(_overAllProgression, args.ProgressPercentage, averageSpeed, resMan.GetString("DownloadindOf", _currentCulture) + _currentSDP.Title);
                    else
                        CatalogUpdateImporterProgress(_overAllProgression, args.ProgressPercentage, averageSpeed, resMan.GetString("DownloadingWithUnkownTotalSize", _currentCulture) + _currentSDP.Title);
            }
        }

        private void PublishSDP()
        {
            Logger.Write("Will try to publish : " + _currentSDP.Title);
            try
            {
                IUpdate alreadyImportedUpdate = _wsus.GetUpdate(new UpdateRevisionId(_currentSDP.PackageId));
                Logger.Write("The update already exists.");
            }
            catch (Exception)
            {
                Logger.Write("The update doesn't already exists. Continue publication...");
                if (!_cancel)
                {
                    FileInfo downloadedFileInfo = new FileInfo(_downloadedFile);
                    if (downloadedFileInfo.Length != 0)
                    {
                        IUpdate publishedUpdate = _wsus.PublishUpdate(_currentSDP, downloadedFileInfo.DirectoryName);
                        if (ShowInWsusConsole)
                            MakeVisibleInWsusConsole(publishedUpdate);
                    }
                    else
                    {
                        Logger.Write("The File : " + downloadedFileInfo.FullName + " is a 0 Byte file.");
                        System.Windows.Forms.MessageBox.Show(resMan.GetString("TheFileIsA0ByteFileSize") + "\r\n" + downloadedFileInfo.FullName);
                    }
                }
            }
        }

        private void PublishMetaDataOnly()
        {
            Logger.EnteringMethod(_currentSDP.Title);

            if (!_cancel)
            {
                IUpdate publishedUpdate = null;

                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(_overAllProgression, 10, 0, resMan.GetString("Publishing", _currentCulture) + " " + _currentSDP.Title);

                Logger.Write("Will try to publish MetaData Only : " + _currentSDP.Title);
                try
                {
                    IUpdate alreadyImportedUpdate = _wsus.GetUpdate(new UpdateRevisionId(_currentSDP.PackageId));
                    Logger.Write("The update already exists.");
                }
                catch (Exception)
                {
                    Logger.Write("The update doesn't already exists. Continue publication...");
                    if (!_cancel)
                    {
                        _currentSDP.InstallableItems.Clear();
                        publishedUpdate = _wsus.PublishUpdate(_currentSDP);
                        if (ShowInWsusConsole)
                            MakeVisibleInWsusConsole(publishedUpdate);
                    }
                }
                _overAllCurrentPosition++;
                _overAllProgression = (int)(_overAllCurrentPosition * 100 / _packageToImport.Count);

                if (publishedUpdate != null)
                {
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 100, 0, resMan.GetString("Published", _currentCulture) + " ");
                }
                else
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 100, 0, resMan.GetString("FailedToPublish", _currentCulture) + " ");

            }
            _downloadCompleted = true;
        }

        private void PublishDetectoid()
        {
            Logger.EnteringMethod(_currentSDP.Title);
            bool isSuccessfulyPublished = false;

            if (!_cancel)
            {
                if (CatalogUpdateImporterProgress != null)
                    CatalogUpdateImporterProgress(_overAllProgression, 10, 0, resMan.GetString("Publishing", _currentCulture) + " " + _currentSDP.Title);

                Logger.Write("Will try to publish Detectoid : " + _currentSDP.Title);

                if (!_cancel)
                {
                    _currentSDP.InstallableItems.Clear();
                    isSuccessfulyPublished = _wsus.PublishDetectoid(_currentSDP);
                }

                _overAllCurrentPosition++;
                _overAllProgression = (int)(_overAllCurrentPosition * 100 / _packageToImport.Count);

                if (isSuccessfulyPublished)
                {
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 100, 0, resMan.GetString("Published", _currentCulture) + " ");
                }
                else
                    if (CatalogUpdateImporterProgress != null)
                        CatalogUpdateImporterProgress(_overAllProgression, 100, 0, resMan.GetString("FailedToPublish", _currentCulture) + " ");

            }
            _downloadCompleted = true;

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

        private void AbortOperation()
        {
            if (CatalogUpdateImporterFinish != null)
                CatalogUpdateImporterFinish();
        }

        #endregion (Private Methods - Méthodes privées)

        #region (Public Event - événement publique)

        internal delegate void CatalogUpdateImporterProgressEventHandler(int overAllProgression, int currentOperationProgression, double averageSpeed, string currentOperationType);
        internal event CatalogUpdateImporterProgressEventHandler CatalogUpdateImporterProgress;

        internal delegate void CatalogUpdateImporterFinishEventHandler();
        internal event CatalogUpdateImporterFinishEventHandler CatalogUpdateImporterFinish;

        #endregion (Public Event - événement publique)
    }
}
