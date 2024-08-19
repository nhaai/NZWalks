namespace NZWalks.API.Models.DTO
{
    public class AddOrderRequestDto
    {
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
