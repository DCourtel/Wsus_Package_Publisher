using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomActions;

namespace Unit_Tests_CustomAction
{
    [TestClass]
    public class CreateShortcutActionTest
    {
        [TestMethod]
        public void CreateShortcutConstructorTest()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("fr");
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            DefaultValueForProperties();

            culture = new System.Globalization.CultureInfo("en");
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            DefaultValueForProperties();
        }

        private void DefaultValueForProperties()
        {
            CreateShortcutAction target = new CreateShortcutAction();

            Assert.AreEqual(55, target.Height, "The property 'Height' is not properly initialized");
            Assert.IsTrue(target.IsTemplate, "The property 'IsTemplate' is not properly initialized");
            Assert.IsFalse(target.IsSelected, "The property 'IsSelected' is not properly initialized");
            Assert.AreEqual(GenericAction.ConfigurationStates.NotConfigured, target.ConfigurationState);
            Assert.IsFalse(target.RefersToUserProfile);

            Assert.AreEqual(String.Empty, target.Target);
            Assert.AreEqual(String.Empty, target.ShortcutName);
            Assert.IsTrue(target.IsDesktopLocation);
            Assert.AreEqual(0, target.DesktopTarget);
            Assert.IsFalse(target.IsPersoLocation);
            Assert.AreEqual(String.Empty, target.PersoLocation);
            Assert.AreEqual(String.Empty, target.Description);
            Assert.AreEqual(String.Empty, target.Icon);
            Assert.AreEqual(String.Empty, target.Arguments);
            Assert.AreEqual(String.Empty, target.WorkingDirectory);
            Assert.AreEqual(0, target.WindowStyle);
            Assert.IsTrue(target.AbortIfTargetDontExist);
        }
        
        /// <summary>
        ///Test pour GetXMLAction
        ///</summary>
        [TestMethod()]
        public void GetXMLActionTest()
        {
            CreateShortcutAction target = new CreateShortcutAction();

            target.Target = @"C:\Windows\System32\test.exe";
            target.ShortcutName = "Raccourcis pour test.exe";
            target.IsDesktopLocation = true;
            target.DesktopTarget = 0;
            target.Description = "Description pour le test";
            target.Icon = "test";
            target.Arguments = "/SetParameters";
            target.WorkingDirectory = @"C:\Windows\temp";
            target.WindowStyle = 0;
            target.AbortIfTargetDontExist = true;

            string expected = "<Action>\r\n<ElementType>CustomActions.CreateShortcutAction</ElementType>\r\n" +
                "<Target>" + @"C:\Windows\System32\test.exe" + "</Target>\r\n" +
                "<ShortcutName>" + "Raccourcis pour test.exe" + "</ShortcutName>\r\n" +
                "<Description>" + "Description pour le test" + "</Description>\r\n" +
                "<Icon>" + "test" + "</Icon>\r\n" +
                "<Arguments>" + "/SetParameters" + "</Arguments>\r\n" +
                "<WorkingDirectory>" + @"C:\Windows\temp" + "</WorkingDirectory>\r\n" +
                "<WindowStyle>" + "0" + "</WindowStyle>\r\n" +
                "<DesktopTarget>" + "0" + "</DesktopTarget>\r\n" +
                "<IsDesktopLocation>" + "true" + "</IsDesktopLocation>\r\n" +
                "<IsPersoLocation>" + "false" + "</IsPersoLocation>\r\n" +
                "<PersoLocation>" + String.Empty + "</PersoLocation>\r\n" + 
                "<AbortIfTargetDontExist>" + "true" + "</AbortIfTargetDontExist>\r\n</Action>";
            string actual = target.GetXMLAction();

            Assert.AreEqual(expected, actual, true, "XML not properly generated");
            
            target.IsPersoLocation = true;
            actual = target.GetXMLAction();

            Assert.IsTrue(target.IsPersoLocation, "The property 'IsPersoLocation' is not properly updated");
            Assert.IsFalse(target.IsDesktopLocation, "The property 'IsDesktopLocation' is not properly updated");

            target.DesktopTarget = 1;
            actual = target.GetXMLAction();

            Assert.IsTrue(actual.Contains("<DesktopTarget>" + "1" + "</DesktopTarget>\r\n"), "The property 'DesktopTarget' is not properly updated");

            target.WindowStyle = 1;
            actual = target.GetXMLAction();

            Assert.IsTrue(actual.Contains("<WindowStyle>" + "1" + "</WindowStyle>\r\n"), "The property 'WindowStyle' is not properly updated");

            target.WindowStyle = 2;
            actual = target.GetXMLAction();

            Assert.IsTrue(actual.Contains("<WindowStyle>" + "2" + "</WindowStyle>\r\n"), "The property 'WindowStyle' is not properly updated");
        }

        /// <summary>
        ///Test pour ValidateData
        ///</summary>
        [TestMethod()]
        public void ValidateDataTest()
        {
            CreateShortcutAction target = new CreateShortcutAction();

            Assert.AreEqual(GenericAction.ConfigurationStates.NotConfigured, target.ConfigurationState);

            target.Target = @"C:\test";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.ShortcutName = "test";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);

            target.Target = @"C:\test\";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.Target = @"C:\test";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);

            target.IsPersoLocation = true;
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.PersoLocation = @"C:\test\test";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);

            target.PersoLocation = @"C:\test\test\";
            Assert.AreEqual(GenericAction.ConfigurationStates.Misconfigured, target.ConfigurationState);
            target.PersoLocation = @"C:\test\test";
            Assert.AreEqual(GenericAction.ConfigurationStates.Configured, target.ConfigurationState);
        }
    }
}
