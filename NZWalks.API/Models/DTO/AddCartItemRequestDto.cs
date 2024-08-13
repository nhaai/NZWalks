namespace NZWalks.API.Models.DTO
{
    public class AddCartItemRequestDto
    {
        public string SessionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
