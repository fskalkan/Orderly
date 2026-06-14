using FluentValidation;

namespace Orderly.Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Order id must be greater than 0.");
        }
    }
}