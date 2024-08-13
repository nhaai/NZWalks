using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> Children { get; } = [];
        public List<ProductCategory> ProductCategories { get; } = [];
    }
}
