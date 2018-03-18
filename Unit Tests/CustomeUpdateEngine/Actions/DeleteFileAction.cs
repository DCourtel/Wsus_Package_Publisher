using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.DeleteFileAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
   public class DeleteFileAction
    {
       [TestClass]
       public class Constuctor_Should
       {
           [TestMethod]
           public void ProperlyInitializeProperties_WhenCalled()
           {
               // Arrange
               SUT action = new SUT(Tools.GetXmlFragment("DeleteFileAction.CustAct"));

               // Act

               // Assert
               Assert.AreEqual(action.FullPath, @"C:\TempAction\DeleteMe.txt");
           }
       }

       [TestClass]
       public class Run_Should
       {
           [TestMethod]
           public void DeleteTheFile_WhenItExists()
           {
               // Arrange
               SUT action = new SUT(Tools.GetXmlFragment("DeleteFileAction.CustAct"));
               FileInfo fileToDelete = new FileInfo(action.FullPath);
               var finalResult = Tools.GetReturnCodeAction();

               // Act
               if(!fileToDelete.Exists)
               {
                   StreamWriter writer = new StreamWriter(action.FullPath);
                   writer.Write("test");
                   writer.Close();
                   fileToDelete.Refresh();
                   Assert.IsTrue(fileToDelete.Exists);
               }
               action.Run(ref finalResult);
               fileToDelete.Refresh();

               // Assert
               Assert.IsFalse(fileToDelete.Exists);
           }

           [TestMethod]
           public void DoNothing_WhenTheFileDoesNotExists()
           {
               // Arrange
               SUT action = new SUT(Tools.GetXmlFragment("DeleteFileAction.CustAct"));
               FileInfo fileToDelete = new FileInfo(action.FullPath);
               var finalResult = Tools.GetReturnCodeAction();

               // Act
               if (fileToDelete.Exists)
               {
                   fileToDelete.Delete();
                   fileToDelete.Refresh();
                   Assert.IsFalse(fileToDelete.Exists);
               }
               action.Run(ref finalResult);
               fileToDelete.Refresh();

               // Assert
               Assert.IsFalse(fileToDelete.Exists);
           }
       }
    }
}
