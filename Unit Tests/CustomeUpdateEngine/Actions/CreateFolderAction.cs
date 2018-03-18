using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.CreateFolderAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class CreateFolderAction
    {
        [TestClass]
        public class Constuctor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperty_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateFolderAction.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FullPath, @"C:\Windows\Temp\NewlyCreatedFolder");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateTheFolder_WhenFolderDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateFolderAction.CustAct"));
                DirectoryInfo newDirectory = new DirectoryInfo(action.FullPath);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (newDirectory.Exists)
                {
                    newDirectory.Delete();
                    newDirectory = new DirectoryInfo(action.FullPath);
                    Assert.IsFalse(newDirectory.Exists);
                }

                action.Run(ref finalResult);
                newDirectory = new DirectoryInfo(action.FullPath);

                // Assert
                Assert.IsTrue(newDirectory.Exists);
            }

            [TestMethod]
            public void DoNothing_WhenFolderAlreadyExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateFolderActionFolderAlreadyExists.CustAct"));
                DirectoryInfo newDirectory = new DirectoryInfo(action.FullPath);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (!newDirectory.Exists)
                {
                    Assert.Fail();
                }
                action.Run(ref finalResult);

                // Assert
                Assert.IsTrue(newDirectory.Exists);
            }
        }
    }
}
