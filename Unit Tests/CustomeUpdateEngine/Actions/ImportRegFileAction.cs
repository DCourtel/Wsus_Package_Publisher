using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.ImportRegFileAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class ImportRegFileAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ImportRegFile.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.FullPath, @"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomeUpdateEngine\Templates for Unit Tests\RegFileToImport.reg");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateRegValue_WhenDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ImportRegFile.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey keyToCreate = null;
                object valueToCreate = null;
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                hkcu.DeleteSubKeyTree(@"Software\EasyCompany", false);
                keyToCreate = hkcu.OpenSubKey(@"Software\EasyCompany\Wsus Package Publisher\Test", false);
                Assert.IsNull(keyToCreate);
                action.Run(ref finalResult);

                keyToCreate = hkcu.OpenSubKey(@"Software\EasyCompany\Wsus Package Publisher\Test", false);
                if(keyToCreate != null)
                {
                    valueToCreate = keyToCreate.GetValue("ImportRegFile", null);
                }

                // Assert
                Assert.IsNotNull(keyToCreate);
                Assert.AreEqual("Success", valueToCreate.ToString());
            }
        }
    }
}
