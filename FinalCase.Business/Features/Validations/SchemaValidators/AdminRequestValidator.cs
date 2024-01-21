﻿using FinalCase.Schema.AppRoles.Requests;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;
public class AdminRequestValidator : AbstractValidator<AdminRequest>
{
    public AdminRequestValidator()
    {
        RuleFor(x => x.Firstname)
             .NotEmpty().WithMessage("Firstname is required.")
             .Length(3, 50).WithMessage("Firstname should be between 3 and 50 characters.");

        RuleFor(x => x.Lastname)
            .NotEmpty().WithMessage("Lastname is required.")
            .Length(3, 50).WithMessage("Lastname should be between 3 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(50).WithMessage("Email should be maximum 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(Regex.PasswordRegex).WithMessage(" Minimum eight characters, at least one upper case English letter, " +
            "one lower case English letter, one number and one special character ");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now).WithMessage("Date of birth should be in the past.");
    }
}
