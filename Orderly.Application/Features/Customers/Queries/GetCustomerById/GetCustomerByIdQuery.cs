using MediatR;

namespace Orderly.Application.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdResponse>
    {
        public int Id { get; set; }
    }
}
