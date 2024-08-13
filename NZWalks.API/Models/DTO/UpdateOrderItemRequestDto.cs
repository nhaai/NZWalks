namespace NZWalks.API.Models.DTO
{
    public class UpdateOrderItemRequestDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
