using AlkoOutletBestPromotionsProvider.Models;
using System.Collections.Generic;

namespace AlkoOutletBestPromotionsProvider.Services
{
    public interface INotificationService
    {
        void NotifyAboutNewPromotions(IEnumerable<WhiskyPromotion> whiskyPromotions);
    }
}
