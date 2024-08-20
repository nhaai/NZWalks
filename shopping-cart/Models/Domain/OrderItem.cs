namespace SA51_CA_Project_Team10.Models.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public bool? IsAvailable { get; set; }
        public int? ProductCount { get; set; }
        public double? BuyingPrice { get; set; }
        public double? Total { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; } = default!;
        public int? ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
