using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Update;
public class UpdatePaymentMethodCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdatePaymentMethodCommand, ApiResponse>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await dbContext.FindAsync<PaymentMethod>(request.Id, cancellationToken);

        if (paymentMethod == null)
            return new ApiResponse(PaymentMethodMessages.PaymentMethodNotFound);

        request.Model.Id = paymentMethod.Id;
        mapper.Map(request.Model, paymentMethod);
        // Model includes two properties, mapper maps all of them (name,desc)

        paymentMethod.UpdateDate = DateTime.Now;
        paymentMethod.UpdateUserId = request.UpdaterId;

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}
