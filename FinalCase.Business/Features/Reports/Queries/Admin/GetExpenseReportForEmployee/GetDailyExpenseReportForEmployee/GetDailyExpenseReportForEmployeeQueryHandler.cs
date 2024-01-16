using Dapper;
using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetDailyExpenseReportForEmployee;
public class GetDailyExpenseReportForEmployeeQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetDailyExpenseReportForEmployeeQuery, ApiResponse<IEnumerable<EmployeeExpenseReport>>>
{
    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> Handle(GetDailyExpenseReportForEmployeeQuery request, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", request.Id, DbType.Int32);
        parameters.Add("@StartDate", DateTime.Now.Date, DbType.Date);
        parameters.Add("@EndDate", DateTime.Now.Date, DbType.Date);

        var dailyExpenses = await DapperExecutor.ExecuteStoredProcedureAsync<EmployeeExpenseReport>(
                StoredProcedures.GetEmployeeExpensesByDateRange,
                parameters,
                configuration.GetConnectionString(DbKeys.SqlServer),
                cancellationToken);

        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(dailyExpenses);
    }
}
