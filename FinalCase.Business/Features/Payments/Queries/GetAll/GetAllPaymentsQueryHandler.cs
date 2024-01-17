using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Queries.GetAll;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Payments.Queries;
public class GetAllPaymentsQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllPaymentsQuery, ApiResponse<IEnumerable<PaymentResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<IEnumerable<PaymentResponse>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var paymentsResponse = await dbContext.Payments
            .AsNoTrackingWithIdentityResolution()
            .ProjectTo<PaymentResponse>(mapper.ConfigurationProvider) // ProjectTo is an extension method - AutoMapper.QueryableExtensions
            // Unnecessary properties are not loaded.
            .ToListAsync(cancellationToken);

        return new ApiResponse<IEnumerable<PaymentResponse>>(paymentsResponse);
    }
}
