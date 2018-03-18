using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = WPP.Tools.CommandLine;

namespace Unit_Tests_Wsus_AD_Comparator
{
    [TestClass]
    public class CommandLineTest_Should
    {
        private string[] _argValid = new string[] { "-Option=123", "-Server=Wsus2012R2", "-SSL=True" };
        private string[] _argValidWithMixedDefaultPrefix = new string[] { "-Option=123", "-Server=Wsus2012R2", "/SSL=True" };
        private string[] _argWithAsterikAsPrefix = new string[] { "*Option=123", "*Server=Wsus2012R2", "*SSL=True" };
        private string[] _argWithoutPrefix = new string[] { "Option=123", "Server=Wsus2012R2", "SSL=True" };
        private string[] _argWithoutValue = new string[] { "-Option=" };
        private string[] _argWithoutEqualSign = new string[] { "-Option", "-Server=Wsus2012R2", "-SSL=True" };
        private string[] _argWithoutName = new string[] { "=123", "*Server=Wsus2012R2", "*SSL=True" };
        private string[] _argWithEmptyOptions = new string[] { };

        [TestMethod]
        public void HaveOptionCountEqualToZero_WhenCallWithAnEmptyStringArray()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithEmptyOptions);

            // Assert

            Assert.IsTrue(commandLine.OptionCount == 0);
        }

        [TestMethod]
        public void HaveOptionCountEqualToThree_WhenCallingWithThreeArgumentsArray()
        {
            // Arrange
            SUT commandLine = new SUT(_argValid);
            
            // Assert

            Assert.IsTrue(commandLine.OptionCount == 3);
        }

        [TestMethod]
        public void HaveOptionCountEqualToThree_WhenCallingWithThreeMixedDefaultPrefixArgumentsArray()
        {
            // Arrange
            SUT commandLine = new SUT(_argValidWithMixedDefaultPrefix);

            // Assert

            Assert.IsTrue(commandLine.OptionCount == 3);
        }

        [TestMethod]
        public void HaveOptionCountEqualToThree_WhenCallingWithThreeAlternativePrefixArgumentsArray()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithAsterikAsPrefix);
            commandLine.Prefix += "*";  
          
            // Assert

            Assert.IsTrue(commandLine.OptionCount == 3);
        }

        [TestMethod]
        public void HaveOptionCountEqualToThree_WhenCallingWithThreeUnprefixArgumentsArray()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithoutPrefix);

            // Assert

            Assert.IsTrue(commandLine.OptionCount == 3);
        }

        [TestMethod]
        public void ReturnTheDefaultValue_WhenCallingWithAnArgumentWithoutValue()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithoutValue);

            // Assert

            Assert.AreEqual<int>(15, commandLine.GetOptionValue<int>("Option", 15));
        }

        [TestMethod]
        public void FindLoweredOption_WhenParsingCamelCasedOption()
        {
            // Arrange
            SUT commandLine = new SUT(_argValid);

            // Assert

            Assert.AreEqual<int>(123, commandLine.GetOptionValue<int>("option", 15));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowAnException_WhenParsingArgumentWithoutEqualSign()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithoutEqualSign);

            // Assert

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowAnException_WhenParsingArgumentWithoutName()
        {
            // Arrange
            SUT commandLine = new SUT(_argWithoutName);

            // Assert

            Assert.Fail();
        }

        [TestMethod]
        public void ReturnDefaultValue_WhenTryingToGetOptionThatNotExists()
        {
            // Arrange
            SUT commandLine = new SUT(_argValid);

            // Assert

            Assert.AreEqual<int>(15, commandLine.GetOptionValue<int>("NotExists", 15));
        }

        [TestMethod]
        public void ReturnTrue_WhenCallingOptionExistsForAnOptionThatExists()
        {
            // Arrange
            SUT commandLine = new SUT(_argValid);

            // Assert

            Assert.IsTrue(commandLine.OptionExists("Server"));
            Assert.IsTrue(commandLine.OptionExists("ssl"));
            Assert.IsTrue(commandLine.OptionExists("opTION"));
        }

    }
}
