using Dapper;
using FinalCase.Data.Entities;
using FinalCase.Schema.Reports;
using System.Data;
using System.Data.SqlClient;

namespace FinalCase.BackgroundJobs.MicroOrm.Dapper;
public static class DapperReportHelper
{
    /// <summary>
    /// Since documents are a collection, there is a need to use a dictionary to avoid duplicates.
    /// expenses and Documents have one to many relationship    
    /// </summary> 
    public static async Task<IEnumerable<EmployeeExpenseReport>> GetEmployeeExpenseReports(string connectionString, string storedProcedure, DynamicParameters parameters, CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        var lookup = new Dictionary<int, EmployeeExpenseReport>();

        _ = await connection.QueryAsync<EmployeeExpenseReport, DocumentReport, EmployeeExpenseReport>(
            storedProcedure,
            (report, document) =>
            {
                if (!lookup.TryGetValue(report.ExpenseId, out var expenseReportEntry))
                {
                    expenseReportEntry = report;
                    expenseReportEntry.Documents = new List<DocumentReport>();
                    lookup.Add(report.ExpenseId, expenseReportEntry);
                }

                expenseReportEntry.Documents.Add(document);
                return expenseReportEntry;
            },
            parameters,
            splitOn: "DocumentName",
            commandType: CommandType.StoredProcedure);

        return lookup.Values;
    }
}