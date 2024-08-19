using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class UpdateProductRequestDto
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        [JsonPropertyName("price")]
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
        [JsonPropertyName("active")]
        public bool? IsActive { get; set; }
        [JsonPropertyName("category_id")]
        public int? CategoryId { get; set; }
    }
}
