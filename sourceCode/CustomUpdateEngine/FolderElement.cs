using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using CustomUpdateEngine.Exceptions;

namespace CustomUpdateEngine
{
    class FolderElement : GenericElement
    {
        private enum ActionType
        {
            Undefined,
            Add,
            Delete,
            Rename
        }

        internal FolderElement(string xmlFragment)
        {
            Logger.Write("Get FolderElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FolderAction"))
                throw new Exception();
            FolderAction = (ActionType)Enum.Parse(typeof(ActionType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("FolderName"))
                throw new Exception();
            FolderName = reader.ReadString();
            if (!reader.ReadToFollowing("NewName"))
                throw new Exception();
            NewName = reader.ReadString();

            Logger.Write("End of Initializing of FolderElement.");
        }

        private ActionType FolderAction { get; set; }
        private string FolderName { get; set; }
        private string NewName { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running FolderElement.");

            FolderName = Utilities.GetExpandedPath(FolderName);

            switch (FolderAction)
            {
                case ActionType.Undefined:
                    break;
                case ActionType.Add:
                    CreateFolder();
                    break;
                case ActionType.Delete:
                    DeleteFolder();
                    break;
                case ActionType.Rename:
                    RenameFolder();
                    break;
                default:
                    break;
            }
        }

        private void CreateFolder()
        {
            Logger.Write("CreateFolder");

            if (!Directory.Exists(FolderName))
            {
                try
                {
                    Directory.CreateDirectory(FolderName);
                }
                catch (Exception ex) { Logger.Write(ex.Message); }
            }
            else
            {
                Logger.Write("Folder " + FolderName + " already extists.");
            }
        }

        private void DeleteFolder()
        {
            Logger.Write("DeleteFolder");

            if (Directory.Exists(FolderName))
            {
                try
                {
                    Directory.Delete(FolderName, true);
                }
                catch (Exception ex) { Logger.Write(ex.Message); }
            }
            else
            {
                Logger.Write("Folder " + FolderName + " doesn't extists.");
            }
        }

        private void RenameFolder()
        {
            Logger.Write("RenameFolder");

            string parentFolder = new DirectoryInfo(FolderName).Parent.FullName;
            string newFolder = parentFolder + "\\" + NewName;

            if (!Directory.Exists(newFolder))
            {
                if (Directory.Exists(FolderName))
                {
                    try
                    {
                        DirectoryInfo oldDirectory = new DirectoryInfo(FolderName);
                        oldDirectory.MoveTo(newFolder);
                    }
                    catch (Exception ex) { Logger.Write(ex.Message); }
                }
                else
                {
                    Logger.Write("Folder " + FolderName + " doesn't extists.");
                }
            }
            else
            {
                Logger.Write("Folder " + newFolder + " already exists.");
            }
        }
    }
}
