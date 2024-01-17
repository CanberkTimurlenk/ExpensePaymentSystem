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
/// A class containing methods for generating daily, weekly, and monthly payment reports using views in the database.
/// </summary>
public static class ScheduledPaymentReportJob
{
    private readonly static string connString = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build().GetConnectionString(DbKeys.HangfireSql);   // Get the configuration settings.

    public static void EnableDailyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("DailyPaymentReport",
            () => CreatePaymentReports(Views.DailyPaymentReport, DailyReportSubject, notificationService), "0 16 * * *"); // everyday 4pm);
    }

    public static void EnableWeeklyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("WeeklyPaymentReport",
            () => CreatePaymentReports(Views.WeeklyPaymentReport, WeeklyReportSubject, notificationService), "0 14 * * 7"); // every friday 2pm
    }

    public static void EnableMonthlyPaymentReports(INotificationService notificationService)
    {
        RecurringJob.AddOrUpdate("MonthlyPaymentReport",
           () => CreatePaymentReports(Views.MonthlyPaymentReport, MonthlyReportSubject, notificationService), "0 14 L * *"); // every last day of the month 2pm
    }

    public static void CreatePaymentReports(string viewName, string mailSubject, INotificationService notificationService)
    {
        if (!IsViewNameValid(viewName))
            throw new ArgumentException("Invalid view name");

        var report = QueryView<PaymentScheduledReport>(viewName, connString); // The report to be sent.
        var adminEmails = QueryView<string>(Views.AdminEmails, connString); // The collection of admin emails.

        notificationService.SendEmail(new Email
        {
            To = adminEmails,
            Subject = mailSubject,
            Body = JsonSerializer.Serialize(report)
        });
    }
}