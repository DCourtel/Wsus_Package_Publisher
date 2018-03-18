using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.RegisterDLLAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RegisterDLLAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RegisterDLL.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FullPath, @"C:\Windows\System32\ThisDLL.dll");
            }
        }
    }
}
