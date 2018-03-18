using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.RenameRegValueAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RenameRegValueAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "RenameMe");
                Assert.AreEqual(action.NewName, "Renamed");
                Assert.IsFalse(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM32()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKLM.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "RenameMe");
                Assert.AreEqual(action.NewName, "Renamed");
                Assert.IsTrue(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "RenameMe");
                Assert.AreEqual(action.NewName, "Renamed");
                Assert.IsFalse(action.UseReg32);
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void RenameRegValue_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKCU.CustAct"));
                RegistryKey targetKey = Tools.CreateRegistryKey(RegistryHive.CurrentUser, action.RegKey, action.UseReg32);
                Tools.CreateRegistryValue(targetKey, action.ValueName, RegistryValueKind.DWord, 10);
                Tools.DeleteRegistryValue(targetKey, action.NewName);
                var finalResult = Tools.GetReturnCodeAction();                

                // Act
                Assert.IsNotNull(targetKey.GetValue(action.ValueName, null));
                Assert.IsNull(targetKey.GetValue(action.NewName, null));
                action.Run(ref finalResult);

                // Assert
                Assert.IsNotNull(targetKey.GetValue(action.NewName, null));
                Assert.IsNull(targetKey.GetValue(action.ValueName, null));
            }

            [TestMethod]
            public void RenameTheRegValue_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKLM.CustAct"));
                RegistryKey targetKey = Tools.CreateRegistryKey(RegistryHive.LocalMachine, action.RegKey, action.UseReg32);
                Tools.CreateRegistryValue(targetKey, action.ValueName, RegistryValueKind.DWord, 10);
                Tools.DeleteRegistryValue(targetKey, action.NewName);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Assert.IsNotNull(targetKey.GetValue(action.ValueName, null));
                Assert.IsNull(targetKey.GetValue(action.NewName, null));
                action.Run(ref finalResult);

                // Assert
                Assert.IsNotNull(targetKey.GetValue(action.NewName, null));
                Assert.IsNull(targetKey.GetValue(action.ValueName, null));
            }

            [TestMethod]
            public void RenameTheRegValue_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegValueHKLM64.CustAct"));
                RegistryKey targetKey = Tools.CreateRegistryKey(RegistryHive.LocalMachine, action.RegKey, action.UseReg32);
                Tools.CreateRegistryValue(targetKey, action.ValueName, RegistryValueKind.DWord, 10);
                Tools.DeleteRegistryValue(targetKey, action.NewName);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                Assert.IsNotNull(targetKey.GetValue(action.ValueName, null));
                Assert.IsNull(targetKey.GetValue(action.NewName, null));
                action.Run(ref finalResult);

                // Assert
                Assert.IsNotNull(targetKey.GetValue(action.NewName, null));
                Assert.IsNull(targetKey.GetValue(action.ValueName, null));
            }
        }
    }
}
