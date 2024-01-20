using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Payments.Commands.Create;
public class CreatePaymentCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = mapper.Map<Payment>(request.Model);

        payment.PaymentMethodName = await dbContext.PaymentMethods
            .Where(x => x.Id == payment.PaymentMethodId)
            .Select(x => x.Name)
            .FirstOrDefaultAsync(cancellationToken);

        await dbContext.AddAsync(payment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<PaymentResponse>(payment);

        return new ApiResponse<PaymentResponse>(response);
    }
}
