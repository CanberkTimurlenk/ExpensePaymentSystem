using FinalCase.Api.Filters;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Expenses.Commands.CreateExpense;
using FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
using FinalCase.Schema.Requests;
using FinalCase.Schema.Responses;
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

    [HttpPost]
    [Authorize(Roles = "employee")]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest request)
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst("Id")?.Value;

        var operation = new CreateExpenseCommand(int.Parse(id), request);
        return await mediator.Send(operation);
    }

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> Get(int? employeeId, int? categoryId, int? minBalance, int? maxBalance,
        DateTime? initialDate, DateTime? finalDate, string? location)
    {
        var operation = new GetExpensesByParameterQuery(employeeId, categoryId, minBalance, maxBalance, initialDate, finalDate, location);
        return await mediator.Send(operation);
    }
}
