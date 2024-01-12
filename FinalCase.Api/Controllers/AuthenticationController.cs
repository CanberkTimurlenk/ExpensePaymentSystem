using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Authentication;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost]
        public async Task<ApiResponse<AuthenticationResponse>> Post([FromBody] AuthenticationRequest request)
        {
            var operation = new AuthenticateUserCommand(request);
            return await mediator.Send(operation);
        }
    }
}
