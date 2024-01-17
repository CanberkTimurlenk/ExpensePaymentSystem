using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentReport.GetMonthlyPaymentReport;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentReport.GetDailyPaymentReport;
public class GetWeeklyPaymentReportQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetWeeklyPaymentReportQuery, ApiResponse<IEnumerable<PaymentScheduledReport>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<PaymentScheduledReport>>> Handle(GetWeeklyPaymentReportQuery request, CancellationToken cancellationToken)
    {
        var dailyPayments = await DapperExecutor.QueryViewAsync<PaymentScheduledReport>(
                                           Views.WeeklyPaymentReport,
                                           configuration.GetConnectionString(DbKeys.SqlServer),
                                           cancellationToken);

        return new ApiResponse<IEnumerable<PaymentScheduledReport>>(dailyPayments);
    }
}
