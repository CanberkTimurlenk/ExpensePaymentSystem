using FinalCase.Base.Response;
using FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetDailyExpenseReportForEmployee;
using FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetMonthlyExpenseReportForEmployee;
using FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetWeeklyExpenseReportForEmployee;
using FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
using FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentReport.GetDailyPaymentReport;
using FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentReport.GetMonthlyPaymentReport;
using FinalCase.Business.Features.Reports.Queries.Employee.GetEmployeeAllExpenseReport;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static FinalCase.Api.Constants.Controller.ControllerConstants;

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


    [HttpGet("expense-report/{" + EmployeeId + ":int}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> GetMonthlyExpenseAmountSummary([FromRoute(Name = EmployeeId)] int employeeId)
    {
        return await mediator.Send(new GetEmployeeAllExpenseReportByIdQuery(employeeId));
    }

    [HttpGet("expense-report/{" + EmployeeId + ":int}/daily")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> GetDailyExpenseReportForEmployee([FromRoute(Name = EmployeeId)] int employeeId)
    {
        return await mediator.Send(new GetDailyExpenseReportForEmployeeQuery(employeeId));
    }

    [HttpGet("expense-report/{" + EmployeeId + ":int}/weekly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> GetWeeklyExpenseReportForEmployee([FromRoute(Name = EmployeeId)] int employeeId)
    {
        return await mediator.Send(new GetWeeklyExpenseReportForEmployeeQuery(employeeId));
    }

    [HttpGet("expense-report/{" + EmployeeId + ":int}/monthly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> GetMonthlyExpenseReportForEmployee([FromRoute(Name = EmployeeId)] int employeeId)
    {
        return await mediator.Send(new GetMonthlyExpenseReportForEmployeeQuery(employeeId));
    }

    #region The Methods actually have planned as scheduled for testing purposes it is also added as endpoint
    ///
    [HttpGet("payments-report/daily")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentScheduledReport>>> GetDailyPaymentReport()
    {
        return await mediator.Send(new GetDailyPaymentReportQuery());
    }

    [HttpGet("payments-report/weekly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentScheduledReport>>> GetWeeklyPaymentReport()
    {
        return await mediator.Send(new GetWeeklyPaymentReportQuery());
    }

    [HttpGet("payments-report/monthly")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<ApiResponse<IEnumerable<PaymentScheduledReport>>> GetMonthlyPaymentReport()
    {
        return await mediator.Send(new GetMonthlyPaymentReportQuery());
    } 
    #endregion
}
