using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLCartItemRepository : ICartItemRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLCartItemRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CartItem>> GetAllAsync()
        {
            return await dbContext.CartItems.ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await dbContext.CartItems.FindAsync(id);
        }

        public async Task<CartItem> CreateAsync(CartItem cartItem)
        {
            await dbContext.CartItems.AddAsync(cartItem);
            await dbContext.SaveChangesAsync();

            return cartItem;
        }

        public async Task<CartItem?> UpdateAsync(int id, CartItem cartItem)
        {
            var existingCartItem = await dbContext.CartItems.FindAsync(id);

            if (existingCartItem == null)
            {
                return null;
            }

            existingCartItem.SessionId = cartItem.SessionId;
            existingCartItem.ProductId = cartItem.ProductId;
            existingCartItem.Quantity = cartItem.Quantity;
            existingCartItem.UpdatedAt = cartItem.UpdatedAt;

            await dbContext.SaveChangesAsync();

            return existingCartItem;
        }

        public async Task<CartItem?> DeleteAsync(int id)
        {
            var existingCartItem = await dbContext.CartItems.FindAsync(id);

            if (existingCartItem == null)
            {
                return null;
            }

            dbContext.CartItems.Remove(existingCartItem);
            await dbContext.SaveChangesAsync();

            return existingCartItem;
        }
    }
}
