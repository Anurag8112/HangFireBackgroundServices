using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HangFireBackgroundServices.Services
{
    public static class SendEmail
    {
        public static void SendWelcomeEmail(string recipient, string subject, string body)
        {
            // Set up SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("anurag.katiyar@bigohtech.com", "ABkan011@#");

            // Set up email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("anurag.katiyar@bigohtech.com");
            mailMessage.To.Add(recipient);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            // Send email
            smtpClient.Send(mailMessage);
        }
    }
}
