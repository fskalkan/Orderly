using MediatR;

namespace Orderly.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<GetOrderByIdResponse>
    {
        public int Id { get; set; }
    }
}
