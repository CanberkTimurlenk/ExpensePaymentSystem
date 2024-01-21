using FinalCase.Base.Schema;

namespace FinalCase.Schema.Entity.Responses;

/// <summary>
/// The entity "Payment" was created for the reporting purposes,
/// This class was created as a requirement of the case,
/// Although a controller class is not planned, it is added to complete the requirements.
/// </summary>
public class PaymentResponse : BaseResponse
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string PaymentDescription { get; set; } // will be the payment desc to send banking system "PaymentId"
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
    public int ExpenseId { get; set; }
    public string EmployeeExpenseDescription { get; set; }
    public int PaymentMethodId { get; set; }
    public string PaymentMethodName { get; set; }
}
