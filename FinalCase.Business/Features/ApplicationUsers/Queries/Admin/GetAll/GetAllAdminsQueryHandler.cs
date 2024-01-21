using AutoMapper;
using FinalCase.Base.Helpers.Linq;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
public class GetAllAdminsQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
        : IRequestHandler<GetAllAdminsQuery, ApiResponse<IEnumerable<AdminResponse>>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // DI with Primary Constructor C# 12

    public async Task<ApiResponse<IEnumerable<AdminResponse>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
    {
        var applicationUsers = await dbContext.ApplicationUsers
            .Where(a => a.Role.Equals(Roles.Admin))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<AdminResponse>>(applicationUsers);
        return new ApiResponse<IEnumerable<AdminResponse>>(response);
    }
}
