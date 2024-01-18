﻿using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Update;
public class UpdatePaymentCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<UpdatePaymentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await dbContext.FindAsync<Payment>(request.Id, cancellationToken);

        if (payment == null)
            return new ApiResponse(PaymentMessages.PaymentNotFound);

        payment.Amount = request.Model.Amount;
        payment.Description = request.Model.PaymentDescription;
        payment.Date = request.Model.Date;
        payment.ReceiverIban = request.Model.ReceiverIban;
        payment.ReceiverName = request.Model.ReceiverName;
        payment.Date = request.Model.Date;
        payment.EmployeeId = request.Model.EmployeeId;
        payment.ExpenseId = request.Model.ExpenseId;
        payment.PaymentMethodId = request.Model.PaymentMethodId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
