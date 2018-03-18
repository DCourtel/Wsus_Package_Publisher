using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = WPP.Security.Crypto;

namespace Unit_Tests_Wsus_AD_Comparator
{
    [TestClass]
    public class Crypto_Should
    {
        [TestMethod]
        public void ReturnTheSameString_WhenCallingGetSecureAndGetUnsecure()
        {
            // Arrange
            string expected = "MyVoiceIsMyPassport!";
            System.Security.SecureString encryptedString;
            string actual = String.Empty;

            // Act
            encryptedString = SUT.GetSecureString(expected);
            actual = SUT.GetUnsecureString(encryptedString);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReturnAReadOnlySecureString_WhenCallingGetSecureString()
        {
            // Arrange
            System.Security.SecureString encryptedString = new System.Security.SecureString();
            Assert.IsFalse(encryptedString.IsReadOnly());
            string password = "Pa55w0rd";

            // Act
            encryptedString = SUT.GetSecureString(password);

            // Assert
            Assert.IsTrue(encryptedString.IsReadOnly());
        }
    }
}
