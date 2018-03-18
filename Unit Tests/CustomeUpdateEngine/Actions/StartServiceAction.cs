using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.StartServiceAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class StartServiceAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StartService.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void StartTheService_WhenCalledTheServiceIsStop()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StartService.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Tools.StopService(action.ServiceName);
                Assert.IsFalse(Tools.IsServiceRunning(action.ServiceName));
                action.Run(ref finalResult);
                
                // Assert
                Assert.IsTrue(Tools.IsServiceRunning(action.ServiceName));
            }

            [TestMethod]
            public void DoesNotChangeServiceState_WhenTheServiceIsAlreadyStarted()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StartService.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Tools.StartService(action.ServiceName);
                Assert.IsTrue(Tools.IsServiceRunning(action.ServiceName));
                action.Run(ref finalResult);

                // Assert
                Assert.IsTrue(Tools.IsServiceRunning(action.ServiceName));
            }

            [TestMethod]
            public void DoesNotThrowException_WhenTheServiceDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("StartServiceThatDoesNotExists.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);

                // Assert                
            }
        }
    }
}
