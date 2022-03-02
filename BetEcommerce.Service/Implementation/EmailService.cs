using BetEcommerce.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BetEcommerce.Service.Implementation
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration Configuration;
        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async void SendEmailAsync(string message, string emailAddress)
        {
            string MailHost = Configuration["MailSettings:MailHost"],
                SmtpUser = Configuration["MailSettings:SmtpUser"],
                SmtpPassword = Configuration["MailSettings:SmtpPassword"],
                FromEmail = Configuration["MailSettings:FromEmail"],
                FromName = Configuration["MailSettings:FromName"];

            SmtpClient smtpClient = new SmtpClient(MailHost, 587);

            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(FromEmail, FromName);
            mail.To.Add(new MailAddress(emailAddress));
            mail.Body = message;

            smtpClient.Send(mail);
        }
    }
}
