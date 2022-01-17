using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infastructure.Data.Configurations;

public class TodoItemConfigutation : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.Property(p => p.Id)
        .UseIdentityColumn()
        .ValueGeneratedOnAdd();

        builder.Property(p => p.Title).HasMaxLength(500).IsRequired();

        // builder.HasData(SeedData.Items());
    }
}
