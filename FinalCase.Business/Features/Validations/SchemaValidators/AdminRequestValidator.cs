using FinalCase.Schema.AppRoles.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class AdminRequestValidator : AbstractValidator<AdminRequest>
{
    public AdminRequestValidator()
    {
        // Since Admin currently inherits from ApplicationUser without additional properties,
        // only ApplicationUserValidator will be used for now. If any properties are added to Admin in the future,
        // Validations for AdminRequest can be included here.
    }
}
