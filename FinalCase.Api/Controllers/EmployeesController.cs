using FinalCase.Api.Filters;
using FinalCase.Business.Features.Expenses.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

namespace FinalCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("{employee-id:int}/expenses")] // get expenses
        [Authorize(Roles = "employee")]
        [AuthorizeIdMatch] // check the summary for more information

        //The employee id could have been directly retrieved from the token in this case, 
        //but by adding the id to the route, we are making the semantic structure of the URI more meaningful.
        public async Task<IActionResult> Get([FromRoute(Name = "employee-id")] int employeeId, int? categoryId, int? minBalance, int? maxBalance,
        DateTime? initialDate, DateTime? finalDate, string? location)
        {
            //GetExpensesByParameterQuery


            return Ok();


            //var result = await mediator.Send(new GetExpensesByParameterQuery());



            //return Ok(result);
        }



    }
}
