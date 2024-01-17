using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.PaymentMethods.Commands.Create;
public class UpdatePaymentMethodCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreatePaymentMethodCommand, ApiResponse<PaymentMethodResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = mapper.Map<PaymentMethod>(request);

        await dbContext.PaymentMethods.AddAsync(paymentMethod, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<PaymentMethodResponse>(paymentMethod);
        return new ApiResponse<PaymentMethodResponse>(response);
    }
}
