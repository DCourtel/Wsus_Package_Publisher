using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine;

namespace Unit_Tests_CustomeUpdateEngine
{
    public class CustomUpdateEngine
    {
        [TestClass]
        public class GetArguments_Should
        {
            [TestMethod]
            public void BeInitializeByDefault_WhenCalledWithEmptyParameters()
            {
                // Arrange
                SUT.CustomUpdateEngine.Arguments args = new SUT.CustomUpdateEngine.Arguments();                

                // Act
                args = SUT.CustomUpdateEngine.GetArguments(new string[] { "", "" });

                // Assert
                Assert.AreEqual(Environment.ExpandEnvironmentVariables("%Temp%"), args.DebugFilePath);
                Assert.AreEqual(String.Empty, args.ActionFilePath);
            }

            [TestMethod]
            public void BeProperlyIniialized_WhenCalledWithValidParameters()
            {
                // Arrange
                SUT.CustomUpdateEngine.Arguments args = new SUT.CustomUpdateEngine.Arguments();
                string expectedActionFile = "Action1";
                string expectedDebugFilePath = @"C:\Users\Courtel\AppData\Local\Temp\WPP";

                // Act
                args = SUT.CustomUpdateEngine.GetArguments(new string[] { @"/actionfile=" + expectedActionFile, @"/debugfilefolder=" + expectedDebugFilePath });

                // Assert
                Assert.AreEqual(expectedDebugFilePath, args.DebugFilePath);
                Assert.AreEqual(expectedActionFile, args.ActionFilePath);
            }
        }

        [TestClass]
        public class ParseActionFile_Should
        {
            [TestMethod]
            public void ReturnAnEmptyList_WhenCalledWithReturnCodeAction()
            {
                // Arrange
                List<SUT.GenericAction> actions1;
                List<SUT.GenericAction> actions2;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions1 = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "Static Return Code.CustAct"));
                actions2 = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "Variable Return Code.CustAct"));
                
                // Assert
                Assert.IsTrue(actions1.Count == 0);
                Assert.IsTrue(actions2.Count == 0);
            }

            [TestMethod]
            public void ReturnAnAddRegKeyAction_WhenCalledWithAddRegKeyAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "AddRegKeyToHKCU.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.AddRegKeyAction));
            }

            [TestMethod]
            public void ReturnAnAddRegValueAction_WhenCalledWithAddRegValueAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "AddRegValueToHKCU.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.AddRegValueAction));
            }

            [TestMethod]
            public void ReturnAnChangeRegDataAction_WhenCalledWithChangeRegDataAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "ChangeRegDataActionHKCU.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.ChangeRegDataAction));
            }

            [TestMethod]
            public void ReturnAnCopyFileAction_WhenCalledWithCopyFileAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "CopyFileAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.CopyFileAction));
            }

            [TestMethod]
            public void ReturnAnCreateFolderAction_WhenCalledWithCreateFolderAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "CreateFolderAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.CreateFolderAction));
            }

            [TestMethod]
            public void ReturnAnCreateShortcutAction_WhenCalledWithCreateShortcutAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "CreateShortcutToAllUsers.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.CreateShortcutAction));
            }

            [TestMethod]
            public void ReturnAnCreateTextFileAction_WhenCalledWithCreateTextFileAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "CreateTextFileAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.CreateTextFileAction));

            }

            [TestMethod]
            public void ReturnAnDeleteFileAction_WhenCalledWithDeleteFileAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "DeleteFileAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.DeleteFileAction));
            }

            [TestMethod]
            public void ReturnAnDeleteFolderAction_WhenCalledWithDeleteFolderAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "DeleteFolderAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.DeleteFolderAction));
            }

            [TestMethod]
            public void ReturnAnDeleteRegKeyAction_WhenCalledWithDeleteRegKeyAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "DeleteRegKeyActionHKLM64.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.DeleteRegKeyAction));
            }

            [TestMethod]
            public void ReturnAnDeleteRegValueAction_WhenCalledWithDeleteRegValueAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "DeleteRegValueHKCU.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.DeleteRegValueAction));
            }

            [TestMethod]
            public void ReturnAnExecutableAction_WhenCalledWithExecutableAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "ExecutableAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.ExecutableAction));
            }

            [TestMethod]
            public void ReturnAnImportRegFileAction_WhenCalledWithImportRegFileAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "ImportRegFile.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.ImportRegFileAction));
            }

            [TestMethod]
            public void ReturnAnKillProcessAction_WhenCalledWithKillProcessAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "KillProcessNotepad.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.KillProcessAction));
            }

            [TestMethod]
            public void ReturnAnRebootAction_WhenCalledWithRebootAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "Reboot.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RebootAction));
            }

            [TestMethod]
            public void ReturnAnRegisterDLLAction_WhenCalledWithRegisterDLLAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "RegisterDLL.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RegisterDLLAction));
            }

            [TestMethod]
            public void ReturnAnRenameFileAction_WhenCalledWithRenameFileAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "RenameFile.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RenameFileAction));
            }

            [TestMethod]
            public void ReturnAnRenameFolderAction_WhenCalledWithRenameFolderAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "RenameFolder.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RenameFolderAction));
            }

            [TestMethod]
            public void ReturnAnRenameRegKeyAction_WhenCalledWithRenameRegKeyAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "RenameRegKey.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RenameRegKeyAction));
            }

            [TestMethod]
            public void ReturnAnRenameRegValueAction_WhenCalledWithRenameRegValueAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "RenameRegValueHKCU.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.RenameRegValueAction));
            }

            [TestMethod]
            public void ShutdownAction_WhenCalledWithShutdownAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "ShutdownAction.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.ShutdownAction));
            }

            [TestMethod]
            public void StartServiceAction_WhenCalledWithStartServiceAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "StartService.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.StartServiceAction));
            }

            [TestMethod]
            public void StopServiceAction_WhenCalledWithStopServiceAction()
            {
                // Arrange
                List<SUT.GenericAction> actions;
                string baseFolder = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests";

                // Act
                actions = SUT.CustomUpdateEngine.ParseActionsFile(System.IO.Path.Combine(baseFolder, "StopService.CustAct"));

                // Assert
                Assert.IsTrue(actions.Count == 1);
                Assert.IsTrue(actions[0].GetType() == typeof(SUT.StopServiceAction));
            }
        }
    }
}
