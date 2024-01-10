using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;
public class Payment : BaseEntityWithId
{
    // Onayladıkları ödemeler için anında ödeme işlemi banka entegrasyonu ile
    // gerçekleştirilecek olup çalışan hesabına EFT ile ilgili tutar yatırılacaktır.

    public int ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }

    public int CreatorAdminId { get; set; }
    public virtual Admin CreatorAdmin { get; set; }

    public int FieldEmployeeId { get; set; }
    public virtual FieldEmployee FieldEmployee { get; set; }
}
