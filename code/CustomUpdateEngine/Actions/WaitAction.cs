using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;

namespace CustomUpdateEngine
{
    public class WaitAction:GenericAction
    {
        public WaitAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("AmountOfTime"))
                throw new ArgumentException("Unable to find token : AmountOfTime");
            SecondToWait = reader.ReadElementContentAsInt();

            LogCompletion();
        }

        public int SecondToWait { get; set; }

        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running WaitElement.");

            System.Threading.Thread.Sleep(SecondToWait * 1000);

            Logger.Write("End waiting.");
        }        
    }
}
