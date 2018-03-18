using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.UnregisterServiceAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class UnregisterServiceAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UnregisterService.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ServiceName, "AdobeARMservice");
            }
        }

        [TestClass]
        public class Run_Should
        {
        }
    }
}
