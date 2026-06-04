using GameTracker.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameTracker.Shared.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.CoverUrl)
            .HasMaxLength(500);

        builder.Property(x => x.LibraryStatus)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.PlayStatus)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(x => x.Modes)
            .HasConversion<int>();

        builder.Property(x => x.PlayedHours)
            .HasDefaultValue(0);

        builder.Property(x => x.IsFavorite)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.Title);

        builder.HasOne(x => x.Platform)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.PlatformId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Launcher)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.LauncherId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}