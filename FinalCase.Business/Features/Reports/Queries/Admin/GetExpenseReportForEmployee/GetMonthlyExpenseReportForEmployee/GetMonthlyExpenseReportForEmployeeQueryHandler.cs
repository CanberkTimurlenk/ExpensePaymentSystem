using Dapper;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Response;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseReportForEmployee.GetMonthlyExpenseReportForEmployee;
public class GetMonthlyExpenseReportForEmployeeQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetMonthlyExpenseReportForEmployeeQuery, ApiResponse<IEnumerable<EmployeeExpenseReport>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> Handle(GetMonthlyExpenseReportForEmployeeQuery request, CancellationToken cancellationToken)
    {
        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(await GetMonthlyExpenses(request.Id, cancellationToken));
    }

    private async Task<IEnumerable<EmployeeExpenseReport>> GetMonthlyExpenses(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id, DbType.Int32); // Employee(User) Id
        parameters.Add("@StartDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date, DbType.DateTime); // First day of the month 00:00:00
        parameters.Add("@EndDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).AddMinutes(1439).AddSeconds(59),
            DbType.DateTime); // Last day of the month 23:59:59

        var connStr = configuration.GetConnectionString(DbKeys.SqlServer);

        return await DapperReportHelper.GetEmployeeExpenseReports(connStr, StoredProcedures.GetEmployeeExpensesByDateRange, parameters, cancellationToken);
    }
}
