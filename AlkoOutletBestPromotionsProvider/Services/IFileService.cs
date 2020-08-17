using AlkoOutletBestPromotionsProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{
    public interface IFileService
    {
        Task WritePromotionsToFile(IEnumerable<WhiskyPromotion> data);
        Task<IEnumerable<string>> ReadPromotionFromFile();
    }
}
