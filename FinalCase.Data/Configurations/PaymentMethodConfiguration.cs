using FinalCase.Data.Configurations.Common;
using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Configurations;
public class PaymentMethodConfiguration : BaseEntityWithIdTypeConfiguration<PaymentMethod>
{
    public override void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(150);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}