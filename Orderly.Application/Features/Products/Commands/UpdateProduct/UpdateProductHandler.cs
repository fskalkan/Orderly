using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdForUpdateAsync(request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException($"Product with Id {request.Id} not found.");
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.IsActive = request.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                StockQuantity = product.StockQuantity,
                Price = product.Price,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
        }    
    }
}
