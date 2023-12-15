using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TechBeyondThoughts.Web.Service
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _adminEmail;
        private readonly bool _enableSSL;
        private readonly bool _useDefaultCredentials;
        private readonly bool _isBodyHtml;

        public EmailService(string smtpHost, int smtpPort, string smtpUsername, string smtpPassword, string adminEmail, bool enableSSL, bool useDefaultCredentials, bool isBodyHtml)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
            _adminEmail = adminEmail;
            _enableSSL = enableSSL;
            _useDefaultCredentials = useDefaultCredentials;
            _isBodyHtml = isBodyHtml;
        }

        public void SendEmail(string name, string email, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(name, email));
            emailMessage.To.Add(new MailboxAddress("", _adminEmail));
            emailMessage.Subject = "New Contact Form Submission";

            emailMessage.Body = new TextPart("plain")
            {
                Text = $"Name: {name}\nEmail: {email}\n\nMessage:\n{message}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpHost, _smtpPort, _enableSSL);
                client.Authenticate(_smtpUsername, _smtpPassword);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }

}
