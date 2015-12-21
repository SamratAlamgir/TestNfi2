using System.Collections.Generic;
using System.Threading.Tasks;
using NFI.Properties;
using NFI.Utility;

namespace NFI.Helper
{
    public static class CommunicationHelper
    {
        public static void SendConfirmationEmailToUser(string subject, string body, string mailTo)
        {
            var fromEmail = Settings.Default.FromEmailAddress;
            var fromName = Settings.Default.FromName;

            Task.Run(() => Emailer.SendMailAsync(mailTo, subject, body,fromEmail, fromName));
        }

        // Admin receipent mail address may vary with application
        public static void SendEmailToAdmin(string subject, string body, string mailTo, string fromEmail, string fromName, List<string> attachmentFilePaths)
        {
            Task.Run(() => Emailer.SendMailAsync(mailTo, subject, body, fromEmail, fromName, attachmentFilePaths));
        }
    }
}