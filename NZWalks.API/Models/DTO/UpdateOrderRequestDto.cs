namespace NZWalks.API.Models.DTO
{
    public class UpdateOrderRequestDto
    {
        public int CustomerId { get; set; }
        public string Code { get; set; }
        public double Total { get; set; }
        public int Status { get; set; }
    }
}
