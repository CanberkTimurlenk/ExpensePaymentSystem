using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Payments.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Payments.Queries.GetById;

public class GetPaymentByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentResponse = await dbContext.Payments
            .AsNoTracking()
            .ProjectTo<PaymentResponse>(mapper.ConfigurationProvider)  // ProjectTo is an extension method from AutoMapper.QueryableExtensions
                                                                       // that allows to project the query results directly to the desired type.
                                                                       // Unnecessary properties are not loaded.
            .FirstOrDefaultAsync(p => p.EmployeeId.Equals(request.EmployeeId)
                    && p.ExpenseId.Equals(request.ExpenseId), cancellationToken);

        if (paymentResponse == null)
            return new ApiResponse<PaymentResponse>(PaymentMessages.PaymentNotFound);

        return new ApiResponse<PaymentResponse>(paymentResponse);
    }
}
