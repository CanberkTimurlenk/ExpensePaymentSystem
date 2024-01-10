using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;
public class ExpenseCategory : BaseEntityWithId
{
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Expense> Expenses { get; set; }
}
