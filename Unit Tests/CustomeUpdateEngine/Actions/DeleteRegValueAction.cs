using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.DeleteRegValueAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class DeleteRegValueAction
    {
        [TestClass]
        public class Constructor_Should
        {

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DeleteMe");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM32()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKLM32.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DeleteMe");
                Assert.IsTrue(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DeleteMe");
                Assert.IsFalse(action.UseReg32);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void DeleteRegValue_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKCU.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if(targetKey == null)
                {
                    targetKey = hkcu.CreateSubKey(action.RegKey);
                }
                object valueToDelete = targetKey.GetValue(action.ValueName, null);
                if(valueToDelete == null)
                {
                    targetKey.SetValue(action.ValueName, 12, RegistryValueKind.DWord);
                }
                valueToDelete = targetKey.GetValue(action.ValueName, null);
                Assert.IsNotNull(valueToDelete);
                action.Run(ref finalResult);
                valueToDelete = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNull(valueToDelete);
            }

            [TestMethod]
            public void DeleteTheRegValue_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKLM32.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    targetKey = hklm.CreateSubKey(action.RegKey);
                }
                object valueToDelete = targetKey.GetValue(action.ValueName, null);
                if (valueToDelete == null)
                {
                    targetKey.SetValue(action.ValueName, 12, RegistryValueKind.DWord);
                }
                valueToDelete = targetKey.GetValue(action.ValueName, null);
                Assert.IsNotNull(valueToDelete);
                action.Run(ref finalResult);
                valueToDelete = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNull(valueToDelete);
            }

            [TestMethod]
            public void DeleteTheRegValue_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    targetKey = hklm.CreateSubKey(action.RegKey);
                }
                object valueToDelete = targetKey.GetValue(action.ValueName, null);
                if (valueToDelete == null)
                {
                    targetKey.SetValue(action.ValueName, 12, RegistryValueKind.DWord);
                } 
                valueToDelete = targetKey.GetValue(action.ValueName, null);
                Assert.IsNotNull(valueToDelete);
                action.Run(ref finalResult);
                valueToDelete = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNull(valueToDelete);
            }

            [TestMethod]
            public void DoNothing_WhenRegValueDoesNotExists()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("DeleteRegValueHKCUNotExists.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (targetKey == null)
                {
                    targetKey = hkcu.CreateSubKey(action.RegKey);
                }
                object valueToDelete = targetKey.GetValue(action.ValueName, null);
                Assert.IsNull(valueToDelete);
                action.Run(ref finalResult);
                valueToDelete = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNull(valueToDelete);
            }
        }
    }
}
