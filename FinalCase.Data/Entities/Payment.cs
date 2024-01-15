
namespace FinalCase.Data.Entities;
public class Payment
{
    // Onayladıkları ödemeler için anında ödeme işlemi banka entegrasyonu ile
    // gerçekleştirilecek olup çalışan hesabına EFT ile ilgili tutar yatırılacaktır.

    // Since the payment will be also used for reporting purposes, the normalization was not considered.
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public string ReceiverIban { get; set; }
    public string ReceiverName { get; set; }
    public string Method { get; set; } // TODO
    public DateTime Date { get; set; }
    public int EmployeeId { get; set; }
    public ApplicationUser Employee { get; set; }
    public int ExpenseId { get; set; }
    public Expense Expense { get; set; }
    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public bool ReferenceNumber { get; set; } // will be the payment desc to send banking system (Base64("EmployeeId,ExpenseId"))
}

