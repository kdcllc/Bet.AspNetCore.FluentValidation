using Microsoft.EntityFrameworkCore;

using System.Linq;

using Xunit;

namespace Sample.FunctionalTests;

public class TodoDatabaseTest : IClassFixture<TodoDatabaseFixture>
{
    public TodoDatabaseTest(TodoDatabaseFixture fixture) => Fixture = fixture;

    public TodoDatabaseFixture Fixture { get; }

    [Fact]
    public void Can_Get_TodoItems()
    {
        using var transaction = Fixture.Connection.BeginTransaction();
        using var context = Fixture.CreateContext(transaction);

        var items = context.Items.Include(p => p.User).ToList();

        Assert.Single(items);
    }
}
