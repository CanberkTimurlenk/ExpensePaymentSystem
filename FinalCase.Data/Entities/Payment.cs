using FinalCase.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Entities;
public class Payment : BaseEntity
{
    // Onayladıkları ödemeler için anında ödeme işlemi banka entegrasyonu ile
    // gerçekleştirilecek olup çalışan hesabına EFT ile ilgili tutar yatırılacaktır.

    public int ExpenseId { get; set; }
    public virtual Expense Expense { get; set; } // payments are created for expenses

    public DateTime PaymentDate { get; set; } // absolute date of payment
    public int CreatorAdminId { get; set; }
    public virtual ApplicationUser CreatorAdmin { get; set; } // payments are created by admins

}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.ExpenseId);
        builder.HasOne(p => p.Expense).WithOne(e => e.Payment).HasForeignKey<Payment>(p => p.ExpenseId).OnDelete(DeleteBehavior.Restrict);
    }
}