using GameTracker.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameTracker.Shared.Infrastructure.Persistence.Configurations;

public class GameTagConfiguration : IEntityTypeConfiguration<GameTag>
{
    public void Configure(EntityTypeBuilder<GameTag> builder)
    {
        builder.HasKey(x => new { x.GameId, x.TagId });

        builder.HasOne(x => x.Game)
            .WithMany(x => x.GameTags)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.GameTags)
            .HasForeignKey(x => x.TagId);
    }
}