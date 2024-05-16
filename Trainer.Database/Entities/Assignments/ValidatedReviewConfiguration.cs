using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Database.Entities.Assignments;

public sealed class ValidatedReviewConfiguration : IEntityTypeConfiguration<ValidatedReview>
{
    public void Configure(EntityTypeBuilder<ValidatedReview> builder)
    {
        builder.HasOne(review => review.Assessment);
    }
}