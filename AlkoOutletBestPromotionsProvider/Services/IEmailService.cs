namespace AlkoOutletBestPromotionsProvider.Services
{
    public interface IEmailService
    {
        void SendEmail(string from, string to, string body, string title);
    }
}
