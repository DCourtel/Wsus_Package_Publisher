using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal static class MaxCabFileChecker
    {
        /// <summary>
        /// Compare the size of the biggest file in cref="folder" with the wsus setting 'LocalPublishingMaxCabSize'. If the size of the file is greater than the setting, allow the user to modify this setting.
        /// </summary>
        /// <param name="folder">Folder where to look for file (search also in sub-folder)</param>
        internal static void CheckMaxFileSize(string folder)
        {
            Logger.EnteringMethod(folder);

            string fullFilename = GetBiggestFile(folder, -1);
            int currentMaxCabSize = WsusWrapper.GetInstance().LocalPublishingMaxCabSize;
            Logger.Write("Current MaxCabFile Size is : " + currentMaxCabSize.ToString());
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fullFilename);
            if (fileInfo.Exists && GetSizeInMegabytes(fileInfo.Length) >= currentMaxCabSize)
            {
                FrmChangeMaxCabSize frmMaxCabSize = new FrmChangeMaxCabSize(fullFilename, GetSizeInMegabytes(fileInfo.Length));
                frmMaxCabSize.ShowDialog();
            }
        }

        private static int GetSizeInMegabytes(long sizeInBytes)
        {
            return (int)(sizeInBytes / 1024 / 1024);
        }

        private static string GetBiggestFile(string folder, long biggestSize)
        {
            Logger.EnteringMethod(folder + " : " + biggestSize.ToString());
            string biggestFile = string.Empty;
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(System.IO.Path.GetFullPath(folder));

            foreach (System.IO.FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Length > biggestSize)
                {
                    biggestSize = file.Length;
                    biggestFile = file.FullName;
                }
            }

            foreach (System.IO.DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                string tempFile = GetBiggestFile(directory.FullName, biggestSize);
                if (!string.IsNullOrEmpty(tempFile))
                {
                    biggestFile = tempFile;
                    biggestSize = (new System.IO.FileInfo(biggestFile)).Length;
                }
            }

            Logger.Write("Will return : " + biggestFile);
            return biggestFile;
        }
    }
}
