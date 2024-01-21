using AutoMapper;
using FinalCase.Base.Helpers.Encryption;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Data.Contexts;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;

public class CreateEmployeeCommandHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeResponse>>
{
    private readonly FinalCaseDbContext dbContext = dbContext;
    private readonly IMapper mapper = mapper;

    public async Task<ApiResponse<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // more logic for creating an employee could be added here

        var user = mapper.Map<ApplicationUser>(request.Model);
        user.Password = Md5Extension.GetHash(request.Model.Password);
        user.Role = Roles.Employee;
        user.InsertDate = DateTime.Now;
        user.InsertUserId = request.InsertUserId;
        user.IsActive = true;

        await dbContext.ApplicationUsers.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<EmployeeResponse>(user);
        return new ApiResponse<EmployeeResponse>(response);
    }
}
