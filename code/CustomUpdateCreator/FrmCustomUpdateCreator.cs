using CustomActions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CustomUpdateCreator
{
    public partial class frmCustomUpdateCreator : Form
    {
        private string _currentFolder = String.Empty;
        private string _currentFilename = String.Empty;
        private Localizator _localizator = Localizator.Getinstance();
        private CustomUpdate _customUpdate;

        public frmCustomUpdateCreator()
        {
            this.InitializeForm();
            _customUpdate = new CustomUpdate();
            _customUpdate.IsUIEnable = true;
        }

        public frmCustomUpdateCreator(string filePath)
        {
            this.InitializeForm();
            _customUpdate = new CustomUpdate(filePath);
            _customUpdate.IsUIEnable = true;
            RefreshDisplay();
        }

        #region (private properties)

        /// <summary>
        /// Gets or Sets if the custom update has been modified since the last saving.
        /// </summary>
        private bool IsUnsaved { get; set; }

        #endregion (private properties)

        #region (public properties)

        public bool RefersToHKeyCurrentUser
        {
            get
            {
                foreach (GenericAction action in this._customUpdate.GetAllActions().ToList<GenericAction>())
                {
                    if (action.RefersToHKeyCurrentUser)
                        return true;
                }
                return false;
            }
        }

        public bool RefersToUserProfile
        {
            get
            {
                foreach (GenericAction action in this._customUpdate.GetAllActions().ToList<GenericAction>())
                {
                    if (action.RefersToUserProfile)
                        return true;
                }
                return false;
            }
        }

        #endregion (public properties)

        #region (public methods)

        public string GetXmlActions()
        {
            return this._customUpdate.GetXmlFormattedActions(this.rdBtnReturnStaticCode.Checked, (int)this.nupStaticCode.Value);
        }

        #endregion (public methods)

        #region (private methods)

        private void InitializeForm()
        {
            //System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ru");
            //System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.IsUnsaved = true;
            this.UpdateToolStripStatus();

            this.ctxMnuRightClick.Items.Add(_localizator.GetLocalizedString("MoveUp"), Properties.Resources.MoveUp);
            this.ctxMnuRightClick.Items.Add(Localizator.Getinstance().GetLocalizedString("MoveDown"), Properties.Resources.MoveDown);
            this.ctxMnuRightClick.Items.Add(new ToolStripSeparator());
            this.ctxMnuRightClick.Items.Add(_localizator.GetLocalizedString("Delete"), Properties.Resources.DeleteRed);

            try
            {
                if (Properties.Settings.Default.LastUsedFolder == String.Empty || !System.IO.Directory.Exists(Properties.Settings.Default.LastUsedFolder))
                {
                    string myDocuments = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None);
                    Properties.Settings.Default.LastUsedFolder = myDocuments;
                    Properties.Settings.Default.Save();
                }
                this._currentFolder = Properties.Settings.Default.LastUsedFolder;
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Add the new action to the Table Layout Panel.
        /// </summary>
        /// <param name="actionToAdd">New GenericAction to add to the layout panel.</param>
        private void AddNewActionToUI(GenericAction actionToAdd)
        {
            actionToAdd.Dock = DockStyle.Top;
            actionToAdd.Click += actionToAdd_Click;
            actionToAdd.Change += actionToAdd_Change;
            this.IsUnsaved = true;
            this.tlpCustomActions.Controls.Add(actionToAdd);
            this.UpdateToolStripStatus();
        }

        private void actionToAdd_Change(object sender, EventArgs e)
        {
            this.IsUnsaved = true;
            this.UpdateToolStripStatus();
        }

        private void actionToAdd_Click(object sender, EventArgs e)
        {
            if (e.GetType() == typeof(MouseEventArgs))
            {
                MouseEventArgs clickSettings = (MouseEventArgs)e;
                GenericAction clickedAction = (GenericAction)sender;

                this.UnselectAllActions(clickedAction, Control.ModifierKeys);
                if (clickSettings.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    clickedAction.IsSelected = true;
                    this.DisplayContextMenu(clickedAction, clickSettings.Location);
                }
            }
        }

        private void DisplayContextMenu(GenericAction clickedAction, Point location)
        {
            int index = this.tlpCustomActions.Controls.IndexOf(clickedAction);

            this.ctxMnuRightClick.Items[0].Enabled = (index != 0);
            this.ctxMnuRightClick.Items[1].Enabled = (index != this.tlpCustomActions.Controls.Count - 1);
            this.ctxMnuRightClick.Show(PointToScreen(this.PointToClient(clickedAction.PointToScreen(location))));
        }

        private void UnselectAllActions(GenericAction clickedAction, Keys modifierKeys)
        {
            foreach (GenericAction action in this.tlpCustomActions.Controls)
            {
                action.IsSelected = false;
            }
        }

        private string GetFileFullPath()
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new SaveFileDialog();
                        
            saveFileDialog.InitialDirectory = System.IO.Directory.Exists(this._currentFolder) ? this._currentFolder : System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None);
            saveFileDialog.FileName = String.IsNullOrEmpty(this._currentFilename) ? _localizator.GetLocalizedString("unnamed") : this._currentFilename;
            saveFileDialog.Filter = "CustomAction Templates|*.CustAct|All files|*.*";

            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(saveFileDialog.FileName);
                this._currentFilename = fileInfo.Name;
                this._currentFolder = fileInfo.DirectoryName;
                Properties.Settings.Default.LastUsedFolder = fileInfo.DirectoryName;
                Properties.Settings.Default.Save();
                return saveFileDialog.FileName;
            }

            return String.Empty;
        }

        private void SaveAsXml(string fullFilePath)
        {
            try
            {
                this._customUpdate.Save(System.IO.Path.Combine(this._currentFolder, this._currentFilename), this.rdBtnReturnStaticCode.Checked, (int)this.nupStaticCode.Value);
                this.IsUnsaved = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(_localizator.GetLocalizedString("AnErrorOccursWhileSavingTemplate") + ex.Message);
            }
        }

        private void LoadFromXml(string fullFilePath)
        {
            try
            {
                this._customUpdate.Load(fullFilePath);
                foreach (GenericAction action in this._customUpdate.GetAllActions())
                {
                    this.AddNewActionToUI(action);
                }
                this.RefreshDisplay();
                switch (this._customUpdate.ReturnCodeMethod)
                {
                    case CustomUpdate.ReturnCode.Static:
                        this.rdBtnReturnStaticCode.Checked = true;
                        this.nupStaticCode.Value = this._customUpdate.StaticCode;
                        break;
                    case CustomUpdate.ReturnCode.Variable:
                        this.rdBtnReturnVariable.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(_localizator.GetLocalizedString("ErrorLoadingTemplate") + "\r\n" + ex.Message);
            }
        }

        private void MoveSelectedAction(int delta)
        {
            GenericAction selectedAction = GetSelectedAction();

            if (selectedAction != null)
            {
                int index = this.tlpCustomActions.Controls.IndexOf(selectedAction) + delta;

                this._customUpdate.MoveAction(selectedAction, index);
                this.IsUnsaved = true;
                this.RefreshDisplay();
            }
        }

        private void DeleteAction()
        {
            GenericAction selectedAction = GetSelectedAction();

            if (selectedAction != null)
            {
                this._customUpdate.DeleteAction(selectedAction);
                this.IsUnsaved = true;
                this.RefreshDisplay();
            }
        }

        private void RefreshDisplay()
        {
            this.tlpCustomActions.SuspendLayout();
            this.tlpCustomActions.Controls.Clear();
            GenericAction[] allActions = this._customUpdate.GetAllActions();

            for (int i = 0; i < allActions.Length; i++)
            {
                this.tlpCustomActions.Controls.Add(allActions[i], 0, i);
            }

            this.tlpCustomActions.ResumeLayout();
            this.UpdateToolStripStatus();
        }

        private void UpdateToolStripStatus()
        {
            this.tssFilename.Text = this._currentFilename;
            this.tssSaved.Text = this.IsUnsaved ? _localizator.GetLocalizedString("unsaved") : _localizator.GetLocalizedString("saved");
        }

        private GenericAction GetSelectedAction()
        {
            GenericAction selectedAction = null;

            foreach (GenericAction action in this.tlpCustomActions.Controls)
            {
                if (action.IsSelected)
                {
                    selectedAction = action;
                    break;
                }
            }
            return selectedAction;
        }

        #endregion (private methods)

        #region (UI events)

        #region (Manage Templates)

        internal void ribBtnSaveTemplate_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(System.IO.Path.Combine(this._currentFolder, this._currentFilename)))
            {
                this.SaveAsXml(this._currentFolder);
                this.UpdateToolStripStatus();
            }
            else
                ribBtnSaveAsTemplate.PerformClick();
        }

        private void ribBtnSaveAsTemplate_Click(object sender, EventArgs e)
        {
            string fileFullPath = this.GetFileFullPath();
            if (fileFullPath != String.Empty)
            {
                this.SaveAsXml(fileFullPath);
                this.UpdateToolStripStatus();
            }
        }

        private void ribBtnLoadTemplate_Click(object sender, EventArgs e)
        {
            if (this.tlpCustomActions.Controls.Count != 0 && this.IsUnsaved)
            {
                DialogResult result = MessageBox.Show(_localizator.GetLocalizedString("DoYouWantToSaveCurrentCustomUpdate"), String.Empty, MessageBoxButtons.YesNoCancel);
                if (result == System.Windows.Forms.DialogResult.Cancel)
                    return;
                if (result == System.Windows.Forms.DialogResult.Yes)
                    this.ribBtnSaveTemplate.PerformClick();
            }

            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "CustomUpdate|*.CustAct|All Files|*.*";

            if (System.IO.Directory.Exists(Properties.Settings.Default.LastUsedFolder))
                openFileDlg.InitialDirectory = Properties.Settings.Default.LastUsedFolder;

            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this._customUpdate.ClearAllActions();
                this.RefreshDisplay();
                System.IO.FileInfo fileToOpen = new System.IO.FileInfo(openFileDlg.FileName);
                this._currentFolder = fileToOpen.DirectoryName;
                this._currentFilename = fileToOpen.Name;
                Properties.Settings.Default.LastUsedFolder = this._currentFolder;
                Properties.Settings.Default.Save();
                this.LoadFromXml(fileToOpen.FullName);
                this.IsUnsaved = false;
                this.UpdateToolStripStatus();
            }
        }

        #endregion (Manage Templates)

        private void AddAction(object sender, EventArgs e)
        {
            string typeName = String.Empty;
            if ((e as MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
            {
                try
                {
                    if ((sender as RibbonButton).Tag != null)
                        typeName = (sender as RibbonButton).Tag.ToString();

                    Type type = Type.GetType("CustomActions." + typeName + ",CustomActions");
                    GenericAction action = (GenericAction)Activator.CreateInstance(type);
                    this._customUpdate.AddAction(action);
                    this.AddNewActionToUI(action);
                }
                catch (Exception)
                {
                    MessageBox.Show(_localizator.GetLocalizedString("CannotCreateAnInstanceOfThisAction") + typeName + "(" + (sender as RibbonButton).ToString() + ")");
                }
            }
        }

        private void tlpCustomActions_DragEnter(object sender, DragEventArgs e)
        {
            if (tlpCustomActions.Controls.Count > 1 && e.Data.GetDataPresent(typeof(CustomActions.DragDropInfo)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tlpCustomActions_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(CustomActions.DragDropInfo)))
            {
                CustomActions.DragDropInfo dragDropInfo = (CustomActions.DragDropInfo)e.Data.GetData(typeof(CustomActions.DragDropInfo));
                CustomActions.GenericAction controlToMove = dragDropInfo.EmbededControl;
                Point mousePoint = tlpCustomActions.PointToClient(new Point(e.X, e.Y));
                CustomActions.GenericAction destinationControl = (CustomActions.GenericAction)tlpCustomActions.GetChildAtPoint(mousePoint);

                this._customUpdate.MoveAction(controlToMove, destinationControl);
                this.IsUnsaved = true;
                RefreshDisplay();
            }
        }

        private void ctxMnuRightClick_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (this.ctxMnuRightClick.Items.IndexOf(e.ClickedItem))
            {
                case 0:
                    this.MoveSelectedAction(-1);
                    break;
                case 1:
                    this.MoveSelectedAction(1);
                    break;
                case 3:
                    this.DeleteAction();
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this._customUpdate.GetActionCount() != 0)
            {
                if (!this._customUpdate.IsReturnCodeConfigurationOk())
                    MessageBox.Show(_localizator.GetLocalizedString("TheVariableIsNeverFill"));
                else if (this._customUpdate.GetActionWithReturnCode() == 1 && this._customUpdate.ReturnCodeMethod != CustomUpdate.ReturnCode.Variable)
                {
                    MessageBox.Show(_localizator.GetLocalizedString("ActionWithReturnCodeAndUpdateWithStaticMethod"));
                }
                else if (this._customUpdate.GetActionWithReturnCode() > 1)
                {
                    MessageBox.Show(_localizator.GetLocalizedString("MoreThanOneActionWithReturnCode"));
                }
                else
                {
                    string misconfiguratedAction = this._customUpdate.GetMisconfiguratedAction();
                    if (!string.IsNullOrEmpty(misconfiguratedAction))
                        MessageBox.Show(_localizator.GetLocalizedString("ThisActionIsNotProperlyConfigurated") + "\r\n" + misconfiguratedAction);
                    else
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
                MessageBox.Show(_localizator.GetLocalizedString("NoActionInThisCustomUpdate"));
        }

        private void returnCodeMethodChanged(object sender, EventArgs e)
        {
            this.nupStaticCode.Enabled = this.rdBtnReturnStaticCode.Checked;
            this._customUpdate.ReturnCodeMethod = this.rdBtnReturnStaticCode.Checked ? CustomUpdate.ReturnCode.Static : CustomUpdate.ReturnCode.Variable;
            this.IsUnsaved = true;
            this.UpdateToolStripStatus();
        }

        #endregion (UI events)

    }
}
