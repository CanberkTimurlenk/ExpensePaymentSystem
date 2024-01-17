using FinalCase.BackgroundJobs.MicroOrm.Dapper;
using FinalCase.Base.Response;
using FinalCase.Data.Constants.DbObjects;
using FinalCase.Data.Constants.Storage;
using FinalCase.Schema.Reports;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentScheduledReport.GetDailyPaymentReport;
public class GetDailyPaymentReportQueryHandler(IConfiguration configuration)
    : IRequestHandler<GetDailyPaymentReportQuery, ApiResponse<IEnumerable<PaymentScheduledReport>>>
{
    private readonly IConfiguration configuration = configuration;

    public async Task<ApiResponse<IEnumerable<PaymentScheduledReport>>> Handle(GetDailyPaymentReportQuery request, CancellationToken cancellationToken)
    {
        var dailyPayments = await DapperExecutor.QueryViewAsync<PaymentScheduledReport>(
                                           Views.DailyPaymentReport,
                                           configuration.GetConnectionString(DbKeys.SqlServer),
                                           cancellationToken);

        return new ApiResponse<IEnumerable<PaymentScheduledReport>>(dailyPayments);
    }
}
