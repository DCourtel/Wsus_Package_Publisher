using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.CopyFileAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class CopyFileAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperies_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CopyFileAction.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.SourceFile, @"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\FileToBeCopied.txt");
                Assert.AreEqual(action.DestinationFolder, @"C:\Windows\temp\Wpp");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CopyTheFile_WhenTheSourceFileExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CopyFileAction.CustAct"));
                FileInfo sourceFile = new FileInfo(action.SourceFile);
                FileInfo destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (destinationFile.Exists)
                { destinationFile.Delete(); }
                action.Run(ref finalResult);
                destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));

                // Assert
                Assert.IsTrue(destinationFile.Exists);
            }

            [TestMethod]
            public void DoNothing_WhenTheSourceFileDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CopyFileActionNoFile.CustAct"));
                FileInfo sourceFile = new FileInfo(action.SourceFile);
                FileInfo destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (sourceFile.Exists)
                { sourceFile.Delete(); }
                if (destinationFile.Exists)
                { destinationFile.Delete(); }
                action.Run(ref finalResult);
                destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));

                // Assert
                Assert.IsFalse(destinationFile.Exists);
            }

            [TestMethod]
            public void CreateTheDestinationFolder_WhenDestinationFolderDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CopyFileActionRandomFolder.CustAct"));
                FileInfo sourceFile = new FileInfo(action.SourceFile);
                FileInfo destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (destinationFile.Exists)
                { destinationFile.Delete(); }
                if (destinationFile.Directory.Exists)
                { destinationFile.Directory.Delete(true); }
                Assert.IsFalse(destinationFile.Directory.Exists);
                action.Run(ref finalResult);
                destinationFile = new FileInfo(Path.Combine(action.DestinationFolder, sourceFile.Name));

                // Assert
                Assert.IsTrue(destinationFile.Exists);
            }
        }

    }
}
