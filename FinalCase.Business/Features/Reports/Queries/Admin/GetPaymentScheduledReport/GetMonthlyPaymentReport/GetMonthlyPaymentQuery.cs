using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentScheduledReport.GetMonthlyPaymentReport;
public record GetMonthlyPaymentReportQuery : IRequest<ApiResponse<IEnumerable<PaymentScheduledReport>>>;