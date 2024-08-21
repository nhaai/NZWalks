

using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart?> UpdateAsync(int id, Cart cart);
        Task<Cart?> DeleteAsync(int id);
    }
}
