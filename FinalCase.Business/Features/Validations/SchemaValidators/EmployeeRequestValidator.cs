using FinalCase.Schema.AppRoles.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
{
    const string ibanRegex = @"^([A-Z]{2}[ '+'\\\\'+'-]?[0-9]{2})(?=(?:[ '+'\\\\'+'-]?[A-Z0-9]){9,30}\$)((?:[ '+'\\\\'+'-]?[A-Z0-9]{3,5}){2,7})([ '+'\\\\'+'-]?[A-Z0-9]{1,3})?\$";
    public EmployeeRequestValidator()
    {
        RuleFor(x => x.Iban)
            .NotEmpty().WithMessage("IBAN is required.")
            .Matches(ibanRegex).WithMessage("Invalid IBAN format.");
    }
}
