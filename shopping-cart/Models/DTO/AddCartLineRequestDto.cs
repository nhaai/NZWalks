namespace SA51_CA_Project_Team10.Models.DTO
{
    public class AddCartLineRequestDto
    {
        public bool? IsAvailable { get; set; }
        public int? ProductCount { get; set; }
        public double? BuyingPrice { get; set; }
        public double? Total { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
    }
}
