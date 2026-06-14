using Orderly.Domain.Entities;

namespace Orderly.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Product?> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken);
        Task<List<Product>> GetByIdsForUpdateAsync(List<int> productIds, CancellationToken cancellationToken);
    }
}
