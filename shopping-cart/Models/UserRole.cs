using Microsoft.AspNetCore.Identity;

namespace SA51_CA_Project_Team10.Models
{
    public class UserRole: IdentityUserRole<string>
    {
        public User User { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}
