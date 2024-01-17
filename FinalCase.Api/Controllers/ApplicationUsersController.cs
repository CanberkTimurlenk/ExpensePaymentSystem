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
        /*
        [HttpGet]
        //[Authorize(Roles = Roles.Admin)]
        //getall
        public async Task<ApiResponse<IEnumerable<ApplicationUserResponse>>> GetAll()
        {
            return await mediator.Send(new GetAllApplicationUsersQuery());
        }
        [HttpPost]
        */



    }
}
