using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    class WaitElement : GenericElement
    {

        internal WaitElement(string xmlFragment)
        {
            Logger.Write("Initializing WaitElement with : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Wait"))
                throw new Exception();
            SecondToWait = reader.ReadElementContentAsInt();

            Logger.Write("End of Initializing of WaitElement.");
        }

        private int SecondToWait { get; set; }

        internal override void Run(List<VariableElement> variables)
        {
            Logger.Write("Running WaitElement.");

            System.Threading.Thread.Sleep(SecondToWait * 1000);

            Logger.Write("End waiting.");
        }
    }
}
