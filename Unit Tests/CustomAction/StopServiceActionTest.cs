using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour StopServiceActionTest, destinée à contenir tous
    ///les tests unitaires StopServiceActionTest
    ///</summary>
    [TestClass()]
    public class StopServiceActionTest
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
        ///Test pour Constructeur StopServiceAction
        ///</summary>
        [TestMethod()]
        public void StopServiceActionConstructorTest()
        {
            StopServiceAction target = new StopServiceAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.ServiceName, "The property 'ServiceName' is not properly initialized");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            StopServiceAction target = new StopServiceAction();

            target.ServiceName = "AdobeARMservice";
            string expected = "<Action>\r\n<ElementType>CustomActions.StopServiceAction</ElementType>\r\n<ServiceName>" + target.ServiceName + "</ServiceName>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");

            target.ServiceName = "   AdobeARMservice  ";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            StopServiceAction target = new StopServiceAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.ServiceName = "AdobeARMservice";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.ServiceName = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        ///Test pour ServiceName
        ///</summary>
        [TestMethod()]
        public void ServiceNameTest()
        {
            StopServiceAction target = new StopServiceAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = "AdobeARMservice";
            string actual;
            target.ServiceName = expected;
            actual = target.ServiceName;
            Assert.AreEqual(expected, actual, "The property 'ServiceName' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.ServiceName = string.Empty;
            actual = target.ServiceName;
            Assert.AreEqual(string.Empty, actual, "The property 'ServiceName' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.ServiceName = "  AdobeARMservice   ";
            actual = target.ServiceName;
            Assert.AreEqual(expected, actual, "The property 'ServiceName' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'ServiceName' can be set to null")]
        public void ServiceNameToNullText()
        {
            StopServiceAction target = new StopServiceAction();
            target.ServiceName = null;
        }

    }
}
