using FinalCase.Base.Entities;

namespace FinalCase.Data.Entities;
public class FieldEmployee : ApplicationUser
{
    // Sahada çalışan personel
    // sisteme masraf girişi yapacak ve geri ödeme talep edecek.

    // mevcut taleplerini görebilecek
    // taleplerinin durumunu takip edebilecek.

    public string IBAN { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; }
    public virtual ICollection<Payment> Payments { get; set; }
}
