using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20);
        Task<Product?> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, Product product);
        Task<Product?> DeleteAsync(int id);
    }
}
