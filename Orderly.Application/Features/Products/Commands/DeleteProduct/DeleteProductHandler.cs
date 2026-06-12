using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdForUpdateAsync(request.Id, cancellationToken);

            if (product is null)
            {
                throw new NotFoundException($"Product with ID {request.Id} not found.");
            }

            product.IsDeleted = true;
            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
