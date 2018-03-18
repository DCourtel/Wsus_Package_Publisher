using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.InstallMsiAction;
using CU = CustomUpdateEngine.UninstallMsiProductByGuidAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class InstallMsiAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithNormalParameters()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("InstallMsi.CustAct"));

                // Act

                // Assert
                Assert.AreEqual("MyMsiName.msi", action.MsiName, true);
                Assert.AreEqual("PROPERTY=Value", action.Parameters, true);
                Assert.IsTrue(action.IsLogRequested);
                Assert.AreEqual(@"C:\Windows\Temp\MyMsiLog.log", action.LogPath, true);
                Assert.AreEqual(1, action.UiLevel);
                Assert.AreEqual(2, action.RestartBehavior);
                Assert.IsTrue(action.KillProcess);
                Assert.AreEqual(4, action.KillAfter);
                Assert.IsTrue(action.StoreToVariable);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestInitialize]
            public void TestInitialize()
            {
                List<CU.MsiProduct> installedProducts = CU.GetMsiProducts();
                if (Tools.ProductsContains(installedProducts, "D0A41530-E762-4C7D-8A72-E2D0E07C8A2E"))
                {
                    CU action = new CU(Tools.GetXmlFragment("UnInstallRemoteMsiManager.custAct"));
                    var finalResult = Tools.GetReturnCodeAction();
                    action.Run(ref finalResult);

                    installedProducts = CU.GetMsiProducts();
                    Assert.IsFalse(Tools.ProductsContains(installedProducts, "D0A41530-E762-4C7D-8A72-E2D0E07C8A2E"));
                }
            }

            [TestMethod]
            public void InstallRemoteMsiManager_WhenAskedToDoSo()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("Install Remote Msi Manager.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                List<CU.MsiProduct> installedProducts = CU.GetMsiProducts();
                action.Run(ref finalResult);
                installedProducts = CU.GetMsiProducts();

                // Assert
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "D0A41530-E762-4C7D-8A72-E2D0E07C8A2E"));
                Assert.AreEqual(0, finalResult.ReturnValue);
            }
        }
    }
}
