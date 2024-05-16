using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trainer.Database.Entities.Assignments;

public sealed class SolutionConfiguration : IEntityTypeConfiguration<Solution>
{
    public void Configure(EntityTypeBuilder<Solution> builder)
    {
        builder.HasOne(solution => solution.Assignment)
            .WithMany(assignment => assignment.Solutions);
    }
}