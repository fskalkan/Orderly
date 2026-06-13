using FluentValidation;

namespace Orderly.Application.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Customer full name is required.")
                .MinimumLength(3)
                .WithMessage("Customer full name must be at least 3 characters.")
                .MaximumLength(100)
                .WithMessage("Customer full name must not exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Email format is invalid.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain only numbers.")
                .MinimumLength(10)
                .WithMessage("Phone number must be at least 10 characters.")
                .MaximumLength(20)
                .WithMessage("Phone number must not exceed 20 characters.");
        }
    }
}