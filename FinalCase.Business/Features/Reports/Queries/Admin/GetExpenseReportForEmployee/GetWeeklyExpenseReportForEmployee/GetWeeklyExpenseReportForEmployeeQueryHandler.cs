using Dapper;
using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetWeeklyExpenseReportForEmployee;
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
        var parameters = new DynamicParameters(); // Dynamic parameters for stored procedure
        parameters.Add("@UserId", id, DbType.Int32); // User Id
        parameters.Add("@StartDate", DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek), DbType.DateTime); // Start date of week
        parameters.Add("@EndDate", DateTime.Now, DbType.DateTime); // Today

        return await DapperExecutor.ExecuteStoredProcedureAsync<EmployeeExpenseReport>(
                        StoredProcedures.GetEmployeeExpensesByDateRange,
                        parameters,
                        configuration.GetConnectionString(DbKeys.SqlServer),
                        cancellationToken);
    }
}