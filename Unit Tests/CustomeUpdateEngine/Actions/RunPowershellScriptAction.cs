using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.RunPowershellScriptAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RunPowershellScriptAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RunPowershellScript.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FullPath, @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests\PowershellReturnCode.ps1");
                Assert.IsTrue(action.KillProcess);
                Assert.AreEqual(action.Parameters, "4 arg1 arg2 \"arg3 With space\"");
                Assert.IsTrue(action.StoreToVariable);
                Assert.AreEqual(action.DelayBeforeKilling, 10);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void ReturnFour_WhenCallingAScriptThatReturnFour()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RunPowershellScript.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;

                // Act
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(4, finalResult.ReturnValue);
            }

            [TestMethod]
            public void KillProcess_WhenTakingTooMuchTime()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RunPowershellAndWaitAndKill.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;
                System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();

                // Act
                chrono.Start();
                action.Run(ref finalResult);
                chrono.Stop();

                // Assert
                Assert.AreEqual(60.0, chrono.Elapsed.TotalSeconds, 2);
                Assert.AreEqual(-1, finalResult.ReturnValue);
            }

            [TestMethod]
            public void DoNotKillProcess_WhenTakingLessTimeThatAllowed()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RunPowershellAndWaitAndDoNotKill.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;
                System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();

                // Act
                chrono.Start();
                action.Run(ref finalResult);
                chrono.Stop();

                // Assert
                Assert.AreEqual(10.0, chrono.Elapsed.TotalSeconds, 2);
                Assert.AreEqual(finalResult.ReturnMethod, global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable);
                Assert.AreEqual(255, finalResult.ReturnValue);
            }
        }
    }
}
