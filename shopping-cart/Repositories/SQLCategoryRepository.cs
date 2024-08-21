using Microsoft.EntityFrameworkCore;
using SA51_CA_Project_Team10;
using SA51_CA_Project_Team10.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLCategoryRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Category>, int)> GetAllAsync(SearchFilter searchFilter)
        {
            var categories = dbContext.Categories.AsQueryable();
            categories = categories.Where(x => x.IsActive == true);

            // filtering
            if (!string.IsNullOrWhiteSpace(searchFilter.FilterOn) && !string.IsNullOrWhiteSpace(searchFilter.FilterQuery))
            {
                if (searchFilter.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = categories.Where(x => x.Name.Contains(searchFilter.FilterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(searchFilter.SortBy))
            {
                if (searchFilter.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = searchFilter.IsAccending == true
                        ? categories.OrderBy(x => x.Name)
                        : categories.OrderByDescending(x => x.Name);
                }
            }

            var totalItemCount = await categories.CountAsync();

            // pagination
            if (searchFilter.PageNumber != null)
            {
                var skipResults = ((int)searchFilter.PageNumber - 1) * searchFilter.PageSize;
                categories = categories.Skip(skipResults).Take(searchFilter.PageSize);
            }

            return (await categories.ToListAsync(), totalItemCount);
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.IsActive = category.IsActive;
            existingCategory.ParentId = category.ParentId;

            await dbContext.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var existingCategory = await dbContext.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.IsActive = false;
            // dbContext.Categories.Remove(existingCategory);
            await dbContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
