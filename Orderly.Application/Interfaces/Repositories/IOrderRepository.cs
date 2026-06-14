using Orderly.Domain.Entities;

namespace Orderly.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order, CancellationToken cancellationToken);

        Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);

        Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Order?> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken);
    }
}
