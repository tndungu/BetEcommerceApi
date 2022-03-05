using BetEcommerce.Repository.Helpers;
using BetEcommerce.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IConfiguration configuration,IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async void SendEmailAsync(string message, string emailAddress)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_mailSettings.FromEmail, _mailSettings.FromName),
                Subject = _mailSettings.Subject,
                Body = message,
                IsBodyHtml = true,
            };

            mail.To.Add(emailAddress);

            var client = new SmtpClient(_mailSettings.Host,_mailSettings.Port)
            {
                Credentials = new NetworkCredential(_mailSettings.SmtpUser, _mailSettings.SmtpPassword),
                EnableSsl = true
            };
            client.Send(mail);
        }
    }
}
