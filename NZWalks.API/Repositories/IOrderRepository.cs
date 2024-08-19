using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IOrderRepository
    {
        Task<(List<Order>, int)> GetAllAsync(SearchFilter searchData);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(int id, Order order);
        Task<Order?> DeleteAsync(int id);
    }
}
