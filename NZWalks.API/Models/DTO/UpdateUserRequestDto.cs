namespace NZWalks.API.Models.DTO
{
    public class UpdateUserRequestDto
    {
        public string? Title { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string PostalCode { get; set; } = null!;
        public string? Country { get; set; }
    }
}
