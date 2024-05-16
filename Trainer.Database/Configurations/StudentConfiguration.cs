using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trainer.Database.Entities.Auth;
using Trainer.Database.Entities.Students;

namespace Trainer.Database.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(student => student.UserId);
        builder.HasOne<Rank>(student => student.CurrentRank).WithMany();
        builder.HasOne<User>(student => student.User).WithOne();
        builder.HasMany(student => student.Subjects).WithMany();
    }
}