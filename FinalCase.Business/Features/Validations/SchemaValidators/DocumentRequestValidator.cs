using FinalCase.Schema.Entity.Requests;
using FluentValidation;
using static System.Net.WebRequestMethods;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
{
    public DocumentRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 100).WithMessage("Name should be between 1 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(250).WithMessage("Description should be maximum 250 characters.");

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required.")
            //.Matches(validUrlRegex).WithMessage("Url is not valid.") 
            // Commented out to simplify easy request testing
            .Length(10, 150).WithMessage("Url should be between 10 and 150 characters.");

        RuleFor(x => x.ExpenseId)
            .NotEmpty().WithMessage("ExpenseId is required.");
    }
}
