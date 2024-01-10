namespace FinalCase.Data.Entities;

public class PendingExpense : Expense
{

    // the class was created to provide single responsibility    
    public int PendingExpenseId { get; set; }
}
