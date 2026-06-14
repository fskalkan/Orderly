using MediatR;

namespace Orderly.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemCommand> Items { get; set; } = new();
    }
}