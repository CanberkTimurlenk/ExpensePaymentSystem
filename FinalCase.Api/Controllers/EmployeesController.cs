using FinalCase.Api.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using FinalCase.Business.Features.ApplicationUsers.Commands.Delete;
using FinalCase.Business.Features.ApplicationUsers.Commands.Create.Admin;
using FinalCase.Schema.AppRoles.Requests;
using FinalCase.Schema.AppRoles.Responses;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
using FinalCase.Business.Features.ApplicationUsers.Queries.GetById;
using FinalCase.Data.Enums;

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

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> GetAll(bool includeDeleted = false)
        {
            return await mediator.Send(new GetAllEmployeesQuery(includeDeleted));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<EmployeeResponse>> GetById(int id, bool includeDeleted = false)
        {
            return await mediator.Send(new GetEmployeeByIdQuery(id, includeDeleted));
        }

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
        [HttpGet("{id:int}/expenses")] // EmployeeId is a constant defined in ControllerConstants.cs, 
        [Authorize(Roles = Roles.Employee)]
        [AuthorizeIdMatch]
        //is extracted from the JWT token,
        //but by adding it to the route, we are making the semantic structure of the URI more meaningful.        
        public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Get([FromRoute] int id, [FromQuery] GetExpensesQueryParameters parameters)
        {
            var operation = new GetExpensesByParameterQuery(id, parameters);
            return await mediator.Send(operation);

        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<EmployeeResponse>> Create(EmployeeRequest request)
        {
            return await mediator.Send(new CreateEmployeeCommand(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Update(int id, EmployeeRequest request)
        {
            return await mediator.Send(new UpdateEmployeeCommand(id, request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Delete(int id)
        {
            return await mediator.Send(new DeleteApplicationUserCommand(id));
        }
    }
}
