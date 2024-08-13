using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLCategoryRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20)
        {
            var categories = dbContext.Categories.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = categories.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = isAccending ? categories.OrderBy(x => x.Name) : categories.OrderByDescending(x => x.Name);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            categories = categories.Skip(skipResults).Take(pageSize);

            return await categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.ParentId = category.ParentId;

            await dbContext.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
