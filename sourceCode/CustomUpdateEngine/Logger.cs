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
        private static string logPath = @"C:\";
        private static string logFilename = "log.txt";
        private static long maxFileSize = 10 * 1024 * 1024; // 10 Mo;
        private static System.Diagnostics.EventLogEntryType logEventLogType = System.Diagnostics.EventLogEntryType.Error;

        /// <summary>
        /// énumération des sorties possibles.
        /// </summary>
        public enum Destination
        {
            Console = 1,
            File = 2,
            WindowsEventLog = 4
        }

        private static Destination logDestination = Destination.Console;

        /// <summary>
        /// Initialise l'objet lorsqu'on veut écrire dans la Console uniquement.
        /// </summary>
        /// <param name="destination">Destination du texte à logger.</param>
        public static void Initialize(Destination destination)
        {
            Initialize(logPath, logFilename, logEventLogType, destination);
        }

        /// <summary>
        /// Initialise l'objet lorsqu'on veut écrire dans un fichier et/ou dans la Console.
        /// </summary>
        /// <param name="path">Chemin où se trouve/créer le fichier journal. (Par ex. C:\journaux\)</param>
        /// <param name="filename">Nom du fichier journal. (Par ex. log.txt)</param>
        /// <param name="destination">Destination du texte à logger.</param>
        public static void Initialize(string path, string filename, Destination destination)
        {
            Initialize(path, filename, logEventLogType, destination);
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void Initialize(string path, string filename, System.Diagnostics.EventLogEntryType eventLogType, Destination destination)
        {
            if (!path.EndsWith(@"\"))
                path += @"\";
            logPath = path;
            logFilename = filename;
            logEventLogType = eventLogType;
            logDestination = destination;
        }

        /// <summary>
        /// Obtient ou défini la taille maximal, en octets, du fichier journal. Le paramètre ne peut pas être inférieur à 1048576 (1 Mo).
        /// La valeur par défaut est 10485760 (10 Mo).
        /// </summary>
        public static long MaximumFileSize
        {
            get
            { return maxFileSize; }
            set
            {
                if (value >= (1 * 1024 * 1024))
                    maxFileSize = value;
            }
        }

        /// <summary>
        /// Ecrit le texte vers la/les sorties configurées.
        /// </summary>
        /// <param name="text">Texte à journaliser.</param>
        public static void Write(string text)
        {
            Write("Unkown", text, System.Diagnostics.EventLogEntryType.Information);
        }

        /// <summary>
        /// Ecrit le texte en précisant le nom de l'application et le type d'événement dans le journal d'événement de Windows.
        /// </summary>
        /// <param name="appName">Nom de l'application qui journalise.</param>
        /// <param name="text">Texte à journaliser.</param>
        /// <param name="type">Type d'événement.</param>
        public static void Write(string appName, string text, System.Diagnostics.EventLogEntryType type)
        {
            if ((logDestination & Destination.Console) == Destination.Console)
                WriteToConsole(text);
            if ((logDestination & Destination.File) == Destination.File)
                WriteToFile(text);
            if ((logDestination & Destination.WindowsEventLog) == Destination.WindowsEventLog)
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

            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);

            if (File.Exists(logPath + logFilename))
            {
                TruncateFileIfNecessary();
                logWriter = File.AppendText(logPath + logFilename);
            }
            else
                logWriter = File.CreateText(logPath + logFilename);
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
            FileInfo info = new FileInfo(logPath + logFilename);
            if (info.Length > MaximumFileSize)
            {
                StreamReader reader = new StreamReader(logPath + logFilename);
                List<string> lines = new List<string>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
                reader.Close();
                reader.Dispose();
                reader = null;
                StreamWriter writer = new StreamWriter(logPath + logFilename);
                for (int i = (lines.Count / 2); i < lines.Count; i++)
                {
                    writer.WriteLine(lines[i]);
                }
                writer.Close();
                writer.Dispose();
                writer = null;
            }
        }

        private static void WriteToConsole(string text)
        {
            System.Console.WriteLine(System.DateTime.Now + " - " + text);
        }

    }
}
