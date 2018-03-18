using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.AddRegKeyAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class AddRegKeyAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKLM.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.IsTrue(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.IsFalse(action.UseReg32);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateTheRegKey_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKCU.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if(targetKey != null)
                {
                    hkcu.DeleteSubKey(action.RegKey);
                    targetKey = null;
                }
                action.Run(ref finalResult);
                targetKey = hkcu.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNotNull(targetKey);
            }

            [TestMethod]
            public void CreateTheRegKey_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKLM.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey != null)
                {
                    hklm.DeleteSubKey(action.RegKey);
                    targetKey = null;
                }
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNotNull(targetKey);
            }

            [TestMethod]
            public void CreateTheRegKey_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegKeyToHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey != null)
                {
                    hklm.DeleteSubKey(action.RegKey);
                    targetKey = null;
                }
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNotNull(targetKey);
            }
        }
    }
}
