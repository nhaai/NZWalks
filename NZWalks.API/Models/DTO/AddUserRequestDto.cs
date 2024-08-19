using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class AddUserRequestDto
    {
        public string? FullName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        [JsonPropertyName("avatar")]
        public string? AvatarUrl { get; set; }
        [JsonPropertyName("active")]
        public bool? IsActive { get; set; }
        public string? Notes { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
