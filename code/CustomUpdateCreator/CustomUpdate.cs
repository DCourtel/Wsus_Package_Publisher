using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using CustomActions;

namespace CustomUpdateCreator
{
    public class CustomUpdate
    {
        internal enum ReturnCode
        {
            Static,
            Variable
        }

        private List<GenericAction> _actions = new List<GenericAction>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomUpdate()
        {
            this.IsUIEnable = false;
            this.ReturnCodeMethod = ReturnCode.Static;
            this.StaticCode = 0;
        }

        /// <summary>
        /// Initialize a new instance of CustomUpdate from a XML file.
        /// </summary>
        /// <param name="fullFilePath">Full path to a XML file that describe the CustomUpdate.</param>
        public CustomUpdate(string fullFilePath)
        {
            this.IsUIEnable = false;
            this.Load(fullFilePath);
            this.ReturnCodeMethod = ReturnCode.Static;
            this.StaticCode = 0;
        }

        #region (public properties)

        /// <summary>
        /// Gets if at least one Action refers to the user profile.
        /// </summary>
        public bool RefersToUserProfile
        {
            get
            {
                foreach (GenericAction action in this._actions)
                {
                    if (action.RefersToUserProfile)
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets if at least one Action refers to HKCU
        /// </summary>
        public bool RefersToHKeyCurrentUser
        {
            get
            {
                foreach (GenericAction action in this._actions)
                {
                    if (action.RefersToHKeyCurrentUser)
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Gets or Sets if this instance has been instanciate by FrmCustomUpdateCreator (has UI) or by a Powershell Script (no UI)
        /// </summary>
        internal bool IsUIEnable { get; set; }

        /// <summary>
        /// Gets or Sets the method to return the ReturnCode at the end of this CustomUpdate
        /// </summary>
        internal ReturnCode ReturnCodeMethod { get; set; }

        /// <summary>
        /// Gets or Sets the Static code to return in case the ReturnCode method was define to 'Static'
        /// </summary>
        internal int StaticCode { get; set; }

        #endregion (public properties)

        #region (public methods)

        /// <summary>
        /// Gets an XML formatted string describing this CustomUpdate. 
        /// </summary>
        /// <param name="isStaticCode">Set whether or not the Return Code is a Static Code.</param>
        /// <param name="staticCode">The value of the Static Code. If the Return Code is not a 'Static Code' type, this value will not be used.</param>
        /// <returns>A string representing this CustomUpdate</returns>
        public string GetXmlFormattedActions(bool isStaticCode, int staticCode)
        {
            string xmlFormattedActions = "<CustomUpdate Version=\"2\">\r\n";

            foreach (GenericAction action in this._actions)
            {
                xmlFormattedActions += action.GetXMLAction() + "\r\n";
            }

            xmlFormattedActions += this.GetRetunCodeXmlConfiguration(isStaticCode, staticCode);
            xmlFormattedActions += "</CustomUpdate>";

            return xmlFormattedActions;
        }

        private string GetRetunCodeXmlConfiguration(bool isStaticCode, int staticCode)
        {
            string xmlConfiguration = "<Action>\r\n<ElementType>CustomActions.ReturnCode</ElementType>\r\n<Method>";

            if (isStaticCode)
                return xmlConfiguration + "Static</Method>\r\n<Code>" + staticCode.ToString() + "</Code>\r\n</Action>\r\n";
            else
                return xmlConfiguration + "Variable</Method>\r\n</Action>\r\n";
        }

        /// <summary>
        /// Add a GenericAction at the end of the list.
        /// </summary>
        /// <param name="actionToAdd">GenericAction to add to the list.</param>
        public void AddAction(GenericAction actionToAdd)
        {
            if (actionToAdd != null)
            {
                this._actions.Add(actionToAdd);
                actionToAdd.IsTemplate = false;
            }
        }

        /// <summary>
        /// Remove a GenericAction for the list.
        /// </summary>
        /// <param name="actionToDelete">GenericAction to remove.</param>
        public void DeleteAction(GenericAction actionToDelete)
        {
            this._actions.Remove(actionToDelete);
        }

        /// <summary>
        /// Move an Action to a specific place in the list
        /// </summary>
        /// <param name="actionToMove">Action to move.</param>
        /// <param name="index">A zero-based index where to place the Action.</param>
        public void MoveAction(GenericAction actionToMove, int index)
        {
            this._actions.Remove(actionToMove);
            if (index >= this._actions.Count)
            {
                this._actions.Add(actionToMove);
            }
            else
            {
                List<GenericAction> actionList = new List<GenericAction>();

                for (int i = 0; i < this._actions.Count; i++)
                {
                    GenericAction tmpAction = this._actions[i];
                    if (i == index)
                    {
                        actionList.Add(actionToMove);
                    }
                    actionList.Add(tmpAction);
                }
                this._actions.Clear();
                this._actions = actionList;
            }
        }

        /// <summary>
        /// Remove all Actions from this CustomUpdate.
        /// </summary>
        public void ClearAllActions()
        {
            this._actions.Clear();
        }

        /// <summary>
        /// Save the current CustomUpdate as a XML file. If the file already exists, it is overwriten.
        /// </summary>
        /// <param name="filePath">Full path to the file where to save.</param>
        /// <param name="isStaticCode">Set whether or not the Return Code is a Static Code.</param>
        /// <param name="staticCode">The value of the Static Code. If the Return Code is not a 'Static Code' type, this value will not be used.</param>
        public void Save(string filePath, bool isStaticCode, int staticCode)
        {
            string xmlFormattedActions = this.GetXmlFormattedActions(isStaticCode, staticCode);

            using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.Unicode))
            {
                writer.Write(xmlFormattedActions);
            }
        }

        /// <summary>
        /// Read a XML file and Initialize this CustomUpdate with its content
        /// </summary>
        /// <param name="fullFilePath">Full path to the file to read.</param>
        public void Load(string fullFilePath)
        {
            FileInfo file = new FileInfo(fullFilePath);

            StreamReader reader = file.OpenText();
            this.ParseXmlTemplateFile(reader);
            reader.Close();
        }

        private void ParseXmlTemplateFile(StreamReader xmlStream)
        {
            XmlReader reader = XmlReader.Create(xmlStream);

            if (!reader.ReadToFollowing("CustomUpdate"))
                throw new Exception(Localizator.Getinstance().GetLocalizedString("CustomUpdateFileBadlyFormated"));
            else
            {
                int version = int.Parse(reader.GetAttribute("Version"));
                if (version != 2)
                    throw new Exception("Wrong version. Expected version = 2, this version = " + version);
                while (reader.ReadToFollowing("Action"))
                {
                    reader.ReadToFollowing("ElementType");
                    GenericAction tempAction = this.GetElementFromXML(reader);

                    if (tempAction != null)
                    {
                        try
                        {
                            Dictionary<string, string> properties = this.GetPropertiesFromXml(reader);
                            tempAction.InitializeProperties(properties);
                        }
                        catch (Exception)
                        {
                            if (this.IsUIEnable)
                                System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("AnErrorOccursWhileIntializingAction") + tempAction.ToString() + "\r\n" + Localizator.Getinstance().GetLocalizedString("ItWillHaveDefaultValues"));
                            else
                                throw new Exception(Localizator.Getinstance().GetLocalizedString("AnErrorOccursWhileIntializingAction") + tempAction.ToString() + "\r\n" + Localizator.Getinstance().GetLocalizedString("ItWillHaveDefaultValues"));
                        }
                            this.AddAction(tempAction);
                    }
                }
                reader.Close();
            }
        }

        private GenericAction GetElementFromXML(XmlReader reader)
        {
            GenericAction element = null;

            string elementType = reader.ReadElementContentAsString();

            switch (elementType)
            {
                case "CustomActions.AddRegKeyAction":
                    element = new AddRegKeyAction();
                    break;
                case "CustomActions.AddRegValueAction":
                    element = new AddRegValueAction();
                    break;
                case "CustomActions.ChangeRegDataAction":
                    element = new ChangeRegDataAction();
                    break;
                case "CustomActions.ChangeServiceAction":
                    element = new ChangeServiceAction();
                    break;
                case "CustomActions.CopyFileAction":
                    element = new CopyFileAction();
                    break;
                case "CustomActions.CreateFolderAction":
                    element = new CreateFolderAction();
                    break;
                case "CustomActions.CreateShortcutAction":
                    element = new CreateShortcutAction();
                    break;
                case "CustomActions.CreateTextFileAction":
                    element = new CreateTextFileAction();
                    break;
                case "CustomActions.DeleteFileAction":
                    element = new DeleteFileAction();
                    break;
                case "CustomActions.DeleteFolderAction":
                    element = new DeleteFolderAction();
                    break;
                case "CustomActions.DeleteRegKeyAction":
                    element = new DeleteRegKeyAction();
                    break;
                case "CustomActions.DeleteRegValueAction":
                    element = new DeleteRegValueAction();
                    break;
                case "CustomActions.DeleteTaskAction":
                    element = new DeleteTaskAction();
                    break;
                case "CustomActions.ExecutableAction":
                    element = new ExecutableAction();
                    break;
                case "CustomActions.ImportRegFileAction":
                    element = new ImportRegFileAction();
                    break;
                case "CustomActions.KillProcessAction":
                    element = new KillProcessAction();
                    break;
                case "CustomActions.RebootAction":
                    element = new RebootAction();
                    break;
                case "CustomActions.RegisterDLLAction":
                    element = new RegisterDLLAction();
                    break;
                case "CustomActions.RenameFileAction":
                    element = new RenameFileAction();
                    break;
                case "CustomActions.RenameFolderAction":
                    element = new RenameFolderAction();
                    break;
                case "CustomActions.RenameRegKeyAction":
                    element = new RenameRegKeyAction();
                    break;
                case "CustomActions.RenameRegValueAction":
                    element = new RenameRegValueAction();
                    break;
                case "CustomActions.RunPowershellScriptAction":
                    element = new RunPowershellScriptAction();
                    break;
                case "CustomActions.RunVbScriptAction":
                    element = new RunVbScriptAction();
                    break;
                case "CustomActions.ShutdownAction":
                    element = new ShutdownAction();
                    break;
                case "CustomActions.StartServiceAction":
                    element = new StartServiceAction();
                    break;
                case "CustomActions.StopServiceAction":
                    element = new StopServiceAction();
                    break;
                case "CustomActions.UninstallMsiProductByGuidAction":
                    element = new UninstallMsiProductByGuidAction();
                    break;
                case "CustomActions.UninstallMsiProductByNameAction":
                    element = new UninstallMsiProductByNameAction();
                    break;
                case "CustomActions.UnregisterDLLAction":
                    element = new UnregisterDLLAction();
                    break;
                case "CustomActions.UnregisterServiceAction":
                    element = new UnregisterServiceAction();
                    break;
                case "CustomActions.WaitAction":
                    element = new WaitAction();
                    break;
                case "CustomActions.InstallMsiAction":
                    element = new InstallMsiAction();
                    break;
                case "CustomActions.ReturnCode":
                    try
                    {
                        this.SetReturnCodeFromXml(reader);
                    }
                    catch (Exception ex)
                    {
                        if (this.IsUIEnable)
                            System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile") + "\r\n" + ex.Message);
                        else
                            throw new Exception(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile") + "\r\n" + ex.Message);
                    }
                    break;
                default:
                    if (this.IsUIEnable)
                        System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("ThisActionHasNotBeenRecognized") + elementType);
                    else
                        throw new Exception(Localizator.Getinstance().GetLocalizedString("ThisActionHasNotBeenRecognized") + elementType);
                    break;
            }

            return element;
        }

        private void SetReturnCodeFromXml(XmlReader reader)
        {
            if (reader.ReadToFollowing("Method"))
            {
                string method = reader.ReadElementContentAsString();
                switch (method)
                {
                    case "Variable":
                        this.ReturnCodeMethod = ReturnCode.Variable;
                        break;
                    case "Static":
                        this.ReturnCodeMethod = ReturnCode.Static;
                        if (reader.ReadToFollowing("Code"))
                        {
                            int code = reader.ReadElementContentAsInt();
                            this.StaticCode = code;
                        }
                        else
                            if (this.IsUIEnable)
                                System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
                            else
                                throw new Exception(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
                        break;
                    default:
                        if (this.IsUIEnable)
                            System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
                        else
                            throw new Exception(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
                        break;
                }
            }
            else
            {
                if (this.IsUIEnable)
                    System.Windows.Forms.MessageBox.Show(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
                else
                    throw new Exception(Localizator.Getinstance().GetLocalizedString("UnableToSetReturnCodeFromXmlFile"));
            }
        }

        private Dictionary<string, string> GetPropertiesFromXml(XmlReader reader)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
                                    
            while (reader.MoveToContent() == XmlNodeType.Element)
            {
                properties.Add(reader.Name, reader.ReadElementContentAsString());
            }

            return properties;
        }

        internal bool IsReturnCodeConfigurationOk()
        {
            return (this.ReturnCodeMethod == CustomUpdate.ReturnCode.Static) || (this.GetActionWithReturnCode() != 0);
        }

        internal int GetActionWithReturnCode()
        {
            int count = 0;
            foreach (GenericAction action in this._actions)
            {
                if (action.GetType().GetInterfaces().Contains(typeof(CustomActions.IStoreReturnCodeToVariable)) && (action as CustomActions.IStoreReturnCodeToVariable).StoreToVariable)
                    count++;
            }
            return count;
        }

        internal string GetMisconfiguratedAction()
        {
            foreach (GenericAction action in this._actions)
            {
                if (action.ConfigurationState != GenericAction.ConfigurationStates.Configured)
                {
                    return action.ToString();
                }

            }
            return String.Empty;
        }

        internal int GetActionCount()
        {
            return this._actions.Count;
        }

        internal GenericAction[] GetAllActions()
        {
            return this._actions.ToArray();
        }

        internal void MoveAction(GenericAction controlToMove, GenericAction destinationControl)
        {
            if (destinationControl == null)
            {
                this._actions.Remove(controlToMove);
                this._actions.Add(controlToMove);
            }
            else
                if (!controlToMove.Equals(destinationControl))
                {
                    this._actions.Remove(controlToMove);

                    List<GenericAction> ctrList = new List<GenericAction>();

                    for (int i = 0; i < this._actions.Count; i++)
                    {
                        GenericAction tmpControl = this._actions[i];
                        if (tmpControl.Equals(destinationControl))
                        {
                            ctrList.Add(controlToMove);
                        }
                        ctrList.Add(tmpControl);
                    }
                    this._actions.Clear();
                    this._actions.AddRange(ctrList.ToArray());
                }
        }

        #endregion (public methods)
    }
}
