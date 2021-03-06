﻿using System;
using System.IO;
using NFI.Enums;
using NFI.Properties;

namespace NFI.Helper
{
    public static class DirectoryHelper
    {
        public static string GetRootDirectory()
        {
            return Settings.Default.RootDir;
        }

        public static string GetApplicationDirPath(ApplicationType appType)
        {
            var dirPath = "";
            switch (appType)
            {
                case ApplicationType.Sorfond:
                    dirPath = Settings.Default.ApplicationDir1;
                    break;
                case ApplicationType.Insentivordning:
                    dirPath = Settings.Default.ApplicationDir2;
                    break;
                case ApplicationType.IncentiveScheme:
                    dirPath = Settings.Default.ApplicationDir3;
                    break;
                case ApplicationType.Lansering:
                    dirPath = Settings.Default.ApplicationDir5;
                    break;
                case ApplicationType.UdsReisestotte:
                    dirPath = Settings.Default.ApplicationDir4;
                    break;
                case ApplicationType.Video:
                    dirPath = Settings.Default.ApplicationDir7;
                    break;
                case ApplicationType.Ordninger:
                    dirPath = Settings.Default.ApplicationDir6;
                    break;
                case ApplicationType.Film:
                    dirPath = Settings.Default.ApplicationDir8;
                    break;
                case ApplicationType.DenKulturelleSkolesekken:
                    dirPath = Settings.Default.ApplicationDir9;
                    break;
            }

            return Path.Combine(GetRootDirectory(), dirPath);
        }

        public static string GetApplicationDataFilePath(ApplicationType appType)
        {
            var filePath = Path.Combine(GetApplicationDirPath(appType), Settings.Default.ApplicationDataFile);

            return filePath;
        }

        public static string GetApplicationAttachmentDirPath(ApplicationType appType)
        {
            var filePath = Path.Combine(GetApplicationDirPath(appType), Settings.Default.AttachmentDir);

            return filePath;
        }

        public static string GetZipFilePath(ApplicationType appType, Guid appId, string userName)
        {
            var dirPath = Path.Combine(GetApplicationDirPath(appType), Settings.Default.ZipDir);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var fileName = userName + "_" + appId + ".zip";

            return Path.Combine(dirPath, fileName);
        }
    }
}