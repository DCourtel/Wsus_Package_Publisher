using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    public class ReturnCodeAction : GenericAction
    {
        public enum ReturnCodeMethod
        {
            Static, Variable
        }

        public ReturnCodeAction(string xmlFragment)
        {
            LogInitialization(xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Method"))
                throw new ArgumentException("Unable to find token : Method");
            ReturnMethod = (ReturnCodeMethod)Enum.Parse(typeof(ReturnCodeMethod), reader.ReadString(), true);
            if (this.ReturnMethod == ReturnCodeMethod.Static)
            {
                if (!reader.ReadToFollowing("Code"))
                    throw new ArgumentException("Unable to find token : Code");
                ReturnValue = reader.ReadElementContentAsInt();
            }
            LogCompletion();
        }

        public ReturnCodeAction()
        {
            this.ReturnMethod = ReturnCodeMethod.Static;
            this.ReturnValue = 0;
        }

        #region (Properties)

        public ReturnCodeMethod ReturnMethod { get; set; }

        public int ReturnValue { get; set; }

        #endregion (Properties)
    }
}
