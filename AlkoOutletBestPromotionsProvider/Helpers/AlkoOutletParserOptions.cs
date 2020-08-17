using System;

namespace AlkoOutletBestPromotionsProvider.Helpers
{
    public class AlkoOutletParserOptions
    {
        public int DiscountMinimumValue { get; set; }

        public AlkoOutletParserOptions()
        {
            DiscountMinimumValue =  Convert.ToInt32(Environment.GetEnvironmentVariable($"{nameof(AlkoOutletParserOptions)}:DiscountMinimumValue"));
        }
    }
}
