using MediatR;

namespace Orderly.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<GetOrdersResponse>>
    {
    }
}