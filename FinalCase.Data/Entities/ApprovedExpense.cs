namespace FinalCase.Data.Entities;

public class ApprovedExpense : Expense
{
    public int ApprovedAdminId { get; set; }
    public virtual Admin ApprovedAdmin { get; set; }
    public DateTime ApprovalDate { get; set; }
}
