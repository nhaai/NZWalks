using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<(List<Category>, int)> GetAllAsync(SearchFilter searchData);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(int id, Category category);
        Task<Category?> DeleteAsync(int id);
    }
}
