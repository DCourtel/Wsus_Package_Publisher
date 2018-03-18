using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace CustomUpdateEngine
{
    /// <summary>
    /// Fourni des méthodes pour écrire sur la Console, dans un fichier ou dans le journal d'événement de Windows.
    /// </summary>
    public static class Logger
    {
        private static string _logPath = @"C:\";
        private static string _logFilename = "log.txt";
        private static long _maxFileSize = 10 * 1024 * 1024; // 10 Mo;
        private static System.Diagnostics.EventLogEntryType _logEventLogType = System.Diagnostics.EventLogEntryType.Error;
        private static Destination _logDestination = Destination.Console;

        /// <summary>
        /// Énumération des sorties possibles.
        /// </summary>
        public enum Destination
        {
            Console = 1,
            File = 2,
            WindowsEventLog = 4
        }

        #region Properties

        /// <summary>
        /// Gets the path of folder where to write the log. To set this property, use the <see cref="Initialize"/> method.
        /// </summary>
        public static string LogPath { get { return _logPath; } }

        /// <summary>
        /// Gets the name of the file where to write log messages. To set this property, use the <see cref="Initialize"/> method.
        /// </summary>
        public static string LogFilename { get { return _logFilename; } }

        /// <summary>
        /// Gets the type of EventLog entries. To set this property, use the <see cref="Initialize"/> method.
        /// </summary>
        public static System.Diagnostics.EventLogEntryType LogEntryType { get { return _logEventLogType; } }

        /// <summary>
        /// Gets where to write the event log entries. To set this property, use the <see cref="Initialize"/> method.
        /// </summary>
        public static Destination LogDestination { get { return _logDestination; } }

        /// <summary>
        /// Obtient ou défini la taille maximal, en octets, du fichier journal. Le paramètre ne peut pas être inférieur à 1048576 (1 Mo).
        /// La valeur par défaut est 10485760 (10 Mo).
        /// </summary>
        public static long MaximumFileSize
        {
            get
            { return _maxFileSize; }
            set
            {
                if (value >= (1 * 1024 * 1024))
                    _maxFileSize = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialise l'objet lorsqu'on veut écrire dans la Console uniquement.
        /// </summary>
        /// <param name="destination">Destination du texte à logger.</param>
        public static void Initialize(Destination destination)
        {
            Initialize(_logPath, _logFilename, _logEventLogType, destination);
        }

        /// <summary>
        /// Initialise l'objet lorsqu'on veut écrire dans un fichier et/ou dans la Console.
        /// </summary>
        /// <param name="path">Chemin où se trouve/créer le fichier journal. (Par ex. C:\journaux\)</param>
        /// <param name="filename">Nom du fichier journal. (Par ex. log.txt)</param>
        /// <param name="destination">Destination du texte à logger.</param>
        public static void Initialize(string path, string filename, Destination destination)
        {
            Initialize(path, filename, _logEventLogType, destination);
        }

        /// <summary>
        /// Initialize l'objet lorsqu'on veut écrire dans le journal d'événement de Windows, dans un fichier et/ou dans la Console.
        /// </summary>
        /// <param name="evenLogType">Type d'événement.</param>
        /// <param name="path">Chemin où se trouve/créer le fichier journal. (Par ex. C:\journaux\)</param>
        /// <param name="filename">Nom du fichier journal. (Par ex. log.txt)</param>
        /// <param name="destination">Destination du texte à logger.</param>
        public static void Initialize(System.Diagnostics.EventLogEntryType evenLogType, string path, string filename, Destination destination)
        {
            Initialize(path, filename, evenLogType, destination);
        }

        /// <summary>
        /// Reset properties and set :
        /// LogPath to C:\
        /// LogFilename to log.txt
        /// MaximumFileSize to 10 MB
        /// LogEntryType to Error
        /// LogDestination to Console
        /// </summary>
        public static void Reset()
        {
            _logPath = @"C:\";
            _logFilename = "log.txt";
            _maxFileSize = 10 * 1024 * 1024; // 10 Mo;
            _logEventLogType = System.Diagnostics.EventLogEntryType.Error;
            _logDestination = Destination.Console;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void Initialize(string path, string filename, System.Diagnostics.EventLogEntryType eventLogType, Destination destination)
        {
            _logPath = path;
            _logFilename = filename;
            _logEventLogType = eventLogType;
            _logDestination = destination;
        }

        /// <summary>
        /// Ecrit le texte vers la/les sorties configurées.
        /// </summary>
        /// <param name="text">Texte à journaliser.</param>
        public static void Write(string text)
        {
            try
            {
                Write("Unkown", text, System.Diagnostics.EventLogEntryType.Information);
            }
            catch (System.Exception) { }
        }

        private static void Write(string appName, string text, System.Diagnostics.EventLogEntryType type)
        {
            if ((_logDestination & Destination.Console) == Destination.Console)
                WriteToConsole(text);
            if ((_logDestination & Destination.File) == Destination.File)
                WriteToFile(text);
            if ((_logDestination & Destination.WindowsEventLog) == Destination.WindowsEventLog)
                WriteToWindowsEventLog(appName, text, type);
        }

        private static void WriteToWindowsEventLog(string appName, string text, System.Diagnostics.EventLogEntryType type)
        {
            System.Diagnostics.EventLog.WriteEntry(appName, text, type);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void WriteToFile(string text)
        {
            StreamWriter logWriter = null;
            string fullPath = Path.Combine(_logPath, _logFilename);

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);

            if (File.Exists(fullPath))
            {
                TruncateFileIfNecessary();
                logWriter = File.AppendText(fullPath);
            }
            else
                logWriter = File.CreateText(fullPath);
            if (logWriter == null)
                return;
            logWriter.WriteLine(System.DateTime.Now + "\t" + text);
            logWriter.Flush();
            logWriter.Close();
            logWriter.Dispose();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void TruncateFileIfNecessary()
        {
            try
            {
                string fullPath = Path.Combine(_logPath, _logFilename);
                FileInfo info = new FileInfo(fullPath);
                if (info.Length > MaximumFileSize)
                {
                    StreamReader reader = new StreamReader(fullPath);
                    List<string> lines = new List<string>();
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                    reader.Close();
                    reader.Dispose();
                    reader = null;
                    StreamWriter writer = new StreamWriter(fullPath);
                    for (int i = (lines.Count / 2); i < lines.Count; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                    writer.Close();
                    writer.Dispose();
                    writer = null;
                }
            }
            catch (System.Exception) { }
        }

        private static void WriteToConsole(string text)
        {
            System.Console.WriteLine(System.DateTime.Now + " - " + text);
        }

        #endregion Methods
    }
}