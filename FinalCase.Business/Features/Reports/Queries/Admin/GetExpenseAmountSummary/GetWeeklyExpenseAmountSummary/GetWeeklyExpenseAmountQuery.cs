using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.Reports;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
public record GetWeeklyExpenseAmountQuery : IRequest<ApiResponse<IEnumerable<ExpenseAmountSummary>>>;