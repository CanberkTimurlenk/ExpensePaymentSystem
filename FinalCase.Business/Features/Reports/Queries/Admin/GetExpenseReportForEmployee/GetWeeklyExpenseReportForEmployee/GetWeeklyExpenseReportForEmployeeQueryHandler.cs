using Dapper;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Extensions;
using FinalCase.Base.Response;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseReportForEmployee.GetWeeklyExpenseReportForEmployee;
public class GetWeeklyExpenseReportForEmployeeQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetWeeklyExpenseReportForEmployeeQuery, ApiResponse<IEnumerable<EmployeeExpenseReport>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> Handle(GetWeeklyExpenseReportForEmployeeQuery request, CancellationToken cancellationToken)
    {
        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(await GetWeeklyExpenses(request.Id, cancellationToken));
    }

    private async Task<IEnumerable<EmployeeExpenseReport>> GetWeeklyExpenses(int id, CancellationToken cancellationToken)
    {
        var start = DateTime.Now.StartOfWeek(DayOfWeek.Monday);

        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id, DbType.Int32); // Employee(User) Id
        parameters.Add("@StartDate", start, DbType.DateTime); // Start date of week 00:00:00
        parameters.Add("@EndDate", start.AddDays(6).AddMinutes(1439).AddSeconds(59), DbType.DateTime); // Last date of week 23:59:59

        var connStr = configuration.GetConnectionString(DbKeys.SqlServer);

        return await DapperReportHelper.GetEmployeeExpenseReports(connStr, StoredProcedures.GetEmployeeExpensesByDateRange, parameters, cancellationToken);
    }
}

