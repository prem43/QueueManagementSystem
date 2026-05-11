using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueueManagement.Domain.Entities;

namespace QueueManagement.Persistence.Configurations;

public class TokenTransferConfiguration: IEntityTypeConfiguration<TokenTransfer>
{
    public void Configure(EntityTypeBuilder<TokenTransfer> builder)
    {
        builder.HasOne(t => t.FromCategory)
            .WithMany()
            .HasForeignKey(t => t.FromCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.ToCategory)
            .WithMany()
            .HasForeignKey(t => t.ToCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.FromSubCategory)
            .WithMany()
            .HasForeignKey(t => t.FromSubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.ToSubCategory)
            .WithMany()
            .HasForeignKey(t => t.ToSubCategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(t => t.Token)
            .WithMany()
            .HasForeignKey(t => t.TokenId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}