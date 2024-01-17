using static FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport.ScheduledPaymentReportJob;
using FinalCase.Services.NotificationService;
using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Data.Constants.DbObjects;

namespace FinalCase.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnableReportingJobs(this IApplicationBuilder app)
    {
        var notificationService = app.ApplicationServices.GetRequiredService<INotificationService>();

        EnableDailyPaymentReports(notificationService);

        EnableWeeklyPaymentReports(notificationService);

        EnableMonthlyPaymentReports(notificationService);

        DapperExecutor.QueryView<string>(Views.DailyPaymentReport, "Server=127.0.0.1,1430;Database=FinalCaseDb;User ID=SA;Password=Ab12345678;TrustServerCertificate=True");

    }
}