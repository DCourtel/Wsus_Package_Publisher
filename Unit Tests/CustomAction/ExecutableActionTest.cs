using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour ExecutableActionTest, destinée à contenir tous
    ///les tests unitaires ExecutableActionTest
    ///</summary>
    [TestClass()]
    public class ExecutableActionTest
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
        ///Test pour Constructeur ExecutableAction
        ///</summary>
        [TestMethod()]
        public void ExecutableActionConstructorTest()
        {
            ExecutableAction target = new ExecutableAction();
            
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not initialized properly.");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not initialized properly.");
            Assert.IsTrue(!string.IsNullOrEmpty(target.Text), "The property 'Text' is not initialized properly.");
            Assert.IsTrue(target.PathToTheFile == string.Empty, "The property 'PathToTheFile' is not properly initialized.");
            Assert.AreEqual(string.Empty, target.Parameters, "The property 'Parameters' is not properly initialized.");
            Assert.IsFalse(target.KillProcess, "The property 'KillProcess' is not properly initialized.");
            Assert.AreEqual(10, target.DelayBeforeKilling, "The property 'DelayBeforKilling' is not properly initialized.");
            Assert.IsFalse(target.StoreToVariable, "The property 'StoreToVariable' is not properly initialized.");
            Assert.AreEqual(55, target.Height, "The property 'Height' is not porperly initialized.");
        }

        /// <summary>
        ///Test pour chkBxKillProcess_CheckedChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void nupKillProcessTest()
        {
            ExecutableAction target = new ExecutableAction();

            Assert.IsFalse(target.nupKillProcess.Enabled, "The 'nupKillProcess' is not properly initialized.");

            target.chkBxKillProcess.Checked = true;
            Assert.IsTrue(target.nupKillProcess.Enabled, "The 'nupKillProcess' is not properly set.");
        }

        /// <summary>
        ///Test pour PathToTheFile
        ///</summary>
        [TestMethod()]
        public void PathToTheFileTest()
        {
            ExecutableAction target = new ExecutableAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            string expected = @"C:\Windows\System32\Config.ini";
            string actual;
            target.PathToTheFile = expected;
            actual = target.PathToTheFile;
            Assert.AreEqual(expected, actual, "The property 'PathToTheFile' is not properly initialized.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");

            target.PathToTheFile = string.Empty;
            actual = target.PathToTheFile;
            Assert.AreEqual(string.Empty, actual, "The property 'PathToTheFile' cannot be set to string.Empty.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' is not properly updated");

            target.PathToTheFile = @"  C:\Windows\System32\Config.ini   ";
            actual = target.PathToTheFile;
            Assert.AreEqual(expected, actual, "The property 'PathToTheFile' is not not properly trimed.");
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'PathToTheFile' can be set to null")]
        public void PathToTheFileToNullText()
        {
            ExecutableAction target = new ExecutableAction();
            target.PathToTheFile = null;
        }

        [TestMethod()]
        public void ValidateDataTest()
        {
            ExecutableAction target = new ExecutableAction();

            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.NotConfigured, "The property 'ConfigurationState' is not properly initialized");
            target.PathToTheFile = @"C:\Windows\System32\file.txt";
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Configured, "The property 'ConfigurationState' is not properly updated");
            target.PathToTheFile = string.Empty;
            Assert.IsTrue(target.ConfigurationState == GenericAction.ConfigurationStates.Misconfigured, "The property 'ConfigurationState' doesn't revert back to Misconfigured");
        }

        /// <summary>
        ///Test pour DelayBeforeKilling
        ///</summary>
        [TestMethod()]
        public void DelayBeforeKillingTest()
        {
            ExecutableAction target = new ExecutableAction();

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
        ///Test pour KillProcess
        ///</summary>
        [TestMethod()]
        public void KillProcessTest()
        {
            ExecutableAction target = new ExecutableAction();

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
            ExecutableAction target = new ExecutableAction();

            Assert.IsTrue(target.Parameters == string.Empty, "The property 'Parameters' is not properly initialized.");

            string expected = "-k";
            string actual;
            target.Parameters = expected;
            actual = target.Parameters;
            Assert.AreEqual(expected, actual, "The property 'Parameters' is not properly set.");
            Assert.IsTrue(target.GetXMLAction().Contains("<Parameters>" + expected + "</Parameters>"), "The property 'Parameters' is not correctly encoded in the XmlAction string.");            
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException), "The property 'Parameters' can be set to null")]
        public void ParametersToNullText()
        {
            ExecutableAction target = new ExecutableAction();
            target.Parameters = null;
        }
        
        /// <summary>
        ///Test pour StoreToVariable
        ///</summary>
        [TestMethod()]
        public void StoreToVariableTest()
        {
            ExecutableAction target = new ExecutableAction();
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
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            ExecutableAction target = new ExecutableAction();

            target.PathToTheFile = @"C:\Windows\System32\MsiExec.exe";
            target.Parameters = @"\u AC76BA86-7AD7-1036-7B44-AB0000000001 \qn \norestart";
            target.KillProcess = true;
            target.DelayBeforeKilling = 5;
            target.StoreToVariable = true;

            string expected = "<Action>\r\n<ElementType>CustomActions.ExecutableAction</ElementType>\r\n<PathToTheFile>" + target.PathToTheFile +
                 "</PathToTheFile>\r\n<Parameters>" + target.Parameters +
                 "</Parameters>\r\n<KillProcess>" + "true" +
                 "</KillProcess>\r\n<DelayBeforeKilling>" + target.DelayBeforeKilling.ToString() + "</DelayBeforeKilling>\r\n<StoreToVariable>true</StoreToVariable>\r\n</Action>";
            string actual;

            actual = target.GetXMLAction();
            Assert.AreEqual(expected, actual, true, "GetXmlAction doesn't returns the good string.");
        }

        /// <summary>
        /// Test if when the Hive refers to HKCU, the property RefersToHKeyCurrentUser is properly updated
        /// </summary>
        [TestMethod()]
        public void UserProfileNotificationTest()
        {
            ExecutableAction target = new ExecutableAction();

            Assert.IsFalse(target.RefersToUserProfile, "The property 'RefersToUserProfile' is not properly initialized");

            foreach (string folder in CommonData.userProfileRelatedFolders)
            {
                target.PathToTheFile = folder;
                Assert.IsTrue(target.RefersToUserProfile, folder + " should set 'RefersToUserProfile' to true.");
            }

            target.PathToTheFile = @"C:\Temp";
            Assert.IsFalse(target.RefersToUserProfile);

            foreach (string folder in CommonData.otherFolders)
            {
                target.PathToTheFile = folder;
                Assert.IsFalse(target.RefersToUserProfile, folder + " should not set 'RefersToUserProfile' to true.");
            }

        }
    }
}
