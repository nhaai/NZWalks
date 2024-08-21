using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class AddCartRequestDto
    {
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<CartLineDto> CartLines { get; set; } = [];
    }
}
