using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
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
