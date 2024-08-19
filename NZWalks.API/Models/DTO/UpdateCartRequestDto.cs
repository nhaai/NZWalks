namespace NZWalks.API.Models.DTO
{
    public class UpdateCartRequestDto
    {
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<CartLineDto> CartLines { get; set; } = [];
    }
}
