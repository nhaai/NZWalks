namespace NZWalks.API.Models.DTO
{
    public class CartLineDto
    {
        public int Id { get; set; }
        public bool? IsAvailable { get; set; }
        public int? ProductCount { get; set; }
        public double? BuyingPrice { get; set; }
        public double? Total { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
    }
}
