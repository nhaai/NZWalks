namespace NZWalks.API.Models.DTO
{
    public class UpdateCartItemRequestDto
    {
        public string SessionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
