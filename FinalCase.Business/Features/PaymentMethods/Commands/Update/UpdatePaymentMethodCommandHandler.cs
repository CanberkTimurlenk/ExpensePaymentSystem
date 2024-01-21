using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Update;
public class UpdatePaymentMethodCommandHandler(FinalCaseDbContext dbContext)
    : IRequestHandler<UpdatePaymentMethodCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await dbContext.FindAsync<PaymentMethod>(request.Id, cancellationToken);

        if (paymentMethod is null)
            return new ApiResponse(PaymentMethodMessages.PaymentMethodNotFound);

        request.Model.Id = paymentMethod.Id;
        request.Model.Name = paymentMethod.Name;
        request.Model.Description = paymentMethod.Description;                

        paymentMethod.UpdateDate = DateTime.Now;
        paymentMethod.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
