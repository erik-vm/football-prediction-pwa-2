using FootballPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballPrediction.Infrastructure.Data.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.HomeTeam).IsRequired().HasMaxLength(100);
        builder.Property(m => m.AwayTeam).IsRequired().HasMaxLength(100);
        builder.Property(m => m.Stage).IsRequired();
        builder.Ignore(m => m.StageMultiplier);

        builder.HasIndex(m => m.GameWeekId);
        builder.HasIndex(m => m.KickoffTime);
        builder.HasIndex(m => m.IsFinished);

        builder.HasOne(m => m.GameWeek)
            .WithMany(g => g.Matches)
            .HasForeignKey(m => m.GameWeekId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
