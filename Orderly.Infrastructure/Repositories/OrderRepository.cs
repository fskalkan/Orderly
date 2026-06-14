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
    }
}
