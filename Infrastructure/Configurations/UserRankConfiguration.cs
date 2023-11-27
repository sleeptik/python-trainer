using Domain.Trainer;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserRankConfiguration: IEntityTypeConfiguration<UserRank>
{
    public void Configure(EntityTypeBuilder<UserRank> builder)
    {
        builder.HasKey(rank => rank.UserId);
        builder.HasOne<Difficulty>().WithMany().HasForeignKey(rank => rank.AssignedDifficultyId);
        builder.HasOne<User>().WithOne();
    }
}