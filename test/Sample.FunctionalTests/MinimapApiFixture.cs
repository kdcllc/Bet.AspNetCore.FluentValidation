using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

using System;

namespace Sample.FunctionalTests;

public class MinimapApiFixture : WebApplicationFactory<Program>
{
    public MinimapApiFixture()
    {
        // Use HTTPS by default and do not follow
        // redirects so they can tested explicitly.
        ClientOptions.AllowAutoRedirect = false;
        ClientOptions.BaseAddress = new Uri("https://localhost");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        return base.CreateHost(builder);
    }
}
