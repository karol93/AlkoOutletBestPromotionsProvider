using AlkoOutletBestPromotionsProvider.Models;
using AlkoOutletBestPromotionsProvider.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider
{
    public class Function1
    {
        private readonly IFileService _fileService;
        private readonly IAlkoOutletParserService _alkoOutletParserService;
        private readonly INotificationService _notificationService;

        public Function1(IFileService fileService, IAlkoOutletParserService alkoOutletParserService, INotificationService notificationService)
        {
            _fileService = fileService;
            _alkoOutletParserService = alkoOutletParserService;
            _notificationService = notificationService;
        }

        [FunctionName("Function1")]
        public async Task RunAsync([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log)

        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            IEnumerable<WhiskyPromotion> currentPromotions = await _alkoOutletParserService.GetPromotions();
            IEnumerable<string> recentSavedPromotions = await _fileService.ReadPromotionFromFile();

            if (ArePromotionsChanged(currentPromotions, recentSavedPromotions))
            {
                _notificationService.NotifyAboutNewPromotions(currentPromotions);
                await _fileService.WritePromotionsToFile(currentPromotions);
            }
        }

        private static bool ArePromotionsChanged(IEnumerable<WhiskyPromotion> whiskyPromotions, IEnumerable<string> latestPromotions) 
            => !latestPromotions.Any() || latestPromotions.Intersect(whiskyPromotions.Select(p => p.Name)).Count() != latestPromotions.Count();
    }
}
