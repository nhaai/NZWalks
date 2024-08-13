using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Models.Domain
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
