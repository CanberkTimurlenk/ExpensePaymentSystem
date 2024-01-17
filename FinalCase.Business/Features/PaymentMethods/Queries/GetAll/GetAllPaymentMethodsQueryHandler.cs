using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetAll;
public class GetAllPaymentMethodsQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllPaymentMethodsQuery, ApiResponse<IEnumerable<PaymentMethodResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<PaymentMethodResponse>>> Handle(GetAllPaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        var paymentMethods = await dbContext.PaymentMethods
                .AsNoTracking() // disable tracking
                .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<PaymentMethodResponse>>(paymentMethods);
        return new ApiResponse<IEnumerable<PaymentMethodResponse>>(response);
    }
}
