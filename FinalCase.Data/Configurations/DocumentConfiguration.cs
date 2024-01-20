using FinalCase.Data.Configurations.Common;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Configurations;
public class DocumentConfiguration : BaseEntityWithIdTypeConfiguration<Document>
{
    public override void Configure(EntityTypeBuilder<Document> builder)
    {
        // Apply base configuration
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(250);
        builder.Property(x => x.Url).HasMaxLength(150).IsRequired();

        builder.HasOne(x => x.Expense)
               .WithMany(x => x.Documents)
               .HasForeignKey(x => x.ExpenseId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
    }
}