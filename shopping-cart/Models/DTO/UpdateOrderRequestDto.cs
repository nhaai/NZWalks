using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Models.DTO
{
    public class UpdateOrderRequestDto
    {
        public double? GrandTotal { get; set; }
        public int? UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
    }
}
