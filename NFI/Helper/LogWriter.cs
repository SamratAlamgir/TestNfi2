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

            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine("Log Time: " + DateTime.Now);
                writer.WriteLine("Log type: " + logType);

                writer.WriteLine(logText);
            }
        }
    }
}