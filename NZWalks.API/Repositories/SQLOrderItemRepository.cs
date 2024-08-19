using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLOrderItemRepository : IOrderItemRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLOrderItemRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await dbContext.OrderItems.ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await dbContext.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> CreateAsync(OrderItem orderItem)
        {
            await dbContext.OrderItems.AddAsync(orderItem);
            await dbContext.SaveChangesAsync();

            return orderItem;
        }

        public async Task<OrderItem?> UpdateAsync(int id, OrderItem orderItem)
        {
            var existingOrderItem = await dbContext.OrderItems.FindAsync(id);

            if (existingOrderItem == null)
            {
                return null;
            }

            existingOrderItem.IsAvailable = orderItem.IsAvailable;
            existingOrderItem.ProductCount = orderItem.ProductCount;
            existingOrderItem.BuyingPrice = orderItem.BuyingPrice;
            existingOrderItem.Total = orderItem.Total;
            existingOrderItem.ProductId = orderItem.ProductId;

            await dbContext.SaveChangesAsync();

            return existingOrderItem;
        }

        public async Task<OrderItem?> DeleteAsync(int id)
        {
            var existingOrderItem = await dbContext.OrderItems.FindAsync(id);

            if (existingOrderItem == null)
            {
                return null;
            }

            dbContext.OrderItems.Remove(existingOrderItem);
            await dbContext.SaveChangesAsync();

            return existingOrderItem;
        }
    }
}
