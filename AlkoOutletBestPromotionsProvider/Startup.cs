using AlkoOutletBestPromotionsProvider.Helpers;
using AlkoOutletBestPromotionsProvider.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AlkoOutletBestPromotionsProvider.Startup))]
namespace AlkoOutletBestPromotionsProvider
{
    public class Startup : FunctionsStartup
    {
        public IConfigurationRoot Configuration { get; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            RegisterOptions(builder);
            RegisterServices(builder);
        }

        private void RegisterOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<AzureStorageBlobOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("MyOptions").Bind(settings);
                });
            builder.Services.AddOptions<EmailOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("MyOptions").Bind(settings);
                });
            builder.Services.AddOptions<AlkoOutletParserOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("MyOptions").Bind(settings);
                });
            builder.Services.AddOptions<NotificationOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("MyOptions").Bind(settings);
                });
        }

        private void RegisterServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IAlkoOutletParserService, AlkoOutletParserService>();
            builder.Services.AddSingleton<IAzureBlobService, AzureBlobService>();
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<INotificationService, NotificationService>();
            builder.Services.AddSingleton<IFileService, FileService>();
        }
    }
}
