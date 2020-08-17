using System;

namespace AlkoOutletBestPromotionsProvider.Helpers
{
    public class EmailOptions
    {
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnalbeSsl { get; set; }

        public EmailOptions()
        {
            Host = Environment.GetEnvironmentVariable($"{nameof(EmailOptions)}:Host");
            Login = Environment.GetEnvironmentVariable($"{nameof(EmailOptions)}:Login");
            Password = Environment.GetEnvironmentVariable($"{nameof(EmailOptions)}:Password");
            Port = Convert.ToInt32(Environment.GetEnvironmentVariable($"{nameof(EmailOptions)}:Port"));
            EnalbeSsl =  Convert.ToBoolean(Environment.GetEnvironmentVariable( $"{nameof(EmailOptions)}:EnalbeSsl"));
        }
    }
}
