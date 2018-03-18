using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.DeleteFolderAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    public class DeleteFolderAction
    {
        [TestClass]
        public class Constuctor_Should
        {
            [TestMethod]
            public void ProperlyInitiatizeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteFolderAction.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FolderPath, @"C:\TempAction\DeleteMe");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void DeleteTheFolder_WhenItExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteFolderAction.CustAct"));
                DirectoryInfo folderToDelete = new DirectoryInfo(action.FolderPath);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (!folderToDelete.Exists)
                {
                    folderToDelete.Create();
                    folderToDelete.Refresh();
                    Assert.IsTrue(folderToDelete.Exists);
                }
                action.Run(ref finalResult);
                folderToDelete.Refresh();

                // Assert
                Assert.IsFalse(folderToDelete.Exists);
            }

            [TestMethod]
            public void DoNothing_WhenTheFolderDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteFolderAction.CustAct"));
                DirectoryInfo folderToDelete = new DirectoryInfo(action.FolderPath);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (folderToDelete.Exists)
                {
                    folderToDelete.Delete(true);
                    folderToDelete.Refresh();
                    Assert.IsFalse(folderToDelete.Exists);
                }
                action.Run(ref finalResult);
                folderToDelete.Refresh();

                // Assert
                Assert.IsFalse(folderToDelete.Exists);
            }

            [TestMethod]
            public void DeleteTheFolder_WhenItContainsFilesAndSubFolders()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteFolderAction.CustAct"));
                DirectoryInfo folderToDelete = new DirectoryInfo(action.FolderPath);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (folderToDelete.Exists)
                {
                    folderToDelete.Delete(true);
                    folderToDelete.Refresh();
                    Assert.IsFalse(folderToDelete.Exists);
                }
                folderToDelete.Create();
                folderToDelete.Refresh();
                Assert.IsTrue(folderToDelete.Exists);
                folderToDelete.CreateSubdirectory(@"SecondLevel\ThirdLevel");
                folderToDelete.CreateSubdirectory("Another Folder");
                StreamWriter writer = new StreamWriter(Path.Combine(folderToDelete.FullName, "test1.txt"));
                writer.Write("test");
                writer.Close();

                writer = new StreamWriter(Path.Combine(folderToDelete.FullName, "test2.txt"));
                writer.Write("test");
                writer.Close();

                action.Run(ref finalResult);
                folderToDelete.Refresh();

                // Assert
                Assert.IsFalse(folderToDelete.Exists);
            }
        }
    }
}
