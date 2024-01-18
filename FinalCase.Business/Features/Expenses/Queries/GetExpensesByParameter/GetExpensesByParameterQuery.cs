using FinalCase.Base.Response;
using FinalCase.Schema.Entity.Responses;
using MediatR;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
public record GetExpensesByParameterQuery // for creator employee id, there is jwt token restriction. The employee could only retrieve his/her own expenses
    (int? CreatorEmployeeId, int? CategoryId, int? PaymentMethodId, int? MinAmount, int? MaxAmount,
        DateTime? InitialDate, DateTime? FinalDate, string? Location)

    : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;