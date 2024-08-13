namespace NZWalks.API.Models.DTO
{
    public class AddCategoryRequestDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ParentId { get; set; }
    }
}
