using FinalCase.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalCase.Data.Configurations.Common;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Data.Configurations;
public class ApplicationUserConfiguration : BaseEntityTypeConfiguration<ApplicationUser>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Apply base configuration
        base.Configure(builder);

        builder.Property(x => x.Firstname).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Lastname).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(200);
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.LastActivityDate).IsRequired();
        builder.Property(x => x.Role).IsRequired().HasMaxLength(50);

        builder.HasIndex(x => x.Email).IsUnique();

        builder.Property(x => x.IsActive).IsRequired();
        builder.HasQueryFilter(x => x.IsActive); // Soft delete default filter, to be applied to all queries
        // To ignore the filter, IgnoreQueryFilters() method must be used in the query                
    }
}