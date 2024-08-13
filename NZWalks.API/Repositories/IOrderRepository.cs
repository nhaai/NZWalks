using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(int id, Order order);
        Task<Order?> DeleteAsync(int id);
    }
}
