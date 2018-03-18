using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CustomUpdateEngine
{
    public class CreateTextFileAction : GenericAction
    {
        public CreateTextFileAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);
            
            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("FilePath"))
                throw new ArgumentException("Unable to find token : FilePath");
            FilePath = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Filename"))
                throw new ArgumentException("Unable to find token : Filename");
            Filename = reader.ReadElementContentAsString();
            if (!reader.ReadToFollowing("Content"))
                throw new ArgumentException("Unable to find token : Content");
            Content = reader.ReadElementContentAsString().Replace("\n", Environment.NewLine);

            LogCompletion();
        }

        public string FilePath { get; private set; }
        public string Filename { get; private set; }
        public string Content { get; private set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running CreateTextFileAction. FilePath = " + this.FilePath + " and Filename = " + this.Filename);

            try
            {
                this.FilePath = Tools.GetExpandedPath(this.FilePath);
                DirectoryInfo folder = new DirectoryInfo(this.FilePath);

                if(!folder.Exists)
                {
                    Logger.Write("The folder doesn't exists. Creating the folder…");
                    folder.Create();
                    Logger.Write("Folder successfully created.");
                }
                StreamWriter writer = new StreamWriter(Path.Combine(this.FilePath, this.Filename), false, Encoding.Unicode);
                writer.Write(this.Content);
                writer.Close();

                Logger.Write("File successfully written");
            }
            catch (Exception ex)
            {
                Logger.Write("**** An error occurs : " + ex.Message);
            }

            Logger.Write("End of CreateTextFileAction.");
        }
    }
}
