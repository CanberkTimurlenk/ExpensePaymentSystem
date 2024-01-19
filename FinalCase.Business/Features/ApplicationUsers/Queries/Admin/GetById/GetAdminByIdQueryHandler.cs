using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Constants;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Responses;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
public class GetAdminByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAdminByIdQuery, ApiResponse<AdminResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // C# 12 Introduces primary constructor which also could used for dependency injection

    public async Task<ApiResponse<AdminResponse>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<ApplicationUser>();

        predicate = predicate.And(u => u.Id.Equals(request.Id));
        predicate = predicate.And(u => u.Role.Equals(Roles.Admin));

        var query = dbContext.ApplicationUsers
            .AsNoTracking();

        if (request.IncludeDeleted)
            query = query.IgnoreQueryFilters();

        var user = await query.SingleOrDefaultAsync(predicate, cancellationToken);

        if (user is null)
            return new ApiResponse<AdminResponse>(ApplicationUserMessages.AdminNotFound);

        var response = mapper.Map<AdminResponse>(user);
        return new ApiResponse<AdminResponse>(response);
    }
}