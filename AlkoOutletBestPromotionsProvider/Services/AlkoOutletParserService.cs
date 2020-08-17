using AlkoOutletBestPromotionsProvider.Helpers;
using AlkoOutletBestPromotionsProvider.Models;
using AngleSharp;
using AngleSharp.Dom;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlkoOutletBestPromotionsProvider.Services
{

    internal class AlkoOutletParserService : IAlkoOutletParserService
    {
        private readonly IBrowsingContext _context;
        private const string BaseAddress = "https://alkooutlet.pl/";
        private const string PromotionAddress = "https://alkooutlet.pl/pl/promotions";
        private readonly int _discountMinimumValue;

        public AlkoOutletParserService(IOptions<AlkoOutletParserOptions> options)
        {
            _discountMinimumValue = options.Value.DiscountMinimumValue;
            var config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(config); 
        }
        public async Task<IEnumerable<WhiskyPromotion>> GetPromotions()
        {
            List<WhiskyPromotion> result = new List<WhiskyPromotion>();
            for (var page = 1; page <= await GetLastPage(); page++)
                await GetProductsFromSinglePage(result, page);
            return result.Where(p => p.Discount >= _discountMinimumValue).OrderByDescending(p => p.Discount);
        }
        private async Task<int> GetLastPage()
        {
            using (IDocument document = await _context.OpenAsync(PromotionAddress))
            {
                var cellSelector = "#box_mainproducts > div.innerbox > div.floatcenterwrap > ul > li:nth-last-child(2) > a";
                var cells = document.QuerySelectorAll(cellSelector);
                return Convert.ToInt32(cells.Select(m => m.TextContent).First());
            }
        }

        private async Task GetProductsFromSinglePage(List<WhiskyPromotion> result, int page)
        {
            using (IDocument document = await _context.OpenAsync($"{PromotionAddress}/{page}"))
            {
                var productsSelector = "#box_mainproducts > div.innerbox > div.products.viewphot.s-row > div[data-category='WHISKY']";
                var products = document.QuerySelectorAll(productsSelector);
                foreach (var product in products)
                {
                    var isAvailable = product.QuerySelector("form.availability-notifier") == null;
                    if (isAvailable)
                        result.Add(ParseProduct(product));
                }
            }
        }

        private WhiskyPromotion ParseProduct(IElement product)
        {
            var whiskyName = product.QuerySelector("span.productname").TextContent;
            var newPrice = decimal.Parse(Regex.Match(product.QuerySelector("div.price > em").TextContent, @"\d+").Value, NumberFormatInfo.InvariantInfo);
            var oldPrice = decimal.Parse(Regex.Match(product.QuerySelector("div.price > del").TextContent, @"\d+").Value, NumberFormatInfo.InvariantInfo);
            var discount = (int)((oldPrice - newPrice) / oldPrice * 100);
            var url = $"{BaseAddress}/{product.QuerySelector("a.prodname").GetAttribute("href")}";
            return new WhiskyPromotion(whiskyName, oldPrice, newPrice, discount, url);
        }
    }
}
