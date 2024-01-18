using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
using FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
using FinalCase.Data.Entities;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("admin")]
        public async Task<ApiResponse<AdminResponse>> CreateAdmin([FromBody] AdminRequest request)
        {
            return await mediator.Send(new CreateAdminCommand(request));
        }

        [HttpPost("employee")]
        public async Task<ApiResponse<EmployeeResponse>> CreateEmployee(EmployeeRequest request)
        {
            return await mediator.Send(new CreateEmployeeCommand(request));
        }

        [HttpGet]
        public async Task<ApiResponse<IEnumerable<ApplicationUserResponse>>> GetUsers(bool includeDeleted = false)
        {
            return await mediator.Send(new GetAllApplicationUsersQuery(includeDeleted));
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResponse<ApplicationUserResponse>> GetUserById(int id)
        {
            return await mediator.Send(new GetApplicationUserByIdQuery(id));
        }

        [HttpPut("{id:int}")]
        public async Task<ApiResponse> UpdateAdmin(int id, AdminRequest admin)
        {
            return await mediator.Send(new UpdateAdminCommand(id, admin));
        }

        [HttpDelete("{id:int}")]
        public async Task<ApiResponse> DeleteUser(int id)
        {
            return await mediator.Send(new DeleteApplicationUserCommand(id));
        }
    }
}
