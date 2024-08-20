using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsActive { get; set; }
        public string? Notes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = default!;
    }
}
