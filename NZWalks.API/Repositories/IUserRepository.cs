using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<(List<User>, int)> GetAllAsync(SearchFilter searchData);
        Task<User?> GetByIdAsync(string id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(string id, User user);
        Task<User?> DeleteAsync(string id);
    }
}
