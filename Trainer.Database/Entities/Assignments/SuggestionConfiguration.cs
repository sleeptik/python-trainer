using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Database.Entities.Assignments;

public sealed class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
{
    public void Configure(EntityTypeBuilder<Suggestion> builder)
    {
        builder.HasOne(suggestion => suggestion.Review)
            .WithMany(review => review.Suggestions);
    }
}