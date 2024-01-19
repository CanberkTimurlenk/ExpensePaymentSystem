using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
using FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
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

        [HttpGet("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<AdminResponse>> GetById(int id, bool includeDeleted = false)
        {
            return await mediator.Send(new GetAdminByIdQuery(id, includeDeleted));
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<AdminResponse>> Create(AdminRequest request)
        {
            return await mediator.Send(new CreateAdminCommand(request));
        }

        [HttpPut("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Update(int id, AdminRequest admin)
        {
            return await mediator.Send(new UpdateAdminCommand(id, admin));
        }

        [HttpDelete("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id < 0)
                return new ApiResponse("Invalid Id");

            return await mediator.Send(new DeleteApplicationUserCommand(id));
        }
    }
}
