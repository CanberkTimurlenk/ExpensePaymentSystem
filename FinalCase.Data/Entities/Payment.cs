using FinalCase.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    public bool IsCompleted { get; set; }
    public bool ReferenceNumber { get; set; } // will be the payment desc to send banking system (Base64("EmployeeId,ExpenseId"))
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.ExpenseId }); // composite PK        
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ReceiverIban).IsRequired().HasMaxLength(26);
        builder.Property(x => x.ReceiverName).IsRequired().HasMaxLength(100);

        builder.HasOne(x => x.Employee).WithMany(x => x.Payments).HasForeignKey(x => x.EmployeeId);

        builder.HasOne(x => x.Expense).WithOne(x => x.Payment).HasForeignKey<Payment>(x => x.ExpenseId);
    }
}