using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.RenameFolderAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RenameFolderAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithRenameFolder()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameFolder.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FolderPath, @"C:\TempAction\RenameMe");
                Assert.AreEqual(action.NewName, "Renamed");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void RenameTheFile_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameFolder.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                DirectoryInfo folderToRename = new DirectoryInfo(action.FolderPath);
                DirectoryInfo renamedFolder = new DirectoryInfo(Path.Combine(folderToRename.Parent.FullName, action.NewName));

                if (!folderToRename.Parent.Exists)
                {
                    folderToRename.Parent.Create();
                }
                if (!folderToRename.Exists)
                {
                    folderToRename.Create();
                }
                if(renamedFolder.Exists)
                {
                    renamedFolder.Delete();
                    renamedFolder.Refresh();
                }

                folderToRename.Refresh();
                Assert.IsTrue(folderToRename.Exists);
                Assert.IsFalse(renamedFolder.Exists);

                // Act
                action.Run(ref finalResult);
                folderToRename.Refresh();
                renamedFolder.Refresh();

                // Assert
                Assert.IsFalse(folderToRename.Exists);
                Assert.IsTrue(renamedFolder.Exists);
            }
        }
    }
}
