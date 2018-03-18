using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.WaitAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class WaitAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithValidData()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("Wait3Seconds.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.SecondToWait, 3);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void WaitThreeSeconds_WhenAskedToDoSo()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("Wait3Seconds.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                System.Diagnostics.Stopwatch chrono = new System.Diagnostics.Stopwatch();
                
                // Act
                chrono.Start();
                action.Run(ref finalResult);
                chrono.Stop();

                // Assert
                Assert.AreEqual(3000.0, chrono.Elapsed.TotalMilliseconds, 10.0);
            }
        }
    }
}
