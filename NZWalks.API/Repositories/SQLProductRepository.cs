using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLProductRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20)
        {
            var products = dbContext.Products.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Sku", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => (x.Sku ?? "").Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAccending ? products.OrderBy(x => x.Name) : products.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAccending ? products.OrderBy(x => x.CreatedAt) : products.OrderByDescending(x => x.CreatedAt);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            products = products.Skip(skipResults).Take(pageSize);

            return await products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product product)
        {
            var existingProduct = await dbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Sku = product.Sku;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.UpdatedAt = product.UpdatedAt;

            await dbContext.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var existingProduct = await dbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return null;
            }

            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
