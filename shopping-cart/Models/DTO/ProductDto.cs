using System.Text.Json.Serialization;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Brand { get; set; }
        public double? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int? Purchases { get; set; }
        public int? Views { get; set; }
        public string? ImageUrl { get; set; }
        [JsonPropertyName("active")]
        public bool? IsActive { get; set; }
        public int? CategoryId { get; set; }
    }
}
