namespace SA51_CA_Project_Team10.Models.DTO
{
    public class UpdateOrderItemRequestDto
    {
        public bool? IsAvailable { get; set; }
        public int? ProductCount { get; set; }
        public double? BuyingPrice { get; set; }
        public double? Total { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}
