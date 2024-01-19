using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
using FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<IEnumerable<AdminResponse>>> GetAll(bool includeDeleted = false)
        {
            return await mediator.Send(new GetAllAdminsQuery(includeDeleted));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<IEnumerable<AdminResponse>>> GetById(bool includeDeleted = false)
        {
            return await mediator.Send(new GetAllAdminsQuery(includeDeleted));
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<AdminResponse>> Create(AdminRequest request)
        {
            return await mediator.Send(new CreateAdminCommand(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Update(int id, AdminRequest admin)
        {
            return await mediator.Send(new UpdateAdminCommand(id, admin));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Delete(int id)
        {
            return await mediator.Send(new DeleteApplicationUserCommand(id));
        }
    }
}
