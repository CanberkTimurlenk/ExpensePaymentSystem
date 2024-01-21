using FinalCase.Data.Configurations.Common;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Configurations;
public class ExpenseCategoryConfiguration : BaseEntityWithIdTypeConfiguration<ExpenseCategory>
{
    public override void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        // Apply base configuration
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.HasIndex(x => x.Name).IsUnique();
    }
}
