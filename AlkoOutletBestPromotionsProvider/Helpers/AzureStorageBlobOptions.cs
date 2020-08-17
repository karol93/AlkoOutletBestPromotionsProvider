using System;

namespace AlkoOutletBestPromotionsProvider.Helpers
{
    public class AzureStorageBlobOptions
    {
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }

        public AzureStorageBlobOptions()
        {
            this.ConnectionString =
                Environment.GetEnvironmentVariable(
                    $"{nameof(AzureStorageBlobOptions)}:ConnectionString");
            this.ContainerName =
                Environment.GetEnvironmentVariable(
                    $"{nameof(AzureStorageBlobOptions)}:ContainerName");
        }
    }
}
