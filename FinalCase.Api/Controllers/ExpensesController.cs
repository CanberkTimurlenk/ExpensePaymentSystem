using FinalCase.Api.Filters;
using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Authentication.Constants.Jwt;
using FinalCase.Business.Features.ApplicationUsers.Authentication.Constants.Roles;
using FinalCase.Business.Features.Expenses.Commands.CreateExpense;
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
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetAll()
    {
        var operation = new GetAllExpensesQuery();
        return await mediator.Send(operation);
    }*/


    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetByParameter(int? employeeId, int? paymentMethodId, int? categoryId,
        int? minAmount, int? maxAmount, DateTime? initialDate, DateTime? finalDate, string? location)
    {
        var operation = new GetExpensesByParameterQuery(employeeId, categoryId, paymentMethodId, minAmount, maxAmount, initialDate, finalDate, location);
        return await mediator.Send(operation);
    }

    [HttpGet("{id:int}")]
    //[Authorize(Roles = Roles.Employee)]   
    public async Task<ApiResponse<ExpenseResponse>> GetByParameter(int id)
    {
        var operation = new GetExpenseByIdQuery(id);
        return await mediator.Send(operation);
    }



    [HttpPost]
    [Authorize(Roles = Roles.Employee)]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense([FromBody] ExpenseRequest request)
    {
        string id = (User.Identity as ClaimsIdentity).FindFirst(JwtPayloadFields.Id)?.Value;

        var operation = new CreateExpenseCommand(int.Parse(id), request);
        return await mediator.Send(operation);
    }



    [HttpPost("approve")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> ApprovePendingExpenses()
    {
        // var operation = new ApprovePendingExpensesCommand();





        return null;
    }

    [HttpPost("reject")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<ExpenseResponse>>> GetPendingExpenses()
    {
        //var operation = new GetPendingExpensesQuery();
        //return await mediator.Send(operation);

        return null;
    }







}
