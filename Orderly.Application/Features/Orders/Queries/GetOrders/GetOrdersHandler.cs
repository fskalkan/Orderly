using MediatR;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<GetOrdersResponse>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<GetOrdersResponse>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(cancellationToken);

            return orders.Select(o => new GetOrdersResponse
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.FullName,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                OrderDate = o.OrderDate,
                Items = o.OrderItems.Select(oi => new GetOrdersItemResponse
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            }).ToList();
        }
    }
}