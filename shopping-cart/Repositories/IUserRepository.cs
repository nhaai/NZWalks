using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
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
