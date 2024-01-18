using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static FinalCase.Base.Helpers.Linq.ExpressionStarterExtensions;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;


public class GetAllApplicationUsersHandler(FinalCaseDbContext dbContext, IMapper mapper)
        : IRequestHandler<GetAllApplicationUsersQuery, ApiResponse<IEnumerable<ApplicationUserResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // DI with Primary Constructor C# 12

    public async Task<ApiResponse<IEnumerable<ApplicationUserResponse>>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
    {

        var applicationUsersQuery = dbContext.ApplicationUsers
            .Include(x => x.CreatedExpenses)
            .AsNoTracking();

        if (request.IncludeDeleted == true) // since it is nullable, we need to check if it is true
            applicationUsersQuery = applicationUsersQuery.IgnoreQueryFilters();

        var applicationUsers = await applicationUsersQuery.ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ApplicationUserResponse>>(applicationUsers);
        return new ApiResponse<IEnumerable<ApplicationUserResponse>>(response);
    }
}
