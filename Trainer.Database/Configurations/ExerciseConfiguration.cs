using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Database.Entities.Exercises;

namespace Trainer.Database.Configurations;

public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasMany<Subject>(exercise => exercise.Subjects)
            .WithMany();
    }
}