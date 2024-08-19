using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
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
