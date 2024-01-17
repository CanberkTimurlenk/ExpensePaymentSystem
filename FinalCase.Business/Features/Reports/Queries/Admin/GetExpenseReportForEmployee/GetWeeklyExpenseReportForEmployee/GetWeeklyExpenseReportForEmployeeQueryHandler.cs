using Dapper;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <returns></returns>
    private async Task<IEnumerable<EmployeeExpenseReport>> GetWeeklyExpenses(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id, DbType.Int32); // Employee(User) Id
        parameters.Add("@StartDate", DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek).Date, DbType.DateTime); // Start date of week 00:00:00
        parameters.Add("@EndDate", DateTime.Now.AddDays(7 - (int)DateTime.Now.DayOfWeek).Date.AddMinutes(1439).AddSeconds(59), DbType.DateTime); // Last date of week 23:59:59

        return await DapperExecutor.ExecuteStoredProcedureAsync<EmployeeExpenseReport>(
                        StoredProcedures.GetEmployeeExpensesByDateRange,
                        parameters,
                        configuration.GetConnectionString(DbKeys.SqlServer),
                        cancellationToken);
    }
}