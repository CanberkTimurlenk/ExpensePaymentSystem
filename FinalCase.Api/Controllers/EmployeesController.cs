using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
using FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
using static FinalCase.Api.Helpers.ClaimsHelper;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetAll()
    {
        return await mediator.Send(new GetAllEmployeesQuery());
    }

    [HttpGet("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<EmployeeResponse>> GetById(int id)
    {
        return await mediator.Send(new GetEmployeeByIdQuery(id));
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<EmployeeResponse>> Create(EmployeeRequest request)
    {
        var (userId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add InsertUserId

        return await mediator.Send(new CreateEmployeeCommand(userId, request));
    }

    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> Update(int id, EmployeeRequest request)
    {
        var (userId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity); // to add UpdateUserId

        return await mediator.Send(new UpdateEmployeeCommand(userId, id, request));
    }

    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> Delete(int id)
    {
        return await mediator.Send(new DeleteApplicationUserCommand(id));
    }
}
