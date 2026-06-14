using MediatR;
using Orderly.Application.Common.Exceptions;
using Orderly.Application.Interfaces;
using Orderly.Application.Interfaces.Repositories;
using Orderly.Domain.Enums;

namespace Orderly.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, UpdateOrderStatusResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateOrderStatusResponse> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdForUpdateAsync(request.Id, cancellationToken);

            if (order is null)
            {
                throw new NotFoundException($"Order with ID {request.Id} not found.");
            }

            if (order.Status != OrderStatus.Pending)
            {
                throw new BadRequestException("Only pending orders can be updated.");
            }

            order.Status = request.Status;
            order.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateOrderStatusResponse
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                UpdatedAt = order.UpdatedAt
            };
        }
    }
}