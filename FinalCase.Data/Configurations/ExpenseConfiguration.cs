using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FinalCase.Data.Configurations.Common;
using FinalCase.Data.Enums;

namespace FinalCase.Data.Configurations;
public class ExpenseConfiguration : BaseEntityTypeConfiguration<Expense>
{
    public override void Configure(EntityTypeBuilder<Expense> builder)
    {
        // Apply base configuration
        base.Configure(builder);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmployeeDescription).HasMaxLength(150);
        builder.Property(x => x.AdminDescription).HasMaxLength(150);
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.Location).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Status).IsRequired().HasDefaultValue(ExpenseStatus.Pending);
        builder.Property(x => x.PaymentMethodId).IsRequired();
        builder.Property(x => x.CreatorEmployeeId).IsRequired();
        builder.Property(x => x.CategoryId).IsRequired();        

        builder.HasOne(x => x.Category).WithMany(x => x.Expenses).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.CreatorEmployee).WithMany(x => x.CreatedExpenses).HasForeignKey(x => x.CreatorEmployeeId);
        builder.HasOne(x => x.PaymentMethod).WithMany(x => x.Expenses).HasForeignKey(x => x.PaymentMethodId);
        builder.HasOne(x => x.Payment).WithOne(x => x.Expense).HasForeignKey<Payment>(x => x.ExpenseId);
    }
}