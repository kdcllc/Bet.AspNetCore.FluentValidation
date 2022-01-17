using Microsoft.EntityFrameworkCore.Design;

namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder EnsureLocalDbCreated(this WebApplicationBuilder builder, string[]? args)
    {
        if (args == null)
        {
            args = Array.Empty<string>();
        }

        var context = new TodoDbDesignFactory().CreateDbContext(args);

        context.Database.EnsureCreated();

        return builder;
    }
}
