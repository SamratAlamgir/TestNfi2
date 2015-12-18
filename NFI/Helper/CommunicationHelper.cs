using System.Collections.Generic;
using NFI.Properties;
using NFI.Utility;

namespace NFI.Helper
{
    public static class CommunicationHelper
    {
        public static async void SendConfirmationEmailToUserAsync(string subject, string body, string mailTo)
        {
            var fromEmail = Settings.Default.FromEmailAddress;
            var fromName = Settings.Default.FromName;

            Emailer.SendMailAsync(mailTo, subject, body,fromEmail, fromName);
        }

        // Admin receipent mail address may vary with application
        public static async void SendEmailToAdminAsync(string subject, string body, string mailTo, string fromEmail, string fromName, List<string> attachmentFilePaths)
        {
            Emailer.SendMailAsync(mailTo, subject, body, fromEmail, fromName, attachmentFilePaths);
        }
    }
}