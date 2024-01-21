using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.Payments.Commands.Delete;
public class DeletePaymentCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<DeletePaymentCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await dbContext
            .FindAsync<Payment>(request.id);

        if (payment == null)
            return new ApiResponse(PaymentMessages.PaymentNotFound);

        //payment.IsDeleted = true;
        // since payment is a reporting table and represents completed payment, there is no soft/hard delete operation exists
        // this method implemented to show how to implement soft delete operation

        await dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}
