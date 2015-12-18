using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using NFI.Helper;
using NFI.Properties;

namespace NFI.Utility
{
    public static class Emailer
    {
        public static async void SendMailAsync(string to, string subject, string body, string fromEmail, string fromName, List<string> attachmentFilePath = null)
        {
            try
            {
                var message = new MailMessage();
                var smtpClient = new SmtpClient(Settings.Default.EmailHost, Convert.ToInt32(Settings.Default.EmailPort));

                if (Settings.Default.ApplicationServer == "Dev")
                {
                    fromEmail = "systemnfi@gmail.com";
                    smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Timeout = 3600000, // 1 hour
                        Credentials = new NetworkCredential(fromEmail, "SystemNfi987")
                    };
                }

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

                if (attachmentFilePath != null)
                {
                    foreach (var filePath in attachmentFilePath)
                    {
                        if (!File.Exists(filePath))
                        {
                            continue;
                        }

                        // Create  the file attachment for this e-mail message.
                        Attachment data = new Attachment(filePath, MediaTypeNames.Application.Octet);
                        data.Name = Path.GetFileName(filePath);
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);
                        // Add the file attachment to this e-mail message.
                        message.Attachments.Add(data);
                    }
                }

                //Send SMTP mail
                string userState = "##Email sending to...: " + message.To.First().Address;
                LogWriter.Write(userState);

                smtpClient.SendCompleted += SendCompletedCallback;
                smtpClient.SendAsync(message, userState);
            }
            catch (SmtpException smtpEx)
            {
                LogWriter.Write(smtpEx.ToString(), "Error");
                LogWriter.Write("Stack trace:" + smtpEx.StackTrace);
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                LogWriter.Write("Stack trace:" + ex.StackTrace);
            }
           
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                LogWriter.Write($"[{token}] Send canceled.");
            }

            if (e.Error != null)
            {
                LogWriter.Write($"Email Sent: {Environment.NewLine} [{token}] {e.Error}");
            }
            else
            {
                LogWriter.Write("##Mail sent.");
            }
        }
    }
}