using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.DeleteRegKeyAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class DeleteRegKeyAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKLM32.CustAct"));

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
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKLM64.CustAct"));

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
            public void DeleteTheRegKey_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKCU.CustAct"));
                RegistryKey hkcu = Registry.CurrentUser;
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    hkcu.CreateSubKey(action.RegKey);
                }
                action.Run(ref finalResult);
                targetKey = hkcu.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNull(targetKey);
            }

            [TestMethod]
            public void DeleteTheRegKey_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKLM32.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    hklm.CreateSubKey(action.RegKey);
                }
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNull(targetKey);
            }

            [TestMethod]
            public void DeleteTheRegKey_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, false);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    hklm.CreateSubKey(action.RegKey);
                }
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNull(targetKey);
            }
            
            [TestMethod]
            public void DeleteAllSubKey_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                RegistryKey targetKey2 = null;
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    targetKey2 = hklm.CreateSubKey(action.RegKey);
                    targetKey2.CreateSubKey(@"SubKey\SubKey\SubKey");
                }
                if(targetKey2 == null)
                {
                    targetKey.CreateSubKey(@"SubKey\SubKey\SubKey");
                }
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNull(targetKey);
            }

            [TestMethod]
            public void DoNothing_WhenTryingToDeleteRegKeyThatDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegKeyActionNotExists.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                var finalResult = Tools.GetReturnCodeAction();
                RegistryKey targetKey = null;

                // Act
               
                action.Run(ref finalResult);
                targetKey = hklm.OpenSubKey(action.RegKey, false);

                // Assert
                Assert.IsNull(targetKey);
            }
        }
    }
}
