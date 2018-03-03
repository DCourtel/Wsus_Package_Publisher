using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CustomUpdateEngine
{
    class CustomUpdateEngine
    {
        internal enum ErrorCodes
        {
            NoError = 0,
            ActionFileNotFound = 65535,
            ActionFileParsingError = 65534,
            UnableToExpandEnvironmentVariable = 65533,
            FileNotFoundException = 65532
        }

        internal struct Arguments
        {
            internal string ActionFilePath { get; set; }
            internal string DebugFilePath { get; set; }
        }

        private const string actionFile = @"\actionfile=";
        private const string debugFile = @"\debugfilefolder=";
        private static List<GenericElement> elements = new List<GenericElement>();
        private static List<VariableElement> variables = new List<VariableElement>();
        internal static Arguments arguments;

        static int Main(string[] args)
        {
            FinalResult = (int)ErrorCodes.NoError;
            arguments = GetArguments(args);
            Logger.Initialize(arguments.DebugFilePath, "CustomUpdateEngine.log", Logger.Destination.File);
            Logger.Write("========================================================================================================================");
            Logger.Write("Starting CustomUpdateEngine with ActionFile : " + arguments.ActionFilePath);
            
            if (!File.Exists(arguments.ActionFilePath))
                return (int)ErrorCodes.ActionFileNotFound;
            try
            {
                Logger.Write("Starting parsing ActionFile.");
                elements = ParseActionsFile(arguments.ActionFilePath);
            }
            catch (Exception ex)
            {
                Logger.Write("Error when parsing ActionFile : " + ex.Message);
                return (int)ErrorCodes.ActionFileParsingError;
            }

            Logger.Write("Running " + elements.Count + " Elements.");

            foreach (GenericElement element in elements)
            {
                Logger.Write("Running " + element.ToString());
                try
                {
                    if (element.GetType() == typeof(ReturnCodeElement))
                        FinalResult = ((ReturnCodeElement)element).Run(variables);
                    else
                        element.Run(variables);
                }
                catch (Exceptions.ExpandEnvironmentVariableException)
                {
                    return (int)ErrorCodes.UnableToExpandEnvironmentVariable;
                }
                catch (Exceptions.FileNotFoundException)
                {
                    return (int)ErrorCodes.FileNotFoundException;
                }
                catch (Exception ex)
                {
                    Logger.Write("**** " + ex.Message);
                }
            }
            return FinalResult;
        }

        #region {private Properties - Propriétés privées}

        private static int FinalResult { get; set; }
        
        #endregion {private Properties - Propriétés privées}

        #region {private Methods - Méthodes privées}

        private static Arguments GetArguments(string[] parameters)
        {
            Arguments arguments = new Arguments();
            arguments.DebugFilePath = Environment.ExpandEnvironmentVariables("%Temp%");

            foreach (string parameter in parameters)
            {
                if (parameter.ToLower().StartsWith(actionFile))
                {
                    arguments.ActionFilePath = parameter.Substring(parameter.IndexOf(actionFile) + actionFile.Length);
                }
                if (parameter.ToLower().StartsWith(debugFile))
                {
                    arguments.DebugFilePath = parameter.Substring(parameter.IndexOf(debugFile) + debugFile.Length);
                }
            }
            return arguments;
        }

        private static List<GenericElement> ParseActionsFile(string actionFilePath)
        {
            List<GenericElement> elements = new List<GenericElement>();

            XmlReader reader = XmlReader.Create(new StreamReader(actionFilePath));

            if (!reader.ReadToFollowing("CustomUpdate"))
                throw new Exception();
            while (reader.ReadToFollowing("Action"))
            {
                GenericElement tempElement = GetElementFromXML(reader.ReadOuterXml());
                if (tempElement.GetType() == typeof(VariableElement))
                    variables.Add((VariableElement)tempElement);
                else
                    elements.Add(tempElement);
            }
            return elements;
        }

        private static GenericElement GetElementFromXML(string xmlAction)
        {
            GenericElement element = new GenericElement();

            if (!string.IsNullOrEmpty(arguments.DebugFilePath))
                Logger.Write("Get Element from : " + xmlAction);

            XmlReader reader = XmlReader.Create(new StringReader(xmlAction));
            if (!reader.ReadToFollowing("ElementType"))
                throw new Exception();
            string elementType = reader.ReadString();

            switch (elementType)
            {
                case "CustomUpdateElements.VariableElement":
                    element = new VariableElement(xmlAction);
                    break;
                case "CustomUpdateElements.ExecutableElement":
                    element = new ExecutableElement(xmlAction);
                    break;
                case "CustomUpdateElements.RegistryElement":
                    element = new RegistryElement(xmlAction);
                    break;
                case "CustomUpdateElements.RegistryKeyElement":
                    element = new RegistryKeyElement(xmlAction);
                    break;
                case "CustomUpdateElements.ReturnCodeElement":
                    element = new ReturnCodeElement(xmlAction);
                    break;
                case "CustomUpdateElements.ServiceElement":
                    element = new ServiceElement(xmlAction);
                    break;
                case "CustomUpdateElements.ScriptElement":
                    element = new ScriptElement(xmlAction);
                    break;
                case "CustomUpdateElements.PowerManagementElement":
                    element = new PowerManagementElement(xmlAction);
                    break;
                case "CustomUpdateElements.WaitElement":
                    element = new WaitElement(xmlAction);
                    break;
                case "CustomUpdateElements.KillProcessElement":
                    element = new KillProcessElement(xmlAction);
                    break;
                case "CustomUpdateElements.TextFileElement":
                    element = new TextFileElement(xmlAction);
                    break;
                case "CustomUpdateElements.FolderElement":
                    element = new FolderElement(xmlAction);
                    break;
                case "CustomUpdateElements.FileElement":
                    element = new FileElement(xmlAction);
                    break;
                default:
                    break;
            }

            return element;
        }
        
        #endregion {private Methods - Méthodes privées}
    }
}
