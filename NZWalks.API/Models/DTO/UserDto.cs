using NZWalks.API.Models.Domain;
using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        [JsonPropertyName("fullname")]
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
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
        public required string Email { get; set; }
        [JsonPropertyName("username")]
        public string UserName => Email.Split('@')[0];
        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; } = [];
        public string Role => String.Join(',', UserRoles.Select(x => x.Role.Name));
    }
}
