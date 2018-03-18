using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class DeleteFolderAction:GenericAction
    {
        public DeleteFolderAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FolderPath"))
                throw new ArgumentException("Unable to find token : FolderPath");
            FolderPath = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string FolderPath { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running DeleteFolderAction. FolderPath = " + this.FolderPath);

            try
            {
                this.FolderPath = Tools.GetExpandedPath(this.FolderPath);
                DirectoryInfo folderToDelete = new DirectoryInfo(this.FolderPath);
                if(folderToDelete.Exists)
                {
                    folderToDelete.Delete(true);
                    folderToDelete.Refresh();
                    Logger.Write(folderToDelete.Exists ? "Unable to delete the folder." : "The folder has been successfully deleted.");
                }
                else
                {
                    Logger.Write("The folder doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to delete the folder : " + this.FolderPath + "\r\n" + ex.Message);
            }
            Logger.Write("End of DeleteFolderAction.");
        }
    }
}
