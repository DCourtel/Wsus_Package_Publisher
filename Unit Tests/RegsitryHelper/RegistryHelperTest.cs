using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RegsitryHelperTest
{
    /// <summary>
    ///Classe de test pour RegistryHelperTest, destinée à contenir tous
    ///les tests unitaires RegistryHelperTest
    ///</summary>
    [TestClass()]
    public class RegistryHelperTest
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
        ///Test pour RemoveLeadingBackSlash
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void RemoveLeadingBackSlashTest()
        {
            string dirtyString = @"\\\Software\JavaSoft";
            string expected = @"Software\JavaSoft";
            string actual;
            actual = RegistryHelper.RemoveLeadingBackSlash(dirtyString);
            Assert.AreEqual(expected, actual, true);

            dirtyString = @"Software\JavaSoft";
            expected = @"Software\JavaSoft";

            actual = RegistryHelper.RemoveLeadingBackSlash(dirtyString);
            Assert.AreEqual(expected, actual, true);

            dirtyString = @"\Software\JavaSoft";
            expected = @"Software\JavaSoft";

            actual = RegistryHelper.RemoveLeadingBackSlash(dirtyString);
            Assert.AreEqual(expected, actual, true);
        }

        /// <summary>
        ///Test pour GetCleanRegKey
        ///</summary>
        [TestMethod()]
        public void GetCleanRegKeyTest()
        {
            RegistryHelper.RegKey actual;
            actual = RegistryHelper.GetCleanRegKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Current_User, false);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);

            actual = RegistryHelper.GetCleanRegKey(@"HKEY_Current_User\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Local_Machine, false);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Current_User, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);

            actual = RegistryHelper.GetCleanRegKey(@"\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Local_Machine, false);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);

            actual = RegistryHelper.GetCleanRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Local_Machine, false);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);

            actual = RegistryHelper.GetCleanRegKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Local_Machine, true);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);

            actual = RegistryHelper.GetCleanRegKey(@"Software\wow6432node\Microsoft\Windows\CurrentVersion\Policies", RegistryHelper.RegistryHive.HKey_Local_Machine, true);
            Assert.AreEqual(RegistryHelper.RegistryHive.HKey_Local_Machine, actual.RegHive);
            Assert.AreEqual(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies", actual.RegKeyName, true);
        }

        /// <summary>
        ///Test pour RemoveRegistryHiveReference
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void RemoveRegistryHiveReferenceTest()
        {
            string dirtyRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Policies";
            RegistryHelper.RegistryHive currentHive = RegistryHelper.RegistryHive.Undefined;
            RegistryHelper.RegKey expected = new RegistryHelper.RegKey();
            expected.RegHive = RegistryHelper.RegistryHive.HKey_Current_User;
            expected.RegKeyName = @"\Software\Microsoft\Windows\CurrentVersion\Policies";
            RegistryHelper.RegKey actual;
            actual = RegistryHelper.RemoveRegistryHiveReference(dirtyRegKey, currentHive);
            Assert.AreEqual(expected.RegKeyName, actual.RegKeyName, true);
            Assert.AreEqual(expected.RegHive, actual.RegHive);

            dirtyRegKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            currentHive = RegistryHelper.RegistryHive.Undefined;
            expected.RegHive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            expected.RegKeyName = @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            actual = RegistryHelper.RemoveRegistryHiveReference(dirtyRegKey, currentHive);
            Assert.AreEqual(expected.RegKeyName, actual.RegKeyName, true);
            Assert.AreEqual(expected.RegHive, actual.RegHive);

            dirtyRegKey = @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            currentHive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            expected.RegHive = RegistryHelper.RegistryHive.HKey_Local_Machine;
            expected.RegKeyName = @"\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            actual = RegistryHelper.RemoveRegistryHiveReference(dirtyRegKey, currentHive);
            Assert.AreEqual(expected.RegKeyName, actual.RegKeyName, true);
            Assert.AreEqual(expected.RegHive, actual.RegHive);

            dirtyRegKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            currentHive = RegistryHelper.RegistryHive.HKey_Current_User;
            expected.RegHive = RegistryHelper.RegistryHive.HKey_Current_User;
            expected.RegKeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies";
            actual = RegistryHelper.RemoveRegistryHiveReference(dirtyRegKey, currentHive);
            Assert.AreEqual(expected.RegKeyName, actual.RegKeyName, true);
            Assert.AreEqual(expected.RegHive, actual.RegHive);
        }

        [TestMethod]
        [DeploymentItem("CustomActions.dll")]
        public void RemoveWowReferenceTest_WhenReferenceExists()
        {
            // Arrange
            string withReference = @"SOFTWARE\Wow6432Node\EasyCompany\Wsus Package Publisher\Test";
            string expected = @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test";

            // Act
            string actual = RegistryHelper.RemoveWowReference(withReference);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [DeploymentItem("CustomActions.dll")]
        public void RemoveWowReferenceTest_WhenReferenceDontExists()
        {
            // Arrange
            string withReference = @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test";
            string expected = @"SOFTWARE\EasyCompany\Wsus Package Publisher\Test";

            // Act
            string actual = RegistryHelper.RemoveWowReference(withReference);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
