using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.KillProcessAction;
using System.Diagnostics;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class KillProcessAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("KillProcessNotepad.CustAct"));

                // Act

                // Assert
                Assert.AreEqual("Notepad", action.ProcessName, true);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void KillTheProcess_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("KillProcessNotepad.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act

                Assert.AreEqual(0, Process.GetProcessesByName(action.ProcessName).Length);
                Process procToKill = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(action.ProcessName);
                procToKill.StartInfo = startInfo;
                procToKill.Start();
                Assert.AreEqual(1, Process.GetProcessesByName(action.ProcessName).Length);
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(0, Process.GetProcessesByName(action.ProcessName).Length);
            }

            [TestMethod]
            public void KillAllProcessesByName_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("KillProcessNotepad.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act

                Assert.AreEqual(0, Process.GetProcessesByName(action.ProcessName).Length);
                Process proc1 = new Process();
                Process proc2 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(action.ProcessName);
                proc1.StartInfo = startInfo;
                proc1.Start();
                proc2.StartInfo = startInfo;
                proc2.Start();
                Assert.AreEqual(2, Process.GetProcessesByName(action.ProcessName).Length);
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(0, Process.GetProcessesByName(action.ProcessName).Length);
            }
        }
    }
}
