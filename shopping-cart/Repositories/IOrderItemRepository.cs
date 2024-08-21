

using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetByIdAsync(int id);
        Task<OrderItem> CreateAsync(OrderItem orderItem);
        Task<OrderItem?> UpdateAsync(int id, OrderItem orderItem);
        Task<OrderItem?> DeleteAsync(int id);
    }
}
