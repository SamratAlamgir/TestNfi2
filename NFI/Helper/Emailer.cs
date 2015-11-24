using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using NFI.Properties;

namespace NFI.Utility
{
    public sealed class Emailer
    {
        private Emailer()
        {
        }

        public static bool SendMail(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                var host = Settings.Default.EmailHost;
                var port = Settings.Default.EmailPort;
                var fromEmail = Settings.Default.FromEmailAddress;
                var fromPassword = Settings.Default.FromPassword;
                var fromName = Settings.Default.FromName;

                var smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = int.Parse(port),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, fromPassword)
                };
                

                var fromAddress = new MailAddress(fromEmail, fromName);
                
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