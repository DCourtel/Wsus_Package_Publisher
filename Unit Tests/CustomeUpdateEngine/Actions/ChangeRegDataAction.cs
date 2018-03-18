using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.ChangeRegDataAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class ChangeRegDataAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.RegValue, "ChangeMe");
                Assert.AreEqual(action.NewData, "NewValue");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM32()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKLM32.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.RegValue, "ChangeMe");
                Assert.AreEqual(action.NewData, "NewValue");
                Assert.IsTrue(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.RegValue, "ChangeMe");
                Assert.AreEqual(action.NewData, "NewValue");
                Assert.IsFalse(action.UseReg32);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void ModifiedTheValueInHKCU_WhenTheValueAlreadyExist()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKCU.CustAct"));

                RegistryKey hkcu = Registry.CurrentUser;
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                targetKey.SetValue(action.RegValue, "OldData");
                Assert.AreEqual("OldData", targetKey.GetValue(action.RegValue, null));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);
                string targetValueContent = (string)targetKey.GetValue(action.RegValue, null);

                // Assert
                Assert.IsNotNull(targetValueContent);
                Assert.AreEqual(action.NewData, targetValueContent);
            }

            [TestMethod]
            public void ModifiedTheValueInHKLM32_WhenTheValueAlreadyExist()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKLM32.CustAct"));

                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                targetKey.SetValue(action.RegValue, "OldData");
                Assert.AreEqual("OldData", targetKey.GetValue(action.RegValue, null));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);
                string targetValueContent = (string)targetKey.GetValue(action.RegValue, null);

                // Assert
                Assert.IsNotNull(targetValueContent);
                Assert.AreEqual(action.NewData, targetValueContent);
            }

            [TestMethod]
            public void ModifiedTheValueInHKLM64_WhenTheValueAlreadyExist()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKLM64.CustAct"));

                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                targetKey.SetValue(action.RegValue, "OldData");
                Assert.AreEqual("OldData", targetKey.GetValue(action.RegValue, null));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);
                string targetValueContent = (string)targetKey.GetValue(action.RegValue, null);

                // Assert
                Assert.IsNotNull(targetValueContent);
                Assert.AreEqual(action.NewData, targetValueContent);
            }

            [TestMethod]
            public void ModifiedTheDefaultValue_WhenCalledWithDefaultValue()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("ChangeRegDataActionHKLM64ToDefaultValue.CustAct"));

                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                targetKey.SetValue("", "OldData");
                Assert.AreEqual("OldData", targetKey.GetValue("", null));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                action.Run(ref finalResult);
                string targetValueContent = (string)targetKey.GetValue("", null);

                // Assert
                Assert.IsNotNull(targetValueContent);
                Assert.AreEqual(action.NewData, targetValueContent);
            }
        }
    }
}
