using Orderly.Domain.Entities;

namespace Orderly.Application.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken);

        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken);

        Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);

        Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Customer?> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken);

        Task<bool> EmailExistsForAnotherCustomerAsync(string email, int customerId, CancellationToken cancellationToken);
    }
}
