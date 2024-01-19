using FinalCase.Base.Response;
using FinalCase.Data.Enums;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace FinalCase.Business.Features.Expenses.Queries.GetExpenseByParameter;
public record GetExpensesByParameterQuery // for creator employee id, there is jwt token restriction. The employee could only retrieve his/her own expenses
    (int? CreatorEmployeeId, GetExpensesQueryParameters Parameters)

    : IRequest<ApiResponse<IEnumerable<ExpenseResponse>>>;


public record GetExpensesQueryParameters
{
    public int? CategoryId { get; set; }
    public int? PaymentMethodId { get; set; }
    public int? MinAmount { get; set; }
    public int? MaxAmount { get; set; }
    public DateTime? InitialDate { get; set; }
    public DateTime? FinalDate { get; set; }
    public string? Location { get; set; }
    public ExpenseStatus Status { get; set; }
}