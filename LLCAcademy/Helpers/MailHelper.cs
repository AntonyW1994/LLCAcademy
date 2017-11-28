using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace LLCAcademy.Helpers
{
    public static class MailHelper
    {
        public static bool SendMessage(string subject, string body)
        {
            var emailsTo = System.Configuration.ConfigurationManager.AppSettings["GeneralEmailTo"].Split(';').ToList();

            var sentSuccessfully = true;

            foreach (var emailTo in emailsTo)
            {
                try
                {
                    var to = new MailAddress(emailTo);
                    var from = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["EmailFrom"]);
                    var mail = new MailMessage(from, to)
                    {
                        Subject = subject,
                        Body = body
                    };
                    var smtp = new SmtpClient
                    {
                        Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"],
                        Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]),
                        Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUsername"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]),
                        EnableSsl = true
                    };
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    SendErrorNotification("LL Coaching Academy - Error Sending Email", ex.Message);
                    sentSuccessfully = false;
                }
            }
            return sentSuccessfully;
        }

        public static bool SendErrorNotification(string subject, string body)
        {
            try
            {
                var to = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["TechEmailTo"]);
                var from = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["SmtpUsername"]);
                var mail = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body
                };
                var smtp = new SmtpClient
                {
                    Host = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"],
                    Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"]),
                    Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SmtpUsername"], System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"]),
                    EnableSsl = true
                };
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}