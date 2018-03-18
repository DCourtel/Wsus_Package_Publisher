using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour CopyFileActionTest, destinée à contenir tous
    ///les tests unitaires CopyFileActionTest
    ///</summary>
    [TestClass()]
    public class CopyFileActionTest
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
        ///Test pour Constructeur CopyFileAction
        ///</summary>
        [TestMethod()]
        public void CopyFileActionConstructorTest()
        {
            CopyFileAction target = new CopyFileAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.SourceFile, "The property 'SourceFile' is not properly initialized");
            Assert.AreEqual(string.Empty, target.DestinationFolder, "The property 'DestinationFile' is not properly initialized");
            Assert.IsFalse(target.RefersToUserProfile);
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            CopyFileAction target = new CopyFileAction();

            target.SourceFile = @"C:\Windows\Config.txt";
            target.DestinationFolder = @"C:\Windows\";

            string expected = "<Action>\r\n<ElementType>CustomActions.CopyFileAction</ElementType>\r\n<SourceFile>" + target.SourceFile + "</SourceFile>\r\n<DestinationFolder>" + target.DestinationFolder + "</DestinationFolder>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The method 'GetXMLAction' doesn't return the good string.");
        }

        /// <summary>
        ///Test pour DestinationFile
        ///</summary>
        [TestMethod()]
        public void DestinationFolderTest()
        {
            CopyFileAction target = new CopyFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32";
            string actual;
            target.DestinationFolder = expected;
            actual = target.DestinationFolder;
            Assert.AreEqual(expected, actual, "The property 'DestinationFile' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.SourceFile = @"C:\Windows\System32";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.DestinationFolder = string.Empty;
            actual = target.DestinationFolder;
            Assert.AreEqual(string.Empty, actual, "The property 'DestinationFile' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.DestinationFolder = @"  C:\Windows\System32   ";
            actual = target.DestinationFolder;
            Assert.AreEqual(expected, actual, "The property 'DestinationFile' is not not properly trimed.");

            Assert.IsFalse(target.RefersToUserProfile);
            target.DestinationFolder = "%AppData%";
            Assert.IsTrue(target.RefersToUserProfile);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException),"The property 'DestinationFolder' can be set to null")]
        public void DestinationFolderToNullText()
        {
            CopyFileAction target = new CopyFileAction();
            target.DestinationFolder = null;
        }
        
        /// <summary>
        ///Test pour SourceFile
        ///</summary>
        [TestMethod()]
        public void SourceFileTest()
        {
            CopyFileAction target = new CopyFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\Config.ini";
            string actual;
            target.SourceFile = expected;
            actual = target.SourceFile;
            Assert.AreEqual(expected, actual, "The property 'DestinationFile' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.DestinationFolder = @"C:\Windows\System32";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.SourceFile = string.Empty;
            actual = target.SourceFile;
            Assert.AreEqual(string.Empty, actual, "The property 'DestinationFile' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.SourceFile = @"  C:\Windows\System32\Config.ini   ";
            actual = target.SourceFile;
            Assert.AreEqual(expected, actual, "The property 'DestinationFile' is not not properly trimed.");

            Assert.IsFalse(target.RefersToUserProfile);
            target.SourceFile = @"%appdata%\config.txt";
            Assert.IsTrue(target.RefersToUserProfile);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException),"The property 'SourceFile' can be set to null")]
        public void SourceFileToNullText()
        {
            CopyFileAction target = new CopyFileAction();
            target.SourceFile = null;
        }

        [TestMethod()]
        public void ValidateDataTest()
        {
            CopyFileAction target = new CopyFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.SourceFile = @"C:\Windows\System32\file.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");
            target.DestinationFolder = @"Windows\System32\file.ini";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.SourceFile = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
            target.SourceFile = @"C:\Windows\System32\file.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.DestinationFolder = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            CopyFileAction target = new CopyFileAction();

            Assert.IsFalse(target.RefersToUserProfile, "The property 'RefersToUserProfile' is not properly initialized");

            target.DestinationFolder = @"C:\Temp";
            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.SourceFile = folder + @"\test.txt";
                Assert.IsTrue(target.RefersToUserProfile, folder + " SourceFile should set 'RefersToUserProfile' to true.");
            }

            target.SourceFile = @"C:\Temp\Test.txt";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.DestinationFolder = folder;
                Assert.IsTrue(target.RefersToUserProfile, folder + " as DestinationFolder should set 'RefersToUserProfile' to true.");
            }

            target.DestinationFolder = @"C:\Temp";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.otherFolders)
            {
                target.DestinationFolder = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " as DestinationFolder should not set 'RefersToUserProfile' to true.");
            }

            foreach (string folder in CommonData.otherFolders)
            {
                target.SourceFile = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " as SourceFile should not set 'RefersToUserProfile' to true.");
            }

        }
    }
}
