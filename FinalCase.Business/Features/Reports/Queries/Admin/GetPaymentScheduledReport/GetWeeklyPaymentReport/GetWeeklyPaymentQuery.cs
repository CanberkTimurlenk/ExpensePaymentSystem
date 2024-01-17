using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentScheduledReport.GetWeeklyPaymentReport;
public record GetWeeklyPaymentReportQuery() : IRequest<ApiResponse<IEnumerable<PaymentScheduledReport>>>;