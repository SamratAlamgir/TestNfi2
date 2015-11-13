using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace NFI.Utility
{
    public sealed class Emailer
    {
        private Emailer()
        {
        }

        public static bool SendMail(string from, string to, string fromName, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                var host = ConfigurationManager.AppSettings[SettingsKey.EmailHost.ToString()];
                var port = Convert.ToInt32(ConfigurationManager.AppSettings[SettingsKey.EmailPort.ToString()]);
                var fromEmail = ConfigurationManager.AppSettings[SettingsKey.FromEmail.ToString()];
                var fromPassword = ConfigurationManager.AppSettings[SettingsKey.FromPassWord.ToString()];

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, fromPassword)
                };

                fromName = string.IsNullOrWhiteSpace(fromName) ? ConfigurationManager.AppSettings[SettingsKey.FromName.ToString()] : fromName;

                var fromAddress = new MailAddress(from, fromName);
                
                //From address will be given as a MailAddress Object
                message.From = fromAddress;

                // To address collection of MailAddress
                message.To.Add(to);

                message.Subject = subject;

                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;

                // Message body content
                message.Body = body;

                //Send SMTP mail
                smtpClient.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

    }
}