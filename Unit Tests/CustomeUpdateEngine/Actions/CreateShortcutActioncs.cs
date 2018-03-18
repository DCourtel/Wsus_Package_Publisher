using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine.CreateShortcutAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class CreateShortcutActioncs
    {
        [TestClass]
        public class Constructor_Should
        {
            [TestMethod]
            public void ProperlyInitializeProperty_WhenCalled()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateShortcutToAllUsers.CustAct"));

                // Act

                // Assert
                Assert.AreEqual(action.Target, @"C:\Windows\Notepad.exe");
                Assert.AreEqual(action.ShortcutName, "Raccourcis vers Notepad");
                Assert.AreEqual(action.Description, "Une description");
                Assert.AreEqual(action.Icon, String.Empty);
                Assert.AreEqual(action.Arguments, String.Empty);
                Assert.AreEqual(action.WorkingDirectory, String.Empty);
                Assert.AreEqual(action.WindowStyle, 0);
                Assert.AreEqual(action.DesktopTarget, 0);
                Assert.IsTrue(action.IsDesktopLocation);
                Assert.IsFalse(action.IsPersoLocation);
                Assert.AreEqual(action.PersoLocation, String.Empty);
                Assert.IsTrue(action.AbortIfTargetDontExist);                
            }
        }

        [TestClass]
        public class Run_Should
        {
            [TestMethod]
            public void CreateTheShortcutToAllUserDesktop_WhenCalledWithAllUsersDesktop()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateShortcutToAllUsers.CustAct"));
                string allUsersDesktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
                FileInfo shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if(shortcut.Exists  )
                {
                    shortcut.Delete();
                    shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));
                    Assert.IsFalse(shortcut.Exists, "The shortcut already exists");
                }
                action.Run(ref finalResult);
                shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));

                // Assert
                Assert.IsTrue(shortcut.Exists);
            }

            [TestMethod]
            public void CreateTheShortcutToCurrentUserDesktop_WhenCalledWithCurrentUsersDesktop()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateShortcutToCurrentUser.CustAct"));
                string currentUserDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                FileInfo shortcut = new FileInfo(Path.Combine(currentUserDesktop, action.ShortcutName + ".lnk"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (shortcut.Exists)
                {
                    shortcut.Delete();
                    shortcut = new FileInfo(Path.Combine(currentUserDesktop, action.ShortcutName + ".lnk"));
                    Assert.IsFalse(shortcut.Exists, "The shortcut already exists");
                }
                action.Run(ref finalResult);
                shortcut = new FileInfo(Path.Combine(currentUserDesktop, action.ShortcutName + ".lnk"));

                // Assert
                Assert.IsTrue(shortcut.Exists);
            }

            [TestMethod]
            public void NotCreateTheShortcutToCurrentUserDesktop_WhenCalledWithInvalidTarget()
            {
                // Arrange
                SUT action = new SUT(Tools.GetXmlFragment("CreateShortcutToAllUsersWithInvalidTarget.CustAct"));
                string allUsersDesktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
                FileInfo shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));
                var finalResult = Tools.GetReturnCodeAction();

                // Act
                if (shortcut.Exists)
                {
                    shortcut.Delete();
                    shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));
                    Assert.IsFalse(shortcut.Exists, "The shortcut already exists");
                }
                action.Run(ref finalResult);
                shortcut = new FileInfo(Path.Combine(allUsersDesktop, action.ShortcutName + ".lnk"));

                // Assert
                Assert.IsFalse(shortcut.Exists);
            }
        }
    }
}
