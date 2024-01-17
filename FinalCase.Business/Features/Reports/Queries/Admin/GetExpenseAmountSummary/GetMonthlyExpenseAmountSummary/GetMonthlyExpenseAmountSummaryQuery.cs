using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.Reports;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetMonthlyExpenseAmountSummary;
public record GetMonthlyExpenseAmountSummaryQuery : IRequest<ApiResponse<ExpenseAmountSummary>>;