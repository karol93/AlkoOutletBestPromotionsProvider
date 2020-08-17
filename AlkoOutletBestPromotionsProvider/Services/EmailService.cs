using AlkoOutletBestPromotionsProvider.Helpers;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AlkoOutletBestPromotionsProvider.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _client;

        public EmailService(IOptions<EmailOptions> options)
        {
            EmailOptions emailOptions = options.Value;
            _client = new SmtpClient(emailOptions.Host)
            {
                Port = emailOptions.Port,
                EnableSsl = emailOptions.EnalbeSsl,
                Credentials = new NetworkCredential(emailOptions.Login, emailOptions.Password)
            };
        }

        public void SendEmail(string from, string to, string body, string title)
        {
            MailMessage objMsg = new MailMessage();
            objMsg.From = new MailAddress(from);
            objMsg.To.Add(new MailAddress(to));
            objMsg.Body = body;
            objMsg.Subject = title;
            objMsg.IsBodyHtml = true;
            _client.Send(objMsg);
        }
    }
}
