using MediatR;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using FinalCase.Business.Features.Reports.GetExpenseReportForUser;
using FinalCase.Data.Enums;

namespace FinalCase.Business.Features.Reports.GetEmployeeExpenseReportById;
/*
public class GetExpenseReportForUserQueryHandler(IConfiguration configuration) : IRequestHandler<GetExpenseReportForUserQuery, GetExpenseReportForUserQueryResponse>
{
    private readonly IConfiguration configuration = configuration;
    public Task<GetExpenseReportForUserQueryResponse> Handle(GetExpenseReportForUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<ExpenseReportForEmployee> GetExpensesByUserId(int userId)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString("MsSqlConnection"));
        connection.Open();

        var expenseReports = connection.Query<ExpenseReportForEmployee>(
            "GetExpenseReportByUserId",
            new { UserId = userId },
            commandType: CommandType.StoredProcedure
            );


        foreach (var report in expenseReports)
            report.Status = Enum.GetName(typeof(ExpenseStatus), report.Status);

        return null;
    }


}
*/