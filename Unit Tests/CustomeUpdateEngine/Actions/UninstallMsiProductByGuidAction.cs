using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.UninstallMsiProductByGuidAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class UninstallMsiProductByGuidAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.MsiProductCodes, "AEBC-14__-DEFAEC%;81309-ADECB-_AED_%");
                Assert.AreEqual(action.Exceptions, @"ADE__-8942394-AEBEA__%");
                Assert.IsTrue(action.DontUninstallIfNoException);
                Assert.IsTrue(action.KillProcess);
                Assert.AreEqual(action.KillAfter, 1);
            }
        }

        [TestClass]
        public class Run_Should
        {
            /*  Java Name and GUID
                Java 8 Update 121	{26A24AE4-039D-4CA4-87B4-2F32180121F0}
                Java 8 Update 131	{26A24AE4-039D-4CA4-87B4-2F32180131F0}
                Java 8 Update 144	{26A24AE4-039D-4CA4-87B4-2F32180144F0}
                Java 8 Update 152	{26A24AE4-039D-4CA4-87B4-2F32180152F0}
                Java Auto Updater	{4A03706F-666A-4037-7777-5F2748764D10}
            */

            [TestInitialize]
            public void TestInitialize()
            {
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"))
                    Tools.InstallJava(@"C:\Users\Courtel\Downloads\Java\jre-8u121-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"))
                    Tools.InstallJava(@"C:\Users\Courtel\Downloads\Java\jre-8u131-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"))
                    Tools.InstallJava(@"C:\Users\Courtel\Downloads\Java\jre-8u144-windows-i586.exe");
                if (!Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"))
                    Tools.InstallJava(@"C:\Users\Courtel\Downloads\Java\jre-8u152-windows-i586.exe");

                installedProducts = SUT.GetMsiProducts();
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
            }

            [TestMethod]
            public void UninstallOneProduct_WhenOnlyOneProductMatch()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid-OneProduct.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = SUT.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 1);
            }

            [TestMethod]
            public void UninstallTwoProduct_WhenPatternMatchTwoProducts()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid-TwoProducts.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = SUT.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 2);
            }

            [TestMethod]
            public void UninstallAllJavaExceptU152_WhenPatternMatchAllAndExceptionListU152()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid-AllJavaExceptU152.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = SUT.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsFalse(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(productCountBefore == productCountAfter + 3);
            }

            [TestMethod]
            public void UninstallNothing_WhenNoExceptionIsInstalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid-DontUninstallWhenNoExceptionMatch.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = SUT.GetMsiProducts();
                productCountAfter = installedProducts.Count;

                // Asset

                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180121F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180131F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180144F0"));
                Assert.IsTrue(Tools.ProductsContains(installedProducts, "26A24AE4-039D-4CA4-87B4-2F32180152F0"));
                Assert.IsTrue(productCountBefore == productCountAfter);
            }

            [TestMethod]
            public void UninstallNothing_WhenPatternDoesNotMatchAnything()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("UninstallMsiByGuid-PatternDoesNotMatchanything.CustAct"));
                var finalResult = Tools.GetReturnCodeAction();
                List<SUT.MsiProduct> installedProducts = SUT.GetMsiProducts();
                int productCountBefore = installedProducts.Count;
                int productCountAfter;

                // Act
                action.Run(ref finalResult);
                installedProducts = SUT.GetMsiProducts();
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
