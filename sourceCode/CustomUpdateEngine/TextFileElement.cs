using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using CustomUpdateEngine.Exceptions;

namespace CustomUpdateEngine
{
    internal class TextFileElement:GenericElement
    {
        internal TextFileElement(string xmlFragment)
        {
            Logger.Write("Get TextFileElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Filepath"))
                throw new Exception();
            Filepath = reader.ReadString();
            if (!reader.ReadToFollowing("Filename"))
                throw new Exception();
            Filename = reader.ReadString();
            if (!reader.ReadToFollowing("FileContent"))
                throw new Exception();
            FileContent = reader.ReadString();

            Logger.Write("End of Initializing of TextFileElement.");
        }

        private string Filepath { get; set; }
        private string Filename { get; set; }
        private string FileContent { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running TextFileElement." );

            try
            {
                Filepath = Utilities.GetExpandedPath(Filepath);
                if (!Directory.Exists(Filepath))
                    Directory.CreateDirectory(Filepath);
                StreamWriter writer = new StreamWriter(Filepath + "\\" + Filename);
                writer.Write(FileContent);
                writer.Flush();
                writer.Close();
                Logger.Write("Text file writed successfully.");
            }
            catch (Exception ex)
            {
                Logger.Write("Error when writing the text file.\r\n" + ex.Message);
            }

            Logger.Write("End of TextFileElement.");
        }
    }
}
