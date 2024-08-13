using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetAllAsync();
        Task<CartItem?> GetByIdAsync(int id);
        Task<CartItem> CreateAsync(CartItem cartItem);
        Task<CartItem?> UpdateAsync(int id, CartItem cartItem);
        Task<CartItem?> DeleteAsync(int id);
    }
}
