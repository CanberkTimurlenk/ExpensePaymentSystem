using AutoMapper;
using FinalCase.Base.Helpers.Encryption;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;

public class CreateAdminCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateAdminCommand, ApiResponse<AdminResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<AdminResponse>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<ApplicationUser>(request.Model);
        user.Password = Md5Extension.GetHash(request.Model.Password);
        user.Role = Roles.Admin;
        user.InsertDate = DateTime.Now;
        user.InsertUserId = request.InsertUserId;
        user.IsActive = true;

        await dbContext.ApplicationUsers.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AdminResponse>(user);
        return new ApiResponse<AdminResponse>(response);
    }
}
