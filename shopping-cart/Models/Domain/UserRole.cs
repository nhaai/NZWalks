using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Models.Domain
{
    public class UserRole: IdentityUserRole<string>
    {
        public User User { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}
