using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Database.Entities.Assignments;

public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.UseTphMappingStrategy();

        builder.HasOne(review => review.Solution)
            .WithOne(solution => solution.Review);

        builder.Ignore(review => review.IsCorrect);
    }
}