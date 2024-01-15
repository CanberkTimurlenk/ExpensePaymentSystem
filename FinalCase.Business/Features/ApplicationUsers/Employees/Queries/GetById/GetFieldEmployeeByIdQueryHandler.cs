using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.FieldEmployee.Constans;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.FieldEmployee.Queries.GetById;

/*
public class GetFieldEmployeeByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetFieldEmployeeByIdQuery, ApiResponse<FieldEmployeeResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // C# 12 Introduces primary constructor which also could used for dependency injection

    public async Task<ApiResponse<FieldEmployeeResponse>> Handle(GetFieldEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var fieldEmployee = await dbContext.ApplicationUsers

            .Include(x => x.Expenses)
            .Include(x => x.Payments)
            .AsNoTrackingWithIdentityResolution() // since the operation is readonly,
                                                  // tracking was disabled to increase performance            
            .FirstOrDefaultAsync(fe => fe.Id.Equals(request.Id), cancellationToken);

        if (fieldEmployee is null)
            return new ApiResponse<FieldEmployeeResponse>(FieldEmployeeMessages.RecordNotExists);

        var response = mapper.Map<FieldEmployeeResponse>(fieldEmployee);

        return new ApiResponse<FieldEmployeeResponse>(response);
    }
}
*/