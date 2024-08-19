using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
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
