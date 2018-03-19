using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.UninstallMsiProductByNameAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class UninstallMsiProductByNameAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8Update1PercentExcept144.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ApplicationName, "Java 8 Update 1%");
                Assert.AreEqual(action.Exceptions, @"Java 8 Update 144");
                Assert.AreEqual(action.Parameters, "PARAMETERNAME=True");
                Assert.IsTrue(action.DontUninstallIfNoException);
                Assert.IsTrue(action.KillProcess);
                Assert.AreEqual(action.KillAfter, 7);
            }
        }

        [TestClass]
        public class Run_Should
        {
            /*  Java Name       and     GUID
                Java 8 Update 121	{26A24AE4-039D-4CA4-87B4-2F32180121F0}
                Java 8 Update 131	{26A24AE4-039D-4CA4-87B4-2F32180131F0}
                Java 8 Update 144	{26A24AE4-039D-4CA4-87B4-2F32180144F0}
                Java 8 Update 152	{26A24AE4-039D-4CA4-87B4-2F32180152F0}
                Java Auto Updater	{4A03706F-666A-4037-7777-5F2748764D10}
            */

            [TestInitialize]
            public void TestInitialize()
            {
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"))
                    Tools.InstallJava(@"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\jre-8u121-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"))
                    Tools.InstallJava(@"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\jre-8u131-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"))
                    Tools.InstallJava(@"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\jre-8u144-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"))
                    Tools.InstallJava(@"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\jre-8u152-windows-i586.exe");

                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
            }

            [TestMethod]
            public void UninstallOneProduct_WhenOnlyOneProductMatch()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8Update152.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 1);
            }

            [TestMethod]
            public void UninstallTwoProducts_WhenTwoProductsMatch()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8Update1_1.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 2);
            }

            [TestMethod]
            public void UninstallAllJavaExcept144_WhenPatternMatchAllReleaseAndExceptionMatch144()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8Update1PercentExcept144.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 3);
            }

            [TestMethod]
            public void UninstallJava8_WhenPatternContains_AndPercent()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8_pdatePercentExceptJava8Update__1.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 2);
            }

            [TestMethod]
            public void UninstallNothing_WhenPatternDoesNotMatchAnything()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-Java8Update2__.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter);
            }

            [TestMethod]
            public void UninstallNothing_WhenExceptionDoesNotMatchInstalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByName-ExceptionDoesNotMatchInstalled.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<global::CustomUpdateEngine.UninstallMsiProductByGuidAction.MsiProduct> installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = global::CustomUpdateEngine.UninstallMsiProductByGuidAction.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter);
            }

        }
    }
}
