using Domain;

using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data;

public static class SeedData
{
    public static List<User> Users()
    {
        var id = 1;

        return new List<User>
        {
            new User
            {
                Id = id++,
                Username = "admin",
                Password = "admin",
                Email = "admin@example.com",
                CreatedOn = DateTime.UtcNow,
                Todos = null
            }
        };
    }

    public static List<TodoItem> Items()
    {
        var id = 1;

        return new List<TodoItem>
        {
            new TodoItem
            {
                Id =id++,
                Title = "Todo Item 1",
                IsCompleted = false,
                UserId = 1,
                CreatedOn = DateTime.UtcNow,
                User = null
            }
        };
    }

    public static void PopulateTestData(TodoDbContext context)
    {
        context.Database.OpenConnection();
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
            context.Users.AddRange(Users());
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
        }
        finally
        {
            context.Database.CloseConnection();
        }

        context.Database.OpenConnection();
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.TodoItems ON");
            context.Items.AddRange(Items());
            context.SaveChanges();
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.TodoItems OFF");
        }
        finally
        {
            context.Database.CloseConnection();
        }
    }
}
