using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLProductCategoryRepository : IProductCategoryRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLProductCategoryRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await dbContext.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetByIdAsync(int id)
        {
            return await dbContext.ProductCategories.FindAsync(id);
        }

        public async Task<ProductCategory> CreateAsync(ProductCategory productCategory)
        {
            await dbContext.ProductCategories.AddAsync(productCategory);
            await dbContext.SaveChangesAsync();

            return productCategory;
        }

        public async Task<ProductCategory?> UpdateAsync(int id, ProductCategory productCategory)
        {
            var existingProductCategory = await dbContext.ProductCategories.FindAsync(id);

            if (existingProductCategory == null)
            {
                return null;
            }

            existingProductCategory.ProductId = productCategory.ProductId;
            existingProductCategory.CategoryId = productCategory.CategoryId;

            await dbContext.SaveChangesAsync();

            return existingProductCategory;
        }

        public async Task<ProductCategory?> DeleteAsync(int id)
        {
            var existingProductCategory = await dbContext.ProductCategories.FindAsync(id);

            if (existingProductCategory == null)
            {
                return null;
            }

            dbContext.ProductCategories.Remove(existingProductCategory);
            await dbContext.SaveChangesAsync();

            return existingProductCategory;
        }
    }
}
