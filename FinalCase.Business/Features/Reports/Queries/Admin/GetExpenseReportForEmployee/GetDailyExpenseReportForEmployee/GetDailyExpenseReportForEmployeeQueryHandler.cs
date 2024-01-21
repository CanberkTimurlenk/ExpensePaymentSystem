using Dapper;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Response;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseReportForEmployee.GetDailyExpenseReportForEmployee;
public class GetDailyExpenseReportForEmployeeQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetDailyExpenseReportForEmployeeQuery, ApiResponse<IEnumerable<EmployeeExpenseReport>>>
{
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> Handle(GetDailyExpenseReportForEmployeeQuery request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", request.Id, DbType.Int32); // Employee(User) Id
        parameters.Add("@StartDate", DateTime.Now.Date.Date, DbType.DateTime); // Today's date with time set to 00:00:00
        parameters.Add("@EndDate", DateTime.Now.Date.AddMinutes(1439).AddSeconds(59), DbType.DateTime); // Today's date with time set to 23:59:59

        var connStr = configuration.GetConnectionString(DbKeys.SqlServer);


        var result = await DapperReportHelper.GetEmployeeExpenseReports(connStr, StoredProcedures.GetEmployeeExpensesByDateRange, parameters, cancellationToken);

        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(result);
    }
}
