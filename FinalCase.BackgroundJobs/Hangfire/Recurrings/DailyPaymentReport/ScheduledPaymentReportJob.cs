using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Email;
using FinalCase.Schema.Reports;
using static FinalCase.Schema.Reports.Constants.ScheduledPaymentReportEmailConstants;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static FinalCase.BackgroundJobs.MicroOrm.Dapper.DapperExecutor;
using FinalCase.Services.NotificationService;

namespace FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport;

/// <summary>
/// A class containing methods for generating daily, weekly, and monthly payment reports using views in the db.
/// </summary>
public static class ScheduledPaymentReportJob
{
    private readonly static string sqlServerConnStr = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build().GetConnectionString(DbKeys.SqlServer);   // Get the configuration settings.

    /// <summary>
    /// Enables the generation and email sending of daily payment reports using Hangfire.
    /// </summary>
    public static void EnableDailyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("DailyPaymentReport",
            () => SendScheduledPaymentReports(Views.DailyPaymentReport, DailyReportSubject, notificationService), "0 16 * * *"); // everyday 4pm);
    }

    /// <summary>
    /// Enables the generation and email sending of weekly payment reports using Hangfire.
    /// </summary>
    public static void EnableWeeklyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("WeeklyPaymentReport",
            () => SendScheduledPaymentReports(Views.WeeklyPaymentReport, WeeklyReportSubject, notificationService), "0 14 * * 7"); // every friday 2pm
    }

    /// <summary>
    /// Enables the generation and email sending of weekly payment reports using Hangfire.
    /// </summary>
    public static void EnableMonthlyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("MonthlyPaymentReport",
           () => SendScheduledPaymentReports(Views.MonthlyPaymentReport, MonthlyReportSubject, notificationService), "0 14 L * *"); // every last day of the month 2pm
    }

    /// <summary>
    /// Generates a scheduled payment report based on the provided view name, and sends it as an email to all admins.
    /// </summary>
    /// <param name="viewName">The name of the database view to generate the report from.</param>
    /// <param name="mailSubject">The subject of the email to be sent.</param>
    /// <param name="notificationService">The notification service used to send the email.</param>
    public static void SendScheduledPaymentReports(string viewName, string mailSubject, INotificationService notificationService)
    {
        if (!IsViewNameValid(viewName))
            throw new ArgumentException("Invalid view name");

        var report = QueryView<PaymentScheduledReport>(viewName, sqlServerConnStr); // The report to be sent.
        var adminEmails = QueryView<string>(Views.AdminEmails, sqlServerConnStr); // The collection of admin emails.

        notificationService.SendEmail(new Email
        {
            To = adminEmails,
            Subject = mailSubject,
            Body = JsonSerializer.Serialize(report)
        });
    }
}