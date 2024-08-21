using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
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
