using AlkoOutletBestPromotionsProvider.Helpers;
using AlkoOutletBestPromotionsProvider.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlkoOutletBestPromotionsProvider.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly string _receiver;
        private readonly string _sender;

        public NotificationService(IEmailService emailService, IOptions<NotificationOptions> options)
        {
            _emailService = emailService;
            _receiver = options.Value.Receiver;
            _sender = options.Value.Sender;
        }

        public void NotifyAboutNewPromotions(IEnumerable<WhiskyPromotion> whiskyPromotions)
        {
            var emailBody = string.Join("</br>", whiskyPromotions.Select(f => $"{f.Name}, Promocja: <b>{f.Discount} %</b>, Różnica w cenie: {f.OldPrice - f.NewPrice} zł, Nowa cena: {f.NewPrice} zł, Stara cena: {f.OldPrice} zł, Url: <a href='{f.Url}'>{f.Url}</a>"));
            var subject = $"Whisky promotions - {DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}";
            _emailService.SendEmail(_sender, _receiver, emailBody, subject);
        }
    }
}
