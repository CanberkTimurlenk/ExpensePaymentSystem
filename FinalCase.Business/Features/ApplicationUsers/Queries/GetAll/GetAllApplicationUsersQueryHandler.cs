using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;


public class GetAllApplicationUsersHandler(FinalCaseDbContext dbContext, IMapper mapper)
        : IRequestHandler<GetAllApplicationUsersQuery, ApiResponse<IEnumerable<ApplicationUserResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // DI with Primary Constructor C# 12

    public async Task<ApiResponse<IEnumerable<ApplicationUserResponse>>> Handle(GetAllApplicationUsersQuery request, CancellationToken cancellationToken)
    {
        var applicationUsers = await dbContext.ApplicationUsers
            .Include(x => x.CreatedExpenses)
            .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<ApplicationUserResponse>>(applicationUsers);
        return new ApiResponse<IEnumerable<ApplicationUserResponse>>(response);

    }
}
