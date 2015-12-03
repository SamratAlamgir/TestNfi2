using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFI.Utility;

namespace NFI.Helper
{
    public static class CommunicationHelper
    {
        public static bool SendMailToExecutive(Dictionary<string, string> values, string mailTo)
        {
            var subject = "Application submitted by <UserName>";
            var body = "A new application of type <ApplicationType> has been submitted." + Environment.NewLine +
                       "Download zip file: <ZipFileLink>" + Environment.NewLine +
                       "View detail: <DetailViewLink>";

            foreach (var key in values.Keys)
            {
                subject = subject.Replace(key, values[key]);
                body = body.Replace(key, values[key]);
            }

            return Emailer.SendMail(mailTo, subject, body);
        }

        public static bool SendMailToExecutive(string subject, string body, string mailTo)
        {
            return Emailer.SendMail(mailTo, subject, body);
        }

        public static bool SendMailToApplicant(Dictionary<string, string> values, string mailTo)
        {
            var subject = "Application type <ApplicationType> submitted";
            var body = "Hi <UserName>," + Environment.NewLine +
                       "Thanks for submitting the application";

            foreach (var key in values.Keys)
            {
                subject = subject.Replace(key, values[key]);
                body = body.Replace(key, values[key]);
            }

            return Emailer.SendMail(mailTo, subject, body);
        }
    }
}