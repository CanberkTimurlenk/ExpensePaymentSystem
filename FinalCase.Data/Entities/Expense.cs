using FinalCase.Base.Entities;
using FinalCase.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Entities;

public class Expense : BaseEntityWithId
{
    // Red olan talepler için bir açıklama alanı girişi sağlanmalı
    // ve talep sahibi masraf talebinin neden red olduğunu görebilmeli.    
    public string? EmployeeDescription { get; set; } // employee could be add description    
    public string? AdminDescription { get; set; } // admin could be add description
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public ExpenseStatus Status { get; set; }
    public string Location { get; set; }

    public int CategoryId { get; set; }
    public virtual ExpenseCategory Category { get; set; }

    public int CreatorEmployeeId { get; set; }
    public virtual ApplicationUser CreatorEmployee { get; set; }

    public Payment? Payment { get; set; }

    public virtual ICollection<Document> Documents { get; set; }
}


public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmployeeDescription).HasMaxLength(500);
        builder.Property(x => x.AdminDescription).HasMaxLength(500);
        //builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        //builder.Property(x => x.Date).IsRequired();
        //builder.Property(x => x.Location).IsRequired().HasMaxLength(200);
        //builder.Property(x => x.Status).IsRequired().HasDefaultValue(ExpenseStatus.Pending);
        builder.HasOne(x => x.Category).WithMany(x => x.Expenses).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.CreatorEmployee).WithMany(x => x.Expenses).HasForeignKey(x => x.CreatorEmployeeId);
    }
}
