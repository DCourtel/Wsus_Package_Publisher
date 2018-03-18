using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class CreateFolderAction : GenericAction
    {
        public CreateFolderAction(string xmlFragment)
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
            Logger.Write("Running CreateFolder. FullPath = " + this.FullPath);

            try
            {
                this.FullPath = Tools.GetExpandedPath(this.FullPath);
                DirectoryInfo destinationFolder = new DirectoryInfo(this.FullPath);

                if (!destinationFolder.Exists)
                {
                    destinationFolder.Create();
                    destinationFolder.Refresh();
                    Logger.Write(destinationFolder.Exists ? "Successfully created : " + this.FullPath : "Unable to create : " + this.FullPath);
                }
                else
                { Logger.Write("The folder already exists."); }
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to create the folder : " + this.FullPath + "\r\n" + ex.Message);
            }
            Logger.Write("End of CreateFolder.");
        }
    }
}
