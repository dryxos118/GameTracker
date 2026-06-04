using GameTracker.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameTracker.Shared.Infrastructure.Persistence.Configurations;

public class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder.HasKey(x => new { x.GameId, x.GenreId });

        builder.HasOne(x => x.Game)
            .WithMany(x => x.GameGenres)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Genre)
            .WithMany(x => x.GameGenres)
            .HasForeignKey(x => x.GenreId);
    }
}