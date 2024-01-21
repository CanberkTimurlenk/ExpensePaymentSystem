using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Delete;
public class DeletePaymentMethodCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<DeletePaymentMethodCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await dbContext.FindAsync<PaymentMethod>(request.Id, cancellationToken);

        if (paymentMethod?.IsActive != true)
            return new ApiResponse(PaymentMethodMessages.PaymentMethodNotFound);

        if (dbContext.Expenses.Any(e => e.PaymentMethodId == paymentMethod.Id))
            return new ApiResponse(PaymentMethodMessages.PaymentMethodExpenseDeleteRestricted);

        paymentMethod.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
