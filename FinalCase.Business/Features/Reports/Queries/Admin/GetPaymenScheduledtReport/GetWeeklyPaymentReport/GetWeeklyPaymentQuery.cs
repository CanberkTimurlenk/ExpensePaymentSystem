using FinalCase.Base.Response;
using FinalCase.Schema.Reports;
using MediatR;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetPaymentReport.GetMonthlyPaymentReport;
public record GetWeeklyPaymentReportQuery() : IRequest<ApiResponse<IEnumerable<PaymentScheduledReport>>>;