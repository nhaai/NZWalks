namespace NZWalks.API.Models.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
