using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(int id, Category category);
        Task<Category?> DeleteAsync(int id);
    }
}
