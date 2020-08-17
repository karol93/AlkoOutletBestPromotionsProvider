using AlkoOutletBestPromotionsProvider.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{
    internal class AzureBlobService : IAzureBlobService
    {
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly IOptions<AzureStorageBlobOptions> _options;

        public AzureBlobService(IOptions<AzureStorageBlobOptions> options)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(options.Value.ConnectionString);
            _cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            _options = options;
        }

        public async Task Upload(Stream stream, string fileName)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_options.Value.ContainerName);
            await cloudBlobContainer.CreateIfNotExistsAsync();
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            await cloudBlockBlob.UploadFromStreamAsync(stream);
        }

        public async Task<MemoryStream> Download(string fileName)
        {
            var blob = GetBlob(fileName);

            var stream = new MemoryStream();
            await blob.DownloadToStreamAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }

        private CloudBlockBlob GetBlob(string fileName)
        {
            var cloudBlobContainer = _cloudBlobClient.GetContainerReference(_options.Value.ContainerName);
            return cloudBlobContainer.GetBlockBlobReference(fileName);
        }
    }
}
