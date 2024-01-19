using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Data.Configurations.Common;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{   // The Entity will be used for reporting purposes
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => new { x.EmployeeId, x.ExpenseId });

        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasOne(x => x.Employee).WithMany(x => x.Payments).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Expense).WithOne(x => x.Payment).HasForeignKey<Payment>(x => x.ExpenseId);
        builder.HasOne(x => x.PaymentMethod).WithMany(x => x.Payments).HasForeignKey(x => x.PaymentMethodId).OnDelete(DeleteBehavior.Restrict);


    }
}