using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetById;
public class GetPaymentMethodByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetPaymentMethodByIdQuery, ApiResponse<PaymentMethodResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await dbContext.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(request.Id), cancellationToken);

        var response = mapper.Map<PaymentMethodResponse>(paymentMethod);

        return new ApiResponse<PaymentMethodResponse>(response);

    }
}
