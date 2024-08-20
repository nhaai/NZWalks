using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
    }
}
