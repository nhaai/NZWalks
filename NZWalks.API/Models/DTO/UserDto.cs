using NZWalks.API.Models.Domain;
using System.Text.Json.Serialization;

namespace NZWalks.API.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        [JsonPropertyName("fullname")]
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
        public string? UserName { get; set; }
        public string? Email => UserName;
        public string? PhoneNumber { get; set; }
        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; } = [];
        public string? Role => string.Join(',', UserRoles.Select(x => x.Role.Name));
    }
}
