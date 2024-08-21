using Microsoft.EntityFrameworkCore;
using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLCartRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await dbContext.Carts.ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await dbContext.Carts.FindAsync(id);
        }

        public async Task<Cart> CreateAsync(Cart Cart)
        {
            await dbContext.Carts.AddAsync(Cart);
            await dbContext.SaveChangesAsync();

            return Cart;
        }

        public async Task<Cart?> UpdateAsync(int id, Cart Cart)
        {
            var existingCart = await dbContext.Carts.FindAsync(id);

            if (existingCart == null)
            {
                return null;
            }

            existingCart.GrandTotal = Cart.GrandTotal;

            await dbContext.SaveChangesAsync();

            return existingCart;
        }

        public async Task<Cart?> DeleteAsync(int id)
        {
            var existingCart = await dbContext.Carts.FindAsync(id);

            if (existingCart == null)
            {
                return null;
            }

            dbContext.Carts.Remove(existingCart);
            await dbContext.SaveChangesAsync();

            return existingCart;
        }
    }
}
