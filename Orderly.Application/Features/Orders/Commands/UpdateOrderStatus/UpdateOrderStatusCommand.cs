using MediatR;
using Orderly.Domain.Enums;

namespace Orderly.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest<UpdateOrderStatusResponse>
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }
    }
}