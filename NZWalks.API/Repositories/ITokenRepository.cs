using Microsoft.AspNetCore.Identity;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(User user, List<string> roles);
    }
}
