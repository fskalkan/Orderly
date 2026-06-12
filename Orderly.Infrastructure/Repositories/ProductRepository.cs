using Microsoft.EntityFrameworkCore;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Domain.Entities;
using Orderly.Infrastructure.Data;

namespace Orderly.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(product, cancellationToken);
            return product;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
