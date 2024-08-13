using Azure;

namespace NZWalks.API.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Sku { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProductCategory> ProductCategories { get; } = [];
    }
}
