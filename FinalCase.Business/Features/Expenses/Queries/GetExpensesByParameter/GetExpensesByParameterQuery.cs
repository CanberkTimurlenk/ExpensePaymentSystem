using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
public record GetExpensesByParameterQuery
    (int? CreatorEmployeeId, int? CategoryId, int? MinAmount, int? MaxAmount,
        DateTime? InitialDate, DateTime? FinalDate, string? Location)

    : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;