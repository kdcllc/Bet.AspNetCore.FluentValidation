using Infastructure.Data;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Data.Common;

namespace Sample.FunctionalTests
{
    public class TodoDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;

        public TodoDatabaseFixture()
        {
            Connection = CreateSqliteDatabase();

            Seed();

            Connection.Open();
        }

        public DbConnection Connection { get; }

        public TodoDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new TodoDbContext(new DbContextOptionsBuilder<TodoDbContext>()
                .UseSqlServer(Connection)
                //.UseSqlite(Connection)
                .Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using var context = CreateContext();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    SeedData.PopulateTestData(context);

                    _databaseInitialized = true;
                }
            }
        }

        private static DbConnection CreateSqliteDatabase(bool inMemory = false)
        {
            // "Filename=Test.db"

            return new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=TodoDbTest;Trusted_Connection=True");

            //return new SqliteConnection(inMemory ? "Filename=:memory:" : "Filename=TodoDbTest.db");
        }

        public void Dispose() => Connection?.Dispose();
    }
}
