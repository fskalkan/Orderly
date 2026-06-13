using MediatR;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<GetCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<GetCustomersResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync(cancellationToken);

            return customers.Select(c => new GetCustomersResponse
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }
    }
}
