using static FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport.ScheduledPaymentReportJob;
using FinalCase.Services.NotificationService;

namespace FinalCase.Api.Extensions;
public static class ApplicationBuilderExtensions
{
    public static void EnableReportingJobs(this IApplicationBuilder app)
    {
        var notificationService = app.ApplicationServices.GetRequiredService<INotificationService>();
        
        EnableDailyPaymentReports(notificationService);

        EnableWeeklyPaymentReports(notificationService);

        EnableMonthlyPaymentReports(notificationService);
    }
}