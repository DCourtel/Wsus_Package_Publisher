using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SUT = CustomUpdateEngine;

namespace Unit_Tests_CustomUpdateEngine
{
    public class Logger
    {
        [TestClass]
        public class Initialize_Should
        {
            [TestMethod]
            public void CorrectlyInitializeTheClass_InitializedToConsole()
            {
                // Arrange
                string expectedLogPath = @"C:\";
                string expectedLogFilename = "log.txt";
                System.Diagnostics.EventLogEntryType expectedEventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.Console;

                // Act
                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogDestination);

                // Assert
                Assert.AreEqual(expectedLogDestination, SUT.Logger.LogDestination);
                Assert.AreEqual(expectedEventLogEntryType, SUT.Logger.LogEntryType);
                Assert.AreEqual(expectedLogFilename, SUT.Logger.LogFilename, true);
                Assert.AreEqual(expectedLogPath, SUT.Logger.LogPath, true);
            }

            [TestMethod]
            public void CorrectlyInitializeTheClass_InitializedToFile()
            {
                // Arrange
                string expectedLogPath = @"C:\Users\Courtel\AppData\Local\Temp\WPP";
                string expectedLogFilename = "Wpp.log";
                System.Diagnostics.EventLogEntryType expectedEventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.File;

                // Act
                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogPath, expectedLogFilename, expectedLogDestination);

                // Assert
                Assert.AreEqual(expectedLogDestination, SUT.Logger.LogDestination);
                Assert.AreEqual(expectedEventLogEntryType, SUT.Logger.LogEntryType);
                Assert.AreEqual(expectedLogFilename, SUT.Logger.LogFilename, true);
                Assert.AreEqual(expectedLogPath, SUT.Logger.LogPath, true);
            }

            [TestMethod]
            public void CorrectlyInitializeTheClass_InitializedToWindowsEventLog()
            {
                // Arrange
                string expectedLogPath = @"C:\Users\Courtel\AppData\Local\Temp\WPP";
                string expectedLogFilename = "Wpp.log";
                System.Diagnostics.EventLogEntryType expectedEventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.WindowsEventLog;

                // Act
                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogPath, expectedLogFilename, expectedLogDestination);

                // Assert
                Assert.AreEqual(expectedLogDestination, SUT.Logger.LogDestination);
                Assert.AreEqual(expectedEventLogEntryType, SUT.Logger.LogEntryType);
                Assert.AreEqual(expectedLogFilename, SUT.Logger.LogFilename, true);
                Assert.AreEqual(expectedLogPath, SUT.Logger.LogPath, true);
            }
        }

        [TestClass]
        public class Reset_Should
        {
            [TestMethod]
            public void SetPropertiesToDefaultValues_WhenCalled()
            {
                // Arrange
                string expectedLogPath = @"C:\";
                string expectedLogFilename = "log.txt";
                System.Diagnostics.EventLogEntryType expectedEventLogEntryType = System.Diagnostics.EventLogEntryType.Error;
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.Console;

                // Act
                SUT.Logger.Initialize(@"C:\Users\Courtel\AppData\Local\Temp\WPP", "Wpp.log", SUT.Logger.Destination.File);
                SUT.Logger.Reset();

                // Assert
                Assert.AreEqual(expectedLogDestination, SUT.Logger.LogDestination);
                Assert.AreEqual(expectedEventLogEntryType, SUT.Logger.LogEntryType);
                Assert.AreEqual(expectedLogFilename, SUT.Logger.LogFilename, true);
                Assert.AreEqual(expectedLogPath, SUT.Logger.LogPath, true);
            }

        }

        [TestClass]
        public class Write_Should
        {
            [TestMethod]
            public void CreateTheLogFile_WhenSetToLogToFile()
            {
                // Arrange
                string expectedLogPath = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Test-Logger";
                string expectedLogFilename = "Wpp.log";
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.File;
                if (File.Exists(Path.Combine(expectedLogPath, expectedLogFilename)))
                {
                    File.Delete(Path.Combine(expectedLogPath, expectedLogFilename));
                }
                Assert.IsFalse(File.Exists(Path.Combine(expectedLogPath, expectedLogFilename)), "Le fichier existe déjà");

                // Act
                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogPath, expectedLogFilename, expectedLogDestination);
                SUT.Logger.Write("Message de test.");

                // Assert
                Assert.IsTrue(File.Exists(Path.Combine(expectedLogPath, expectedLogFilename)), "Le fichier n'existe pas.");
            }

            [TestMethod]
            public void WriteMessagesToTheFile_WhenCalled()
            {
                // Arrange
                string expectedLogPath = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Test-Logger";
                string expectedLogFilename = "Wpp.log";
                string expectedLogContain = "Message de test.";
                string actualContain = String.Empty;
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.File;
                string fullPath = Path.Combine(expectedLogPath, expectedLogFilename);

                // Act
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                Assert.IsFalse(File.Exists(fullPath), "Le fichier existe déjà");

                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogPath, expectedLogFilename, expectedLogDestination);
                SUT.Logger.Write(expectedLogContain);
                StreamReader reader = new StreamReader(fullPath);
                actualContain = reader.ReadToEnd();
                reader.Close();

                // Assert
                Assert.IsTrue(File.Exists(fullPath), "Le fichier n'existe pas.");
                Assert.IsTrue(actualContain.Contains(expectedLogContain));
            }
            
            [TestMethod]
            public void TruncateTheFile_WhenFileSizeIsGreaterThanMaximumFileSize()
            {
                // Arrange
                string expectedLogPath = @"C:\Users\Courtel\Documents\Visual Studio 2013\Projects\Wsus Package Publisher2\Unit Tests-CustomeUpdateEngine\Test-Logger";
                string expectedLogFilename = "Wpp.log";
                string expectedLogContain = "Message de test.";
                SUT.Logger.Destination expectedLogDestination = SUT.Logger.Destination.File;
                string fullPath = Path.Combine(expectedLogPath, expectedLogFilename);

                // Act
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                Assert.IsFalse(File.Exists(fullPath), "Le fichier existe déjà");

                SUT.Logger.Reset();
                SUT.Logger.Initialize(expectedLogPath, expectedLogFilename, expectedLogDestination);
                CreateBigLogFile(fullPath);
                Assert.IsTrue((new FileInfo(fullPath)).Length > SUT.Logger.MaximumFileSize, "Le fichier n'est pas assez gros.");
                SUT.Logger.Write(expectedLogContain);

                // Assert
                Assert.IsTrue((new FileInfo(fullPath)).Length < SUT.Logger.MaximumFileSize, "Le fichier n'a pas été tronqué.");                
            }

            private void CreateBigLogFile(string fullPath)
            {
                StreamWriter writter = new StreamWriter(fullPath,false, System.Text.Encoding.UTF8);                
                string str = new string('e', 1024 * 1024);
                for (int i = 0; i < 12; i++)
                {
                    writter.WriteLine(str); 
                }
                writter.Close();
            }
        }
    }
}
