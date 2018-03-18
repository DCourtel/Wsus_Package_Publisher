using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour AddRegValueActionTest, destinée à contenir tous
    ///les tests unitaires AddRegValueActionTest
    ///</summary>
    [TestClass()]
    public class AddRegValueActionTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test pour Constructeur AddRegValueAction
        ///</summary>
        [TestMethod()]
        public void AddRegValueActionConstructorTest()
        {
            AddRegValueAction target = new AddRegValueAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.RegKey, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(string.Empty, target.ValueName, "The property 'ValueName' is not properly initialized");
            Assert.AreEqual(string.Empty, target.Data, "The property 'Data' is not properly initialized");
            Assert.IsTrue(target.Type == CustomActions.RegistryHelper.ValueType.REG_SZ, "The property 'Type' is not properly initialized");
            Assert.IsFalse(target.RefersToHKeyCurrentUser, "The property 'RefersToCurrentUser' is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            AddRegValueAction target = new AddRegValueAction();

            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.ValueName = "DisplayVersion";
            target.Type = CustomActions.RegistryHelper.ValueType.REG_SZ;
            target.Data = "1.2.3.4";

            string expected = "<Action>\r\n<ElementType>CustomActions.AddRegValueAction</ElementType>\r\n<Hive>HKEY_LOCAL_MACHINE</Hive>\r\n<RegKey>" + target.RegKey + "</RegKey>\r\n" +
                "<ValueName>DisplayVersion</ValueName>\r\n" +
                "<Data>1.2.3.4</Data>\r\n" +
                "<Type>" + CustomActions.RegistryHelper.ValueType.REG_SZ.ToString() + "</type>\r\n<UseReg32>False</UseReg32></Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' doesn't return the good string");
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {            
            AddRegValueAction target = new AddRegValueAction();
            

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly initialized");
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly initialized");
            target.RegKey = @"HKLM\Software\Vnc";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.ValueName = "DisplayVersion";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.Type = CustomActions.RegistryHelper.ValueType.REG_SZ;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.Data = "1.2.3.4";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The configuration State is not properly updated");

            target.RegKey = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.RegKey = @"HKLM\Software\Vnc";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The configuration State is not properly updated");
            target.ValueName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.ValueName = "DisplayVersion";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The configuration State is not properly updated");
            target.Data = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The configuration State is not properly updated");
            target.Data = "1.2.3.4";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The configuration State is not properly updated");
        }

        /// <summary>
        ///Test pour Data
        ///</summary>
        [TestMethod()]
        public void DataTest()
        {
            AddRegValueAction target = new AddRegValueAction();
            string expected = "1.2.3.4";
            string actual;
            target.Data = expected;
            actual = target.Data;
            Assert.AreEqual(expected, actual, "The property 'Data' is not properly initialized");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'Data' can be set to null")]
        public void DataToNullText()
        {
            AddRegValueAction target = new AddRegValueAction();
            target.Data = null;
        }

        /// <summary>
        ///Test pour RegKey
        ///</summary>
        [TestMethod()]
        public void RegKeyTest()
        {
            AddRegValueAction target = new AddRegValueAction();
            string expected = @"Software\Vnc";
            string actual;
            target.RegKey = expected;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized");

            expected = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, target.Hive, "The Hive is not set properly");

            expected = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"HKEY_Current_User\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Current_User, target.Hive, "The Hive is not set properly");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'RegKey' can be set to null")]
        public void RegKeyToNullText()
        {
            AddRegValueAction target = new AddRegValueAction();
            target.RegKey = null;
        }

        /// <summary>
        ///Test pour Type
        ///</summary>
        [TestMethod()]
        public void TypeTest()
        {
            AddRegValueAction target = new AddRegValueAction();
            CustomActions.RegistryHelper.ValueType expected = CustomActions.RegistryHelper.ValueType.REG_DWORD;
            CustomActions.RegistryHelper.ValueType actual;
            target.Type = expected;
            actual = target.Type;
            Assert.AreEqual(expected, actual, "The property 'Type' is not properly updated");
        }

        /// <summary>
        ///Test pour ValueName
        ///</summary>
        [TestMethod()]
        public void ValueNameTest()
        {
            AddRegValueAction target = new AddRegValueAction();
            string expected = "Displayversion";
            string actual;
            target.ValueName = expected;
            actual = target.ValueName;
            Assert.AreEqual(expected, actual, "The property 'ValueName' is not properly initialized");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'ValueName' can be set to null")]
        public void ValueNameToNullText()
        {
            AddRegValueAction target = new AddRegValueAction();
            target.ValueName = null;
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void HiveNotificationTest()
        {
            AddRegValueAction target = new AddRegValueAction();

            Assert.IsFalse(target.RefersToHKeyCurrentUser, "The property 'RefersToHKeyCurrentUser' is not properly initialized");
            target.Hive = RegistryHelper.RegistryHive.HKey_Current_User;
            Assert.IsTrue(target.RefersToHKeyCurrentUser, "The property 'RefersToHKeyCurrentUser' is not properly updated");
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            Assert.IsFalse(target.RefersToHKeyCurrentUser, "The property 'RefersToHKeyCurrentUser' is not properly updated");
            target.Hive = RegistryHelper.RegistryHive.Undefined;
            Assert.IsFalse(target.RefersToHKeyCurrentUser, "The property 'RefersToHKeyCurrentUser' is not properly updated");
        }

    }
}
