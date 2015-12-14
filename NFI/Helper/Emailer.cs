using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using NFI.Helper;
using NFI.Properties;

namespace NFI.Utility
{
    public sealed class Emailer
    {
        private Emailer()
        {
        }

        public static bool SendMail(string to, string subject, string body, string fromEmail, string fromName, List<string> attachmentFilePath = null)
        {
            try
            {
                var message = new MailMessage();
             
                var smtpClient = new SmtpClient(Settings.Default.EmailHost, 25);
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
                        if (!File.Exists(filePath)) { continue; }

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
                smtpClient.Send(message);
            }
            catch (SmtpException smtpEx)
            {
                LogWriter.Write(smtpEx.ToString(), "Error");
                return false;
            }
            catch (Exception ex)
            {
                LogWriter.Write(ex.ToString(), "Error");
                return false;
            }

            return true;
        }

    }
}