using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonPropertyName("active")]
        public bool? IsActive { get; set; }
        [JsonPropertyName("parent")]
        public int? ParentId { get; set; }
    }
}
