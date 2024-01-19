using FinalCase.Schema.Entity.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class PaymentMethodRequestValidator : AbstractValidator<PaymentMethodRequest>
{
    public PaymentMethodRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 50).WithMessage("Name should be between 1 and 50 characters.")
            .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name should only contain alphanumeric characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(150).WithMessage("Description should be maximum 150 characters.");
    }
}
