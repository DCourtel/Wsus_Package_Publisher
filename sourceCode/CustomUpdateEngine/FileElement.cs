using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using CustomUpdateEngine.Exceptions;

namespace CustomUpdateEngine
{
    class FileElement:GenericElement
    {
        private enum ActionType
        {
            Undefined,
            Copy,
            Delete,
            Rename
        }
        
        internal FileElement(string xmlFragment)
        {
            Logger.Write("Get FileElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FileAction"))
                throw new Exception();
            FileAction = (ActionType)Enum.Parse(typeof(ActionType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("FileName"))
                throw new Exception();
            FileName = reader.ReadString();
            if (!reader.ReadToFollowing("Destination"))
                throw new Exception();
            Destination = reader.ReadString();
            if (!reader.ReadToFollowing("NewName"))
                throw new Exception();
            NewName = reader.ReadString();

            Logger.Write("End of Initializing of FileElement.");
        }

        private ActionType FileAction { get; set; }
        private string FileName { get; set; }
        private string Destination { get; set; }
        private string NewName { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running FolderElement.");

            FileName = Utilities.GetExpandedPath(FileName);
            if (!string.IsNullOrEmpty(Destination))
                Destination = Utilities.GetExpandedPath(Destination);

            switch (FileAction)
            {
                case ActionType.Copy:
                    CopyFile();
                    break;
                case ActionType.Delete:
                    DeleteFile();
                    break;
                case ActionType.Rename:
                    RenameFile();
                    break;
                default:
                    break;
            }
        }

        private void CopyFile()
        {
            Logger.Write("CopyFile");

            try
            {
                if (File.Exists(FileName))
                {
                    string destinationPath = new FileInfo(Destination).Directory.FullName;
                    if (!Directory.Exists(destinationPath))
                        Directory.CreateDirectory(destinationPath);

                    File.Copy(FileName, Destination, false);
                    Logger.Write(FileName + " have been copied to " + Destination);
                }
                else
                {
                    Logger.Write(FileName + " doesn't exists");
                }
            }
            catch (Exception ex) { Logger.Write(ex.Message); }

            Logger.Write("End of CopyFile");
        }

        private void DeleteFile()
        {
            Logger.Write("DeleteFile");

            if (File.Exists(FileName))
            {
                try
                {
                    File.Delete(FileName);
                    Logger.Write(FileName + " have been deleted");
                }
                catch (Exception ex) { Logger.Write(ex.Message); }
            }
            else
            {
                Logger.Write(FileName + " doesn't exists.");
            }
            Logger.Write("End of DeleteFile");
        }

        private void RenameFile()
        {
            Logger.Write("RenameFile");

            try
            {
                if(File.Exists(FileName))
                {
                    FileInfo oldFile = new FileInfo(FileName);
                    string newFile = oldFile.DirectoryName + "\\" + NewName;

                    FileStream newFileCreator = File.Create(newFile);
                    newFileCreator.Flush();
                    newFileCreator.Close();
                    File.Replace(FileName, newFile, null);
                    Logger.Write(FileName + " have been renamed");
                }
                else
                {
                    Logger.Write(FileName + " doesn't exists");
                }
            }
            catch (Exception ex) { Logger.Write(ex.Message); }
            Logger.Write("End of RenameFile");
        }
    }
}
