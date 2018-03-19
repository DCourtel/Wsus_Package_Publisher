using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.ExecutableAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class ExecutableAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableAction.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(@"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Templates for Unit Tests\CustomeUpdateEngine.exe", action.PathToTheFile);
                Assert.AreEqual(@"/rien", action.Parameters);
                Assert.IsTrue(action.KillProcess);
                Assert.AreEqual(10, action.DelayBeforeKilling);
                Assert.IsTrue(action.StoreToVariable);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void ReturnTwelve_WhenCalledWithStartWaitAndReturn12()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableActionReturn12.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;

                // Act  
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(12, finalResult.ReturnValue);
            }

            [TestMethod]
            public void ReturnMinusOne_WhenCalledWithStartWaitAndReturnMinusOne()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableActionReturnMinusOne.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;

                // Act
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(-1, finalResult.ReturnValue);
            }

            [TestMethod]
            public void ReturnZero_WhenCalledWithStartWaitAndReturnZero()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableActionReturnZero.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;

                // Act    
                action.Run(ref finalResult);

                // Assert
                Assert.AreEqual(0, finalResult.ReturnValue);
            }

            [TestMethod]
            public void KillProcess_WhenProcessRunTooLong()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableActionWait2MinutesAndKillAt1Minute.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();

                // Act  
                chrono.Start();
                action.Run(ref finalResult);
                chrono.Stop();

                // Assert
                Assert.AreEqual(60 * 1000, chrono.ElapsedMilliseconds, 150);
            }

            [TestMethod]
            public void DoNotKillProcess_WhenProcessRun1MinutesWithKillAt2()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ExecutableActionWait1MinuteKillAt2AndReturn14.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                finalResult.ReturnValue = 0;
                finalResult.ReturnMethod = global::CustomUpdateEngine.ReturnCodeAction.ReturnCodeMethod.Variable;
                System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();

                // Act  
                chrono.Start();
                action.Run(ref finalResult);
                chrono.Stop();

                // Assert
                Assert.AreEqual(60 * 1000, chrono.ElapsedMilliseconds, 200);
                Assert.AreEqual(14, finalResult.ReturnValue);
            }
        }
    }
}
