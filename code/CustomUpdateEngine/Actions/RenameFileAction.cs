using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class RenameFileAction : GenericAction
    {
        public RenameFileAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FullPath"))
                throw new ArgumentException("Unable to find token : FullPath");
            FullPath = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("NewName"))
                throw new ArgumentException("Unable to find token : NewName");
            NewName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string FullPath { get; private set; }
        public string NewName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RenameFileAction. FullPath = " + this.FullPath + " and NewName = " + this.NewName);

            try
            {
                this.FullPath = Tools.GetExpandedPath(this.FullPath);
                FileInfo fileToRename = new FileInfo(this.FullPath);
                if (fileToRename.Exists)
                {
                    FileStream newFile = File.Create(Path.Combine(fileToRename.DirectoryName, this.NewName));
                    newFile.Close();
                    File.Replace(fileToRename.FullName, newFile.Name, null);
                    FileInfo renamedFile = new FileInfo(newFile.Name);
                    Logger.Write(renamedFile.Exists ? "The file have been successfully renamed" : "The file have NOT been renamed");
                }
                else
                {
                    Logger.Write("The file to rename doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RenameFileAction.");
        }
    }
}
