using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using NFI.Enums;

namespace NFI.Helper
{
    public static class ZipHelper
    {
        public static void CreateZipFromDirectory(string srcFolder, string zipFilePath)
        {
            ZipFile.CreateFromDirectory(srcFolder, zipFilePath);
        }

        public static void CreateZipFromFiles(List<string> filesToZip, string zipFilePath)
        {
            // Do nothing if no files are sent
            if (filesToZip == null || filesToZip.Count == 0 || string.IsNullOrEmpty(zipFilePath))
                return;

            try
            {
                string dirName = Path.GetDirectoryName(zipFilePath);
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }


                using (ZipArchive zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (var filePath in filesToZip)
                    {
                        zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    }
                }
            }
            catch(Exception ex)
            {
                // Remove invalid zip archive
                if (File.Exists(zipFilePath))
                    File.Delete(zipFilePath);
            }
        }

    }
}