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
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date should not be in the future.");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(150).WithMessage("Location should be maximum 150 characters.");

        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage("Payment method is required.");
       
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(x => x.Documents)
             .NotEmpty().WithMessage("Category is required.");

        RuleForEach(x => x.Documents).ChildRules(d =>
        {
            d.RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name should be between 1 and 100 characters.");

            d.RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(250).WithMessage("Description should be maximum 250 characters.");

            d.RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Length(10, 150).WithMessage("Url should be between 10 and 150 characters.");
        });

    }
}
