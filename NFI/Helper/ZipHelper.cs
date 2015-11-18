using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using NFI.Enums;

namespace NFI.Helper
{
    public class ZipHelper
    {
        public static void CreateZipFromDirectory(string srcFolder, ApplicationType appType, Guid appId)
        {
            string zipFile = DirectoryHelper.GetZipFilePath(appType, appId);

            //call the ZipFile.CreateFromDirectory() method
            ZipFile.CreateFromDirectory(srcFolder, zipFile);
        }

        public static void CreateZipFromFiles(List<string> filesToZip, ApplicationType appType, Guid appId)
        {
            // Do nothing if no files are sent
            if (filesToZip == null || filesToZip.Count == 0)
                return;

            //provide the path and name for the zip file to create
            string zipFile = DirectoryHelper.GetZipFilePath(appType, appId);

            try
            {
                using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
                {
                    foreach (var filePath in filesToZip)
                    {
                        zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                    }
                }
            }
            catch 
            {
                // Remove invalid zip archive
                if (File.Exists(zipFile))
                    File.Delete(zipFile);
            }
        }

    }
}