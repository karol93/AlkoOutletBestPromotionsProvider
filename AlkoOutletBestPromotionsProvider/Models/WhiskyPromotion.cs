namespace AlkoOutletBestPromotionsProvider.Models
{
    public class WhiskyPromotion
    {
        public WhiskyPromotion(string name, decimal oldPrice, decimal newPrice, int discount, string url)
        {
            Name = name;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            Discount = discount;
            Url = url;
        }

        public string Name { get; }
        public decimal OldPrice { get; }
        public decimal NewPrice { get; }
        public int Discount { get; }
        public string Url { get; }
    }
}
