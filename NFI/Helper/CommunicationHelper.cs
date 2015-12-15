using System;
using System.Collections.Generic;
using NFI.Properties;
using NFI.Utility;

namespace NFI.Helper
{
    public static class CommunicationHelper
    {
        public static bool SendConfirmationEmailToUser(string subject, string body, string mailTo, List<string> attachmentFilePaths = null)
        {
            var fromEmail = Settings.Default.FromEmailAddress;
            var fromName = Settings.Default.FromName;

            return Emailer.SendMail(mailTo, subject, body,fromEmail, fromName, attachmentFilePaths);
        }

        // Admin mail address may vary with application
        public static bool SendEmailToAdmin(string subject, string body, string mailTo, string fromEmail, string fromName, List<string> attachmentFilePaths = null)
        {
            return Emailer.SendMail(mailTo, subject, body, fromEmail, fromName, attachmentFilePaths);
        }
    }
}