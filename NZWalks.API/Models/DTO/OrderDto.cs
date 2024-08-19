namespace NZWalks.API.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
