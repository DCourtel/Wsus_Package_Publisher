using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.DeleteTaskAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class DeleteTaskAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteScheduledTask.CustAct"));

                // Act

                // Assert
                Assert.AreEqual("Adobe Acrobat Update Task", action.TaskName);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void DeleteTheTask_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteScheduledTask.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if(!Tools.IsScheduledTaskExist("Adobe Acrobat Update Task"))
                {
                    Tools.ImportScheduledTask(@"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\Adobe Acrobat Update Task.xml", "Adobe Acrobat Update Task");
                    Assert.IsTrue(Tools.IsScheduledTaskExist("Adobe Acrobat Update Task"));
                }
                action.Run(ref finalResult);

                // Assert
                Assert.IsFalse(Tools.IsScheduledTaskExist("Adobe Acrobat Update Task"));
            }
        }
    }
}
