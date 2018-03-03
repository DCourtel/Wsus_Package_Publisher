using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    public partial class FrmUpdateFilesWizard : Form
    {
        public enum UpdateType
        {
            WindowsInstaller,
            WindowsInstallerPatch,
            Executable
        }
        private List<ReturnCode> _returnCodes = new List<ReturnCode>();

        System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(FrmUpdateFilesWizard).Assembly);

        private string _updateFileName = "";
        private List<string> _additionnalFileName = new List<string>();
        private UpdateType _fileType;

        public FrmUpdateFilesWizard()
        {
            Logger.EnteringMethod("FrmUpdateFilesWizard");
            InitializeComponent();
            txtBxUpdateFile.Select();
            HaveMSTFile = false;
        }

        public FrmUpdateFilesWizard(string fileName)
        {
            Logger.EnteringMethod("Filename : " + fileName);
            InitializeComponent();
            if (System.IO.File.Exists(fileName))
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                string ext = fileInfo.Extension.ToLower();
                if (ext == ".msi" || ext == ".msp" || ext == ".exe")
                {
                    txtBxUpdateFile.Text = fileName;
                    Logger.Write(txtBxUpdateFile);
                    SetFileType(ext);
                }
            }
            HaveMSTFile = false;
        }

        internal string UpdateFileName
        {
            get { return _updateFileName; }
            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Logger.Write("Setting UpdateFileName : " + value);
                    _updateFileName = value;
                }
            }
        }

        internal List<string> AdditionnalFileName
        {
            get { return _additionnalFileName; }
        }

        internal UpdateType FileType
        {
            get { return _fileType; }
            private set
            {
                Logger.Write("Setting File Type : " + value.ToString());
                _fileType = value;
            }
        }

        internal bool HaveMSTFile { get; set; }

        internal void AddAdditionnalFile(string filePath)
        {
            Logger.EnteringMethod(filePath);
            lstBxAdditionnalFiles.Items.Add(filePath);
            _additionnalFileName.Add(filePath);
            DetectMSTFile();
        }

        private void DetectMSTFile()
        {
            Logger.EnteringMethod();
            HaveMSTFile = false;
            foreach (string file in _additionnalFileName)
            {
                if (file.ToLower().EndsWith(".mst"))
                {
                    Logger.Write("True");
                    HaveMSTFile = true;
                    break;
                }
            }
            Logger.Write("false");
        }

        private void SetFileType(string extension)
        {
            Logger.EnteringMethod(extension);
            switch (extension)
            {
                case ".msi":
                    FileType = FrmUpdateFilesWizard.UpdateType.WindowsInstaller;
                    break;
                case ".msp":
                    FileType = FrmUpdateFilesWizard.UpdateType.WindowsInstallerPatch;
                    break;
                case ".exe":
                    FileType = FrmUpdateFilesWizard.UpdateType.Executable;
                    break;
                default:
                    break;
            }
        }

        private void btnBrowseUpdateFile_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            OpenFileDialog openFileDialogUpdateFile = new OpenFileDialog();

            if (!string.IsNullOrEmpty(txtBxUpdateFile.Text))
                openFileDialogUpdateFile.InitialDirectory = txtBxUpdateFile.Text;
            else
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUpdateFolder) && System.IO.Directory.Exists(Properties.Settings.Default.LastUpdateFolder))
                    openFileDialogUpdateFile.InitialDirectory = Properties.Settings.Default.LastUpdateFolder;
                else
                    openFileDialogUpdateFile.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            openFileDialogUpdateFile.Filter = resManager.GetString("openFileDialogueUpdateFile");
            openFileDialogUpdateFile.Multiselect = false;

            if (openFileDialogUpdateFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileInfo path = new System.IO.FileInfo(openFileDialogUpdateFile.FileName);

                if (!path.DirectoryName.StartsWith(@"\\"))
                {
                    if (Properties.Settings.Default.UpdateFilePathSetting == 0)
                    {
                        Properties.Settings.Default.LastUpdateFolder = path.Directory.FullName;
                        Properties.Settings.Default.Save();
                    }
                    if (!IsFileAlreadyInAdditionnalFiles(openFileDialogUpdateFile.FileName))
                    {
                        txtBxUpdateFile.Text = openFileDialogUpdateFile.FileName;
                        Logger.Write("Update File : " + txtBxUpdateFile.Text);
                    }
                    else
                    {
                        MessageBox.Show(resManager.GetString("AdditionnalFileAlreadyExist") + "\r\n" + openFileDialogUpdateFile.FileName);
                    }
                }
                else
                {
                    MessageBox.Show(resManager.GetString("CantAddUNCPath"));
                }
            }
        }

        private void txtBxUpdateFile_TextChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            btnNext.Enabled = false;

            if (!string.IsNullOrEmpty(txtBxUpdateFile.Text) && System.IO.File.Exists(txtBxUpdateFile.Text))
            {
                if (!txtBxUpdateFile.Text.StartsWith(@"\\"))
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtBxUpdateFile.Text);
                    string extension = fileInfo.Extension.ToLower();
                    if (extension == ".msi" || extension == ".msp" || extension == ".exe")
                    {
                        btnNext.Enabled = true;
                        UpdateFileName = txtBxUpdateFile.Text;
                        SetFileType(extension);
                    }
                }
                else
                {
                    MessageBox.Show(resManager.GetString("CantAddUNCPath"));
                    txtBxUpdateFile.Text = string.Empty;
                }
            }
        }

        private void btnAddAdditonnalFiles_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            OpenFileDialog openFileDialogAdditionnalFile = new OpenFileDialog();

            if (Properties.Settings.Default.AdditionalUpdateFilePathAsMainFile)
            {
                if (!string.IsNullOrEmpty(txtBxUpdateFile.Text) && System.IO.File.Exists(txtBxUpdateFile.Text))
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtBxUpdateFile.Text);
                    openFileDialogAdditionnalFile.InitialDirectory = fileInfo.Directory.FullName;
                }
                else
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUpdateFolder) && System.IO.Directory.Exists(Properties.Settings.Default.LastUpdateFolder))
                        openFileDialogAdditionnalFile.InitialDirectory = Properties.Settings.Default.LastUpdateFolder;
                    else
                        openFileDialogAdditionnalFile.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }
            else
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUpdateFolder) && System.IO.Directory.Exists(Properties.Settings.Default.LastUpdateFolder))
                    openFileDialogAdditionnalFile.InitialDirectory = Properties.Settings.Default.LastUpdateFolder;
                else
                    openFileDialogAdditionnalFile.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            }
            openFileDialogAdditionnalFile.Multiselect = true;
            openFileDialogAdditionnalFile.ValidateNames = true;

            if (openFileDialogAdditionnalFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && openFileDialogAdditionnalFile.FileNames.Length != 0)
            {
                System.IO.FileInfo path = new System.IO.FileInfo(openFileDialogAdditionnalFile.FileNames[0]);

                if (!path.DirectoryName.StartsWith(@"\\"))
                {
                    if (Properties.Settings.Default.UpdateFilePathSetting == 0)
                    {
                        Properties.Settings.Default.LastUpdateFolder = path.Directory.FullName;
                        Properties.Settings.Default.Save();
                    }
                    string biggestFile = string.Empty;
                    long biggestFileSize = 0;
                    Logger.Write("Adding additionnal files : ");
                    foreach (string file in openFileDialogAdditionnalFile.FileNames)
                    {
                        if (!IsFileSameAsUpdateFile(file) && !IsFileAlreadyInAdditionnalFiles(file))
                        {
                            lstBxAdditionnalFiles.Items.Add(file);
                            _additionnalFileName.Add(file);
                            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                            if (fileInfo.Exists && fileInfo.Length > biggestFileSize)
                            {
                                biggestFileSize = fileInfo.Length;
                                biggestFile = file;
                            }
                            Logger.Write(file);
                        }
                        else
                        {
                            MessageBox.Show(resManager.GetString("AdditionnalFileAlreadyExist") + "\r\n" + file);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(resManager.GetString("CantAddUNCPath"));
                }
            }
            DetectMSTFile();
        }

        private bool IsFileSameAsUpdateFile(string file)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            if (!string.IsNullOrEmpty(txtBxUpdateFile.Text) && System.IO.File.Exists(txtBxUpdateFile.Text))
            {
                System.IO.FileInfo updateFileInfo = new System.IO.FileInfo(txtBxUpdateFile.Text);
                if (updateFileInfo.Name.ToLower() == fileInfo.Name.ToLower())
                    return true;
            }
            return false;
        }

        private bool IsFileAlreadyInAdditionnalFiles(string file)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            foreach (string additionnalFile in _additionnalFileName)
            {
                System.IO.FileInfo additionnalInfo = new System.IO.FileInfo(additionnalFile);
                if (additionnalInfo.Name.ToLower() == fileInfo.Name.ToLower())
                    return true;
            }
            return false;
        }

        private void btnAddFolders_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            FolderBrowserEx.FolderBrowserEx fldBrowser = new FolderBrowserEx.FolderBrowserEx();
            fldBrowser.AddCommonPlace(resManager.GetString("Computer"), FavoritesContainer.FavoritesContainer.CommonPlace.MyComputer, String.Empty);
            fldBrowser.AddCommonPlace(resManager.GetString("MyDocuments"), FavoritesContainer.FavoritesContainer.CommonPlace.MyDownloads, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            fldBrowser.AddCommonPlace(resManager.GetString("Desktop"), FavoritesContainer.FavoritesContainer.CommonPlace.MyDownloads, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            fldBrowser.AddCommonPlace(resManager.GetString("WPPSetting"), FavoritesContainer.FavoritesContainer.CommonPlace.HomeFolder, Properties.Settings.Default.LastUpdateFolder);
            fldBrowser.InitialDirectory = String.Empty;

            if (Properties.Settings.Default.UpdateFilePathSetting == 0)
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUpdateFolder) && System.IO.Directory.Exists(Properties.Settings.Default.LastUpdateFolder))
                    fldBrowser.InitialDirectory = Properties.Settings.Default.LastUpdateFolder;
            }
            else
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUpdateFolder) && System.IO.Directory.Exists(Properties.Settings.Default.LastUpdateFolder))
                    fldBrowser.InitialDirectory = Properties.Settings.Default.LastUpdateFolder;

                if (Properties.Settings.Default.AdditionalUpdateFilePathAsMainFile)
                {
                    if (!string.IsNullOrEmpty(txtBxUpdateFile.Text) && System.IO.File.Exists(txtBxUpdateFile.Text))
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtBxUpdateFile.Text);
                        fldBrowser.InitialDirectory = fileInfo.Directory.FullName;
                    }
                }
            }

            if (fldBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK && fldBrowser.SelectedFolders.Count != 0)
            {
                if (Properties.Settings.Default.UpdateFilePathSetting == 0)
                {
                    Properties.Settings.Default.LastUpdateFolder = fldBrowser.InitialDirectory;
                    Properties.Settings.Default.Save();
                }
                foreach (string selectedFolder in fldBrowser.SelectedFolders)
                {
                    lstBxAdditionnalFiles.Items.Add(selectedFolder + @"\");
                    _additionnalFileName.Add(selectedFolder + @"\");
                    Logger.Write("Adding folder : " + selectedFolder + @"\");
                }
            }
        }

        private void btnRemoveAdditionnalFile_Click(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            int index = lstBxAdditionnalFiles.SelectedIndex;

            if (index != -1)
            {
                Logger.Write("Removing : " + lstBxAdditionnalFiles.SelectedItem.ToString());
                lstBxAdditionnalFiles.Items.RemoveAt(lstBxAdditionnalFiles.SelectedIndex);
            }
            if (lstBxAdditionnalFiles.Items.Count != 0)
                if (index == 0)
                    lstBxAdditionnalFiles.SelectedIndex = 0;
                else
                    if (index == lstBxAdditionnalFiles.Items.Count)
                        lstBxAdditionnalFiles.SelectedIndex = index - 1;
                    else
                        lstBxAdditionnalFiles.SelectedIndex = index;
            _additionnalFileName.Clear();
            foreach (object file in lstBxAdditionnalFiles.Items)
            {
                _additionnalFileName.Add((string)file);
            }
            DetectMSTFile();
        }

        private void lstBxAdditionnalFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.EnteringMethod();
            if (lstBxAdditionnalFiles.SelectedIndex == -1)
                btnRemoveAdditionnalFile.Enabled = false;
            else
                btnRemoveAdditionnalFile.Enabled = true;
        }

        private void lstBxAdditionnalFiles_DragEnter(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void lstBxAdditionnalFiles_DragDrop(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (fileNames != null)
                    foreach (string file in fileNames)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                            if (!fileInfo.FullName.StartsWith(@"\\"))
                            {
                                if (!IsFileSameAsUpdateFile(file) && !IsFileAlreadyInAdditionnalFiles(file))
                                {
                                    Logger.Write("Adding file : " + file);
                                    lstBxAdditionnalFiles.Items.Add(file);
                                    _additionnalFileName.Add(file);
                                }
                                else
                                {
                                    MessageBox.Show(resManager.GetString("AdditionnalFileAlreadyExist") + "\r\n" + file);
                                }
                            }
                            else
                            {
                                MessageBox.Show(resManager.GetString("CantAddUNCPath"));
                            }
                        }
                        else
                            if (System.IO.Directory.Exists(file))
                            {
                                Logger.Write("Adding folder : " + file + @"\");
                                lstBxAdditionnalFiles.Items.Add(file + @"\");
                                _additionnalFileName.Add(file + @"\");
                            }
                    }
                DetectMSTFile();
            }
        }

        private void txtBxUpdateFile_DragEnter(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            DragDropEffects effect = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Array tab = (Array)e.Data.GetData(DataFormats.FileDrop);
                if (tab != null && tab.GetValue(0) != null && tab.Length == 1)
                {
                    string fileName = tab.GetValue(0).ToString();
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (!fileInfo.DirectoryName.StartsWith(@"\\") && (ext == ".msi" || ext == ".msp" || ext == ".exe"))
                        effect = DragDropEffects.Copy;
                }
            }
            e.Effect = effect;
        }

        private void txtBxUpdateFile_DragDrop(object sender, DragEventArgs e)
        {
            Logger.EnteringMethod();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (fileNames != null && fileNames.GetValue(0) != null)
                {
                    string fileName = fileNames[0];
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                    string ext = fileInfo.Extension.ToLower();
                    if (!fileInfo.DirectoryName.StartsWith(@"\\") && (ext == ".msi" || ext == ".msp" || ext == ".exe") && !IsFileAlreadyInAdditionnalFiles(fileInfo.Name))
                        txtBxUpdateFile.Text = fileName;
                }
                Logger.Write(txtBxUpdateFile);
            }
        }
    }
}
