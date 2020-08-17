using System.IO;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{
    public interface IAzureBlobService
    {
        Task Upload(Stream stream, string fileName);
        Task<MemoryStream> Download(string fileName);
    }
}
