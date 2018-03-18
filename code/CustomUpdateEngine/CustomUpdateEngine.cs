using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CustomUpdateEngine
{
    public class CustomUpdateEngine
    {
        public enum ErrorCodes
        {
            ActionFileNotFound = 65535,
            ActionFileParsingError = 65534
        }

        public struct Arguments
        {
            public string ActionFilePath { get; set; }
            public string DebugFilePath { get; set; }
        }

        private const string _actionFile = @"/actionfile=";
        private const string _debugFile = @"/debugfilefolder=";
        private static List<GenericAction> _actions = new List<GenericAction>();
        private static ReturnCodeAction _returnCode;

        public static int Main(string[] args)
        {
            Arguments _arguments = GetArguments(args);

#if(DEBUG)
            string logFile = Path.Combine(_arguments.DebugFilePath, "CustomUpdateEngine.log");
            if (System.IO.File.Exists(logFile))
            {
                try
                {
                    System.IO.File.Delete(logFile);
                }
                catch (Exception) { }
            }
#endif
            Logger.Initialize(_arguments.DebugFilePath, "CustomUpdateEngine.log", Logger.Destination.File);
            Logger.Write("========================================================================================================================");
            Logger.Write("Starting CustomUpdateEngine v2 with ActionFile : " + _arguments.ActionFilePath);

            if (!File.Exists(_arguments.ActionFilePath))
            {
                Logger.Write("Can't find the ActionFile. " + _arguments.ActionFilePath);
                return (int)ErrorCodes.ActionFileNotFound;
            }

            try
            {
                Logger.Write("Starting parsing ActionFile.");
                _actions = ParseActionsFile(_arguments.ActionFilePath);
            }
            catch (Exception ex)
            {
                Logger.Write("Error when parsing ActionFile : " + ex.Message);
                return (int)ErrorCodes.ActionFileParsingError;
            }

            foreach (GenericAction action in _actions)
            {
                Logger.Write("ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
                Logger.Write("Running " + action.ToString());
                try
                {
                    action.Run(ref _returnCode);
                }
                catch (Exception ex)
                {
                    Logger.Write("Custom Action : " + action.ToString() + " has throw an execption.\r\n" + ex.Message);
                }
            }

            Logger.Write("End of CustomUpdateEngine. Exit code : " + _returnCode.ReturnValue);
            return _returnCode.ReturnValue;
        }

        #region {private Methods - Méthodes privées}

        public static Arguments GetArguments(string[] parameters)
        {
            Arguments arguments = new Arguments();
            arguments.ActionFilePath = String.Empty;
            arguments.DebugFilePath = Environment.ExpandEnvironmentVariables("%Temp%");

            foreach (string parameter in parameters)
            {
                if (parameter.ToLower().StartsWith(_actionFile))
                {
                    arguments.ActionFilePath = parameter.Substring(parameter.IndexOf(_actionFile) + _actionFile.Length);
                }
                if (parameter.ToLower().StartsWith(_debugFile))
                {
                    arguments.DebugFilePath = parameter.Substring(parameter.IndexOf(_debugFile) + _debugFile.Length);
                }
            }
            return arguments;
        }

        public static List<GenericAction> ParseActionsFile(string fullPath)
        {
            List<GenericAction> actions = new List<GenericAction>();

            XmlReader reader = XmlReader.Create(new StreamReader(fullPath, System.Text.Encoding.Unicode));

            if (!reader.ReadToFollowing("CustomUpdate"))
                throw new Exception("Unable to find the token : CustomUpdate");

            int version = int.Parse( reader.GetAttribute("Version"));
            if (version != 2)
                throw new Exception("Wrong version (expected version = 2, this version = " + version + ")");
            while (reader.ReadToFollowing("Action"))
            {
                GenericAction currentAction = GetActionFromXML(reader.ReadOuterXml());
                if (currentAction.GetType() == typeof(ReturnCodeAction))
                    _returnCode = (ReturnCodeAction)currentAction;
                else
                    actions.Add(currentAction);
            }
            return actions;
        }

        private static GenericAction GetActionFromXML(string xmlAction)
        {
            GenericAction action;

            Logger.Write("Get Element from : " + xmlAction);

            XmlReader reader = XmlReader.Create(new StringReader(xmlAction));
            if (!reader.ReadToFollowing("ElementType"))
                throw new Exception("Unable to find the token : ElementType");
            string elementType = reader.ReadString();

            switch (elementType)
            {
                case "CustomActions.ReturnCode":
                    action = new ReturnCodeAction(xmlAction);
                    break;
                case "CustomActions.AddRegKeyAction":
                    action = new AddRegKeyAction(xmlAction);
                    break;
                case "CustomActions.AddRegValueAction":
                    action = new AddRegValueAction(xmlAction);
                    break;
                case "CustomActions.ChangeRegDataAction":
                    action = new ChangeRegDataAction(xmlAction);
                    break;
                case "CustomActions.ChangeServiceAction":
                    action = new ChangeServiceAction(xmlAction);
                    break;
                case "CustomActions.CopyFileAction":
                    action = new CopyFileAction(xmlAction);
                    break;
                case "CustomActions.CreateFolderAction":
                    action = new CreateFolderAction(xmlAction);
                    break;
                case "CustomActions.CreateShortcutAction":
                    action = new CreateShortcutAction(xmlAction);
                    break;
                case "CustomActions.CreateTextFileAction":
                    action = new CreateTextFileAction(xmlAction);
                    break;
                case "CustomActions.DeleteFileAction":
                    action = new DeleteFileAction(xmlAction);
                    break;
                case "CustomActions.DeleteFolderAction":
                    action = new DeleteFolderAction(xmlAction);
                    break;
                case "CustomActions.DeleteRegKeyAction":
                    action = new DeleteRegKeyAction(xmlAction);
                    break;
                case "CustomActions.DeleteRegValueAction":
                    action = new DeleteRegValueAction(xmlAction);
                    break;
                case "CustomActions.DeleteTaskAction":
                    action = new DeleteTaskAction(xmlAction);
                    break;
                case "CustomActions.ExecutableAction":
                    action = new ExecutableAction(xmlAction);
                    break;
                case "CustomActions.ImportRegFileAction":
                    action = new ImportRegFileAction(xmlAction);
                    break;
                case "CustomActions.KillProcessAction":
                    action = new KillProcessAction(xmlAction);
                    break;
                case "CustomActions.RebootAction":
                    action = new RebootAction(xmlAction);
                    break;
                case "CustomActions.RegisterDLLAction":
                    action = new RegisterDLLAction(xmlAction);
                    break;
                case "CustomActions.RenameFileAction":
                    action = new RenameFileAction(xmlAction);
                    break;
                case "CustomActions.RenameFolderAction":
                    action = new RenameFolderAction(xmlAction);
                    break;
                case "CustomActions.RenameRegKeyAction":
                    action = new RenameRegKeyAction(xmlAction);
                    break;
                case "CustomActions.RenameRegValueAction":
                    action = new RenameRegValueAction(xmlAction);
                    break;
                case "CustomActions.RunPowershellScriptAction":
                    action = new RunPowershellScriptAction(xmlAction);
                    break;
                case "CustomActions.RunVbScriptAction":
                    action = new RunVbScriptAction(xmlAction);
                    break;
                case "CustomActions.ShutdownAction":
                    action = new ShutdownAction(xmlAction);
                    break;
                case "CustomActions.StartServiceAction":
                    action = new StartServiceAction(xmlAction);
                    break;
                case "CustomActions.StopServiceAction":
                    action = new StopServiceAction(xmlAction);
                    break;
                case "CustomActions.UnregisterDLLAcion":
                    action = new UnregisterDLLAction(xmlAction);
                    break;
                case "CustomActions.UnregisterServiceAction":
                    action = new UnregisterServiceAction(xmlAction);
                    break;
                case "CustomActions.UninstallMsiProductByGuidAction":
                    action = new UninstallMsiProductByGuidAction(xmlAction);
                    break;
                case "CustomActions.UninstallMsiProductByNameAction":
                    action = new UninstallMsiProductByNameAction(xmlAction);
                    break;
                case "CustomActions.InstallMsiAction":
                    action = new InstallMsiAction(xmlAction);
                    break;
                case "CustomActions.WaitAction":
                    action = new WaitAction(xmlAction);
                    break;
                default:
                    throw new Exception("Unknown ElementType : " + elementType);
            }

            if (action == null)
                throw new Exception("Unable to create an Action from the xmlFragment.");

            return action;
        }

        #endregion {private Methods - Méthodes privées}
    }
}
