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

            return dirPath;
        }

        public static string GetApplicationDataFilePath(ApplicationType appType)
        {
            var fileName = "";

            if (appType == ApplicationType.Application1)
            {
                fileName = Settings.Default.ApplicationDataFile;
            }

            var filePath = Path.Combine(GetApplicationDirPath(appType), fileName);

            return filePath;
        }
    }
}