using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wsus_Package_Publisher
{
    internal partial class FrmCatalogUpdateImporter : Form
    {
        private List<CatalogUpdate> _packageToImport;
        private bool _makeLanguageIndependent;
        private string _sourceFolder;
        private CatalogUpdateImporter _importer;
        private System.Threading.Thread importerThread;

        internal FrmCatalogUpdateImporter(List<CatalogUpdate> packageToImport, bool makeLanguageIndependent, string sourceFolder)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            _packageToImport = packageToImport;
            _makeLanguageIndependent = makeLanguageIndependent;
            _sourceFolder = sourceFolder;
            ShowInWsusConsole = false;
        }

        #region {Internal Properties - Propriétés Internes}

        internal bool ShowInWsusConsole
        {
            get;
            set;
        }

        #endregion {Internal Properties - Propriétés Internes}

        #region (Private Methods - Méthodes privées)


        #endregion (Private Methods - Méthodes privées)

        #region (Responses to events - Réponses aux événements)

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Logger.Write("Aborting update importation");
            importerThread.Abort();
            btnCancel.Enabled = false;
            prgBarCurrent.Value = 0;
            prgBarOverAll.Value = 0;
        }

        private void FrmCatalogUpdateImporter_Shown(object sender, EventArgs e)
        {
            _importer = new CatalogUpdateImporter(_packageToImport, _makeLanguageIndependent, _sourceFolder);
            _importer.CatalogUpdateImporterProgress += new CatalogUpdateImporter.CatalogUpdateImporterProgressEventHandler(_importer_CatalogUpdateImporterProgress);
            _importer.CatalogUpdateImporterFinish += new CatalogUpdateImporter.CatalogUpdateImporterFinishEventHandler(_importer_CatalogUpdateImporterFinish);

            btnClose.Enabled = false;
            importerThread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(_importer.Import));
            importerThread.Start((object)ShowInWsusConsole);
        }

        private void _importer_CatalogUpdateImporterFinish()
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                Action action = () =>
                    {
                        btnClose.Enabled = true;
                        btnCancel.Enabled = false;
                    };
                if (!this.IsDisposed && !this.Disposing && this.InvokeRequired)
                    this.Invoke(action);
            }
        }

        private void _importer_CatalogUpdateImporterProgress(int overAllProgression, int currentOperationProgression, double averageSpeed, string currentOperationType)
        {
            if (!this.IsDisposed && !this.Disposing)
            {
                Action action = () =>
                    {
                        prgBarOverAll.Value = overAllProgression;
                        prgBarCurrent.Value = currentOperationProgression;
                        lblProgression.Text = currentOperationType;
                        txtBxAverageSpeed.Text = averageSpeed.ToString("0.00") + " KB/s";
                    };
                    if (!this.IsDisposed && !this.Disposing && this.InvokeRequired)
                        this.Invoke(action);
            }
        }

        #endregion (Responses to events - Réponses aux événements)
    }
}
