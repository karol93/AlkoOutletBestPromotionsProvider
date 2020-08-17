using AlkoOutletBestPromotionsProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{
    public interface IAlkoOutletParserService
    {
        Task<IEnumerable<WhiskyPromotion>> GetPromotions();
    }
}
