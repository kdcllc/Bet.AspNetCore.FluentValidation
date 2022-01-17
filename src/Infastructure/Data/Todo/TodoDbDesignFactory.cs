using Infastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.EntityFrameworkCore.Design;

public class TodoDbDesignFactory : IDesignTimeDbContextFactory<TodoDbContext>
{
    public TodoDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args)
                .Build();

        var builder = new DbContextOptionsBuilder<TodoDbContext>();
        var connectionString = configuration.GetConnectionString("TodoDb");
        builder.UseSqlite(connectionString);

        return new TodoDbContext(builder.Options);
    }
}
