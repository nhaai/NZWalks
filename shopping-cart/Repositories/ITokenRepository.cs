using Microsoft.AspNetCore.Identity;
using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(User user, List<string> roles);
    }
}
