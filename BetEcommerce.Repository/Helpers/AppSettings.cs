namespace BetEcommerce.Repository.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public class MailSettings
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
    }
}
