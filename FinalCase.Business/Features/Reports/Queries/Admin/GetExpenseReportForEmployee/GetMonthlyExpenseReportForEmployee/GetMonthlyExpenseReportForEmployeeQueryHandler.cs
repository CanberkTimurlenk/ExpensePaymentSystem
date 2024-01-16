using Dapper;
using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Constants;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinalCase.Business.Features.Reports.Queries.Admin.ExpenseReportForEmployee.GetMonthlyExpenseReportForEmployee;
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
        parameters.Add("@UserId", id, DbType.Int32);
        parameters.Add("@StartDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DbType.DateTime);
        parameters.Add("@EndDate", DateTime.Now, DbType.DateTime);

        return await DapperExecutor.ExecuteStoredProcedureAsync<EmployeeExpenseReport>(
                        StoredProcedures.GetEmployeeExpensesByDateRange,
                        parameters,
                        configuration.GetConnectionString(DbKeys.SqlServer),
                        cancellationToken);
    }
}
