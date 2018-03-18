using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour RenameRegValueActionTest, destinée à contenir tous
    ///les tests unitaires RenameRegValueActionTest
    ///</summary>
    [TestClass()]
    public class RenameRegValueActionTest
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
        ///Test pour Constructeur RenameRegValueAction
        ///</summary>
        [TestMethod()]
        public void RenameRegValueActionConstructorTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(String.Empty, target.RegKey, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(String.Empty, target.ValueName, "The property 'ValueName' is not properly initialized");
            Assert.AreEqual(String.Empty, target.NewName, "The property 'NewName' is not properly initialized");
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();
            string expected = "<Action>\r\n<ElementType>CustomActions.RenameRegValueAction</ElementType>\r\n<Hive>HKey_Local_Machine</Hive>\r\n<RegKey>SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies</RegKey>\r\n" +
                "<UseReg32>False</UseReg32>\r\n<ValueName>DisplayVersion</ValueName>\r\n<NewName>Display-Version</NewName>\r\n</Action>";
            string actual;
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.ValueName = "DisplayVersion";
            target.NewName = "Display-Version";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            expected = "<Action>\r\n<ElementType>CustomActions.RenameRegValueAction</ElementType>\r\n<Hive>HKey_Current_User</Hive>\r\n<RegKey>SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies</RegKey>\r\n" +
                "<UseReg32>False</UseReg32>\r\n<ValueName>DisplayVersion</ValueName>\r\n<NewName>Display-Version</NewName>\r\n</Action>";
            target.RegKey = @"HKEY_Current_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.ValueName = "DisplayVersion";
            target.NewName = "Display-Version";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true);
            Assert.IsTrue(target.RefersToHKeyCurrentUser);

            expected = "<Action>\r\n<ElementType>CustomActions.RenameRegValueAction</ElementType>\r\n<Hive>HKey_Local_Machine</Hive>\r\n<RegKey>SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies</RegKey>\r\n" +
                "<UseReg32>False</UseReg32>\r\n<ValueName>DisplayVersion</ValueName>\r\n<NewName>Display-Version</NewName>\r\n</Action>";
            target.RegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            target.ValueName = "DisplayVersion";
            target.NewName = "Display-Version";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();

            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.RegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.ValueName = "DisplayVersion";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.NewName = "Display-Version";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.NewName = String.Empty;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.NewName = "Display-Version";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.ValueName = String.Empty;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.ValueName = "DisplayVersion";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.RegKey = String.Empty;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.RegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
        }

        /// <summary>
        ///Test pour Hive
        ///</summary>
        [TestMethod()]
        public void HiveTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();

            Assert.IsFalse(target.RefersToHKeyCurrentUser);
            target.Hive = RegistryHelper.RegistryHive.HKey_Current_User;
            Assert.IsTrue(target.RefersToHKeyCurrentUser);
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
            target.Hive = RegistryHelper.RegistryHive.Undefined;
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour NewName
        ///</summary>
        [TestMethod()]
        public void NewNameTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();
            string expected = "Display-version";
            string actual;
            target.NewName = expected;
            actual = target.NewName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour RegKey
        ///</summary>
        [TestMethod()]
        public void RegKeyTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();
            
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"SOFTWARE\JavaSoft";
            string actual;
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            target.ValueName = "DisplayVersion";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = "Display-Version";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.RegKey = @"    HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft    ";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.RegKey = string.Empty;
            actual = target.RegKey;
            Assert.AreEqual(string.Empty, actual, "The property 'RegKey' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.RegKey = @"    HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft    ";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.NewName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");
        }

        /// <summary>
        ///Test pour ValueName
        ///</summary>
        [TestMethod()]
        public void ValueNameTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();
            string expected = "DisplayVersion";
            string actual;
            target.ValueName = expected;
            actual = target.ValueName;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void HiveNotificationTest()
        {
            RenameRegValueAction target = new RenameRegValueAction();

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
