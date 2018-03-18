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
    public class KillProcessActionTest
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
        public void KillProcessActionConstructorTest()
        {
            KillProcessAction target = new KillProcessAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.ProcessName, "The property 'ProcessName' is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            KillProcessAction target = new KillProcessAction();

            target.ProcessName = "notepad.exe";
            string expected = "<Action>\r\n<ElementType>CustomActions.KillProcessAction</ElementType>\r\n<ProcessName>" + target.ProcessName + "</ProcessName>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");

            target.ProcessName = "   notepad.exe    ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour FullPath
        ///</summary>
        [TestMethod()]
        public void ProcessNameTest()
        {
            KillProcessAction target = new KillProcessAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = "notepad.exe";
            string actual;
            target.ProcessName = expected;
            actual = target.ProcessName;
            Assert.AreEqual(expected, actual, "The property 'ProcessName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.ProcessName = string.Empty;
            actual = target.ProcessName;
            Assert.AreEqual(string.Empty, actual, "The property 'ProcessName' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.ProcessName = @"  notepad.exe   ";
            actual = target.ProcessName;
            Assert.AreEqual(expected, actual, "The property 'ProcessName' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'ProcessName' can be set to null")]
        public void KillProcessActionText()
        {
            KillProcessAction target = new KillProcessAction();
            target.ProcessName = null;
        }

        [TestMethod()]
        public void ValidateDataTest()
        {
            KillProcessAction target = new KillProcessAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.ProcessName = "notepad.exe";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.ProcessName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }
    }
}
