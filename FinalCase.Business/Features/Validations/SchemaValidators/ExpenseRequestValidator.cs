using FinalCase.Schema.Entity.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseRequestValidator()
    {
        RuleFor(x => x.EmployeeDescription)
        .MaximumLength(150).WithMessage("Employee description should be maximum 150 characters.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount should be greater than 0.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date should no be in the future.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(150).WithMessage("Location should be maximum 150 characters.");

        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage("Payment method is required.");

        RuleFor(x => x.CreatorEmployeeId)
            .NotEmpty().WithMessage("Creator employee is required.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required.");
        
        RuleForEach(x => x.Documents)
            .SetValidator(new DocumentRequestValidator());

    }
}
