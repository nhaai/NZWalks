﻿using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Models.Domain
{
    public class User : IdentityUser
    {
        public string? Title { get; set; }
        public string? DisplayName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsActive { get; set; }
        public string? Notes { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}