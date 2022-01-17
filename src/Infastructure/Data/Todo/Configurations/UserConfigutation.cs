using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.Data.Configurations;

public class Useronfigutation : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.Property(p => p.Id)
        .UseIdentityColumn()
        .ValueGeneratedOnAdd();

        builder.Property(p => p.Username).HasMaxLength(500).IsRequired();

        builder.Property(p => p.Password).HasMaxLength(500).IsRequired();

        builder.Property(p => p.Email).HasMaxLength(500).IsRequired();

        builder.HasMany(x => x.Todos)
               .WithOne(x => x.User)
               .HasForeignKey(k => k.UserId);

        // builder.HasData(SeedData.Users());
    }
}
