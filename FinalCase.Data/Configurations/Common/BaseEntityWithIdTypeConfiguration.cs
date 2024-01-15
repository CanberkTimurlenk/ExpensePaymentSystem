using FinalCase.Base.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalCase.Data.Configurations.Common;

public abstract class BaseEntityWithIdTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntityWithId
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
    }
}