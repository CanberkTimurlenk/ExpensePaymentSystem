using FinalCase.Base.Response;
using FinalCase.Business.Features.ApplicationUsers.Authentication.Constants.Roles;
using FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalCase.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet("expense-amount-summary/daily")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<ExpenseAmountSummary>> GetDailyExpenseAmountSummary()
    {
        return await mediator.Send(new GetDailyExpenseAmountSummaryQuery());
    }

    [HttpGet("expense-amount-summary/weekly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<ExpenseAmountSummary>> GetWeeklyExpenseAmountSummary()
    {
        return await mediator.Send(new GetWeeklyExpenseAmountSummaryQuery());
    }

    [HttpGet("expense-amount-summary/monthly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<ExpenseAmountSummary>> GetMonthlyExpenseAmountSummary()
    {
        return await mediator.Send(new GetMonthlyExpenseAmountSummaryQuery());
    }
}
