using FinalCase.Base.Response;
using MediatR;
using FinalCase.Schema.Reports;

namespace FinalCase.Business.Features.Reports.Queries.Admin.GetExpenseAmountSummary.GetDailyExpenseAmountSummary;
public record GetDailyExpenseAmountQuery : IRequest<ApiResponse<IEnumerable<ExpenseAmountSummary>>>;