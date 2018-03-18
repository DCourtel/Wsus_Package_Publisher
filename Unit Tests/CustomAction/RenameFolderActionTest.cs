using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour RenameFolderActionTest, destinée à contenir tous
    ///les tests unitaires RenameFolderActionTest
    ///</summary>
    [TestClass()]
    public class RenameFolderActionTest
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
        ///Test pour Constructeur RenameFolderAction
        ///</summary>
        [TestMethod()]
        public void RenameFolderActionConstructorTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized.");
            Assert.IsTrue(target.IsTemplate, "The 'Template' property is not properly initialized.");
            Assert.IsFalse(target.IsSelected, "the 'IsSelected' property is not properly initialized.");
            Assert.AreEqual(string.Empty, target.FolderPath, "The property 'FolderPath' is not properly initialized.");
            Assert.AreEqual(string.Empty, target.NewName, "The property 'NewName' is not properly initialized.");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            string expected = "<Action>\r\n<ElementType>CustomActions.RenameFolderAction</ElementType>\r\n<FolderPath>" + target.FolderPath + "</FolderPath>\r\n<NewName>" + target.NewName + "</NewName>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string.");
        }

        /// <summary>
        ///Test pour FolderPath
        ///</summary>
        [TestMethod()]
        public void FolderPathTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\OldConfig";
            string actual;
            target.FolderPath = expected;
            actual = target.FolderPath;
            Assert.AreEqual(expected, actual, "The property 'FolderPath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = "Config";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.FolderPath = string.Empty;
            actual = target.FolderPath;
            Assert.AreEqual(string.Empty, actual, "The property 'FolderPath' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FolderPath = @"  C:\Windows\System32\OldConfig   ";
            actual = target.FolderPath;
            Assert.AreEqual(expected, actual, "The property 'FolderPath' is not not properly trimed.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "'FolderPath' can be set to null.")]
        public void FolderPathToNullTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.AreEqual<string>(string.Empty, target.FolderPath, "The property 'FolderPath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The 'ConfigurationState' is not properly initialized.");

            target.FolderPath = null;
        }

        /// <summary>
        ///Test pour NewName
        ///</summary>
        [TestMethod()]
        public void NewNameTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.AreEqual<string>(string.Empty, target.NewName, "The property 'NewName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The 'ConfigurationState' is not properly initialized.");

            string expected = "NewName";
            string actual;
            target.NewName = expected;
            actual = target.NewName;
            Assert.AreEqual(expected, actual, "The property 'NewName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The 'ConfigurationState' is not properly updated.");

            target.FolderPath = @"C:\Windows\System32\OldFolder";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The 'ConfigurationState' is not properly updated.");
            target.NewName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The 'ConfigurationState' doesn't revert back to 'MisConfigured' when set to string.Empty.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "'NewName' can be set to null.")]
        public void NewNameToNullTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.AreEqual<string>(string.Empty, target.NewName, "The property 'NewName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The 'ConfigurationState' is not properly initialized.");

            target.NewName = null;
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "'ConfigurationState' is not properly initialized.");

            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "'ConfigurationState' is not properly updated.");

            target.FolderPath = @"C:\Windows\System32\OldFolder";
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "'ConfigurationState' is not properly updated.");
            
            target.NewName = "NewName";
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "'ConfigurationState' is not properly updated.");

            target.FolderPath = string.Empty;
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "'ConfigurationState' is not properly updated.");

            target.FolderPath = @"C:\Windows\System32\OldFolder";
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "'ConfigurationState' is not properly updated.");

            target.NewName = string.Empty;
            target.ValidateData();
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "'ConfigurationState' is not properly updated.");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            RenameFolderAction target = new RenameFolderAction();

            Assert.IsFalse(target.RefersToUserProfile, "The property 'RefersToUserProfile' is not properly initialized");

            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.FolderPath = folder;
                Assert.IsTrue(target.RefersToUserProfile, folder + " should set 'RefersToUserProfile' to true.");
            }

            target.FolderPath = @"C:\Temp";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.otherFolders)
            {
                target.FolderPath = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " should not set 'RefersToUserProfile' to true.");
            }

        }
    }
}
