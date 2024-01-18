using FinalCase.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FinalCase.Api.Constants.Controller.ControllerConstants;
using FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
using FinalCase.Business.Features.Authentication.Constants.Roles;

namespace FinalCase.Api.Controllers
{
    /// <summary>
    /// Since there is only one class to represent the employee entity,
    /// we will use the EmployeesController for employee role
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("{" + EmployeeId + ":int}/expenses")] // EmployeeId is a constant defined in ControllerConstants.cs, 
        [Authorize(Roles = Roles.Employee)]
        [AuthorizeIdMatch] // check the summary for more information
        //The employee id could have been directly retrieved from the token in this case, 
        //but by adding the id to the route, we are making the semantic structure of the URI more meaningful.
        public async Task<IActionResult> Get([FromRoute(Name = EmployeeId)] int employeeId, int? paymentMethodId, int? categoryId, int? minBalance, int? maxBalance,
        DateTime? initialDate, DateTime? finalDate, string? location)
        {
            var operation = new GetExpensesByParameterQuery(employeeId, paymentMethodId, categoryId, minBalance, maxBalance, initialDate, finalDate, location);
            var response = await mediator.Send(operation);
            return Ok(response);
        }
    }
}
