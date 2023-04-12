using Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TransactionalTestDatabaseFixture
    {
        private const string ConnectionString = @"Server=localhost;" +
                                               "Port=5432;Database=employee_sample;" +
                                               "User Id=postgres;" +
                                               "Password=123;";

        public EmployeeContext CreateContext()
            => new EmployeeContext(
              new DbContextOptionsBuilder<EmployeeContext>()
              .UseNpgsql(ConnectionString)
              .Options);

        public TransactionalTestDatabaseFixture()
        {
            using var context = CreateContext();
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Cleanup();
        }

        public void Cleanup()
        {
            using var context = CreateContext();

            context.Employees.RemoveRange(context.Employees);
            context.AddRange(
              new Employee("Filipe Brito", 27, ""),
              new Employee("Adriane Brito", 27, ""));
            context.SaveChanges();
        }
    }

    [CollectionDefinition("TransactionalTests")]
    public class TransactionalTestsCollection : ICollectionFixture<TransactionalTestDatabaseFixture>
    {

    }
}
