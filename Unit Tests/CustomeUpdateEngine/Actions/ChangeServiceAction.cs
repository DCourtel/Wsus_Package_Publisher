using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.ChangeServiceAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class ChangeServiceAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithAuto()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeServiceActionAuto.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
                Assert.AreEqual(action.Mode, "Automatic");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithManual()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeServiceActionManual.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
                Assert.AreEqual(action.Mode, "Manual");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithDisable()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeServiceActionDisable.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
                Assert.AreEqual(action.Mode, "Disabled");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void ChangeTheStatTypeToAutomatic_WhenCalledWithAutomatic()
            {
                // Arrange
                SUT action;
                string startType = Tools.GetServiceStartType("AdobeARMservice");
                var finalResult = Tools.GetReturnCodeAction();
                if (startType == "Auto")
                {
                    action = new SUT(Tools.GetXmlFragment("ChangeServiceActionDisable.CustAct"));
                    action.Run(ref finalResult);
                    startType = Tools.GetServiceStartType("AdobeARMservice");
                    Assert.AreEqual("Disabled", startType);
                }
                action = new SUT(Tools.GetXmlFragment("ChangeServiceActionAuto.CustAct"));

                // Act
                action.Run(ref finalResult);
                startType = Tools.GetServiceStartType(action.ServiceName);

                // Assert
                Assert.AreEqual("Auto", startType);
            }

            [TestMethod]
            public void ChangeTheStatTypeToDisabled_WhenCalledWithDisabled()
            {
                // Arrange
                SUT action;
                string startType = Tools.GetServiceStartType("AdobeARMservice");
                var finalResult = Tools.GetReturnCodeAction();
                if (startType == "Disabled")
                {
                    action = new SUT(Tools.GetXmlFragment("ChangeServiceActionManual.CustAct"));
                    action.Run(ref finalResult);
                    startType = Tools.GetServiceStartType("AdobeARMservice");
                    Assert.AreEqual("Manual", startType);
                }
                action = new SUT(Tools.GetXmlFragment("ChangeServiceActionDisable.CustAct"));

                // Act
                action.Run(ref finalResult);
                startType = Tools.GetServiceStartType(action.ServiceName);

                // Assert
                Assert.AreEqual("Disabled", startType);
            }

            [TestMethod]
            public void ChangeTheStatTypeToManual_WhenCalledWithManual()
            {
                // Arrange
                SUT action;
                string startType = Tools.GetServiceStartType("AdobeARMservice");
                var finalResult = Tools.GetReturnCodeAction();
                if (startType == "Manual")
                {
                    action = new SUT(Tools.GetXmlFragment("ChangeServiceActionAutomatic.CustAct"));
                    action.Run(ref finalResult);
                    startType = Tools.GetServiceStartType("AdobeARMservice");
                    Assert.AreEqual("Auto", startType);
                }
                action = new SUT(Tools.GetXmlFragment("ChangeServiceActionManual.CustAct"));

                // Act
                action.Run(ref finalResult);
                startType = Tools.GetServiceStartType(action.ServiceName);

                // Assert
                Assert.AreEqual("Manual", startType);
            }
        }

    }
}
