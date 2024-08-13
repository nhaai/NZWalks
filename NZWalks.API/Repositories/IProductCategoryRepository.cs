using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetAllAsync();
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<ProductCategory> CreateAsync(ProductCategory productCategory);
        Task<ProductCategory?> UpdateAsync(int id, ProductCategory productCategory);
        Task<ProductCategory?> DeleteAsync(int id);
    }
}
