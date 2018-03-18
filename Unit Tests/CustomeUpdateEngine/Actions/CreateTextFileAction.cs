using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.CreateTextFileAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    public class CreateTextFileAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateTextFileAction.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FilePath, @"C:\TempAction");
                Assert.AreEqual(action.Filename, @"CreateTextFileAction.txt");
                Assert.AreEqual(action.Content, "Voici un test de création de fichier texte.\r\nIl se compose de plusieurs lignes.\r\néèêîôâ ï ë ÿ ẍ © ® æ œ\r\nDont voici la dernière.");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateTheTextFile_WhenItDoesNotExist()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateTextFileAction.CustAct"));
                string actualContent = String.Empty;
                FileInfo newFile = new FileInfo(Path.Combine(action.FilePath, action.Filename));
                var finalResult = Tools.GetReturnCodeAction();
                StreamReader reader;

                // Act
                if(newFile.Exists)
                {
                    newFile.Delete();
                    newFile.Refresh();
                    Assert.IsFalse(newFile.Exists);
                }
                action.Run(ref finalResult);
                newFile.Refresh();
                reader = new StreamReader(newFile.OpenRead());

                // Assert
                Assert.IsTrue(newFile.Exists);
                Assert.AreEqual(action.Content, reader.ReadToEnd());
                reader.Close();
            }

            [TestMethod]
            public void ReplaceTheFile_WhenItDoesExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateTextFileAction.CustAct"));
                string actualContent = String.Empty;
                FileInfo newFile = new FileInfo(Path.Combine(action.FilePath, action.Filename));
                var finalResult = Tools.GetReturnCodeAction();
                StreamReader reader;

                // Act
                if (newFile.Exists)
                {
                    newFile.Delete();
                    newFile.Refresh();
                    Assert.IsFalse(newFile.Exists);
                } 
                StreamWriter writer = new StreamWriter(newFile.Create());
                writer.Write("Test");
                writer.Close();
                newFile.Refresh();
                Assert.IsTrue(newFile.Exists);

                action.Run(ref finalResult);
                newFile.Refresh();
                reader = new StreamReader(newFile.OpenRead());

                // Assert
                Assert.IsTrue(newFile.Exists);
                Assert.AreEqual(action.Content, reader.ReadToEnd());
                reader.Close();
            }
        }
    }
}
