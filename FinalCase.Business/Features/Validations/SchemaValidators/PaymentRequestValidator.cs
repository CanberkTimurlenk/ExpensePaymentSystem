using FinalCase.Schema.Entity.Responses;
using FluentValidation;

namespace FinalCase.Business.Features.Validations.SchemaValidators;

public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount should be positive.")
            .NotEmpty().WithMessage("Amount is required.");

        RuleFor(x => x.ReceiverIban)
            .NotEmpty().WithMessage("Receiver IBAN is required.")
            .Length(1, 34).WithMessage("Receiver IBAN should be between 1 and 34 characters.");

        RuleFor(x => x.ReceiverName)
            .NotEmpty().WithMessage("Receiver name is required.")
            .Length(1, 100).WithMessage("Receiver name should be between 1 and 100 characters.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date should not be in the future.")
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.ReceiverIban)
            .NotEmpty().WithMessage("IBAN is required.")
            .Length(26).WithMessage("IBAN must be 26 characters.")
            .Matches("^TR").WithMessage("Iban must be starts with 'TR' ");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID is required.");

        RuleFor(x => x.ExpenseId)
            .NotEmpty().WithMessage("Expense ID is required.");

        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage("Payment method ID is required.");
    }
}
