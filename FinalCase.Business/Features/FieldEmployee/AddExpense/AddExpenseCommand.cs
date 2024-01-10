using MediatR;

namespace FinalCase.Business.Features.FieldEmployee.AddExpense;
public record AddExpenseCommand : IRequest<AddExpenseResponse>
{
    public string Name { get; init; }
    public string EmployeeDescription { get; init; }
    public int CategoryId { get; init; }
    public decimal Amount { get; init; }
    public DateTime Date { get; init; }
    public ICollection<string> Documents { get; init; }
}
