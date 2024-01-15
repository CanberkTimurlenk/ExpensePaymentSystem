
namespace FinalCase.Data.Entities;
public class Payment
{
    // this class used to be created a outgoing payment request to the banking system
    // and also repoting purposes
    public decimal Amount { get; set; }
    public string Description { get; set; } // will be the payment desc to send banking system (Base64("EmployeeId,ExpenseId"))
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string PaymentMethodName { get; set; }
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
    public ApplicationUser Employee { get; set; }
    public int ExpenseId { get; set; }
    public Expense Expense { get; set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}