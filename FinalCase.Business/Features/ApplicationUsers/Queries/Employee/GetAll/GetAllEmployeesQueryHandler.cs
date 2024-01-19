using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Data.Contexts;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
public class GetAllEmployeesQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
        : IRequestHandler<GetAllEmployeesQuery, ApiResponse<IEnumerable<EmployeeResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // DI with Primary Constructor C# 12

    public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.ApplicationUsers
             .Where(a => a.Role.Equals(Roles.Employee))
             .AsNoTracking();

        if (request.IncludeDeleted)
            query.IgnoreQueryFilters();

        var applicationUsers = await query.ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<EmployeeResponse>>(applicationUsers);
        return new ApiResponse<IEnumerable<EmployeeResponse>>(response);
    }
}
