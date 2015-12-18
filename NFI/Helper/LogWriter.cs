using System;
using System.IO;

namespace NFI.Helper
{
    public static class LogWriter
    {
        public static void Write(string logText, string logType = "Info")
        {
            string logFileName = "log_" + DateTime.Today.ToString("dd-MM-yyyy") + ".txt";
            string logFilePath = Path.Combine(DirectoryHelper.GetRootDirectory(), logFileName);


            using (FileStream fs = File.Open(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine("Log Time: " + DateTime.Now);
                    writer.WriteLine("Log type: " + logType);

                    writer.WriteLine(logText);
                }
            }
        }
    }
}
