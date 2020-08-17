using System;

namespace AlkoOutletBestPromotionsProvider.Helpers
{
    public class NotificationOptions
    {
        public string Receiver { get; set; }
        public string Sender { get; set; }

        public NotificationOptions()
        {
            Receiver = Environment.GetEnvironmentVariable($"{nameof(NotificationOptions)}:Receiver");
            Sender =Environment.GetEnvironmentVariable($"{nameof(NotificationOptions)}:Sender");
        }
    }
}
