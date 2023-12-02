using Domain.Trainer;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(student => student.UserId);
        builder.HasOne<Rank>(student => student.CurrentRank).WithMany();
        builder.HasOne<User>(student => student.User).WithOne();
    }
}