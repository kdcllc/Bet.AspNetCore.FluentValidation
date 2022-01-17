using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using Xunit;

namespace Sample.FunctionalTests;

public class MinimapApiTests
{
    [Fact]
    public async Task Test1()
    {
        await using var app = new MinimapApiFixture();

        var client = app.CreateClient();

        var response = await client.GetAsync("/weatherforecast");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // var json = await response.Content.ReadAsStringAsync();
        var json = await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(await response.Content.ReadAsStreamAsync());

        Assert.NotNull(json);
        Assert.Equal(5, json?.Count());
    }
}
