using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentId { get; set; }
        public Category ParentCategory { get; set; } = default!;
        public virtual ICollection<Category> Children { get; set; } = default!;
    }
}
