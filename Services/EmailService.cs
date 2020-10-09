using System;
using dotenv.net.Utilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

namespace tp1_restaurant.Services
{
    public interface IEmailService 
    {
        bool SendEmail(string to, string subject, string html);
    }

    public class EmailService: IEmailService
    {
        private readonly EnvReader _envReader;
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger, EnvReader envReader) {
            _logger = logger;
            _envReader = envReader;
        }

        public bool SendEmail(string to, string subject, string html) {
            try {
                string SMTP_HOST = _envReader.GetStringValue("SMTP_HOST");
                int SMTP_PORT = _envReader.GetIntValue("SMTP_PORT");
                string SMTP_USER = _envReader.GetStringValue("SMTP_USER");
                string SMTP_PASSWORD = _envReader.GetStringValue("SMTP_PASSWORD");
                bool SMTP_SECURE = _envReader.GetBooleanValue("SMTP_SECURE");

                SmtpClient smtpClient = new SmtpClient(SMTP_HOST, SMTP_PORT);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(SMTP_USER, SMTP_PASSWORD);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = SMTP_SECURE;

                MailAddress From = new MailAddress(SMTP_USER, "Zhao Restaurant");
                MailAddress To = new MailAddress(to);
                MailMessage Message = new MailMessage(From, To);
                Message.IsBodyHtml = true;
                Message.Body = html;
                Message.BodyEncoding =  System.Text.Encoding.UTF8;
                Message.Subject = subject;
                Message.SubjectEncoding = System.Text.Encoding.UTF8;

                smtpClient.Send(Message);

                Message.Dispose();
                smtpClient.Dispose();

                return true;
            } catch(Exception ex) {
                _logger.LogError($"Une erreur s'est produite pendant l'envoi de courriel: {ex.Message}");
                return false;
            }
        }
    }
}