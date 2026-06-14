using Orderly.Domain.Entities;

namespace Orderly.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken);
    }
}
