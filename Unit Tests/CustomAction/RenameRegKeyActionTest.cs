using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour RenameRegKeyActionTest, destinée à contenir tous
    ///les tests unitaires RenameRegKeyActionTest
    ///</summary>
    [TestClass()]
    public class RenameRegKeyActionTest
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
        ///Test pour Constructeur RenameRegKeyAction
        ///</summary>
        [TestMethod()]
        public void RenameRegKeyActionConstructorTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.RegKey, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(string.Empty, target.NewName, "The property 'NewName' is not properly initialized");
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            target.NewName = "Java-Soft";
            string expected = "<Action>\r\n<ElementType>CustomActions.RenameRegKeyAction</ElementType>\r\n<Hive>HKEY_LOCAL_MACHINE</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<UseReg32>False</UseReg32>\r\n<NewName>Java-Soft</NewName>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"   HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft  ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");
            target.NewName = "  Java-Soft   ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour NewName
        ///</summary>
        [TestMethod()]
        public void NewNameTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"SOFTWARE\JavaSoft";
            string actual;
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = "Java-Soft";
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
        ///Test pour RegKey
        ///</summary>
        [TestMethod()]
        public void RegKeyTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"SOFTWARE\JavaSoft";
            string actual;
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, "The property 'RegKey' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = "Java-Soft";
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
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly initialized");
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");
            target.NewName = "Java-Soft";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.RegKey = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.NewName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
            target.NewName = "Java-Soft";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        /// <summary>
        ///Test pour Hive
        ///</summary>
        [TestMethod()]
        public void HiveTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, target.Hive, "The property 'Hive' is not properly initialized");

            target.Hive = RegistryHelper.RegistryHive.HKey_Current_User;
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Current_User, target.Hive, "The property 'Hive' is not properly updated");
            Assert.IsTrue(target.RefersToHKeyCurrentUser);

            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, target.Hive);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            target.RegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies";
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Current_User, target.Hive);
            Assert.IsTrue(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void HiveNotificationTest()
        {
            RenameRegKeyAction target = new RenameRegKeyAction();

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
