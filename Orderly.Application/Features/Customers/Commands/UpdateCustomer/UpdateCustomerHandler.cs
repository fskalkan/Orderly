using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdForUpdateAsync(request.Id, cancellationToken);

            if (customer is null)
            {
                throw new NotFoundException($"Customer with Id {request.Id} not found.");
            }

            var emailExists = await _customerRepository.EmailExistsForAnotherCustomerAsync(request.Email, request.Id, cancellationToken);

            if (emailExists)
            {
                throw new BadRequestException("Customer email already exists.");
            }

            customer.FullName = request.FullName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateCustomerResponse
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }
    }
}
