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

        [HttpGet("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse<EmployeeResponse>> GetById(int id, bool includeDeleted = false)
        {
            return await mediator.Send(new GetEmployeeByIdQuery(id, includeDeleted));
        }

        
        [HttpGet("{id:min(1)}/expenses")] // EmployeeId is a constant defined in ControllerConstants.cs, 
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

        [HttpPut("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Update(int id, EmployeeRequest request)
        {
            return await mediator.Send(new UpdateEmployeeCommand(id, request));
        }

        [HttpDelete("{id:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ApiResponse> Delete(int id)
        {
            return await mediator.Send(new DeleteApplicationUserCommand(id));
        }
    }
}
