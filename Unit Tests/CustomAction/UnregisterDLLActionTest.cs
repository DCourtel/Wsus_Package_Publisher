using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour DeleteFileActionTest, destinée à contenir tous
    ///les tests unitaires DeleteFileActionTest
    ///</summary>
    [TestClass()]
    public class UnregisterDLLActionTest
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
        ///Test pour Constructeur DeleteFileAction
        ///</summary>
        [TestMethod()]
        public void UnregisterDLLActionConstructorTest()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.FullPath, "The property 'FullPath' is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();

            target.FullPath = @"C:\Windows\System32\Config.dll";
            string expected = "<Action>\r\n<ElementType>CustomActions.UnregisterDLLAction</ElementType>\r\n<FullPath>" + target.FullPath + "</FullPath>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");

            target.FullPath = @"  C:\Windows\System32\Config.dll   ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour FullPath
        ///</summary>
        [TestMethod()]
        public void FullPathTest()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\Config.dll";
            string actual;
            target.FullPath = expected;
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = string.Empty;
            actual = target.FullPath;
            Assert.AreEqual(string.Empty, actual, "The property 'FullPath' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = @"  C:\Windows\System32\Config.dll   ";
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'FullPath' can be set to null")]
        public void FullPathToNullText()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();
            target.FullPath = null;
        }

        [TestMethod()]
        public void ValidateDataTest()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.FullPath = @"C:\Windows\System32\file.dll";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.FullPath = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            UnregisterDLLAction target = new UnregisterDLLAction();

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
