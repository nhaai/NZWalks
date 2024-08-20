namespace SA51_CA_Project_Team10.Models.Domain
{
    public class CartLine
    {
        public int Id { get; set; }
        public bool? IsAvailable { get; set; }
        public int? ProductCount { get; set; }
        public double? BuyingPrice { get; set; }
        public double? Total { get; set; }
        public int? CartId { get; set; }
        public Cart Cart { get; set; } = default!;
        public int? ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
