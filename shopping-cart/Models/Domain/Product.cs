namespace NZWalks.API.Models.Domain
{
    public class Product
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
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
