using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentScheduledReport.GetDailyPaymentReport;
public record GetDailyPaymentReportQuery() : IRequest<ApiResponse<IEnumerable<PaymentScheduledReport>>>;