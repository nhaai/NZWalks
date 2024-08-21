

using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface IProductRepository
    {
        Task<(List<Product>, int)> GetAllAsync(SearchFilter searchData);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
    }
}
