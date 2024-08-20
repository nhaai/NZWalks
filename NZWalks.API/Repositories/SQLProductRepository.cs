using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLProductRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Product>, int)> GetAllAsync(SearchFilter searchFilter)
        {
            var products = dbContext.Products.AsQueryable();
            products = products.Where(x => x.IsActive == true);

            // filtering
            if (!string.IsNullOrWhiteSpace(searchFilter.FilterOn) && !string.IsNullOrWhiteSpace(searchFilter.FilterQuery))
            {
                if (searchFilter.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => x.Name.Contains(searchFilter.FilterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(searchFilter.SortBy))
            {
                if (searchFilter.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = searchFilter.IsAccending == true
                        ? products.OrderBy(x => x.Name)
                        : products.OrderByDescending(x => x.Name);
                }
            }

            var totalItemCount = await products.CountAsync();

            // pagination
            if (searchFilter.PageNumber != null)
            {
                var skipResults = ((int)searchFilter.PageNumber - 1) * searchFilter.PageSize;
                products = products.Skip(skipResults).Take(searchFilter.PageSize);
            }

            return (await products.ToListAsync(), totalItemCount);
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products.FindAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            product.Purchases = 0;
            product.Views = 0;
            product.CreatedAt = DateTime.Now;

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
            existingProduct.Code = product.Code;
            existingProduct.Description = product.Description;
            existingProduct.Brand = product.Brand;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Purchases = product.Purchases;
            existingProduct.Views = product.Views;
            existingProduct.ImageUrl = product.ImageUrl;
            existingProduct.IsActive = product.IsActive;
            existingProduct.UpdatedAt = DateTime.Now;
            existingProduct.CategoryId = product.CategoryId;

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

            existingProduct.IsActive = false;
            // dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }
    }
}
