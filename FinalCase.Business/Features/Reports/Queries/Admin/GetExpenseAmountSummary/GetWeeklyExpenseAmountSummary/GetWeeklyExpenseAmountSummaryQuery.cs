using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.Reports;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetWeeklyExpenseAmountSummary;
public record GetWeeklyExpenseAmountSummaryQuery : IRequest<ApiResponse<ExpenseAmountSummary>>;