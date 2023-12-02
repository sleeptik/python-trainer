using Domain.Trainer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasKey(history => new { history.StudentId, history.ExerciseId });
    }
}