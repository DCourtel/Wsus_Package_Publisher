using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CustomUpdateEngine
{
    internal class ReturnCodeElement : GenericElement
    {
        public enum MethodType
        {
            Undefined,
            Static,
            Variable
        }

        internal ReturnCodeElement(string xmlFragment)
        {
            Logger.Write("Initializing ReturnCodeElement with : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("ReturnCodeMethod"))
                throw new Exception();
            ReturnCodeMethod = (MethodType)Enum.Parse(typeof(MethodType), reader.ReadString(), true);
            if (!reader.ReadToFollowing("StaticReturnCode"))
                throw new Exception();
            StaticReturnCode = reader.ReadElementContentAsInt();
            if (!reader.ReadToFollowing("Variable"))
                throw new Exception();
            if (!reader.IsEmptyElement)
                Variable = new Guid(reader.ReadString());
        }

        private MethodType ReturnCodeMethod { get; set; }
        private int StaticReturnCode { get; set; }
        private Guid Variable { get; set; }

        internal new int Run(List<VariableElement> variables)
        {
            Logger.Write("Return Code Method is : " + ReturnCodeMethod.ToString());

            switch (ReturnCodeMethod)
            {
                case MethodType.Static:
                    Logger.Write("Returning : " + StaticReturnCode.ToString());
                    return StaticReturnCode;
                case MethodType.Variable:
                    {
                        foreach (VariableElement variable in variables)
                        {
                            if (variable.VarID == Variable && variable.VarType == VariableElement.VariableType.Int)
                            {
                                Logger.Write("Returning :  " + variable.IntValue.ToString());
                                return variable.IntValue;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            Logger.Write("Returning 0 (default value)");

            return 0;
        }
    }
}
