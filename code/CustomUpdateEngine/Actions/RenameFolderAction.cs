using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class RenameFolderAction : GenericAction
    {
        public RenameFolderAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FolderPath"))
                throw new ArgumentException("Unable to find token : FolderPath");
            FolderPath = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("NewName"))
                throw new ArgumentException("Unable to find token : NewName");
            NewName = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string FolderPath { get; private set; }
        public string NewName { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running RenameFolderAction. FolderPath = " + this.FolderPath + " and NewName = " + this.NewName);

            try
            {
                this.FolderPath = Tools.GetExpandedPath(this.FolderPath);
                DirectoryInfo oldDirectory = new DirectoryInfo(this.FolderPath);
                if (oldDirectory.Exists)
                {
                    oldDirectory.MoveTo(Path.Combine(oldDirectory.Parent.FullName, this.NewName));
                    DirectoryInfo newDirectory = new DirectoryInfo(this.NewName);
                    Logger.Write(newDirectory.Exists ? "The folder have been successfully renamed" : "The folder have NOT been renamed");
                }
                else
                { Logger.Write("The folder to rename doesn't exists."); }
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of RenameFolderAction.");
        }
    }
}
