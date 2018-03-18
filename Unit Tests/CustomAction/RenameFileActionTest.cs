using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour RenameFileTest, destinée à contenir tous
    ///les tests unitaires RenameFileTest
    ///</summary>
    [TestClass()]
    public class RenameFileActionTest
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
        ///Test pour Constructeur RenameFile
        ///</summary>
        [TestMethod()]
        public void RenameFileConstructorTest()
        {
            RenameFileAction target = new RenameFileAction();

            Assert.AreEqual(55, target.Height, "The 'Height' property is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.FullPath, "The 'FullPath' property is not properly initialized");
            Assert.AreEqual(string.Empty, target.NewName, "The 'NewName' property is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            RenameFileAction target = new RenameFileAction();

            target.FullPath = @"C:\Windows\System32\Config.txt";
            target.NewName = "Config.ini";
            string expected = "<Action>\r\n<ElementType>CustomActions.RenameFileAction</ElementType>\r\n<FullPath>" + target.FullPath + "</FullPath>\r\n<NewName>" + target.NewName + "</NewName>\r\n</Action>";

            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string.");
        }

        /// <summary>
        ///Test pour FullPath
        ///</summary>
        [TestMethod()]
        public void FullPathTest()
        {
            RenameFileAction target = new RenameFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\Config.ini";
            string actual;
            target.FullPath = expected;
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = "Config.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = string.Empty;
            actual = target.FullPath;
            Assert.AreEqual(string.Empty, actual, "The property 'FullPath' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = @"  C:\Windows\System32\Config.ini   ";
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'NewName' can be set to null")]
        public void FullPathToNullText()
        {
            RenameFileAction target = new RenameFileAction();
            target.FullPath = null;
        }

        /// <summary>
        ///Test pour NewName
        ///</summary>
        [TestMethod()]
        public void NewNameTest()
        {
            RenameFileAction target = new RenameFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"Config.ini";
            string actual;
            target.NewName = expected;
            actual = target.NewName;
            Assert.AreEqual(expected, actual, "The property 'NewName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = @"C:\Windows\System32\Config.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = string.Empty;
            actual = target.NewName;
            Assert.AreEqual(string.Empty, actual, "The property 'NewName' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.NewName = @"  Config.ini   ";
            actual = target.NewName;
            Assert.AreEqual(expected, actual, "The property 'NewName' is not not properly trimed.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'NewName' can be set to null")]
        public void NewNameToNullText()
        {
            RenameFileAction target = new RenameFileAction();
            target.NewName = null;
        }

        [TestMethod()]
        public void ValidateDataTest()
        {
            RenameFileAction target = new RenameFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.FullPath = @"C:\Windows\System32\file.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");
            target.NewName = @"file.ini";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.FullPath = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
            target.FullPath = @"C:\Windows\System32\file.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.NewName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            RenameFileAction target = new RenameFileAction();

            Assert.IsFalse(target.RefersToUserProfile, "The property 'RefersToUserProfile' is not properly initialized");

            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.FullPath = folder;
                Assert.IsTrue(target.RefersToUserProfile, folder + " should set 'RefersToUserProfile' to true.");
            }

            target.FullPath = @"C:\Temp";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.otherFolders)
            {
                target.FullPath = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " should not set 'RefersToUserProfile' to true.");
            }

        }
    }
}
