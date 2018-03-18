using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.RenameFileAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RenameFileAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameFile.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FullPath, @"C:\TempAction\RenameMe.txt");
                Assert.AreEqual(action.NewName, "NewName.txt");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void RenameTheFile_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameFile.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                FileInfo fileToRename = new FileInfo(action.FullPath);
                FileInfo fileRenamed = new FileInfo(Path.Combine(fileToRename.DirectoryName, action.NewName));
                if(!Directory.Exists(fileToRename.DirectoryName))
                {
                    fileToRename.Directory.Create();
                }
                if(!fileToRename.Exists)
                {
                    StreamWriter writer = fileToRename.CreateText();
                    writer.Write("This file must be rename.");
                    writer.Close();
                    fileToRename.Refresh();
                }
                if(fileRenamed.Exists)
                {
                    fileRenamed.Delete();
                }
                Assert.IsTrue(fileToRename.Exists);
                Assert.IsFalse(fileRenamed.Exists);

                // Act
                action.Run(ref finalResult);
                fileToRename.Refresh();
                fileRenamed.Refresh();

                // Assert
                Assert.IsFalse(fileToRename.Exists);
                Assert.IsTrue(fileRenamed.Exists);
            }
        }
    }
}
