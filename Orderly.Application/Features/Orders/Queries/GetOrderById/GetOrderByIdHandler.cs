using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces.Repositories;

namespace Orderly.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderByIdResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.Id} not found.");
            }

            return new GetOrderByIdResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer.FullName,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                OrderDate = order.OrderDate,
                Items = order.OrderItems.Select(oi => new GetOrderByIdItemResponse
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice
                }).ToList()
            };
        }
    }
}