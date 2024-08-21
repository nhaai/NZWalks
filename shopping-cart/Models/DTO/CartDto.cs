using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<CartLineDto> CartLines { get; set; } = [];
    }
}
