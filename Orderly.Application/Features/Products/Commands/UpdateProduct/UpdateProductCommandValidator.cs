using FluentValidation;

namespace Orderly.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Product id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MinimumLength(3)
                .WithMessage("Product name must be at least 3 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stock quantity cannot be negative.");
        }
    }
}