using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour CreateFolderActionTest, destinée à contenir tous
    ///les tests unitaires CreateFolderActionTest
    ///</summary>
    [TestClass()]
    public class WaitActionTest
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
        ///Test pour Constructeur CreateFolderAction
        ///</summary>
        [TestMethod()]
        public void WaitActionConstructorTest()
        {
            WaitAction target = new WaitAction();

            Assert.IsTrue(target.IsTemplate, "The Property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The Property 'IsSelected' is not properly initialized");
            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized.");
            Assert.AreEqual(15, target.AmountOfTime, "The property 'AmountOfTime' is not properly initialized.");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            WaitAction target = new WaitAction();

            target.AmountOfTime = 45;

            string expected = "<Action>\r\n<ElementType>CustomActions.WaitAction</ElementType>\r\n<AmountOfTime>" + target.AmountOfTime + "</AmountOfTime>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, "The method 'GetXMLAction' doesn't return the good string.");
        }

        [TestMethod()]
        public void AmountOfTimeTest()
        {
            WaitAction target = new WaitAction();

            Assert.AreEqual(15, target.AmountOfTime);

            target.AmountOfTime = -10;
            Assert.AreEqual(1, target.AmountOfTime, "AmountOfTime can be set below 1");

            target.AmountOfTime = 700;
            Assert.AreEqual(600, target.AmountOfTime, "AmountOfTime can be set above 600");
        }
    }
}
