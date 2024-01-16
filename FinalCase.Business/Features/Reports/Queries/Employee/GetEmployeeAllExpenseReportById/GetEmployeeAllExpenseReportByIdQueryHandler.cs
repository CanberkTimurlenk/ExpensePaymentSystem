using MediatR;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using FinalCase.Data.Enums;
using FinalCase.Schema.Reports;
using FinalCase.Base.Response;
using FinalCase.Business.MicroOrm.Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Data.Constants.DbObjects;

namespace FinalCase.Business.Features.Reports.Queries.Employee.GetEmployeeAllExpenseReport;

public class GetEmployeeAllExpenseReportByIdQueryHandler(IConfiguration configuration) : IRequestHandler<GetEmployeeAllExpenseReportByIdQuery, ApiResponse<IEnumerable<EmployeeExpenseReport>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<EmployeeExpenseReport>>> Handle(GetEmployeeAllExpenseReportByIdQuery request, CancellationToken cancellationToken)
    {
        var expenses = await GetEmployeeAllExpensesByUserId(request.Id, cancellationToken);

        return new ApiResponse<IEnumerable<EmployeeExpenseReport>>(expenses);
    }

    private async Task<IEnumerable<EmployeeExpenseReport>> GetEmployeeAllExpensesByUserId(int id, CancellationToken cancellationToken)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", id, DbType.Int32);

        var expenseReports = await DapperExecutor.ExecuteStoredProcedureAsync<EmployeeExpenseReport>(
                StoredProcedures.GetEmployeeAllExpenses,
                parameters,
                configuration.GetConnectionString(DbKeys.SqlServer),
                cancellationToken);

        foreach (var report in expenseReports)
            report.ExpenseStatus = Enum.GetName(typeof(ExpenseStatus), report.ExpenseStatus);

        return expenseReports;
    }
}