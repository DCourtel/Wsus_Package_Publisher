using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{   
    /// <summary>
    ///Classe de test pour DeleteRegValueActionTest, destinée à contenir tous
    ///les tests unitaires DeleteRegValueActionTest
    ///</summary>
    [TestClass()]
    public class DeleteRegValueActionTest
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
        ///Test pour Constructeur DeleteRegValueAction
        ///</summary>
        [TestMethod()]
        public void DeleteRegValueActionConstructorTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.RegKey, "The property 'RegKey' is not properly initialized");
            Assert.AreEqual(String.Empty, target.ValueName, "The property 'ValueName' is not properly initialized");
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, target.Hive);
            Assert.IsFalse(target.RefersToHKeyCurrentUser, "The property 'referToHKeyCurrentUser' is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft";
            target.ValueName = "ValueToDelete";
            string expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_LOCAL_MACHINE</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"   HKEY_LOCAL_MACHINE\SOFTWARE\JavaSoft  ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"HKEY_Current_User\SOFTWARE\JavaSoft";
            expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_Current_User</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"\SOFTWARE\JavaSoft";
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_LOCAL_MACHINE</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"SOFTWARE\JavaSoft";
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_LOCAL_MACHINE</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"\SOFTWARE\JavaSoft";
            target.Hive = RegistryHelper.RegistryHive.HKey_Current_User;
            expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_Current_User</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");

            target.RegKey = @"SOFTWARE\JavaSoft";
            target.Hive = RegistryHelper.RegistryHive.HKey_Current_User;
            expected = "<Action>\r\n<ElementType>CustomActions.DeleteRegValueAction</ElementType>\r\n<Hive>HKEY_Current_User</Hive>\r\n<RegKey>SOFTWARE\\JavaSoft</RegKey>\r\n<ValueName>ValueToDelete</ValueName>\r\n<UseReg32>False</UseReg32></Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();
            target.ValidateData();
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.RegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.ValueName = "DisplayVersion";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.ValueName = String.Empty;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.ValueName = "DisplayVersion";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.Hive = RegistryHelper.RegistryHive.Undefined;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.Hive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
            target.RegKey = String.Empty;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
        }

        /// <summary>
        ///Test pour RegKey
        ///</summary>
        [TestMethod()]
        public void RegKeyTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();
            string expected = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            string actual;
            target.RegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies"; ;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(target.RefersToHKeyCurrentUser);

            expected = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies"; ;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
            
            expected = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies"; ;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            expected = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"\\\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies"; ;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            expected = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            target.RegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies"; ;
            actual = target.RegKey;
            Assert.AreEqual(expected, actual, true);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour ValueName
        ///</summary>
        [TestMethod()]
        public void ValueNameTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();
            string expected = "DisplayVersion";
            string actual;
            target.ValueName = "  DisplayVersion  ";
            actual = target.ValueName;
            Assert.AreEqual(expected, actual, true);
        }

        /// <summary>
        ///Test pour Hive
        ///</summary>
        [TestMethod()]
        public void HiveTest()
        {
            DeleteRegValueAction target = new DeleteRegValueAction();

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
            DeleteRegValueAction target = new DeleteRegValueAction();

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
