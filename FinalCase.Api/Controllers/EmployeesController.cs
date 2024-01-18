using FinalCase.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FinalCase.Api.Constants.Controller.ControllerConstants;
using FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using System.Runtime.Intrinsics.X86;
using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;

namespace FinalCase.Api.Controllers
{
    /// <summary>
    /// The controller class for the role 'employee'.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        /// <summary>
        /// Retrieves a list of expenses based on specified parameters
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="paymentMethodId">Optional: Filter by Payment Method ID.</param>
        /// <param name="categoryId">Optional: Filter by Expense Category ID.</param>
        /// <param name="minAmount">Optional:Filter by Minimum amount.</param>
        /// <param name="maxAmout">Optional:Filter by Maximum amount.</param>
        /// <param name="initialDate">Optional: Filter by Initial date.</param>
        /// <param name="finalDate">Optional: Filter by Final date.</param>
        /// <param name="location">Optional: Filter by Expense location.</param>
        /// <returns>A list of expenses based on the specified parameters.</returns>
        [HttpGet("{" + EmployeeId + ":int}/expenses")] // EmployeeId is a constant defined in ControllerConstants.cs, 
        [Authorize(Roles = Roles.Employee)]
        [AuthorizeIdMatch]
        //is extracted from the JWT token,
        //but by adding it to the route, we are making the semantic structure of the URI more meaningful.        
        public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Get([FromRoute(Name = EmployeeId)] int employeeId, int? paymentMethodId, int? categoryId, int? minAmount, int? maxAmout,
        DateTime? initialDate, DateTime? finalDate, string? location)
        {
            var operation = new GetExpensesByParameterQuery(employeeId, paymentMethodId, categoryId, minAmount, maxAmout, initialDate, finalDate, location);
            return await mediator.Send(operation);
        }
    }
}
