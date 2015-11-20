using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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

            if (appType == ApplicationType.Application1)
            {
                dirPath = Settings.Default.ApplicationDir1;
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