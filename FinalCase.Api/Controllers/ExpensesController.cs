using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Roles;
using FinalCase.Business.Features.Expenses.Commands.Approve;
using FinalCase.Business.Features.Expenses.Commands.Create;
using FinalCase.Business.Features.Expenses.Commands.Delete;
using FinalCase.Business.Features.Expenses.Commands.Reject;
using FinalCase.Business.Features.Expenses.Commands.Update;
using FinalCase.Business.Features.Expenses.Queries.GetAllExpenses;
using FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
using FinalCase.Schema.Entity.Requests;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using static FinalCase.Api.Helpers.ClaimsHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    /*[HttpGet]  
    // Commented since there is another method with the same signature and also have parameters.
    // To follow the project requirements, this method have still added but commented.
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetAll()
    {
        var operation = new GetAllExpensesQuery();
        return await mediator.Send(operation);
    }*/

    [HttpGet]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetByParameter(int? employeeId, [FromQuery] GetExpensesQueryParameters parameters)
    {
        var operation = new GetExpensesByParameterQuery(employeeId, parameters);
        return await mediator.Send(operation);
    }

    [HttpGet("{id:min(1)}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<ExpenseResponse>> GetById(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        return await mediator.Send(operation);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Employee)]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest request)
    {
        var (employeeId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        var operation = new CreateExpenseCommand(employeeId, request);
        return await mediator.Send(operation);
    }

    [HttpPost("approve")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> ApprovePendingExpenses(ICollection<ApproveExpenseRequest> request)
    {
        var (adminId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        return await mediator.Send(new ApproveExpensesCommand(adminId, request));
    }

    [HttpPost("reject")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse> RejectPendingExpenses(ICollection<RejectExpensesRequest> request)
    {
        var (adminId, _) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        return await mediator.Send(new RejectExpensesCommand(adminId, request));
    }

    // Only for pending expenses
    [HttpPut("{id:min(1)}")]
    [Authorize(Roles = $"{Roles.Employee},{Roles.Admin}")]
    public async Task<ApiResponse> UpdateExpense(int id, ExpenseRequest request)
    {
        var (UserId, Role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        return await mediator.Send(new UpdateExpenseCommand(UserId, Role, id, request));
    }

    // Only for pending expenses
    [HttpDelete("{id:min(1)}")]
    [Authorize(Roles = $"{Roles.Employee},{Roles.Admin}")]
    public async Task<ApiResponse> DeleteExpense(int id)
    {
        var (UserId, Role) = GetUserIdAndRoleFromClaims(User.Identity as ClaimsIdentity);
        return await mediator.Send(new DeleteExpenseCommand(UserId, Role, id));
    }
}
