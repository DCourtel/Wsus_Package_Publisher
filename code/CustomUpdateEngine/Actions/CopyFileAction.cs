using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class CopyFileAction : GenericAction
    {
        public CopyFileAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("SourceFile"))
                throw new ArgumentException("Unable to find token : SourceFile");
            SourceFile = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("DestinationFolder"))
                throw new ArgumentException("Unable to find token : DestinationFolder");
            DestinationFolder = reader.ReadElementContentAsString();

            LogCompletion();
        }

        public string SourceFile { get; private set; }
        public string DestinationFolder { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running CopyFileAction. SourceFile = " + this.SourceFile + " and DestinationFolder = " + this.DestinationFolder);

            try
            {
                this.SourceFile = Tools.GetExpandedPath(this.SourceFile);
                this.DestinationFolder = Tools.GetExpandedPath(this.DestinationFolder);
                FileInfo sourceFile = new FileInfo(this.SourceFile);
                DirectoryInfo destinationFolder = new DirectoryInfo(this.DestinationFolder);

                if (!destinationFolder.Exists)
                {
                    Logger.Write("Creating : " + this.DestinationFolder);
                    destinationFolder.Create();
                }

                FileInfo destinationFile = sourceFile.CopyTo(Path.Combine(this.DestinationFolder, sourceFile.Name), true);
                if (destinationFile.Exists)
                { Logger.Write("Successfully copied : " + this.SourceFile + " to : " + this.DestinationFolder); }
                else
                { Logger.Write("Unable to copy : " + this.SourceFile + " to : " + this.DestinationFolder); }
            }
            catch (Exception ex)
            {
                Logger.Write("Failed to copy the file : " + this.SourceFile + " to : " + this.DestinationFolder + "\r\n" + ex.Message);
            }
            Logger.Write("End of CopyFileAction.");
        }
    }
}
