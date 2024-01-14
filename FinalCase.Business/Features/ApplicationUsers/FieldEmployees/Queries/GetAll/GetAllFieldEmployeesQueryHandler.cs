using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.FieldEmployee.Queries.GetAll;

/*
public class GetAllFieldEmployeesQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
        : IRequestHandler<GetAllFieldEmployeesQuery, ApiResponse<IEnumerable<FieldEmployeeResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // DI with Primary Constructor C# 12

    public async Task<ApiResponse<IEnumerable<FieldEmployeeResponse>>> Handle(GetAllFieldEmployeesQuery request, CancellationToken cancellationToken)
    {
        
        var fieldEmployees = await dbContext.FieldEmployees
            .Include(x => x.Expenses)
            .Include(x => x.Payments)
            .AsNoTrackingWithIdentityResolution() // since the operation is readonly,
                                                  // tracking was disabled to increase performance            
            .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<FieldEmployeeResponse>>(fieldEmployees);

        return new ApiResponse<IEnumerable<FieldEmployeeResponse>>(response);
        

        return null;
    }
}
*/