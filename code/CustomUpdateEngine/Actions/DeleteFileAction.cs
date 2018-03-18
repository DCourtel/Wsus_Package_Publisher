using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class DeleteFileAction : GenericAction
    {
        public DeleteFileAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FullPath"))
                throw new ArgumentException("Unable to find token : FullPath");
            FullPath = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string FullPath { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running DeleteFileAction. FullPath = " + this.FullPath);

            try
            {
                this.FullPath = Tools.GetExpandedPath(this.FullPath);
                FileInfo fileToDelete = new FileInfo(this.FullPath);
                if (fileToDelete.Exists)
                {
                    fileToDelete.Delete();
                    fileToDelete.Refresh();
                    Logger.Write(fileToDelete.Exists ? "Unable to delete the file." : "The file has been successfully deleted.");
                }
                else
                    Logger.Write("The file doesn't exists.");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of DeleteFileAction.");
        }
    }
}
