using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ExpenseCategories.Constants;
using FinalCase.Business.Features.PaymentMethods.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace FinalCase.Business.Features.PaymentMethods.Queries.GetById;
public class GetPaymentMethodByIdQueryHandler(FinalCaseDbContext dbContext, IDistributedCache cache, IMapper mapper)
    : IRequestHandler<GetPaymentMethodByIdQuery, ApiResponse<PaymentMethodResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    private readonly IDistributedCache cache = cache;

    public async Task<ApiResponse<PaymentMethodResponse>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var byteArr = await cache.GetAsync(PaymentMethodCacheKeys.AllPaymentMethods, cancellationToken);

        PaymentMethodResponse? response;

        if (byteArr != null)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiResponse<IEnumerable<PaymentMethodResponse>>>
                (Encoding.UTF8.GetString(byteArr));

            response = apiResponse?.Response.FirstOrDefault(ec => ec.Id.Equals(request.Id));
            return new ApiResponse<PaymentMethodResponse>(response!);
        }

        var paymentMethod = await dbContext.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(request.Id), cancellationToken);

        response = mapper.Map<PaymentMethodResponse>(paymentMethod);

        return new ApiResponse<PaymentMethodResponse>(response);

    }
}
