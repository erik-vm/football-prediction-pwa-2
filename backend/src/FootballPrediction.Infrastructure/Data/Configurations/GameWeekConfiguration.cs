using FootballPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballPrediction.Infrastructure.Data.Configurations;

public class GameWeekConfiguration : IEntityTypeConfiguration<GameWeek>
{
    public void Configure(EntityTypeBuilder<GameWeek> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasOne(g => g.Tournament)
            .WithMany(t => t.GameWeeks)
            .HasForeignKey(g => g.TournamentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
