using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<CartLineDto> CartLines { get; set; } = [];
    }
}
