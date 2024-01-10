using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;

public class Document : BaseEntityWithId
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }
}
