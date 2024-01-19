using FinalCase.Api.Helpers;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Authentication.Constants.Jwt;
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
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetByParameter(int? employeeId, int? paymentMethodId, int? categoryId,
        int? minAmount, int? maxAmount, DateTime? initialDate, DateTime? finalDate, string? location)
    {
        /*
        var operation = new GetExpensesByParameterQuery(employeeId, categoryId, paymentMethodId, minAmount, maxAmount, initialDate, finalDate, location);
        return await mediator.Send(operation);
        */
        return null;
    }

    [HttpGet("{id:int}")]
    ////[Authorize(Roles = Roles.Employee)]   
    public async Task<ApiResponse<ExpenseResponse>> GetById(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        return await mediator.Send(operation);
    }



    [HttpPost]
    [Authorize(Roles = Roles.Employee)]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int employeeId))
            return new ApiResponse<ExpenseResponse>(false);

        var operation = new CreateExpenseCommand(employeeId, request);
        return await mediator.Send(operation);
    }

    [HttpPost("approve")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> ApprovePendingExpenses(ICollection<ApproveExpenseRequest> request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int adminId))
            return new ApiResponse<IEnumerable<ExpenseResponse>>(false);

        return await mediator.Send(new ApproveExpensesCommand(adminId, request));
    }

    [HttpPost("reject")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> RejectPendingExpenses(ICollection<RejectExpensesRequest> request)
    {
        if (!ClaimsHelper.TryGetUserIdFromClaims(User.Identity as ClaimsIdentity, out int adminId))
            return new ApiResponse<IEnumerable<ExpenseResponse>>(false);

        return await mediator.Send(new RejectExpensesCommand(adminId, request));
    }

    // Only for pending expenses
    [HttpPut("{id:int}")]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Employee}")]
    public async Task<ApiResponse> UpdateExpense(int id, ExpenseRequest request)
    {
        return await mediator.Send(new UpdateExpenseCommand(id, request));
    }

    // Only for pending expenses
    [HttpDelete("{id:int}")]
    //[Authorize(Roles = $"{Roles.Admin},{Roles.Employee}")]
    public async Task<ApiResponse> DeleteExpense(int id)
    {
        return await mediator.Send(new DeleteExpenseCommand(id));
    }
}
