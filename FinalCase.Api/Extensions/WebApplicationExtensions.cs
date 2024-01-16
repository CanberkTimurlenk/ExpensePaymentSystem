using static FinalCase.BackgroundJobs.Hangfire.Recurrings.CreateReport.PeriodicalPaymentReportJob;
using Hangfire;

namespace FinalCase.Api.Extensions;

public static class Jobs
{
    public static void EnableReportingJobs()
    {
        RecurringJob.AddOrUpdate("DailyPaymentReport", () => EnableDailyPaymentReports(), "0 16 * * *"); // everyday 4pm
         
        RecurringJob.AddOrUpdate("WeeklyPaymentReport", () => EnableWeeklyPaymentReports(), "0 14 * * 5"); // every friday 2pm

        RecurringJob.AddOrUpdate("MonthlyPaymentReport", () => EnableMonthlyPaymentReports(), "0 14 L * *"); // every last day of the month 2pm
    }
}