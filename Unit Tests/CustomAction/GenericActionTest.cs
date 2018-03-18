using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour CustomActionTest, destinée à contenir tous
    ///les tests unitaires CustomActionTest
    ///</summary>
    [TestClass()]
    public class GenericActionTest
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

        #region GenericAction

        /// <summary>
        ///Does 'BackColor' property, always returns 'Transparent' ?
        ///</summary>
        [TestMethod()]
        [Description("Does 'BackColor' property, always returns 'Transparent' ?")]
        public void BackColorTest()
        {
            GenericAction target = new GenericAction();
            Color expected = Color.Transparent;
            Color actual;
            target.BackColor = Color.SpringGreen;
            actual = target.BackColor;
            Assert.AreEqual(expected, actual, "After assigning a color to the 'BackColor' property, this property should always return 'Transparent'.");
        }

        /// <summary>
        ///Test pour BackgroundUnselectedColor
        ///</summary>
        [TestMethod()]
        [Description("Does the property 'BackgroundUnselectedColor' set properly ?")]
        public void BackgroundUnselectedColorTest()
        {
            GenericAction target = new GenericAction();
            Color expected = Color.MistyRose;
            Color actual;
            target.BackgroundUnselectedColor = expected;
            actual = target.BackgroundUnselectedColor;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does 'BackgroundSelectedColor' property set properly ?
        ///</summary>
        [TestMethod()]
        [Description("Does 'BackgroundSelectedColor' property set properly ?")]
        public void BackgroundSelectedColor()
        {
            GenericAction target = new GenericAction();
            Color expected = Color.Tomato;
            Color actual;
            target.BackgroundSelectedColor = expected;
            actual = target.BackgroundSelectedColor;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the 'ForeColor' property set properly ?
        ///</summary>
        [TestMethod()]
        [Description("Does the 'ForeColor' property set properly ?")]
        public void ForeColorTest()
        {
            GenericAction target = new GenericAction();
            Color expected = Color.SandyBrown;
            Color actual;
            target.ForeColor = expected;
            actual = target.ForeColor;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the 'SurroundColor' property set properly ?
        ///</summary>
        [TestMethod()]
        [Description("Does the 'SurroundColor' property set properly ?")]
        public void SurroundColorTest()
        {
            GenericAction target = new GenericAction();
            Color expected = Color.RosyBrown;
            Color actual;
            target.SurroundColor = expected;
            actual = target.SurroundColor;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the property 'Text' set properly ?
        ///Does the property 'Text' returns the default text when initialized with an empty string ?
        ///Does the property 'Text' returns the default text when initialized with a null object ?
        ///Does the property 'Text' set properly when containing 'Carriage Returns' ?
        ///Does the property 'Text' troncate the parameter to 255 characters when value exceed 255 ?
        ///Does the property 'Text' set properly when initialized with a 255 length string ?
        ///</summary>
        [TestMethod()]
        [Description("Does the property 'Text' set properly ? Does the property 'Text' returns the default text when initialized with an empty string ? Does the property 'Text' returns the default text when initialized with a null object ? Does the property 'Text' set properly when containing 'Carriage Returns' ? Does the property 'Text' troncate the parameter to 255 characters when value exceed 255 ? Does the property 'Text' set properly when initialized with a 255 length string ?")]
        public void TextTest()
        {
            GenericAction target = new GenericAction();
            string expected = "Testing";
            string actual;
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property is not set properly.");

            expected = "There is no description for this CustomAction.";
            target.Text = string.Empty;
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property doesn't returns the default value when initialized with string.Empty.");

            expected = "There is no description for this CustomAction.";
            target.Text = null;
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property doesn't returns the default value when initialized with null.");

            expected = "Title\r\n- First Item\r\n- Second Item\r\n";
            target.Text = expected;
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains 'carriage return'.");

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(1000000);
            strBuilder.Append('A', 1000000);
            target.Text = strBuilder.ToString();
            expected = new string('A', 255);
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains a lot of characters.");
            Assert.IsTrue(actual.Length == 255, "The Text is not troncat to 255 characters.");

            expected = new string('A', 255);
            actual = target.Text;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains exactly 255 characters.");
            Assert.IsTrue(actual.Length == 255, "The Text is not troncat to 255 characters.");
        }

        /// <summary>
        ///Does the 'ActionIcon" property set properly ?
        ///Does the 'ActionIcon" property set properly when initialized with a nul object ?
        ///</summary>
        [TestMethod()]
        [Description("Does the 'ActionIcon' property set properly ? Does the 'ActionIcon' property set properly when initialized with a nul object ?")]
        public void ActionIconTest()
        {
            GenericAction target = new GenericAction();
            Image expected = Properties.Resources.RegEdit;
            Image actual;
            target.ActionIcon = expected;
            actual = target.ActionIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly.");

            target.ActionIcon = null;
            actual = target.ActionIcon;
            Assert.IsTrue(actual != null, "The property is not set properly when initialized with a null object.");
        }

        /// <summary>
        ///Does the Control respect minimal size ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the Control respect minimal size ?")]
        public void OnSizeChangedTest()
        {
            GenericAction target = new GenericAction();

            Size expected = new Size(120, 55);
            Size actual;
            target.Size = new Size(120, 10);
            actual = target.Size;
            Assert.AreEqual(expected, actual, "The Control doesn't respect the minimal Heigth.");

            expected = new Size(90, 120);
            target.Size = new Size(12, 120);
            actual = target.Size;
            Assert.AreEqual(expected, actual, "The Control doesn't respect the minimal Width.");
        }

        /// <summary>
        ///Does the property 'HelpMessage' set properly ?
        ///Does the property 'HelpMessage' still unchange when initialized with an empty string ?
        ///Does the property 'HelpMessage' still unchange when initialized with a null object ?
        ///Does the property 'HelpMessage' set properly when containing 'Carriage Returns' ?
        ///Does the property 'HelpMessage' troncate the parameter to 512 characters when value exceed 512 ?
        ///Does the property 'HelpMessage' set properly when initialized with a 512 length string ?
        ///</summary>
        [TestMethod()]
        [Description("Does the property 'HelpMessage' set properly ? Does the property 'HelpMessage' still unchange when initialized with an empty string ? Does the property 'HelpMessage' still unchange when initialized with a null object ? Does the property 'HelpMessage' set properly when containing 'Carriage Returns' ? Does the property 'HelpMessage' troncate the parameter to 512 characters when value exceed 512 ? Does the property 'HelpMessage' set properly when initialized with a 512 length string ?")]
        public void HelpMessageTest()
        {
            GenericAction target = new GenericAction();
            string expected = "Testing";
            string actual;
            target.HelpMessage = expected;
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is not set properly.");

            expected = "There is no help message for this CustomAction.";
            target.HelpMessage = expected;
            target.HelpMessage = string.Empty;
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is change when initialized with string.Empty.");

            expected = "There is no help message for this CustomAction.";
            target.HelpMessage = expected;
            target.HelpMessage = null;
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is change when initialized with null.");

            expected = "Title\r\n- First Item\r\n- Second Item\r\n";
            target.HelpMessage = expected;
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains 'carriage return'.");

            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(1000000);
            strBuilder.Append('A', 1000000);
            target.HelpMessage = strBuilder.ToString();
            expected = new string('A', 512);
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains a lot of characters.");
            Assert.IsTrue(actual.Length == 512, "The Text is not troncat to 512 characters.");

            expected = new string('A', 512);
            actual = target.HelpMessage;
            Assert.AreEqual(expected, actual, "The property is not set properly when contains exactly 512 characters.");
            Assert.IsTrue(actual.Length == 512, "The Text is not troncat to 512 characters.");
        }

        /// <summary>
        ///Does the property 'ConfigurationState' set properly ?
        ///</summary>
        [TestMethod()]
        [Description("Does the property 'ConfigurationState' set properly ?")]
        public void ConfigurationStateTest()
        {
            GenericAction target = new GenericAction();
            Assert.AreEqual(target.ConfigurationState, GenericAction.ConfigurationStates.NotConfigured, "The property is not initialized properly.");

            GenericAction.ConfigurationStates expected = GenericAction.ConfigurationStates.Misconfigured;
            target.ConfigurationState = expected;
            GenericAction.ConfigurationStates actual = target.ConfigurationState;
            Assert.AreEqual(expected, actual, "The property is not set properly.");

            expected = GenericAction.ConfigurationStates.Configured;
            target.ConfigurationState = expected;
            actual = target.ConfigurationState;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Test pour Constructeur GenericAction
        ///</summary>
        [TestMethod()]
        [Description("Does public properties properly initialized ?")]
        public void GenericActionConstructorTest()
        {
            GenericAction target = new GenericAction();

            Assert.AreEqual(target.BackgroundUnselectedColor, Color.Cornsilk);
            Assert.AreEqual(target.BackgroundSelectedColor, Color.PowderBlue);
            Assert.AreEqual(target.ForeColor, Color.Black);
            Assert.AreEqual(target.SurroundColor, Color.SteelBlue);
            Assert.AreEqual(target.Text, "There is no description for this CustomAction.");
            Assert.AreEqual(target.HelpMessage, "Aucune aide disponible pour cette Action.");
            Assert.IsTrue(target.IsTemplate);
            Assert.IsNotNull(target.ActionIcon, "ActionIcon should'nt be null when initialized the class.");            
        }

        /// <summary>
        ///Does the property 'IsOnHelpIcon' set properly when the class is instanciate ?
        ///Does the property 'IsOnHelpIcon' set properly ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'IsOnHelpIcon' set properly when the class is instanciate ? Does the property 'IsOnHelpIcon' set properly ?")]
        public void IsOnHelpIconTest()
        {
            GenericAction target = new GenericAction();
            bool expected = false;
            bool actual;
            actual = target.IsOnHelpIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly when the class is instanciate.");

            expected = true;
            target.IsOnHelpIcon = expected;
            actual = target.IsOnHelpIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the property 'IsOnConfigurationStateIcon' set properly when the class is instanciate ?
        ///Does the property 'IsOnConfigurationStateIcon' set properly ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'IsOnConfigurationStateIcon' set properly when the class is instanciate ? Does the property 'IsOnConfigurationStateIcon' set properly ?")]
        public void IsOnConfigurationStateIconTest()
        {
            GenericAction target = new GenericAction();
            bool expected = false;
            bool actual;
            actual = target.IsOnConfigurationStateIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly when the class is instanciate.");

            expected = true;
            target.IsOnConfigurationStateIcon = expected;
            actual = target.IsOnConfigurationStateIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the property 'IsOnUpDownArrowIcon' set properly when the class is instanciate ?
        ///Does the property 'IsOnUpDownArrowIcon' set properly ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'IsOnUpDownArrowIcon' set properly when the class is instanciate ? Does the property 'IsOnUpDownArrowIcon' set properly ?")]
        public void IsOnUpDownArrowIconTest()
        {
            GenericAction target = new GenericAction();
            bool expected = false;
            bool actual;
            actual = target.IsOnUpDownArrowIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly when the class is instanciate.");

            expected = true;
            target.IsOnUpDownArrowIcon = expected;
            actual = target.IsOnUpDownArrowIcon;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }
        
        /// <summary>
        ///Does the property 'IsMouseDown' set properly when the class is instanciate ?
        ///Does the property 'IsMouseDown' set properly ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'IsMouseDown' set properly when the class is instanciate ? Does the property 'IsMouseDown' set properly ?")]
        public void IsMouseDownTest()
        {
            GenericAction target = new GenericAction();
            bool expected = false;
            bool actual;
            actual = target.IsMouseDown;
            Assert.AreEqual(expected, actual, "The property is not set properly when the class is instanciate.");

            expected = true;
            target.IsMouseDown = expected;
            actual = target.IsMouseDown;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Does the property 'DragDropHasStarted' set properly when the class is instanciate ?
        ///Does the property 'DragDropHasStarted' set properly ?
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'DragDropHasStarted' set properly when the class is instanciate ?Does the property 'DragDropHasStarted' set properly ?")]
        public void DragDropHasStartedTest()
        {
            GenericAction target = new GenericAction();
            bool expected = false;
            bool actual;
            actual = target.DragDropHasStarted;
            Assert.AreEqual(expected, actual, "The property is not set properly when the class is instanciate.");

            expected = true;
            target.DragDropHasStarted = expected;
            actual = target.DragDropHasStarted;
            Assert.AreEqual(expected, actual, "The property is not set properly.");
        }

        /// <summary>
        ///Test pour GetLocalizedString
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the method returns the correct value ? Does the method return the default message when call with string.Empty ? Does the method returns the default error message when call with 'null' ?")]
        public void GetLocalizedStringTest()
        {
            GenericAction target = new GenericAction();            
            string unlocalizedString = "ThisActionIs";
            string expected = "Cette Action est";
            string actual;
            actual = target.GetLocalizedString(unlocalizedString);
            Assert.AreEqual(expected, actual, "The correct string is not returns.");
            
            unlocalizedString = "yzuiytcinaitbzityibtrey\r\naé_ç_é'éç(_àé";
            expected = "Missing_Localized_String_For(yzuiytcinaitbzityibtrey\r\naé_ç_é'éç(_àé)";
            actual = target.GetLocalizedString(unlocalizedString);
            Assert.AreEqual(expected, actual, "The method doesn't returns the default error message when call with rubbish.");

            unlocalizedString = string.Empty;
            expected = "Missing_Localized_String_For()";
            actual = target.GetLocalizedString(unlocalizedString);
            Assert.AreEqual(expected, actual, "The method doesn't returns the default error message when call with string.Empty.");

            unlocalizedString = null;
            expected = "Missing_Localized_String_For(null)";
            actual = target.GetLocalizedString(unlocalizedString);
            Assert.AreEqual(expected, actual, "The method doesn't returns the default error message when call with null.");
        }

        /// <summary>
        ///Test pour IsTemplate
        ///</summary>
        [TestMethod()]
        [Description("Does the 'IsTemplate' properly initialized ? Does the property 'IsTemplate' properly set ?")]
        public void IsTemplateTest()
        {
            GenericAction target = new GenericAction();
            Assert.IsTrue(target.IsTemplate);

            bool expected = false;
            bool actual;
            target.IsTemplate = expected;
            actual = target.IsTemplate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Test pour IsSelected
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        [Description("Does the property 'IsSelected' initialized properly ? Does the property 'IsSelected' properly set ?")]
        public void IsSelectedTest()
        {
            GenericAction target = new GenericAction();
            Assert.IsFalse(target.IsSelected, "The property is not iniatilized properly.");

            target.IsSelected = true;
            Assert.IsTrue(target.IsSelected, "The property is not set properly.");
        }

        #endregion GenericAction

        /// <summary>
        ///Test pour UpdateHiveNotificationStatus
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CustomActions.dll")]
        public void UpdateHiveNotificationStatusTest()
        {
            GenericAction target = new GenericAction();

            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            target.UpdateHiveNotificationStatus(RegistryHelper.RegistryHive.HKey_Current_User);
            Assert.IsTrue(target.RefersToHKeyCurrentUser);

            target.UpdateHiveNotificationStatus(RegistryHelper.RegistryHive.HKey_Local_Machine);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);

            target.UpdateHiveNotificationStatus(RegistryHelper.RegistryHive.Undefined);
            Assert.IsFalse(target.RefersToHKeyCurrentUser);
        }

        /// <summary>
        ///Test pour HasReferenceToUserProfile
        ///</summary>
        [TestMethod()]
        public void HasReferenceToUserProfileTest()
        {
            // %HomePath%
            // %LocalAppData%
            // %UserProfile%
            // %AppData%
            // %UserName%

            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"C:\Users\Courtel\Documents\file.txt"));
            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"C:\Documents and Settings\Courtel\Documents\file.txt"));
            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"%HomePath%\Documents\file.txt"));
            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"%LocalAppData%\file.txt"));
            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"%UserProfile%\file.txt"));
            Assert.IsTrue(GenericAction.HasReferenceToUserProfile(@"%AppData%\file.txt"));
            Assert.IsFalse(GenericAction.HasReferenceToUserProfile(@"C:\Program Files\Broadcom\BACS\Bacs.AppInfo"));
            Assert.IsFalse(GenericAction.HasReferenceToUserProfile(@"C:\Program Files (x86)\Adobe\Reader 11.0"));
            Assert.IsFalse(GenericAction.HasReferenceToUserProfile(@"%WinDir%\System32"));
        }

        /// <summary>
        ///Test pour RemoveIllegalCharacters
        ///</summary>
        [TestMethod()]
        public void ContainsIllegalCharactersTest()
        {
            string dirtyString = "FolderName";
            Assert.IsFalse(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder<Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder<<Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder>Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder>>Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder:Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder:Na:me";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder\"Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder\"Na\"me";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder/Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Fo/lder/Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder\\Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "\\Folder\\Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder|Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder|Name|";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder?Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "?Folder?Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder*Name";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Folder*Name*";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));

            dirtyString = "Fo\\l/der|N*ame";
            Assert.IsTrue(GenericAction.ContainsIllegalCharacters(dirtyString));
        }

        /// <summary>
        ///Test pour IsValidFileOrFolderName
        ///</summary>
        [TestMethod()]
        public void IsValidFileOrFolderNameTest()
        {
            // CON, PRN, AUX, NUL, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, and LPT9

            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("Documents and Settings"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("Users"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("System32"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("Config.txt"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("Settings.ini"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("Nullable"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("CONtains"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("PRNational"));
            Assert.IsTrue(GenericAction.IsValidFileOrFolderName("PRN Type"));

            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("Con"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("PrN"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("AuX"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("Nul"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COm1"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("Com2"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("CoM3"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("cOM4"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM5"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM6"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM7"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM8"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM9"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT1"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT2"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT3"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT4"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT5"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT6"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT7"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT8"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("LPT9"));

            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("Nul.xml.txt"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("Nul.txt"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("PRN.txt"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("CON.txt"));
            Assert.IsFalse(GenericAction.IsValidFileOrFolderName("COM1.txt"));
        }
    }
}
