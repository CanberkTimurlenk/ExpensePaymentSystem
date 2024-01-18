using AutoMapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
public class GetUserByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetApplicationUserByIdQuery, ApiResponse<ApplicationUserResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;
    // C# 12 Introduces primary constructor which also could used for dependency injection

    public async Task<ApiResponse<ApplicationUserResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.ApplicationUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(fe => fe.Id.Equals(request.Id), cancellationToken);

        if (user is null)
            return new ApiResponse<ApplicationUserResponse>(ApplicationUserMessages.UserNotFound);

        var response = mapper.Map<ApplicationUserResponse>(user);

        return new ApiResponse<ApplicationUserResponse>(response);
    }
}