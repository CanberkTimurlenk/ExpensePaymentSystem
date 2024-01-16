using Dapper;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport;

/// <summary>
/// A class containing methods for generating daily, weekly, and monthly payment reports using views in the database.
/// </summary>
public static class PeriodicalPaymentReportJob
{
    private readonly static IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();   // Get the configuration settings.

    /// <summary>
    /// The method called by Hangfire to create the daily payment report.
    /// </summary>
    /// <returns>Daily Payment Report</returns>
    public static IEnumerable<PaymentScheduledReport> EnableDailyPaymentReports()
        => GetPaymentReports("SELECT * FROM DailyPaymentReport");

    /// <summary>
    /// The method called by Hangfire to create the weekly payment report.
    /// </summary>
    /// <returns>Weekly Payment Report</returns>
    public static IEnumerable<PaymentScheduledReport> EnableWeeklyPaymentReports()
        => GetPaymentReports("SELECT * FROM WeeklyPaymentReport");

    /// <summary>
    /// The method called by Hangfire to create the monthly payment report.
    /// </summary>
    /// <returns>Monthly Payment Report</returns>
    public static IEnumerable<PaymentScheduledReport> EnableMonthlyPaymentReports()
        => GetPaymentReports("SELECT * FROM MonthlyPaymentReport");

    /// <summary>
    /// Opens a connection to the database and executes the query with Dapper to get the payment reports.
    /// </summary>
    /// <param name="query">The SQL query to retrieve payment reports.</param>
    /// <returns>An IEnumerable which representing the result of the query.</returns>
    private static IEnumerable<PaymentScheduledReport> GetPaymentReports(string query)
    {
        using var connection = new SqlConnection(configuration.GetConnectionString(DbKeys.SqlServer));
        connection.Open();

        var result = connection.Query<PaymentScheduledReport>(query);

        return result;

    }
}
