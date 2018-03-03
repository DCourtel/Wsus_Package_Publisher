using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    internal class VariableElement : GenericElement
    {
        internal enum VariableType
        {
            Int,
            String,
            Undefined
        }

        internal VariableElement(string xmlFragment)
        {
            Logger.Write("Get VariableElement from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("Name"))
                throw new Exception();
            VarName = reader.ReadString();
            if (!reader.ReadToFollowing("Type"))
                throw new Exception();
            VarType = (VariableType)Enum.Parse(typeof(VariableType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("ID"))
                throw new Exception();
            VarID = new Guid(reader.ReadString());
        }

        internal string VarName { get; set; }
        internal VariableType VarType { get; set; }
        internal Guid VarID { get; set; }
        internal int IntValue { get; set; }
        internal string StringValue { get; set; }

        internal override void Run(List<VariableElement> variables) { }
    }
}
