using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using SUT = CustomUpdateEngine.AddRegValueAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class AddRegValueAction
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Current_User");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DisplayVersion");
                Assert.AreEqual(action.Data, "45.4.2.0");
                Assert.AreEqual(action.ValueType, "REG_SZ");
                Assert.IsFalse(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM32()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM32.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DisplayVersion");
                Assert.AreEqual(action.Data, "45.4.2.0");
                Assert.AreEqual(action.ValueType, "REG_SZ");
                Assert.IsTrue(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeProperties_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Hive, "HKey_Local_Machine");
                Assert.AreEqual(action.RegKey, @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test");
                Assert.AreEqual(action.ValueName, "DisplayVersion");
                Assert.AreEqual(action.Data, "45.4.2.0");
                Assert.AreEqual(action.ValueType, "REG_SZ");
                Assert.IsFalse(action.UseReg32);
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_SZ.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_SZ");
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_BINARY()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_BINARY.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_BINARY");
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_DWORD()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_DWORD.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_DWORD");
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_QWORD()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_QWORD.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_QWORD");
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_MULTI_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_MULTI_SZ.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_MULTI_SZ");
            }

            [TestMethod]
            public void ProperlyInitializeValueType_WhenCalledWithReg_EXPAND_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64_Reg_EXPAND_SZ.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.ValueType, "REG_EXPAND_SZ");
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateRegValue_WhenCalledWithHKCU()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(action.Data, newValue);
            }

            [TestMethod]
            public void CreateTheRegValue_WhenCalledWithHKLM()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM32.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32);
                var finalResult = Tools.GetReturnCodeAction();
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                object newValue = null;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(action.Data, newValue);
            }

            [TestMethod]
            public void CreateTheRegValue_WhenCalledWithHKLM64()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKLM64.CustAct"));
                RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry64);
                RegistryKey targetKey = hklm.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;

                // Act
                if (targetKey != null)
                    targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                if(targetKey == null)
                    targetKey = hklm.OpenSubKey(action.RegKey, false);
                newValue = targetKey.GetValue(action.ValueName, null);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(action.Data, newValue);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_SZ.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                var finalResult = Tools.GetReturnCodeAction();
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.String.ToString(), kindOfNewValue.ToString(), true);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_BINARY()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_BINARY.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.Binary.ToString(), kindOfNewValue.ToString(), true);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_DWORD()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_DWORD.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                var finalResult = Tools.GetReturnCodeAction();
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.DWord.ToString(), kindOfNewValue.ToString(), true);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_QWORD()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_QWORD.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.QWord.ToString(), kindOfNewValue.ToString(), true);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_EXPAND_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_EXPAND_SZ.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.ExpandString.ToString(), kindOfNewValue.ToString(), true);
            }

            [TestMethod]
            public void CreateAReg_SzValue_WhenCalledWithReg_MULTI_SZ()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                RegistryKey hkcu = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, Microsoft.Win32.RegistryView.Registry32);
                RegistryKey targetKey = hkcu.OpenSubKey(action.RegKey, true);
                var finalResult = Tools.GetReturnCodeAction();
                object newValue = null;
                RegistryValueKind kindOfNewValue;

                // Act
                if (targetKey != null)
                    targetKey.DeleteValue(action.ValueName, false);
                action.Run(ref finalResult);
                newValue = targetKey.GetValue(action.ValueName, null);
                kindOfNewValue = targetKey.GetValueKind(action.ValueName);

                // Assert
                Assert.IsNotNull(newValue);
                Assert.AreEqual(RegistryValueKind.MultiString.ToString(), kindOfNewValue.ToString(), true);
            }
        }

        [TestClass]
        public class GetStringData_Should
        {
            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeData()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithOneData()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(1, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndFinalBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\Data3\";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataStartingByBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"\Data1\Data2\Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithTwoDataAndEscapedBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\\Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(2, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual(@"Data2\Data3", actualData[1]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndBackslash0()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\0Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("0Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndFinalDoubleBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\Data3\\";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual(@"Data3\", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndStartingDoubleBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"\\Data1\Data2\Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual(@"\Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndStartingFourBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"\\\\Data1\Data2\Data3";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual(@"\\Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual("Data3", actualData[2]);
            }

            [TestMethod]
            public void ReturnCorrectStringArray_WhenCalledWithThreeDataAndEndingFourBackslash()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("AddRegValueToHKCU_Reg_MULTI_SZ.CustAct"));
                string testData = @"Data1\Data2\Data3\\\\";
                string[] actualData;

                // Act
                actualData = action.GetStringData(testData);

                // Assert
                Assert.AreEqual(3, actualData.Length);
                Assert.AreEqual("Data1", actualData[0]);
                Assert.AreEqual("Data2", actualData[1]);
                Assert.AreEqual(@"Data3\\", actualData[2]);
            }
        }
    }
}
