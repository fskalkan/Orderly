using FluentValidation;
using Orderly.Domain.Enums;

namespace Orderly.Application.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Order id must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Order status is invalid.")
                .Must(status => status != OrderStatus.Pending)
                .WithMessage("Order status can only be changed to Completed or Cancelled.");
        }
    }
}