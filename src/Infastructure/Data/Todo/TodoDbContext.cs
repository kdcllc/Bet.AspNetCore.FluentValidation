using Domain;

using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> Items { get; set; } // => Set<TodoItem>();

    public DbSet<User>? Users { get; set; } //=> Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
    }
}
