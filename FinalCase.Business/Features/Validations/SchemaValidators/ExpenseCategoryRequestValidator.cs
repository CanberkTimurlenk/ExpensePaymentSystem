using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class ExpenseCategoryRequestValidator : AbstractValidator<ExpenseCategoryRequest>
{
    public ExpenseCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Name is required.")
        .Length(3, 50).WithMessage("Name should be between 1 and 50 characters.")
        .Matches("^[a-zA-Z0-9 ]*$").WithMessage("Name should contain only letters and numbers.");

        RuleFor(x => x.Description)
            .MaximumLength(150).WithMessage("Description should be maximum 150 characters.");
    }
}
