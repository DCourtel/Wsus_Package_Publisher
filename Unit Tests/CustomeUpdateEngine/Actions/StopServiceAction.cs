using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.StopServiceAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class StopServiceAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StopService.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void StopTheService_WhenCalledTheServiceIsStarted()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StopService.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Tools.StartService(action.ServiceName);
                Assert.IsTrue(Tools.IsServiceRunning(action.ServiceName));
                action.Run(ref finalResult);

                // Assert
                Assert.IsFalse(Tools.IsServiceRunning(action.ServiceName));
            }

            [TestMethod]
            public void DoesNotChangeServiceState_WhenTheServiceIsAlreadyStopped()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StopService.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Tools.StopService(action.ServiceName);
                Assert.IsFalse(Tools.IsServiceRunning(action.ServiceName));
                action.Run(ref finalResult);

                // Assert
                Assert.IsFalse(Tools.IsServiceRunning(action.ServiceName));
            }

            [TestMethod]
            public void DoesNotThrowException_WhenTheServiceDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StopServiceThatDoesNotExists.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);

                // Assert                
            }
        }
    }
}
