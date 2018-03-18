using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour RunVbScriptActionTest, destinée à contenir tous
    ///les tests unitaires RunVbScriptActionTest
    ///</summary>
    [TestClass()]
    public class RunVbScriptActionTest
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
        ///Test pour Constructeur RunVbScriptAction
        ///</summary>
        [TestMethod()]
        public void RunVbScriptActionConstructorTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(string.Empty, target.FullPath, "The property 'FullPath' is not properly initialized");
            Assert.AreEqual(string.Empty, target.Parameters, "The property 'Parameters' is not properly initialized");
            Assert.IsFalse(target.KillProcess, "The property 'KillProcess' is not properly initialized.");
            Assert.AreEqual(10, target.DelayBeforeKilling, "The property 'DelayBeforKilling' is not properly initialized.");
            Assert.IsFalse(target.StoreToVariable, "The property 'StoreToVariable' is not properly initialized.");
        }

        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            target.FullPath = @"C:\Windows\System32\Config.vbs";
            target.Parameters = @"\u AC76BA86-7AD7-1036-7B44-AB0000000001 \qn \norestart";
            target.KillProcess = true;
            target.DelayBeforeKilling = 5;
            target.StoreToVariable = true;

            string expected = "<Action>\r\n<ElementType>CustomActions.RunVbScriptAction</ElementType>\r\n<FullPath>" + target.FullPath +
                 "</FullPath>\r\n<Parameters>" + target.Parameters +
                 "</Parameters>\r\n<KillProcess>" + "true" +
                 "</KillProcess>\r\n<DelayBeforeKilling>" + target.DelayBeforeKilling.ToString() + "</DelayBeforeKilling>\r\n<StoreToVariable>true</StoreToVariable>\r\n</Action>";
            string actual;
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual,true, "The 'GetXMLAction' method doesn't return the good string");

            target.FullPath = @"  C:\Windows\System32\Config.vbs   ";
            expected = "<Action>\r\n<ElementType>CustomActions.RunVbScriptAction</ElementType>\r\n<FullPath>" + target.FullPath +
                 "</FullPath>\r\n<Parameters>" + target.Parameters +
                 "</Parameters>\r\n<KillProcess>" + "true" +
                 "</KillProcess>\r\n<DelayBeforeKilling>" + target.DelayBeforeKilling.ToString() + "</DelayBeforeKilling>\r\n<StoreToVariable>true</StoreToVariable>\r\n</Action>";
            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual,true, "The 'GetXMLAction' method doesn't return the good string");
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void ValidateDataTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.FullPath = @"C:\Windows\System32\file.vbs";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.FullPath = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        ///Test pour FullPath
        ///</summary>
        [TestMethod()]
        public void FullPathTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\Config.vbs";
            string actual;
            target.FullPath = expected;
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = string.Empty;
            actual = target.FullPath;
            Assert.AreEqual(string.Empty, actual, "The property 'FullPath' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.FullPath = @"  C:\Windows\System32\Config.vbs   ";
            actual = target.FullPath;
            Assert.AreEqual(expected, actual, "The property 'FullPath' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'FullPath' can be set to null")]
        public void FullPathToNullText()
        {
            RunVbScriptAction target = new RunVbScriptAction();
            target.FullPath = null;
        }
        
        /// <summary>
        ///Test pour KillProcess
        ///</summary>
        [TestMethod()]
        public void KillProcessTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            Assert.IsFalse(target.KillProcess, "The property is not properly initialized.");

            bool expected = true;
            bool actual;
            target.KillProcess = expected;
            actual = target.KillProcess;
            Assert.AreEqual(expected, actual, "The property is not properly set.");
            Assert.IsTrue(target.GetXMLAction().Contains("<KillProcess>" + expected.ToString() + "</KillProcess>"), "The property 'KillProcess' is not correctly encoded in the XmlAction string.");
        }

        /// <summary>
        ///Test pour Parameters
        ///</summary>
        [TestMethod()]
        public void ParametersTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();
            string expected = "/Uninstall /NoRestart";
            string actual;
            target.Parameters = expected;
            actual = target.Parameters;
            Assert.AreEqual(expected, actual,"The property 'Parameters' is not properly initialized.");

            target.Parameters = string.Empty;
            actual = target.Parameters;
            Assert.AreEqual(string.Empty, actual, "The property 'Parameters' can not be set to string.Empty.");

            target.Parameters = "  /Uninstall /NoRestart  ";
            actual = target.Parameters;
            Assert.AreEqual(expected, actual, "The property 'Parameters' is not properly trimed.");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'Parameters' can be set to null")]
        public void ParametersToNullText()
        {
            RunVbScriptAction target = new RunVbScriptAction();
            target.Parameters = null;
        }
        
        /// <summary>
        ///Test pour StoreToVariable
        ///</summary>
        [TestMethod()]
        public void StoreToVariableTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            bool expected = false;
            bool actual;
            target.StoreToVariable = expected;
            actual = target.StoreToVariable;
            Assert.AreEqual(expected, actual, "The property 'StoreToVariable' is not properly initialized.");
            Assert.IsTrue(target.GetXMLAction().Contains("<StoreToVariable>" + expected.ToString() + "</StoreToVariable>"), "The property 'StoreToVariable' is not correctly encoded in the XmlAction string.");

            expected = true;
            target.StoreToVariable = expected;
            actual = target.StoreToVariable;
            Assert.AreEqual(expected, actual, "The property 'StoreToVariable' is not properly set.");
            Assert.IsTrue(target.GetXMLAction().Contains("<StoreToVariable>" + expected.ToString() + "</StoreToVariable>"), "The property 'StoreToVariable' is not correctly encoded in the XmlAction string.");
        }

        /// <summary>
        ///Test pour DelayBeforeKilling
        ///</summary>
        [TestMethod()]
        public void DelayBeforeKillingTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

            Assert.AreEqual(target.DelayBeforeKilling, 10, "The property 'DelayBeforeKilling' is not properly initialized.");

            int expected = 1;
            int actual;
            target.DelayBeforeKilling = expected;
            actual = target.DelayBeforeKilling;
            Assert.AreEqual(expected, actual, "The property 'DelayBeforeKilling' is not properly set.");
            Assert.IsTrue(target.GetXMLAction().Contains("<DelayBeforeKilling>" + expected.ToString() + "</DelayBeforeKilling>"), "The property 'DelayBeoreKilling' is not correctly encoded in the XmlAction string.");

            expected = 120;
            target.DelayBeforeKilling = expected;
            actual = target.DelayBeforeKilling;
            Assert.AreEqual(expected, actual, "The property 'DelayBeforeKilling' is not properly set.");
            Assert.IsTrue(target.GetXMLAction().Contains("<DelayBeforeKilling>" + expected.ToString() + "</DelayBeforeKilling>"), "The property 'DelayBeoreKilling' is not correctly encoded in the XmlAction string.");

            expected = 120;
            target.DelayBeforeKilling = 121;
            actual = target.DelayBeforeKilling;
            Assert.AreEqual(expected, actual, "The property 'DelayBeforeKilling' doesn't respect maximum.");

            expected = 120;
            target.DelayBeforeKilling = 0;
            actual = target.DelayBeforeKilling;
            Assert.AreEqual(expected, actual, "The property 'DelayBeforeKilling' doesn't respect minimum.");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            RunVbScriptAction target = new RunVbScriptAction();

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
