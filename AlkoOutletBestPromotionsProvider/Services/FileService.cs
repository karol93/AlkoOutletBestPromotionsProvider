using AlkoOutletBestPromotionsProvider.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{
    internal class FileService : IFileService
    {
        private readonly IAzureBlobService _azureBlobService;
        private const string FileName = "latestPromotions.txt";

        public FileService(IAzureBlobService azureBlobService)
        {
            _azureBlobService = azureBlobService ?? throw new ArgumentNullException(nameof(azureBlobService));
        }

        public async Task<IEnumerable<string>> ReadPromotionFromFile()
        {
            List<string> result = new List<string>();
            using (var reader = new StreamReader(await _azureBlobService.Download(FileName), Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
            return result;
        }


        public async Task WritePromotionsToFile(IEnumerable<WhiskyPromotion> data)
        {
            using (var ms = new MemoryStream())
            {
                TextWriter tw = new StreamWriter(ms);
                tw.Write(string.Join(Environment.NewLine, data.Select(f => f.Name)));
                tw.Flush();
                ms.Position = 0;
                await _azureBlobService.Upload(ms, FileName);
            }
        }
    }
}
