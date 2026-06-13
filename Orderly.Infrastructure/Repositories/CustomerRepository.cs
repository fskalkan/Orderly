using Microsoft.EntityFrameworkCore;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Domain.Entities;
using Orderly.Infrastructure.Data;

namespace Orderly.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
            return customer;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .AnyAsync(c => c.Email == email && !c.IsDeleted, cancellationToken);
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<Customer?> GetByIdForUpdateAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .Where(c => !c.IsDeleted)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<bool> EmailExistsForAnotherCustomerAsync(string email, int customerId, CancellationToken cancellationToken)
        {
            return await _dbContext.Customers
                .AnyAsync(c => c.Email == email && c.Id != customerId && !c.IsDeleted, cancellationToken);
        }
    }
}
