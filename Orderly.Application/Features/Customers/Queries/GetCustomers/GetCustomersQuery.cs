using MediatR;

namespace Orderly.Application.Features.Customers.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<List<GetCustomersResponse>>
    {
    }
}
