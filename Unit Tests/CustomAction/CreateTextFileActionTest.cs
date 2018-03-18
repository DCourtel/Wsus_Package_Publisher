using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{ 
    /// <summary>
    ///Classe de test pour CreateTextFileActionTest, destinée à contenir tous
    ///les tests unitaires CreateTextFileActionTest
    ///</summary>
    [TestClass()]
    public class CreateTextFileActionTest
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
        ///Test pour Constructeur CreateTextFileAction
        ///</summary>
        [TestMethod()]
        public void CreateTextFileActionConstructorTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.FilePath, "The Property 'FilePath' is not properly initialized");
            Assert.AreEqual(string.Empty, target.Filename, "The Property 'Filename' is not properly initialized");
            Assert.AreEqual(string.Empty, target.Content, "The Property 'Content' is not properly initialized");
            Assert.IsFalse(target.RefersToUserProfile);
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            target.FilePath = @"C:\Windows\System32";
            target.Filename = "Config.ini";
            target.Content = "This is a test for CreateTextFileAction.";

            string expected = "<Action>\r\n<ElementType>CustomActions.CreateTextFileAction</ElementType>\r\n<FilePath>" + target.FilePath  + "</FilePath>\r\n<Filename>" + target.Filename + "</Filename>\r\n<Content>" + target.Content + "</Content>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The GetXMLAction method doesn't return the expected string.");
        }

        /// <summary>
        ///Test pour Content
        ///</summary>
        [TestMethod()]
        public void ContentTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();
            string expected = "   This is a test for the 'Content' property. Some special characters @|éèöûîñ   ";
            string actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual, "The property 'Content' is not properly set.");
            
            target.Content = string.Empty;
            actual = target.Content;
            Assert.AreEqual(string.Empty, actual, "The property 'Content' can not be set to string.Empty.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'FilePath' can be set to null")]
        public void ContentToNullTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();
            target.Content = null;
        }

        /// <summary>
        ///Test pour FilePath
        ///</summary>
        [TestMethod()]
        public void FilePathTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\";
            string actual;
            target.FilePath = expected;
            actual = target.FilePath;
            Assert.AreEqual(expected, actual, "The property 'FilePath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.Filename = "Config.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.FilePath = string.Empty;
            actual = target.FilePath;
            Assert.AreEqual(string.Empty, actual, "The property 'DestinationFile' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FilePath = @"  C:\Windows\System32\Config.ini   ";
            actual = target.FilePath;
            Assert.AreEqual(target.FilePath, actual, "The property 'DestinationFile' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            Assert.IsFalse(target.RefersToUserProfile);
            target.FilePath = @"%appData%\Borland";
            Assert.IsTrue(target.RefersToUserProfile);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException),"The property 'FilePath' can be set to null")]
        public void FilePathToNullTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();
            target.FilePath = null;
        }

        /// <summary>
        ///Test pour Filename
        ///</summary>
        [TestMethod()]
        public void FilenameTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = "Config.txt";
            string actual;
            target.Filename = expected;
            actual = target.Filename;
            Assert.AreEqual(expected, actual, "The property 'Filename' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FilePath = @"C:\Windows\System32\";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.Filename = string.Empty;
            actual = target.Filename;
            Assert.AreEqual(string.Empty, actual, "The property 'Filename' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.Filename = "   Config.txt   ";
            actual = target.Filename;
            Assert.AreEqual(expected, actual, "The property 'Filename' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'FilePath' can be set to null")]
        public void FilenameToNullTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();
            target.Filename = null;
        }
        
        [TestMethod()]
        public void ValidateDataTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.FilePath = @"C:\Windows\System32";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");
            target.Filename = "file.ini";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.FilePath = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
            target.FilePath = @"C:\Windows\System32";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.Filename = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            CreateTextFileAction target = new CreateTextFileAction();

            Assert.IsFalse(target.RefersToUserProfile, "The property 'RefersToUserProfile' is not properly initialized");

            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.FilePath = folder;
                Assert.IsTrue(target.RefersToUserProfile, folder + " should set 'RefersToUserProfile' to true.");
            }

            target.FilePath = @"C:\Temp";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.otherFolders)
            {
                target.FilePath = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " should not set 'RefersToUserProfile' to true.");
            }

        }
    }
}
