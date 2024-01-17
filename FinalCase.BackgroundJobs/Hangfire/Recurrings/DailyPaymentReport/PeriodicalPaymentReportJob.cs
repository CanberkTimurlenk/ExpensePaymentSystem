using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using Microsoft.Extensions.Configuration;
using static FinalCase.BackgroundJobs.MicroOrm.Dapper.DapperExecutor;

namespace FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport;

/// <summary>
/// A class containing methods for generating daily, weekly, and monthly payment reports using views in the database.
/// </summary>
public static class PeriodicalPaymentReportJob
{
    private readonly static string connString = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build().GetConnectionString(DbKeys.SqlServer);   // Get the configuration settings.

    /// <summary>
    /// The method called by Hangfire to create the daily payment report.
    /// </summary>
    /// <returns>Daily Payment Report</returns>
    public async static Task<IEnumerable<PaymentScheduledReport>> EnableDailyPaymentReports()
        => await QueryView<PaymentScheduledReport>(Views.DailyPaymentReport, connString);

    /// <summary>
    /// The method called by Hangfire to create the weekly payment report.
    /// </summary>
    /// <returns>Weekly Payment Report</returns>
    public async static Task<IEnumerable<PaymentScheduledReport>> EnableWeeklyPaymentReports()
        => await QueryView<PaymentScheduledReport>(Views.WeeklyPaymentReport, connString);


    /// <summary>
    /// The method called by Hangfire to create the monthly payment report.
    /// </summary>
    /// <returns>Monthly Payment Report</returns>
    public async static Task<IEnumerable<PaymentScheduledReport>> EnableMonthlyPaymentReports()
        => await QueryView<PaymentScheduledReport>(Views.MonthlyPaymentReport, connString);
}
