using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class UpdateCartRequestDto
    {
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<CartLineDto> CartLines { get; set; } = [];
    }
}
