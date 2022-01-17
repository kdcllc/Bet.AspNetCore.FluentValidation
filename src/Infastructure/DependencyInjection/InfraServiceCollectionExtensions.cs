using Infastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class InfraServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TodoDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            options.UseSqlite(configuration.GetConnectionString("TodoDb"));
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
