using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.RenameRegKeyAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RenameRegKeyAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.NewName, "Renamed");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKLM.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.IsTrue(action.UseReg32);
                Assert.AreEqual(action.NewName, "Renamed");
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"Software\EasyCompany\Wsus Package Publisher\Test");
                Assert.IsFalse(action.UseReg32);
                Assert.AreEqual(action.NewName, "Renamed");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void RenameTheRegKey_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKCU.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey keyToRename = hkcu.OpenSubKey(action.RegKey, false);
                string newKeyName = action.RegKey.Substring(0, action.RegKey.LastIndexOf(@"\")) + "\\" + action.NewName;
                RegistryKey renamedKey = hkcu.OpenSubKey(newKeyName);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (keyToRename == null)
                {
                    keyToRename = hkcu.CreateSubKey(action.RegKey);
                    Assert.IsNotNull(keyToRename);
                }
                if(renamedKey != null)
                {
                    hkcu.DeleteSubKeyTree(newKeyName);
                }
                action.Run(ref finalResult);
                renamedKey = hkcu.OpenSubKey(newKeyName);

                // Assert
                Assert.IsNotNull(renamedKey);
            }

            [TestMethod]
            public void RenameTheRegKey_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKLM.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey keyToRename = hklm.OpenSubKey(action.RegKey, false);
                string newKeyName = action.RegKey.Substring(0, action.RegKey.LastIndexOf(@"\")) + "\\" + action.NewName;
                RegistryKey renamedKey = hklm.OpenSubKey(newKeyName);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (keyToRename == null)
                {
                    keyToRename = hklm.CreateSubKey(action.RegKey);
                    Assert.IsNotNull(keyToRename);
                }
                if (renamedKey != null)
                {
                    hklm.DeleteSubKeyTree(newKeyName);
                }
                action.Run(ref finalResult);
                renamedKey = hklm.OpenSubKey(newKeyName);

                // Assert
                Assert.IsNotNull(renamedKey);
            }

            [TestMethod]
            public void RenameTheRegKey_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("RenameRegKeyHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey keyToRename = hklm.OpenSubKey(action.RegKey, false);
                string newKeyName = action.RegKey.Substring(0, action.RegKey.LastIndexOf(@"\")) + "\\" + action.NewName;
                RegistryKey renamedKey = hklm.OpenSubKey(newKeyName);
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (keyToRename == null)
                {
                    keyToRename = hklm.CreateSubKey(action.RegKey);
                    Assert.IsNotNull(keyToRename);
                }
                if (renamedKey != null)
                {
                    hklm.DeleteSubKeyTree(newKeyName);
                }
                action.Run(ref finalResult);
                renamedKey = hklm.OpenSubKey(newKeyName);

                // Assert
                Assert.IsNotNull(renamedKey);
            }
        }
    }
}
