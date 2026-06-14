using Microsoft.EntityFrameworkCore;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Domain.Entities;
using Orderly.Infrastructure.Data;

namespace Orderly.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            return order;
        }

        public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(o => !o.IsDeleted)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync(cancellationToken);
        }
        public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(o => !o.IsDeleted)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }
    }
}