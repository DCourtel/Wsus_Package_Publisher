using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.ReturnCodeAction;

namespace Unit_Tests_CustomeUpdateEngine
{
    class ReturnCodeAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithStaticXmlFragment()
            {
                // Arrange 
                SUT retCodeAction = new SUT(Tools.GetXmlFragment("Static Return Code.CustAct"));

                // Act
                
                // Assert
                Assert.AreEqual(retCodeAction.ReturnMethod, SUT.ReturnCodeMethod.Static);
                Assert.AreEqual(retCodeAction.ReturnValue, 0);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithVariableXmlFragment()
            {
                // Arrange 
                SUT retCodeAction = new SUT(Tools.GetXmlFragment("Variable Return Code.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(retCodeAction.ReturnMethod, SUT.ReturnCodeMethod.Variable);                
            }
        }
    }
}
